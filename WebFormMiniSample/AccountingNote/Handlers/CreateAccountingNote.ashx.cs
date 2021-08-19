using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingNote.Handlers
{
    /// <summary>
    /// CreateAccountingNote 的摘要描述
    /// </summary>
    public class CreateAccountingNote : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
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
            if(!int.TryParse(amountText, out tempAmount) ||
               !int.TryParse(actTypeText,out tempActType))
            {
                this.ProcessError(context, "Amount, ActType should be a integer.");
                return;
            }

            // 建立流水帳
            AccountingManger.CreateAccounting(id, caption, tempAmount, tempActType, body);

            context.Response.ContentType = "text/plain";
            context.Response.Write("OK");
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