using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Extensions
{
    public static class HasingExtensions
    {
        public static async Task<string> Aragon2(this string input, int outputLength = 32)
        {
            return await new HashingAragon2().Hash(input, outputLength);
        }
    }
}
