namespace Client_Interface_Eventit.Models.Auth
{
    public class LoginUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // User API expects Audience
        public string Audience { get; set; } = string.Empty;
    }
}