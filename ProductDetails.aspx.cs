using System;
using System.Data.SqlClient;

namespace theProject
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        string connectionString = @"Server=DESKTOP-7PHPOBO\SQLEXPRESS;Database=theDatabase;Integrated Security=True;";
        int productId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("Login.aspx");

            if (!int.TryParse(Request.QueryString["ProductID"], out productId))
            {
                lblMessage.Text = "Invalid Product!";
                btnAddToCart.Visible = false;
                return;
            }

            if (!IsPostBack)
                LoadProductDetails();
        }

        void LoadProductDetails()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, Price, Description, ImageURL FROM Products WHERE ProductID=@ProductID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblName.Text = reader["Name"].ToString();
                        lblPrice.Text = reader["Price"].ToString();
                        lblDescription.Text = reader["Description"].ToString();
                        imgProduct.ImageUrl = reader["ImageURL"].ToString();
                    }
                    else
                    {
                        lblMessage.Text = "Product not found!";
                        btnAddToCart.Visible = false;
                    }
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            int quantity = 1;

            if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
                quantity = 1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if product exists
                string checkQuery = "SELECT COUNT(*) FROM Products WHERE ProductID=@ProductID";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@ProductID", productId);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        lblMessage.Text = "This product does not exist!";
                        return;
                    }
                }

                // Insert or update cart
                string query = @"
                    IF EXISTS (SELECT 1 FROM Cart WHERE UserID=@UserID AND ProductID=@ProductID)
                        UPDATE Cart SET Quantity = Quantity + @Quantity WHERE UserID=@UserID AND ProductID=@ProductID
                    ELSE
                        INSERT INTO Cart (UserID, ProductID, Quantity) VALUES (@UserID, @ProductID, @Quantity)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("Cart.aspx");
        }
    }
}
