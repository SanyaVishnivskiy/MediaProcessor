using Core.Business.Auth.Models;
using Core.Business.Models;
using System.Threading.Tasks;

namespace Core.Business.Auth.Component
{
    public interface IAuthComponent
    {
        Task<LoginResult> Login(LoginModel model);
    }
}
