﻿using AutoMapper;
using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.Album.Add;
using PaniMusic.Services.Map.CrudDtos.Artist.Add;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaniMusic.Services.Map
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AddAlbumInput, Album>();

            CreateMap<AddArtistInput, Artist>();
        }
    }
}
