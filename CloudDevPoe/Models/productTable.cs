using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CloudDevPoe.Models
{
    public class productTable
    {
        public productTable() { }
        public static string con_string = "Server=tcp:cloud-dev-poe-server.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=james;Password=y6EKb9Rz@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static SqlConnection con = new SqlConnection(con_string);

        public int productID { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string category { get; set; }
        public string availability { get; set; }

        public int InsertProduct(productTable t)
        {
            try
            {
                string sqlQuery = "INSERT INTO productTable (productName, productPrice, productCategory, productAvailability) VALUES (@name, @price, @category, @availability)";
                SqlCommand insertProductCmd = new SqlCommand(sqlQuery, con);
                insertProductCmd.Parameters.AddWithValue("@name", t.name);
                insertProductCmd.Parameters.AddWithValue("@price", t.price);
                insertProductCmd.Parameters.AddWithValue("@category", t.category);
                insertProductCmd.Parameters.AddWithValue("@availability", t.availability);
                con.Open();
                int rowsAffected = insertProductCmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<productTable> GetAllProducts()
        {
            List<productTable> products = new List<productTable>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sqlQuery = "SELECT * FROM productTable";
                SqlCommand SelectProductsCmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = SelectProductsCmd.ExecuteReader();
                while (rdr.Read())
                {
                    productTable product = new productTable();
                    product.productID = Convert.ToInt32(rdr["productID"]);
                    product.name = rdr["productName"].ToString();
                    product.price = Convert.ToDecimal(rdr["productPrice"]);
                    product.category = rdr["productCategory"].ToString();
                    product.availability = rdr["productAvailability"].ToString();

                    products.Add(product);
                }
            }

            return products;
        }
    }
}
