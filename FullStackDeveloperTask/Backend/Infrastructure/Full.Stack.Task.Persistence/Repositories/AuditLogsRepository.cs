using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Domain.Entities;
using Full.Stack.Task.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Persistence.Repositories
{
    
    public class AuditLogsRepository(ApplicationDbContext _context) : IAuditLogsRepository
    {
        public async System.Threading.Tasks.Task AddAsync(AuditLog auditLog, CancellationToken cancellationToken)
        {
            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync(cancellationToken);
        }
       
    }
}
