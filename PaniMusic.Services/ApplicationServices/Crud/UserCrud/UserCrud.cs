using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.DatabaseContext;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.User.Add;
using PaniMusic.Services.Map.CrudDtos.User.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.UserCrud
{
    public class UserCrud : IUserCrud
    {
        private readonly UserManager<User> userManager;

        // We can't use IRepository. For this reason, we use dbcontext.

        private readonly PaniMusicDbContext paniMusicDbContext;

        private readonly IMapper mapper;

        public UserCrud(UserManager<User> userManager, PaniMusicDbContext paniMusicDbContext, IMapper mapper)
        {
            this.userManager = userManager;

            this.paniMusicDbContext = paniMusicDbContext;

            this.mapper = mapper;
        }

        public async Task<IdentityResult> AddUser(AddUserInput addUserInput)
        {
            var mapUser = mapper.Map<User>(addUserInput);

            var newUser = await userManager.CreateAsync(mapUser,addUserInput.Password);

            return newUser;
        }

        public async Task<User> GetUserById(string id)
        {
            var getUser = await this.paniMusicDbContext
                .Set<User>()
                .FirstOrDefaultAsync(user => user.Id == id);

            if (getUser == null)
                return null;

            return getUser;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var getAllUsers = await this.paniMusicDbContext
                .Set<User>()
                .ToListAsync();

            if (getAllUsers == null)
                return null;

            return getAllUsers;
        }  

        public async Task<IdentityResult> UpdateUser(UpdateUserInput updateUserInput)
        {
            var getUser = await GetUserById(updateUserInput.Id);

            getUser.Name = updateUserInput.Name;

            getUser.Email = updateUserInput.Email;

            getUser.PasswordHash = updateUserInput.Password;

            getUser.EmailConfirmed = updateUserInput.EmailConfirmed;

            var updatedUser = await userManager.UpdateAsync(getUser);

            return updatedUser;
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            var getUser = await GetUserById(id);

            var deletedUser = await userManager.DeleteAsync(getUser);

            return deletedUser;
        }
    }
}
