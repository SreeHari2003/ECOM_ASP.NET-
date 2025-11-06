using System;
using System.Data.SqlClient;

namespace theProject
{
    public partial class Login : System.Web.UI.Page
    {
        string connectionString = @"Server=DESKTOP-7PHPOBO\SQLEXPRESS;Database=theDatabase;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT UserID, Role FROM Users WHERE Email=@Email AND PasswordHash=@Password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int userId = Convert.ToInt32(reader["UserID"]);
                        string role = reader["Role"].ToString();

                        Session["UserID"] = userId;
                        Session["Email"] = email;
                        Session["Role"] = role;

                        if (role == "Admin")
                            Response.Redirect("AdminDashboard.aspx");
                        else
                            Response.Redirect("UserDashboard.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Invalid email or password.";
                    }
                }
            }
        }
    }
}
