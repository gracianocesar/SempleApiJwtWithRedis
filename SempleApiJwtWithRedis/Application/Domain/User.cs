namespace SempleApiJwtWithRedis.Application.Domain
{
    public class User(string name, string username, string password)
    {
        public string Name { get; private set; } = name;
        public string Username { get; private set; } = username ?? throw new ArgumentException(nameof(username));
        public string Password { get; private set; } = password ?? throw new ArgumentException(nameof(username));
    }
}
