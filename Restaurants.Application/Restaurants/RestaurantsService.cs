using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService : IRestaurantsService
    {
        private readonly IRestaurantsRepository _restaurantRepository;
        private readonly ILogger<RestaurantsService> _logger;

        public RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger)
        {
            _restaurantRepository = restaurantsRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurants()
        {
            _logger.LogInformation("Getting all restaurants");
            var restaurants = await _restaurantRepository.GetAllAsync();
            var restaurantsDTO = restaurants.Select(RestaurantDTO.FromEntity);
            return restaurantsDTO!;
        }

        public async Task<RestaurantDTO?> GetRestaurantById(int id)
        {
            _logger.LogInformation($"Getting restaurant with id: {id}");
            var restaurant = await _restaurantRepository.GetAsync(id);
            var restaurantDTO = RestaurantDTO.FromEntity(restaurant);
            return restaurantDTO;
        }
    }
}
