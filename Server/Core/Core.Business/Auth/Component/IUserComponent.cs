using Core.Business.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Auth.Component
{
    public interface IUserComponent
    {
        Task<CreateUserResult> Create(CreateUserModel user);
        Task<UserModel> GetById(string id);
        Task<UserModel> GetByEmployeeId(string id);
        Task<UpdateUserResult> Update(UpdateUserModel user);
    }
}
