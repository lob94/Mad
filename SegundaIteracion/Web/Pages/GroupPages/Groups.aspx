<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages.Groups" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form" style="margin-right: auto; margin-left: auto;">
        <form id="form1" runat="server">
            <div class="form-group">
                <asp:TextBox ID="textEntry" runat="server" AutoPostBack="True"> </asp:TextBox>
                <asp:Button ID="searchButton" class="btn btn-primary" runat="server" OnClick="search_Click" meta:resourcekey="btnLogin" Text="Search" />
            </div>
            <asp:GridView ID="groupList" runat="server"
                            AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Justify" Width="50%">
                            <Columns>
                                <asp:BoundField DataField="name" meta:resourcekey="name" />
                                <asp:BoundField DataField="description" meta:resourcekey="description" />
                                <asp:BoundField DataField="usersCount" meta:resourcekey="usersCount" />
                                <asp:BoundField DataField="recommendationsCount" meta:resourcekey="recommendationCount" />
                                <asp:TemplateField  HeaderText="Seleccionar">
                                  <ItemTemplate>
                                    <asp:Button ID="subs" runat="server" OnClick="subs_Click" Text="Inscribirse"/>
                                  </ItemTemplate> 
                                </asp:TemplateField>
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
