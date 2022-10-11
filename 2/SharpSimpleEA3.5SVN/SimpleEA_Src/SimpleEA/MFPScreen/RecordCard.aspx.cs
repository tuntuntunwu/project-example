using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Osa.Util;
using Osa.MfpWebService;
using System.Data.SqlClient;
using SesMiddleware;

public partial class MFPScreen_RecordCard : MainOSA
{
    // Control for error message display.
    protected ErrorDisp div_errordisp;
    private E_EA_OSA_TYPE type;
    private string userID;
    public string timeOutPeriod = UtilCommon.GetAppSettingString("TimeOutPeriod").ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        hidRecordTime.Value = (new DateTime().Minute).ToString();

        SetTdVisible();
        div_errordisp = new ErrorDisp(span_msg);

        // OSA version
        if (Request.Params["type"] != null)
        {
            OSAType.Value = Request.Params["type"].ToString();
            hidUrlType.Value = OSAType.Value.ToString().Equals(E_EA_OSA_TYPE.OSAMAIN.ToString()) ? "../Main.aspx" : "../OsaMain4.aspx";
        }


        // User login id
        if (Request.Params["loginid"] != null)
        {
            ViewState["loginid"] = Request.Params["loginid"].ToString();
            Session["loginid"] = Request.Params["loginid"].ToString();
        }

        if (ViewState["loginid"] != null)
        {
            userID = ViewState["loginid"].ToString();
        }


        // Exit Initial
        if (ViewState["strserialNumber"] == null)
            ViewState["strserialNumber"] = Application["strserialNumber"];

        if (string.IsNullOrEmpty(div_error))
        {
            div_errordisp.InnerText = "";
        }
        else
        {
            div_errordisp.InnerText = div_error;
            return;
        }


        // For Ic Card loginIn In Simple EA version 1.2.2
        if (status.Value == "Login_Click")
        {
            Login_Click(sender, e);
            status.Value = "";
        }

        status.Value = "";

    }

    #region Start record IC card
    /// <summary>
    /// btnLogIn_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.15</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    protected void Login_Click(object sender, EventArgs e)
    {
        try
        {
            string iccardid = InputID.Text;
            InputID.Text = "";

            // edit by Weichangye 2012.03.20 
            //RegisterMessage(iccardid, type, userID);
            RegisterCard(iccardid, type, userID);

            //iccard_Login(iccardid, type);
            if (!string.IsNullOrEmpty(div_error))
            {
                div_errordisp.InnerText = div_error;
            }
        }
        catch
        {

            div_errordisp.InnerText = "System Error! Please contect to Administrator.";
        }
        // 2010.12.09 Update By SES Jijianxiog ED
    }
    #endregion

    #region "Error Display"
    /// <summary>
    /// Error Display
    /// </summary>
    /// <Date>2010.09.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public class ErrorDisp
    {
        TextBox span;

        public ErrorDisp(TextBox _span)
        {
            span = _span;
        }

        public string InnerText
        {
            set
            {
                span.Text = value.Trim();
            }
        }
    }

    #endregion

    /// <summary>
    /// back to previous
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnBack_Click(object sender, EventArgs e)
    {
        if (OSAType.Value != "")
        {
            if (OSAType.Value.ToString().Equals(E_EA_OSA_TYPE.OSAMAIN.ToString()))
                Response.Redirect("UserPrintDetails.aspx", false);
            else
                if (OSAType.Value.ToString().Equals(E_EA_OSA_TYPE.OSA40.ToString()))
                    Response.Redirect("UserPrintDetailsFor4.aspx", false);
        }
    }

    public void SetTdVisible()
    {
        leftTd.Visible = false;
        rigthTd.Visible = false;
        mainTable.Border = 0;
    }

    #region "RegisterCard"
    /// <summary>
    /// RegisterCard . add by Weichangye 2012.03.26
    /// save to DB and update XML
    /// </summary>
    /// <param name="cardId"></param>
    /// <param name="type"></param>
    public void RegisterCard(string cardId, E_EA_OSA_TYPE type, string userID)
    {
        dtUserInfoTableAdapters.UserInfoTableAdapter adpt = new dtUserInfoTableAdapters.UserInfoTableAdapter();
        dtUserInfo.UserInfoDataTable table = adpt.GetDataByUserId(Convert.ToInt32(userID));

        object count = adpt.AuthenticationMFP(table[0].LoginName, table[0].Password);

        RegisterICcard(table[0].LoginName, table[0].Password, cardId);

        Response.Redirect(string.Format("RegisterCardSuccess.aspx?type={0}&iccard={1}", OSAType.Value.ToString(), cardId), false);
    }
    #endregion

    /// <summary>
    /// DB & XML handle
    /// </summary>
    /// <param name="loginName"></param>
    /// <param name="pwd"></param>
    /// <param name="cardID"></param>
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

    #region "IC card Register Message."
    /// <summary>
    /// IC card Register Message. add by Weichangye 2011.12.28
    /// </summary>
    /// <param name="cardId"></param>
    /// <param name="type"></param>
    public void RegisterMessage(string cardId, E_EA_OSA_TYPE type, string userID)
    {
        string strScript = "";
        strScript =
            "    <script language='javascript' type='text/javascript'>" + "\r\n" +
            "        function register() {" + "\r\n" +
            "            if(confirm('需要对编号:" + cardId + "的IC卡进行注册吗？')) {window.parent.location.href='" + GetRegisterUrl(cardId, type, userID) + "'\r\n" +
            "        }}" + "\r\n" +
            "        // Registermsg." + "\r\n" +
            "        if ( window.attachEvent)  window.attachEvent('onload',function() { register();});" + "\r\n" +
            "        else window.addEventListener('load',function() { register();},true);" + "\r\n" +
            "    </script>" + "\r\n";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RegisterMessage", strScript);
    }
    #endregion

    #region Get IC card register url for diffirent OSA version
    /// <summary>
    /// Get IC card register url for diffirent OSA version.
    /// </summary>
    /// <param name="cardId"></param>
    /// <param name="type"></param>
    /// <Date>2011.12.28</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>1.2</Version>
    /// <returns></returns>
    private string GetRegisterUrl(string cardId, E_EA_OSA_TYPE type, string userID)
    {
        return string.Format("RegisterCardSuccess.aspx?id={0}&type={1}&userid={2}", cardId, type, userID);
    }
    #endregion
}