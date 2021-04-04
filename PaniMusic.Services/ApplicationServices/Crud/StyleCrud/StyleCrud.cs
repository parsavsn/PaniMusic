using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.Style.Add;
using PaniMusic.Services.Map.CrudDtos.Style.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.StyleCrud
{
    public class StyleCrud : IStyleCrud
    {
        private readonly IRepository<Style> styleRepository;

        private readonly IMapper mapper;

        public StyleCrud(IRepository<Style> styleRepository, IMapper mapper)
        {
            this.styleRepository = styleRepository;

            this.mapper = mapper;
        }

        public async Task AddStyle(AddStyleInput addStyleInput)
        {
            var newStyle = mapper.Map<Style>(addStyleInput);

            styleRepository.Insert(newStyle);

            await styleRepository.Save();
        }

        public async Task<Style> GetStyle(string link)
        {
            var getStyle = await styleRepository.GetQuery()
                .FirstOrDefaultAsync(style => style.Link == link);

            if (getStyle == null)
                return null;

            return getStyle;
        }

        public async Task<List<Style>> GetAllStyles()
        {
            return await styleRepository.GetAll();
        }

        public async Task UpdateStyle(UpdateStyleInput updateStyleInput)
        {
            var getStyle = await GetStyle(updateStyleInput.Link);

            var changeStyle = ChangeForUpdate(getStyle, updateStyleInput);

            styleRepository.Update(changeStyle);

            await styleRepository.Save();
        }

        public async Task DeleteStyle(string link)
        {
            var getStyle = await GetStyle(link);

            styleRepository.Delete(getStyle.Id);

            await styleRepository.Save();
        }

        private Style ChangeForUpdate(Style style, UpdateStyleInput input)
        {
            style.Name = input.Name;

            style.Description = input.Description;

            style.Link = input.Link;

            style.TitleTag = input.TitleTag;

            style.MetaDescription = input.MetaDescription;

            style.MetaTag = input.MetaTag;

            return style;
        }
    }
}
