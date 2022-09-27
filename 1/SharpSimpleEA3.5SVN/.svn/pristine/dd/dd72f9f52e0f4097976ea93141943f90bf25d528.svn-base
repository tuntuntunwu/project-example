#region Copyright SHARP Corporation
//	Copyright (c) 2010 SHARP CORPORATION. All rights reserved.
//
//	SHARP Simple EA Application
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using dtSettingManagementTableAdapters;

/// <summary>
/// The BasePage For Simple EA Application MainPage
/// </summary>
/// <Date>2010.06.07</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public class MainPage : System.Web.UI.Page
{
    // JobList With All Job and Function Item.
    public List<JobTypeList> JobList;

    // JobList WithOut Other Item.
    public List<JobTypeList> JobListWithOutOther;

    #region "MainPage"
    public MainPage()
    {
        Page.PreInit +=new EventHandler(Page_PreInit);
        // 2010.11.24 Update By SES zhoumiao Ver.1.1 Update ST
        // Reprot Job Type List.
         JobList = new List<JobTypeList>();
         // Copy
         JobTypeList item = new JobTypeList(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1,
          UtilConst.ITEM_TITLE_Copy_FunctionId2, UtilConst.ITEM_TITLE_Copy);
         JobList.Add(item);
         // Print
         item = new JobTypeList(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1,
          UtilConst.ITEM_TITLE_Print_FunctionId2, UtilConst.ITEM_TITLE_Print);
         JobList.Add(item);
        //// Document Filing Print
        //item = new JobTypeList(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1,
        // UtilConst.ITEM_TITLE_DFPrint_FunctionId2, UtilConst.ITEM_TITLE_DFPrint);
        //JobList.Add(item);
        //// Scan Save
        //item = new JobTypeList(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1,
        // UtilConst.ITEM_TITLE_ScanSave_FunctionId2, UtilConst.ITEM_TITLE_ScanSave);
        //JobList.Add(item);
        //// List Print
        //item = new JobTypeList(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1,
        // UtilConst.ITEM_TITLE_ListPrint_FunctionId2, UtilConst.ITEM_TITLE_ListPrint);
        //JobList.Add(item);
        //// Scan
        //item = new JobTypeList(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1,
        // UtilConst.ITEM_TITLE_Scan_FunctionId2, UtilConst.ITEM_TITLE_Scan);
        //JobList.Add(item);
        //// Fax
        //item = new JobTypeList(UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1,
        // 99, UtilConst.ITEM_TITLE_Fax);
        //JobList.Add(item);
        //// Fax (Channel2)
        //item = new JobTypeList(UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1,
        // 99, UtilConst.ITEM_TITLE_FaxC2);
        //JobList.Add(item);
        //// Internet Fax
        //item = new JobTypeList(UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1,
        // 99, UtilConst.ITEM_TITLE_IntFax);
        //JobList.Add(item);
        //// Other
        //item = new JobTypeList(UtilConst.ITEM_TITLE_Other_JobId, UtilConst.ITEM_TITLE_Other_FunctionId1,
        // UtilConst.ITEM_TITLE_Other_FunctionId2, UtilConst.ITEM_TITLE_Other);
        //JobList.Add(item);

         // Scan
         item = new JobTypeList(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1,
          UtilConst.ITEM_TITLE_Scan_FunctionId2, UtilConst.ITEM_TITLE_Scan);
         JobList.Add(item);
         // Fax
         item = new JobTypeList(UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1,
          99, UtilConst.ITEM_TITLE_Fax);
         JobList.Add(item);
         // Document Filing Print
         item = new JobTypeList(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1,
          UtilConst.ITEM_TITLE_DFPrint_FunctionId2, UtilConst.ITEM_TITLE_DFPrint);
         JobList.Add(item);
         // Scan Save
         item = new JobTypeList(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1,
          UtilConst.ITEM_TITLE_ScanSave_FunctionId2, UtilConst.ITEM_TITLE_ScanSave);
         JobList.Add(item);
         // List Print
         item = new JobTypeList(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1,
          UtilConst.ITEM_TITLE_ListPrint_FunctionId2, UtilConst.ITEM_TITLE_ListPrint);
         JobList.Add(item);         
         // Fax (Channel2)
         item = new JobTypeList(UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1,
          99, UtilConst.ITEM_TITLE_FaxC2);
         JobList.Add(item);
         // Internet Fax
         item = new JobTypeList(UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1,
          99, UtilConst.ITEM_TITLE_IntFax);
         JobList.Add(item);
         // Other
         item = new JobTypeList(UtilConst.ITEM_TITLE_Other_JobId, UtilConst.ITEM_TITLE_Other_FunctionId1,
          UtilConst.ITEM_TITLE_Other_FunctionId2, UtilConst.ITEM_TITLE_Other);
         JobList.Add(item);

        // 2010.11.24 Update By SES zhoumiao Ver.1.1 Update ED


        // AvailableReportResult's Job List
        // With Out Other Item.
        JobListWithOutOther = new List<JobTypeList>();
        // Copy
        item = new JobTypeList(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1,
         UtilConst.ITEM_TITLE_Copy_FunctionId2, UtilConst.ITEM_TITLE_Copy);
        JobListWithOutOther.Add(item);
        // Print
        item = new JobTypeList(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1,
         UtilConst.ITEM_TITLE_Print_FunctionId2, UtilConst.ITEM_TITLE_Print);
        JobListWithOutOther.Add(item);
        // Document Filing Print
        item = new JobTypeList(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1,
         UtilConst.ITEM_TITLE_DFPrint_FunctionId2, UtilConst.ITEM_TITLE_DFPrint);
        JobListWithOutOther.Add(item);
        // Scan Save
        item = new JobTypeList(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1,
         UtilConst.ITEM_TITLE_ScanSave_FunctionId2, UtilConst.ITEM_TITLE_ScanSave);
        JobListWithOutOther.Add(item);
        // List Print
        item = new JobTypeList(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1,
         UtilConst.ITEM_TITLE_ListPrint_FunctionId2, UtilConst.ITEM_TITLE_ListPrint);
        JobListWithOutOther.Add(item);
        // Scan
        item = new JobTypeList(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1,
         UtilConst.ITEM_TITLE_Scan_FunctionId2, UtilConst.ITEM_TITLE_Scan);
        JobListWithOutOther.Add(item);
        // Fax
        item = new JobTypeList(UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1,
         99, UtilConst.ITEM_TITLE_Fax);
        JobListWithOutOther.Add(item);
        // Fax (Channel2)
        item = new JobTypeList(UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1,
         99, UtilConst.ITEM_TITLE_FaxC2);
        JobListWithOutOther.Add(item);
        // Internet Fax
        item = new JobTypeList(UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1,
         99, UtilConst.ITEM_TITLE_IntFax);
        JobListWithOutOther.Add(item);
    }
    #endregion

    #region"Delete the Reprot Job Type List"
    /// <summary>
    /// Delete the Reprot Job Type List
    /// </summary>
    /// <param name="ListName"></param>
    /// <Date>2010.11.24</Date>
    /// <Author>SES zhou miao</Author>
    /// <Version>1.10</Version>
    public void Delete_JobList(String ListName)
    {
        int fond_index = -1;
        foreach (JobTypeList item in JobList)
        {
            if (ListName.Equals(item.JobName))
            {
                fond_index = JobList.IndexOf(item);
                break;
            }
        }
        if (fond_index >= 0)
        {
            JobList.RemoveAt(fond_index);
        }

    }
    #endregion

    #region "ConnectionStrings For SimpleEA"
    /// <summary>
    /// ConnectionStrings For SimpleEA
    /// </summary>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public String DBConnectionStrings
    {
        get { return UtilCommon.DBConnectionStrings; }
    }

    #endregion

    #region "Page_PreInit"
    /// <summary>
    /// Page_PreInit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
        Response.AddHeader("pragma", "no-cache");
        Response.CacheControl = "no-cache";

        //2011.03.21 Delete By SES Jijianxiong ST
        // Bug Management Sheet_SimpleEA_110321.xls No.23
        //Helper.ConfigFileInfo infolice = new Helper.ConfigFileInfo();
        //string filelice = infolice.ExistsLicenseFile();
        //if (string.IsNullOrEmpty(filelice))
        //{
        //    Page.Response.Redirect("../LicenseErrorPage.htm");
        //    return;
        //}
        //2011.03.21 Delete By SES Jijianxiong ED

        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Page.Response.Redirect("~/Login/Login.aspx");
        }
        else
        {
            FormsAuthenticationTicket authenticationTicket;

            authenticationTicket = new FormsAuthenticationTicket(1, HttpContext.Current.User.Identity.Name, DateTime.Now, DateTime.Now.AddMinutes(UtilConst.CON_FORM_TIMEOUT), false, HttpContext.Current.User.Identity.Name);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(authenticationTicket);

            // Create the cookie.
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }
    }
    #endregion

    #region "Success message."
    /// <summary>
    /// Success message.
    /// </summary>
    /// <param name="msg"></param>
    /// <Date>2010.08.20</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void SuccessMessage(string msg)
    {
        string strScript = "";
        strScript = 
            "    <script language='javascript' type='text/javascript'>" + "\r\n" +
            "        function success() {" + "\r\n" +
            "            alert('" + msg + "','操作已成功!');" + "\r\n" +
            "        }" + "\r\n" + 
            "        // Successmsg." + "\r\n" + 
            "        if ( window.attachEvent)  window.attachEvent('onload',function() { success();});" + "\r\n" +
            "        else window.addEventListener('load',function() { success();},true);" + "\r\n" + 
            "    </script>" + "\r\n";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SuccessMessage", strScript);
    }
    #endregion

    #region "Warninng message."
    /// <summary>
    /// Warninng message.
    /// </summary>
    /// <param name="msg"></param>
    /// <Date>2010.12.17</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    public void WarninngMessage(string msg)
    {
        string strScript = "";
        strScript =
            "    <script language='javascript' type='text/javascript'>" + "\r\n" +
            "        function success() {" + "\r\n" +
            "            alert('" + msg + "','提示信息!');" + "\r\n" +
            "        }" + "\r\n" +
            "        // Successmsg." + "\r\n" +
            "        if ( window.attachEvent)  window.attachEvent('onload',function() { success();});" + "\r\n" +
            "        else window.addEventListener('load',function() { success();},true);" + "\r\n" +
            "    </script>" + "\r\n";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SuccessMessage", strScript);
    }
    #endregion

    #region "the confirmation dialog with msg Item."
    /// <summary>
    /// the confirmation dialog with msg Item.
    /// </summary>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void ScriptConfirm(string name, string strValidationGroup)
    {
        string strScript = "";
        strScript = "    <script language='javascript' type='text/javascript'>" + "\r\n";
        strScript += "    function " + name + "(msg) {" + "\r\n";        

        if (!"".Equals(strValidationGroup))
        {
            strScript += "        if (typeof(Page_ClientValidate) == 'function') {" + "\r\n";
            strScript += "            if ( !Page_ClientValidate('" + strValidationGroup + "') )" + "\r\n";
            strScript += "            { " + "\r\n";
            strScript += "                return false;" + "\r\n";
            strScript += "            }" + "\r\n";
            strScript += "        }" + "\r\n";
        }

        strScript += "        msg = msg.replace('<BR>','\\n')" + "\r\n";
        // Update By SES.JiJianXiong 2010.08.20 ST
        //strScript += "        if ( window.confirm(msg) ) {" + "\r\n";
        //strScript += "            return true;" + "\r\n";
        //strScript += "        } else {" + "\r\n";
        //strScript += "            if ( window.event ) {" + "\r\n";
        //strScript += "                window.event.returnValue = false;" + "\r\n";
        //strScript += "            }" + "\r\n";
        //strScript += "            return false;" + "\r\n";
        //strScript += "        }" + "\r\n";

        strScript += "        var fun =null;" + "\r\n";
        strScript += "        if ( window.event.srcElement ) {" + "\r\n";
        strScript += "            var buttonid = window.event.srcElement.name;" + "\r\n";

        strScript += "            fun = function (e) {" + "\r\n";
        strScript += "                __doPostBack(buttonid,'');" + "\r\n";
        strScript += "            };" + "\r\n";

        strScript += "        };" + "\r\n";
        //strScript += "        fun = function (e) {" + "\r\n";
        //strScript += "            window.document.forms[0].submit();" + "\r\n";
        //strScript += "        };" + "\r\n";
        strScript += "        window.confirm(msg , null, fun);" + "\r\n";
        strScript += "        if ( window.event ) {" + "\r\n";
        strScript += "            window.event.returnValue = false;" + "\r\n";
        strScript += "        }" + "\r\n";
        strScript += "        return false;" + "\r\n";
        // Update By SES.JiJianXiong 2010.08.20 ED
        strScript += "    }" + "\r\n";
        strScript += "    </script>" + "\r\n";

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), name, strScript);
    }
    #endregion

    #region "SQL CONVERT(STRING)"
    /// <summary>
    /// SQL CONVERT(STRING)
    /// </summary>
    /// <param name="strInput"></param>
    /// <returns></returns>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static string ConvertStringToSQL(string strInput)
    {
        return UtilCommon.ConvertStringToSQL(strInput);
    }
    #endregion
    //pupeng add 2014 4-14
    #region "SQL CONVERT(money)"
    /// <summary>
    /// SQL CONVERT(money)
    /// </summary>
    /// <param name="strInput"></param>
    /// <returns></returns>
    /// <Date>2014 4.14</Date>
    /// <Author>pupeng</Author>
    /// <Version>0.01</Version>
    public static string ConvertMoneyToSQL(string strInput)
    {
        return UtilCommon.decimalToMoney(strInput);
    }
  
    #endregion
    #region "SQL CONVERT(INT)"
    /// <summary>
    /// SQL CONVERT(INT)
    /// </summary>
    /// <param name="strInput"></param>
    /// <returns></returns>
    /// <Date>2010.06.14</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static string ConvertIntToSQL(string strInput)
    {
        return UtilCommon.ConvertIntToSQL(strInput);
    }
    #endregion

    #region "SQL CONVERT(DATETIME)"
    /// <summary>
    /// SQL CONVERT(STRING)
    /// </summary>
    /// <param name="strInput"></param>
    /// <returns></returns>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static string ConvertDateToSQL(DateTime  datInput)
    {
        return UtilCommon.ConvertDateToSQL(datInput);
    }
    #endregion


    #region "Server's Path" 
    /// <summary>
    /// Server's Path
    /// </summary>
    /// <Date>2010.06.23</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public string ServerPath
    {
        get
        {
            return Server.MapPath("~");
        }
    }
    #endregion


    #region "ExecuteReader"
    /// <summary>
    /// ExecuteReader
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    /// <Date>2010.06.24</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public SqlDataReader ExecuteReader(string sql){

        SqlConnection con = new SqlConnection(this.DBConnectionStrings);
        con.Open();

        try
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
        }
        catch (Exception)
        {
            con.Close();
            throw;
        }
    }
    #endregion

    #region "ExecuteReader"
    /// <summary>
    /// ExecuteReader
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    /// <Date>2010.06.24</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public int ExecuteScalar(string sql)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    int retVal = (int)cmd.ExecuteScalar();
                    return retVal;
                }
            }
        }
        catch (Exception)
        {
            return 0;
        }
    }

    #endregion
    
    #region "ExecuteDataTable"
    /// <summary>
    /// ExecuteDataTable
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    /// <Date>2010.06.28</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public DataTable ExecuteDataTable(string sql)
    {
        DataTable data = new DataTable();

        using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(data);
                return data;
            }
        }
    }
    #endregion

    #region "Job Type Class"
    /// <summary>
    /// Job Type Class
    /// </summary>
    /// <Date>2010.07.01</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public class JobTypeList
    {
        // Job Type's  Id.
        int intJobId;
        // Function Type's  Id(B/W).
        int intBWFunctionId;
        // Function Type's  Id(FullColor).
        int intFCFunctionId;
        // Job Type's Name.
        string strJobNm;

        /// <summary>
        /// JobTypeList
        /// </summary>
        /// <param name="intJobIdIn">Job Type's  Id</param>
        /// <param name="intBWFunctionIdIn">Function Type's  Id(B/W).</param>
        /// <param name="intFCFunctionIdIn">Function Type's  Id(FullColor)</param>
        /// <param name="strJobNmIn">Job Type's Name</param>
        /// <Date>2010.07.01</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public JobTypeList(int intJobIdIn, int intBWFunctionIdIn, int intFCFunctionIdIn, string strJobNmIn)
        {
            // Job Type's  Id.
            intJobId = intJobIdIn;
            // Function Type's  Id(B/W).
            intBWFunctionId = intBWFunctionIdIn;
            // Function Type's  Id(FullColor).
            intFCFunctionId = intFCFunctionIdIn;
            // Job Type's Name.
            strJobNm = strJobNmIn;
        }

        /// <summary>
        /// Job Type's  Id
        /// </summary>
        /// <Date>2010.07.01</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public int JobId
        {
            get
            {
                return intJobId;
            }
        }

        /// <summary>
        /// Function Type's  Id(B/W).
        /// </summary>
        /// <Date>2010.07.01</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public int BWFunctionId
        {
            get
            {
                return intBWFunctionId;
            }
        }

        /// <summary>
        /// Function Type's  Id(Full Color).
        /// </summary>
        /// <Date>2010.07.01</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public int FCFunctionId
        {
            get
            {
                return intFCFunctionId;
            }
        }

        /// <summary>
        /// JobName
        /// </summary>
        /// <Date>2010.07.01</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public String JobName
        {
            get
            {
                return strJobNm;
            }
        }
    }
    #endregion


    #region "ErrorLog" 
    /// <summary>
    /// UtilCommon.IntToMoney
    /// </summary>
    /// <param name="intput"></param>
    /// <returns></returns>
    /// <Date>2010.07.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void ErrorLog(Exception ex)
    {
        string strError = "Error Caught in Application_Error event\n" +
                "Error in:" + Request.Url.ToString() +
                "\nError Message:" + ex.Message.ToString() +
                "\nStach Trace:" + ex.StackTrace.ToString();
        if (User.Identity.Name != "")
        {
            strError = strError + "\nLogin Name:" + User.Identity.Name;
        }
        System.Diagnostics.EventLog.WriteEntry("Simple EA System", strError);

    }
    #endregion

    #region "ErrorAlert"
    /// <summary>
    /// ErrorAlert
    /// </summary>
    /// <param name="ex"></param>
    /// <Date>2010.07.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void ErrorAlert(Exception ex)
    {
        // Log
        ErrorLog(ex);

        // Message
        string strScript = "error_alert('{0}');";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorMsg", string.Format(strScript, UtilConst.MSG_ERROR) , true);
    }

    #endregion

    #region "ErrorAlert"
    /// <summary>
    /// ErrorAlert
    /// </summary>
    /// <param name="ex"></param>
    /// <Date>2010.07.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void ErrorAlert(string message)
    {

        // Message
        string strScript = "error_alert('{0}');";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorMsg", string.Format(strScript, message), true);
    }

    #endregion

    #region "User Check"
    /// <summary>
    /// User Check
    /// </summary>
    /// <Date>2010.07.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void CheckUser()
    {
        if (!(HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME) 
            || HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_SECUADMIN))
            )
        {
            FormsAuthentication.SignOut();

            Page.Response.Redirect("../Login/Login.aspx", true);

            return;
        }

    }
    #endregion

    
    #region "User Check In UserEdit Screen."
    /// <summary>
    /// User Check
    /// </summary>
    /// <Date>2010.07.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void CheckUserInUserEdit(String UserID)
    {
        if (!HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            // Check UserID.
            dtUserInfoTableAdapters.UserInfoTableAdapter adapter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
            dtUserInfo.UserInfoDataTable dt = adapter.GetDataByLoginName(HttpContext.Current.User.Identity.Name);
            if (UserID.Equals(((dtUserInfo.UserInfoRow)dt.Rows[0]).ID))
            {
                FormsAuthentication.SignOut();

                Page.Response.Redirect("../Login/Login.aspx", true);

                return;
            }
            return;
        }

    }
    #endregion


    #region "Show Error Message."
    /// <summary>
    /// Show Error Message. (Display with new UI)
    /// </summary>
    /// <param name="jsfun"></param>
    /// <Date>2010.08.18</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void showError(string jsfun)
    {
        string strScript = "";
        strScript = "    <script language='javascript' type='text/javascript'>" + "\r\n";
        if (jsfun.IndexOf("(") > 0)
        {
            strScript += "     " + jsfun + ";" + "\r\n";
        }
        else
        {
            strScript += "     " + jsfun + "(); " + "\r\n";
        }
        strScript += "    </script>" + "\r\n";

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "showError", strScript);
    }
    #endregion

}



