using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Events.AuditLogs
{
    public class AuditLogEvent : INotification
    {
        public string TableName { get; }
        public Guid RecordId { get; }
        public string Action { get; }
        public string OldValues { get; }
        public string NewValues { get; }
        public Guid? UserId { get; }

        public AuditLogEvent(string tableName, Guid recordId, string action, string oldValues, string newValues, Guid? userId)
        {
            TableName = tableName;
            RecordId = recordId;
            Action = action;
            OldValues = oldValues;
            NewValues = newValues;
            UserId = userId;
        }
    }
}
