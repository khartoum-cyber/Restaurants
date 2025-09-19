using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository : IRestaurantsRepository
    {
        private readonly RestaurantsDbContext _repository;
        public RestaurantsRepository(RestaurantsDbContext repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await _repository.Restaurants.ToListAsync();
            return restaurants;
        }
    }
}
