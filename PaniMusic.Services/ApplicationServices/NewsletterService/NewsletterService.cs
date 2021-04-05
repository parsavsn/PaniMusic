using AutoMapper;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.NewsletterDtos.Add;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.NewsletterService
{
    public class NewsLetterService : INewsletterService
    {
        private readonly IRepository<Newsletter> newsletterRepository;

        private readonly IMapper mapper;

        public NewsLetterService(IRepository<Newsletter> newsletterRepository, IMapper mapper)
        {
            this.newsletterRepository = newsletterRepository;

            this.mapper = mapper;
        }

        public async Task Add(AddNewsletterInput addNewsLetterInput)
        {
            var newNewsletter = mapper.Map<Newsletter>(addNewsLetterInput);

            newsletterRepository.Insert(newNewsletter);

            await newsletterRepository.Save();
        }
    }
}
