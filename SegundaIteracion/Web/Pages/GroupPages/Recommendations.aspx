<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="Recommendations.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages.RecommendationsPage" meta:resourcekey="Page"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
    <div id="form" style="margin-right: auto; margin-left: auto;">
      
            <div id="recomGrid" style="margin-left: 100px; margin-top: 20px; float: left; height: 198px;">
            <asp:GridView ID="recommendationList" runat="server" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="False" HorizontalAlign="Left" Width="402%" Height="170px" style="margin-left: 0px">
                     <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="eventId" DataTextField="eventName" 
                                 meta:resourcekey="eventName" DataNavigateUrlFormatString="~/Pages/EventPages/EventComments.aspx?eventId={0}" />
                            <asp:BoundField DataField="reason" meta:resourcekey="reason"/>
                            <asp:BoundField DataField="groupId" meta:resourcekey="groupId"/>
                     </Columns>
                 <EmptyDataTemplate> <asp:Label runat="server" meta:resourcekey="gridVacio"/></EmptyDataTemplate>
            </asp:GridView> 
            </div>
              
      
        </div>
        <div class="previousNextLinks" style="margin-top: 10px;">
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
</asp:Content>