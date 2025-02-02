using Full.Stack.Task.Domain.GeneralResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Messaging
{
    public interface ICommand : IRequest<Result> { }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
}
