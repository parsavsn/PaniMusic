using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin.Comments
{
    public class AllCommentsModel : PageModel
    {
        private readonly IFeedbackCrud feedbackCrud;

        public AllCommentsModel(IFeedbackCrud feedbackCrud)
        {
            this.feedbackCrud = feedbackCrud;
        }

        public List<Feedback> AllFeedbacks { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery]int page = 1)
        {
            // paging

            var getAllFeedbacks = await feedbackCrud.GetAllFeedbacks();

            int skip = (page - 1) * 10;

            int countOfFeedbacks = getAllFeedbacks.Count;

            PageId = page;

            double countOfPages = (double)countOfFeedbacks / 10;

            PageCount = Math.Ceiling(countOfPages);

            AllFeedbacks = getAllFeedbacks.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
