using System;
using System.Collections.Generic;
using System.Web;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using Osa.MfpWebService;
using Osa.Util;
using System.Text;

/// <summary>
///FollowMEHandler 的摘要说明
/// </summary>
/// add by Wei Changye 2012.03.29
/// 
public class FollowMEHandler
{
    dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter;

	public FollowMEHandler()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public void MFPEntry(string sn, string userID)
    {
        if (sn != null)
        {
            Helper.DeviceSession dev = Helper.DeviceSession.Get(sn);
            //dev.LogUserIn(userID);
            dev.LogUserIn(sn, userID);
        }
    }

    public void MFPExit(string sn)
    {
        if (sn != null)
        {
            try
            {
                Helper.DeviceSession dev = Helper.DeviceSession.Get(sn);
                MFPCoreWSEx mfpWS = dev.GetConfiguredMfpCoreWS();
                mfpWS.ShowScreen(E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN);
            }
            catch (Exception e)
            {

            }
        }
    }

    #region BuildSQL
    /// <summary>
    /// BuildSQL
    /// </summary>
    /// <param name="loginName"></param>
    /// <returns></returns>
    public string BuildSQL(string loginName)
    {
        return string.Format("Select MFPPrintTaskID,LoginName,DiskFileName,FileLocation,PrintFileName,CreateTime,State" +
        " from MFPPrintTask where LoginName ={0} and State = '1' order by CreateTime asc", loginName);
    }

    #endregion

    #region DeleteTask
    /// <summary>
    /// DeleteTask
    /// </summary>
    /// <param name="lst"></param>
    public void DeleteTask(List<string> lst)
    {
        if (lst.Count > 0)
        {
            string str = string.Empty;
            foreach (string item in lst)
            {
                if (str == "")
                    str = UtilCommon.ConvertStringToSQL(item);
                else
                    str += "," + UtilCommon.ConvertStringToSQL(item);
            }

            using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    //not delete ,system will delete the file after cycle
                    // 2012.03.02 Wei Changye
                    ////1. Delete file
                    //if (!DeleteFile(lst))
                    //{
                    //    ErrorAlert(UtilConst.MSG_DELETE_FILE_ERROR);
                    //}

                    //1. Update info set state = 0
                    string sql = string.Format("UPDATE MFPPrintTask set State = '0' where MFPPrintTaskID in ({0})", str);
                    using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran.Connection != null)
                    {
                        tran.Rollback();
                    }
                    Global.Log(ex.ToString());
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                }
            }
        }
    }

    #endregion

    #region DeleteFile
    /// <summary>
    /// DeleteFile
    /// </summary>
    /// <param name="lst"></param>
    public bool DeleteFile(List<string> lst)
    {
        if (lst.Count > 0)
        {
            foreach (string item in lst)
            {
                if (adpter == null)
                    adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();

                dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(Convert.ToInt32(item));
                if (table == null)
                    return false;

                //get path
                string filePath = table[0].FileLocation + table[0].DiskFileName;

                //try to delete
                if (!File.Exists(filePath))
                    return false;
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return true;
        }
        else
            return false;
    }

    #endregion

    #region PrintTask
    /// <summary>
    /// PrintTask
    /// </summary>
    /// <param name="lst"></param>
    /// <Date>2012.02.27</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    public void PrintTask(List<string> lst, string remoteIP, string loginName)
    {
        // OSA API Print 
        // edit by Wei Changye 2012.02.08
        ////MFPPrintDirect print = new MFPPrintDirect();
        //print.RunWSCalls(lst);

        //FTP Print
        //FtpSend send = new FtpSend();
        //foreach (string item in lst)
        //{
        //send.UploadFile(item);
        //}

        //SocketSend socketSend = new SocketSend(remoteIP, loginName);
        foreach (string item in lst)
        {
            //socketSend.Send(item);
            //ThreadPool.QueueUserWorkItem(socketSend.Send, item);



            Thread handlThread = new Thread(new ParameterizedThreadStart(new SocketSend(remoteIP).Send));
            handlThread.Start(item);
        }

    }

    #endregion

    #region Send Msg
    /// <summary>
    /// SendMsg
    /// </summary>
    /// <param name="lst"></param>
    /// <Date>2012.03.27</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    public void SendMsg(string msg, string remoteIP)
    {
        using (SocketSend s =new SocketSend(remoteIP))
        {
            s.SendMsg(msg , remoteIP);
        }
    }

    #endregion

    #region Send Print Task ID

    /// <summary>
    /// SendMsg
    /// </summary>
    /// <param name="lst"></param>
    /// <Date>2012.03.27</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    public void SendPrintTaskID(List<string> list, string remoteIP,string followMEServerIP)
    {
        StringBuilder sb = new StringBuilder();
        //int interval = Convert.ToInt32(UtilCommon.GetAppSettingString("Interval"));
        foreach (string item in list)
        {
            if (sb.ToString() != "")
                sb.Append(UtilConst.FOLLOWME_SPLIT_SYMBOL + item);
            else
                sb.Append(item);
            //SendMsg("sendData@" + item + "@" + remoteIP + "@", followMEServerIP);
            //Thread.Sleep( interval * 100);

        }

        SendMsg("sendData@" + sb.ToString() + "@" + remoteIP + "@", followMEServerIP);
    }

    #endregion

    //public void DeleteCopyPdf(List<string> list, string followMEServerIP)
    //{
    //    foreach (string item in list)
    //    {
    //        SendMsg("DelCopyPDF@" + item + "@", followMEServerIP);
    //        Thread.Sleep(1000);
    //    }

    //}
}