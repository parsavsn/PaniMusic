using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.Feedback.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud
{
    public class FeedbackCrud : IFeedbackCrud
    {
        private readonly IRepository<Feedback> feedbackRepository;

        private readonly UserManager<User> userManager;

        private readonly IMapper mapper;

        public FeedbackCrud(IRepository<Feedback> feedbackRepository, UserManager<User> userManager, IMapper mapper)
        {
            this.feedbackRepository = feedbackRepository;

            this.userManager = userManager;

            this.mapper = mapper;
        }

        public async Task<bool> AddFeedback(AddFeedbackInput addFeedbackInput)
        {
            var newFeedback = mapper.Map<Feedback>(addFeedbackInput);

            feedbackRepository.Insert(newFeedback);

            await feedbackRepository.Save();

            return true;
        }

        public async Task<List<Feedback>> UserFeedbacks(string userId)
        {
            var getUserFeedbacks = await feedbackRepository.GetQuery()
                .Include(feedback => feedback.Track)
                .Include(feedback => feedback.Album)
                .Include(feedback => feedback.MusicVideo)
                .Where(feedback => feedback.UserId == userId)
                .ToListAsync();

            return getUserFeedbacks;
        }

        public async Task<List<Feedback>> GetAllFeedbacks()
        {
            var getAllFeedbacks = await feedbackRepository.GetQuery()
                .Include(feedback => feedback.User)
                .Include(feedback => feedback.Track)
                .Include(feedback => feedback.Album)
                .Include(feedback => feedback.MusicVideo)
                .ToListAsync();

            return getAllFeedbacks;
        }

        public async Task<bool> UpdateAcceptFeedback(int id)
        {
            var getFeedback = await feedbackRepository.Get(id);

            if (getFeedback == null)
                return false;

            getFeedback.IsAccept = true;

            feedbackRepository.Update(getFeedback);

            await feedbackRepository.Save();

            return true;
        }

        public async Task<bool> DeleteFeedback(int id)
        {
            var getFeedback = await feedbackRepository.Get(id);

            if (getFeedback == null)
                return false;

            feedbackRepository.Delete(id);

            await feedbackRepository.Save();

            return true;
        }
    }
}
