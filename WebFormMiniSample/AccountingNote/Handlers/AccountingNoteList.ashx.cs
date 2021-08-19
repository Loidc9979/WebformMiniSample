using AccountingNote.DBSource;
using AccountingNote.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccountingNote.Handlers
{
    /// <summary>
    /// AccountingNoteList 的摘要描述
    /// </summary>
    public class AccountingNoteList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string account = context.Request.QueryString["Account"];

            if (string.IsNullOrWhiteSpace(account))
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            var dr = UserInfoManger.GetUserInfoByAccount(account);

            if (dr == null)
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            string userID = dr["ID"].ToString();
            DataTable datatable = AccountingManger.GetAccountingList(userID);


            List<AccountingNoteViewModel> list = new List<AccountingNoteViewModel>();
            foreach(DataRow drAccounting in datatable.Rows)
            {
                AccountingNoteViewModel model = new AccountingNoteViewModel()
                {
                    ID = drAccounting["ID"].ToString(),
                    Caption = drAccounting["Caption"].ToString(),
                    Amount = drAccounting.Field<int>("Amount"),
                    ActType = (drAccounting.Field<int>("ActType") == 0) ? "支出" : "收入",
                    CreateDate = drAccounting.Field<DateTime>("CreateDate").ToString("yyyy-MM-dd")
                };
            }

            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(datatable);

            context.Response.ContentType = "application/json";
            context.Response.Write(jsonText);

            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}