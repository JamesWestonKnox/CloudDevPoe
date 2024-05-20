using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using CloudDevPoe.Models;

namespace CloudDevPoe.Controllers
{
    public class TransactionController : Controller
    {
        [HttpPost]
        public ActionResult PlaceOrder(int userID, int productID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(productTable.con_string))
                {
                    string sql = "INSERT INTO transactionTable (userID, productID) VALUES (@userID, @productID)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("userID", userID);
                        cmd.Parameters.AddWithValue("productID", productID);

                        con.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        con.Close();

                        if (rowsAffected > 0)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return View("OrderFailed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}
