using Hostel.Security.Domain.Repositories;
using Hostel.Shared.Types;

namespace Hostel.Security.Infrastructure.Repositories
{
    public class Clock : IClock
    {
        public DateTime Current()
        {
            return DateTime.UtcNow;
        }
    }
}
