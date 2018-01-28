<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true"
    Codebehind="EditComment.aspx.cs" Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.EditComment" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
       
   <div id="form">
       <asp:Label ID="eventName" runat="server"></asp:Label>
        <div class="field">
            <br />
            <asp:TextBox ID="txtEdit" runat="server" TextMode="MultiLine" Columns="40" Rows="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvComment" runat="server"
                        ControlToValidate="txtEdit" Display="Dynamic"  meta:resourcekey="editComment"/>
        </div>
        <div class="button">
            <asp:Button ID="btnEdit" runat="server" meta:resourcekey="btnEdit" OnClick="BtnEditClick"/>
        </div>
        <br />
    </div>
    <ul>
        <li><asp:HyperLink ID="lnkAddLabel" runat="server" 
                    meta:resourcekey="lnkAddLabel"></asp:HyperLink></li>
        <li><asp:HyperLink ID="lnkRemoveLabel" runat="server" 
                    meta:resourcekey="lnkRemoveLabel"></asp:HyperLink></li>
    </ul>
     
</asp:Content>
