using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.NewsletterMembership;
using PaniMusic.Services.Map.NewsletterDtos.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.NewsletterMembership
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsletterMembershipController : ControllerBase
    {
        private readonly INewsletterMembership newsletterMembership;

        public NewsletterMembershipController(INewsletterMembership newsletterMembership)
        {
            this.newsletterMembership = newsletterMembership;
        }

        [HttpPost]
        public async Task<IActionResult> AddToNewsletterMembership([FromBody] AddNewsletterInput input)
        {
            var addToNewsletterMembership = await newsletterMembership.Add(input);

            if (addToNewsletterMembership == false)
                return BadRequest("ایمیل وارد شده از قبل موجود است");

            return Ok();
        }
    }
}
