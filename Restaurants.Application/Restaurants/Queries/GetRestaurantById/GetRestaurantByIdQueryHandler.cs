using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDTO?>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;

        public GetRestaurantByIdQueryHandler(IMapper mapper, ILogger<CreateRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
        }

        public async Task<RestaurantDTO?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting restaurant with id: {request.Id}");
            var restaurant = await _restaurantsRepository.GetAsync(request.Id);
            var restaurantDTO = _mapper.Map<RestaurantDTO?>(restaurant);
            return restaurantDTO;
        }
    }
}
