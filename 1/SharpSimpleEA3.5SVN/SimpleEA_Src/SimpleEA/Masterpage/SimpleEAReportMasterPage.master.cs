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
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using dtMFPInformationTableAdapters;


/// <summary>
/// MasterPage For Job Report screens.
/// </summary>
/// <Date>2010.06.22</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Masterpage_SimpleEAReportMasterPage : System.Web.UI.MasterPage
{
    #region"Some Icons return"
    //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ST

    //SimpleEAMasterPAGE Search Button
    public Button btn_Search_EAMaster()
    {
        return this.Master.btn_Search();
    }
    //SimpleEAMasterPAGE Search text
    public TextBox txt_Search_EAMaster()
    {
        return this.Master.txt_UserName();
    }
    //SimpleEAMasterPAGE Search DropDownList
    public DropDownList ddl_Search_EAMasterItem()
    {
        return this.Master.ddl_SearchList();
    }
    //update Button
    public Button Btn_update()
    {
        return this.Btnupdate;
    }
    //MFP Name / Serial Number DropDownList
    public DropDownList ddl_MFPItem()
    {
        return this.ddlMFPItem;
    }
    //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ED
    #endregion

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ST
        this.Master.CheakSearchItem = true;
        //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ED

        //2010.12.9 Add By SES zhoumiao Ver.1.1 Update ST
        if (!HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            this.Master.CheakSearchItem = false;
        }
        //2010.12.9 Add By SES zhoumiao Ver.1.1 Update ED
        // Defulat for Group Item Row is disVisible
        if (!IsPostBack)
        {
            Load_Once();
        }

        // 2010.11.22 Add By SES Jijianxiong Ver.1.1 Update ST
        if (!HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            // Access Prohibit:
            // Total Job Reprot Screen
            linkTotal.Disabled = true;
            // Group Job Report Screen
            linkGroup.Disabled = true;
            // MFP Job Report Screen
            linkMFP.Disabled = true;
            //puepng 2014 05 30
            linkGroupUser.Disabled = true;
            linkGroupUser.HRef = "";
            //end 
            // 2011.03.23 Add By SES Jijianxiong Ver.1.1 Update ST
            linkTotal.HRef = "";
            linkGroup.HRef = "";
            linkMFP.HRef = "";
            // 2011.03.23 Add By SES Jijianxiong Ver.1.1 Update ED
        }

        // 2010.11.22 Add By SES Jijianxiong Ver.1.1 Update ED

    }
    #endregion

    #region "Title"
    /// <summary>
    /// Title
    /// </summary>
    /// <Date>2010.06.15</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public string Title
    {
        set
        {
            // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ST
            //// Update By SES Jijianxiogn 2010-08-23 ST
            //// this.Master.Title = "Í³¼Æ - " + value;
            //this.Master.Title = UtilConst.CON_PAGE_JOBREPORT;
            //Master.SubTitle(value, "JobReport.aspx", false);
            //// Update By SES Jijianxiogn 2010-08-23 ED
            this.Master.Title = UtilConst.CON_PAGE_JOBREPORT;
            Master.SubTitle(value, "#", false);
            Master.JobReportTitle();

            // Set Title Style
            switch (value)
            {
                case UtilConst.CON_PAGE_TOTALJOBREPORT:
                    linkTotal.Attributes.Remove("class");
                    linkTotal.Attributes.Add("class", "small_link_select");
                    linkUser.Attributes.Remove("class");
                    linkUser.Attributes.Add("class", "small_link");
                    linkGroup.Attributes.Remove("class");
                    linkGroup.Attributes.Add("class", "small_link");
                    linkMFP.Attributes.Remove("class");
                    linkMFP.Attributes.Add("class", "small_link");
                    linkGroupUser.Attributes.Remove("class");
                    linkGroupUser.Attributes.Add("class", "small_link");
                    break;
                case UtilConst.CON_PAGE_USERJOBREPORT:
                    linkTotal.Attributes.Remove("class");
                    linkTotal.Attributes.Add("class", "small_link");
                    linkUser.Attributes.Remove("class");
                    linkUser.Attributes.Add("class", "small_link_select");
                    linkGroup.Attributes.Remove("class");
                    linkGroup.Attributes.Add("class", "small_link");
                    linkMFP.Attributes.Remove("class");
                    linkMFP.Attributes.Add("class", "small_link");
                    linkGroupUser.Attributes.Remove("class");
                    linkGroupUser.Attributes.Add("class", "small_link");
                    break;
                case UtilConst.CON_PAGE_GROUPJOBREPORT:
                    linkTotal.Attributes.Remove("class");
                    linkTotal.Attributes.Add("class", "small_link");
                    linkUser.Attributes.Remove("class");
                    linkUser.Attributes.Add("class", "small_link");
                    linkGroup.Attributes.Remove("class");
                    linkGroup.Attributes.Add("class", "small_link_select");
                    linkMFP.Attributes.Remove("class");
                    linkMFP.Attributes.Add("class", "small_link");
                    linkGroupUser.Attributes.Remove("class");
                    linkGroupUser.Attributes.Add("class", "small_link");
                    break;
                case UtilConst.CON_PAGE_MFPJOBREPORT:
                    linkTotal.Attributes.Remove("class");
                    linkTotal.Attributes.Add("class", "small_link");
                    linkUser.Attributes.Remove("class");
                    linkUser.Attributes.Add("class", "small_link");
                    linkGroup.Attributes.Remove("class");
                    linkGroup.Attributes.Add("class", "small_link");
                    linkMFP.Attributes.Remove("class");
                    linkMFP.Attributes.Add("class", "small_link_select");
                    linkGroupUser.Attributes.Remove("class");
                    linkGroupUser.Attributes.Add("class", "small_link");
                    break;
                case UtilConst.CON_PAGE_GROUPUSERJOBREPORT:
                    linkTotal.Attributes.Remove("class");
                    linkTotal.Attributes.Add("class", "small_link");
                    linkUser.Attributes.Remove("class");
                    linkUser.Attributes.Add("class", "small_link");
                    linkGroup.Attributes.Remove("class");
                    linkGroup.Attributes.Add("class", "small_link");
                    linkMFP.Attributes.Remove("class");
                    linkMFP.Attributes.Add("class", "small_link");
                    linkGroupUser.Attributes.Remove("class");
                    linkGroupUser.Attributes.Add("class", "small_link_select");
                    break;
                default:
                    break;
            }
            // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ED
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
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
        Btnupdate.Visible = true;
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED
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
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
        Btnupdate.Visible = true;
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED
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
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void List_Init()
    {
        ListItem item = new ListItem();
        // Total Target Item
        // User
        item.Value = UtilConst.CON_TOTALTARGET_USER;
        item.Text = UtilConst.CON_TOTALTARGET_USERNAME;
        ddlGroupItem.Items.Add(item);
        item = new ListItem();
        // Group
        item.Value = UtilConst.CON_TOTALTARGET_GROUP;
        item.Text = UtilConst.CON_TOTALTARGET_GROUPNAME;
        ddlGroupItem.Items.Add(item);

        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
        MFPInformationListTableAdapter MFPAdapter = new MFPInformationListTableAdapter();
        ddlMFPItem.DataSource = MFPAdapter.GetData();
        ddlMFPItem.DataBind();
        ddlMFPItem.Items.Insert(0, "");
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED


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
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
        Btnupdate.Visible = true;
         //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED
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
        if (CheckNeedReloadStartDate )
        {
            reLoadDayList(ddlBeginDay , ddlBeginYear.SelectedValue , ddlBeginMonth.SelectedValue);
        }

        // End Date
        if (CheckNeedReloadEndDate )
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

        get {
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
            if ( ddlBeginDay.Items[ddlBeginDay.Items.Count - 1].Value.Equals(
                    DateTime.DaysInMonth(int.Parse(ddlBeginYear.SelectedValue),int.Parse(ddlBeginMonth.SelectedValue)).ToString() ) ) {
                return false;
            } else {
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
            if ( ddlEndDay.Items[ddlEndDay.Items.Count - 1].Value.Equals(
                    DateTime.DaysInMonth(int.Parse(ddlEndYear.SelectedValue),int.Parse( ddlEndMonth.SelectedValue)).ToString()))
            {
                return false;
            } else {
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
        get {
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

    #region "Group Item Change Event"
    /// <summary>
    /// Group Item Change Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void ddlGroupItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
        //Set Init value 
        SetPeriod(DateTime.Now);
        this.Btnupdate.Visible = false;
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED        
        SelectedIndexChanged_ddlMaster(this, new CommandEventArgs(ddlGroupItem.SelectedItem.Text, ddlGroupItem.SelectedValue));
    }

    public event CommandEventHandler SelectedIndexChanged_ddlMaster;

    #endregion

    #region "Group Item's Display Set"
    /// <summary>
    /// Group Item's Display Set
    /// </summary>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public Boolean GroupDisplay
    {
        set
        {
            GroupItem.Visible = value;
            //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
            GroupItemList.Visible = value;
            MFPItem.ColumnSpan = 3;
            //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED
        }
    }
    #endregion

    #region "Table Search Visible"
    /// <summary>
    /// Table Search Visible
    /// </summary>
    /// <Date>2012.01.13</Date>
    /// <Author> Wei Changye</Author>
    /// <Version>1.20</Version>
    public Boolean TableSearch
    {
        set
        {
            tableSearch.Visible = value;
        }
    }
    #endregion
    

    #region "Selected Total Target"
    /// <summary>
    /// Selected Total Target
    /// </summary>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public string TotalTarget
    {
        get
        {
            if (ddlGroupItem.SelectedValue == "")
            {
                return UtilConst.CON_TOTALTARGET_USER;
            }
            else
            {
                return ddlGroupItem.SelectedValue;
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

    #region"Search Item"
    /// <summary>
    /// Set Search Item
    /// </summary>
    /// <Date>2010.12.9</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.10</Version>   

    public String SearchItem
    {
        set
        {
            this.Master.ddl_SearchList().Items.Clear();
            ListItem item = new ListItem();
            switch (value)
            {
                case UtilConst.CON_PAGE_USERJOBREPORT:

                    item.Value = UtilConst.CON_ITEM_USER;
                    item.Text = UtilConst.CON_ITEM_USERNAME;
                    this.Master.ddl_SearchList().Items.Add(item);
                    //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ST
                    this.Master.ddl_SearchList().Enabled = false;
                    //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ED
                    break;
                case UtilConst.CON_PAGE_GROUPJOBREPORT:

                    item.Value = UtilConst.CON_ITEM_GROUP;
                    item.Text = UtilConst.CON_ITEM_GROUPNAME;
                    this.Master.ddl_SearchList().Items.Add(item);
                    //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ST
                    this.Master.ddl_SearchList().Enabled = false;
                    //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ED
                    break;
                case UtilConst.CON_PAGE_MFPJOBREPORT:

                    item.Value = UtilConst.CON_ITEM_MFPModel;
                    item.Text = UtilConst.CON_ITEM_MFPModelName;
                    this.Master.ddl_SearchList().Items.Add(item);
                    item = new ListItem();
                    item.Value = UtilConst.CON_ITEM_MFPSerial;
                    item.Text = UtilConst.CON_ITEM_MFPSerialNumber;
                    this.Master.ddl_SearchList().Items.Add(item);
                    break;
                 
                case UtilConst.CON_PAGE_GROUPUSERJOBREPORT:
                     item.Value = UtilConst.CON_ITEM_GROUP;
                    item.Text = UtilConst.CON_ITEM_GROUPNAME;
                    this.Master.ddl_SearchList().Items.Add(item);
                    //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ST
                    this.Master.ddl_SearchList().Enabled = false;
                    //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ED
                    break;
              
                default:
                    break;

            }

        }

    }
    #endregion

    #region "Selected MFP Target"
    /// <summary>
    /// Selected MFP Target
    /// </summary>
    /// <Date>2010.12.10</Date>
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

    #region "MFP Item's Display Set"
    /// <summary>
    /// Group Item's Display Set
    /// </summary>
    /// <Date>2010.12.10</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    public Boolean MFPDisplay
    {
        set
        {
            MFPItem.Visible = value;
            MFPItemList.Visible = value;

        }
    }
    #endregion

    #region "MFP Item Change Event"
    /// <summary>
    /// MFP Item Change Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.10</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void ddlMFPItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Btnupdate.Visible = true;

    }
    #endregion

}


