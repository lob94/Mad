<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="EventInfo.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.EventInfo" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
        <form id="form2" runat="server">
            <div style="width: 40%; margin-top:33px; margin-bottom:33px; font-size:20px;">
                <asp:Label ID="nombre" runat="server"> </asp:Label><br/>
                <asp:Label ID="categoria" runat="server"></asp:Label><br/>
                <asp:Label ID="review" runat="server"></asp:Label><br/>
                <asp:Label ID="date" runat="server"></asp:Label><br />
            </div>
            <div style="width: 40%; vertical-align:top; margin-right:40px; margin-top:-125px; float:right;">
      
            </div>
            <div style="float:none; margin-top:-125px; width:85%">
                  <asp:Button ID="addComentario" runat="server" Text="Añadir comentario" Width="160px" OnClick="addComentario_Click" Height="34px"></asp:Button><br />  
                  <asp:TextBox ID="introducirComentario" runat="server" Width="160px" Height="34px"></asp:TextBox>
            </div>
        </form>
</asp:Content>
