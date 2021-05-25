using AutoMapper;
using Core.Business.Auth.Models;
using Core.DataAccess.Auth;
using System;
using System.Threading.Tasks;

namespace Core.Business.Auth.Component
{
    public class UserComponent : IUserComponent
    {
        private readonly UserManager _userManager;
        private readonly IMapper _mapper;

        public UserComponent(
            UserManager userManager,
            IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CreateUserResult> Create(CreateUserModel model)
        {
            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            return new CreateUserResult(result);
        }

        public async Task<UserModel> GetByEmployeeId(string id)
        {
            var user = await _userManager.FindByNameAsync(id);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<UpdateUserResult> Update(UpdateUserModel model)
        {
            var storedUser = await _userManager.FindByIdAsync(model.Id);
            var user = _mapper.Map(model, storedUser);

            var response = await _userManager.UpdateAsync(user);
            if (!response.Succeeded || !model.PasswordChangeNeeded)
            {
                return new UpdateUserResult(response);
            }

            return await ChangePassword(user, model.Password);
        }

        private async Task<UpdateUserResult> ChangePassword(User user, string password)
        {
            var response = await _userManager.RemovePasswordAsync(user);
            if (!response.Succeeded)
            {
                return new UpdateUserResult(response);
            }

            response = await _userManager.AddPasswordAsync(user, password);
            return new UpdateUserResult(response);
        }
    }
}
