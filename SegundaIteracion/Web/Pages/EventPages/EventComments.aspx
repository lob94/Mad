<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="EventComments.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages.EventComments" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
     <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
      
             <div id="form" align="left" style="margin-bottom: 20px; margin-left: 20px;">
               <asp:GridView ID="comentariosList" runat="server" Font-Size="Medium"
                         AutoGenerateColumns="False"
                         ShowHeaderWhenEmpty="False" Visible="true" HorizontalAlign="Left" Width="70%" style="margin-left: 100px">
                                    <Columns>
                                        <asp:BoundField DataField="loginName"  meta:resourcekey="loginName"/>
                                        <asp:BoundField DataField="content"  meta:resourcekey="content"/>
                                        <asp:BoundField DataField="commentDate"  meta:resourcekey="date"/>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="edit" runat="server" OnClick="edit_Click" meta:resourcekey="btnEdit" Visible='<%# visibility((String)Eval("loginName")) %>'/>
                                            </ItemTemplate> 
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="remove" runat="server" CommandName="Remove" CommandArgument='<%#(Eval("commentId")) %>' OnCommand="remove_Click" meta:resourcekey="btnRemove" Visible='<%# visibility((String)Eval("loginName")) %>'/>
                                            </ItemTemplate> 
                                        </asp:TemplateField>
                                    </Columns>
                  <EmptyDataTemplate> <asp:Label runat="server" meta:resourcekey="gridVacio"/></EmptyDataTemplate>
                </asp:GridView>
                <asp:Button ID="volverHome" meta:resourcekey="volverHome" runat="server" Width="194px" OnClick="volver_Click" Height="34px"
                 style=" margin-left: 20px; margin-top: 100px;">
                </asp:Button>
                <br />
             </div>
      
</asp:Content>
