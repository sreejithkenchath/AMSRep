<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AMS_SuperAdmin.Forms.Login" %>
<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title>AMS</title>

    <link rel="icon" href="faviconya.ico" type="image/x-icon" />

    <link rel="stylesheet" type="text/css" href="../Content/css/bootstrap.min.css"  />
    <link rel="stylesheet" type="text/css" href="../Content/css/main.css">

    <script type="text/javascript" src="../Content/js/jquery.min.js"></script>
    <!--[if lt IE 9]>
    <script src="js/html5.js"></script>
    <![endif]-->
</head>

<body>
    <br/><br/><br/><br/><br/>
    <form id="Login" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-6 ">
                <div class="login_wrapper">
                    <div class="login_head">
                        <div class="glyphicon glyphicon-user"></div>
                        <h3>LOGIN</h3>
                    </div>

                    <div class="login_content pull-left">
                        <div class="clearfix">&nbsp;</div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                            <asp:TextBox ID="txtUserName" runat="server" placeholder="User Name" CssClass="textbox"></asp:TextBox></div> 
                              
                            <div class="col-md-1"></div>
                        </div>
                        <div class="clearfix">&nbsp;</div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" 
                                    CssClass="textbox" TextMode="Password"></asp:TextBox></div> 
                            <div class="col-md-1"></div>
                        </div>
                        <div class="clearfix">&nbsp;</div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-5">
                            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-success login_btn" 
                                    Text="Login" onclick="btnLogin_Click" />
                                                           
                        </div>
                                                        <div class="col-md-5">
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger login_btn" Text="Clear" />
                                                           
                        </div>
                            <div class="col-md-1"></div>
                    </div>
                        <div class="clearfix">&nbsp;</div>
                        <div class="row">
                            <p class="frgt_pw">Forgot Password? <a href="#">Click Here to Reset</a></p>
                        </div>

                </div>

                    <div class="new_reg_panel">
                        
                        <p><a href="mailto:sree@gmail.com">Click Here</a> for New Registration</p>
                    </div>


            </div>
            <div class="col-md-3"></div>
        </div>
    </div>
</div>

</form>
    <script type="text/javascript" src="../Content/js/bootstrap.min.js"></script>        
</body>
</html>

