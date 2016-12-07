using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BestRestaurant.Objects;

namespace BestRestaurant
{
    public class RestaurantTest : IDisposable
    {
        public RestaurantTest()
        {
        DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog = best_restaurant_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_RestaurantsEmptyAtFirst()
        {
            int result = Restaurant.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameName()
        {
            Restaurant restaurant1 = new Restaurant("Tako", "Not a place to get tacos.", "$", 1);
            Restaurant restaurant2 = new Restaurant("Tako", "Not a place to get tacos.", "$", 1);

            Assert.Equal(restaurant1, restaurant2);
        }

        [Fact]
        public void Test_Save_SavesRestaurantToDatabase()
        {
            Restaurant testRestaurant = new Restaurant("Tako", "Not a place to get tacos.", "$", 1);
            testRestaurant.Save();

            List<Restaurant> result = Restaurant.GetAll();
            List<Restaurant> testList = new List<Restaurant>{testRestaurant};

            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToRestaurantObject()
        {
            Restaurant testRestaurant = new Restaurant("Tako", "Not a place to get tacos.", "$", 1);
            testRestaurant.Save();

            Restaurant savedRestaurant = Restaurant.GetAll()[0];

            int testId = testRestaurant.GetId();
            int result = savedRestaurant.GetId();

            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindsRestaurantInDatabase()
        {
            Restaurant testRestaurant = new Restaurant("Tako", "Not a place to get tacos.", "$", 1);
            testRestaurant.Save();

            Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

            Assert.Equal(testRestaurant, foundRestaurant);
        }

        [Fact]
        public void Test_Update_UpdatesRestaurantInDatabase()
        {
            string name = "Tako";
            string description = "Not a place to get tacos.";
            string price = "$$";
            int cuisineId = 1;
            Restaurant testRestaurant = new Restaurant(name, description, price, cuisineId);
            testRestaurant.Save();

            string newName = "Mario\'s";
            string newDescription = "Fresh pasta, fresh sauce.";
            string newPrice = "$";
            int newCuisineId = 2;
            testRestaurant.Update(newName, newDescription, newPrice, newCuisineId);

            Restaurant result = Restaurant.GetAll()[0];
            int compareId = result.GetId();
            Restaurant comparisonRestaurant = new Restaurant(newName, newDescription, newPrice, newCuisineId, compareId);

            Assert.Equal(comparisonRestaurant, result);
        }

        [Fact]
        public void Test_Delete_DeletesIndividualRestaurantFromDatabase()
        {
            Restaurant restaurant1 = new Restaurant("Tako", "Not a place to get tacos.", "$", 1);
            Restaurant restaurant2 = new Restaurant("Mario\'s", "Fresh pasta, fresher sauce.", "$$", 1);
            Restaurant restaurant3 = new Restaurant("Peperoncini", "Focused on the flavor of tasty peperoncini.", "$", 1);
            restaurant1.Save();
            restaurant2.Save();
            restaurant3.Save();

            restaurant2.Delete();
            List<Restaurant> expectedRestaurants = new List<Restaurant> { restaurant1, restaurant3 };
            List<Restaurant> restaurantResults = Restaurant.GetAll();

            Assert.Equal(expectedRestaurants, restaurantResults);
        }

        public void Dispose()
        {
            Restaurant.DeleteAll();
            Cuisine.DeleteAll();
        }
    }
}
