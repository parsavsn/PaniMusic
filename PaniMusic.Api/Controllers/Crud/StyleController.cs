using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.Map.CrudDtos.Style.Add;
using PaniMusic.Services.Map.CrudDtos.Style.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly IStyleCrud styleCrud;

        public StyleController(IStyleCrud styleCrud)
        {
            this.styleCrud = styleCrud;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "NewItem")]
        public async Task<IActionResult> CreateStyle([FromBody] AddStyleInput addStyleInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await styleCrud.AddStyle(addStyleInput);

            return Ok();
        }

        [HttpGet("{link}")]
        public async Task<IActionResult> GetStyleByLink(string link)
        {
            var getStyle = await styleCrud.GetStyleByLink(link);

            if (getStyle == null)
                return NotFound();

            return Ok(getStyle);
        }

        [HttpGet]
        public async Task<IActionResult> GetStyleById([FromQuery] int id)
        {
            var getStyle = await styleCrud.GetStyleById(id);

            if (getStyle == null)
                return NotFound();

            return Ok(getStyle);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStyles()
        {
            var getAllStyles = await styleCrud.GetAllStyles();

            return Ok(getAllStyles);
        }

        [HttpPut]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "EditItem")]
        public async Task<IActionResult> UpdateStyle([FromBody] UpdateStyleInput updateStyleInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updateStyle = await styleCrud.UpdateStyle(updateStyleInput);

            if (updateStyle == false)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "DeleteItem")]
        public async Task<IActionResult> DeleteStyle([FromQuery] int id)
        {
            var deleteStyle = await styleCrud.DeleteStyle(id);

            if (deleteStyle == false)
                return NotFound();

            return Ok();
        }
    }
}
