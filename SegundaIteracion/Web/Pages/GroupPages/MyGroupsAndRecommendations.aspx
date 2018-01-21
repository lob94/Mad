<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="MyGroupsAndRecommendations.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages.MyGroupsAndRecommendationsPage" meta:resourcekey="Page"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
    <div id="form" style="margin-right: auto; margin-left: auto;">
        <form id="form1" runat="server">
            <div id="myGroupsGrid" style="margin-left: 100px; margin-top: 20px; float: left; width: 1082px;" >
             <asp:GridView ID="myGroupsList" runat="server"  AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Justify" Width="18%" Height="171px">
                      <Columns>
                           <asp:HyperLinkField DataNavigateUrlFields="groupId" DataTextField="name" 
                                    meta:resourcekey="nameField" DataNavigateUrlFormatString="~/Pages/GroupPages/MyGroupsAndRecommendations.aspx?groupId={0}" />
                           <asp:TemplateField HeaderText="Darse de Baja">
                                  <ItemTemplate>
                                    <asp:Button ID="dropoutButton" meta:resourcekey="dropout" runat="server" OnClick="dropout_Click"
                                      CommandName = "migrupo" 
                                      CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                      Text="darse de baja" Height="20" Width="50" />
                                  </ItemTemplate> 
                           </asp:TemplateField>
                     </Columns>
            </asp:GridView>
                </div>
            <div id="recomGrid" style="margin-right: 100px; margin-top: 20px; float: right; height: 198px;">
            <asp:GridView ID="recommendationList" runat="server" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Left" Width="402%" Height="170px" style="margin-left: 0px">
                     <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="eventId" DataTextField="name" 
                                 meta:resourcekey="eventName" DataNavigateUrlFormatString="~/Pages/EventPages/EventComments.aspx?eventId={0}" />
                            <asp:BoundField DataField="recommendationText" />
                     </Columns>
            </asp:GridView> 
                </div>
              
        </form>
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

