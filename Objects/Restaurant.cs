using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurant.Objects
{
    public class Restaurant
    {
        private int _id;
        private string _name;
        private string _description;
        private string _price;
        private int _cuisineId;

        public Restaurant(string restaurantName, string restaurantDescription, string restaurantPrice, int restaurantCuisineId, int restaurantId = 0)
        {
            _id = restaurantId;
            _name = restaurantName;
            _description = restaurantDescription;
            _price = restaurantPrice;
            _cuisineId = restaurantCuisineId;
        }

        public void SetId(int newId)
        {
            _id = newId;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetName(string newName)
        {
            _name = newName;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetDescription(string newDescription)
        {
            _description = newDescription;
        }

        public string GetDescription()
        {
            return _description;
        }

        public void SetPrice(string newPrice)
        {
            _price = newPrice;
        }

        public string GetPrice()
        {
            return _price;
        }

        public void SetCuisineId(int newCuisineId)
        {
            _cuisineId = newCuisineId;
        }

        public int GetCuisineId()
        {
            return _cuisineId;
        }
    }
}
