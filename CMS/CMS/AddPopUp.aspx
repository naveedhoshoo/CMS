<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPopUp.aspx.cs" Inherits="ClaimManagement.popup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .h1 {
            text-align: center;
            font-size: x-large;
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="container-fluid">
            <br />
            <h1 class="h1">Management Claim</h1>
            <br />
            <div class="row">
                <div class="col-sm-4">
                    <asp:Label ID="lbl_description" runat="server" Text="Description:" Font-Size="Larger"></asp:Label>
                    <asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" Width="255px" CssClass="form-control" Height="40px" placeholder="Please enter Description"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:Label ID="lblCliamHold" runat="server" Text="Claim Hold:" Font-Size="Larger"></asp:Label>
                    <asp:DropDownList ID="ddlClaimHold" runat="server" CssClass="form-control" Width="255px" Height="40px"></asp:DropDownList>
                </div>
                <div class="col-sm-4">
                 
                    <asp:Label ID="lblStatus" runat="server" Text="Status:" Font-Size="Larger"></asp:Label>
                     <asp:DropDownList ID="ddlClaimStatus" runat="server" CssClass="form-control" Width="255px" Height="40px"></asp:DropDownList>
                 <%--   <asp:CheckBox ID="chkStatus" runat="server" Text="YES/NO" />--%>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group" style="padding-left:350px">
                        <div class="col-md-12">
                            <asp:Button ID="btnAdd"  class="button" runat="server" Text="Add" Width="80px" Height="40px" CssClass="btn btn-success" OnClick="btnAdd_Click" />
                            <br />
                            <asp:Label ID="lblMsg" ForeColor="Green" Font-Bold="true" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:Label ID="lblError" ForeColor="Red" Font-Bold="true" runat="server" Text=""></asp:Label>
            </div>
        </div>
       <br />
    </form>

</body>

</html>
