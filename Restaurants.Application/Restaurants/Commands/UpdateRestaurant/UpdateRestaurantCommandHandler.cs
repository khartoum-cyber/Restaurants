using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

        public UpdateRestaurantCommandHandler(IMapper mapper, ILogger<CreateRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _mapper = mapper;
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
            _restaurantAuthorizationService = restaurantAuthorizationService;
        }

        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurant = await _restaurantsRepository.GetAsync(request.Id);
            if (restaurant == null)
                throw new NotFoundException(nameof(restaurant), request.Id.ToString());

            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceEnum.Update))
                throw new ForbidException();

            _mapper.Map(request, restaurant);

            await _restaurantsRepository.SaveChanges();

        }
    }
}
