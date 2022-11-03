using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ModelsDb;
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
        public async Task<ActionResult<List<Advert>>> GetAdvertsAsync(AdvertFilter advertFilter)
        {
            var getAdvert = await _advertService.GetAdvertAsync(advertFilter);

            if (getAdvert != null)
            {
                return getAdvert;
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpGet("GetAdvertByIdAsync")]
        public async Task<ActionResult<Advert>> GetAdvertByIdAsync(Guid advertId)
        {
            var getAdvert = await _advertService.GetAdvertByIdAsync(advertId);

            if (getAdvert != null)
            {
                return getAdvert;
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdvertAsync(Advert advert)
        {
            await _advertService.AddAsync(advert);

            var getUser = await _advertService.GetAdvertAsync(new AdvertFilter()
            {
                UserId = advert.UserId,
                Text = advert.Text,
                DateOfCreation = advert.DateOfCreation
            });

            if (getUser != null)
            {
                return Ok();
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdvertAsync(Guid advertId, Advert advert)
        {
            await _advertService.UpdateAsync(advertId, advert);

            var getAdvert = await _advertService.GetAdvertByIdAsync(advertId);

            if (getAdvert.Equals(advert))
            {
                return Ok();
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid advertId)
        {
            await _advertService.DeleteAsync(advertId);

            var getAdvert = await _advertService.GetAdvertAsync(new AdvertFilter() { Id = advertId });

            if (getAdvert.Count != 1)
            {
                return Ok();
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpPost("AddImageAsync")]
        public async Task<IActionResult> AddImageAsync(Guid advertId, IFormFile fileUpload)
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

                        return Ok();
                    }
                }
                else
                {
                    return new BadRequestObjectResult(ModelState);
                }
            }
            catch
            {
                return new BadRequestObjectResult(ModelState);
            }
        }

        [HttpGet("GetImageAsync")]
        public async Task<IActionResult> GetImageAsync(Guid advertId)
        {
            var getAdvert = await _advertService.GetAdvertByIdAsync(advertId);

            var filePath = getAdvert.ImageName;

            if (System.IO.File.Exists(filePath))
            {
                byte[] reader = System.IO.File.ReadAllBytes(filePath);

                return File(reader, "image/png");
            }

            return new BadRequestObjectResult(ModelState);
        }
    }
}
