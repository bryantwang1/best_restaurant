namespace BestRestaraunt.Objects
{
    public class Cuisine
    {
        private int _id;
        private string _name;

        public Cuisine(string CuisineName, int Id = 0)
        {
            _name = CuisineName;
        }

        public override bool Equals(System.Object otherCuisine)
        {
            if (!(othercuisine is Cuisine))
            {
                return false;
            }
            else
            {
                Cuising newCuisine = (Cuisine) otherCuisine;
                bool idEquality = this.GetId() == newCuisine.GetId();
                bool nameEquality = this.GetName() == newCuisine.GetName();
                return (idEquality && nameEquality);
            }
        }

        public void SetName(string newName)
        {
            _name = newName;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetId(int newId)
        {
            _id = newId;
        }

        public int GetId()
        {
            return _id;
        }

        public static List<Cuisine> GetAll()
        {
            List<Cuisine> allCuisines = new List<Cuisine> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int cuisineId = rdr.GetInt32(0);
                string cuisineName = rdr.GetString(1);
                Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);
                allCuisines.Add(newCuisine);
            }

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allCuisines;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO cuisines (name) OUTPUT INSERTED.id VALUES (@CuisineName)", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@CuisineName";
            nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);
            Sql DataReader rdr = cmd.ExecuteReader();

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

        public void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM cuisines", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
