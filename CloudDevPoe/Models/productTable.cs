using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CloudDevPoe.Models
{
    public class productTable
    {
        public productTable() { }
        public static string con_string = "Server=tcp:cloud-dev-poe-server.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=james;Password=y6EKb9Rz@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static SqlConnection con = new SqlConnection(con_string);

        public int transactionID { get; set; }
        public int productID { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string category { get; set; }
        public string availability { get; set; }

        public productTable(int productID, string name, decimal price, string category, string availability, int? userID)
        {
            this.transactionID = transactionID;
            this.productID = productID;
            this.name = name;
            this.price = price;
            this.category = category;
            this.availability = availability;
        }

        public int InsertProduct(productTable t, int? userID)
        {
            try
            {
                string sqlQuery = "INSERT INTO productTable (productName, productPrice, productCategory, productAvailability, userID) VALUES (@name, @price, @category, @availability, @userID)";
                SqlCommand insertProductCmd = new SqlCommand(sqlQuery, con);
                insertProductCmd.Parameters.AddWithValue("@name", t.name);
                insertProductCmd.Parameters.AddWithValue("@price", t.price);
                insertProductCmd.Parameters.AddWithValue("@category", t.category);
                insertProductCmd.Parameters.AddWithValue("@availability", t.availability);
                insertProductCmd.Parameters.AddWithValue("@userId", userID);
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

        public static List<productTable> GetUserProducts(int? userID) 
        { 
            List<productTable> userProducts = new List<productTable>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT p.productID, p.productName, p.productPrice, p.productCategory, p.productAvailability FROM productTable p WHERE p.userID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productTable userProduct = new productTable();
                    userProduct.productID = Convert.ToInt32(reader["productID"]);
                    userProduct.name = Convert.ToString(reader["productName"]);
                    userProduct.price = Convert.ToDecimal(reader["productPrice"]);
                    userProduct.category = Convert.ToString(reader["productCategory"]);
                    userProduct.availability = Convert.ToString(reader["productAvailability"]);

                    userProducts.Add(userProduct);
                }
                reader.Close();
            }
            return userProducts;
        }

        public static List<productTable> GetSales(int? userID)
        {
            List<productTable> sales = new List<productTable>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT t.transactionID, p.productID, p.productName, p.productPrice FROM transactionTable t JOIN productTable p ON t.productID = p.productID WHERE p.userID = @UserID;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productTable sale = new productTable();
                    sale.transactionID = Convert.ToInt32(reader["transactionID"]);
                    sale.productID = Convert.ToInt32(reader["productID"]);
                    sale.name = Convert.ToString(reader["productName"]);
                    sale.price = Convert.ToDecimal(reader["productPrice"]);

                    sales.Add(sale);
                }
                reader.Close();
            }
            return sales;
        }
    }

   
}
