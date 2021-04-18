using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PaniMusic.Core.Models;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.AccountDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Account
{

    // Account controller services are written in the controller itself due to some problems with onion architecture.

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        private readonly IMapper mapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.userManager = userManager;

            this.signInManager = signInManager;

            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginInput loginInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var signInUser = await signInManager
                .PasswordSignInAsync(loginInput.Email, loginInput.Password, false, false);

            if (!signInUser.Succeeded)
                return Unauthorized("نام کاربری یا رمز عبور اشتباه است");

            var findUser = await userManager.FindByNameAsync(loginInput.Email);

            var userClaimList = await UserClaims(findUser);

            var secretKey = Encoding.UTF8.GetBytes("1234567890asdfgh");

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey)
                , SecurityAlgorithms.HmacSha256Signature);

            var encrytionKey = Encoding.UTF8.GetBytes("qwsadfrewtyh4532");

            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encrytionKey)
                , SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = "",
                Audience = "",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(userClaimList),
                EncryptingCredentials = encryptingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(securityToken);

            return Ok(tokenString);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterInput registerInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var mapNewUser = mapper.Map<User>(registerInput);

            var registerResult = await userManager.CreateAsync(mapNewUser, registerInput.Password);

            if (!registerResult.Succeeded)
            {
                foreach (var error in registerResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            await userManager.AddClaimAsync(mapNewUser, new Claim(PaniClaims.UserPanel, true.ToString()));

            var emailConfirmationToken =
                await userManager.GenerateEmailConfirmationTokenAsync(mapNewUser);

            return Ok(emailConfirmationToken);
        }

        [HttpPost]
        public async Task<IActionResult> EmailConfirmed([FromBody] string userName, string token)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null || user.EmailConfirmed == true)
                return BadRequest();

            await userManager.ConfirmEmailAsync(user, token);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordInput forgetPasswordInput)
        {
            var user = await userManager.FindByNameAsync(forgetPasswordInput.Email);

            if (user == null || user.EmailConfirmed == false)
            {
                return BadRequest();
            }

            var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);

            return Ok(resetPasswordToken);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordInput input)
        {
            var user = await userManager.FindByNameAsync(input.UserName);

            if (user == null)
            {
                return BadRequest();
            }

            var updateResult = await userManager.ResetPasswordAsync(user, input.Token, input.NewPassword);

            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Ok();
        }

        private async Task<List<Claim>> UserClaims(User user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);

            var hasClaims = userClaims.Select(x => x.Type).ToList();

            var claimsList = new List<Claim>();

            claimsList.Add(new Claim(user.UserName, true.ToString()));

            claimsList.Add(new Claim(user.Name, true.ToString()));

            foreach (var item in hasClaims)
            {
                claimsList.Add(new Claim(item, true.ToString()));
            }

            return claimsList;
        }
    }
}