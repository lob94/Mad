<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.Event.Home" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form" style="margin-right: auto; margin-left: auto;">
        <form id="form1" runat="server">
            <div class="form-group">
                <asp:TextBox ID="textEntry" runat="server" AutoPostBack="True"> </asp:TextBox>
                <asp:DropDownList ID="dropDownList" runat="server" DataTextField="name" DataValueField="categoryId" 
                    AppendDataBoundItems="True"></asp:DropDownList>
                <asp:Button ID="searchButton" class="btn btn-primary" runat="server" OnClick="search_Click" meta:resourcekey="btnLogin" Text="Search" />
            </div>
            <asp:GridView ID="eventList2" runat="server"
                            AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Justify" Width="50%">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="eventId" DataTextField="name" 
                                    meta:resourcekey="nameField" DataNavigateUrlFormatString="~/Pages/Event/Event.aspx?eventId={0}" />
                                <asp:BoundField DataField="name" meta:resourcekey="name" />
                                <asp:BoundField DataField="categoryName" meta:resourcekey="categoryName" />
                            </Columns>
            </asp:GridView>
        </form>
        <div class="previousNextLinks">
            <span class="previousLink">
                <asp:HyperLink ID="linkPrevious" Text="Previous" 
                    runat="server" Visible="False">
                </asp:HyperLink>
            </span>
            <span class="nextLink">
                <asp:HyperLink ID="linkNext" Text="Next" 
                    runat="server" Visible="False">
                </asp:HyperLink>
            </span>
        </div>
</asp:Content>
