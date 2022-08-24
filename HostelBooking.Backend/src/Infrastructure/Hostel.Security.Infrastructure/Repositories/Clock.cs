using Hostel.Security.Domain.Repositories;

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
