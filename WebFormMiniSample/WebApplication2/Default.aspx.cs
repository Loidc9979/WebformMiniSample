using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WebApplication2
{
    public partial class Default : System.Web.UI.Page
    {
        public int ForJSInt { get; set; } = 500;
        public bool ForJSBool { get; set; } = true;
        public string ForJSString { get; set; } = "Fuck";


        protected void Page_Load(object sender, EventArgs e)
        {
            this.hf2.Value = "Test Message";
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            //this.lblName.Text = this.txt1.Text;
            this.lblName.Text = this.hf1.Value;
        }
    }
}