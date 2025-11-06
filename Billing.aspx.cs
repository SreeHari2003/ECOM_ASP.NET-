using System;
using System.Data;
using System.Data.SqlClient;

namespace theProject
{
    public partial class Billing : System.Web.UI.Page
    {
        string connectionString = @"Server=DESKTOP-7PHPOBO\SQLEXPRESS;Database=theDatabase;Integrated Security=True;";
        decimal totalAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
                LoadBilling();
        }

        void LoadBilling()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT c.ProductID, p.Name, p.Price, c.Quantity, (c.Quantity * p.Price) AS Total
                                 FROM Cart c
                                 INNER JOIN Products p ON c.ProductID = p.ProductID
                                 WHERE c.UserID=@UserID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvBilling.DataSource = dt;
                    gvBilling.DataBind();

                    // Calculate total amount
                    totalAmount = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        totalAmount += Convert.ToDecimal(row["Total"]);
                    }
                    lblTotalAmount.Text = totalAmount.ToString("0.00");
                }
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Insert order
                    string orderQuery = "INSERT INTO Orders (UserID, TotalAmount) VALUES (@UserID, @TotalAmount); SELECT SCOPE_IDENTITY();";
                    int orderId;
                    using (SqlCommand cmd = new SqlCommand(orderQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        orderId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Insert order items
                    string itemsQuery = @"INSERT INTO OrderItems (OrderID, ProductID, Quantity, Price)
                                          SELECT @OrderID, ProductID, Quantity, Price
                                          FROM Cart
                                          WHERE UserID=@UserID";

                    using (SqlCommand cmd = new SqlCommand(itemsQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderId);
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.ExecuteNonQuery();
                    }

                    // Clear cart
                    string clearCart = "DELETE FROM Cart WHERE UserID=@UserID";
                    using (SqlCommand cmd = new SqlCommand(clearCart, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    Response.Redirect("OrderConfirmation.aspx?OrderID=" + orderId);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMessage.Text = "Error placing order: " + ex.Message;
                }
            }
        }
    }
}
