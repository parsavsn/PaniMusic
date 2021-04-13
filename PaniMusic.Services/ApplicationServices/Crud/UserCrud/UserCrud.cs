using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.DatabaseContext;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.CrudDtos.User.Add;
using PaniMusic.Services.Map.CrudDtos.User.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

            mapUser.EmailConfirmed = true;

            var newUser = await userManager.CreateAsync(mapUser, addUserInput.Password);

            if (addUserInput.UserPanel == true)
                await userManager.AddClaimAsync(mapUser, new Claim(PaniClaims.UserPanel, true.ToString()));

            if (addUserInput.AdminPanel == true)
                await userManager.AddClaimAsync(mapUser, new Claim(PaniClaims.AdminPanel, true.ToString()));

            if (addUserInput.NewItem == true)
                await userManager.AddClaimAsync(mapUser, new Claim(PaniClaims.NewItem, true.ToString()));

            if (addUserInput.EditItem == true)
                await userManager.AddClaimAsync(mapUser, new Claim(PaniClaims.EditItem, true.ToString()));

            if (addUserInput.DeleteItem == true)
                await userManager.AddClaimAsync(mapUser, new Claim(PaniClaims.DeleteItem, true.ToString()));

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

        public async Task<IdentityResult> UpdateUser(UpdateUserInput input)
        {
            var getUser = await GetUserById(input.Id);

            getUser.Name = input.Name;

            getUser.UserName = input.Email;

            await userManager.UpdateNormalizedUserNameAsync(getUser);

            getUser.Email = input.Email;

            await userManager.UpdateNormalizedEmailAsync(getUser);

            if (!string.IsNullOrEmpty(input.Password))
            {
                await userManager.RemovePasswordAsync(getUser);

                await userManager.AddPasswordAsync(getUser, input.Password);
            }

            getUser.EmailConfirmed = input.EmailConfirmed;

            var updatedUser = await userManager.UpdateAsync(getUser);

            await UpdateClaims(getUser
                , input.UserPanel
                , input.AdminPanel
                , input.NewItem
                , input.EditItem
                , input.DeleteItem);

            return updatedUser;
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            var getUser = await GetUserById(id);

            var deletedUser = await userManager.DeleteAsync(getUser);

            return deletedUser;
        }

        private async Task UpdateClaims(User user
            , bool userPanel
            , bool adminPanel
            , bool newItem
            , bool editItem
            , bool deleteItem)
        {
            var getUserClaim = await userManager.GetClaimsAsync(user);

            if (userPanel == true)
            {
                var hasClaim = getUserClaim.FirstOrDefault(x => x.Type == "UserPanel");

                if (hasClaim == null)
                    await userManager.AddClaimAsync(user, new Claim(PaniClaims.UserPanel, true.ToString()));
            }
            else
                await userManager.RemoveClaimAsync(user, new Claim(PaniClaims.UserPanel, true.ToString()));

            if (adminPanel == true)
            {
                var hasClaim = getUserClaim.FirstOrDefault(x => x.Type == "AdminPanel");

                if (hasClaim == null)
                    await userManager.AddClaimAsync(user, new Claim(PaniClaims.AdminPanel, true.ToString()));
            }
            else
                await userManager.RemoveClaimAsync(user, new Claim(PaniClaims.AdminPanel, true.ToString()));

            if (newItem == true)
            {
                var hasClaim = getUserClaim.FirstOrDefault(x => x.Type == "NewItem");

                if (hasClaim == null)
                    await userManager.AddClaimAsync(user, new Claim(PaniClaims.NewItem, true.ToString()));
            }
            else
                await userManager.RemoveClaimAsync(user, new Claim(PaniClaims.NewItem, true.ToString()));

            if (editItem == true)
            {
                var hasClaim = getUserClaim.FirstOrDefault(x => x.Type == "EditItem");

                if (hasClaim == null)
                    await userManager.AddClaimAsync(user, new Claim(PaniClaims.EditItem, true.ToString()));
            }
            else
                await userManager.RemoveClaimAsync(user, new Claim(PaniClaims.EditItem, true.ToString()));

            if (deleteItem == true)
            {
                var hasClaim = getUserClaim.FirstOrDefault(x => x.Type == "DeleteItem");

                if (hasClaim == null)
                    await userManager.AddClaimAsync(user, new Claim(PaniClaims.DeleteItem, true.ToString()));
            }
            else
                await userManager.RemoveClaimAsync(user, new Claim(PaniClaims.DeleteItem, true.ToString()));
        }
    }
}
