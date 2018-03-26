<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Default.aspx.vb" Inherits="SafeCookSmartShop._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

          <section id="portfolio" class="portfolio">
            <div class="container">
                <div class="row">
                    <div class="main_mix_content text-center sections">
                        <div class="head_title">
                            <h2>My Favorites</h2>
                        </div>
                      
                        <div id="mixcontent" class="mixcontent" >
                            <div class="col-md-4 mix cat1">
                                <div class="single_mixi_portfolio">
                                   <a href="ViewRecipe.aspx?recipeid=7"><img src="assets/images/pf1.jpg" alt="" style="width:80%"/></a>
                                    <div class="mixi_portfolio_overlay">
                                        <div class="overflow_hover_text">
                                            <h2>SALMON</h2>                                           
                                            <a href="ViewRecipe.aspx?recipeid=7"><i class="lnr lnr-plus-circle"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 mix cat2" style="padding-left:10px!important;padding-right:10px!important;">
                                <div class="single_mixi_portfolio">
                                    <a href="ViewRecipe.aspx?recipeid=9"><img src="assets/images/jhonnycakes.jpg" alt="" style="width:80%" /></a>
                                    <div class="mixi_portfolio_overlay">
                                        <div class="overflow_hover_text">
                                            <h2>Jhonny Cakes</h2>
                                            <a href="ViewRecipe.aspx?recipeid=9"><i class="lnr lnr-plus-circle"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 mix cat3">
                                <div class="single_mixi_portfolio">
                                    <a href="ViewRecipe.aspx?recipeid=8"><img src="assets/images/pf3.jpg" alt="" style="width:80%" /></a>
                                    <div class="mixi_portfolio_overlay">
                                        <div class="overflow_hover_text">
                                            <h2>SHRIMP & ASPARAGUS</h2>                                          
                                            <a href="ViewRecipe.aspx?recipeid=8"><i class="lnr lnr-plus-circle"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                         
            
                            </div>

                            <div class="gap"></div>
                        </div>
                    </div>                     
                </div>
          
        </section>
     <!-- End of portfolio two Section --> 

</asp:Content>
