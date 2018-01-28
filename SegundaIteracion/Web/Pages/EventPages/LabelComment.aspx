<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="LabelComment.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.LabelComment" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <div style="text-align: left;">
           <asp:CheckBoxList ID="checkboxLabels" runat="server" RepeatColumns="5">
           </asp:CheckBoxList>
         </div>
            <div class="button">
                <asp:Button ID="btnAddLabel" runat="server" meta:resourcekey="addLabel" OnClick="btnAddClick"/>
            </div>
            <br />
     </div>
</asp:Content>
