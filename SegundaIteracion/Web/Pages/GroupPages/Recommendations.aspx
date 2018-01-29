<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="Recommendations.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages.RecommendationsPage" meta:resourcekey="Page"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
    <div id="form" style="margin-bottom: 10px;">
            <div id="recomGrid" style="margin-left: 10px; margin-top: 10px; float: left; height: 180px;">
            <asp:GridView ID="recommendationList" runat="server" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="False" HorizontalAlign="Left" Width="396%" Height="170px" style="margin-left: 0px">
                     <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="eventId" DataTextField="eventName" 
                                 meta:resourcekey="eventName" DataNavigateUrlFormatString="~/Pages/EventPages/EventComments.aspx?eventId={0}" />
                            <asp:BoundField DataField="reason" meta:resourcekey="reason"/>
                            <asp:BoundField DataField="groupName" meta:resourcekey="groupName"/>
                     </Columns>
                     <EmptyDataTemplate> <asp:Label runat="server" meta:resourcekey="gridVacio"/></EmptyDataTemplate>
            </asp:GridView> 
            </div>
         <div class="previousNextLinks" style="margin-top: 20px;">
            <span class="previousLink">
                <asp:HyperLink ID="linkPrevious" meta:resourcekey="previous" Text="Previous" 
                    runat="server" Visible="False">
                </asp:HyperLink>
            </span>
            <span class="nextLink">
                <asp:HyperLink ID="linkNext" meta:resourcekey="next" Text="Next" 
                    runat="server" Visible="False">
                </asp:HyperLink>
            </span>
        </div>
        </div>
</asp:Content>