using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml;
using System.ServiceProcess;
using System.Net;
using dtMFPInformationTableAdapters;
using BLL;
using Model;
using common;
using System.IO;



public partial class Settings_ContentBackupSetting : MainPage
{
    private string CON_JSCRIPT_FUN_CANCEL = "ScriptConfirmCancel";
    //protected FollowMEHandler handler;

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.01</Date>
    /// <Author>SLC  Wei Changye</Author>
    /// <Version>1.2</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.Master.Title = UtilConst.CON_PAGE_FOLLOWME_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_FOLLOWME_SET, "ContentBackupSetting.aspx", true);
        this.Master.JobReportTitle();

        btnDeleteCopy.OnClientClick = ConfirmFunctionCancel(UtilConst.MSG_COPY_DELETE);

 
        //Set Init value        

        
        

        // Check Access Role
        CheckUser();

        if (!IsPostBack)
        {
            InitialPage();
        }
    }
    #endregion

    #region "InitialPage"
    /// <summary>
    /// InitialPage
    /// </summary>
    /// <Date>2012.03.01</Date>
    /// <Author>SLC  Wei Changye</Author>
    /// <Version>1.2</Version>
    private void InitialPage()
    {
        BllMFP mfpBll = new BllMFP();
        Model.CopyConfigEntry model = new Model.CopyConfigEntry();
        DAL.DalCopyConfig dalcopyConfig = new DAL.DalCopyConfig();
        model = dalcopyConfig.GetCopyConfigInfo();
        string CopyFileLocation = "";
        if (model.CopyFileLocation != null)
        {
            CopyFileLocation = model.CopyFileLocation;
        }

        //confirmbox
        
        txtFileLocation.Text = CopyFileLocation.Trim();

        //Time init

        //Start&End Year
        for (int year = UtilConst.CON_START_YEAR; year <= DateTime.Now.Year; year++)
        {
            ListItem itemBegin = new ListItem();
            itemBegin.Value = year.ToString();
            itemBegin.Text = year.ToString();
            ddlBeginYear.Items.Add(itemBegin);

            ListItem itemEnd = new ListItem();
            itemEnd.Value = year.ToString();
            itemEnd.Text = year.ToString();
            ddlEndYear.Items.Add(itemEnd);
        }

        //Start&End  Month
        for (int mouth = 1; mouth <= 12; mouth++)
        {
            ListItem itemBegin = new ListItem();
            itemBegin.Value = mouth.ToString();
            itemBegin.Text = mouth.ToString();
            ddlBeginMonth.Items.Add(itemBegin);

            ListItem itemEnd = new ListItem();
            itemEnd.Value = mouth.ToString();
            itemEnd.Text = mouth.ToString();
            ddlEndMonth.Items.Add(itemEnd);
        }

        //Start&End  Day
        for (int day = 1; day <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); day++)
        {
            ListItem itemBegin = new ListItem();
            itemBegin.Value = day.ToString();
            itemBegin.Text = day.ToString();
            ddlBeginDay.Items.Add(itemBegin);

            ListItem itemEnd = new ListItem();
            itemEnd.Value = day.ToString();
            itemEnd.Text = day.ToString();
            ddlEndDay.Items.Add(itemEnd);
        }

        //Start&End  Hour
        for (int Hour = 0; Hour <= 23; Hour++)
        {
            ListItem itemBegin = new ListItem();
            itemBegin.Value = Hour.ToString();
            itemBegin.Text = Hour.ToString();
            ddlBeginHour.Items.Add(itemBegin);

            ListItem itemEnd = new ListItem();
            itemEnd.Value = Hour.ToString();
            itemEnd.Text = Hour.ToString();
            ddlEndHour.Items.Add(itemEnd);
        }
        SetPeriod(DateTime.Now);
        //txtSPLPeriod.Text = GetFollowME("SplPeriod");
       // txtUserOper.Text = GetFollowME("OpPeriod");
        string flag = GetFollowME("RawRecvLog");

        //if (flag == "true")
        //   //ddlDebug.SelectedIndex = ddlDebug.Items.IndexOf(ddlDebug.Items.FindByValue("1"));
        //else
        //    //ddlDebug.SelectedIndex = ddlDebug.Items.IndexOf(ddlDebug.Items.FindByValue("0"));
    }
    #endregion

    #region "btnReset_Click"
    /// <summary>
    /// btnReset_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.01</Date>
    /// <Author>SLC  Wei Changye</Author>
    /// <Version>1.2</Version>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        InitialPage();
    }
    #endregion

    #region "btnApply_Click"
    /// <summary>
    /// btnApply_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.01</Date>
    /// <Author>SLC  Wei Changye</Author>
    /// <Version>1.2</Version>
    protected void btnApply_Click(object sender, EventArgs e)
    {
        //Modify("FileLocation", txtFileLocation.Text.Trim());
        //Modify("SplPeriod", txtSPLPeriod.Text.Trim());
        //Modify("OpPeriod", txtSPLPeriod.Text.Trim());
        //Modify("RawRecvPort", txtPort.Text.Trim());

        //if (ddlDebug.SelectedIndex == 0)
        //{
        //    Modify("AppLog", "false");
        //    Modify("HttpLog", "false");
        //    Modify("RawRecvLog", "false");
        //}
        //else
        //{
        //    Modify("AppLog", "true");
        //    Modify("HttpLog", "true");
        //    Modify("RawRecvLog", "true");
        //}
        
        //ModifyFollowME("FileLocation", txtFileLocation.Text.Trim());
        //ModifyFollowME("SplPeriod", txtSPLPeriod.Text.Trim());
       // ModifyFollowME("OpPeriod", txtUserOper.Text.Trim());

        //if (txtPort.Text.Trim() != GetFollowME("RawRecvPort"))
        //{
        //    ModifyFollowME("RawRecvPort", txtPort.Text.Trim());
        //    btnRestartIIS_Click(new object(), new EventArgs());
        //}

      

    }
    #endregion

    #region "btnModify_Click"
    /// <summary>
    /// btnModify onclick
    /// 文件存放地址修改
    /// </summary>
    /// <Date>2017.06.08</Date>
    /// <Author>CC TuJinyu</Author>
    /// <Version>1.0</Version>
    protected void btnModify_Click(object sender, EventArgs e)
    {
        String copyfilelocation = txtFileLocation.Text;
        BllCopyConfig copyBll = new BllCopyConfig();
        Model.CopyConfigEntry model = new Model.CopyConfigEntry();
        model.CopyFileLocation= copyfilelocation.Trim();
        int result = copyBll.Update(model);      
               

    }
    #endregion


    #region "btn_DeleteCopy"
    /// <summary>
    /// 根据日期删除留底
    /// btn_DeleteCopy onclick
    /// </summary>
    /// <Date>2017.06.08</Date>
    /// <Author>CC TuJinyu</Author>
    /// <Version>1.0</Version>
    protected void btn_DeleteCopy(object sender, EventArgs e)
    {
        //if (handler == null)
        //{
        //    handler = new FollowMEHandler();
        //}

        String startTime_str = StartDate.ToString("yyyyMMddHHmmss");
        String endTime_str = EndDate.ToString("yyyyMMddHHmmss");
        //Console.WriteLine("starttime:" + startTime_str);
        //Console.WriteLine("endtime:" + endTime_str);
        //删除DB中的留底文件记录
        BllMFP mfpBll = new BllMFP();
        Model.CopyConfigEntry model = new Model.CopyConfigEntry();
        DAL.DalPrintCopy dalprintcopy = new DAL.DalPrintCopy();
        //先删除文件
        List<PrintCopyModel> DeletedRecords = dalprintcopy.SelectBetweenTime(StartDate, EndDate);
     
        List<string> pdflist = new List<string>();
        String copyfilelocation = txtFileLocation.Text;
        try
        {
            foreach (PrintCopyModel pcm in DeletedRecords)
            {
                string location = copyfilelocation + pcm.CopyFile + ".pdf";//默认无拓展名+pdf
                if (System.IO.File.Exists(location))
                {
                    //System.IO.File.Delete(location);
                    //pdflist.Add(location);
                    //判断当前文件属性是否是只读
                    FileInfo fi = new FileInfo(location);
                    string strreadonly = fi.Attributes.ToString();
                    if (strreadonly.IndexOf("ReadOnly") >= 0)
                    {
                        fi.Attributes = FileAttributes.Normal;
                    }
                    System.IO.File.Delete(location);
                }
            }
        }
        catch (Exception ex)
        {
            ;
        }

        //IPHostEntry ipHostInfo = Dns.Resolve(Request.Url.Host);
        //IPAddress ipAddress = ipHostInfo.AddressList[0];
        //if (ipAddress.ToString().Equals("127.0.0.1"))
        //{
        //    ipHostInfo = Dns.Resolve(Dns.GetHostName());
        //    ipAddress = ipHostInfo.AddressList[0];
        //}
        //handler.DeleteCopyPdf(pdflist, ipAddress.ToString());

        //再删除数据库中的记录
        int result = dalprintcopy.Delete(StartDate, EndDate);
    }
    #endregion
    

    #region "ModifyFollowME"
    ///<summary>
    /// 修改web.config文件appSettings配置节中的Add里的value属性
    ///</summary>
    ///<remarks>
    /// 注意，调用该函数后，会使整个WebApplication重启，导致当前所有的会话丢失
    ///</remarks>
    ///<paramname="key">要修改的键key</param>
    ///<paramname="strValue">修改后的value</param>
    ///<exceptioncref="">找不到相关的键</exception>
    ///<exceptioncref="">权限不够，无法保存到web.config文件中</exception>
    public void ModifyFollowME(string key, string strValue)
    {
        try
        {
            UtilCommon.ModifyFollowME(key, strValue);
        }
        catch (Exception ex)
        {
            ErrorAlert("WebConfig 中的配置出现错误，请及时联系管理员！");
        }
    }
    #endregion

    #region "Get Follow ME param"
    /// <summary>
    /// Get Follow ME param from config file
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetFollowME(string key)
    {
        try
        {
            return UtilCommon.GetFollowME(key);
        }
        catch (Exception ex)
        {
            ErrorAlert("WebConfig 中的配置出现错误，请及时联系管理员！");
            return "";
        }
    }
    #endregion

    #region "Calenda"
    protected void SetPeriod(DateTime PeriodTime)
    {

        // Start Period.
        DateTime StartPeriod;
        // End Period.
        DateTime EndPeriod;
        // Get Period Time By Now.
        //UtilCommon.GetPeriodBy(PeriodTime, out StartPeriod, out EndPeriod);
        UtilCommon.GetSTartEndTimeBy(PeriodTime, out StartPeriod, out EndPeriod);

        //Get Min End Year In ddlEndYear
        int intMinBeginYear = int.Parse(ddlBeginYear.Items[0].Value);
        int intMinEndYear = int.Parse(ddlEndYear.Items[0].Value);
        // Get Min Year Between StartPeriod and EndPeriod.
        int intMinYear = StartPeriod.Year;
        if (intMinYear > EndPeriod.Year)
        {
            intMinYear = EndPeriod.Year;
        }

        // Add Year In Min list( Begin Year)
        if (intMinYear < intMinEndYear || intMinYear < intMinBeginYear)
        {
            ListItem itemStart = new ListItem();
            itemStart.Value = intMinYear.ToString();
            itemStart.Text = intMinYear.ToString();
            ddlBeginYear.Items.Insert(0, itemStart);

        }

        // Add Year In Min list( End Year)
        if (intMinYear < intMinEndYear || intMinYear < intMinBeginYear)
        {
            ListItem itemEnd = new ListItem();
            itemEnd.Value = intMinYear.ToString();
            itemEnd.Text = intMinYear.ToString();
            ddlEndYear.Items.Insert(0, itemEnd);
        }

        //Get Max End Year In ddlEndYear
        int intMaxBeginYear = int.Parse(ddlBeginYear.Items[ddlBeginYear.Items.Count - 1].Value);
        int intMaxEndYear = int.Parse(ddlEndYear.Items[ddlEndYear.Items.Count - 1].Value);
        // Get Max Year Between StartPeriod and EndPeriod.
        int intMaxYear = StartPeriod.Year;
        if (intMaxYear < EndPeriod.Year)
        {
            intMaxYear = EndPeriod.Year;
        }

        // Add Year In Max list( Begin Year)
        if (intMaxYear > intMaxEndYear || intMaxYear > intMaxBeginYear)
        {
            ListItem itemStart = new ListItem();
            itemStart.Value = intMaxYear.ToString();
            itemStart.Text = intMaxYear.ToString();
            ddlBeginYear.Items.Add(itemStart);

        }

        // Add Year In Max list( End Year)
        if (intMaxYear > intMaxEndYear || intMaxYear > intMaxBeginYear)
        {
            ListItem itemEnd = new ListItem();
            itemEnd.Value = intMaxYear.ToString();
            itemEnd.Text = intMaxYear.ToString();
            ddlEndYear.Items.Add(itemEnd);

        }



        // Year
        ddlBeginYear.SelectedValue = StartPeriod.Year.ToString();
        ddlEndYear.SelectedValue = EndPeriod.Year.ToString();
        List_reLoad();
        // Month
        ddlBeginMonth.SelectedValue = StartPeriod.Month.ToString();
        ddlEndMonth.SelectedValue = EndPeriod.Month.ToString();
        List_reLoad();
        // Day
        ddlBeginDay.SelectedValue = StartPeriod.Day.ToString();
        ddlEndDay.SelectedValue = EndPeriod.Day.ToString();
        // Hour
        ddlBeginHour.SelectedValue = StartPeriod.Hour.ToString();
        ddlEndHour.SelectedValue = EndPeriod.Hour.ToString();

        StartDateTime.Text = StartDate.ToString("yyyyMMddHHmmss");
        EndDateTime.Text = EndDate.ToString("yyyyMMddHHmmss");

    }

    public DateTime StartDate
    {

        get
        {
            return new DateTime(int.Parse(ddlBeginYear.SelectedValue),
                int.Parse(ddlBeginMonth.SelectedValue),
                int.Parse(ddlBeginDay.SelectedValue),
                int.Parse(ddlBeginHour.SelectedValue), 0, 0);
        }
    }

    public DateTime EndDate
    {
        get
        {
            return new DateTime(int.Parse(ddlEndYear.SelectedValue),
                int.Parse(ddlEndMonth.SelectedValue),
                int.Parse(ddlEndDay.SelectedValue),
                int.Parse(ddlEndHour.SelectedValue), 59, 59);
        }
    }

    protected void List_reLoad()
    {

        // When Year Or Month list Changes's ,the Day Count of Month(February) will be changed.
        // So need to reLoad Day List.
        // Start Date
        if (CheckNeedReloadStartDate)
        {
            reLoadDayList(ddlBeginDay, ddlBeginYear.SelectedValue, ddlBeginMonth.SelectedValue);
        }

        // End Date
        if (CheckNeedReloadEndDate)
        {
            reLoadDayList(ddlEndDay, ddlEndYear.SelectedValue, ddlEndMonth.SelectedValue);
        }

        StartDateTime.Text = StartDate.ToString("yyyyMMddHHmmss");
        EndDateTime.Text = EndDate.ToString("yyyyMMddHHmmss");
    }
    protected void List_reLoad(bool flag)
    {
        if (flag == true)
        {
            reLoadDayList(ddlBeginDay, ddlBeginYear.SelectedValue, ddlBeginMonth.SelectedValue);
            reLoadDayList(ddlEndDay, ddlEndYear.SelectedValue, ddlEndMonth.SelectedValue);
            StartDateTime.Text = StartDate.ToString("yyyyMMddHHmmss");
            EndDateTime.Text = EndDate.ToString("yyyyMMddHHmmss");
        }
    }



    protected Boolean CheckNeedReloadStartDate
    {
        get
        {
            if (ddlBeginDay.Items[ddlBeginDay.Items.Count - 1].Value.Equals(
                    DateTime.DaysInMonth(int.Parse(ddlBeginYear.SelectedValue), int.Parse(ddlBeginMonth.SelectedValue)).ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    protected Boolean CheckNeedReloadEndDate
    {
        get
        {
            if (ddlEndDay.Items[ddlEndDay.Items.Count - 1].Value.Equals(
                    DateTime.DaysInMonth(int.Parse(ddlEndYear.SelectedValue), int.Parse(ddlEndMonth.SelectedValue)).ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    protected void reLoadDayList(DropDownList list, string strYear, string strMonth)
    {
        //Save Now SelectIndex
        int intSelectIndex = list.SelectedIndex;
        // Clear DropDownList
        list.ClearSelection();
        list.Items.Clear();

        ListItem item;
        // reLoad DropDownList
        for (int day = 1; day <= DateTime.DaysInMonth(int.Parse(strYear), int.Parse(strMonth)); day++)
        {
            item = new ListItem();
            item.Value = day.ToString();
            item.Text = day.ToString();
            list.Items.Add(item);
        }

        // Reservation SelectIndex.
        if (list.Items.Count > intSelectIndex)
        {
            list.SelectedIndex = intSelectIndex;
        }
        else
        {
            list.SelectedIndex = list.Items.Count - 1;
        }
        return;

    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
        //Btnupdate.Visible = true;
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED
        List_reLoad();
    }
    protected void btnPeriodPre_Click(object sender, EventArgs e)
    {

        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
       // Btnupdate.Visible = true;
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED
        SetPeriod(StartDate.AddSeconds(-1));
    }
    protected void btnPeriodNext_Click(object sender, EventArgs e)
    {
      
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
       // Btnupdate.Visible = true;
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED
        SetPeriod(EndDate.AddSeconds(2));
    }

    #endregion


    #region "message for page.(Cancel Button)"
    /// <summary>
    /// message for page.(Cancel Button)
    /// </summary>
    /// <seealso cref="ScriptConfirmListCancel"/>
    /// <param name="msg"></param>
    /// <returns>OnClientClick Function</returns>
    /// <seealso cref="ScriptConfirmList"/>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected string ConfirmFunctionCancel(string msg)
    {
        string strMsg;
        strMsg = CON_JSCRIPT_FUN_CANCEL + "('{0}')";
        strMsg = string.Format(strMsg, msg);
        ScriptConfirmListCancel();
        return strMsg;
    }
    private void ScriptConfirmListCancel()
    {
        ScriptConfirm(CON_JSCRIPT_FUN_CANCEL, "");
    }


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


}