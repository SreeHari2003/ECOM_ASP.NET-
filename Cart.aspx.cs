using System;
using System.Data;
using System.Data.SqlClient;

namespace theProject
{
    public partial class Cart : System.Web.UI.Page
    {
        string connectionString = @"Server=DESKTOP-7PHPOBO\SQLEXPRESS;Database=theDatabase;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
                LoadCart();
        }

        void LoadCart()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT c.ProductID, p.Name, p.Price, p.ImageURL, c.Quantity
                                 FROM Cart c
                                 INNER JOIN Products p ON c.ProductID = p.ProductID
                                 WHERE c.UserID=@UserID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvCart.DataSource = dt;
                    gvCart.DataBind();
                }
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            int productId = Convert.ToInt32(((System.Web.UI.WebControls.Button)sender).CommandArgument);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Cart WHERE UserID=@UserID AND ProductID=@ProductID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            LoadCart();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Billing.aspx");
        }
    }
}
