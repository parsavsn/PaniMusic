using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.Feedback.Add;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud
{
    public interface IFeedbackCrud
    {
        // we don't need update & get methods for feedback

        Task<bool> AddFeedback(AddFeedbackInput addFeedbackInput);

        Task<List<Feedback>> GetAllFeedbacks();

        Task<bool> UpdateAcceptFeedback(int id);

        Task<bool> DeleteFeedback(int id);
    }
}
