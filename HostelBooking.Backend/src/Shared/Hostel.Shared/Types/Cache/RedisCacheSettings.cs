using StackExchange.Redis;

namespace Hostel.Shared.Types.Cache
{
    public class RedisCacheSettings
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
    }
}
