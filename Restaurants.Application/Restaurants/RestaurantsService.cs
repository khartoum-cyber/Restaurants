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

        }

        public async Task<RestaurantDTO?> GetRestaurantById(int id)
        {

        }
    }
}
