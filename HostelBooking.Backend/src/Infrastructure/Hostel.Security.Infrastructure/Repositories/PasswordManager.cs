using Hostel.Security.Application.Common.Repositories;
using Hostel.Security.Domain.Const;
using System.Security.Cryptography;

namespace Hostel.Security.Infrastructure.Repositories
{
    public class PasswordManager : IPasswordManager
    {
        public string Secure(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(StringConst._saltSize);
            var key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                StringConst._iterations,
                StringConst._algorithm,
                StringConst._keySize
            );

            return string.Join(
                StringConst.segmentDelimiter,
                Convert.ToHexString(key),
                Convert.ToHexString(salt),
                StringConst._iterations,
                StringConst._algorithm
            );
        }

        public bool Validate(string password, string passwordHash)
        {
            var segments = passwordHash.Split(StringConst.segmentDelimiter);
            var key = Convert.FromHexString(segments[0]);
            var salt = Convert.FromHexString(segments[1]);
            var iterations = int.Parse(segments[2]);
            var algorithm = new HashAlgorithmName(segments[3]);

            var inputSecretKey = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                algorithm,
                key.Length
            );

            return key.SequenceEqual(inputSecretKey);
        }
    }
}
