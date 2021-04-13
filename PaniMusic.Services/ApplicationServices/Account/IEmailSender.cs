using Microsoft.AspNetCore.Identity;
using PaniMusic.Services.Map.AccountDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Account
{
    public interface IEmailSender
    {
        public Task SendEmail(string toEmail, string subject, string message);
    }
}
