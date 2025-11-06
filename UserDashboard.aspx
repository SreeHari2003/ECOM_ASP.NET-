<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="theProject.UserDashboard" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Dashboard</title>
    <style>
        body { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f5f5f5; margin: 0; padding: 0; }
        .header { background-color: #2874f0; color: white; padding: 15px 30px; display: flex; justify-content: space-between; align-items: center; }
        .header h2 { margin: 0; font-size: 22px; }
        .btn-logout, .btn-cart { background-color: white; color: #2874f0; padding: 8px 15px; border: none; border-radius: 5px; font-weight: bold; cursor: pointer; transition: background-color 0.3s ease, color 0.3s ease; margin-left: 10px; }
        .btn-logout:hover, .btn-cart:hover { background-color: #0b61d2; color: white; }
        .container { width: 95%; max-width: 1200px; margin: 20px auto; }
        .filter { margin-bottom: 25px; font-size: 16px; display: flex; align-items: center; gap: 15px; }
        select { padding: 8px 12px; border-radius: 6px; border: 1px solid #ccc; font-size: 14px; }
        .products { display: flex; flex-wrap: wrap; gap: 20px; }
        .product-card { background-color: #fff; width: 200px; border-radius: 10px; box-shadow: 0 4px 15px rgba(0,0,0,0.1); padding: 10px; text-align: center; transition: transform 0.2s ease, box-shadow 0.2s ease; }
        .product-card:hover { transform: translateY(-5px); box-shadow: 0 8px 25px rgba(0,0,0,0.15); }
        .product-card img { width: 180px; height: 180px; object-fit: cover; border-radius: 8px; margin-bottom: 10px; }
        .product-card h4 { margin: 8px 0 5px 0; font-size: 16px; color: #333; }
        .product-card p { margin: 0 0 10px 0; font-weight: bold; color: #2874f0; }
        .btn-add-cart { background-color: #2874f0; color: white; padding: 6px 12px; border: none; border-radius: 6px; cursor: pointer; font-weight: bold; transition: background-color 0.3s ease; }
        .btn-add-cart:hover { background-color: #0b61d2; }
        @media (max-width: 768px) { .products { justify-content: center; } .filter { flex-direction: column; align-items: flex-start; gap: 10px; } }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <h2>Welcome!</h2>
            <div>
                <asp:Button ID="btnCart" runat="server" Text="Go to Cart" CssClass="btn-cart" OnClick="btnCart_Click" />
                <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn-logout" OnClick="btnLogout_Click" />
            </div>
        </div>

        <div class="container">
            <div class="filter">
                Category:
                <asp:DropDownList ID="ddlCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged">
                </asp:DropDownList>

                Sort by Price:
                <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
                    <asp:ListItem Text="None" Value="None" />
                    <asp:ListItem Text="Low to High" Value="ASC" />
                    <asp:ListItem Text="High to Low" Value="DESC" />
                </asp:DropDownList>
            </div>

            <div class="products">
                <asp:Repeater ID="rptProducts" runat="server">
                    <ItemTemplate>
                        <div class="product-card">
                            <a href='ProductDetails.aspx?ProductID=<%# Eval("ProductID") %>'>
                                <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("Name") %>' />
                            </a>
                            <h4><%# Eval("Name") %></h4>
                            <p>₹<%# Eval("Price") %></p>
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CommandArgument='<%# Eval("ProductID") %>' OnClick="btnAddToCart_Click" CssClass="btn-add-cart" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
