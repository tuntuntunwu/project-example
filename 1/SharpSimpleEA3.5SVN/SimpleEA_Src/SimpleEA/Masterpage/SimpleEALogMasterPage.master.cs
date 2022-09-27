using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using dtMFPInformationTableAdapters;

public partial class Masterpage_SimpleEALogMasterPage : System.Web.UI.MasterPage
{
    //update Button
    public Button Btn_Search()
    {
        return this.BtnSearch;
    }     

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Defulat for Group Item Row is disVisible
        if (!IsPostBack)
        {
            Load_Once();
        }
    }
    #endregion

    #region "Title"
    /// <summary>
    /// Title
    /// </summary>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    public string Title
    {
        set
        {           
          this.Master.Title = UtilConst.CON_PAGE_LOG;
        }
    }
    #endregion

    #region "btnPeriodPre_Click"
    /// <summary>
    /// btnPeriodPre_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.23</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnPeriodPre_Click(object sender, EventArgs e)
    {
        SetPeriod(StartDate.AddSeconds(-1));
    }
    #endregion

    #region "btnPeriodNext_Click"
    /// <summary>
    /// btnPeriodNext_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.23</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnPeriodNext_Click(object sender, EventArgs e)
    {
        SetPeriod(EndDate.AddSeconds(2));
    }
    #endregion

    #region "Load_Once"
    /// <summary>
    /// Load_Once
    /// Is't Post Back
    /// </summary>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Load_Once()
    {
        List_Init();
        valDateTime.ErrorMessage = UtilConst.MSG_DATETIME;

    }
    #endregion

    #region "List's Initial"
    /// <summary>
    /// List's Initial
    /// </summary>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void List_Init()
    {       
        //// Total Target Item
        //MFP Model Name (Serial Number)
        MFPInformationListTableAdapter MFPAdapter = new MFPInformationListTableAdapter();
        ddlMFPItem.DataSource = MFPAdapter.GetData();
        ddlMFPItem.DataBind();
        ddlMFPItem.Items.Insert(0, "");
        ListItem item = new ListItem();
        // Status      
        item.Value = UtilConst.JOB_STATUS_SUCCESSVALUE;
        item.Text = UtilConst.JOB_STATUS_SUCCESS;
        ddlStatus.Items.Add(item);
        item = new ListItem();
        item.Value = UtilConst.JOB_STATUS_ERRORVALUE;
        item.Text = UtilConst.JOB_STATUS_ERR;
        ddlStatus.Items.Add(item);
        item = new ListItem();
        item.Value = UtilConst.JOB_STATUS_ALLVALUE;
        item.Text = UtilConst.JOB_STATUS_ALL;
        ddlStatus.Items.Add(item);
        //Job Type
        item = new ListItem();
        item.Value = UtilConst.JOB_JOBTYPE_ALLVALUE;
        item.Text = UtilConst.JOB_JOBTYPE_ALL;
        ddlJobtype.Items.Add(item);
        item = new ListItem();
        item.Value = UtilConst.JOB_JOBTYPE_COPYVALUE;
        item.Text = UtilConst.JOB_JOBTYPE_COPY;
        ddlJobtype.Items.Add(item);
        item = new ListItem();
        item.Value = UtilConst.JOB_JOBTYPE_PRINTVALUE;
        item.Text = UtilConst.JOB_JOBTYPE_PRINT;
        ddlJobtype.Items.Add(item);
        item = new ListItem();
        item.Value = UtilConst.JOB_JOBTYPE_SCANVALUE;
        item.Text = UtilConst.JOB_JOBTYPE_SCAN;
        ddlJobtype.Items.Add(item);
        item = new ListItem();
        item.Value = UtilConst.JOB_JOBTYPE_FAXVALUE;
        item.Text = UtilConst.JOB_JOBTYPE_FAX;
        ddlJobtype.Items.Add(item);

        // Start Year
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

        // Start Month
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

        // Month
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

        // Hour
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

        //Set Init value 
        SetPeriod(DateTime.Now);
    }
    #endregion

    #region "List's SelectedIndexChanged Event"
    /// <summary>
    /// List's SelectedIndexChanged Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {      
        List_reLoad();
    }
    #endregion

    #region "List's Reload"
    /// <summary>
    /// List's Reload
    /// </summary>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
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
    #endregion

    #region "Start Date( YYYY / MM / DD )"
    /// <summary>
    /// Start Date( YYYY / MM / DD )
    /// </summary>
    /// <returns>Start Date( YYYY / MM / DD )</returns>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected string StartDateYYYYMMDD()
    {
        return ddlBeginYear.SelectedValue + "/" + ddlBeginMonth.SelectedValue + "/" + ddlBeginDay.SelectedValue;
    }

    #endregion

    #region "Start Date"
    /// <summary>
    /// Start Date
    /// </summary>
    /// <returns>Start Date</returns>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
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
    #endregion

    #region "Check is it need to reload.(Start Date)"
    /// <summary>
    /// Check is it need to reload.(Start Date)
    /// </summary>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
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
    #endregion

    #region "Check is it need to reload.(End Date) ."
    /// <summary>
    /// Check is it need to reload.(End Date) 
    /// </summary>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
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
    #endregion

    #region "End Date( YYYY / MM / DD )"
    /// <summary>
    /// End Date( YYYY / MM / DD )
    /// </summary>
    /// <returns>End Date( YYYY / MM / DD )</returns>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected string EndDateYYYYMMDD()
    {
        return ddlEndYear.SelectedValue + "/" + ddlEndMonth.SelectedValue + "/" + ddlEndDay.SelectedValue;
    }

    #endregion

    #region "End Date"
    /// <summary>
    /// End Date
    /// </summary>
    /// <returns>Start Date</returns>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
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
    #endregion

    #region "Re Load DropDownList of Day."
    /// <summary>
    /// Re Load DropDownList of Day.
    /// </summary>
    /// <param name="list">the Re Load List.</param>
    /// <param name="strYear">Year</param>
    /// <param name="strMonth">Month</param>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
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
    #endregion

    #region "User/Login Textbox"
    /// <summary>
    /// User/Login Textbox
    /// </summary>
    /// <Date>2010.12.15</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.1</Version>
    public string UserLogin
    {
        get
        {
            return txtUserLogin.Text.ToString().Trim();

        }
    }
    #endregion

    #region "Selected Status Target"
    /// <summary>
    /// Selected Status Target
    /// </summary>
    /// <Date>2010.12.15</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.1</Version>
    public string StatusTarget
    {
        get
        {
            String JobError="";
            switch (ddlStatus.SelectedValue)
            {
                case UtilConst.JOB_STATUS_SUCCESSVALUE:
                    JobError= UtilConst.JOB_STATUS_FINISHED;
                    break;
                case UtilConst.JOB_STATUS_ERRORVALUE:

                    JobError = UtilConst.JOB_STATUS_CANCELED;
                    JobError += "," + UtilConst.JOB_STATUS_SUSPENDED;
                    JobError += "," + UtilConst.JOB_STATUS_ERROR;
                    break;             
                default:
                    break;
            }

            return JobError;
         
        }
    }
    #endregion

    #region "Selected Jobtype Target"
    /// <summary>
    /// Selected Jobtype Target
    /// </summary>
    /// <Date>2010.12.15</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.1</Version>
    public string JobtypeTarget
    {
        get
        {
            String Jobtype = "";
            switch (ddlJobtype.SelectedValue)
            {
                case UtilConst.JOB_JOBTYPE_ALLVALUE:
                    break;
                default:
                    Jobtype = ddlJobtype.SelectedValue;                   
                    break;
            }

            return Jobtype;

        }
    }
    #endregion

    #region "Selected MFP Target"
    /// <summary>
    /// Selected MFP Target
    /// </summary>
    /// <Date>2010.12.15</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.1</Version>
    public string MFPTarget
    {
        get
        {
            if (ddlMFPItem.SelectedValue == "")
            {
                return "";
            }
            else
            {
                return ddlMFPItem.SelectedValue;
            }
        }
    }
    #endregion

    #region "Set Period"
    /// <summary>
    /// Set Period
    /// </summary>
    /// <param name="PeriodTime"></param>
    /// <Date>2010.06.23</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
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
    #endregion
}
