namespace Core.Business.Auth.Models
{
    public class LoginResult
    {
        public bool Succeeded { get; init; }
        public string Token { get; init; }

        public static LoginResult CreateSucceeded(string token)
        {
            return new LoginResult
            {
                Succeeded = true,
                Token = token
            };
        }

        public static LoginResult CreateFailed()
        {
            return new LoginResult
            {
                Succeeded = false
            };
        }
    }
}
