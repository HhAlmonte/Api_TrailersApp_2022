using Core.Entities;

namespace WebApi.Dto_s
{
    public class RegisterDto
    {
        public RegisterDto(string email, string name, string lastName, string password)
        {
            Email = email;
            Name = name;
            LastName = lastName;
            Password = password;
        }

        public string Email { get; set; }
        public string? UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string? Image { get; set; }
    }
}
