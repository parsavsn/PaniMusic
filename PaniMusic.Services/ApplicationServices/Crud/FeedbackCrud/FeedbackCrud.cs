using AutoMapper;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.Feedback.Add;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud
{
    public class FeedbackCrud : IFeedbackCrud
    {
        private readonly IRepository<Feedback> feedbackRepository;

        private readonly IMapper mapper;

        public FeedbackCrud(IRepository<Feedback> feedbackRepository, IMapper mapper)
        {
            this.feedbackRepository = feedbackRepository;

            this.mapper = mapper;
        }

        public async Task AddFeedback(AddFeedbackInput addFeedbackInput)
        {
            var newFeedback = mapper.Map<Feedback>(addFeedbackInput);

            feedbackRepository.Insert(newFeedback);

            await feedbackRepository.Save();
        }

        public async Task<List<Feedback>> GetAllFeedbacks()
        {
            return await feedbackRepository.GetAll();
        }

        public async Task DeleteFeedback(int feedbackId)
        {
            feedbackRepository.Delete(feedbackId);

            await feedbackRepository.Save();
        }
    }
}
