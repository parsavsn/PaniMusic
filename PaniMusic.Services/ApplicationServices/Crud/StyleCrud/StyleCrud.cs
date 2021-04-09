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

        public async Task<bool> AddStyle(AddStyleInput addStyleInput)
        {
            var newStyle = mapper.Map<Style>(addStyleInput);

            styleRepository.Insert(newStyle);

            await styleRepository.Save();

            return true;
        }

        public async Task<Style> GetStyleByLink(string link)
        {
            var getStyle = await styleRepository.GetQuery()
                .Include(style => style.Tracks)
                .FirstOrDefaultAsync(style => style.Link == link);

            if (getStyle == null)
                return null;

            return getStyle;
        }

        public async Task<Style> GetStyleById(int id)
        {
            var getStyle = await styleRepository.Get(id);

            if (getStyle == null)
                return null;

            return getStyle;
        }

        public async Task<List<Style>> GetAllStyles()
        {
            return await styleRepository.GetAll();
        }

        public async Task<bool> UpdateStyle(UpdateStyleInput updateStyleInput)
        {
            var getStyle = await styleRepository.Get(updateStyleInput.Id);

            var changeStyle = ChangeForUpdate(getStyle, updateStyleInput);

            styleRepository.Update(changeStyle);

            await styleRepository.Save();

            return true;
        }

        public async Task<bool> DeleteStyle(int id)
        {
            var getStyle = await styleRepository.Get(id);

            if (getStyle == null)
                return false;

            styleRepository.Delete(id);

            await styleRepository.Save();

            return true;
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
