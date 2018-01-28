<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="EventInfo.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.EventInfo" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
       
            <div id="atributosEvento" style="width: 30%; float:left; margin-left:20px; margin-top:33px; margin-bottom:33px; margin-right: -300px; font-size:20px;">
                <asp:Label ID="nombre" meta:resourcekey="nombre" runat="server"> </asp:Label><br/>
                <asp:Label ID="categoria" meta:resourcekey="categoria" runat="server"></asp:Label><br/>
                <asp:Label ID="review" meta:resourcekey="review" runat="server"></asp:Label><br/>
                <asp:Label ID="date" meta:resourcekey="date" runat="server"></asp:Label><br/>
            </div>
            <!-- <div id="addCommentDiv" style="margin-left:0px;  margin-top: -20px; margin-bottom:100px; width:40%; height: 100px;"> -->
                  <asp:TextBox ID="introducirComentario" meta:resourcekey="writeComment" runat="server" Width="160px" Height="40px"></asp:TextBox>
                  <asp:Button ID="addComentario" meta:resourcekey="addComment" runat="server" Text="Añadir comentario" Width="160px" 
                      OnClick="addComentario_Click" Height="34px" MaxLength="150">
                  </asp:Button><br />  
                  <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="introducirComentario"
                    Display="Dynamic" meta:resourcekey="rfvcommentResource1">
                  </asp:RequiredFieldValidator>
                <asp:Button ID="commentsButton" meta:resourcekey="comments" runat="server" Width="250px" OnClick="comments_Click" Height="34px"
                     style="">
                </asp:Button>
                  <asp:Button ID="addRecommendation" meta:resourcekey="addRecommendation" runat="server" Width="100px" Visible="false" OnClick="addRecommendation_Click" Height="34px"
                    style="">
                </asp:Button>
          <!--  </div> -->
     
</asp:Content>
