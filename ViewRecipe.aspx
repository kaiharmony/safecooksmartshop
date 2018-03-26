<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="ViewRecipe.aspx.vb" Inherits="SafeCookSmartShop.ViewRecipe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/ViewRecipe.css" rel="stylesheet" />
    <style type="text/css">
    th
    {
        text-align:center!important;
    }
    </style>
       <script type="text/javascript">
           $(function () {


               $("#liRecipe").toggleClass("active");
               $("#liHome").removeClass("active");
               $("#liShopping").removeClass("active");


           });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section id="portfolio" class="portfolio">
            <div class="container">
                <div class="row">
                    <div class="main_mix_content text-center sections">
                       
                        <div class="wrapper-penci-recipe">
                            <div class="penci-recipe" id="printrepcipe6515" itemscope="" itemtype="http://schema.org/Recipe">
                                <div class="penci-recipe-heading">
                                    <h1 ><asp:Label ID="lblName" runat="server"></asp:Label></h1>

                                    <img style="display: none !important;" itemprop="image" src="http://cdn-soledad.pencidesign.com/soledad-food/wp-content/uploads/sites/2/2017/07/waffle-150x150.jpg" width="50" height="50">

                                    <div class="penci-recipe-meta">
                                        <span>
                                            <i class="fa fa-user"></i>Serves: 
                                            <span class="servings" itemprop="recipeYield"><asp:Label ID="servings" runat="server"></asp:Label></span>
                                            <i class="fa fa-user"></i>Ethnicity: 
                                            <span class="servings" itemprop="recipeYield"><asp:Label ID="ethnicity" runat="server"></asp:Label></span>
                                             <i class="fa fa-user"></i>Estimated Cost: 
                                            <span class="servings" itemprop="recipeYield"><asp:Label ID="cost" runat="server" Text="$-"></asp:Label></span>
                                        </span>
                                      
                                    </div>
                                </div>
                                <center>
                                    <h2>Ingredients</h2>
                                         <asp:GridView ID="GridViewIngredients" runat="server" cssclass="table-bordered table-striped" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BorderStyle="None" EmptyDataText="no data found" BorderWidth="0px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate><ItemStyle Width="5%"></ItemStyle>
                                                    </asp:TemplateField>
                                        <asp:BoundField DataField="ingredientname" HeaderText="Item" SortExpression="ingredientname" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="align-center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="measurement" HeaderText="Measurement" SortExpression="measurement" >
                                        <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cost" HeaderText="Cost" SortExpression="cost" >
                                        <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                    <h2>Directions</h2>
                                <asp:GridView ID="GridViewDirections" runat="server" CssClass="table-bordered table-striped" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" BorderStyle="None" BorderWidth="0px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>

                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="directions" HeaderText="Directions" SortExpression="directions">

                                            <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                    </Columns>
                                </asp:GridView>
                                    <br />
                                </center>
                                
                           

                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="Provider=SQLNCLI11.1;Data Source=zionorion.database.windows.net;Password=SafeCookSmartShop01;User ID=orion;Initial Catalog=SafeCookSmartShop" ProviderName="System.Data.OleDb" SelectCommand="SELECT [directions] FROM [recipedirections] WHERE ([recipeid] = ?)">
                                    <SelectParameters>
                                        <asp:QueryStringParameter DefaultValue="1" Name="recipeid" QueryStringField="recipeid" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>

                                      <div class="form-group text-right row">
                            <button type="submit" class="btn" runat="server" id ="btnUpdate" style="background-color:#ddd;border-color:black;">Update</button>
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target="#dialogModal">Back</button>
                        </div>

                            </div>
                        </div>
                        </div> 
                    </div> 
                </div>
         </section> 
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Provider=SQLNCLI11.1;Data Source=zionorion.database.windows.net;Password=SafeCookSmartShop01;User ID=orion;Initial Catalog=SafeCookSmartShop" SelectCommand="SELECT [ingredientname], [cost], [measurement] FROM [ingredients] WHERE ([recipeid] = ?)" ProviderName="System.Data.OleDb">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="1" Name="recipeid" QueryStringField="recipeid" Type="Int32" />
        </SelectParameters>
     </asp:SqlDataSource>

    <div class="modal" tabindex="-1" role="dialog" id="dialogModal">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Safe Cook Smart Shop</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <p>Are you sure you want to leave this page?</p>
      </div>
      <div class="modal-footer">
          <asp:Button ID="btnYes" runat="server" Text="Yes" cssclass="btn btn-primary" PostBackUrl="~/Default.aspx" />
        <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
      </div>
    </div>
  </div>
</div>
</asp:Content>
