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
        Task AddStyle(AddStyleInput addStyleInput);

        Task<Style> GetStyle(string link);

        Task<List<Style>> GetAllStyles();

        Task UpdateStyle(UpdateStyleInput updateStyleInput);

        Task DeleteStyle(string link);
    }
}
