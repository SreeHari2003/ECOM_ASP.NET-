using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace theProject
{
    public partial class UserDashboard : System.Web.UI.Page
    {
        string connectionString = @"Server=DESKTOP-7PHPOBO\SQLEXPRESS;Database=theDatabase;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                LoadCategories();
                ddlCategories.SelectedValue = "0";
                ddlSort.SelectedValue = "None";
                LoadProducts();
            }
        }

        void LoadCategories()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID, Name FROM Categories";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlCategories.Items.Clear();
                    ddlCategories.Items.Add(new ListItem("All", "0"));
                    while (reader.Read())
                    {
                        ddlCategories.Items.Add(new ListItem(reader["Name"].ToString(), reader["CategoryID"].ToString()));
                    }
                }
            }
        }

        void LoadProducts()
        {
            int selectedCategory = 0;
            int.TryParse(ddlCategories.SelectedValue, out selectedCategory);
            string sortOrder = ddlSort.SelectedValue;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID, Name, Price, ImageURL FROM Products";
                if (selectedCategory != 0)
                    query += " WHERE CategoryID=@CategoryID";

                if (sortOrder == "ASC")
                    query += " ORDER BY Price ASC";
                else if (sortOrder == "DESC")
                    query += " ORDER BY Price DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (selectedCategory != 0)
                        cmd.Parameters.AddWithValue("@CategoryID", selectedCategory);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rptProducts.DataSource = dt;
                    rptProducts.DataBind();
                }
            }
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int productId = Convert.ToInt32(btn.CommandArgument);
                int userId = Convert.ToInt32(Session["UserID"]);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        IF EXISTS (SELECT 1 FROM Cart WHERE UserID=@UserID AND ProductID=@ProductID)
                            UPDATE Cart SET Quantity = Quantity + 1 WHERE UserID=@UserID AND ProductID=@ProductID
                        ELSE
                            INSERT INTO Cart (UserID, ProductID, Quantity) VALUES (@UserID, @ProductID, 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("Cart.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void btnCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }
    }
}
