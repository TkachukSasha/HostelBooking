using Hostel.Security.Domain.Const;
using Hostel.Security.Domain.ExceptionCodes;
using Hostel.Shared.Types.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel.Security.Domain.ValueObjects
{
    public sealed class Role
    {
        public static IReadOnlyCollection<string> AvailableRoles { get; } = new[] { StringConst.AdminRole, StringConst.UserRole };

        public string Value { get; }

        public Role(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
                throw new HostelException(Codes.InvalidRole,
                    $"Role: {value} is invalid!");

            if (!AvailableRoles.Contains(value))
                throw new HostelException(Codes.InvalidRole,
                    $"Role: {value} is invalid!");

            Value = value;
        }

        public static Role Admin() => new Role(StringConst.AdminRole);
        public static Role User() => new Role(StringConst.UserRole);


        public static implicit operator Role(string value) => value is null ? null : new Role(value);

        public static implicit operator string(Role value) => value?.Value;

        public override string ToString() => Value;
    }
}
