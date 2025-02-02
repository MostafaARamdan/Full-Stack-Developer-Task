using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Authentication.Commands.EmailAuthentication
{
    public class AuthResponse
    {
        public string? Fullname { get; set; }
        public string? Token { get; set; }
    }
}
