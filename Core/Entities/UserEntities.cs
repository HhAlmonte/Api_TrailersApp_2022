using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class UserEntities : IdentityUser
    {
        public UserEntities(string name,
                            string lastName)
        {
            Name = name;
            LastName = lastName;
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Image { get; set; }
    }
}
