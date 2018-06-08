using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimManagement
{
    public partial class popup : System.Web.UI.Page
    {
        private dal db;
        private ClaimClass cc;
        private DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDropDown();
                fillCalimStatusDropDown();
                //if (Request.QueryString["claimIdx"] != null)
                //{
                //    int claimIdx = int.Parse(Request.QueryString["claimIdx"]);

                //}
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                cc = new ClaimClass();
                dt = new DataTable();
                cc.statusIdx = int.Parse(ddlClaimStatus.SelectedValue.ToString());
                cc.userIdx = "Kashif";
                cc.holdIdx = int.Parse(ddlClaimHold.SelectedValue.ToString());
                cc.desc = txtdescription.Text;
                cc.Date = DateTime.Now.ToShortDateString();
                cc.claimIdx = int.Parse(Session["claimIdx"].ToString());
                cc.insertClaims(dt);

                lblMsg.Text = "Save Successfully";
                txtdescription.Text = "";
                ddlClaimHold.SelectedIndex = ddlClaimStatus.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        public void fillDropDown()
        {
            try
            {
                db = new dal();
                dt = new DataTable();
                cc = new ClaimClass();
                ddlClaimHold.DataValueField = "idx";
                ddlClaimHold.DataTextField = "ClaimHold";
                cc.getClaimHold(dt);
                ddlClaimHold.DataSource = dt;
                ddlClaimHold.DataBind();
                ddlClaimHold.Items.Insert(0, new ListItem("Select Claim Hold."));
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }


        }
        public void fillCalimStatusDropDown()
        {
            try
            {
                db = new dal();
                dt = new DataTable();
                cc = new ClaimClass();
                ddlClaimStatus.DataValueField = "idx";
                ddlClaimStatus.DataTextField = "ClaimStatus";
                cc.getClaimStatus(dt);
                ddlClaimStatus.DataSource = dt;
                ddlClaimStatus.DataBind();
                ddlClaimStatus.Items.Insert(0, new ListItem("Select Claim Status."));
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }


        }

    }
}
