using System;


namespace Perficient.Training.JwtAuthentication
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserRole Role { get; set; }
        public bool IsActiveRole { get; set; } = true;
    }
}
