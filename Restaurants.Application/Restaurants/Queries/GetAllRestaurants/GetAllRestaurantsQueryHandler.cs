using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IRestaurantsRepository _restaurantsRepository;

        public GetAllRestaurantsQueryHandler(IMapper mapper, ILogger<CreateRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
        }
        public async Task<IEnumerable<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all restaurants");
            
            var restaurants = await _restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase);
            var restaurantsDTO = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            return restaurantsDTO!;
        }
    }
}
