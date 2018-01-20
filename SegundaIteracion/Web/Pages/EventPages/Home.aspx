<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.Home" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form" style="margin-right: auto; margin-left: auto;">
        <form id="form1" runat="server">
            <div class="form-group">
                <asp:TextBox ID="textEntry" meta:resourcekey="searchEventName" runat="server" AutoPostBack="True"> </asp:TextBox>
                <asp:DropDownList ID="dropDownList" meta:resourcekey="searchCategory" runat="server" DataTextField="name" DataValueField="categoryId" 
                    AppendDataBoundItems="True"></asp:DropDownList>
                <asp:Button ID="searchButton" runat="server" OnClick="search_Click" meta:resourcekey="btnSearch" Text="Search" />
            <asp:GridView ID="eventList2" runat="server"
                            AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Justify" Width="50%">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="eventId" DataTextField="name" 
                                    meta:resourcekey="nameField" DataNavigateUrlFormatString="~/Pages/EventPages/EventInfo.aspx?eventId={0}" />
                                <asp:BoundField DataField="name" />
                                <asp:BoundField DataField="categoryName"/>
                            </Columns>
            </asp:GridView>
                <asp:Button ID="myGroupsButton" runat="server" OnClick="myGroups_Click" meta:resourcekey="btnMyGroups" Text="My Groups" />
                <asp:Button ID="newGroupButton" runat="server" OnClick="newGroup_Click" meta:resourcekey="btnNewGroup" Text="Create New Group" />
            </div>
                <asp:Button ID="recommendationsButton" runat="server" OnClick="myGroups_Click" meta:resourcekey="btnRecommendations" Text="Show Recommendations" Width="386px" />
                <asp:Button ID="showGroupsButton" runat="server" OnClick="showGroups_Click" meta:resourcekey="btnGroups" Text="Show Groups" />
        </form>
        </div>
        <div class="previousNextLinks">
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
