﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Account;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.AccountDtos;

namespace PaniMusic.Ui.Pages.Account
{
    [ValidateAntiForgeryToken]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> userManager;

        private readonly IEmailSender account;

        private readonly IMapper mapper;

        public RegisterModel(UserManager<User> userManager, IEmailSender account, IMapper mapper)
        {
            this.userManager = userManager;

            this.account = account;

            this.mapper = mapper;
        }

        [BindProperty]
        public RegisterInput Input { get; set; }

        public IdentityResult Result { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var mapNewUser = mapper.Map<User>(Input);

                Result = await userManager.CreateAsync(mapNewUser, Input.Password);

                if (Result.Succeeded)
                {
                    await userManager.AddClaimAsync(mapNewUser, new Claim(PaniClaims.UserPanel , true.ToString()));

                    var emailConfirmationToken =
                        await userManager.GenerateEmailConfirmationTokenAsync(mapNewUser);

                    var emailMessage =
                        Url.Page("EmailConfirmed",
                        values: new { username = mapNewUser.UserName, token = emailConfirmationToken },
                        pageHandler: null,
                        protocol: Request.Scheme
                        );

                    await account.SendEmail(Input.Email, "تایید ایمیل کاربری در پانی موزیک", emailMessage);

                    return Page();
                }

                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                    return Page();
                }
            }

            return Page();
        }
    }
}
