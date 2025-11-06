<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="theProject.Register" %>
<!DOCTYPE html>
<html>
<head>
    <title>Register - Flipkart Clone</title>
    <style>
        /* Body & background */
        body {
            margin: 0;
            padding: 0;
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            background: linear-gradient(135deg, #2874f0, #6b92f4);
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        /* Center card */
        .register-page {
            width: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .register-card {
            background: #ffffff;
            border-radius: 12px;
            padding: 40px 50px;
            box-shadow: 0 10px 25px rgba(0,0,0,0.2);
            max-width: 400px;
            width: 90%;
            text-align: center;
        }

        /* Title */
        .register-card h1 {
            margin: 0 0 10px 0;
            color: #2874f0;
            font-size: 28px;
            font-weight: 700;
        }

        .register-card p {
            color: #555;
            margin-bottom: 30px;
            font-size: 14px;
        }

        /* Form groups */
        .form-group {
            text-align: left;
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            font-weight: 600;
            margin-bottom: 6px;
            color: #333;
        }

        .form-control {
            width: 100%;
            padding: 12px 15px;
            border: 1px solid #ccc;
            border-radius: 8px;
            font-size: 14px;
            transition: all 0.3s ease;
        }

        .form-control:focus {
            border-color: #2874f0;
            box-shadow: 0 0 5px rgba(40,116,240,0.5);
            outline: none;
        }

        /* Submit button */
        .btn-submit {
            width: 100%;
            padding: 14px;
            background-color: #2874f0;
            color: white;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: background-color 0.3s ease, transform 0.2s ease;
        }

        .btn-submit:hover {
            background-color: #0b61d2;
            transform: translateY(-2px);
        }

        /* Message */
        .lblMessage {
            display: block;
            margin-top: 15px;
            font-weight: bold;
            color: green;
        }

        /* Login link */
        .login-link {
            margin-top: 25px;
            font-size: 14px;
            color: #555;
        }

        .login-link a {
            color: #2874f0;
            text-decoration: none;
            font-weight: 600;
        }

        .login-link a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <div class="register-page">
        <div class="register-card">
            <h1>Create Account</h1>
            <p>Join and start shopping!</p>

            <form runat="server">
                <div class="form-group">
                    <label for="txtName">Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label for="txtEmail">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>

                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn-submit" OnClick="btnRegister_Click" />

                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" />
            </form>

            <div class="login-link">
                Already have an account? <a href="Login.aspx">Login</a>
            </div>
        </div>
    </div>
</body>
</html>
