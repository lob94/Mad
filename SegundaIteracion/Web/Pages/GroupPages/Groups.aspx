<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages.Groups" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form" style="margin-right: auto; margin-left: auto; margin-bottom: 10px;">
        <form id="form1" runat="server">
            <div class="form-group" style="width: 1374px; margin-left: 0px">
                <asp:TextBox ID="textEntry" runat="server" style="margin-left: 400px;" AutoPostBack="True" Width="311px"></asp:TextBox>
                <asp:Button ID="searchButton" class="btn btn-primary" runat="server" OnClick="search_Click" meta:resourcekey="btnLogin" Text="Search"
                     Height="55px" Width="159px" style="margin-top: 10px; margin-bottom: 10px; margin-left: 50px;"/>
            </div>
            <asp:GridView ID="groupList" runat="server"
                            AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Justify" Width="50%" style="margin-left: 404px">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="groupId" DataTextField="name" 
                                    meta:resourcekey="nameField" DataNavigateUrlFormatString="~/Pages/GroupPages/GroupInfo.aspx?groupId={0}" />
                            </Columns>
            </asp:GridView>
        </form>
        <div class="previousNextLinks" style="margin-bottom: 15px;">
            <span class="previousLink">
                <asp:HyperLink ID="linkPrevious" meta:resourcekey="previous" Text="Previous" 
                    runat="server" Visible="False" style="margin-bottom: 10px;">
                </asp:HyperLink>
            </span>
            <span class="nextLink">
                <asp:HyperLink ID="linkNext" meta:resourcekey="next" Text="Next" 
                    runat="server" Visible="False" style="margin-bottom: 10px;">
                </asp:HyperLink>
            </span>
        </div>
</asp:Content>
