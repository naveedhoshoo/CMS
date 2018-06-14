using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimManagement;
namespace CMS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dal objdal = new dal();
            DataSet1 ds =    objdal.GetPolicyData();
            CrystalReport1 objRpt = new CrystalReport1();
            objRpt.SetDataSource(ds.Tables[1]);
            CrystalReportViewer1.ReportSource = objRpt;

        }
    }
}