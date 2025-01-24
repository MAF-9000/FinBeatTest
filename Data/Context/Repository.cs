using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class Repository : IRepository
    {
        private readonly DbSet<DataRecord> _data;
        protected readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _data = _context.DataRecords;
        }

        ///<inheritdoc/>
        public IQueryable<DataRecord> GetAll()
        {
            return _data;
        }

        ///<inheritdoc/>
        public async Task CreateMany(IEnumerable<DataRecord> records)
        {
            _data.AddRange(records);
            await _context.SaveChangesAsync();
        }

        ///<inheritdoc/>
        public async Task Clear()
        {
            var allRecords = GetAll();
            _data.RemoveRange(allRecords);
            await _context.SaveChangesAsync();
        }
    }
}
