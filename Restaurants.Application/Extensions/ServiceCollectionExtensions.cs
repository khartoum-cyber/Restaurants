﻿using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddScoped<IRestaurantsService, RestaurantsService>();
            services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
        }
    }
}
