<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="EventComments.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.EventComments" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
     <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
    <div id="container">
        <div id="form" align="left" style="margin-bottom: 10px; margin-left: 10px;">
        <form id="form1"  runat="server">
               <asp:GridView ID="comentariosList" runat="server" Font-Size="Medium"
                         AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" Visible="true" HorizontalAlign="Left" Width="70%" style="margin-left: 100px">
                                    <Columns>
                                        <asp:BoundField DataField="loginName"/>
                                        <asp:BoundField DataField="content"/>
                                        <asp:BoundField DataField="commentDate"/>
                                    </Columns>
                </asp:GridView>
            <asp:Button ID="volverHome" meta:resourcekey="volverHome" runat="server" Width="194px" OnClick="volver_Click" Height="34px"
                style=" margin-left: 0px; ">
            </asp:Button>
            <br />
          </form>
        </div>
    </div>
</asp:Content>
