<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wishlist.aspx.cs" Inherits="theProject.Wishlist" %>
<!DOCTYPE html>
<html>
<head><title>Wishlist</title></head>
<body>
<form runat="server">
    <h2>Wishlist</h2>
    <asp:Repeater ID="rptWishlist" runat="server">
        <ItemTemplate>
            <div style="display:inline-block; margin:10px; text-align:center;">
                <img src='<%# Eval("ImageURL") %>' width="150" height="150" /><br />
                <%# Eval("Name") %><br />
                ₹<%# Eval("Price") %><br />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</form>
</body>
</html>
