using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

        public DeleteRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
            _restaurantAuthorizationService = restaurantAuthorizationService;
        }

        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting restaurant with id : {RestaurantId}", request.Id);
            var restaurant = await _restaurantsRepository.GetAsync(request.Id);
            if (restaurant == null)
                throw new NotFoundException(nameof(restaurant), request.Id.ToString());

            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceEnum.Delete))
                throw new ForbidException();

            await _restaurantsRepository.Delete(restaurant);
        }
    }
}
