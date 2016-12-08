using System.Collections.Generic;
using System;
using Nancy;
using BestRestaurant.Objects;

namespace BestRestaurant
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                List<Restaurant> allRestaurants = Restaurant.GetAll();
                return View["index.cshtml", allRestaurants];
            };

            Get["/cuisines/new"] = _ => {
                return View["cuisine_form.cshtml"];
            };

            Post["/cuisines/added"] = _ => {
                string cuisineName = Request.Form["cuisine-name"];

                Cuisine newCuisine = new Cuisine(cuisineName);
                return View["cuisine_added.cshtml", newCuisine];
            };

            Get["/restaurants/new"] = _ => {
                List<Cuisine> allCuisines = Cuisine.GetAll();
                return View["restaurant_form.cshtml", allCuisines];
            };

            Post["/restaurants/added"] = _ => {
                string restaurantName = Request.Form["restaurant-name"];
                string restaurantDescription = Request.Form["restaurant-description"];
                string restaurantPrice = Request.Form["restaurant-price"];
                int restaurantCuisineId = Request.Form["restaurant-cuisine"];

                Restaurant newRestaurant = new Restaurant(restaurantName, restaurantDescription, restaurantPrice, restaurantCuisineId);
                newRestaurant.Save();
                return View["restaurant_added.cshtml", newRestaurant];
            };

            Get["/cuisines"] = _ => {
                List<Cuisine> allCuisines = Cuisine.GetAll();
                return View["cuisines.cshtml", allCuisines];
            };
        }
    }
}
