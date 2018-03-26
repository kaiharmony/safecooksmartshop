<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="SearchRecipe.aspx.vb" Inherits="SafeCookSmartShop.SearchRecipe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
       $(function () {


           $("#liRecipe").toggleClass("active");
           $("#liHome").removeClass("active");
           $("#liShopping").removeClass("active");


       });

    </script>
            <link href="Content/ViewRecipe.css" rel="stylesheet" />
    <style type="text/css">
    th
    {
        text-align:center!important;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <section id="portfolio" class="portfolio">
            <div class="container">
                <div class="row">
                    <div class="main_mix_content text-center sections">
                       
                        <div class="wrapper-penci-recipe">
    <center>
            <asp:GridView ID="GridViewItems" runat="server"  ShowHeaderWhenEmpty="true" cssclass="table-bordered table-striped" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" EmptyDataText="no data found">
                <Columns>
                        <asp:TemplateField HeaderText="No." ItemStyle-Width="5%">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
                            <HeaderStyle Font-Size="Large" />
            <ItemStyle Width="5%" Font-Size="Large"></ItemStyle>
        </asp:TemplateField>
        <asp:ButtonField CommandName="recipename" DataTextField="recipename"  HeaderText="Recipe Name" ShowHeader="True" sortexpression="SmallToolID" >
                        <HeaderStyle Font-Size="Large" />
                        <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
                        </asp:ButtonField>
            <asp:BoundField DataField="recipeid" ItemStyle-CssClass="hidden" 
                HeaderStyle-CssClass="hidden" >                                                   
                            <HeaderStyle CssClass="hidden"></HeaderStyle>
                            <ItemStyle CssClass="hidden"></ItemStyle>
             </asp:BoundField>
            <asp:BoundField DataField="serving" HeaderText="Servings">
                      <HeaderStyle Font-Size="Large" />
                        <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
                </asp:BoundField>
            <asp:BoundField DataField="ethnicity" HeaderText="Ethnicity">
                   <HeaderStyle Font-Size="Large" />
                        <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
                </asp:BoundField>                                                 
                </Columns>
</asp:GridView>
<br />                         
 </center>
                            </div>
                        </div>
                    </div>
                </div>
         </section>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
         ConnectionString="Provider=SQLNCLI11.1;Data Source=zionorion.database.windows.net;Password=SafeCookSmartShop01;User ID=orion;Initial Catalog=SafeCookSmartShop" 
        SelectCommand="SELECT recipeid , recipename, ethnicity, serving FROM [recipe] WHERE ([recipeid] >= 7 )" ProviderName="System.Data.OleDb">
     
     </asp:SqlDataSource>
</asp:Content>
