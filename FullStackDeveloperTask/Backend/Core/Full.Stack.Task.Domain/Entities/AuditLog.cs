using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TableName { get; set; } 
        public Guid RecordId { get; set; } 
        public string Action { get; set; } 
        public string OldValues { get; set; } 
        public string NewValues { get; set; } 
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; 
        public Guid? UserId { get; set; } 
    }
   
}
