using Models;
using Models.Validation;
using Xunit;

namespace ValidationsTests
{
    public class AdvertValidatorTests
    {
        [Fact]
        public void Properties_AdvertTest()
        {
            //Arange

            var validator = new AdvertValidator();

            var advert = new Advert()
            {
                Number = 1,
                UserId = Guid.NewGuid(),
                Text = "Это новое объявление",
                Image = @"C:\Users\37377\Desktop\Dex\image.png",
                Rating = 2,
                DateOfCreation = DateTime.Parse("22.10.2022")
            };

            //Act

            var validateResult = validator.Validate(advert);

            //Asset

            Assert.True(validateResult.IsValid);
        }

        [Fact]
        public void Properties_Advert_ErrorTest()
        {
            //Arange

            var validator = new AdvertValidator();

            var advert = new Advert()
            {
                Number = 1,
                UserId = Guid.Empty,
                Text = "Это новое объявление",
                Image = @"C:\Users\37377\Desktop\Dex\image.png",
                Rating = 2,
                DateOfCreation = DateTime.Parse("22.10.2022")
            };

            //Act

            var validateResult = validator.Validate(advert);

            //Asset

            Assert.True(!validateResult.IsValid);
        }
    }
}
