using Core.Business.Auth.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Business.Auth.Component
{
    public interface IUserComponent
    {
        Task<CreateUserResult> Create(CreateUserModel user);
        Task<UserModel> GetById(string id);
        Task<List<UserModel>> Search(SearchUsersContext context);
        Task<UpdateUserResult> Update(UpdateUserModel user);
        Task<DeleteUserResult> Delete(string id);
    }
}
