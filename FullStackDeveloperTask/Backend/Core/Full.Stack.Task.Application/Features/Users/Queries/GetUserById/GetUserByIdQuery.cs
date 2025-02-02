using Full.Stack.Task.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IQuery<GetUserByIdResponse>
    {
        public required Guid Id { get; set; }
    }
}
