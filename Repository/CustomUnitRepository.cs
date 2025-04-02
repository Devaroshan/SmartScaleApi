using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using SmartScaleApi.Domain.Entities;
using SmartScaleApi.Domain.Interfaces;

namespace SmartScaleApi.Infrastructure.Data
{
    public class CustomUnitRepository : ICustomUnitRepository
    {
        private readonly AppDbContext _context;

        public CustomUnitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CustomUnit customUnit)
        {
            await _context.CustomUnits.AddAsync(customUnit);
            await _context.SaveChangesAsync();
        }

        public async Task<CustomUnit> GetCustomUnitByUnitAsync(string fromUnit)
        {
            return await _context.CustomUnits.FirstAsync(x => x.Unit == fromUnit);
        }

        public async Task<IEnumerable<CustomUnit>> GetAllAsync()
        {
            return await _context.CustomUnits.ToListAsync();
        }

        public async Task<Sample?> GetSampleByNameAsync(string name)
        {
            return await _context.Samples.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task AddSampleAsync(Sample sample)
        {
            await _context.Samples.AddAsync(sample);
            await _context.SaveChangesAsync();
        }
    }
}