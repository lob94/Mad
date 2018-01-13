<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="RecomendationsPage.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages.RecomendationsPage" meta:resourcekey="Page"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
    <div id="form" style="margin-right: auto; margin-left: auto;">
        <form id="form1" runat="server">
            <asp:GridView ID="recommendationList" runat="server"
                            AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Justify" Width="50%">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="eventId" DataTextField="name" 
                                    meta:resourcekey="nameField" DataNavigateUrlFormatString="~/Pages/EventPages/EventComments.aspx?eventId={0}" />
                                <asp:BoundField DataField="groupName" meta:resourcekey="groupName" />
                                <asp:BoundField DataField="recommendationText" meta:resourcekey="recommendationText" />
                            </Columns>
            </asp:GridView>
        </form>
        </div>
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

