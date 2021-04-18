using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.UserCrud;
using PaniMusic.Services.Map.CrudDtos.User.Add;
using PaniMusic.Services.Map.CrudDtos.User.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserCrudController : ControllerBase
    {
        private readonly IUserCrud userCrud;

        public UserCrudController(IUserCrud userCrud)
        {
            this.userCrud = userCrud;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "NewItem")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserInput addUserInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newUserResult = await userCrud.AddUser(addUserInput);

            foreach (var error in newUserResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "AdminPanel")]
        public async Task<IActionResult> GetUserById([FromQuery] string id)
        {
            var getUser = await userCrud.GetUserById(id);

            if (getUser == null)
                return NotFound();

            return Ok(getUser);
        }

        [HttpGet]
        [Authorize(Policy = "AdminPanel")]
        public async Task<IActionResult> GetAllUsers()
        {
            var getAllUsers = await userCrud.GetAllUsers();

            return Ok(getAllUsers);
        }

        [HttpPut]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "EditItem")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserInput updateUserInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updateUserResult = await userCrud.UpdateUser(updateUserInput);

            foreach (var error in updateUserResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "DeleteItem")]
        public async Task<IActionResult> DeleteUser([FromQuery] string id)
        {
            var deleteUserResult = await userCrud.DeleteUser(id);

            foreach (var error in deleteUserResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            return Ok();
        }
    }
}
