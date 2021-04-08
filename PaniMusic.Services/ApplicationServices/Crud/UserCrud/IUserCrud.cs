using Microsoft.AspNetCore.Identity;
using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.User.Add;
using PaniMusic.Services.Map.CrudDtos.User.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.UserCrud
{
    public interface IUserCrud
    {
        Task<IdentityResult> AddUser(AddUserInput addUserInput);

        Task<User> GetUserById(string id);

        Task<List<User>> GetAllUsers();

        Task<IdentityResult> UpdateUser(UpdateUserInput updateUserInput);

        Task<IdentityResult> DeleteUser(string id);
    }
}
