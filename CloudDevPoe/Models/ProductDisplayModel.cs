using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CloudDevPoe.Models
{
    public class ProductDisplayModel
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public string productCategory { get; set; }
        public bool productAvailability { get; set; }

        public ProductDisplayModel() { }

        public ProductDisplayModel(int productID, string productName, decimal productPrice, string productCategory, bool productAvailability)
        {
            this.productID = productID;
            this.productName = productName;
            this.productPrice = productPrice;
            this.productCategory = productCategory;
            this.productAvailability = productAvailability;
        }

        public static List<ProductDisplayModel> SelectProducts()
        {
            List<ProductDisplayModel> products = new List<ProductDisplayModel>();

            string con_string = "Server=tcp:cloud-dev-poe-server.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=james;Password=y6EKb9Rz@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT productID, productName, productPrice, productCategory, productAvailability FROM productTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductDisplayModel product = new ProductDisplayModel();
                    product.productID = Convert.ToInt32(reader["productID"]);
                    product.productName = Convert.ToString(reader["productName"]);
                    product.productPrice = Convert.ToDecimal(reader["productPrice"]);
                    product.productCategory = Convert.ToString(reader["productCategory"]);
                    product.productAvailability = Convert.ToBoolean(reader["productAvailability"]);
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }
    }
}
