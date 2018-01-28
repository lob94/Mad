<%@ Page Language="C#" MasterPageFile="~/Miniportal.Master" AutoEventWireup="true" CodeBehind="NewRecommendationForm.aspx.cs" 
    Inherits="Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages.NewRecommendationForm" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    
 
        <asp:GridView ID="groupsList" runat="server"
                            AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" HorizontalAlign="Justify" Width="50%" style="margin-left: 404px">
                            <Columns>
                                <asp:BoundField DataField="name" meta:resourcekey="name" />
                                <asp:TemplateField  HeaderText="Seleccionar">
                                  <ItemTemplate>
                                    <asp:Checkbox ID="Sel" runat="server"/>
                                  </ItemTemplate> 
                                </asp:TemplateField>
                            </Columns>
            </asp:GridView>
        <asp:TextBox ID="textEntry" runat="server" Width="500px" Height="100px" TextMode="MultiLine" MaxLength="50"/>
        <asp:Button ID="addRecommendation" runat="server" OnClick="AddRecommendation_Click" Text="Añadir"/>
  
    
</asp:Content>