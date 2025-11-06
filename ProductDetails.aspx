<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="theProject.ProductDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Details</title>
    <style>
        body { font-family: Arial; padding: 20px; background-color: #f8f8f8; }
        .product-details { display: flex; gap: 20px; background-color: #fff; padding: 20px; border-radius: 10px; box-shadow: 0 1px 5px rgba(0,0,0,0.2); }
        .product-details img { width: 300px; height: 300px; object-fit: cover; border-radius: 10px; }
        .product-info h2 { margin: 0 0 10px 0; }
        .btn { background-color: #2874f0; color: white; padding: 8px 16px; border: none; border-radius: 5px; cursor: pointer; margin-top: 20px; }
        .btn:hover { background-color: #0b61d2; }
        .quantity { width: 50px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <div class="product-details">
            <asp:Image ID="imgProduct" runat="server" Width="300px" Height="300px" />
            <div class="product-info">
                <h2><asp:Label ID="lblName" runat="server"></asp:Label></h2>
                <p><strong>Price:</strong> ₹<asp:Label ID="lblPrice" runat="server"></asp:Label></p>
                <p><asp:Label ID="lblDescription" runat="server"></asp:Label></p>
                Quantity: <asp:TextBox ID="txtQuantity" runat="server" Text="1" CssClass="quantity"></asp:TextBox><br />
                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" CssClass="btn" />
            </div>
        </div>
    </form>
</body>
</html>
