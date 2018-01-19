<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="MyGroupsAndRecommendations.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages.MyGroupsAndRecommendationsPage" meta:resourcekey="Page"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
    <div id="form" style="margin-right: auto; margin-left: auto;">
        <form id="form1" runat="server">
             <asp:GridView ID="myGroupsList" runat="server"  AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Justify" Width="25%">
                      <Columns>
                           <asp:HyperLinkField DataNavigateUrlFields="groupId" DataTextField="name" 
                                    meta:resourcekey="nameField" DataNavigateUrlFormatString="~/Pages/GroupPages/MyGroupsAndRecommendations.aspx?groupId={0}" />
                           <asp:TemplateField HeaderText="Darse de Baja">
                                  <ItemTemplate>
                                    <asp:Button ID="dropoutButton" runat="server" OnClick="dropout_Click"
                                      CommandName = "migrupo" 
                                      CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                      Text="darse de baja" Height="20" Width="50" />
                                  </ItemTemplate> 
                           </asp:TemplateField>
                     </Columns>
            </asp:GridView>
            <asp:GridView ID="recommendationList" runat="server" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Justify" Width="50%">
                     <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="eventId" DataTextField="name" 
                                 meta:resourcekey="nameField" DataNavigateUrlFormatString="~/Pages/EventPages/EventComments.aspx?eventId={0}" />
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

