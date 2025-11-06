<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="theProject.Login" %>
<!DOCTYPE html>
<html>
<head>
    <title>Login - AnimeStore</title>

    <style>
        /* ====== AnimeStore Style (Amazon-inspired) ====== */
        body {
            font-family: "Amazon Ember", Arial, sans-serif;
            background-color: #f3f3f3;
            margin: 0;
            padding: 0;
        }

        .login-page {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: flex-start;
            min-height: 100vh;
        }

        /* AnimeStore logo text */
        .store-logo {
            margin-top: 50px;
            margin-bottom: 20px;
            font-size: 34px;
            font-weight: bold;
            color: #1a1a1a;
            letter-spacing: 1px;
        }

        .store-logo span {
            color: #ff9900;
        }

        /* Login card */
        .login-card {
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 8px;
            width: 350px;
            padding: 25px 30px;
            box-shadow: 0 1px 4px rgba(0, 0, 0, 0.08);
        }

        .login-card h1 {
            font-size: 24px;
            font-weight: 500;
            margin-bottom: 15px;
            color: #111;
        }

        .login-card p {
            font-size: 14px;
            color: #555;
            margin-bottom: 25px;
        }

        /* ===== Form Styles ===== */
        .form-group {
            margin-bottom: 20px;
        }

        label {
            display: block;
            font-size: 14px;
            font-weight: 600;
            color: #111;
            margin-bottom: 6px;
        }

        .form-control {
            width: 100%;
            padding: 10px 8px;
            border: 1px solid #a6a6a6;
            border-radius: 3px;
            font-size: 14px;
            box-sizing: border-box;
        }

        .form-control:focus {
            outline: none;
            border-color: #ff9900;
            box-shadow: 0 0 4px 1px rgba(255,153,0,0.4);
        }

        /* ===== Button ===== */
        .btn-submit {
            width: 100%;
            background-color: #ffb84d;
            border: 1px solid #a88734;
            border-radius: 3px;
            color: #111;
            font-size: 15px;
            font-weight: 600;
            padding: 10px 0;
            cursor: pointer;
            transition: 0.2s;
        }

        .btn-submit:hover {
            background-color: #f0a429;
        }

        .lblMessage {
            display: block;
            margin-top: 12px;
            color: #c40000;
            font-size: 13px;
        }

        /* ===== Register Section ===== */
        .register-link {
            text-align: center;
            margin-top: 25px;
            font-size: 14px;
            color: #111;
        }

        .register-link a {
            color: #0066c0;
            text-decoration: none;
        }

        .register-link a:hover {
            text-decoration: underline;
        }

        /* ===== Divider ===== */
        .divider {
            margin: 30px 0 20px 0;
            text-align: center;
            color: #777;
            font-size: 13px;
            position: relative;
        }

        .divider::before,
        .divider::after {
            content: "";
            position: absolute;
            top: 50%;
            width: 40%;
            height: 1px;
            background-color: #ddd;
        }

        .divider::before { left: 0; }
        .divider::after { right: 0; }

        /* ===== Create Account button ===== */
        .create-account-btn {
            width: 350px;
            margin-top: 10px;
            background-color: #e7e9ec;
            border: 1px solid #adb1b8;
            border-radius: 3px;
            padding: 10px 0;
            color: #111;
            font-weight: 500;
            cursor: pointer;
        }

        .create-account-btn:hover {
            background-color: #d7d9dc;
        }

        /* ===== Responsive ===== */
        @media (max-width: 420px) {
            .login-card, .create-account-btn {
                width: 90%;
            }
        }
    </style>
</head>

<body>
    <div class="login-page">
        <div class="store-logo">Anime<span>Store</span></div>

        <div class="login-card">
            <h1>Sign-In</h1>
            <p>Welcome back! Please login to your AnimeStore account.</p>

            <form runat="server">
                <div class="form-group">
                    <label for="txtEmail">Email or mobile number</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Sign-In" CssClass="btn-submit" OnClick="btnLogin_Click" />
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" />
            </form>
        </div>

        <div class="divider">New to AnimeStore?</div>

        <button class="create-account-btn" onclick="location.href='Register.aspx'">
            Create your AnimeStore account
        </button>
    </div>
</body>
</html>
