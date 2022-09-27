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
using System.Collections.Generic;
//using dtLogInformationTableAdapters;
using System.Data.SqlClient;
//using dtUserInfoTableAdapters;
//using dtGroupInfoTableAdapters;
//using dtPaperSizeInformationTableAdapters;
//using dtJobTypeInformationTableAdapters;
//using dtFunctionTypeInformationTableAdapters;
//using dtOSAErrorInformationTableAdapters;

/// <summary>
/// Available Report Result.
/// </summary>
/// <Date>2010.12.16</Date>
/// <Author>SES Zhou Miao</Author>
/// <Version>1.1</Version>
public partial class Report_LogViewReportResult : MainPage
{
    private int Dsp_Count_mode = 0;
    private int Dsp_A3_A4 = 0;

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.16</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        // 2010.12.22 Add By SES Jijianxiong Check Access Role ST
        // Check Access Role
        CheckUser();
        // 2010.12.22 Add By SES Jijianxiong Check Access Role ED
        // SimpleDetailPage.PageLoad
        this.Master.PageLoad();

        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;
        Dsp_A3_A4 = settingrow.Dis_A3_A4;

        // Get Result and displayed in Page.
        DisplayDetailResult();
        

    }
    #endregion

    // 2011.1.10 Delete By SES Zhou Miao Ver.1.1 Update ST
    //#region "Get Result and displayed in Page."
    ///// <summary>
    ///// DisplayDetailResult
    ///// </summary>
    ///// <Date>2010.12.17</Date>
    ///// <Author>SES Zhou Miao</Author>
    ///// <Version>1.1</Version>
    //private void DisplayDetailResult()
    //{
    //    int GroupId;
    //    // 3.3 Detail
    //    foreach (string item in this.Master.ID_LIST)
    //    {
    //        TableRow row = new TableRow();
    //        // Style
    //        if (!Convert.ToBoolean((tblDetail.Rows.Count - 2) % 2))
    //        {
    //            row.CssClass = UtilConst.CSS_ITEM_EVEN;
    //        }
    //        else
    //        {               
    //            row.CssClass = UtilConst.CSS_ITEM_ODD;             
    //        }     

    //        LogInformationTableAdapter LogList = new LogInformationTableAdapter();
    //        dtLogInformation.LogInformationRow LogListRow = LogList.GetDataByID(int.Parse(item))[0];
            
    //        TableCell tCell = new TableCell();
    //        //Process Time
    //        // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ST
    //        //tCell.Text = LogListRow.Time.ToString("yyyy/MM/dd HH:mm:ss").Replace("-","/");
    //        tCell.Text = LogListRow.Time.ToString("yyyy/MM/dd HH:mm:ss");
    //        // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ED
    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);

    //        //User Name
    //        tCell = new TableCell();
    //        UserInfoTableAdapter UserInfo = new UserInfoTableAdapter();
    //        dtUserInfo.UserInfoDataTable UserInfoDataTable = UserInfo.GetDataByUserId(LogListRow.UserID);
    //        if (UserInfoDataTable.Rows.Count < 1)
    //        {
    //            tCell.Text = "未知";
    //            GroupId = -3;

    //        }
    //        else
    //        {
    //            dtUserInfo.UserInfoRow UserInfoRow = UserInfoDataTable[0];
    //            tCell.Text = UserInfoRow.UserName;
    //            GroupId = UserInfoRow.GroupID;
    //        }

    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);

    //        //Group Name
    //        tCell = new TableCell();
    //        GroupInfoTableAdapter GroupInfo = new GroupInfoTableAdapter();
    //        dtGroupInfo.GroupInfoDataTable GroupInfoDataTable = GroupInfo.GetGroupInfoDataById(GroupId);
    //        if (GroupInfoDataTable.Rows.Count < 1)
    //        {
    //            tCell.Text = "未知";
    //        }
    //        else
    //        {
    //            dtGroupInfo.GroupInfoRow GroupInfoRow = GroupInfoDataTable[0];
    //            tCell.Text = GroupInfoRow.GroupName;
    //        }
    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);

    //        //MFP Model Name
    //        tCell = new TableCell();
    //        tCell.Text = LogListRow.MFPModel;
    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);

    //        //MFP Serial Number
    //        tCell = new TableCell();
    //        tCell.Text = LogListRow.SerialNumber;
    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);

    //        //MFP IP Address
    //        tCell = new TableCell();
    //        tCell.Text = LogListRow.MFPIPAddress;
    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);

    //        //Job Type
    //        if ((LogListRow.JobID == 8) && (LogListRow.FunctionID == 2))
    //        {
    //            tCell = new TableCell();
    //            tCell.Text = UtilConst.ITEM_TITLE_FaxC2;
    //            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //            row.Cells.Add(tCell);
    //        }
    //        else
    //        {
    //            tCell = new TableCell();
    //            JobTypeInformationTableAdapter JobTypeInfo = new JobTypeInformationTableAdapter();
    //            dtJobTypeInformation.JobTypeInformationDataTable JobTypeInfoDataTable = JobTypeInfo.GetDataByJobId(LogListRow.JobID);
    //            if (JobTypeInfoDataTable.Rows.Count < 1)
    //            {
    //                tCell.Text = "未知";
    //            }
    //            else
    //            {
    //                dtJobTypeInformation.JobTypeInformationRow JobTypeInfoRow = JobTypeInfoDataTable[0];
    //                tCell.Text = JobTypeInfoRow.JobNameDisp;
    //            }

    //            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //            row.Cells.Add(tCell);
    //        }
    //        //File Name
    //        tCell = new TableCell();
    //        tCell.Text = IsStringNull(LogListRow.FileName);
    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);

    //        //Color Mode
    //        if ((LogListRow.JobID == 8) && (LogListRow.FunctionID == 2))
    //        {
    //            tCell = new TableCell();
    //            tCell.Text = UtilConst.COLOR_BW;
    //            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //            row.Cells.Add(tCell);
    //        }            
    //        else
    //        {
    //            tCell = new TableCell();
    //            FunctionTypeInformationTableAdapter FunctionTypeInfo = new FunctionTypeInformationTableAdapter();
    //            dtFunctionTypeInformation.FunctionTypeInformationDataTable FunctionTypeInfoDataTable =
    //                FunctionTypeInfo.GetDataByJobIdAndFunctionID(LogListRow.JobID, LogListRow.FunctionID);
    //            if (FunctionTypeInfoDataTable.Rows.Count < 1)
    //            {
    //                tCell.Text = UtilConst.JOB_UNKNOWN;
    //            }
    //            else
    //            {
    //                dtFunctionTypeInformation.FunctionTypeInformationRow FunctionTypeInfoRow = FunctionTypeInfoDataTable[0];
    //                tCell.Text = FunctionTypeInfoRow.FunctionNameDisp;
    //            }
    //            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //            row.Cells.Add(tCell);
    //        }
    //        //Page Size
    //        tCell = new TableCell();
    //        PaperSizeInformationTableAdapter PaperSizeInfor = new PaperSizeInformationTableAdapter();
    //        dtPaperSizeInformation.PaperSizeInformationDataTable PaperSizeInforDataTable = PaperSizeInfor.GetDataByID(LogListRow.PageID);
    //        if (PaperSizeInforDataTable.Rows.Count < 1)
    //        {
    //            tCell.Text = UtilConst.JOB_UNKNOWN;
    //        }
    //        else
    //        {
    //            dtPaperSizeInformation.PaperSizeInformationRow PaperSizeInforRow = PaperSizeInforDataTable[0];
    //            tCell.Text = IsStringNull(PaperSizeInforRow.PaperSize);
    //        }

    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);           
    //        //Page Count
    //        if (LogListRow.Number==-1)
    //        {
    //            tCell = new TableCell();
    //            tCell.Text = UtilConst.JOB_UNKNOWN;
    //            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //            row.Cells.Add(tCell);
    //        }
    //        else
    //        {
    //            tCell = new TableCell();
    //            tCell.Text = UtilCommon.IntToMoney(LogListRow.Number);
    //            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
    //            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //            row.Cells.Add(tCell);
    //        }
    //        //Duplex Setting
    //        tCell = new TableCell();
    //        switch (LogListRow.Duplex)
    //        {
    //            case 1:
    //                tCell.Text = UtilConst.JOB_DUPLEX_1SIDED;
    //                 break;
    //            case 2:
    //                tCell.Text = UtilConst.JOB_DUPLEX_2SIDED;
    //                break;
    //            case 3:
    //                tCell.Text = UtilConst.JOB_DUPLEX_2SIDED_BOOKLET;
    //                break;
    //            case 4:
    //                tCell.Text = UtilConst.JOB_DUPLEX_2SIDED_TABLET;
    //                 break;
    //            case 5:
    //                tCell.Text = UtilConst.JOB_DUPLEX_PAMPHLET;
    //                 break;
    //            default:
    //                tCell.Text = UtilConst.JOB_UNKNOWN;
    //                break;
    //        }
            
    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);
    //        //Status
    //        tCell = new TableCell();
    //        switch (LogListRow.Status)
    //        {
    //            case 0:
    //                tCell.Text = UtilConst.JOB_STATUS_CREATENAME;
    //                break;
    //            case 3:
    //                tCell.Text = UtilConst.JOB_STATUS_CANCELEDNAME;
    //                tCell.Style.Add(HtmlTextWriterStyle.Color, "red");
    //                break;
    //            case 4:
    //                tCell.Text = UtilConst.JOB_STATUS_SUSPENDEDNAME;
    //                tCell.Style.Add(HtmlTextWriterStyle.Color, "red");
    //                break;
    //            case 5:
    //                tCell.Text = UtilConst.JOB_STATUS_FINISHEDNAME;
    //                break;
    //            case 6:
    //                tCell.Text = UtilConst.JOB_STATUS_ERRORNAME;
    //                tCell.Style.Add(HtmlTextWriterStyle.Color, "red");
    //                break;
    //            default:
    //                tCell.Text = UtilConst.JOB_UNKNOWN;
    //                break;
    //        }            
    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);
    //        //Error Information
          
    //        OSAErrorInformationTableAdapter OSAErrorInfo = new OSAErrorInformationTableAdapter();
    //        dtOSAErrorInformation.OSAErrorInformationDataTable OSAErrorInfoTable = OSAErrorInfo.GetData(LogListRow.ErrorCode);
    //        tCell = new TableCell();
    //        if (OSAErrorInfoTable.Rows .Count< 1)
    //        {
    //            tCell.Text = UtilConst.JOB_UNKNOWN;
    //        }
    //        else
    //        {
    //            dtOSAErrorInformation.OSAErrorInformationRow OSAErrorInfoRow = OSAErrorInfoTable[0];                
    //            tCell.Text = OSAErrorInfoRow.ErrorInfo;
    //        }
            
    //        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
    //        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
    //        row.Cells.Add(tCell);
           

    //        tblDetail.Rows.Add(row);
    //    }

    //    this.Page.Form.Style.Add(HtmlTextWriterStyle.Width,  "1700px");

    //}

    //#endregion     
    // 2011.1.10 Delete By SES Zhou Miao Ver.1.1 Update ED

    #region "Get Result and displayed in Page."
    /// <summary>
    /// DisplayDetailResult
    /// </summary>
    /// <Date>2011.1.10</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    private void DisplayDetailResult()
    {
        //chen add st
        int Dsp_Count_mode = 0;
        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;
        //chen add ed    
        // 1.Get PageID List.
        // 1.1 ID
        string sqlId = "";

        foreach (string itme in this.Master.ID_LIST)
        {
            if ("".Equals(sqlId))
            {
                sqlId = "'" + itme + "'";
            }
            else
            {
                sqlId += ",'" + itme + "'";
            }
        }
        String strSql = "";

        //chen update 20140624 start
        string selSql = "";
        string selSql2 = "";
        if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A4))
        {
            selSql = " ,[Number] ";
            selSql2 = " ,[PapeCount] ";
        }
        else
        {
            selSql = " ,[DspNumber] AS Number";
            selSql2 = " ,[DspPapeCount] AS PapeCount";
        }
        //chen update 20140624 end


        //get Log information 
        strSql = "SELECT ID,Time,UserID,UserName,LoginName,GroupID,GroupName,SerialNumber,MFPModel,MFPIPAddress,Duplex ,JobID"
             + ",[FunctionID] ,[FileName]" + Environment.NewLine
             + ",(SELECT   JobNameDisp FROM  JobTypeInformation" + Environment.NewLine
             + "  WHERE ID = LogInformation.JobID) AS JobType"

             + ",(SELECT FunctionNameDisp  FROM  FunctionTypeInformation" + Environment.NewLine
             + "  WHERE JobID = LogInformation.JobID" + Environment.NewLine
             + "  AND FunctionID = LogInformation.FunctionID) AS ColorMode"            

               // 2011.03.22 Update By SES zhoumiao Ver.1.1 Update ST
           //  + ",(SELECT PaperSize  FROM  PaperSizeInformation" + Environment.NewLine
            // + "  WHERE ID = LogInformation.PageID) AS PaperSize"
             + ",(SELECT PaperName  FROM  PaperSizeInformation" + Environment.NewLine
             + "  WHERE ID = LogInformation.PageID) AS PaperName"
            // 2011.03.22 Update By SES zhoumiao Ver.1.1 Update ED

            //chen 20140509 update st
             //+ ",[Number] ,[Status]"
             //+ ",[Number]"
             + selSql
             //+ ",[PapeCount]"
             + selSql2
             + ",[CopyCount]"
             + ",[SpendMoney]"
             + ",[Status]"
            //chen 20140509 update ed
             + ",(SELECT ErrorInfo  FROM  OSAErrorInformation" + Environment.NewLine
             + "  WHERE ErrorCode = LogInformation.ErrorCode) AS ErrorInfo"
             + "  FROM [LogInformation]"
             + "Where ID IN ({0})";

        // 3.2 Get Date
        // Detail Date
        DataTable LogViewtable = ExecuteDataTable(string.Format(strSql, sqlId));

        // 3.3 Detail
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
                row.CssClass = UtilConst.CSS_ITEM_ODD;
            }

            TableCell tCell = new TableCell();

            //Process Time  
            String ProcessTime = GetDetailLogValueFromTable(LogViewtable, item, "Time");
            tCell.Text = ProcessTime;
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //User Name
            tCell = new TableCell();
            String UserName = GetDetailLogValueFromTable(LogViewtable, item, "UserName");
            tCell.Text = UserName;
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //Group Name
            tCell = new TableCell();
            String GroupName = GetDetailLogValueFromTable(LogViewtable, item, "GroupName");
            tCell.Text = GroupName;
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //MFP Model Name
            tCell = new TableCell();
            tCell.Text = GetDetailLogValueFromTable(LogViewtable, item, "MFPModel");
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //MFP Serial Number
            tCell = new TableCell();
            tCell.Text = GetDetailLogValueFromTable(LogViewtable, item, "SerialNumber");
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //MFP IP Address
            tCell = new TableCell();
            tCell.Text = GetDetailLogValueFromTable(LogViewtable, item, "MFPIPAddress");
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //Job Type
            String JobID = GetDetailLogValueFromTable(LogViewtable, item, "JobID");
            String FunctionID = GetDetailLogValueFromTable(LogViewtable, item, "FunctionID");
            tCell = new TableCell();
            if (("8".Equals(JobID)) && ("2".Equals(FunctionID)))
            {

                tCell.Text = UtilConst.ITEM_TITLE_FaxC2;
            }
            else
            {
                String JobType = GetDetailLogValueFromTable(LogViewtable, item, "JobType");
                if (string.IsNullOrEmpty(JobType))
                {
                    tCell.Text = "未知";
                }
                else
                {
                    tCell.Text = JobType;
                }
            }
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //File Name
            tCell = new TableCell();
            String FileName = GetDetailLogValueFromTable(LogViewtable, item, "FileName");
            tCell.Text = IsStringNull(FileName);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //Color Mode
            tCell = new TableCell();
            if (("8".Equals(JobID)) && ("2".Equals(FunctionID)))
            {
                tCell.Text = UtilConst.COLOR_BW;
            }
            else
            {
                String ColorMode = GetDetailLogValueFromTable(LogViewtable, item, "ColorMode");
                tCell.Text = IsStringNull(ColorMode);
            }
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);
            //Page Size
            tCell = new TableCell();
            // 2011.03.22 Update By SES zhoumiao Ver.1.1 Update ST
           // String PaperSize = GetDetailLogValueFromTable(LogViewtable, item, "PaperSize");
            String PaperSize = GetDetailLogValueFromTable(LogViewtable, item, "PaperName");
            // 2011.03.22 Update By SES zhoumiao Ver.1.1 Update ED
            tCell.Text = IsStringNull(PaperSize);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);
 
            //Page Count
            String PageCount = GetDetailLogValueFromTable(LogViewtable, item, "Number");
            tCell = new TableCell();

            if (string.IsNullOrEmpty(PageCount))
            {
                tCell.Text = UtilConst.JOB_UNKNOWN;
                tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            }
            else
            {
                tCell.Text = UtilCommon.IntToMoney(PageCount);
                tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            }
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //chen 20140516 add st

            PageCount = GetDetailLogValueFromTable(LogViewtable, item, "PapeCount");
            tCell = new TableCell();

            if (string.IsNullOrEmpty(PageCount))
            {
                tCell.Text = UtilConst.JOB_UNKNOWN;
                tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            }
            else
            {
                tCell.Text = UtilCommon.IntToMoney(PageCount);
                tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            }
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //copy count
            string CopyCount = GetDetailLogValueFromTable(LogViewtable, item, "CopyCount");
            tCell = new TableCell();

            if (string.IsNullOrEmpty(CopyCount))
            {
                tCell.Text = UtilConst.JOB_UNKNOWN;
                tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            }
            else
            {
                tCell.Text = UtilCommon.IntToMoney(CopyCount);
                tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            }
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            //spendmoney
            decimal SpendMoney = GetDetailDecimalValueFromTable(LogViewtable, item, "SpendMoney");
            tCell = new TableCell();
            //tCell.Text = UtilCommon.decimalToMoney(SpendMoney);
            tCell.Text = UtilCommon.decimalToMoney(SpendMoney, Dsp_Count_mode);
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);
            //chen 20140516 ed


            //Duplex Setting
            tCell = new TableCell();
            String Duplex = GetDetailLogValueFromTable(LogViewtable, item, "Duplex");
            switch (Duplex)
            {
                case "1":
                    tCell.Text = UtilConst.JOB_DUPLEX_1SIDED;
                    break;
                case "2":
                    tCell.Text = UtilConst.JOB_DUPLEX_2SIDED;
                    break;
                case "3":
                    tCell.Text = UtilConst.JOB_DUPLEX_2SIDED_BOOKLET;
                    break;
                case "4":
                    tCell.Text = UtilConst.JOB_DUPLEX_2SIDED_TABLET;
                    break;
                case "5":
                    tCell.Text = UtilConst.JOB_DUPLEX_PAMPHLET;
                    break;
                default:
                    tCell.Text = UtilConst.JOB_UNKNOWN;
                    break;
            }

            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);
            //Status
            tCell = new TableCell();
            String Status = GetDetailLogValueFromTable(LogViewtable, item, "Status");
            switch (Status)
            {
                case UtilConst.JOB_STATUS_CREATE:
                    tCell.Text = UtilConst.JOB_STATUS_CREATENAME;
                    break;
                case UtilConst.JOB_STATUS_CANCELED:
                    tCell.Text = UtilConst.JOB_STATUS_CANCELEDNAME;
                    tCell.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case UtilConst.JOB_STATUS_SUSPENDED:
                    tCell.Text = UtilConst.JOB_STATUS_SUSPENDEDNAME;
                    tCell.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case UtilConst.JOB_STATUS_FINISHED:
                    tCell.Text = UtilConst.JOB_STATUS_FINISHEDNAME;
                    break;
                case UtilConst.JOB_STATUS_ERROR:
                    tCell.Text = UtilConst.JOB_STATUS_ERRORNAME;
                    tCell.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                default:
                    tCell.Text = UtilConst.JOB_UNKNOWN;
                    break;
            }
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);
            //Error Information
            tCell = new TableCell();
            String ErrorInformation = GetDetailLogValueFromTable(LogViewtable, item, "ErrorInfo");
            tCell.Text = IsStringNull(ErrorInformation);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);


            tblDetail.Rows.Add(row);
        }

        this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, "1700px");

    }

    #endregion     

    #region "btnCSV_Click"
    /// <summary>
    /// btnCSV_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.17</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void btnCSVOutPut_Click(object sender, EventArgs e)
    {       
        OutPutCsvFile("LogViewReport", tblDetail);
    }
    #endregion

    #region "Get CSV OutPut Date" 
    /// <summary>
    /// Get CSV OutPut Date
    /// </summary>
    /// <param name="detail"></param>
    /// <returns></returns>
    /// <Date>2010.12.17</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected List<List<string>> GetCsvDate(Table detail)
    {
        List<List<string>> CsvList = new List<List<string>>();
        // Tbale Cell 
        TableCell cell;
        List<string> strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_PAGE_AVAILREPORT);
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_TIME_PERIOD);
        strHeadList.Add(this.Master.tbcTargetPeriod_text().Text);
        CsvList.Add(strHeadList);
        // Get Date
        for (int i = 0; i < detail.Rows.Count; i++)
        {            
            List<string> strList = new List<string>();
            // Table Row
            TableRow row = detail.Rows[i];
            // Get Header Date
            //  Get Big Header Date.
            if (i == 0)
            {
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    // Tbale Cell 
                    cell = row.Cells[j];
                    strList.Add(cell.Text);                    
                }                
            }
            else
            {
                foreach ( TableCell detailcell in row.Cells ) {
                    strList.Add(detailcell.Text.Replace(",","，"));                    

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
    /// <Date>2010.12.17</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    private void OutPutCsvFile(string filename, Table detail)
    {
        // 1.Get Date
        List<List<string>> CsvList = GetCsvDate(detail);

        // 2.To Csv Date.
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach ( List<string> item in CsvList ) {
            string strOutPut = null;
            foreach ( string strItem in item ) {
                if (strOutPut == null)
                {
                    strOutPut = strItem;
                }
                else
                {
                    strOutPut = strOutPut  + "," + strItem;
                }
            }
            strOutPut = strOutPut + "\r\n";
            sb.Append(strOutPut);
        }

        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename  + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".csv");

        Response.ContentType = "application/text";
        
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        
        Response.Write(sb);
        
        Response.End(); 

    }
    #endregion

    #region "Function:Is String Null"
    /// <summary>
    /// Function:Is String Null
    /// </summary>
    /// <param name="strDate"></param>
    /// <returns></returns>
    /// <Date>2010.12.17</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    private string IsStringNull(string strDate)
    {
        if (String.IsNullOrEmpty(strDate.Trim()))
        {
            return "-";
        }
        return strDate;

    }
    #endregion

    #region "GetDetailLogValueFromTable"
    /// <summary>
    /// GetDetailLogValueFromTable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="Id"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    /// <Date>2011.1.10</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    private String GetDetailLogValueFromTable(DataTable dt, string Id, string strColumnName)
    {
        string strSql = "ID = {0}";
        DataRow[] row = dt.Select(string.Format(strSql, Id));
        if (row == null || row.Length == 0)
        {
            return "";
        }
        else
        {
            if ("Time".Equals(strColumnName))
            {
                return Convert.ToDateTime(row[0][strColumnName]).ToString("yyyy/MM/dd HH:mm:ss");
            }
            else
            {
                return row[0][strColumnName].ToString();
            }
        }

    }
    #endregion

    #region "GetDetailIntValueFromTable"
    /// <summary>
    /// GetDetailIntValueFromTable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="id"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    /// <Date>2014.05.09</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    private int GetDetailIntValueFromTable(DataTable dt, string Id, string strColumnName)
    {
        string strSql = "ID = {0}";
        DataRow[] row = dt.Select(string.Format(strSql, Id));
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

    #region "GetDetailDecimalValueFromTable"
    /// <summary>
    /// GetDetailDecimalValueFromTable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="id"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    /// <Date>2014.05.09</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    private decimal GetDetailDecimalValueFromTable(DataTable dt, string Id, string strColumnName)
    {
        string strSql = "ID = {0}";
        DataRow[] row = dt.Select(string.Format(strSql, Id));
        if (row == null || row.Length == 0)
        {
            return (0);
        }
        else
        {
            if (row[0][strColumnName] == null || row[0][strColumnName].ToString().Equals(""))
            {
                return (0);
            }
            return decimal.Parse(row[0][strColumnName].ToString());
        }
    }
    #endregion
}
