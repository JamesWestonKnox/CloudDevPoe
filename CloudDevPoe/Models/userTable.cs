using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CloudDevPoe.Models
{
    public class userTable
    {
        public userTable() { }
        public static string con_string = "Server=tcp:cloud-dev-poe-server.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=james;Password=y6EKb9Rz@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static SqlConnection con = new SqlConnection(con_string);

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public int InsertUser(userTable t)
        {
            try
            {
                string sqlQuery = "INSERT INTO userTable (userName, userSurname, userEmail) VALUES (@Name, @Surname, @Email)";
                SqlCommand insertUserCmd = new SqlCommand(sqlQuery, con);
                insertUserCmd.Parameters.AddWithValue("@Name", t.Name);
                insertUserCmd.Parameters.AddWithValue("@Surname", t.Surname);
                insertUserCmd.Parameters.AddWithValue("@Email", t.Email);
                con.Open();
                int rowsAffected = insertUserCmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
