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

        public override bool Equals(System.Object otherRestaurant)
        {
            if (!(otherRestaurant is Restaurant))
            {
                return false;
            }
            else
            {
                Restaurant newRestaurant = (Restaurant) otherRestaurant;
                bool idEquality = this.GetId() == newRestaurant.GetId();
                bool nameEquality = this.GetName() == newRestaurant.GetName();
                bool descriptionEquality = this.GetDescription() == newRestaurant.GetDescription();
                bool priceEquality = this.GetPrice() == newRestaurant.GetPrice();
                bool cuisineIdEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
                return (idEquality && nameEquality && descriptionEquality && priceEquality && cuisineIdEquality);
            }
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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, description, price, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantDescription, @RestaurantPrice, @RestaurantCuisineId);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@RestaurantName";
            nameParameter.Value = this.GetName();

            SqlParameter descriptionParameter = new SqlParameter();
            descriptionParameter.ParameterName = "@RestaurantDescription";
            descriptionParameter.Value = this.GetDescription();

            SqlParameter priceParameter = new SqlParameter();
            priceParameter.ParameterName = "@RestaurantPrice";
            priceParameter.Value = this.GetPrice();

            SqlParameter cuisineIdParameter = new SqlParameter();
            cuisineIdParameter.ParameterName = "@RestaurantCuisineId";
            cuisineIdParameter.Value = this.GetCuisineId();

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(descriptionParameter);
            cmd.Parameters.Add(priceParameter);
            cmd.Parameters.Add(cuisineIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static List<Restaurant> GetAll()
        {
            List<Restaurant> allRestaurants = new List<Restaurant> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int restaurantId = rdr.GetInt32(0);
                string restaurantName = rdr.GetString(1);
                string restaurantDescription = rdr.GetString(2);
                string restaurantPrice = rdr.GetString(3);
                int restaurantCuisineId = rdr.GetInt32(4);

                Restaurant newRestaurant = new Restaurant(restaurantName, restaurantDescription, restaurantPrice, restaurantCuisineId, restaurantId);
                allRestaurants.Add(newRestaurant);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allRestaurants;
        }

        public static Restaurant Find(int searchId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @SearchId", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@SearchId";
            idParameter.Value = searchId.ToString();
            cmd.Parameters.Add(idParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int restaurantId = 0;
            string restaurantName = null;
            string restaurantDescription = null;
            string restaurantPrice = null;
            int restaurantCuisineId = 0;

            while(rdr.Read())
            {
                restaurantId = rdr.GetInt32(0);
                restaurantName = rdr.GetString(1);
                restaurantDescription = rdr.GetString(2);
                restaurantPrice = rdr.GetString(3);
                restaurantCuisineId = rdr.GetInt32(4);
            }
            Restaurant foundRestaurant = new Restaurant(restaurantName, restaurantDescription, restaurantPrice, restaurantCuisineId, restaurantId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return foundRestaurant;
        }

        public void Update(string restaurantName, string restaurantDescription, string restaurantPrice, int restaurantCuisineId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE restaurants SET name = @NewName, description = @NewDescription, price = @NewPrice, cuisine_id = @NewCuisineId WHERE id = @RestaurantId;", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@NewName";
            nameParameter.Value = restaurantName;
            if(restaurantName == "")
            {
                nameParameter.Value = this.GetName();
            }

            SqlParameter descriptionParameter = new SqlParameter();
            descriptionParameter.ParameterName = "@NewDescription";
            descriptionParameter.Value = restaurantDescription;
            if(restaurantDescription == "")
            {
                descriptionParameter.Value = this.GetDescription();
            }

            SqlParameter priceParameter = new SqlParameter();
            priceParameter.ParameterName = "@NewPrice";
            priceParameter.Value = restaurantPrice;
            if(restaurantPrice == "")
            {
                priceParameter.Value = this.GetPrice();
            }

            SqlParameter cuisineIdParameter = new SqlParameter();
            cuisineIdParameter.ParameterName = "@NewCuisineId";
            cuisineIdParameter.Value = restaurantCuisineId.ToString();
            if(restaurantCuisineId == 0)
            {
                cuisineIdParameter.Value = this.GetCuisineId().ToString();
            }

            SqlParameter restaurantIdParameter = new SqlParameter();
            restaurantIdParameter.ParameterName = "@RestaurantId";
            restaurantIdParameter.Value = this.GetId();

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(descriptionParameter);
            cmd.Parameters.Add(priceParameter);
            cmd.Parameters.Add(cuisineIdParameter);
            cmd.Parameters.Add(restaurantIdParameter);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM restaurants WHERE id = @RestaurantId", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@RestaurantId";
            idParameter.Value = this.GetId();
            cmd.Parameters.Add(idParameter);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }
    }
}
