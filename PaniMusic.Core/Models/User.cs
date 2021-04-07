using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaniMusic.Core.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public List<Feedback> Feedbacks { get; set; }
    }
}
