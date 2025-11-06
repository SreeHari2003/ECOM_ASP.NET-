using System;
using System.Data;
using System.Data.SqlClient;

namespace theProject
{
    public partial class Wishlist : System.Web.UI.Page
    {
        string connectionString = @"Server=DESKTOP-7PHPOBO\SQLEXPRESS;Database=theDatabase;Integrated Security=True;";
        int userId = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT p.Name, p.Price, p.ImageURL FROM Wishlist w JOIN Products p ON w.ProductID=p.ProductID WHERE w.UserID=@u", con);
                    da.SelectCommand.Parameters.AddWithValue("@u", userId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rptWishlist.DataSource = dt;
                    rptWishlist.DataBind();
                }
            }
        }
    }
}
