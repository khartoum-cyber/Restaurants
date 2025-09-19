using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDTO>> GetAllRestaurants();
    Task<RestaurantDTO?> GetRestaurantById(int id);
}