using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.Session["UserLoginInfo"] !=null)
            {
                this.plcLogin.Visible = false;
                Response.Redirect("/systemAdmin/UserInfo.aspx");
            }
            else
            {
                this.plcLogin.Visible = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPWD.Text;

            string msg;
            if (!AuthManger.TryLogin(inp_Account, inp_PWD, out msg))
            {
                this.ltlMsg.Text = "Account / PWD is required.";
                return;
            }

            Response.Redirect("/SystemAdmin/UserInfo.aspx");
        }
    }
}