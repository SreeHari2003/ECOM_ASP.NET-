<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="theProject.Checkout" %>
<!DOCTYPE html>
<html>
<head><title>Checkout</title></head>
<body>
<form runat="server">
    <h2>Confirm Order</h2>
    <asp:Label ID="lblTotal" runat="server" /><br />
    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" OnClick="btnPlaceOrder_Click" /><br />
    <asp:Label ID="lblMessage" runat="server" />
</form>
</body>
</html>
