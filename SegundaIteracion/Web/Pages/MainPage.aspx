<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true"
    Codebehind="MainPage.aspx.cs" Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.MainPage" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="form1" runat="server">
    <br />
    <br />
        <asp:ListBox ID="ListBox1" runat="server" Width="881px"></asp:ListBox>
    <br />
    <br />
    <asp:Localize ID="lclContent" runat="server" meta:resourcekey="lclContent" />
    <br />
    <br />
    <br />
    <br />
    </form>
</asp:Content>
