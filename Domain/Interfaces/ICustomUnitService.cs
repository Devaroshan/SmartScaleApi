namespace SmartScaleApi.Domain.Interfaces
{
    using Microsoft.Extensions.AI;
    using SmartScaleApi.Domain.Entities;
    using System.Collections.Generic;

    public interface ICustomUnitService
    {
        void SaveCustomUnit(CustomUnit customUnit);
        Task<ConversionResponse> GetConvertedValue(ConversionRequest request);
        Task<IEnumerable<CustomUnit>> GetAllCustomUnits();
        Task<string[]> CustomUnits(string sample);
    }
}
