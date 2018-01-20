<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="NewGroupForm.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages.NewGroupForm" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    
    <form id="form1" runat="server">
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclNewGroupName" runat="server" meta:resourcekey="lclNewGroupName" /></span><span
                    class="entry">
                    <asp:TextBox ID="txtNewGroupName" runat="server" Width="100" Columns="16"></asp:TextBox>
             </span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclNewGroupDescription" runat="server" meta:resourcekey="lclNewGroupDescription" /></span><span
                    class="entry">
                    <asp:TextBox ID="TxtNewGroupDescription" runat="server" Width="100" Columns="16"></asp:TextBox>
             </span>
        </div>
        <div class="field">
            <div class="button">
                <asp:Button ID="btnEnter" runat="server" OnClick="BtnNewGroupClick" meta:resourcekey="btnEnter" />
            </div>
        </div>
    </form>
    
</asp:Content>