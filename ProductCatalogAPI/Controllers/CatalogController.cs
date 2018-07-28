using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;

namespace ProductCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    public class CatalogController : Controller
    {
        private readonly CatalogContext _catalogContext;

        public CatalogController(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CatalogTypes()
        {
            var items = await _catalogContext.CatalogTypes.ToListAsync();
            return Ok(items);

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CatalogBrands()
        {
            var items = await _catalogContext.CatalogBrands.ToListAsync();
            return Ok(items);
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Items(
            [FromQuery] int pageSize = 6, 
            [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _catalogContext.CatalogItems
                              .LongCountAsync();
            var itemsOnPage = await _catalogContext.CatalogItems
                              .OrderBy(c => c.Name)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            return Ok(itemsOnPage);
        }

        private List<CatalogItem> ChangeUrlPlaceHolder(List<CatalogItem> items)
        {
            items.ForEach(
                x => x.PictureUrl = 
                x.PictureUrl
                .Replace("http://externalcatalogbaseurltobereplaced",
                "http://localhost:6000"));
            return items;
        }

    }
}