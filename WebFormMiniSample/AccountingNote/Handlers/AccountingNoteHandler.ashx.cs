using AccountingNote.DBSource;
using AccountingNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AccountingNote.ORM.DBModels;

namespace AccountingNote.Handlers
{
    /// <summary>
    /// AccountingNoteHandler 的摘要描述
    /// </summary>
    public class AccountingNoteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string actionName = context.Request.QueryString["ActionName"];

            if (string.IsNullOrWhiteSpace(actionName))
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                context.Response.Write("ActionName is required");
                context.Response.End();
            }

            if(actionName == "create")
            {
                string caption = context.Request.Form["Caption"];
                string amountText = context.Request.Form["Amount"];
                string actTypeText = context.Request.Form["ActType"];
                string body = context.Request.Form["Body"];

                // ID of Nick
                string id = "CB1F7A59-A960-417D-A2CE-49F34AA9DBD7";

                if (string.IsNullOrWhiteSpace(caption) ||
                    string.IsNullOrWhiteSpace(amountText) ||
                    string.IsNullOrWhiteSpace(actTypeText))
                {
                    this.ProcessError(context, "caption, amount, actType is required.");
                    return;
                }

                // 轉型
                int tempAmount, tempActType;
                if (!int.TryParse(amountText, out tempAmount) ||
                   !int.TryParse(actTypeText, out tempActType))
                {
                    this.ProcessError(context, "Amount, ActType should be a integer.");
                    return;
                }
                try
                {
                    // 建立流水帳
                    AccountingManger.CreateAccounting(id, caption, tempAmount, tempActType, body);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("OK");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 503;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("ERROR");
                }
            }
            else if(actionName == "update")
            {

            }
            else if (actionName == "delete")
            {

            }
            else if (actionName == "list")
            {
                Guid userGUID = new Guid("CB1F7A59-A960-417D-A2CE-49F34AA9DBD7");

                List<Accounting> sourceList = AccountingManger.GetAccountingList(userGUID);
                List<AccountingNoteViewModel> list =
                sourceList.Select(obj => new AccountingNoteViewModel()
                {
                    ID = obj.ID,
                    Caption = obj.Caption,
                    Amount = obj.Amount,
                    ActType = (obj.ActType == 0) ? "支出" : "收入",
                    CreateDate = obj.CreateDate.ToString("yyyy-MM-dd")
                }).ToList();

                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
            else if (actionName == "query")
            {
                string idText = context.Request.Form["ID"];
                int id;
                int.TryParse(idText, out id);
                Guid userGUID = new Guid("CB1F7A59-A960-417D-A2CE-49F34AA9DBD7");

                var accounting = AccountingManger.GetAccounting(id, userGUID);

                if (accounting == null)
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("No Data: " + idText);
                    context.Response.End();
                }

                AccountingNoteViewModel model = new AccountingNoteViewModel()
                {
                    ID = accounting.ID,
                    Caption = accounting.Caption,
                    Body = accounting.Body,
                    Amount = accounting.Amount,
                    ActType = (accounting.ActType == 0) ? "支出" : "收入",
                    CreateDate = accounting.CreateDate.ToString("yyyy-MM-dd")
                };

                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
        }

        public void ProcessError(HttpContext context, string msg)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
            context.Response.End();
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