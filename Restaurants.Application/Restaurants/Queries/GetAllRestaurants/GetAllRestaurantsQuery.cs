using MediatR;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDTO>>
    {
        public string? SearchPhrase { get; set; }
        public int PageSize { get; set; }
        public int PageNumber{ get; set; }

    }
}
