using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.AI;
using Newtonsoft.Json.Linq;
using SmartScaleApi.Domain.Entities;
using SmartScaleApi.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace SmartScaleApi.Services
{
    public class CustomUnitService:ICustomUnitService
    {
        private readonly ICustomUnitRepository _customUnitRepository;
        private readonly OllamaChatClient _chatClient;

        public CustomUnitService(ICustomUnitRepository customUnitRepository, OllamaChatClient chatClient)
        {
            _customUnitRepository = customUnitRepository;
            _chatClient = chatClient;
        }

        public void SaveCustomUnit(CustomUnit customUnit)
        {
            _customUnitRepository.AddAsync(customUnit);
        }

        public async Task<ConversionResponse> GetConvertedValue(ConversionRequest request)
        {
            var customUnit = await _customUnitRepository.GetCustomUnitByUnitAsync(request.FromUnit);

            // Replace the placeholder 'x' with the actual numeric value
            string script = customUnit.ConversionFormulae.Replace("x", request.Value.ToString());
            script = script.Replace("/", ".0/");

            // Evaluate the formula using CSharpScript
            var result = await CSharpScript.EvaluateAsync<double>(script, ScriptOptions.Default.WithReferences(typeof(Math).Assembly).WithImports("System"));

            return new ConversionResponse
            {
                Value = result,
                ToUnit = customUnit.StandardUnit,
            };
        }

        public Task<IEnumerable<CustomUnit>> GetAllCustomUnits()
        {
            return _customUnitRepository.GetAllAsync();
        }

        public async Task<string[]> CustomUnits(string sample)
        {
            var sampleUnits = await _customUnitRepository.GetSampleByNameAsync(sample);
            if (sampleUnits == null)
            {
                var prompt = $"Provide the commonly used units for measuring {sample} samples and the formulae to convert each unit to standard unit. Format the response as follows: \"StandardUnit | Unit1: ConversionFormula1| Unit2: ConversionFormula2| ...\". Ensure the conversion formulae are compatible with C# scripting, using expressions like \"Math.Pow(10, -9)*x\" for powers, with x representing unit. Do not include any additional text or explanations.if something irrelevant is given respond wrong.";
                var response = await _chatClient.GetResponseAsync(prompt);
                if (response.Text == "wrong")
                {
                    return null;
                }
                // Parse the response and create CustomUnit and Sample models
                var units = ParseResponse(response.Text);
                var exampleUnits = "";
                var i = 1;
                foreach (var unit in units)
                {
                    exampleUnits = i == units.Capacity ? exampleUnits + unit.Unit : exampleUnits + unit.Unit + ",";
                    await _customUnitRepository.AddAsync(unit);
                    i++;
                }
                await _customUnitRepository.AddSampleAsync(new Sample
                {
                    Name = sample,
                    Units = exampleUnits,
                });
                return exampleUnits.Split(',');
            }
            return sampleUnits.Units.Split(',');           
        }

        private List<CustomUnit> ParseResponse(string response)
        {
            var units = new List<CustomUnit>();
            var unitPairs = response.Split('|');
            var standardUnit = "";
            foreach (var pair in unitPairs)
            {
                var parts = pair.Split(':');
                if (parts.Length == 2 && parts[1].Trim() != "x")
                {
                    var unit = new CustomUnit
                    {
                        Unit = parts[0].Trim(),
                        ConversionFormulae = parts[1].Trim(),
                        StandardUnit = standardUnit
                    };
                    units.Add(unit);
                }
                else
                {
                    standardUnit = parts[0].Trim();
                }
            }
            return units;
        }
    }
}