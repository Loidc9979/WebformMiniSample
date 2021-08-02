using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManger.IsLogined())
            // 如果尚未登入，導至登入頁
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var currentUser = AuthManger.GetCurrentUser();

            if (currentUser == null)
            // 如果帳號不存在，導至登入頁
            {
                Response.Redirect("/Login.aspx");
                return;
            }
        }
    }
}