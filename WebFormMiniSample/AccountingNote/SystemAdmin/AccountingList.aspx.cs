using AccountingNote.Auth;
using AccountingNote.DBSource;
using AccountingNote.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class AccountingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // check is logined
            //if (this.Session["UserLoginInfo"] == null)
            if (!AuthManger.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var currentUser = AuthManger.GetCurrentUser();

            if (currentUser == null)
            // 如果帳號不存在，導至登入頁
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            // read accounting data
            //var dt = AccountingManger.GetAccountingList(currentUser.ID);
            var list = AccountingManger.GetAccountingList(currentUser.UserGuid);

            //if (dt.Rows.Count > 0)      // check is empty data
            //{
            //    var dtPaged = this.GetPagedDataTable(dt);

            //    this.ucPager2.TotalSize = dt.Rows.Count;
            //    this.ucPager2.Bind();

            //    this.gvAccountingList.DataSource = dtPaged;
            //    this.gvAccountingList.DataBind();

            //}
            //else
            //{
            //    this.gvAccountingList.Visible = false;
            //    this.plcNoData.Visible = true;
            //}

            if (list.Count > 0)      // check is empty data
            {
                //var dtPaged = this.GetPagedDataTable(list);

                //this.gvAccountingList.DataSource = dtPaged;

                var pagedList = this.GetPagedDataTable(list);
                this.gvAccountingList.DataSource = pagedList;
                this.gvAccountingList.DataBind();

                //this.ucPager2.TotalSize = list.Rows.Count;
                this.ucPager2.TotalSize = list.Count;
                this.ucPager2.Bind();
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.plcNoData.Visible = true;
            }
        }

        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;

            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;

            if (intPage <= 0)
                return 1;

            return intPage;
        }

        private List<Accounting> GetPagedDataTable(List<Accounting> list)
        {
            int pageSize = this.ucPager2.PageSize;
            int startIndex = (this.GetCurrentPage() - 1) * pageSize;
            return list.Skip(startIndex).Take(pageSize).ToList();
        }

        private DataTable GetPagedDataTable(DataTable dt)
        {
            DataTable dtPaged = dt.Clone();
            int pageSize = this.ucPager2.PageSize;

            int startIndex = (this.GetCurrentPage() - 1) * pageSize;
            int endIndex = (this.GetCurrentPage()) * pageSize;
            if (endIndex > dt.Rows.Count)
                endIndex = dt.Rows.Count;

            for (var i = startIndex; i < endIndex; i++)
            {
                DataRow dr = dt.Rows[i];
                var drNew = dtPaged.NewRow();

                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }
                dtPaged.Rows.Add(drNew);
            }
            return dtPaged;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }

        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

            if (row.RowType == DataControlRowType.DataRow)
            {
                //Literal ltl = row.FindControl("ltActType") as Literal;
                Label lbl = row.FindControl("lblActType") as Label;
                //ltl.Text = "OK";

                //var dr = row.DataItem as DataRowView;
                //int actType = dr.Row.Field<int>("ActType");
                var rowData = row.DataItem as Accounting;
                int actType = rowData.ActType;

                if (actType == 0)
                {
                    //ltl.Text = "支出";
                    lbl.Text = "支出";
                }
                else
                {
                    //ltl.Text = "收入";
                    lbl.Text = "收入";
                }

                if (rowData.Amount > 1500)
                {
                    lbl.ForeColor = Color.Red;
                }
            }
        }
    }
}