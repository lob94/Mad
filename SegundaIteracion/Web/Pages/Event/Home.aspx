<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.Event.Home" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form" style="margin-right: auto; margin-left: auto;">
        <form id="form1" runat="server">
            <asp:ListBox ID="eventList" runat="server" Width="881px"></asp:ListBox>
        </form>
    </div>
</asp:Content>
