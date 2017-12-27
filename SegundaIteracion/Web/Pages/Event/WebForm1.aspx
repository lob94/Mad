<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.Event.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
         <asp:GridView ID="listEvents" runat="server" CssClass="Event" 
        AutoGenerateColumns="False" 
        onpageindexchanging="GvEventPageIndexChanging" 
        ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField DataField="Date" HeaderText="<%$ Resources:, date %>" />
            <asp:BoundField DataField="Name" HeaderText="<%$ Resources:, name %>" />
            <asp:BoundField DataField="Review" HeaderText="<%$ Resources:, review %>" />
            <asp:BoundField DataField="Category" HeaderText="<%$ Resources:, category %>" />
        </Columns>
    </asp:GridView>

    </div>
    </form>
</body>
</html>
