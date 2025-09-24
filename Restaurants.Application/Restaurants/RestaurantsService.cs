using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService : IRestaurantsService
    {
        private readonly IRestaurantsRepository _restaurantRepository;
        private readonly ILogger<RestaurantsService> _logger;
        private readonly IMapper _mapper;

        public RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger, IMapper mapper)
        {
            _restaurantRepository = restaurantsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurants()
        {
            _logger.LogInformation("Getting all restaurants");
            var restaurants = await _restaurantRepository.GetAllAsync();
            var restaurantsDTO = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            return restaurantsDTO!;
        }

        public async Task<RestaurantDTO?> GetRestaurantById(int id)
        {
            _logger.LogInformation($"Getting restaurant with id: {id}");
            var restaurant = await _restaurantRepository.GetAsync(id);
            var restaurantDTO = _mapper.Map<RestaurantDTO?>(restaurant);
            return restaurantDTO;
        }
    }
}
