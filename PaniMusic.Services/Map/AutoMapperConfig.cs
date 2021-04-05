using AutoMapper;
using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.Album.Add;
using PaniMusic.Services.Map.CrudDtos.Artist.Add;
using PaniMusic.Services.Map.CrudDtos.Feedback.Add;
using PaniMusic.Services.Map.CrudDtos.GalleryCategory.Add;
using PaniMusic.Services.Map.CrudDtos.GalleryImage.Add;
using PaniMusic.Services.Map.CrudDtos.MusicVideo.Add;
using PaniMusic.Services.Map.CrudDtos.Style.Add;
using PaniMusic.Services.Map.CrudDtos.Track.Add;
using PaniMusic.Services.Map.NewsletterDtos.Add;
using PaniMusic.Services.Map.RecommendedDtos;
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

            CreateMap<AddFeedbackInput, Feedback>();

            CreateMap<AddGalleryCategoryInput, GalleryCategory>();

            CreateMap<AddGalleryImageInput, GalleryImage>();

            CreateMap<AddMusicVideoInput, MusicVideo>();

            CreateMap<AddStyleInput, Style>();

            CreateMap<AddTrackInput, Track>();

            CreateMap<AddNewsletterInput, Newsletter>();

            CreateMap<Track, RecommendedOutput>()
                .ForMember(x => x.Artist, y => y.MapFrom(z => z.Artist.Name));

            CreateMap<Album, RecommendedOutput>()
                .ForMember(x => x.Artist, y => y.MapFrom(z => z.Artist.Name));

            CreateMap<MusicVideo, RecommendedOutput>()
                .ForMember(x => x.Artist, y => y.MapFrom(z => z.Artist.Name));
        }
    }
}
