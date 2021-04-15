using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.FavoriteAlbum
{
    public class AddFavoriteAlbumInput
    {
        [Required(ErrorMessage = "وارد کردن شناسه کاربری الزامی است")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "وارد کردن شناسه آلبوم الزامی است")]
        public int AlbumId { get; set; }
    }
}
