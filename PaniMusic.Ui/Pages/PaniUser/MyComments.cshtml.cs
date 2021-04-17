using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud;

namespace PaniMusic.Ui.Pages.PaniUser
{
    [Authorize(Policy = "UserPanel")]
    public class MyCommentsModel : PageModel
    {
        private readonly IFeedbackCrud feedbackCrud;

        private readonly UserManager<User> userManager;

        public MyCommentsModel(IFeedbackCrud feedbackCrud, UserManager<User> userManager)
        {
            this.feedbackCrud = feedbackCrud;

            this.userManager = userManager;
        }

        public List<Feedback> Feedbacks { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] int page = 1)
        {
            var currentUser = await userManager.GetUserAsync(User);

            var userFeedbacks = await feedbackCrud.UserFeedbacks(currentUser.Id);

            int skip = (page - 1) * 10;

            int countOfFeedbacks = userFeedbacks.Count;

            PageId = page;

            double countOfPages = (double)countOfFeedbacks / 10;

            PageCount = Math.Ceiling(countOfPages);

            Feedbacks = userFeedbacks.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
