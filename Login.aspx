<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="SafeCookSmartShop.Login" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Safe Cook Smart Shop</title>
    <!-- Bootstrap Core CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="Content/Signin.css" rel="stylesheet" />

    <!-- jQuery -->
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <!-- Bootstrap Core JavaScript -->
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container thumbnail">

            <div class="form-signin">

                <h2 class="form-signin-heading">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/assets/images/homebg.jpg" Height="253px" Width="300px" />
                    Safe Cook Smart Shop</h2>

                <label for="txtbxLoginName">Login Name</label>
                <input type="text" id="txtbxLoginName" required class="form-control" placeholder="Login Name" autofocus="" runat="server"
                    validationgroup="Login" targetcontrolid="txtbxLoginName" causesvalidation="True" />

      
               

                <label for="txtbxPassword">Password</label>
                <input type="password" id="txtbxPassword" class="form-control" placeholder="Password" runat="server"
                    validationgroup="Loginform" causesvalidation="True" required />
           
              
                <asp:Button ID="btnSignIn" runat="server" Text="Sign In" CssClass="btn btn-lg btn-primary btn-block" ValidationGroup="LoginForm"  />
                <br />
               
                <asp:Label ID="lblError" runat="server" Font-Italic="true" ForeColor="Red" Font-Size="small"></asp:Label>
            </div>


        </div>
              <!-- Footer -->
        <footer>
            <div class="row">
                <div class="col-lg-12" style="text-align: center;">
                    <p>Copyright © Safe Cook Smart Shop</p>
                </div>
            </div>
        </footer>
    </form>
</body>
</html>
