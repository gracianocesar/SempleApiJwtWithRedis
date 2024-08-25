﻿namespace SempleApiJwtWithRedis.Application.Domain
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task<User> Add(User user);
    }
}
