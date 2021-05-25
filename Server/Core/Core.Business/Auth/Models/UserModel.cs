namespace Core.Business.Auth.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
