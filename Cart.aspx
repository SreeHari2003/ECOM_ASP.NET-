<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="theProject.Cart" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Cart - Flipkart Clone</title>
    <style>
        body {
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f5f5f5;
        }

        .container {
            width: 95%;
            max-width: 1100px;
            margin: 30px auto;
            background-color: #fff;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 6px 20px rgba(0,0,0,0.1);
        }

        h2 {
            color: #2874f0;
            margin-bottom: 20px;
        }

        .lblMessage {
            font-weight: bold;
            margin-bottom: 15px;
            display: block;
            color: red;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
        }

        th, td {
            padding: 12px;
            border: 1px solid #ddd;
            text-align: center;
        }

        th {
            background-color: #2874f0;
            color: white;
        }

        tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        img {
            width: 80px;
            height: 80px;
            object-fit: cover;
            border-radius: 6px;
        }

        .btn {
            padding: 6px 12px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-weight: bold;
            transition: background-color 0.3s ease, transform 0.2s ease;
        }

        .btn-remove {
            background-color: #f44336;
            color: white;
        }

        .btn-remove:hover {
            background-color: #c62828;
            transform: translateY(-1px);
        }

        .btn-checkout {
            background-color: #388e3c;
            color: white;
            margin-top: 20px;
            padding: 10px 20px;
            font-size: 16px;
        }

        .btn-checkout:hover {
            background-color: #2e7d32;
            transform: translateY(-2px);
        }

        @media (max-width: 768px) {
            table, th, td {
                font-size: 12px;
            }
            .btn-checkout {
                width: 100%;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Your Shopping Cart</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage"></asp:Label>

            <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="">
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" Visible="False" />
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <img src='<%# Eval("ImageURL") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Product" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="₹{0}" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:TemplateField HeaderText="Remove">
                        <ItemTemplate>
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandArgument='<%# Eval("ProductID") %>' CssClass="btn btn-remove" OnClick="btnRemove_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" CssClass="btn btn-checkout" OnClick="btnCheckout_Click" />
        </div>
    </form>
</body>
</html>
