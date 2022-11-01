using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ModelsDb;
using Models.Validation;
using Services.Filters;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class UserService
    {
        private AdvertBoardDbContext _advertsBoard;

        public UserService()
        {
            _advertsBoard = new AdvertBoardDbContext();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await Task.Run(async () =>
            {
                var user = await GetUserAsync(new UserFilter()
                {
                    Id = userId
                });

                return user.FirstOrDefault();
            });
        }

        public async Task<List<User>> GetUserAsync(UserFilter userFilter)
        {
            return await Task.Run(() =>
            {
                var query = _advertsBoard.Users.Select(x => x);

                if (userFilter.Id != Guid.Empty)
                {
                    query = query.Where(x => x.Id == userFilter.Id);
                }

                if (userFilter.Name != null)
                {
                    query = query.Where(x => x.Name == userFilter.Name);
                }

                query.Where(x => x.IsAdmin == userFilter.IsAdmin);

                if (userFilter.PageNumber != 0 && userFilter.PageSize != 0)
                {
                    query = query.Skip((userFilter.PageNumber - 1) * userFilter.PageSize).Take(userFilter.PageSize);
                }

                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UserDb, User>();
                    cfg.CreateMap<AdvertDb, Advert>();
                });

                var mapper = new Mapper(mapperConfig);

                return mapper.Map<List<User>>(query.ToList());
            });
        }

        public async Task UpdateAsync(Guid userId, User user)
        {
            user.Id = userId;

            var validator = new UserValidator();

            if (validator.Validate(user).IsValid)
            {
                await Task.Run(async () =>
                {
                    await UserExistsAsync(userId);

                    var getUser = await _advertsBoard.Users.FirstOrDefaultAsync(x => x.Id == userId);

                    var mapperConfig = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<User, UserDb>();
                        cfg.CreateMap<Advert, AdvertDb>();
                    });

                    var mapper = new Mapper(mapperConfig);

                    _advertsBoard.Entry(getUser).CurrentValues.SetValues(mapper.Map<UserDb>(user));

                    await _advertsBoard.SaveChangesAsync();
                });
            }
            else
            {
                throw new ValidationException("Данные для обновления не корректны!");
            }
        }

        public async Task DeleteAsync(Guid userId)
        {
            await Task.Run(async () =>
            {
                await UserExistsAsync(userId);

                var getUser = await _advertsBoard.Users.FirstOrDefaultAsync(x => x.Id == userId);

                var getAdverts = _advertsBoard.Adverts.Where(x => x.Id == userId).ToList();

                _advertsBoard.Users.Remove(getUser);

                if (getAdverts != null)
                {
                    _advertsBoard.Adverts.RemoveRange(getAdverts);
                }

                await _advertsBoard.SaveChangesAsync();
            });
        }

        public async Task AddAsync(User user)
        {
            var validator = new UserValidator();

            if (validator.Validate(user).IsValid)
            {
                await Task.Run(async () =>
                {
                    var getUser = await _advertsBoard.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

                    if (getUser == null)
                    {
                        var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDb>());

                        var mapper = new Mapper(mapperConfig);

                        await _advertsBoard.Users.AddAsync(mapper.Map<UserDb>(user));

                        await _advertsBoard.SaveChangesAsync();
                    }
                    else
                    {
                        throw new ArgumentException("Такой пользователь уже существует!");
                    }
                });
            }
            else
            {
                throw new ValidationException("Данные не корректны для добавления пользователя!");
            }
        }

        public async Task UserExistsAsync(Guid id)
        {
            var getUser = await _advertsBoard.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (getUser == null)
            {
                throw new ArgumentException("Такого пользователя не существует!");
            }
        }
    }
}