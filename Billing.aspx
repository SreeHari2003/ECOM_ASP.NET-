<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="theProject.Billing" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Checkout - Flipkart Clone</title>
    <style>
        body {
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f5f5f5;
        }

        .container {
            width: 95%;
            max-width: 900px;
            margin: 30px auto;
            background-color: #fff;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 6px 20px rgba(0,0,0,0.1);
        }

        h2, h3 {
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

        .btn {
            padding: 10px 20px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-weight: bold;
            background-color: #2874f0;
            color: white;
            transition: background-color 0.3s ease, transform 0.2s ease;
            margin-top: 20px;
        }

        .btn:hover {
            background-color: #0b61d2;
            transform: translateY(-2px);
        }

        @media (max-width: 768px) {
            table, th, td {
                font-size: 12px;
            }
            .btn {
                width: 100%;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Checkout</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage"></asp:Label>

            <asp:GridView ID="gvBilling" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Product" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="₹{0}" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="₹{0}" />
                </Columns>
            </asp:GridView>

            <h3>Total Amount: ₹<asp:Label ID="lblTotalAmount" runat="server"></asp:Label></h3>
            <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn" OnClick="btnPlaceOrder_Click" />
        </div>
    </form>
</body>
</html>
