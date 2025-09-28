using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishForRestaurant
{
    public class GetDishesForRestaurantQueryHandler : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IDishesRepository _dishesRepository;

        public GetDishesForRestaurantQueryHandler(IMapper mapper, ILogger<CreateRestaurantCommandHandler> logger,IRestaurantsRepository restaurantsRepository , IDishesRepository dishesRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
            _dishesRepository = dishesRepository;
        }

        public async Task<IEnumerable<DishDTO>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all dishes for restaurant with id : {@Restaurant}", request.RestaurantId);

            var restaurant = await _restaurantsRepository.GetAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

            var results = _mapper.Map<IEnumerable<DishDTO>>(restaurant.Dishes);

            return results;
        }
    }
}
