using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Services.Filters;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace AdvertBoardAPI.Controllers
{
    [Route("[controller]")]
    public class AdvertController : ControllerBase
    {
        private AdvertService _advertService { get; set; }

        public AdvertController()
        {
            _advertService = new AdvertService();
        }

        [HttpGet]
        public async Task<List<Advert>> GetAdvertsAsync(AdvertFilter advertFilter)
        {
            return await _advertService.GetAdvertAsync(advertFilter);
        }

        [HttpGet("GetAdvertByIdAsync")]
        public async Task<Advert> GetAdvertByIdAsync(Guid advertId)
        {
            return await _advertService.GetAdvertByIdAsync(advertId);
        }

        [HttpPost]
        public async Task AddAdvertAsync(Advert advert)
        {
            await _advertService.AddAsync(advert);
        }

        [HttpPut]
        public async Task UpdateAdvertAsync(Guid advertId, Advert advert)
        {
            await _advertService.UpdateAsync(advertId, advert);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid advertId)
        {
            await _advertService.DeleteAsync(advertId);
        }

    }
}
