<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" Inherits="ManageGroups.Groups" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Welcome"></asp:Label>
    
        :<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
    
        <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
    
    </div>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Button1_Click">Edit...</asp:LinkButton>
    </form>
</body>
</html>
