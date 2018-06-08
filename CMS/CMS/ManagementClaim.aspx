<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagementClaim.aspx.cs" MasterPageFile="~/ManagementClaimMaster.Master" Inherits="ClaimManagement.ManagementClaim" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<!DOCTYPE html>--%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12" style="display: -webkit-inline-box;">

                <asp:Label ID="lblSearch" runat="server" Font-Bold="true" Style="font-size: larger" Text="Search By:"></asp:Label>
                <asp:DropDownList ID="ddlClient" Style="margin-left: 10px" runat="server" CssClass="form-control" Width="170px" Height="40px">
                    <asp:ListItem> Indus Motors</asp:ListItem>
                    <asp:ListItem>Habib Sugar</asp:ListItem>
                    <asp:ListItem>Metro</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtPolicyNo" Style="margin-left: 10px" runat="server" Width="170px" CssClass="form-control" Height="40px" placeholder="Policy No."></asp:TextBox>
                <asp:TextBox ID="txtIntimationNo" Style="margin-left: 10px" runat="server" Width="170px" CssClass="form-control" Height="40px" placeholder="Intimation No."></asp:TextBox>
                <asp:DropDownList ID="ddlBranch" Style="margin-left: 10px" runat="server" CssClass="form-control" Width="170px" Height="40px">
                    <asp:ListItem>Karachi Zonel</asp:ListItem>
                    <asp:ListItem>Lahore Gulberg</asp:ListItem>
                    <asp:ListItem>Lahore Eden</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlInsuranceType" Style="margin-left: 10px" runat="server" CssClass="form-control" Width="170px" Height="40px">
                    <asp:ListItem>Fire</asp:ListItem>
                    <asp:ListItem>Marine</asp:ListItem>
                    <asp:ListItem>Motor</asp:ListItem>

                </asp:DropDownList>
                <asp:DropDownList ID="ddlStatus" Style="margin-left: 10px" runat="server" CssClass="form-control" Width="170px" Height="40px">
                    <asp:ListItem>Intimated</asp:ListItem>
                    <asp:ListItem>Settlement</asp:ListItem>
                    <asp:ListItem>Settled</asp:ListItem>
                </asp:DropDownList>

                <asp:Button ID="btnSearch" class="button" Style="margin-left: 10px" runat="server" Text="Search" Width="80px" Height="40px" CssClass="btn btn-success" OnClick="btnSearch_Click" />
            </div>
        </div>


        <br />
        <div class="GridDiv">
            <asp:GridView ID="GridView1" runat="server"
                AllowPaging="true" PageSize="10"
                CssClass="table table-striped table-bordered responsive" AutoGenerateColumns="false"
                OnRowCommand="GridView1_RowCommand" AllowSorting="true" DataKeyNames="INTIMATIONNO" OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging" EnableViewState="true">
                   <HeaderStyle CssClass="GridHeader" Font-Size="Larger" ForeColor="Black" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField HeaderText="Intimation No" DataField="gih_doc_ref_no" />
                    <asp:BoundField HeaderText="Year" DataField="gih_year" />
                    <asp:BoundField HeaderText="Claimant" DataField="INSURED" />
                    <asp:BoundField HeaderText="Branch" DataField="BR_DESC" />
                    <asp:BoundField HeaderText="Department" DataField="PDP_DESC" />
                    <asp:BoundField HeaderText="Insurance Type" DataField="piydesc" />
                    <asp:BoundField HeaderText="Loss Amount" DataField="TOTALLOSS" />
                    <asp:BoundField HeaderText="Total Paid" DataField="TOTALPAID" />
                    <asp:BoundField HeaderText="OS" DataField="OS" />


                    <%--
 <asp:BoundField HeaderText="PPS_PARTY_CODE" DataField="PPS_PARTY_CODE" />
                     <asp:BoundField HeaderText="BR_CODE" DataField="BR_CODE" />
              

                    <asp:BoundField HeaderText="TOTALPAID" DataField="TOTALPAID" />
                    <asp:BoundField HeaderText="GIC_SHARE" DataField="GIC_SHARE" />--%>



                    <asp:TemplateField HeaderText="">
                        <ItemTemplate >
                            <%--<asp:LinkButton ID="lnkPopUp" OnClick="lnkPopUp_Click" runat="server" Text="ADD" CommandName="Add">
                                </asp:LinkButton>--%>
                            <asp:Button ID="lnkPopUp" Width="60px" Height="30px" CssClass="btn btn-success"  OnClick="lnkPopUp_Click" Font-Size="XX-Small" runat="server" Text="ADD" CommandName="Add" />


                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button ID="lnkPopUpGrid" Width="60px" Height="30px" CssClass="btn btn-success" Font-Size="XX-Small" OnClick="lnkPopUpGrid_Click" runat="server" Text="VIEW" CommandName="view">
                                    
                            </asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

             
                <%-- <FooterStyle Font-Size="Larger" ForeColor="Black" HorizontalAlign="Center"  />--%>
                <PagerStyle   CssClass="pagination-ys" HorizontalAlign="Right" />
            </asp:GridView>

        </div>

        <cc1:ModalPopupExtender ID="mp1" TargetControlID="btnShowPopupAdd" runat="server" PopupControlID="pnlPopUp"
            CancelControlID="btnClose" BackgroundCssClass="Background">
        </cc1:ModalPopupExtender>

        <asp:Button ID="btnShowPopupAdd" runat="server" Style="display: none" />


        <cc1:ModalPopupExtender ID="mp1Grid" runat="server" PopupControlID="pnlPopUpGrid" TargetControlID="btnShowPopUpView"
            CancelControlID="btnClose" BackgroundCssClass="Background">
        </cc1:ModalPopupExtender>

        <asp:Button ID="btnShowPopUpView" runat="server" Style="display: none" />



        <asp:Panel ID="pnlPopUp" runat="server" CssClass="Popup" align="center" Style="display: grid">
            <iframe style="width: 890px; height: 500px;" id="irm1" src="AddPopUp.aspx" runat="server"></iframe>
            <br />
            <asp:Button ID="btnClose" runat="server" Width="80px" Height="40px" CssClass="btn btn-success" Text="Close" />

        </asp:Panel>

        <asp:Panel ID="pnlPopUpGrid" runat="server" CssClass="Popup" align="center" Style="display: grid">
            <iframe style="width: 890px; height: 500px;" id="irm2" src="ViewPopUp.aspx" runat="server"></iframe>
            <br />
            <asp:Button ID="btnCloseGrid" runat="server" Width="80px" Height="40px" CssClass="btn btn-success" Text="Close" />

        </asp:Panel>
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblClaimIdx" runat="server" Text=""></asp:Label>
    </div>
    <br />


</asp:Content>
<%--    </form>
</body>
</html>--%>
