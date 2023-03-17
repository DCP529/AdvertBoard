using Models;
using Services;
using Services.Filters;
using Xunit;


namespace ServicesTests
{
    public class UserServiceTests
    {
        [Fact]
        public async void Get_UsersTest()
        {
            //Arrange

            var userService = new UserService();

            var user = new User()
            {
                Name = "Bob",
                IsAdmin = true,
                AdvertCollection = new List<Advert>()
            };

            //Act

            var getUsers = await userService.GetUserAsync(new UserFilter()
            {
                IsAdmin = user.IsAdmin,
                Name = user.Name
            });

            //Assert

            Assert.Equal(getUsers.Count, 1);
        }

        [Fact]
        public async void Add_UsersTest()
        {
            //Arrange

            var userService = new UserService();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Bob",
                IsAdmin = true,
                AdvertCollection = new List<Advert>()
            };

            //Act

            await userService.AddAsync(user);

            var getUsers = await userService.GetUserAsync(new UserFilter()
            {
                IsAdmin = user.IsAdmin,
                Name = user.Name
            });

            //Assert

            Assert.Equal(getUsers.Count, 1);
        }

        [Fact]
        public async void Update_UsersTest()
        {
            //Arrange

            var userService = new UserService();

            var getUser = await userService.GetUserByIdAsync(Guid.Parse("e673dc38-3b2d-4d3d-9a69-0984a273f1bc"));

            var user = new User()
            {
                Name = "Jery",
                IsAdmin = true,
                AdvertCollection = new List<Advert>()
            };

            //Act 

            await userService.UpdateAsync((Guid)getUser.Id, user);

            getUser = await userService.GetUserByIdAsync(Guid.Parse("e673dc38-3b2d-4d3d-9a69-0984a273f1bc"));


            //Assert

            Assert.Equal(getUser.Id, user.Id);
        }

        [Fact]
        public async void Delete_UsersTest()
        {
            //Arrange

            var userService = new UserService();

            var user = await userService.GetUserByIdAsync(Guid.Parse("e673dc38-3b2d-4d3d-9a69-0984a273f1bc"));

            //Act

            await userService.DeleteAsync((Guid)user.Id);

            user = await userService.GetUserByIdAsync(Guid.Parse("e673dc38-3b2d-4d3d-9a69-0984a273f1bc"));

            //Assert

            Assert.Equal(user, null);
        }
    }
}
