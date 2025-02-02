using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TasksNamespace = System.Threading.Tasks;

namespace Full.Stack.Task.Application.Events.AuditLogs
{
    public class AuditLogEventHandler(IAuditLogsRepository _auditLogsRepository) : INotificationHandler<AuditLogEvent>
    {
        public async TasksNamespace.Task Handle(AuditLogEvent notification, CancellationToken cancellationToken)
        {
            var auditLog = new AuditLog
            {
                TableName = notification.TableName,
                RecordId = notification.RecordId,
                Action = notification.Action,
                OldValues = notification.OldValues,
                NewValues = notification.NewValues,
                UserId = notification.UserId
            };

            await _auditLogsRepository.AddAsync(auditLog, cancellationToken);
        }
    }
}
