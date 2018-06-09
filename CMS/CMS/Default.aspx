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
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"s />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css">

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

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
            .modal {
             text-align: center;
            }

            @media screen and (min-width: 768px) { 
              .modal:before {
                display: inline-block;
                vertical-align: middle;
                content: " ";
                height: 100%;
              }
            }

        .modal-dialog {
          display: inline-block;
          text-align: left;
          vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#lnkforgetpassword').click(function () {
                $('#resetModal').modal('show');
            })
        })
        function sendResetRequest() {

            $.ajax({
                type: 'POST',
                // make sure you respect the same origin policy with this url:
                // http://en.wikipedia.org/wiki/Same_origin_policy
                url: 'http://nakolesah.ru/',
                data: {
                    'foo': 'bar',
                    'ca$libri': 'no$libri' // <-- the $ sign in the parameter name seems unusual, I would avoid it
                },
                success: function (msg) {
                    alert('wow' + msg);
                }
            });
        }
    </script>
  
   <ContentTemplate>
<div class="modal fade" id="resetModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header" style="background-color:green">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Password Reset</h4>
        </div>
        <div class="modal-body">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="text-center">
                          
                          <p>If you have forgotten your password you can reset it here.</p>
                            <div class="panel-body">
                                <fieldset>
                                    <div class="form-group">
                                        <input class="form-control input-lg" placeholder="E-mail Address" name="email" type="email">
                                    </div>
                                    <input class="btn btn-lg btn-success btn-block" value="Send My Password" type="submit">
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
  

    <div class="container">    
        <div id="loginbox" style="margin-top:50px;" class="mainbox col-md-4 col-md-offset-4 col-sm-8 col-sm-offset-2">                    
            <div class="panel panel-info" >
                    <div class="panel-heading">
                       <h4 class="panel-title"><a HREF="#"><img src="images/logo.png" class="img-responsive center-block"></a></h4>
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
                                        <asp:label id ="lblInavlid" runat="server"   style="font:bold; color:red;  " visible ="false"  Text ="Invalid Username or password"  >
                                            
                                        </asp:label>
                                            
                                        
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>
                            	
                             <div style="float:right; font-size: 80%; position: relative; top:-10px"><a href="#" id="lnkforgetpassword">Forgot password?</a></div>
                                <div style="margin-top:10px" class="form-group">
                                    <!-- Button -->

                                    <div class="col-sm-12 col-sm-offset-4 controls ">
                                      <asp:LinkButton id="lnk_login"  runat="server" OnClick="btnLogin_Click"  CssClass="btn btn-success" >Sign In  </asp:LinkButton>
                                      

                                    </div>
                                </div>

                                
                            </form>     



                        </div>                     
                    </div>  
        </div>
      
    </div>
        </ContentTemplate>
   
    


