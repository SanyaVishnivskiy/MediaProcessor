using AutoMapper;
using Core.Business.Auth.Models;
using Core.DataAccess.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!result.Succeeded)
            {
                return new CreateUserResult(result);
            }

            var rolesResult = await SetRoles(user, model.Roles);
            return new CreateUserResult(rolesResult);
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

        public async Task<List<UserModel>> Search(SearchUsersContext context)
        {
            var users = await _userManager.Users
                .Where(x => x.EmployeeId.Contains(context.Search))
                .Take(context.PageSize)
                .ToListAsync();

            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<UpdateUserResult> Update(UpdateUserModel model)
        {
            var storedUser = await _userManager.FindByIdAsync(model.Id);
            var user = _mapper.Map(model, storedUser);

            var response = await _userManager.UpdateAsync(user);
            if (!response.Succeeded)
            {
                return new UpdateUserResult(response);
            }

            var rolesResult = await SetRoles(user, model.Roles);
            if (!response.Succeeded || !model.PasswordChangeNeeded)
            {
                return new UpdateUserResult(rolesResult);
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

        private async Task<IdentityResult> SetRoles(User user, List<string> roles)
        {
            if (roles.Count == 0)
            {
                roles.Add("user");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            var rolesIntersection = currentRoles.Intersect(roles);
            var toDelete = currentRoles.Except(rolesIntersection);
            var toAdd = roles.Except(rolesIntersection);

            var deletionResult = await _userManager.RemoveFromRolesAsync(user, toDelete);
            if (!deletionResult.Succeeded)
            {
                return deletionResult;
            }

            return await _userManager.AddToRolesAsync(user, toAdd);
        }

        public async Task<DeleteUserResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            return new DeleteUserResult(result);
        }
    }
}
