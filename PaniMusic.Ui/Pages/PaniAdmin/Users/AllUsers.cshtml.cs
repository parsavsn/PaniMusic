using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.UserCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin.Users
{
    [Authorize(Policy = "AdminPanel")]
    public class AllUsersModel : PageModel
    {
        private readonly IUserCrud userCrud;

        public AllUsersModel(IUserCrud userCrud)
        {
            this.userCrud = userCrud;
        }

        public List<User> AllUsers { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery]int page = 1)
        {
            // paging

            var getAllUsers = await userCrud.GetAllUsers();

            int skip = (page - 1) * 10;

            int countOfUsers = getAllUsers.Count;

            PageId = page;

            double countOfPages = (double)countOfUsers / 10;

            PageCount = Math.Ceiling(countOfPages);

            AllUsers = getAllUsers.OrderByDescending(x => x.Email)
                .Skip(skip)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
