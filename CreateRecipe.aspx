<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="CreateRecipe.aspx.vb" Inherits="SafeCookSmartShop.CreateRecipe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {          


                $("#liRecipe").toggleClass("active");
                $("#liHome").removeClass("active");
                $("#liShopping").removeClass("active");


        });
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:ScriptManager ID="ScriptManagerRecipe" runat="server"></asp:ScriptManager>
      <section id="portfolio" class="portfolio">
            <div class="container">
                <div class="row">
                    <div class="main_mix_content text-center sections">
                        <div class="head_title">
                            <h2>Create New Recipe</h2>
                        </div>
                        <div class="form-group row">
                            <label for="recipeName" class="col-sm-2 col-form-label">Recipe:</label>
                            <div class="col-sm-10">
                              <input type="text" class="form-control" required id="recipeName" runat="server"
                                   placeholder="Recipe Name">
                            </div>
                          </div>
                        <div class="form-group row">
                            <label for="serving" class="col-sm-2 col-form-label">Serving:</label>
                            <div class="col-sm-10">
                              <input type="text" class="form-control" id="serving" placeholder="serving size" runat="server">
                            </div>
                          </div>
                        <div class="form-group row">
                            <label for="serving" class="col-sm-2 col-form-label">Ethnicity:</label>
                            <div class="col-sm-10">
                              <asp:DropDownList ID="ethnicity" runat="server" CssClass="form-control" placeholder="test">
                                  <asp:ListItem>--Select an Ethnicity--</asp:ListItem>
                                  <asp:ListItem>Creole</asp:ListItem>
                                  <asp:ListItem>Chinese</asp:ListItem>
                                  <asp:ListItem>Garifuna</asp:ListItem>
                                  <asp:ListItem>Maya</asp:ListItem>
                                  <asp:ListItem>East Indian</asp:ListItem>
                                  <asp:ListItem>Korean</asp:ListItem>
                                  <asp:ListItem>Taiwanese</asp:ListItem>
                                  <asp:ListItem>Mennonite</asp:ListItem>
                                  <asp:ListItem>Lebanese</asp:ListItem>
                                  <asp:ListItem>Mestizo</asp:ListItem>
                                  <asp:ListItem>Syrian</asp:ListItem>
                                  <asp:ListItem>Other</asp:ListItem>
                              </asp:DropDownList>
                            </div>
                          </div>
                        <div class="form-group row">
                            <label for="uploadPicture" class="col-sm-2 col-form-label">Picture:</label>
                            <div class="col-sm-10">
                                <asp:FileUpload ID="uploadPicture" runat="server" ToolTip="Currently unable to upload pictures" Enabled="false" CssClass="for-control" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="itemList" class="col-sm-2 col-form-label">Items:</label>
                            <div class="col-sm-10">
                                 <asp:UpdatePanel ID="UpdatePanelDisplay" runat="server">
                                        <ContentTemplate>                                  
                                             <asp:GridView ID="GridViewCreateDisplay" runat="server" CssClass="table table-bordered table-responsive "
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                                TabIndex="8">
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
                                                    <asp:TemplateField HeaderText="Measurement" HeaderStyle-BackColor="White">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtbxCreateMeasurement" runat="server" class="form-control" Width="80%" ValidationGroup="PageValidate" CausesValidation="True"></asp:TextBox>
                                                             <asp:RequiredFieldValidator ID="rfv_txtbxCreateMeasurement" runat="server" ErrorMessage="Field cannot be empty."
                                                                ControlToValidate="txtbxCreateMeasurement" Display="None" ValidationGroup="PageValidate"></asp:RequiredFieldValidator>
                                                           
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
                        <div class="form-group row">
                            <label for="directionList" class="col-sm-2 col-form-label">Directions:</label>
                            <div class="col-sm-10">
                                 <asp:UpdatePanel ID="UpdatePanelDirection" runat="server">
                                        <ContentTemplate>                                
                                             <asp:GridView ID="GridViewCreateDirection" runat="server" CssClass="table table-bordered table-responsive "
                                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                                TabIndex="8">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Directions" HeaderStyle-BackColor="White">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Directions" Text="" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:ButtonField CommandName="Deleterow" HeaderStyle-BackColor="White" HeaderText="Delete"
                                                        ShowHeader="True" ImageUrl="~/assets/images/delete.png" ButtonType="Image" ControlStyle-Width="40" ControlStyle-Height="20" />
                                                </Columns>
                                            </asp:GridView>
                                            <button type="submit" class="floatingButton" runat="server" id="addDirection" style="font-size: x-large;">+</button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                            </div>
                        </div>
                        <div class="form-group text-right row">
                            <button type="submit" class="btn " style="background-color:#ddd;border-color:black;" runat="server" id="btnSave">Save</button>
                             <button type="button" class="btn btn-default" data-toggle="modal" data-target="#dialogModal">Back</button>
                        </div>
                        </div>
                    </div>
                </div>
          </section>
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
