using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetDishForRestaurant
{
    public class GetDishesForRestaurantQuery : IRequest<IEnumerable<DishDTO>>
    {
        public int RestaurantId { get; }
        public GetDishesForRestaurantQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }


    }
}
