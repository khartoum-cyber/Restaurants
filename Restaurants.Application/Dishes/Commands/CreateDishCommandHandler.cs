using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IDishesRepository _dishesRepository;

        public CreateDishCommandHandler(IMapper mapper, ILogger<CreateRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
            _dishesRepository = dishesRepository;
        }
        public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new dish: {@DishRequest}", request);
            var restaurant = _restaurantsRepository.GetAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

            var dish = _mapper.Map<Dish>(request);
            await _dishesRepository.Create(dish);
        }
    }
}
