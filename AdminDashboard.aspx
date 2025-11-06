<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="theProject.AdminDashboard" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard</title>
    <style>
        body { font-family: Arial, sans-serif; background-color: #f8f8f8; margin:0; padding:0; }
        .header { background-color: #2874f0; color: white; padding: 15px 30px; display: flex; justify-content: space-between; align-items: center; }
        .header h2 { margin: 0; font-size: 22px; }
        .btn-logout { background-color: white; color: #2874f0; padding: 8px 15px; border: none; border-radius: 5px; font-weight: bold; cursor: pointer; }
        .btn-logout:hover { background-color: #0b61d2; color: white; }
        .container { width: 95%; max-width: 1200px; margin: 20px auto; }
        h3 { margin-top:30px; }
        input[type=text], input[type=number], select { padding: 8px 12px; border-radius:6px; border:1px solid #ccc; width: 300px; margin-bottom:10px; }
        .btn { padding: 6px 12px; border:none; border-radius:6px; background-color:#2874f0; color:white; cursor:pointer; font-weight:bold; }
        .btn:hover { background-color:#0b61d2; }
        table { width:100%; border-collapse: collapse; margin-top:10px; }
        th, td { padding: 8px; border: 1px solid #ccc; text-align: center; }
        img { width:80px; height:80px; object-fit:cover; border-radius:5px; }
        .file-upload { margin-top:5px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <h2>Admin Dashboard</h2>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn-logout" OnClick="btnLogout_Click" />
        </div>

        <div class="container">
            <!-- Add Category -->
            <h3>Add Category</h3>
            <asp:TextBox ID="txtCategoryName" runat="server" placeholder="Category Name" /><br />
            <asp:FileUpload ID="fuCategoryImage" runat="server" CssClass="file-upload" /><br />
            <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" OnClick="btnAddCategory_Click" CssClass="btn" /><br />
            <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="False" DataKeyNames="CategoryID"
                OnRowEditing="gvCategories_RowEditing" OnRowUpdating="gvCategories_RowUpdating" OnRowCancelingEdit="gvCategories_RowCancelingEdit" OnRowDeleting="gvCategories_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="CategoryID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <img src='<%# Eval("ImageURL") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:FileUpload ID="fuEditCategoryImage" runat="server" CssClass="file-upload" /><br />
                            Current: <img src='<%# Eval("ImageURL") %>' style="width:80px;" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>

            <!-- Add Product -->
            <h3>Add Product</h3>
            Category: <asp:DropDownList ID="ddlCategories" runat="server" /><br />
            <asp:TextBox ID="txtProductName" runat="server" placeholder="Product Name" /><br />
            <asp:TextBox ID="txtDescription" runat="server" placeholder="Description" /><br />
            <asp:TextBox ID="txtPrice" runat="server" placeholder="Price" /><br />
            <asp:FileUpload ID="fuProductImage" runat="server" CssClass="file-upload" /><br />
            <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" CssClass="btn" /><br />

            <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID"
                OnRowEditing="gvProducts_RowEditing" OnRowUpdating="gvProducts_RowUpdating" OnRowCancelingEdit="gvProducts_RowCancelingEdit" OnRowDeleting="gvProducts_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="Price" HeaderText="Price" />
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <img src='<%# Eval("ImageURL") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:FileUpload ID="fuEditProductImage" runat="server" CssClass="file-upload" /><br />
                            Current: <img src='<%# Eval("ImageURL") %>' style="width:80px;" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
