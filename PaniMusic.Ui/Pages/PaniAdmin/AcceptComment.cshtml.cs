using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class AcceptCommentModel : PageModel
    {
        private readonly IFeedbackCrud feedbackCrud;

        public AcceptCommentModel(IFeedbackCrud feedbackCrud)
        {
            this.feedbackCrud = feedbackCrud;
        }

        [TempData]
        public bool AcceptComment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            AcceptComment = await feedbackCrud.UpdateAcceptFeedback(id);

            return RedirectToPage("AllComments");
        }
    }
}
