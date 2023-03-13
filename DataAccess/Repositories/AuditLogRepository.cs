using DataAccess.DBContexts;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly NotesDbContext _context;

        public AuditLogRepository(NotesDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Audit>> GetAllAuditsAsync(string TableName = null)
        {
            if (TableName == null)
                return await _context.Audits.ToListAsync();
            else
                return await _context.Audits.Where(x => x.TableName.ToLower() == TableName.ToLower()).ToListAsync();

        }
    }
}
