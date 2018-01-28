<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="LabelComments.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.LabelComments" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
     <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
             <div id="form" align="left" style="margin-bottom: 100px; margin-left: 20px;">
               <asp:GridView ID="commentsList" runat="server" Font-Size="Medium"
                         AutoGenerateColumns="False"
                         ShowHeaderWhenEmpty="False" Visible="true" HorizontalAlign="Left" Width="70%" style="margin-left: 100px">
                                    <Columns>
                                        <asp:BoundField DataField="loginName"/>
                                        <asp:BoundField DataField="content"/>
                                        <asp:BoundField DataField="commentDate"/>
                                    </Columns>
                  <EmptyDataTemplate> <asp:Label runat="server" meta:resourcekey="gridVacio"/></EmptyDataTemplate>
                </asp:GridView>
                <br />
             </div>
</asp:Content>
