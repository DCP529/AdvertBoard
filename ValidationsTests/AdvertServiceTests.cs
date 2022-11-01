using Models;
using Services.Filters;
using Services;
using Xunit;
using Models.ModelsDb;


namespace ServicesTests
{
    public class AdvertServiceTests
    {
        [Fact]
        public async void Get_AdvertTest()
        {
            //Arrange

            var advertService = new AdvertService();

            //Act

            var getAdvert = await advertService.GetAdvertByIdAsync(Guid.Parse("5415d2ae-a011-4bb2-993d-54a22c21444b"));

            //Assert

            Assert.NotNull(getAdvert);
        }

        [Fact]
        public async void Add_AdvertTest()
        {
            //Arrange

            var advertService = new AdvertService();

            var advert = new Advert()
            {
                Id = Guid.NewGuid(),
                Number = 1,
                UserId = Guid.Parse("c4777863-825f-482d-a3b3-2cb52cd3dfa5"),
                Text = "Это новое объявление",
                ImagePath = @"C:\Users\37377\Desktop\Dex\image.png",
                Rating = 2,
                DateOfCreation = DateTime.Parse("22.10.2022")
            };

            //Act

            await advertService.AddAsync(advert);

            var getAdvert = await advertService.GetAdvertByIdAsync((Guid)advert.Id);

            //Assert

            Assert.Equal(getAdvert.Number, 1);
        }

        [Fact]
        public async void Update_AdvertTest()
        {
            //Arrange

            var advertService = new AdvertService();

            var getAdvert = await advertService.GetAdvertByIdAsync(Guid.Parse("5415d2ae-a011-4bb2-993d-54a22c21444b"));

            //Act

            getAdvert.Rating = 8;

            await advertService.UpdateAsync((Guid)getAdvert.Id, getAdvert);

            getAdvert = await advertService.GetAdvertByIdAsync(Guid.Parse("5415d2ae-a011-4bb2-993d-54a22c21444b"));

            //Assert

            Assert.Equal(getAdvert.Rating, 8);
        }

        [Fact]
        public async void Delete_AdvertTest()
        {
            //Arrange

            var advertService = new AdvertService();

            var getAdvert = await advertService.GetAdvertByIdAsync(Guid.Parse("5415d2ae-a011-4bb2-993d-54a22c21444b"));

            //Act

            await advertService.DeleteAsync((Guid)getAdvert.Id);

            getAdvert = await advertService.GetAdvertByIdAsync(Guid.Parse("5415d2ae-a011-4bb2-993d-54a22c21444b"));

            //Assert

            Assert.Equal(getAdvert, null);
        }
    }
}
