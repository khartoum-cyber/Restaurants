using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishForRestaurant;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/dishes")]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            await _mediator.Send(command);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetAllForRestaurant(int restaurantId)
        {
            var dishes = await _mediator.Send(new GetDishesForRestaurantQuery(restaurantId));

            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDTO?>> GetByIdForRestaurant(int restaurantId, int dishId)
        {
            var dish = await _mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));

            return Ok(dish);
        }
    }
}
