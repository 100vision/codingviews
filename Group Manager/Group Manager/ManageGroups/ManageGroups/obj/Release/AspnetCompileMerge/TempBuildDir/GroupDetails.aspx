<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupDetails.aspx.cs" Inherits="ManageGroups.GroupDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Label"></asp:Label>
    
    </div>
        <br />
        <asp:ListBox ID="ListBox1" runat="server" Width="200px"></asp:ListBox>
        <p style="height: 374px">
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Remove" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="+" />
        </p>
    </form>
</body>
</html>
