using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("AuditLog")]
    public class Audit
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}
