<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="AddLabel.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.AddLabel" meta:resourcekey="Page" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:Localize ID="AddLabelLoc" runat="server" meta:resourcekey="AddLabel" />
    <br />
    <div id="form">
        <div class="field">
            <asp:Label ID="labelName" runat="server" meta:resourcekey="txtLabelName" Width="15%" Style="vertical-align: top; font-weight: bold"></asp:Label>
            <asp:TextBox ID="txtLabel" runat="server" Width="200px" Columns="16"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLabelName" runat="server"
                        ControlToValidate="txtLabel" Display="Dynamic" meta:resourcekey="labelRequired"/>
        </div>
        <div class="button">
            <asp:Button ID="create" runat="server" meta:resourcekey="btnCreate" OnClick="CreateLabelClick" />
        </div>
    </div>
    <br />
</asp:Content>
