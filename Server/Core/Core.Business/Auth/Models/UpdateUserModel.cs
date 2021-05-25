namespace Core.Business.Auth.Models
{
    public class UpdateUserModel : CreateUserModel
    {
        public string Id { get; set; }

        public bool PasswordChangeNeeded => !string.IsNullOrEmpty(Password);
    }
}
