using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.FavoriteMusicVideo
{
    public class AddFavoriteMusicVideoInput
    {
        [Required(ErrorMessage = "وارد کردن شناسه کاربری الزامی است")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "وارد کردن شناسه موزیک ویدیو الزامی است")]
        public int MusicVideoId { get; set; }
    }
}
