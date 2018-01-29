<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.Home" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form" style="margin-right: auto; margin-left: auto;">
      
            <div class="form-group">
                <asp:TextBox ID="textEntry" meta:resourcekey="searchEventName" runat="server" AutoPostBack="False"
                    style="margin-left: 0px;">
                </asp:TextBox>
                <asp:DropDownList ID="dropDownList" runat="server" DataTextField="name" DataValueField="categoryId" 
                    AppendDataBoundItems="True" style="margin-left: 10px;"></asp:DropDownList>
                <asp:Button ID="searchButton" runat="server" OnClick="search_Click" meta:resourcekey="btnSearch" Text="Search"
                    style=" margin-left: 10px;" />
                <asp:GridView ID="eventList2" runat="server"
                            AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="False" HorizontalAlign="Justify" Width="50%" style="margin-left: 500px">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="eventId" DataTextField="name" 
                                    meta:resourcekey="nameField" DataNavigateUrlFormatString="~/Pages/EventPages/EventInfo.aspx?eventId={0}" />
                                <asp:BoundField DataField="review" meta:resourcekey="review" />
                                <asp:BoundField DataField="categoryName" meta:resourcekey="categoryName" />
                            </Columns>
                 </asp:GridView>
            </div>
       
        </div>
        <div class="previousNextLinks" style="margin-bottom: 30px;">
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
