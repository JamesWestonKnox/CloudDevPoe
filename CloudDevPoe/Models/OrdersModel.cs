using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CloudDevPoe.Models
{
    public class OrdersModel
    {
        public int transactionID { get; set; }
        public int productID { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public string productCategory { get; set; }


        public OrdersModel() { }

        public OrdersModel(int transactionID, int productID, string productName, decimal productPrice, string productCategory)
        {
            this.transactionID = transactionID;
            this.productID = productID;
            this.productName = productName;
            this.productPrice = productPrice;
            this.productCategory = productCategory;
        }


        public static List<OrdersModel> ShowOrders(int? userID)
        {
            List<OrdersModel> orders = new List<OrdersModel>();
            string con_string = "Server=tcp:cloud-dev-poe-server.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=james;Password=y6EKb9Rz@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection con = new SqlConnection(con_string)) 
            {
                string sql = "SELECT t.transactionID, p.productID, p.productName, p.productPrice, p.productCategory FROM transactionTable t JOIN productTable p ON t.productID = p.productID WHERE t.userID = @UserID;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrdersModel order = new OrdersModel();
                    order.transactionID = Convert.ToInt32(reader["transactionID"]);
                    order.productID = Convert.ToInt32(reader["productID"]);
                    order.productName = Convert.ToString(reader["productName"]);
                    order.productPrice = Convert.ToDecimal(reader["productPrice"]);
                    order.productCategory = Convert.ToString(reader["productCategory"]);

                    orders.Add(order);
                }
                reader.Close();
            }
            return orders;
        }
    }
}

    

