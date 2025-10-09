using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService : IRestaurantAuthorizationService
    {
        private readonly ILogger<RestaurantAuthorizationService> _logger;
        private readonly IUserContext _userContext;

        public RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }
        public bool Authorize(Restaurant restaurant, ResourceEnum resourceOperation)
        {
            var user = _userContext.GetCurrentUser();

            _logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}", user.Email, resourceOperation, restaurant.Name);

            if (resourceOperation == ResourceEnum.Read || resourceOperation == ResourceEnum.Create)
            {
                _logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceEnum.Delete && user.IsInRole(UserRoles.Admin))
            {
                _logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }

            if ((resourceOperation == ResourceEnum.Delete || resourceOperation == ResourceEnum.Update)
                && user.Id == restaurant.OwnerId)
            {
                _logger.LogInformation("Restaurant owner - successful authorization");
                return true;
            }

            return false;
        }
    }
}
