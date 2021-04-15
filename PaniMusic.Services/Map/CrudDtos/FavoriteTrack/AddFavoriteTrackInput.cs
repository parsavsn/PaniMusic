using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.FavoriteTrack
{
    public class AddFavoriteTrackInput
    {
        [Required(ErrorMessage = "وارد کردن شناسه کاربری الزامی است")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "وارد کردن شناسه آهنگ الزامی است")]
        public int TrackId { get; set; }
    }
}
