using Full.Stack.Task.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Domain.GeneralResponse
{
    public class Result { }

    public class Result<T> : Result
    {
        public EResponseStatus Status { get; set; }
        public T Resource { get; set; } = default!;
        public List<string> Messages { get; set; } = [];

        public Result() { }

        public Result(T resource, EResponseStatus status = EResponseStatus.Success, params string[] messages)
        {
            Messages = new List<string>(messages);
            Resource = resource;
            Status = status;
        }

        public Result(EResponseStatus statusCode, params string[] messages)
        {
            Messages = new List<string>(messages);
            Resource = default!;
            Status = statusCode;
        }

        public static Result<T> Success(T resource, params string[] messages) =>
            new(resource, EResponseStatus.Success, messages);

        public static Result<T> Error(params string[] messages) =>
            new(EResponseStatus.Error, messages);

        public static Result<T> Exception(params string[] messages) =>
            new(EResponseStatus.Exception, messages);
        public static Result<T> BadGateway(params string[] messages) =>
            new(EResponseStatus.BadGateway, messages);
    }
}
