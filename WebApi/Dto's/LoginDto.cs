namespace WebApi.Dto_s
{
    public class loginDto
    {
        public loginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
