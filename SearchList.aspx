<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="SearchList.aspx.vb" Inherits="SafeCookSmartShop.SearchList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <script type="text/javascript">
                $(function () {

                    $("#liShopping").toggleClass("active");
                    $("#liRecipe").removeClass("active");
                    $("#liHome").removeClass("active");

                });
    </script>
        <link href="Content/ViewRecipe.css" rel="stylesheet" />
    <style type="text/css">
    th
    {
        text-align:center!important;
    }
        table {
            min-width: 500px;
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

                            <HeaderStyle Font-Size="X-Large" />

            <ItemStyle Width="5%" Font-Size="X-Large"></ItemStyle>
        </asp:TemplateField>
        <asp:ButtonField CommandName="listname" DataTextField="listname"  HeaderText="List Name" ShowHeader="True" sortexpression="SmallToolID" >
                        <HeaderStyle Font-Size="X-Large" />
                        <ItemStyle Font-Size="X-Large" HorizontalAlign="Center" />
                        </asp:ButtonField>
    <asp:BoundField DataField="listid" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" >
                                                 
<HeaderStyle CssClass="hidden"></HeaderStyle>

<ItemStyle CssClass="hidden"></ItemStyle>
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
        SelectCommand="SELECT listname , listid FROM [grocerylist] WHERE ([listid] >= 4 )" ProviderName="System.Data.OleDb">
     
     </asp:SqlDataSource>
</asp:Content>
