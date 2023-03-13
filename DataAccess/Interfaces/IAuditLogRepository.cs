using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<Audit>> GetAllAuditsAsync(string TableName = null);
    }
}
