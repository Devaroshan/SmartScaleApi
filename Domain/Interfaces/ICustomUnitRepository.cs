namespace SmartScaleApi.Domain.Interfaces
{
    using Microsoft.Extensions.AI;
    using SmartScaleApi.Domain.Entities;
    using System.Collections.Generic;

    public interface ICustomUnitRepository
    {
        Task AddAsync(CustomUnit customUnit);
        Task<CustomUnit> GetCustomUnitByUnitAsync(string fromUnit);
        Task<IEnumerable<CustomUnit>> GetAllAsync();
        Task<Sample?> GetSampleByNameAsync(string name);
        Task AddSampleAsync(Sample sample);
    }
}