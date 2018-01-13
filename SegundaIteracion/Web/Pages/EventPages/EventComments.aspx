<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="EventComments.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.EventComments" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
     <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
    <form id="form1" runat="server">
           <asp:GridView ID="comentariosList" runat="server" Font-Size="Medium"
                     AutoGenerateColumns="False"
                                ShowHeaderWhenEmpty="True" Visible="true" HorizontalAlign="Right" Width="70%">
                                <Columns>
                                    <asp:BoundField DataField="loginName" meta:resourcekey="loginName"/>
                                    <asp:BoundField DataField="content" meta:resourcekey="content" />
                                    <asp:BoundField DataField="commentDate" meta:resourcekey="commentDate" />
                                </Columns>
            </asp:GridView>
            <asp:Button ID="volverHome" runat="server" Width="160px" OnClick="volver_Click" Height="34px"></asp:Button><br />
    </form>
</asp:Content>
