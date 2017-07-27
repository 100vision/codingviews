<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMember.aspx.cs" Inherits="ManageGroups.AddMember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label"></asp:Label>
        <br />
        <asp:ListBox ID="ListBox1" runat="server" Width="299px" Height="431px"></asp:ListBox>
    
    </div>
        <asp:Button ID="Button2" runat="server" Text="Add" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Back" />
        <asp:Button ID="Button4" runat="server" Text="Reset" OnClick="Button4_Click" />
    </form>
</body>
</html>
