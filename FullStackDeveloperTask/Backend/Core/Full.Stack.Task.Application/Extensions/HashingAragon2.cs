using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Extensions
{
    internal class HashingAragon2
    {
        public async Task<string> Hash(string input, int outputLength = 32)
        {
            byte[] salt = GenerateSalt();
            return await Hash(input, salt, outputLength);
        }

        private async Task<string> Hash(string input, byte[] salt, int outputLength)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            using (var argon2 = new Argon2id(inputBytes))
            {
                argon2.DegreeOfParallelism = 8;
                argon2.MemorySize = 65536;
                argon2.Iterations = 4;
                argon2.Salt = salt;

                byte[] hashBytes = await argon2.GetBytesAsync(outputLength);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = Encoding.UTF8.GetBytes("HI!96255&Iron98^^&stf%uuRRXYZ");
            return salt;
        }
    }
}
