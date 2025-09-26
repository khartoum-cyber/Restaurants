using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishRepository : IDishesRepository
    {
        private readonly RestaurantsDbContext _repository;
        public DishRepository(RestaurantsDbContext repository)
        {
            _repository = repository;
        }
        public async Task<int> Create(Dish entity)
        {
            _repository.Dishes.Add(entity);
            await _repository.SaveChangesAsync();
            return entity.Id;
        }
    }
}
