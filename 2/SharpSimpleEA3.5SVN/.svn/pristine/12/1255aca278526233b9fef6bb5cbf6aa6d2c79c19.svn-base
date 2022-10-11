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
using System.Collections.Generic;
using dtGroupInfoTableAdapters;
using System.Data.SqlClient;
using dtPaperSizeInformationTableAdapters;
using dtJobInformationTableAdapters;
using dtUserInfoTableAdapters;
using dtMFPInformationTableAdapters;


public partial class Report_GroupUserMFPJobReportResult : MainPage
{
    #region "private const"
    // Cell Width for BigTitle.
    private Unit WIDTH_BIGTITLE = new Unit(180);
    // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ST
    // Cell Width for Second Title.
    private Unit WIDTH_SECONDTITLE = new Unit(120);
    // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ED

    // Cell Width for SmallTitle.
    private Unit WIDTH_SMALLTITLE = new Unit(60);

    private int Dsp_Count_mode = 0;
    private int Dsp_A3_A4 = 0;


    #endregion

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.24</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        // SimpleDetailPage.PageLoad
        this.Master.PageLoad();
        StyleChange();

        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;
        Dsp_A3_A4 = settingrow.Dis_A3_A4;

        if (!IsPostBack)
        {
            //CheckBox Icons are Display
            this.Master.cheakCellItem = true;
            //The default display item settings for User Job Report Result screen to Checked
            this.Master.CheckBox_item_Set();
            // Get Result and displayed in Page.
            DisplayDetailResult();
        }
        // CheckBox in this page
        this.Master.CheckBox_chkCopy().CheckedChanged += new EventHandler(chkCopy_OnCheckedChanged);
        this.Master.CheckBox_chkPrint().CheckedChanged += new EventHandler(chkPrint_OnCheckedChanged);
        
    }
    #endregion

    #region "Get Result and displayed in Page."
    /// <summary>
    /// DisplayDetailResult
    /// </summary>
    /// <Date>2010.06.28</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void DisplayDetailResult()
    {
        string strSql = "";
        // 1.Get PageID List.
        // 1.1 ID
        string sqlId = "";
        foreach (string itme in this.Master.ID_LIST)
        {
            if ("".Equals(sqlId))
            {
                sqlId = itme;
            }
            else
            {
                sqlId += "," + itme;
            }
        }

        // 2.For each Result Column's(Job and Function type) Name , Get DateInformation.
        // 2.1 New DateTable and Sql.
        string strColumnName = "";
        strSql = "";
        // Foot
        Hashtable foothashList = new Hashtable();

        // [All Count] Column
        strColumnName = "Count";
        //decimal intAllCount = 0;
        foothashList.Add(strColumnName, 0);

        // [B/W Count] Column
        strColumnName = "BWCount";
        decimal intBWCount = 0;
        foothashList.Add(strColumnName, 0);

        // [FullColor Count] Column
        strColumnName = "FullColorCount";
        decimal intFCCount = 0;
        foothashList.Add(strColumnName, 0);

        //chen update 20140624 start
        string selSql = "";
        if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
        {
            selSql = " ,SUM(SpendMoney) AS PageCount ";
        }
        else
        {
            if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
            {
                selSql = " ,SUM(DspNumber) AS PageCount ";
            }
            else
            {
                selSql = " ,SUM(Number) AS PageCount ";
            }
        }
        //chen update 20140624 end



        // Get PageCount Group By JobID and GroupId
        //strSql = " SELECT JobID , FunctionID , GroupID ,B.ModelName, SUM(Number)  AS PageCount " + Environment.NewLine;
        strSql = " SELECT JobID , FunctionID , GroupID ,B.ModelName " + Environment.NewLine;
        strSql += selSql + Environment.NewLine;

        strSql += "  FROM [JobInformation] as A, MFPInformation as B" + Environment.NewLine;
        strSql += " WHERE GroupID in ({0}) " + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= " + 
            ConvertDateToSQL(this.Master.START_TIME) + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= " + 
            ConvertDateToSQL(this.Master.END_TIME) + Environment.NewLine;
        strSql += "   AND A.SerialNumber = B.SerialNumber" + Environment.NewLine;
        if (!("0".Equals(this.Master.MFP_SERIAL_NUMBER)))
        {
            strSql += " AND A.SerialNumber=" + ConvertStringToSQL(this.Master.MFP_SERIAL_NUMBER.ToString());

        }
        if (this.Master.CheckBox_chkCopy().Checked == true && this.Master.CheckBox_chkPrint().Checked == true)
        {
            strSql += " AND (A.JobID = 1 or A.JobID = 2)";
        }
        else
            if (this.Master.CheckBox_chkCopy().Checked == true)
            {
                strSql += " AND A.JobID = 1";
            }
            else
                if (this.Master.CheckBox_chkPrint().Checked == true)
                {
                    strSql += " AND A.JobID = 2";
                }


        strSql += "GROUP BY GroupID , ModelName , JobID , FunctionID " + Environment.NewLine;

        // 3.2 Get Date
        // Detail Date
        DataTable table = ExecuteDataTable(string.Format(strSql, sqlId));

        // 3.2 Get Column Count
        // Get PageCount Group By JobID and GroupId
        strSql = " SELECT distinct(B.ModelName) " + Environment.NewLine;
        strSql += "  FROM [JobInformation] as A, MFPInformation as B" + Environment.NewLine;
        strSql += " WHERE GroupID in ({0}) " + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= " + ConvertDateToSQL(this.Master.START_TIME) + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= " + ConvertDateToSQL(this.Master.END_TIME) + Environment.NewLine;
        strSql += "   AND A.SerialNumber = B.SerialNumber" + Environment.NewLine;
        if (!("0".Equals(this.Master.MFP_SERIAL_NUMBER)))
        {
            strSql += " AND A.SerialNumber=" + ConvertStringToSQL(this.Master.MFP_SERIAL_NUMBER.ToString());

        }
        if (this.Master.CheckBox_chkCopy().Checked == true && this.Master.CheckBox_chkPrint().Checked == true)
        {
            strSql += " AND (A.JobID = 1 or A.JobID = 2)";
        }
        else
            if (this.Master.CheckBox_chkCopy().Checked == true)
            {
                strSql += " AND A.JobID = 1";
            }
            else
                if (this.Master.CheckBox_chkPrint().Checked == true)
                {
                    strSql += " AND A.JobID = 2";
                }
        strSql += "GROUP BY GroupID , ModelName" + Environment.NewLine;

        DataTable columnTable = ExecuteDataTable(string.Format(strSql, sqlId));

        // 3.2 Get User count
        // Get PageCount Group By JobID and GroupId
        //strSql = " SELECT [GroupID] ,UserID , FunctionID ,B.ModelName, SUM(Number)  AS PageCount " + Environment.NewLine;
        strSql = " SELECT [GroupID] ,UserID , FunctionID ,B.ModelName " + Environment.NewLine;
        strSql += selSql  + Environment.NewLine;

        strSql += "  FROM [JobInformation] as A, MFPInformation as B" + Environment.NewLine;
        strSql += " WHERE GroupID in ({0}) " + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= " + ConvertDateToSQL(this.Master.START_TIME) + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= " + ConvertDateToSQL(this.Master.END_TIME) + Environment.NewLine;
        strSql += "   AND A.SerialNumber = B.SerialNumber" + Environment.NewLine;
        if (!("0".Equals(this.Master.MFP_SERIAL_NUMBER)))
        {
            strSql += " AND A.SerialNumber=" + ConvertStringToSQL(this.Master.MFP_SERIAL_NUMBER.ToString());

        }
        if (this.Master.CheckBox_chkCopy().Checked == true && this.Master.CheckBox_chkPrint().Checked == true)
        {
            strSql += " AND (A.JobID = 1 or A.JobID = 2)";
        }
        else
            if (this.Master.CheckBox_chkCopy().Checked == true)
            {
                strSql += " AND A.JobID = 1";
            }
            else
                if (this.Master.CheckBox_chkPrint().Checked == true)
                {
                    strSql += " AND A.JobID = 2";
                }
        strSql += "GROUP BY GroupID, ModelName , UserID , FunctionID " + Environment.NewLine;

        DataTable userTable = ExecuteDataTable(string.Format(strSql, sqlId));




        // 3.4 Title Of Head
        TableHeaderCell hCell;
        foreach (DataRow row in columnTable.Rows)
        {
            hCell = new TableHeaderCell();

            hCell.ColumnSpan = 3;
            hCell.Width = WIDTH_BIGTITLE;

            hCell.Text = row[0].ToString();
            hCell.CssClass = UtilConst.CSS_SMALL_ROW;
            hCell.Height = new Unit(23);
            tblHRBigTitle.Cells.Add(hCell);
            foothashList.Add(row[0].ToString(), 0);

            // ColorType
            // BW
            hCell = new TableHeaderCell();
            hCell.Text = UtilConst.COLOR_BW;
            hCell.Width = WIDTH_SMALLTITLE;
            hCell.Wrap = false;
            // 2010.09.16 Add By SES Ji.JianXiong ST
            hCell.Height = new Unit(23);
            // 2010.09.16 Add By SES Ji.JianXiong ED
            tblHRSmallTitle.Cells.Add(hCell);
            foothashList.Add(row[0].ToString() + "BW", 0);


            // Full Color
            hCell = new TableHeaderCell();
            hCell.Text = UtilConst.COLOR_FULLCOLOR;
            hCell.Width = WIDTH_SMALLTITLE;
            hCell.Wrap = false;
            tblHRSmallTitle.Cells.Add(hCell);
            foothashList.Add(row[0].ToString() + "FC", 0);


            // count
            hCell = new TableHeaderCell();
            hCell.Text = "合计";
            hCell.Width = WIDTH_SMALLTITLE;
            hCell.Wrap = false;
            // 2010.09.16 Add By SES Ji.JianXiong ST
            hCell.Height = new Unit(23);
            // 2010.09.16 Add By SES Ji.JianXiong ED
            tblHRSmallTitle.Cells.Add(hCell);
            foothashList.Add(row[0].ToString() + "ALL", 0);
        }

        // 3.5 Detail
        foreach (string item in this.Master.ID_LIST)
        {
            TableRow row = new TableRow();
            // Style
            if (!Convert.ToBoolean((tblDetail.Rows.Count - 2) % 2))
            {
                row.CssClass = UtilConst.CSS_ITEM_EVEN;
            }
            else
            {
                // 2010.09.16 Add By SES Ji.JianXiong ST
                row.CssClass = UtilConst.CSS_ITEM_ODD;
                // 2010.09.16 Add By SES Ji.JianXiong ED
            }

            int intGroupID = int.Parse(item);
            // Group
            TableCell tCell = new TableCell();
            GroupInfoTableAdapter groupAdapter = new GroupInfoTableAdapter();
            tCell.Text = (string)groupAdapter.GetGroupNameById(intGroupID);
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);

            //intAllCount = 0;
            intBWCount = 0;
            intFCCount = 0;


            foreach (DataRow columnRow in columnTable.Rows)
            {
                decimal BWCount = 0;
                decimal FCCount = 0;

                // BW
                tCell = new TableCell();
                BWCount = GetDetailValueFromTable(table, UtilCommon.ConvertStringToSQL(columnRow[0].ToString()), 1, intGroupID, "PageCount");
                //tCell.Text = UtilCommon.IntToMoney(BWCount);
                tCell.Text = UtilCommon.decimalToMoney(BWCount, Dsp_Count_mode);

                strColumnName = columnRow[0].ToString() + "BW";
                //foothashList[strColumnName] = (int)foothashList[strColumnName] + BWCount;
                foothashList[strColumnName] = (decimal)foothashList[strColumnName] + BWCount;
                row.Cells.Add(tCell);

                // full Color
                tCell = new TableCell();
                FCCount = GetDetailValueFromTable(table, UtilCommon.ConvertStringToSQL(columnRow[0].ToString()), 2, intGroupID, "PageCount");
                //tCell.Text = UtilCommon.IntToMoney(FCCount);
                tCell.Text = UtilCommon.decimalToMoney(FCCount, Dsp_Count_mode);
                strColumnName = columnRow[0].ToString() + "FC";
                //foothashList[strColumnName] = (int)foothashList[strColumnName] + FCCount;
                foothashList[strColumnName] = (decimal)foothashList[strColumnName] + FCCount;
                row.Cells.Add(tCell);

                // Count
                tCell = new TableCell();
                //tCell.Text = UtilCommon.IntToMoney(BWCount + FCCount);
                tCell.Text = UtilCommon.decimalToMoney(BWCount + FCCount, Dsp_Count_mode);

                strColumnName = columnRow[0].ToString() + "ALL";
                //foothashList[strColumnName] = (int)foothashList[strColumnName] + BWCount + FCCount;
                foothashList[strColumnName] = (decimal)foothashList[strColumnName] + BWCount + FCCount;
                row.Cells.Add(tCell);

                intBWCount = intBWCount + BWCount;
                intFCCount = intFCCount + FCCount;
            }
            tblDetail.Rows.Add(row);



            // user
            UserInfoTableAdapter userAdapter = new UserInfoTableAdapter();
            dtUserInfo.UserInfoDataTable tmpTable = userAdapter.GetDataByGroupID(intGroupID);

            foreach (DataRow userRow in tmpTable)
            {
                row = new TableRow();
                // Style
                if (!Convert.ToBoolean((tblDetail.Rows.Count - 2) % 2))
                {
                    row.CssClass = UtilConst.CSS_ITEM_EVEN;
                }
                else
                {
                    // 2010.09.16 Add By SES Ji.JianXiong ST
                    row.CssClass = UtilConst.CSS_ITEM_ODD;
                    // 2010.09.16 Add By SES Ji.JianXiong ED
                }


                tCell = new TableCell();
                tCell.Text = userRow[1].ToString();
                tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
                tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                row.Cells.Add(tCell);


                foreach (DataRow columnRow in columnTable.Rows)
                {
                    decimal BWCount = 0;
                    decimal FCCount = 0;

                    tCell = new TableCell();
                    BWCount = GetDetailValueFromTable(userTable, UtilCommon.ConvertStringToSQL(columnRow[0].ToString()), Convert.ToInt32(userRow[6].ToString()), 1, intGroupID, "PageCount");
                    //tCell.Text = UtilCommon.IntToMoney(BWCount);
                    tCell.Text = UtilCommon.decimalToMoney(BWCount, Dsp_Count_mode);
                    row.Cells.Add(tCell);


                    tCell = new TableCell();
                    FCCount = GetDetailValueFromTable(userTable, UtilCommon.ConvertStringToSQL(columnRow[0].ToString()), Convert.ToInt32(userRow[6].ToString()), 2, intGroupID, "PageCount");
                    //tCell.Text = UtilCommon.IntToMoney(FCCount);
                    tCell.Text = UtilCommon.decimalToMoney(FCCount, Dsp_Count_mode);
                    row.Cells.Add(tCell);

                    // Count
                    tCell = new TableCell();
                    //tCell.Text = UtilCommon.IntToMoney(BWCount + FCCount);
                    tCell.Text = UtilCommon.decimalToMoney(BWCount + FCCount, Dsp_Count_mode);

                    row.Cells.Add(tCell);
                }
                tblDetail.Rows.Add(row);
            }
        }


        // Foot
        // Foot Count
        TableFooterRow tblFootRow = new TableFooterRow();
        hCell = new TableHeaderCell();
        hCell.Text = "合计";
        hCell.ColumnSpan = 1;
        hCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        tblFootRow.Cells.Add(hCell);

        // Foot Details
        foreach (DataRow columnRow in columnTable.Rows)
        {
            // B/W
            strColumnName = columnRow[0].ToString() + "BW";
            hCell = new TableHeaderCell();
            //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName]);
            hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName]);
            hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            tblFootRow.Cells.Add(hCell);

            // FullColor

            strColumnName = columnRow[0].ToString() + "FC";
            hCell = new TableHeaderCell();
            //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName]);
            hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName]);
            hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            tblFootRow.Cells.Add(hCell);

            // Count
            strColumnName = columnRow[0].ToString() + "ALL";
            hCell = new TableHeaderCell();
            //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName]);
            hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName]);
            hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            tblFootRow.Cells.Add(hCell);
        }


        // 2010.09.16 Add By SES JiJianXiong ST
        tblFootRow.CssClass = UtilConst.CSS_FOOT_ROW;
        // 2010.09.16 Add By SES JiJianXiong ED
        tblDetail.Rows.Add(tblFootRow);

        
        // Display Set
        // Display Set
        int intWidth = (int)(300 + JobList.Count * WIDTH_BIGTITLE.Value);
        // 2011.1.12 Update By SES zhoumiao Ver.1.1 Update ST
        //if (intWidth > 1024)
        //{
        //    this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, intWidth.ToString() + "px");
        //}
        switch (JobList.Count)
        {
            case 1:
                this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, "1000px");
                break;
            case 2:
                this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, "1100px");
                break;
            case 3:
                this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, "1200px");
                break;
            case 4:
                this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, "1300px");
                break;
            default:
                this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, intWidth.ToString() + "px");
                break;
        }

        // 2011.1.12 Update By SES zhoumiao Ver.1.1 Update ED
    }

    #endregion

    #region "GetDetailValueFromTable"
    /// <summary>
    /// GetDetailValueFromTable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    /// <Date>2010.06.29</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private decimal GetDetailValueFromTable(DataTable dt, string modelName, int FunId, int GroupID, string strColumnName)
    {
        string strSql = "ModelName = {0} AND FunctionID = {1} AND GroupID = {2}";
        DataRow[] row = dt.Select(string.Format(strSql, modelName, FunId, GroupID));
        if (row == null || row.Length == 0)
        {
            return (0);
        }
        else
        {
            //return (int)row[0][strColumnName];
            return decimal.Parse(row[0][strColumnName].ToString());
        }

    }
    #endregion

    #region "GetDetailValueFromTable"
    /// <summary>
    /// GetDetailValueFromTable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    /// <Date>2010.06.29</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private int GetDetailValueFromTable(DataTable dt, string modelName,int userID , int FunId, int GroupID, string strColumnName)
    {
        string strSql = "ModelName = {0} AND FunctionID = {1} AND GroupID = {2} AND UserID = {3}";
        DataRow[] row = dt.Select(string.Format(strSql, modelName, FunId, GroupID, userID));
        if (row == null || row.Length == 0)
        {
            return (0);
        }
        else
        {
            return (int)row[0][strColumnName];
        }

    }
    #endregion

    #region "btnCSV_Click"
    /// <summary>
    /// btnCSV_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.30</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnCSVOutPut_Click(object sender, EventArgs e)
    {
        // 2010.11.24 Add By SES zhoumiao Ver.1.1 Update ST
        this.DisplayDetailResult();
        // 2010.11.24 Add By SES zhoumiao Ver.1.1 Update ST
        OutPutCsvFile("GroupJobReport", tblDetail);

    }
    #endregion

    #region "Get CSV OutPut Date"
    /// <summary>
    /// GetCsvDate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.30</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected List<List<string>> GetCsvDate(Table detail)
    {
        List<List<string>> CsvList = new List<List<string>>();
        // Tbale Cell 
        TableCell cell;
        //2010.12.14 Add By SES zhoumiao Ver.1.1 Update ST
        //Add titles and Target period on the csv files
        List<string> strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_PAGE_GROUPREPORT);
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_TARGET_MFP);
        strHeadList.Add(this.Master.GetMFPTargetName());
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_TIME_PERIOD);
        strHeadList.Add(this.Master.tbcTargetPeriod_text().Text);
        CsvList.Add(strHeadList);
        //2010.12.14 Add By SES zhoumiao Ver.1.1 Update ED
        // Get Date
        for (int i = 0; i < detail.Rows.Count; i++)
        {

            List<string> strList = new List<string>();
            // Table Row
            TableRow row = detail.Rows[i];
            // 1 Get Header Date
            // 1.1 Get Big Header Date.
            if (i == 0)
            {
                for (int j = 0; j < row.Cells.Count; j++)
                {

                    cell = row.Cells[j];
                    strList.Add(cell.Text);
                    for (int k = 0; k < cell.ColumnSpan - 1; k++)
                    {
                        strList.Add("");
                    }

                }
            }
            else if (i == 1)
            {
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    // Tbale Cell 
                    cell = row.Cells[j];
                    if (j == 0)
                    {
                        // Header's Blank and Count.
                        strList.Add("");

                        strList.Add(cell.Text);
                    }
                    else
                    {
                        strList.Add(cell.Text);
                    }
                }
            }
            else
            {
                foreach (TableCell detailcell in row.Cells)
                {
                    //2010.12.14 Update By SES zhoumiao Ver.1.1 Update ST
                    //strList.Add(detailcell.Text.Replace(",",""));
                    strList.Add(detailcell.Text.Replace(",", "，"));
                    //2010.12.14 Update By SES zhoumiao Ver.1.1 Update ED

                    for (int k = 0; k < detailcell.ColumnSpan - 1; k++)
                    {
                        strList.Add("");
                    }

                }
            }
            CsvList.Add(strList);
        }

        return CsvList;
    }
    #endregion

    #region "OutPutCsvFile"
    /// <summary>
    /// OutPutCsvFile
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="detail"></param>
    /// <Date>2010.06.30</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void OutPutCsvFile(string filename, Table detail)
    {
        // 1.Get Date
        List<List<string>> CsvList = GetCsvDate(detail);

        // 2.To Csv Date.
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (List<string> item in CsvList)
        {
            string strOutPut = null;
            foreach (string strItem in item)
            {
                if (strOutPut == null)
                {
                    strOutPut = strItem;
                }
                else
                {
                    strOutPut = strOutPut + "," + strItem;
                }
            }
            strOutPut = strOutPut + "\r\n";
            sb.Append(strOutPut);
        }

        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".csv");

        Response.ContentType = "application/text";

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

        Response.Write(sb);

        Response.End();

    }
    #endregion

    #region"chkCopy_OnCheckedChanged"
    /// <summary>
    /// chkCopy_OnCheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.11.24</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>
    public void chkCopy_OnCheckedChanged(object sender, EventArgs e)
    {
        if (this.Master.CheckBox_Selected_Check())
        {
            this.Master.CheckBox_chkCopy().Checked = true;
            //The Display job item control. At least one item must be selected.
            ErrorAlert(UtilConst.MSG_LEAST_ONE_SELECT);
        }

        this.DisplayDetailResult();

    }
    #endregion

    #region"chkPrint_OnCheckedChanged"
    /// <summary>
    /// chkPrint_OnCheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.11.24</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>
    public void chkPrint_OnCheckedChanged(object sender, EventArgs e)
    {
        if (this.Master.CheckBox_Selected_Check())
        {
            this.Master.CheckBox_chkPrint().Checked = true;
            //The Display job item control. At least one item must be selected.
            ErrorAlert(UtilConst.MSG_LEAST_ONE_SELECT);
        }

        this.DisplayDetailResult();


    }
    #endregion

    private void StyleChange()
    {
        this.Master.CheckBox_chkFax().Enabled = false;
        this.Master.CheckBox_chkOther().Enabled = false;
        this.Master.CheckBox_chkScan().Enabled = false;
        this.Master.CheckBox_chkFax().Checked = false;
        this.Master.CheckBox_chkOther().Checked = false;
        this.Master.CheckBox_chkScan().Checked = false;
    }
}