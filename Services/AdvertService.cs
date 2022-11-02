using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ModelsDb;
using Models.Validation;
using Services.Filters;

namespace Services
{
    public class AdvertService
    {
        private AdvertBoardDbContext _advertsBoard;

        public AdvertService()
        {
            _advertsBoard = new AdvertBoardDbContext();
        }

        public async Task<Advert> GetAdvertByIdAsync(Guid advertId)
        {
            return await Task.Run(async () =>
            {
                var advert = await GetAdvertAsync(new AdvertFilter()
                {
                    Id = advertId
                });

                return advert.FirstOrDefault();
            });
        }

        public async Task<List<Advert>> GetAdvertAsync(AdvertFilter advertFilter)
        {
            return await Task.Run(() =>
            {
                var query = _advertsBoard.Adverts.Select(x => x);

                if (advertFilter.Id != null && advertFilter.Id != Guid.Empty)
                {
                    query = query.Where(x => x.Id == advertFilter.Id);
                }

                if (advertFilter.ImagePath != null)
                {
                    query = query.Where(x => x.ImagePath == advertFilter.ImagePath);
                }

                if (advertFilter.UserId != Guid.Empty)
                {
                    query = query.Where(x => x.UserId == advertFilter.UserId);
                }

                if (advertFilter.Text != null)
                {
                    query = query.Where(x => x.Text == advertFilter.Text);
                }

                if (advertFilter.Number != 0)
                {
                    query = query.Where(x => x.Number == advertFilter.Number);
                }

                if (advertFilter.Rating != 0)
                {
                    query = query.Where(x => x.Rating == advertFilter.Rating);
                }

                if (advertFilter.DateOfCreation != null)
                {
                    query = query.Where(x => x.DateOfCreation == advertFilter.DateOfCreation);
                }

                if (advertFilter.ExpirationDate != null)
                {
                    query = query.Where(x => x.ExpirationDate <= DateTime.Now && x.ExpirationDate.Day >= DateTime.Now.Day - 7);
                }

                if (advertFilter.PageNumber != 0 && advertFilter.PageSize != 0)
                {
                    query = query.Skip((advertFilter.PageNumber - 1) * advertFilter.PageSize).Take(advertFilter.PageSize);
                }

                var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<AdvertDb, Advert>());

                var mapper = new Mapper(mapperConfig);

                return mapper.Map<List<Advert>>(query.ToList());
            });
        }

        public async Task AdvertExistsAsync(Guid id)
        {
            var getAdvert = await _advertsBoard.Adverts.FirstOrDefaultAsync(x => x.Id == id);

            if (getAdvert == null)
            {
                throw new ArgumentException("Такого объявления не существует!");
            }
        }

        public async Task UpdateAsync(Guid advertId, Advert advert)
        {
            var validator = new AdvertValidator();

            if (validator.Validate(advert).IsValid)
            {
                await Task.Run(async () =>
                {
                    await AdvertExistsAsync(advertId);

                    var getAdvert = await _advertsBoard.Adverts.FirstOrDefaultAsync(x => x.Id == advertId);

                    var mapperConfig = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<User, UserDb>();
                        cfg.CreateMap<Advert, AdvertDb>();
                    });

                    var mapper = new Mapper(mapperConfig);

                    _advertsBoard.Entry(getAdvert).CurrentValues.SetValues(mapper.Map<AdvertDb>(advert));

                    await _advertsBoard.SaveChangesAsync();
                });
            }
            else
            {
                throw new ValidationException("Данные для обновления не корректны!");
            }
        }

        public async Task DeleteAsync(Guid advertId)
        {
            await Task.Run(async () =>
            {
                await AdvertExistsAsync(advertId);

                var getAdverts = _advertsBoard.Adverts.Where(x => x.Id == advertId).FirstOrDefault();

                if (getAdverts != null)
                {
                    _advertsBoard.Adverts.Remove(getAdverts);
                }

                await _advertsBoard.SaveChangesAsync();
            });
        }

        public async Task AddAsync(Advert advert)
        {
            var validator = new AdvertValidator();

            if (validator.Validate(advert).IsValid)
            {
                await Task.Run(async () =>
                {
                    var getAdvert = await _advertsBoard.Adverts.FirstOrDefaultAsync(x => x.Id == advert.Id);

                    if (getAdvert == null)
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<Advert, AdvertDb>();
                            cfg.CreateMap<User, UserDb>();
                        });

                        var mapper = new Mapper(mapperConfig);

                        await _advertsBoard.Adverts.AddAsync(mapper.Map<AdvertDb>(advert));

                        await _advertsBoard.SaveChangesAsync();
                    }
                    else
                    {
                        throw new ArgumentException("Такое объявление уже существует!");
                    }
                });
            }
            else
            {
                throw new ValidationException("Данные не корректны для добавления объявления!");
            }
        }
    }
}
