<%@ Page Title="Home Page" Language="C#"  AutoEventWireup="true" CodeBehind="Default.aspx.cs"  Inherits="ClaimManagement._Default" %>

<!DOCTYPE html>
<!--[if IE 9 ]><![endif]-->
<meta charset="utf-8" />
<meta content="IE=edge" http-equiv="X-UA-Compatible" />
<meta content="width=device-width, initial-scale=1" name="viewport" />
<title>Log In</title>

    <!-- Vendor CSS -->
<link href="vendors/bower_components/animate.css/animate.min.css" rel="stylesheet" />
<link href="vendors/bower_components/material-design-iconic-font/dist/css/material-design-iconic-font.min.css" rel="stylesheet" />

    <!-- CSS -->
<link href="css/app.min.1.css" rel="stylesheet" />
<link href="css/app.min.2.css" rel="stylesheet" />
<style type="text/css">
          .panel-front { /* Only for this preview */
                margin-bottom:20px;
                margin-top:20px;
            }





            .panel-front .panel-heading .panel-title img {
	            border-radius:3px 3px 0px 0px;
	            width:100%;
                vertical-align : middle;
            }

            .panel-front .panel-heading {
	            padding: 0px;	
            }

            .panel-front .panel-heading h4 {
	            line-height: 0;
            }

            .panel-front .panel-body h4 {
	            font-weight: bold;
	            margin-top: 5px;
	            margin-bottom: 15px;
            }

            .text-right {
                margin-top: 10px;
            }
    </style>

  
   <ContentTemplate>
    <div class="container">    
        <div id="loginbox" style="margin-top:50px;" class="mainbox col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2">                    
            <div class="panel panel-info" >
                    <div class="panel-heading">
                       <h4 class="panel-title"><a HREF="#"><img src="images/logo.png" class="center"></a></h4>
                    </div>     

                    <div style="padding-top:30px" class="panel-body" >

                        <div style="display:none" id="login-alert" class="alert alert-danger col-sm-12"></div>
                            
                        <form id="loginform" runat="server" class="form-horizontal" role="form">
                                    <asp:ScriptManager ID="scMain" runat="server">
                                     </asp:ScriptManager>
      
                            <div style="margin-bottom: 25px" class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:TextBox id="login_username" runat="server" type="text" CssClass="form-control" name="username" value="" placeholder="username or email" />                                        
                                    </div>
                                
                            <div style="margin-bottom: 25px" class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                        <asp:TextBox TextMode="password" id="login_password" runat="server" CssClass="form-control" name="password" placeholder="password" />
                                    </div>
                                    
                                
                                
                                <div class="input-group">
                                        <asp:label id ="lblInavlid" runat="server" style="display:none;" text="Invalid Username or Password" >
                                            <span style="font:bold; color:red; " > Invalid Username or Password  </span>
                                        </asp:label>
                                            
                                        
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>
                            	
                             <div style="float:right; font-size: 80%; position: relative; top:-10px"><a href="#">Forgot password?</a></div>
                                <div style="margin-top:10px" class="form-group">
                                    <!-- Button -->

                                    <div class="col-sm-12 controls">
                                      <asp:LinkButton id="lnk_login"  runat="server" OnClick="btnLogin_Click"  CssClass="btn btn-success" >Sign In  </asp:LinkButton>
                                      

                                    </div>
                                </div>

                                
                            </form>     



                        </div>                     
                    </div>  
        </div>
      
    </div>
        </ContentTemplate>
   
    


