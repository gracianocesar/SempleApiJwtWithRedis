using Microsoft.Extensions.Caching.Distributed;
using SempleApiJwtWithRedis.Application.Domain;
using System.Text.Json;


namespace SempleApiJwtWithRedis.Application.Repositories
{      

    public class UserRepository(IDistributedCache distributedCache) : IUserRepository
    {

        private readonly IDistributedCache _distributedCache = distributedCache ?? throw new ArgumentException(nameof(distributedCache));

        public async Task<User> Add(User user)
        {
            await _distributedCache.SetStringAsync(user.Username, JsonSerializer.Serialize(user));
            return user;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var data = await _distributedCache.GetStringAsync(username);

            if (string.IsNullOrEmpty(data)) return null;

            var user = JsonSerializer.Deserialize<User>(data);
            return user;
        }
    }
}
