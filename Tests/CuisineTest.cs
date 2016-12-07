using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BestRestaurant.Objects;

namespace BestRestaurant
{
    public class CuisineTest : IDisposable
    {
        public CuisineTest()
        {
        DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog = best_restaurant_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_CuisinesEmptyAtFirst()
        {
            int result = Cuisine.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameName()
        {
            Cuisine cuisine1 = new Cuisine("Italian");
            Cuisine cuisine2 = new Cuisine("Italian");

            Assert.Equal(cuisine1, cuisine2);
        }

        [Fact]
        public void Test_Save_SavesCuisineToDatabase()
        {
            Cuisine testCuisine = new Cuisine("Italian");
            testCuisine.Save();

            List<Cuisine> result = Cuisine.GetAll();
            List<Cuisine> testList = new List<Cuisine>{testCuisine};

            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToCuisineObject()
        {
            Cuisine testCuisine = new Cuisine("Italian");
            testCuisine.Save();

            Cuisine savedCuisine = Cuisine.GetAll()[0];

            int testId = testCuisine.GetId();
            int result = savedCuisine.GetId();

            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindsCuisineInDatabase()
        {
            Cuisine testCuisine = new Cuisine("Italian");
            testCuisine.Save();

            Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

            Assert.Equal(testCuisine, foundCuisine);
        }

        public void Dispose()
        {
            Cuisine.DeleteAll();
        }
    }
}
