<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="ViewList.aspx.vb" Inherits="SafeCookSmartShop.ViewList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                            <div class="penci-recipe" id="printrepcipe6515" itemscope="" itemtype="http://schema.org/Recipe">
                                <div class="penci-recipe-heading">
                                    <h1 ><asp:Label ID="lblName" runat="server"></asp:Label></h1>

                                    <img style="display: none !important;" itemprop="image" src="http://cdn-soledad.pencidesign.com/soledad-food/wp-content/uploads/sites/2/2017/07/waffle-150x150.jpg" width="50" height="50">
                                    <i class="fa fa-user"></i>Estimated Cost: 
                                            <span class="servings" itemprop="recipeYield"><asp:Label ID="cost" runat="server" Text="$-"></asp:Label></span>
                                 </div>
                                <center>
                                         <asp:GridView ID="GridViewItems" runat="server" cssclass="table-bordered table-striped" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" EmptyDataText="no data found">
                                             <Columns>
                                                      <asp:TemplateField HeaderText="No." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>

                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:TemplateField>
                                                 <asp:BoundField DataField="item" HeaderText="Item" SortExpression="item" />
                                                 <asp:BoundField DataField="quantity" HeaderText="Quantity" SortExpression="quantity" />
                                                 <asp:BoundField DataField="cost" HeaderText="Cost" SortExpression="cost" />
                                             </Columns>
                                </asp:GridView>
                                <br />
                         
                                </center>
                                
                           

                             

                                      <div class="form-group text-right row">
                            <button type="submit" class="btn" runat="server" id ="btnUpdate" style="background-color:#ddd">Update</button>
                           <button type="button" class="btn btn-default" data-toggle="modal" data-target="#dialogModal">Back</button>
                        </div>

                            </div>
                        </div>
                        </div> 
                    </div> 
                </div>
         </section> 
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Provider=SQLNCLI11.1;Data Source=zionorion.database.windows.net;Password=SafeCookSmartShop01;User ID=orion;Initial Catalog=SafeCookSmartShop" SelectCommand="SELECT [quantity], [cost], [item] FROM [groceryitems] WHERE ([listid] = ?)" ProviderName="System.Data.OleDb">
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
