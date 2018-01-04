<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="EventInfo.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.EventInfo" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
        <form id="form2" runat="server">
           <div id="form" style="margin-right: auto; margin-left: auto;">
             <asp:Label ID="nombre" runat="server"> </asp:Label>
             <asp:Label ID="categoria" runat="server"></asp:Label>
             <asp:Label ID="review" runat="server"></asp:Label>
             <asp:Label ID="date" runat="server"></asp:Label>     
          </div>
          <div>
             <asp:GridView ID="comentarios" runat="server"
                 AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Justify" Width="50%">
                            <Columns>
                                <asp:BoundField DataField="loginName" meta:resourcekey="loginName" />
                                <asp:BoundField DataField="content" meta:resourcekey="content" />
                                <asp:BoundField DataField="commentDate" meta:resourcekey="commentDate" />
                            </Columns>
             </asp:GridView>
          </div>
          <div>
            <asp:Button ID="addComentario" runat="server" Text="Añadir comentario" Width="264px" OnClick="addComentario_Click"></asp:Button>
            <asp:TextBox ID="introducirComentario" runat="server" Width="247px"></asp:TextBox>
          </div>
        </form>
</asp:Content>
