using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PaniMusic.Core.Models;
using PaniMusic.Services.Map.AccountDtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Account
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmail(string toEmail, string subject, string message)
        {
            using (var client = new SmtpClient())
            {

                var credentials = new NetworkCredential()
                {
                    UserName = "",
                    Password = ""
                };

                client.Credentials = credentials;
                client.Host = "";
                client.Port = 587;
                client.EnableSsl = true;

                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(toEmail) },
                    From = new MailAddress(""),
                    Subject = subject,
                    Body = message,
                };

                client.Send(emailMessage);
            }

            return Task.CompletedTask;
        }
    }
}
