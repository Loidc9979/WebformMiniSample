using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    public class AuthManger
    {
        /// <summary>
        /// 負責處理登入的元件
        /// </summary>
        /// <returns></returns>
        public static bool IsLogined()
        {
            /// <summary>
            /// 檢查目前是否登入
            /// </summary>
            /// <returns></returns>
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }
    }
}
