using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Perficient.Training.JwtAuthentication
{
    public interface IUserService
    {
        Task<IReadOnlyCollection<User>> GetUsersAsync();
        Task<User> GetUserAsync(Guid id);
        Task<User> CreateUserAsync(UserDto user);
        Task DeleteUserAsync(Guid id);
        Task UpdateUserAsync(Guid id, UserDto user);
    }
}
