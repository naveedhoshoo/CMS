﻿using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace ClaimManagement
{
    public partial class ManagementClaim : System.Web.UI.Page
    {
        private dal db;
        private DataTable dt;
        private ClaimClass cc;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillgrid();
                DropdownClient();
                DropdownBranch();
                DropdownStatus();
                DropdownInsuranceType();

            }
        }


        public void fillgrid()
        {
            try
            {
                db = new dal();
                dt = new DataTable();
                cc = new ClaimClass();
                cc.getClaimRecordDemo(dt);
                 DataTable brdt=  dt.DefaultView.ToTable(true, new String[] { "BR_CODE", "BR_DESC" });

                ddlBranch.DataTextField = "BR_DESC";
                ddlBranch.DataValueField = "BR_CODE";

                DataRow row = brdt.NewRow();
                row[0] = "";


                row[1] = "";

                brdt.Rows.InsertAt(row, 0);
                brdt.AcceptChanges();

                ddlBranch.DataSource = brdt;
                ddlBranch.DataBind();

                DataTable cldt = dt.DefaultView.ToTable(true, new String[] { "INSURED" });
                row = cldt.NewRow();
                row[0] = "";

                cldt.Rows.InsertAt(row, 0);
                cldt.AcceptChanges();


                ddlClient.DataTextField = "INSURED";
                ddlClient.DataValueField = "INSURED";
                ddlClient.DataSource = cldt;
                ddlClient.DataBind();

                DataTable cltype = dt.DefaultView.ToTable(true, new String[] { "piydesc" });

                row = cltype.NewRow();
                row[0] = "";

                cltype.Rows.InsertAt(row, 0);
                cltype.AcceptChanges();

                ddlInsuranceType.DataTextField = "piydesc";
                ddlInsuranceType.DataValueField = "piydesc";
                ddlInsuranceType.DataSource = cltype;
                ddlInsuranceType.DataBind();

                DataTable dtStaut = new DataTable();
                dtStaut.Columns.Add("Status");
                row = dtStaut.NewRow();
                row[0] = "Intimated";
                dtStaut.Rows.Add(row);
                row = dtStaut.NewRow();
                row[0] = "Settlement";
                dtStaut.Rows.Add(row);
                row = dtStaut.NewRow();
                row[0] = "Settled";
                dtStaut.Rows.Add(row);

                

                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField= "Status";
                ddlStatus.DataSource = dtStaut;
                ddlStatus.DataBind();

                DataTable dtYear = dt.DefaultView.ToTable(true, new String[] { "GIH_YEAR" });
                row = dtYear.NewRow();
                row[0] = "";
                dtYear.Rows.InsertAt(row, 0);
                dtYear.AcceptChanges();
                ddlgihyear.DataTextField = "GIH_YEAR";
                ddlgihyear.DataValueField = "GIH_YEAR";
                ddlgihyear.DataSource = dtYear;
                ddlgihyear.DataBind();


                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {

                if (e.CommandName.Equals("Add"))
                {
                    

                    //int index = int.Parse(e.CommandArgument.ToString());
                    //lblClaimIdx.Text = GridView1.DataKeys[index].Values["INTIMATIONNO"].ToString();
                    //Session["claimIdx"] = lblClaimIdx.Text;
                    //pnlPopUp.Visible = true;


                }
                else if (e.CommandName.Equals("view"))
                {
                    //int index = int.Parse(e.CommandArgument.ToString());
                    //lblIdx.Text = GridView1.DataKeys[index].Values["studentIdx"].ToString();
                    //Response.Redirect("studentRecord.aspx?stdIdx=" + lblIdx.Text);
                    pnlPopUpGrid.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }


        //protected void btn_click(object sender, EventArgs e) {
        //    GridViewRow gvrow = (GridViewRow)lnkPopUp.NamingContainer;

        //    lblClaimIdx.Text = gvrow.Cells[0].Text;
        //    Session["claimIdx"] = lblClaimIdx.Text;
        //    pnlPopUp.Visible = true;

        //    this.ModalPopupExtender1.Show();
        //}
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            fillgrid();
        }

        protected void lnkPopUp_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);
            lblClaimIdx.Text = GridView1.DataKeys[i].Values["INTIMATIONNO"].ToString();
            Session["claimIdx"] = lblClaimIdx.Text;
            this.mp1.Show();
            

        }

        protected void lnkPopUpGrid_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);
            lblClaimIdx.Text = GridView1.DataKeys[i].Values["INTIMATIONNO"].ToString();
            Session["claimDescIdx"] = lblClaimIdx.Text;
            this.mp1Grid.Show();
        }

       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            cc = new ClaimClass();
            cc.getClaimRecordsByCriteria(dt,"" , txtIntimationNo.Text, ddlBranch.SelectedItem.Text, ddlClient.SelectedItem.Text, ddlInsuranceType.SelectedItem.Text, ddlgihyear.SelectedItem.Text);

      
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        public void DropdownClient()
        {
         //   ddlClient.Items.Insert(0, new ListItem("Clients"));
        }
        public void DropdownBranch()
        {
           // ddlBranch.Items.Insert(0, new ListItem("Branch"));
        }
        public void DropdownInsuranceType()
        {
           // ddlInsuranceType.Items.Insert(0, new ListItem("Insurance Type"));
        }
         public void DropdownStatus()
        {
            //ddlStatus.Items.Insert(0, new ListItem("Status"));
        }
    }
}

