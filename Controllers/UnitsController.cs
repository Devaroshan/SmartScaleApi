using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using SmartScaleApi.Domain.Entities;
using SmartScaleApi.Domain.Interfaces;
using SmartScaleApi.Services;

namespace SmartScaleApi.Controllers
{
    [ApiController]
    [Route("api/units")]
    public class UnitsController : ControllerBase
    {
        private readonly ICustomUnitRepository _customUnitRepository;
        private readonly ICustomUnitService _customUnitService;
        private readonly OllamaChatClient _chatClient;

        public UnitsController(ICustomUnitRepository customUnitRepository, ICustomUnitService customUnit, OllamaChatClient chatClient)
        {
            _customUnitRepository = customUnitRepository;
            _customUnitService = customUnit;
            _chatClient = chatClient;
        }

        // Endpoint to save a custom unit
        [HttpPost("save")]
        public async Task<IActionResult> SaveCustomUnit([FromBody] CustomUnit customUnit)
        {
            await _customUnitRepository.AddAsync(customUnit);
            return Ok("Custom unit saved successfully.");
        }

        // Endpoint to evaluate value for a selected unit
        [HttpGet("evaluate")]
        public async Task<IActionResult> EvaluateValue([FromQuery] string sample)
        {
            // Evaluate the value logic here
            var response = await _customUnitService.CustomUnits(sample);
            return Ok(response);
        }

        // Endpoint to convert between two units
        [HttpPost("convert")]
        public async Task<IActionResult> ConvertUnits([FromBody] ConversionRequest request)
        {
            // Conversion logic here
            var convertedResponse = await _customUnitService.GetConvertedValue(request);
            return Ok(convertedResponse);
        }
    }
}
