using PaniMusic.Services.Map.NewsletterDtos.Add;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.NewsletterService
{
    public interface INewsletterService
    {
        Task Add(AddNewsletterInput addNewsLetterInput);
    }
}
