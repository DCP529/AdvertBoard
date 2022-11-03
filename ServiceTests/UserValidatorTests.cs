using Models;
using Models.Validation;
using Xunit;

namespace ValidationsTests
{
    public class UserValidatorTests
    {
        [Fact]
        public void ValidationTest()
        {
            //Arange

            var user = new User()
            {
                IsAdmin = true,
                Name = "Jim"
            };

            var userValidator = new UserValidator();

            //Act

            var validationResult = userValidator.Validate(user);

            //Assert

            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void Validation_ErrorTest()
        {
            //Arange

            var user = new User()
            {
                IsAdmin = true,
                Name = ""
            };

            var userValidator = new UserValidator();

            //Act

            var validationResult = userValidator.Validate(user);

            //Assert

            Assert.True(!validationResult.IsValid);
        }
    }
}