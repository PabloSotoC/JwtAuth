using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Perficient.Training.JwtAuthentication
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new()
        {
            new User
            {
                Id = Guid.NewGuid(), Name = "John Doe", Email = "john@test.com", Password = "123456",
                CreatedAt = DateTime.Now, Role = UserRole.Reader
            },
            new User
            {
                Id = Guid.NewGuid(), Name = "Karl Marx", Email = "karl@test.com", Password = "qwerty",
                CreatedAt = DateTime.Now, Role = UserRole.Contributor
            },
            new User
            {
                Id = Guid.NewGuid(), Name = "Sammy Silva", Email = "sammy@test.com", Password = "s4mmy",
                CreatedAt = DateTime.Now, Role = UserRole.Manager
            }
        };

        public async Task<IReadOnlyCollection<User>> GetUsersAsync()
        {
            return await Task.FromResult(_users.AsReadOnly());
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await Task.FromResult(_users.FirstOrDefault(user => user.Id.Equals(id)));
        }

        public async Task<User> AuthenticateUser(LoginDto login)
        {

            return await Task.FromResult(_users.FirstOrDefault(user => user.Email.Equals(login.Email) && user.Password.Equals(login.Password) && user.IsActiveRole == true));
        }

        public async Task<User> CreateUserAsync(UserDto user)
        {
            var userToCreate = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = DateTime.Now,
                Role = user.Role
            };
            _users.Add(userToCreate);

            return await Task.FromResult(userToCreate);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await GetUserAsync(id);

            user.IsActiveRole = false;
        }

        public async Task UpdateUserAsync(Guid id, UserDto user)
        {
            var userToUpdate = await GetUserAsync(id);

            userToUpdate.Email = user.Email;
            userToUpdate.Name = user.Name;
            userToUpdate.Password = user.Password;
            userToUpdate.Role = user.Role;
        }
    }
}