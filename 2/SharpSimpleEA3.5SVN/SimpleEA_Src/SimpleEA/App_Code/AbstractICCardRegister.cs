using System;
using System.Collections.Generic;
using System.Web;
using SesMiddleware;
using System.Collections.Specialized;
using Osa.Util;
using System.Data.SqlClient;

/// <summary>
///add by Wei Changye 2011.12.27 use template pattern to format the register process
/// </summary>
public abstract class AbstractICCardRegister : System.Web.UI.Page
{
	public AbstractICCardRegister()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    protected void RegisterProcessTemplate(string loginName,string pwd,string cardId)
    {
        if (CheckValidate(loginName, pwd))
        {
            RegisterICcard(loginName, pwd, cardId);
            RegisterComplete();
            RedirectToLogin();
        }
        else
        {
            RegisterError();
        }
    }
    protected void LDAPRegisterProcessTemplate(string loginName, string pwd, string cardId)
    {
            RegisterComplete();
    }
    private bool CheckValidate(string loginName, string pwd)
    {
        dtUserInfoTableAdapters.UserInfoTableAdapter adpt = new dtUserInfoTableAdapters.UserInfoTableAdapter();

        object count = adpt.AuthenticationMFP(loginName, pwd);

        if (count != null && (int)count > 0)
            return true;
        else
            return false;
    }

    private void RegisterICcard(string loginName, string pwd, string cardID)
    {
        string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");

        using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                //DB
                // 1. keep card for one person
                // delete reapt record
                string updateSql;
                updateSql = "   UPDATE [UserInfo]          " + Environment.NewLine;
                updateSql += "  SET                        " + Environment.NewLine;
                updateSql += "       [ICCardID] = {0}      " + Environment.NewLine;
                updateSql += "WHERE ICCardID = {1}   " + Environment.NewLine;

                string[] updateParamslist = new string[2];
                updateParamslist[0] = UtilCommon.ConvertStringToSQL("");
                updateParamslist[1] = UtilCommon.ConvertStringToSQL(cardID);
                updateSql = string.Format(updateSql, updateParamslist);
                using (SqlCommand cmd = new SqlCommand(updateSql, con, tran))
                {
                    cmd.ExecuteNonQuery();
                }

                string strSql;
                //2. update user info
                strSql = "   UPDATE [UserInfo]          " + Environment.NewLine;
                strSql += "  SET                        " + Environment.NewLine;
                strSql += "       [ICCardID] = {0}      " + Environment.NewLine;
                // Add BY JiJianxiong 2010-07-09 ED
                strSql += "WHERE LoginName = {1}   " + Environment.NewLine;

                string[] paramslist = new string[2];
                // Card ID
                paramslist[0] = UtilCommon.ConvertStringToSQL(cardID);
                // LoginName
                paramslist[1] = UtilCommon.ConvertStringToSQL(loginName);

                strSql = string.Format(strSql, paramslist);

                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    cmd.ExecuteNonQuery();
                }

                
                // end DB

                //XML
                if (ICCardData.CheckUser(loginName, serverPath))
                    ICCardData.DeleteICCardInfoByUserName(loginName, serverPath);
                if (ICCardData.CheckCardID(cardID, serverPath))
                    ICCardData.DeleteICCardInfoByCardID(cardID, serverPath);

                //add new
                ICCardData.AddICCardInfo(cardID, loginName, pwd, serverPath);
                //end XML

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

    protected void RedirectToLogin()
    {
        //Helper.DeviceSession dev = null;
        //if (Application["strserialNumber"] != null)
        //{
        //    dev = Helper.DeviceSession.Get(Application["strserialNumber"].ToString());
        //    MFPCoreWSEx mfpWS = dev.GetConfiguredMfpCoreWS();
        //    Application.Clear();
        //    mfpWS.ShowScreen("../Default.aspx");
        //}
        //else
        //    Response.Redirect("../Main.aspx");
    }

    protected abstract void InitialParam(NameValueCollection collection);

    protected abstract void RegisterComplete();

    protected abstract void RegisterError();
}