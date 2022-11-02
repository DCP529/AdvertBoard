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
        private IWebHostEnvironment _webHostEvironment { get; set; }

        public AdvertController(IWebHostEnvironment webHostEnvironment)
        {
            _advertService = new AdvertService();

            _webHostEvironment = webHostEnvironment;
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

        [HttpPost("AddImageAsync")]
        public async Task<string> AddImageAsync(Guid advertId, IFormFile fileUpload)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    if (fileUpload.Length > 0)
                    {
                        string path = _webHostEvironment.WebRootPath + "\\images\\";

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        using (FileStream fileStream = System.IO.File.Create(path + fileUpload.FileName))
                        {
                            await fileUpload.CopyToAsync(fileStream);

                            await fileStream.FlushAsync();

                            var getAdvert = await _advertService.GetAdvertByIdAsync(advertId);

                            getAdvert.ImageName = path + fileUpload.FileName;

                            await _advertService.UpdateAsync(advertId, getAdvert);

                            return "Image done.";
                        }
                    }
                    else
                    {
                        return "Failed";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            });
        }

        [HttpGet("GetImageAsync")]
        public async Task<IActionResult> GetImageAsync(Guid advertId)
        {
            return await Task.Run(async () =>
            {
                var getAdvert = await _advertService.GetAdvertByIdAsync(advertId);

                var filePath = getAdvert.ImageName;

                if (System.IO.File.Exists(filePath))
                {
                    byte[] reader = System.IO.File.ReadAllBytes(filePath);

                    return File(reader, "image/png");
                }

                return null;
            });
        }
    }
}
