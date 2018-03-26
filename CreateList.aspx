<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="CreateList.aspx.vb" Inherits="SafeCookSmartShop.CreateList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
            $(function () {

                $("#liShopping").toggleClass("active");
                $("#liRecipe").removeClass("active");
                $("#liHome").removeClass("active");

            });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
          <section id="portfolio" class="portfolio">
            <div class="container">
                <div class="row">
                    <div class="main_mix_content text-center sections">
                        <div class="head_title">
                            <h2>Create New List</h2>
                        </div>
                        <div class="form-group row">
                            <label for="ListName" class="col-sm-2 col-form-label">List Name:</label>
                            <div class="col-sm-10">
                              <input type="text" required  class="form-control" id="ListName" runat="server"
                                   placeholder="List Name">
                            </div>
                          </div>
  
                        <div class="form-group row">
                            <label for="itemList" class="col-sm-2 col-form-label">Items:</label>
                            <div class="col-sm-10">
                                 <asp:UpdatePanel ID="UpdatePanelDisplay" runat="server">
                                        <ContentTemplate>                                  
                                             <asp:GridView ID="GridViewCreateDisplay" runat="server" CssClass="table table-bordered table-responsive "
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                                TabIndex="8" >
                                                <Columns>
                                                     <asp:TemplateField HeaderText="No." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item" HeaderStyle-BackColor="White">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Item" Text="" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-BackColor="White">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtbxQuantity" runat="server" class="form-control" Width="80%" ValidationGroup="PageValidate" CausesValidation="True"></asp:TextBox>
                                                             <asp:RequiredFieldValidator ID="rfv_txtbxQuantity" runat="server" ErrorMessage="Field cannot be empty."
                                                                ControlToValidate="txtbxQuantity" Display="None" ValidationGroup="PageValidate"></asp:RequiredFieldValidator>
                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cost" HeaderStyle-BackColor="White">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtbxCost" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:ButtonField CommandName="Deleterow" HeaderStyle-BackColor="White" HeaderText="Delete"
                                                        ShowHeader="True" ImageUrl="~/assets/images/delete.png" ButtonType="Image" ControlStyle-Width="40" ControlStyle-Height="20" />
                                                </Columns>
                                            </asp:GridView>
                                            <button type="submit" class="floatingButton" runat="server" id="btnAddItem" style="font-size: x-large;">+</button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                            </div>
                        </div>
                        
                        <div class="form-group text-right row">
                            <button type="submit" class="btn" runat="server" style="background-color:#ddd;border-color:black;" id="btnSaveList">Save</button>
                           <button type="button" class="btn btn-default" data-toggle="modal" data-target="#dialogModal">Back</button>
                        </div>
                        </div>
                    </div>
                </div>
          </section>
        
               <asp:SqlDataSource ID="SqlDataSourceList" runat="server"
              ConnectionString="<%$ ConnectionStrings:myConnectionString %>" 
                SelectCommand="select quantity, cost, item from groceryitems where listid = @listid"></asp:SqlDataSource>

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
