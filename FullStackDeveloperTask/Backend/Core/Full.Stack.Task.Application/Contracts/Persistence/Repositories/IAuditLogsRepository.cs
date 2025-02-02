using Full.Stack.Task.Application.Features.Users.Queries.GetUsers;
using Full.Stack.Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskNamespace = System.Threading.Tasks;

namespace Full.Stack.Task.Application.Contracts.Persistence.Repositories
{
    public interface IAuditLogsRepository
    {
        TaskNamespace.Task AddAsync(AuditLog auditLog, CancellationToken cancellationToken);
    }
}
