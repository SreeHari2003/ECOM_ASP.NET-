using System;
using System.Data.SqlClient;

namespace theProject
{
    public partial class Register : System.Web.UI.Page
    {
        string connectionString = @"Server=DESKTOP-7PHPOBO\SQLEXPRESS;Database=theDatabase;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Name, Email, PasswordHash, Role) OUTPUT INSERTED.UserID VALUES (@Name, @Email, @Password, 'User')";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();
                    int userId = (int)cmd.ExecuteScalar();

                    // Store user session
                    Session["UserID"] = userId;
                    Session["Email"] = email;
                    Session["Role"] = "User";

                    Response.Redirect("UserDashboard.aspx");
                }
            }
        }
    }
}
