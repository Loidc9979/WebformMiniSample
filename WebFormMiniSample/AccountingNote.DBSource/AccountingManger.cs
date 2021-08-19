using AccountingNote.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class AccountingManger
    {
        /// <summary>
        /// 查詢流水帳清單
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetAccountingList(string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT 
                        ID,
                        Caption,
                        Amount,
                        ActType,
                        CreateDate
                    FROM Accounting
                    WHERE UserID = @userID
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", userID));

            try
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 查詢流水帳清單
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<Accounting> GetAccountingList(Guid userID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Accountings
                         where item.UserID == userID
                         select item);

                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 查詢流水帳
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataRow GetAccounting(int id, string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT 
                        ID,
                        Caption,
                        Amount,
                        ActType,
                        CreateDate,
                        Body
                    FROM Accounting
                    WHERE id = @id AND UserID = @userID";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));
            list.Add(new SqlParameter("@userID", userID));

            try
            {
                return DBHelper.ReadDataRow(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 查詢流水帳
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static Accounting GetAccounting(int id, Guid userID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Accountings
                         where item.ID ==id && item.UserID == userID
                         select item);

                    var obj = query.FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 建立流水帳
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="caption"></param>
        /// <param name="amount"></param>
        /// <param name="actType"></param>
        /// <param name="body"></param>
        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {

            // <<<< check input >>>>
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000.");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("actType must be 0 or 1.");
            // <<<< check input >>>>

            string bodyColumnSQL = "";
            string bodyValueSQL = "";
            if (!string.IsNullOrWhiteSpace(body))
            {
                bodyColumnSQL = ", Body";
                bodyValueSQL = ", @Body";
            }

            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
            $@" INSERT INTO [dbo].[Accounting]
                (
                    UserID
                    ,Caption
                    ,Amount
                    ,ActType
                    ,CreateDate
                    {bodyColumnSQL}
                )
                VALUES
                (
                    @userID
                    ,@caption
                    ,@amount
                    ,@actType
                    ,@createDate
                    {bodyValueSQL}
                )";

            // connect db & execute
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@userID", userID);
                    comm.Parameters.AddWithValue("@caption", caption);
                    comm.Parameters.AddWithValue("@amount", amount);
                    comm.Parameters.AddWithValue("@actType", actType);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now);

                    if (!string.IsNullOrWhiteSpace(body))
                        comm.Parameters.AddWithValue("@body", body);

                    try
                    {
                        conn.Open();
                        var reader = comm.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                    }
                }
            }
        }

        /// <summary>
        /// 變更流水帳
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="userID"></param>
        /// <param name="caption"></param>
        /// <param name="amount"></param>
        /// <param name="actType"></param>
        /// <param name="body"></param>
        public static bool UpdateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        {

            // <<<< check input >>>>
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000.");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("actType must be 0 or 1.");
            // <<<< check input >>>>

            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" UPDATE [Accounting]
                    SET
                        UserID      =  @userID
                       ,Caption     =  @caption
                       ,Amount      =  @amount
                       ,ActType     =  @acttype
                       ,CreateDate  =  @createdate
                       ,Body        =  @body
                    WHERE
                       ID = @id ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@userID", userID));
            paramList.Add(new SqlParameter("@caption", caption));
            paramList.Add(new SqlParameter("@amount", amount));
            paramList.Add(new SqlParameter("@acttype", actType));
            paramList.Add(new SqlParameter("@createdate", DateTime.Now));
            paramList.Add(new SqlParameter("@body", body));
            paramList.Add(new SqlParameter("@id", ID));


            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, paramList);

                if (effectRows == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

        /// <summary>
        /// 刪除流水帳
        /// </summary>
        /// <param name="ID"></param>
        public static void DeleteAccounting(int ID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" DELETE [Accounting]
                    WHERE ID = @id ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@id", ID));

            try
            {
                DBHelper.ModifyData(connStr, dbCommand, paramList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
    }
}
