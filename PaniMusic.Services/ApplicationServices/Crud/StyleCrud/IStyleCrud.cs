using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.Style.Add;
using PaniMusic.Services.Map.CrudDtos.Style.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.StyleCrud
{
    public interface IStyleCrud
    {
        Task<bool> AddStyle(AddStyleInput addStyleInput);

        Task<Style> GetStyleByLink(string link);

        Task<Style> GetStyleById(int id);

        Task<List<Style>> GetAllStyles();

        Task<bool> UpdateStyle(UpdateStyleInput updateStyleInput);

        Task<bool> DeleteStyle(int id);
    }
}
