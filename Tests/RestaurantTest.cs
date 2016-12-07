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

        // [Fact]
        // public void Test_Update_UpdatesRestaurantInDatabase()
        // {
        //     string name = "Chinese";
        //     Restaurant testRestaurant = new Restaurant(name);
        //     testRestaurant.Save();
        //     string newName = "French";
        //
        //     testRestaurant.Update(newName);
        //
        //     string result = testCategory.GetName();
        //
        //     Assert.Equal(newName, result);
        // }

        public void Dispose()
        {
            Restaurant.DeleteAll();
            Cuisine.DeleteAll();
        }
    }
}
