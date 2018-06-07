using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimManagement
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
           var login_name=    login_username.Text;
            var login_pass = login_password.Text;
            dal objdal = new dal();
            if (!objdal.isValidLogin(login_name, login_pass))
            {
                this.lblInavlid.Visible = true;
            }

            
        }
    }
}