using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin.Comments
{
    [Authorize(Policy = "AdminPanel")]
    [Authorize(Policy = "DeleteItem")]
    public class DeleteCommentModel : PageModel
    {
        private readonly IFeedbackCrud feedbackCrud;

        public DeleteCommentModel(IFeedbackCrud feedbackCrud)
        {
            this.feedbackCrud = feedbackCrud;
        }

        [TempData]
        public bool DeleteComment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeleteComment = await feedbackCrud.DeleteFeedback(id);

            return RedirectToPage("AllComments");
        }
    }
}
