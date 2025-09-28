using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestaurantCommandHandler : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IDishesRepository _dishesRepository;

        public DeleteDishesForRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository)
        {
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
            _dishesRepository = dishesRepository;
        }
        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Deleting all dishes for restaurant with id : {RestaurantId}", request.RestaurantId);

            var restaurant = await _restaurantsRepository.GetAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

            await _dishesRepository.Delete(restaurant.Dishes);
        }
    }
}
