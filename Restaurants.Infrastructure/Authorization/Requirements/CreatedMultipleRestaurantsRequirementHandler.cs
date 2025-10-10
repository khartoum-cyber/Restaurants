using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;
        private readonly IUserContext _userContext;
        private readonly IRestaurantsRepository _restaurantsRepository;

        public CreatedMultipleRestaurantsRequirementHandler(IRestaurantsRepository restaurantsRepository ,ILogger<MinimumAgeRequirementHandler> logger, IUserContext userContext)
        {
            _restaurantsRepository = restaurantsRepository;
            _logger = logger;
            _userContext = userContext;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
        {
            var currentUser = _userContext.GetCurrentUser();

            var restaurants = await _restaurantsRepository.GetAllAsync();

            var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser.Id);

            if (userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
