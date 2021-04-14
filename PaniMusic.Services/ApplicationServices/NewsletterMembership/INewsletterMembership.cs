using PaniMusic.Services.Map.NewsletterDtos.Add;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.NewsletterMembership
{
    public interface INewsletterMembership
    {
        Task<bool> Add(AddNewsletterInput addNewsLetterInput);
    }
}
