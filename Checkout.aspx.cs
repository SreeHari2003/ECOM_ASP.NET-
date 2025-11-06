using System;
using System.Data.SqlClient;

namespace theProject
{
    public partial class Checkout : System.Web.UI.Page
    {
        string connectionString = @"Server=DESKTOP-7PHPOBO\SQLEXPRESS;Database=theDatabase;Integrated Security=True;";
        int userId = 1; // Replace with Session["UserID"]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT SUM(p.Price*c.Quantity) FROM Cart c JOIN Products p ON c.ProductID=p.ProductID WHERE c.UserID=@u", con);
                    cmd.Parameters.AddWithValue("@u", userId);
                    con.Open();
                    lblTotal.Text = "Total: ₹" + Convert.ToString(cmd.ExecuteScalar());
                }
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Orders (UserID, TotalAmount) VALUES (@u, (SELECT SUM(p.Price*c.Quantity) FROM Cart c JOIN Products p ON c.ProductID=p.ProductID WHERE c.UserID=@u)); SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@u", userId);
                int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                SqlCommand insertItems = new SqlCommand(@"INSERT INTO OrderItems (OrderID, ProductID, Quantity, Price)
                        SELECT @order, c.ProductID, c.Quantity, p.Price FROM Cart c JOIN Products p ON c.ProductID=p.ProductID WHERE c.UserID=@u", con);
                insertItems.Parameters.AddWithValue("@order", orderId);
                insertItems.Parameters.AddWithValue("@u", userId);
                insertItems.ExecuteNonQuery();

                SqlCommand clear = new SqlCommand("DELETE FROM Cart WHERE UserID=@u", con);
                clear.Parameters.AddWithValue("@u", userId);
                clear.ExecuteNonQuery();

                lblMessage.Text = "✅ Order placed successfully!";
            }
        }
    }
}
