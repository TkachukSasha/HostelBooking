using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Hostel.Security.Domain.Const
{
    public static class StringConst
    {
        // Templates
        public static readonly Regex EmailTemplate = new Regex(
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.Compiled);

        // Security
        public const int _saltSize = 16;
        public const int _keySize = 32;
        public const int _iterations = 100000;
        public static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;
        public const char segmentDelimiter = ':';

        //Roles
        public static readonly string AdminRole = "admin";
        public static readonly string UserRole = "user";
    }
}
