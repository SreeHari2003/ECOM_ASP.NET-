using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace theProject
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        string connectionString = @"Server=DESKTOP-7PHPOBO\SQLEXPRESS;Database=theDatabase;Integrated Security=True;";
        string categoryImageFolder = "~/Images/Categories/";
        string productImageFolder = "~/Images/Products/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategoriesDropdown();
                LoadCategoriesGrid();
                LoadProductsGrid();
            }
        }

        #region Categories

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text != "" && fuCategoryImage.HasFile)
            {
                string fileName = Path.GetFileName(fuCategoryImage.PostedFile.FileName);
                string path = Server.MapPath(categoryImageFolder + fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                fuCategoryImage.SaveAs(path);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Categories(Name, ImageURL) VALUES(@Name, @Image)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", txtCategoryName.Text);
                    cmd.Parameters.AddWithValue("@Image", categoryImageFolder + fileName);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                txtCategoryName.Text = "";
                LoadCategoriesDropdown();
                LoadCategoriesGrid();
            }
        }

        void LoadCategoriesDropdown()
        {
            ddlCategories.Items.Clear();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID, Name FROM Categories";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ddlCategories.Items.Add(new ListItem(dr["Name"].ToString(), dr["CategoryID"].ToString()));
                }
            }
        }

        void LoadCategoriesGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Categories";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
        }

        protected void gvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategories.EditIndex = e.NewEditIndex;
            LoadCategoriesGrid();
        }

        protected void gvCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategories.EditIndex = -1;
            LoadCategoriesGrid();
        }

        protected void gvCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvCategories.Rows[e.RowIndex];
            string name = ((TextBox)row.Cells[1].Controls[0]).Text;
            string imageUrl = ((Image)row.FindControl("Image1"))?.ImageUrl ?? "";

            FileUpload fu = (FileUpload)row.FindControl("fuEditCategoryImage");
            if (fu != null && fu.HasFile)
            {
                string fileName = Path.GetFileName(fu.PostedFile.FileName);
                string path = Server.MapPath(categoryImageFolder + fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                fu.SaveAs(path);
                imageUrl = categoryImageFolder + fileName;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Categories SET Name=@Name, ImageURL=@ImageURL WHERE CategoryID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@ImageURL", imageUrl);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            gvCategories.EditIndex = -1;
            LoadCategoriesGrid();
        }

        protected void gvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Categories WHERE CategoryID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            LoadCategoriesGrid();
            LoadCategoriesDropdown();
        }

        #endregion

        #region Products

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text != "" && txtDescription.Text != "" && txtPrice.Text != "" && fuProductImage.HasFile)
            {
                string fileName = Path.GetFileName(fuProductImage.PostedFile.FileName);
                string path = Server.MapPath(productImageFolder + fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                fuProductImage.SaveAs(path);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Products(Name, Description, Price, ImageURL, CategoryID) VALUES(@Name, @Desc, @Price, @Image, @CategoryID)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@Desc", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@Image", productImageFolder + fileName);
                    cmd.Parameters.AddWithValue("@CategoryID", ddlCategories.SelectedValue);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                txtProductName.Text = "";
                txtDescription.Text = "";
                txtPrice.Text = "";
                LoadProductsGrid();
            }
        }

        void LoadProductsGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            LoadProductsGrid();
        }

        protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            LoadProductsGrid();
        }

        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvProducts.Rows[e.RowIndex];

            string name = ((TextBox)row.Cells[1].Controls[0]).Text;
            string description = ((TextBox)row.Cells[2].Controls[0]).Text;
            decimal price = Convert.ToDecimal(((TextBox)row.Cells[3].Controls[0]).Text);
            string imageUrl = ((Image)row.FindControl("Image1"))?.ImageUrl ?? "";

            FileUpload fu = (FileUpload)row.FindControl("fuEditProductImage");
            if (fu != null && fu.HasFile)
            {
                string fileName = Path.GetFileName(fu.PostedFile.FileName);
                string path = Server.MapPath(productImageFolder + fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                fu.SaveAs(path);
                imageUrl = productImageFolder + fileName;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Products SET Name=@Name, Description=@Desc, Price=@Price, ImageURL=@Image WHERE ProductID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Desc", description);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Image", imageUrl);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            gvProducts.EditIndex = -1;
            LoadProductsGrid();
        }

        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE ProductID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            LoadProductsGrid();
        }

        #endregion

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
