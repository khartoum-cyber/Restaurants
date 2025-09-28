using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler : IRequestHandler<GetDishByIdForRestaurantQuery, DishDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;

        public GetDishByIdForRestaurantQueryHandler(IMapper mapper, ILogger<CreateRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
        }
        public async Task<DishDTO> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting dish: {@DishId} for restaurant with id : {@Restaurant}", request.RestaurantId, request.DishId);

            var restaurant = await _restaurantsRepository.GetAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
            if (dish == null)
                throw new NotFoundException(nameof(dish), request.DishId.ToString());

            var result = _mapper.Map<DishDTO>(dish);

            return result;
        }
    }
}
