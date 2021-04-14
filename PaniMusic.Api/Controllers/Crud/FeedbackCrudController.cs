using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud;
using PaniMusic.Services.Map.CrudDtos.Feedback.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackCrudController : ControllerBase
    {
        private readonly IFeedbackCrud feedbackCrud;

        public FeedbackCrudController(IFeedbackCrud feedbackCrud)
        {
            this.feedbackCrud = feedbackCrud;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] AddFeedbackInput addFeedbackInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var createFeedback = await feedbackCrud.AddFeedback(addFeedbackInput);

            return Ok(createFeedback);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var getAllFeedbacks = await feedbackCrud.GetAllFeedbacks();

            return Ok(getAllFeedbacks);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAcceptFeedback(int id)
        {
            var getFeedbackForAccept = await feedbackCrud.UpdateAcceptFeedback(id);

            if (getFeedbackForAccept == false)
                return BadRequest();

            return Ok(getFeedbackForAccept);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var deleteFeedback = await feedbackCrud.DeleteFeedback(id);

            if (deleteFeedback == false)
                return BadRequest();

            return Ok(deleteFeedback)
        }
    }
}
