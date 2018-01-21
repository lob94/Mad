<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="EventInfo.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.EventInfo" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
        <form id="form2" runat="server">
            <div id="atributosEvento" style="width: 40%; margin-left:300px; margin-top:33px; margin-bottom:33px; font-size:20px;">
                <asp:Label ID="nombre" meta:resourcekey="nombre" runat="server"> </asp:Label><br/>
                <asp:Label ID="categoria" meta:resourcekey="categoria" runat="server"></asp:Label><br/>
                <asp:Label ID="review" meta:resourcekey="review" runat="server"></asp:Label><br/>
                <asp:Label ID="date" meta:resourcekey="date" runat="server"></asp:Label><br/>
            </div>
            <div id="addCommentDiv" style="margin-left:-200px;  margin-top:-125px; margin-bottom:33px; width:39%; float: right;">
                  <asp:Button ID="addComentario" meta:resourcekey="addComment" runat="server" Text="Añadir comentario" Width="160px" OnClick="addComentario_Click" Height="34px"></asp:Button><br />  
                  <asp:TextBox ID="introducirComentario" meta:resourcekey="writeComment" runat="server" Width="160px" Height="34px"></asp:TextBox>
            </div>
        </form>
</asp:Content>
