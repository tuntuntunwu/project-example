using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace FollowMEService
{
    public class CycleSweep
    {
        /// <summary>
        /// start sweep task
        /// </summary>
        public void TaskStart()
        {
            Log.log("Delete task service start!");
            //Add by Wei Changye 2012.02.27 cycle delete the print file------86400000 millisecond = 1 day
            System.Timers.Timer myTimer = new System.Timers.Timer(Convert.ToDouble(ServiceCommon.GetAppSettingString("RunCycle")));
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(CheckCycle);
            myTimer.Enabled = true;
        }

        #region CheckCycle
        /// <summary>
        /// DeleteFile
        /// </summary>
        /// <param name="lst"></param>
        /// <Date>2012.03.12</Date>
        /// <Author>SLC Wei Changye</Author>
        /// <Version>1.2</Version>
        public void CheckCycle(object source, System.Timers.ElapsedEventArgs e)
        {
            Log.log("Start Check Overdue Data...");
            int splPeriod = Convert.ToInt32(ServiceCommon.GetAppSettingString("SplPeriod"));
            //int userOper = Convert.ToInt32(ServiceCommon.GetAppSettingString("OpPeriod"));


            List<string> lstSplOverdue = new List<string>();
            DateTime splDeadLine =DateTime.Now.AddDays(-splPeriod);
            //DateTime userOperDeadLine = DateTime.Now.AddDays(-userOper);

            try
            {
                string sql = "SELECT   MFPPrintTask.* FROM  MFPPrintTask WHERE CreateTime < {0}";
                sql = string.Format(sql, ServiceCommon.ConvertDateToSQL(splDeadLine));
                DataTable table = ServiceCommon. ExecuteDataTable(sql);

                // Initial OverDue
                if (table != null)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        lstSplOverdue.Add(dr["MFPPrintTaskID"].ToString());
                    }
                }

                using (SqlConnection con = new SqlConnection(ServiceCommon.DBConnectionStrings))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();

                    // 1. delete file
                    DeleteFile(lstSplOverdue);

                    // 2. delete Data in DB
                    try
                    {
                        string deleteSqlstring = "DELETE FROM [MFPPrintTask] WHERE CreateTime < ({0})";
                        deleteSqlstring = string.Format(deleteSqlstring, ServiceCommon.ConvertDateToSQL(splDeadLine));
                        using (SqlCommand cmd = new SqlCommand(deleteSqlstring, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        //string updateSqlString = string.Format(
                        //    "UPDATE MFPPrintTask set State = '0' where CreateTime < ({0})", ServiceCommon.ConvertDateToSQL(userOperDeadLine));
                        //using (SqlCommand cmd = new SqlCommand(updateSqlString, con, tran))
                        //{
                        //    cmd.ExecuteNonQuery();
                        //}

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (tran.Connection != null)
                        {
                            tran.Rollback();
                        }
                        throw ex;
                    }
                    finally
                    {
                        tran.Dispose();
                        tran = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.log("SPL Delete & Update Exception:" + DateTime.Now + "\n");
                Log.log(ex.ToString());
                throw (ex);
            }
            Log.log("Clear Overdue Data End...");
        }

        #endregion

        #region DeleteFile
        /// <summary>
        /// DeleteFile
        /// </summary>
        /// <param name="lst"></param>
        /// <Date>2012.03.12</Date>
        /// <Author>SLC Wei Changye</Author>
        /// <Version>1.2</Version>
        private  bool DeleteFile(List<string> lst)
        {
            if (lst.Count > 0)
            {
                foreach (string item in lst)
                {
                    string sql = string.Format("SELECT MFPPrintTask.* FROM  MFPPrintTask WHERE MFPPrintTaskID={0}", ServiceCommon.ConvertIntToSQL(item));

                    DataTable table = ServiceCommon.ExecuteDataTable(sql);
                    string filePath = string.Empty;

                    foreach (DataRow dr in table.Rows)
                    {
                        if (dr["FileLocation"] != null && dr["DiskFileName"] != null)
                            filePath = dr["FileLocation"].ToString() + dr["DiskFileName"].ToString();
                    }
                    if (table == null)
                        return false;


                    //try to delete
                    if (!File.Exists(filePath))
                        continue;
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    //删除bk文件
                    string filePath_bk = filePath + "-bk";
                    if (!File.Exists(filePath_bk))
                        continue;
                    try
                    {
                        File.Delete(filePath_bk);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //

                }
                return true;
            }
            else
                return false;
        }

        #endregion

    }

}
