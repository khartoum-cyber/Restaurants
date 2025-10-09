using Restaurants.Domain.Entities;
using Restaurants.Domain.Constants;

namespace Restaurants.Domain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceEnum resourceOperation);
    }
}
