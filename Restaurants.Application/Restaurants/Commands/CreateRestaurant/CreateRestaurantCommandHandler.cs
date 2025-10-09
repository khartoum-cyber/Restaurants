using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;

        public CreateRestaurantCommandHandler(IMapper mapper, ILogger<CreateRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IUserContext userContext)
        {
            _userContext = userContext;
            _mapper = mapper;
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
        }
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            _logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Restaurant}", currentUser.Email, currentUser.Id ,request);
            var restaurant = _mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;
            int id = await _restaurantsRepository.Create(restaurant);
            return id;
        }
    }
}
