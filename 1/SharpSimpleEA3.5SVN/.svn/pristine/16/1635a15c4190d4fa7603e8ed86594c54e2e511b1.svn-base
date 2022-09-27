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
using System.Web.Configuration;
using dtSettingManagementTableAdapters;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Osa.MfpWebService;
using System.Xml;
using dtUserInfoTableAdapters;
using dtPriceDetailTableAdapters;
using dtPriceMasterTableAdapters;
using dtPaperSizeInformationTableAdapters;
using System.Diagnostics;
using DAL;
using Model;
/// <summary>
/// UtilCommon
/// </summary>
public class UtilCommon
{
    public static Dictionary<string, Dictionary<string, string>> dicWebServerJob = new Dictionary<string, Dictionary<string, string>>();

    #region "ConnectionStrings For SimpleEA"
    /// <summary>
    /// ConnectionStrings For SimpleEA
    /// </summary>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static String DBConnectionStrings
    {
        get { return ConfigurationManager.ConnectionStrings["SimpleEAConnectionString"].ConnectionString; }
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
        // 2010.11.16 Add By SES Jijianxiong Ver.1.1 Update ST
        // Add null
        if (string.IsNullOrEmpty(strInput))
        {
            return "NULL";
        }
        // 2010.11.16 Add By SES Jijianxiong Ver.1.1 Update ED
        strInput = strInput.Trim().Replace("'", "''");
        strInput = "'" + strInput + "'";
        return strInput;
    }
    #endregion
    //chen 20140429 add start
    #region "SQL CONVERT(INT)"
    /// <summary>
    /// SQL CONVERT(decimal)
    /// </summary>
    /// <param name="Input"></param>
    /// <returns></returns>
    /// <Date>2014.04.29</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public static string ConvertDecimalToSQL(string Input)
    {
        if (!string.IsNullOrEmpty(Input))
        {
            return ((IConvertible)Input).ToDecimal(null).ToString();
        }
        return "NULL";
    }
    #endregion
    //chen 20140429 add end
    #region "SQL CONVERT(INT)"
    /// <summary>
    /// SQL CONVERT(INT)
    /// </summary>
    /// <param name="Input"></param>
    /// <returns></returns>
    /// <Date>2010.06.14</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static string ConvertIntToSQL(string Input)
    {
        if (!string.IsNullOrEmpty(Input))
        {
            return ((IConvertible)Input).ToInt32(null).ToString();
        }
        return "NULL";
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
    public static string ConvertDateToSQL(DateTime datInput)
    {
        string strOutPut = datInput.ToString("yyyy-MM-dd HH:mm:ss");
        strOutPut = "'" + strOutPut + "'";
        return strOutPut;
    }
    #endregion

    //chen add 20140429 for 当月时间 开始日 与 结尾日 start
   
    #region "Get start end Time By YYYYMM"
    /// <summary>
    /// Get Period Time By PeriodTime
    /// </summary>
    /// <param name="nowtime"></param>
    /// <param name="StartTime"></param>
    /// <param name="EndTime"></param>
    /// <Date>2014.04.29</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public static void GetSTartEndTimeBy(DateTime nowTime, out DateTime StartTime, out DateTime EndTime)
    {
        int year = nowTime.Year;
        int month = nowTime.Month;
        int[] day = new int[12];
        day[0] = 31;
        day[1] = 28;
        day[2] = 31;
        day[3] = 30;
        day[4] = 31;
        day[5] = 30;
        day[6] = 31;
        day[7] = 31;
        day[8] = 30;
        day[9] = 31;
        day[10] = 30;
        day[11] = 31;

        if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
        {
            day[1] = 29;
        }
        StartTime = new DateTime(
                    year,
                    month,
                    1, 0, 0, 0);
        EndTime = new DateTime(
                    year,
                    month,
                    day[month - 1], 23, 59, 59);
    }
    #endregion
    //chen add 20140429 for 当月时间 开始日 与 结尾日 end
    #region "Get Period Time By PeriodTime"
    /// <summary>
    /// Get Period Time By PeriodTime
    /// </summary>
    /// <param name="periodtime"></param>
    /// <param name="StartPeriod"></param>
    /// <param name="EndPeriod"></param>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static void GetPeriodBy(DateTime PeriodTime, out DateTime StartPeriod, out DateTime EndPeriod)
    {
        // SetPeriod
        int intSetPeriod = UtilConst.SETPERIOD_MONTH;
        // SetPeriodTime
        int intSetPeriodTime = UtilConst.SETPERIODTIME_MONTH_ITMELAST;
        // SetTime
        int intSetTime = 24;

        // End Period
        int EndPeriodHour = 0;

        EndPeriod = DateTime.Now;

        // Get Setting Information from settingmanagement.
        SettingManagementTableAdapter settingAdapter = new SettingManagementTableAdapter();
        dtSettingManagement.SettingManagementDataTable settingtableset = new dtSettingManagement.SettingManagementDataTable();
        settingtableset = settingAdapter.GetData();

        if (settingtableset.Count > 0)
        {
            intSetPeriod = settingtableset[0].SetPeriod;
            intSetPeriodTime = settingtableset[0].SetPeriodTime;
            intSetTime = settingtableset[0].SetTime;
        }

        // 1.Set Hour
        // End Period Hour
        EndPeriodHour = intSetTime - 1;
        if (EndPeriodHour == -1)
        {
            EndPeriodHour = 0;
        }

        // 2.Set Day and month and Year
        // SetPeriod

        if (UtilConst.SETPERIOD_MONTH.Equals(intSetPeriod))
        {
            // SetPeriod is 1:Every month
            // Set Day
            EndPeriod = GetSetPeriodByMonth(PeriodTime, intSetPeriodTime, intSetTime);

            EndPeriod = new DateTime(EndPeriod.Year, EndPeriod.Month, EndPeriod.Day, EndPeriodHour, 59, 59);

            // Get Start Period
            StartPeriod = EndPeriod.AddMonths(-1).AddSeconds(1);
            if (intSetPeriodTime.Equals(UtilConst.SETPERIODTIME_MONTH_ITMELAST))
            {
                // 2010.12.20 Delete By SES Jijianxiong Ver.1.1 Update ST
                // ReDo
                //// 2010.11.16 Update By SES Jijianxiong Ver 1.1 Update ST
                //////StartPeriod = new DateTime(EndPeriod.Year, EndPeriod.Month,
                //////    1,
                //////    StartPeriod.Hour, StartPeriod.Minute, StartPeriod.Second);
                ////StartPeriod = StartPeriod;
                //StartPeriod = new DateTime(EndPeriod.Year, EndPeriod.Month,
                //    1,
                //    StartPeriod.Hour, StartPeriod.Minute, StartPeriod.Second);
                //// 2010.11.16 Update By SES Jijianxiong Ver 1.1 Update ED
                ;
                // 2010.12.20 Delete By SES Jijianxiong Ver.1.1 Update ED
            }
            else
            {
                StartPeriod = new DateTime(StartPeriod.Year, StartPeriod.Month,
                    EndPeriod.Day,
                    StartPeriod.Hour, StartPeriod.Minute, StartPeriod.Second);
            }


        }
        else if (UtilConst.SETPERIOD_WEEK.Equals(intSetPeriod))
        {
            // SetPeriod is 2:Every week
            EndPeriod = GetSetPeriodByWeek(PeriodTime, intSetPeriodTime, intSetTime);

            EndPeriod = new DateTime(EndPeriod.Year, EndPeriod.Month, EndPeriod.Day, EndPeriodHour, 59, 59);
            StartPeriod = EndPeriod.AddDays(-7).AddSeconds(1);
        }
        //chen 20140429 for 时间无限 start
        else if (UtilConst.SETPERIOD_UNLIMIT.Equals(intSetPeriod))
        {
            StartPeriod = new DateTime( 2013, 
                                        1,
                                        1,
                                        0, 
                                        0, 
                                        0);
            EndPeriod = new DateTime(2500,
                                        12,
                                        31,
                                        23,
                                        59,
                                        59); 
        }
        //chen 20140429 for 时间无限 start
        else
        {
            // SetPeriod is 3:Everyday
            if (PeriodTime.Hour > intSetTime ||
                (PeriodTime.Hour == intSetTime &&
                    (PeriodTime.Minute >= 0 || PeriodTime.Second >= 0)
                )
               )
            {
                EndPeriod = new DateTime(PeriodTime.AddDays(1).Year, PeriodTime.AddDays(1).Month,
                    PeriodTime.AddDays(1).Day, EndPeriodHour, 59, 59);
            }
            else
            {
                EndPeriod = new DateTime(PeriodTime.Year, PeriodTime.Month, PeriodTime.Day, EndPeriodHour, 59, 59);
            }

            StartPeriod = EndPeriod.AddDays(-1).AddSeconds(1);
        }
    }
    #endregion

    #region "Get SetPeriod In Case SetPeriod is 1:Every month."
    /// <summary>
    /// Get SetPeriod In Case SetPeriod is 1:Every month.
    /// </summary>
    /// <param name="PeriodTime"></param>
    /// <param name="intSetPeriodtime"></param>
    /// <param name="intSetTime"></param>
    /// <returns></returns>
    /// <Date>2010.06.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected static DateTime GetSetPeriodByMonth(DateTime PeriodTime, int intSetPeriodtime, int intSetTime)
    {
        int EndPeriodDay = 1;
        // Case SetPeriod is 1:Every month
        //     1:5, 2:10, 3:15, 4:20, 5:25, 6:last
        if (UtilConst.SETPERIODTIME_MONTH_ITME5.Equals(intSetPeriodtime))
        {
            EndPeriodDay = 5;
        }
        else if (UtilConst.SETPERIODTIME_MONTH_ITME10.Equals(intSetPeriodtime))
        {
            EndPeriodDay = 10;
        }
        else if (UtilConst.SETPERIODTIME_MONTH_ITME15.Equals(intSetPeriodtime))
        {
            EndPeriodDay = 15;
        }
        else if (UtilConst.SETPERIODTIME_MONTH_ITME20.Equals(intSetPeriodtime))
        {
            EndPeriodDay = 20;
        }
        else if (UtilConst.SETPERIODTIME_MONTH_ITME25.Equals(intSetPeriodtime))
        {
            EndPeriodDay = 25;
        }
        else if (UtilConst.SETPERIODTIME_MONTH_ITMELAST.Equals(intSetPeriodtime))
        {
            EndPeriodDay = DateTime.DaysInMonth(PeriodTime.Year, PeriodTime.Month);
        }

        // If PeriodTime.Day > intSetPeriodTime then
        // the end of Period will be in next month of PeriodTime's month.
        if (PeriodTime.Day > EndPeriodDay || (PeriodTime.Day == EndPeriodDay && PeriodTime.Hour >= intSetTime))
        {
            // EndPeriodDay is over the days in month
            int intDaysInMonth = DateTime.DaysInMonth(PeriodTime.AddMonths(1).Year, PeriodTime.AddMonths(1).Month);
            if (EndPeriodDay > intDaysInMonth)
            {
                EndPeriodDay = intDaysInMonth;
            }

            // Path : 
            if (UtilConst.SETPERIODTIME_MONTH_ITMELAST.Equals(intSetPeriodtime))
            {
                EndPeriodDay = DateTime.DaysInMonth(PeriodTime.AddMonths(1).Year, PeriodTime.AddMonths(1).Month);
            }
            return new DateTime(PeriodTime.AddMonths(1).Year, PeriodTime.AddMonths(1).Month, EndPeriodDay);
        }
        else
        {
            return new DateTime(PeriodTime.Year, PeriodTime.Month, EndPeriodDay);
        }



    }
    #endregion

    #region "Get SetPeriod In Case SetPeriod is 2:Every week."
    /// <summary>
    /// Get SetPeriod In Case SetPeriod is 2:Every week.
    /// </summary>
    /// <param name="PeriodTime"></param>
    /// <param name="intSetPeriodtime"></param>
    /// <param name="intSetTime"></param>
    /// <returns></returns>
    /// <Date>2010.06.23</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected static DateTime GetSetPeriodByWeek(DateTime PeriodTime, int intSetPeriodtime, int intSetTime)
    {
        // Day Of Week to Int.
        int intWeek = 0;
        if (DayOfWeek.Monday.Equals(PeriodTime.DayOfWeek))
        {
            intWeek = 1;
        }
        else if (DayOfWeek.Tuesday.Equals(PeriodTime.DayOfWeek))
        {
            intWeek = 2;
        }
        else if (DayOfWeek.Wednesday.Equals(PeriodTime.DayOfWeek))
        {
            intWeek = 3;
        }
        else if (DayOfWeek.Thursday.Equals(PeriodTime.DayOfWeek))
        {
            intWeek = 4;
        }
        else if (DayOfWeek.Friday.Equals(PeriodTime.DayOfWeek))
        {
            intWeek = 5;
        }
        else if (DayOfWeek.Saturday.Equals(PeriodTime.DayOfWeek))
        {
            intWeek = 6;
        }
        else if (DayOfWeek.Sunday.Equals(PeriodTime.DayOfWeek))
        {
            intWeek = 7;
        }

        //About PeriodTime's Week , Get The End of PeriodTime.
        //  The End of PeriodTime　＝　Get The Monday Of PeriodTime's Week + the SetPeriodtime.
        int intThisWeek = intSetPeriodtime - intWeek;
        // The End of PeriodTime > PeriodTime.
        if (intThisWeek < 0 || (intThisWeek == 0 && PeriodTime.Hour >= intSetTime))
        {
            // The PeriodTime is over the End of PeriodTime in PeriodTime's Week.
            return PeriodTime.AddDays(intThisWeek + 7);
        }
        else
        {
            // The PeriodTime is less the End of PeriodTime in PeriodTime's Week.
            return PeriodTime.AddDays(intThisWeek);
        }
    }

    #endregion

    #region "To Money"
    /// <summary>
    /// UtilCommon.IntToMoney
    /// </summary>
    /// <param name="intput"></param>
    /// <returns></returns>
    /// <Date>2010.06.29</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static string IntToMoney(int intput)
    {
        return string.Format("{0:N0}", intput);
    }
    #endregion

    #region "To Money"
    /// <summary>
    /// UtilCommon.decimalToMoney
    /// </summary>
    /// <param name="intput"></param>
    /// <returns></returns>
    /// <Date>2014.04.29</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public static string decimalToMoney(decimal intput)
    {
        return string.Format(UtilConst.CON_MONEY_FORMAT, intput);
    }
    #endregion
    #region "To Money"
    /// <summary>
    /// UtilCommon.decimalToMoney
    /// </summary>
    /// <param name="intput"></param>
    /// <returns></returns>
    /// <Date>2014.04.29</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public static string decimalToMoney(decimal intput, int countMode)
    {
        if (countMode == UtilConst.CON_COUNT_MODE_MONEY)
        {
            return string.Format(UtilConst.CON_MONEY_FORMAT, intput);
        }
        else
        {
            return string.Format(UtilConst.CON_PAPER_FORMAT, intput);
        }
    }

    public static string doubleToMoney(double intput, int countMode)
    {
        if (countMode == UtilConst.CON_COUNT_MODE_MONEY)
        {
            return string.Format(UtilConst.CON_MONEY_D_FORMAT, intput);
        }
        else
        {
            return string.Format(UtilConst.CON_PAPER_D_FORMAT, intput);
        }
    }

    #endregion


    #region "To Money"
    /// <summary>
    /// UtilCommon.IntToMoney
    /// </summary>
    /// <param name="intput"></param>
    /// <returns></returns>
    /// <Date>2010.06.29</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static string IntToMoney(string intput)
    {
        return string.Format("{0:N0}", int.Parse(intput));
    }
    /// <summary>
    /// UtilCommon.Float ToMoney
    /// </summary>
    /// <param name="intput"></param>
    /// <returns></returns>
    /// <Date>2014.4.16</Date>
    /// <Author>pupeng</Author>
    /// <Version>0.01</Version>
    public static string decimalToMoney(string intput)
    {
        return string.Format("{0:F2}", float.Parse(intput));
     //   Convert.ToDecimal(intput)
    }
    #endregion

    #region "To Money"
    /// <summary>
    /// UtilCommon.decimalToMoney
    /// </summary>
    /// <param name="intput"></param>
    /// <returns></returns>
    /// <Date>2014.04.29</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public static string toMoney(string intput, int countMode)
    {
        if (countMode == UtilConst.CON_COUNT_MODE_MONEY)
        {
            return string.Format("{0:C2}", intput);
        }
        else
        {
            return string.Format("{0:N0}", intput);
        }
    }
    #endregion

    #region "Get Limit Information"
    /// <summary>
    /// Get Limit Information
    /// </summary>
    /// <param name="row"></param>
    /// <param name="intUsedNum"></param>
    /// <returns></returns>
    /// <Date>2010.07.14</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private static string GetLimitNum(dtRestrictionInformation.RestrictionInformationRow row, int intUsedNum)
    {
        // 2010.12.16 Update By SES Jijianxiong Ver.1.1 Update ST
        // Cost Solution
        //if (row == null)
        //{
        //    // 2010.09.26 Update By SES.JiJianXiong ST
        //    // return -1;
        //    return "";
        //    // 2010.09.26 Update By SES.JiJianXiong ED
        //}
        //if (row.Status.Equals(UtilConst.STATUS_UNLIMITED))
        //{
        //    // 2010.09.26 Update By SES.JiJianXiong ST
        //    // return -1;
        //    return "";
        //    // 2010.09.26 Update By SES.JiJianXiong ED
        //}
        //else if (row.Status.Equals(UtilConst.STATUS_PROHIBITION))
        //{
        //    // 2010.09.26 Update By SES.JiJianXiong ST
        //    // return 0;
        //    return "0";
        //    // 2010.09.26 Update By SES.JiJianXiong ED
        //}
        //else
        //{
        //    int reVal = int.Parse(row.LimitNum) - intUsedNum;

        //    // 2010.09.26 Update By SES.JiJianXiong ST
        //    // return reVal;
        //    return reVal.ToString();
        //    // 2010.09.26 Update By SES.JiJianXiong ED
        //}
        int intLimitNum = 0;

        if (row == null)
        {
            return "";
        }
        if (row.Status.Equals(UtilConst.STATUS_UNLIMITED))
        {
            return "";
        }
        else if (row.Status.Equals(UtilConst.STATUS_PROHIBITION))
        {
            //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ST
            //intLimitNum = 0;
            return UtilConst.STATUS_PROHIBITION_NAME;
            //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ED 
            
        }
        else
        {
            intLimitNum = int.Parse(row.LimitNum);
        }

        int reVal = intLimitNum - intUsedNum;
        return reVal.ToString();
        // 2010.12.16 Update By SES Jijianxiong Ver.1.1 Update ED
    }

    /// <summary>
    /// Get Limit Information
    /// </summary>
    /// <param name="row"></param>
    /// <param name="intUsedNum"></param>
    /// <returns></returns>
    /// <Date>2010.07.14</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static string GetLimitInfo(dtRestrictionInformation.RestrictionInformationRow row, int intUsedNum)
    {

        // 2010.09.26 Update By SES.JiJianXiong ST
        // int intReVal = GetLimitNum(row, intUsedNum);
        string strReVal = GetLimitNum(row, intUsedNum);

        //if (intReVal == -1)
        //{
        //    return UtilConst.STATUS_UNLIMITED_NAME;
        //}
        //else
        //{
        //    return UtilCommon.IntToMoney(intReVal);
        //}
        if (strReVal == "")
        {
            return UtilConst.STATUS_UNLIMITED_NAME;
        }
        else
        {
            //2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
            //return UtilCommon.IntToMoney(strReVal);
            return strReVal;
            //2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED
        }
        // 2010.09.26 Update By SES.JiJianXiong ED
    }
    #endregion

    #region "Get IC Card Flg"
    /// <summary>
    /// Get IC Card Flg
    /// </summary>
    /// <Date>2010.07.20</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static Boolean GetICCardFlg
    {
        get
        {
            string strICCardFlg = WebConfigurationManager.AppSettings[UtilConst.WEBCONFIG_ICCARD];
            // In web.config, If the Item for Ic Card Flg is not exist
            //     Or the value is "ture" means use IC Card In MFP.
            if (string.IsNullOrEmpty(strICCardFlg) || strICCardFlg.Equals("true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    #endregion

    //Added by Le Ning 2014-9-10
    #region "Get Extra Limit After Exceed Limit"
    /// <summary>
    /// Get Extra Limit After Exceed Limit
    /// </summary>
    /// <Date>2014.09.10</Date>
    /// <Author>SLC Le Ning</Author>
    /// <Version>0.01</Version>
    public static int GetExtraLimit
    {
        get
        {
            string strExtraLimit = WebConfigurationManager.AppSettings[UtilConst.WEBCONFIC_EXTRALIMIT];
            // In web.config, If the Item for Extra Limit is not exist
            //     Or the value is "ture" means use EXTRALIMIT In MFP.
            if (string.IsNullOrEmpty(strExtraLimit))
            {
                return UtilConst.WEBCONFIC_EXTRALIMIT_DEFULAT;
            }
            else
            {
                try
                {
                    return int.Parse(strExtraLimit);
                }
                catch (Exception)
                {
                    return UtilConst.WEBCONFIC_EXTRALIMIT_DEFULAT;
                }
            }
        }
    }
    #endregion

    #region "Get IC Card ID's Length"
    /// <summary>
    /// Get IC Card Flg
    /// </summary>
    /// <Date>2010.07.20</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static int GetICCardLen
    {
        get
        {
            string strICCardLen = WebConfigurationManager.AppSettings[UtilConst.WEBCONFIC_ICCARDLEN];
            // In web.config, If the Item for Ic Card Flg is not exist
            //     Or the value is "ture" means use IC Card In MFP.
            if (string.IsNullOrEmpty(strICCardLen))
            {
                return UtilConst.WEBCONFIC_ICCARDLEN_DEFULAT;
            }
            else
            {
                try
                {
                    return int.Parse(strICCardLen);
                }
                catch (Exception )
                {
                    return UtilConst.WEBCONFIC_ICCARDLEN_DEFULAT;
                }
            }
        }
    }
    #endregion

    #region "Get ICCardLogin flg"
    /// <summary>
    /// Get ICCardLogin flg
    /// </summary>
    /// <Date>2014.08.05</Date>
    /// <Author>SES Chen youguang</Author>
    /// <Version>2.0</Version>
    public static string GetICCardLoginFlg
    {
        get
        {
            string strICCardLogin = WebConfigurationManager.AppSettings[UtilConst.WEBCONFIG_ICCARD];
            // In web.config, If the Item for Ic Card Flg is not exist
            //     Or the value is "ture" means use IC Card In MFP.
            if (string.IsNullOrEmpty(strICCardLogin))
            {
                return UtilConst.WEBCONFIC_ICCARDLOGIN_DEFULAT;
            }
            else
            {
                return strICCardLogin;
            }

        }
    }
    #endregion

    #region "Get Normal Login Flg"
    /// <summary>
    /// Get Normal Login Flg
    /// </summary>
    /// <Date>2012.08.22</Date>
    /// <Author>SLC Wei Changye</Author>
    /// <Version>0.01</Version>
    public static Boolean GetNormalLoginFlg
    {
        get
        {
            string strNormalLoginFlg = WebConfigurationManager.AppSettings[UtilConst.WEBCONFIG_NORMAL_LOGIN];
            // In web.config, If the Item for Ic Card Flg is not exist
            //     Or the value is "ture" means use IC Card In MFP.
            if (string.IsNullOrEmpty(strNormalLoginFlg) || strNormalLoginFlg.Equals("true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    #endregion

    #region "MFPJob"
    /// <summary>
    /// MFPJob
    /// </summary>
    /// <Date>2010.07.20</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public struct MFPJob
    {
        private int intJobId;
        private Nullable<int> intFunctionId;

        public MFPJob(int _intJobId, Nullable<int> _intFunctionId)
        {
            intJobId = _intJobId;
            intFunctionId = _intFunctionId;
        }

        public int JOBId
        {
            get
            {
                return intJobId;
            }
        }


        public Nullable<int> FunctionId
        {
            get
            {
                return intFunctionId;
            }
        }
    }
    #endregion

    #region "Get RestrictionInformation by jobid and function id."
    /// <summary>
    /// Get RestrictionInformation by jobid and function id.
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <returns></returns>
    /// <Date>2010.07.23</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static Boolean GetResByJobAndFun(dtRestrictionInformation.RestrictionInformationDataTable dt, int JobId, int FunId)
    {
        string strSql = "JobID = {0} AND FunctionID = {1}";
        DataRow[] row = dt.Select(string.Format(strSql, JobId, FunId));
        if (row.Length > 0)
        {
            dtRestrictionInformation.RestrictionInformationRow detailrow = (dtRestrictionInformation.RestrictionInformationRow)row[0];
            if (detailrow.Status.Equals(UtilConst.STATUS_PROHIBITION))
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    //chen add 20140426 start
    #region "Get User restrictid  from DB"
    /// <summary>
    /// Get User res restrictid from DB
    /// </summary>
    /// <param name="restrictID"></param>
    /// <returns></returns>
    /// <Date>2014.04.26</Date>
    /// <Author>SES chen you guang</Author>
    /// <Version>0.01</Version>
    public static int  GetUserRestrictidFromDB(int userid)
    {
        dtUserInfoTableAdapters.UserInfoTableAdapter userAdapter = new UserInfoTableAdapter();
        dtUserInfo.UserInfoDataTable userinfoDT = userAdapter.GetDataByUserId(userid);
        if (userinfoDT.Count == 0)
        {
            return -1;
        }
        dtUserInfo.UserInfoRow row = userinfoDT[0] as dtUserInfo.UserInfoRow;
        int restrictID = row.RestrictionID;

        if (restrictID == -1)
        {
            int groupID = row.GroupID;
            dtGroupInfoTableAdapters.GroupInfoTableAdapter groupAdapter = new dtGroupInfoTableAdapters.GroupInfoTableAdapter();
            dtGroupInfo.GroupInfoDataTable groupinfoDT = groupAdapter.GetGroupInfoDataById(groupID);
            if( groupinfoDT.Count == 0 )
            {
                return -1;
            }
            dtGroupInfo.GroupInfoRow grouRow = groupinfoDT[0] as dtGroupInfo.GroupInfoRow;
            restrictID = Int32.Parse(grouRow.RestrictionID);
        }

        return restrictID;
    }
    #endregion

    #region "Get User info remain from DB"
    /// <summary>
    /// Get User info remain money from DB
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    /// <Date>2014.04.26</Date>
    /// <Author>SES chen you guang</Author>
    /// <Version>0.01</Version>
    public static dtUserInfo.UserInfoDataTable GetUserInfoFromDB(int userid)
    {
        dtUserInfoTableAdapters.UserInfoTableAdapter userAdapter = new UserInfoTableAdapter();
        dtUserInfo.UserInfoDataTable dt = userAdapter.GetDataByUserId(userid);
        return dt;
    }
    #endregion

    #region "Get User res info limits from DB"
    /// <summary>
    /// Get User res inf limits from DB
    /// </summary>
    /// <param name="restrictID"></param>
    /// <returns></returns>
    /// <Date>2014.04.26</Date>
    /// <Author>SES chen you guang</Author>
    /// <Version>0.01</Version>
    public static dtRestrictionInfo.RestrictionInfoDataTable GetUserResmoneyLimitsFromDB(int restrictID)
    {
        dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter resAdapter = new dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter();
        dtRestrictionInfo.RestrictionInfoDataTable dt = resAdapter.GetRestrictionInfoDataByID(restrictID);
        return dt;
    }
    #endregion
    #region "Get User pay detail from DB"
    /// <summary>
    /// Get User pay detail from DB
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    /// <Date>2014.04.28</Date>
    /// <Author>SES chen you guang</Author>
    /// <Version>0.01</Version>
    public static dtUserPayDetail.UserPayDetailDataTable GetUserPayDetailFromDB(int userid)
    {
        dtUserPayDetailTableAdapters.UserPayDetailTableAdapter payAdapter = new dtUserPayDetailTableAdapters.UserPayDetailTableAdapter();
        dtUserPayDetail.UserPayDetailDataTable dt = payAdapter.GetSumMoneyByUserID(userid);
        return dt;
    }
    #endregion

    #region "Get User limits from DB"
    /// <summary>
    /// Get User limits from DB
    /// </summary>
    /// <param name="restrictid"></param>
    /// <returns></returns>
    /// <Date>2014.04.26</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public static dtRestrictionInformation.RestrictionInformationDataTable GetUserResLimitsFromDB(int restrictid)
    {
        dtRestrictionInformationTableAdapters.RestrictionInformationTableAdapter resAdapter = new dtRestrictionInformationTableAdapters.RestrictionInformationTableAdapter();
        dtRestrictionInformation.RestrictionInformationDataTable dt = resAdapter.GetDataByResId(restrictid);
        return dt;
    }
    #endregion


    #region "Get User PayDetailT from DB."
    /// <summary>
    /// Get User Pay data Information from DB.
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    /// <Date>2014.05.7</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public static dtUserPayDetail.UserPayDetailDataTable GetUserPayDataFromDB(int userid)
    {
        dtUserPayDetailTableAdapters.UserPayDetailTableAdapter payAdapter = new  dtUserPayDetailTableAdapters.UserPayDetailTableAdapter();

        DateTime start_time;
        DateTime end_time;
        UtilCommon.GetPeriodBy(DateTime.Now, out start_time, out end_time);

        dtUserPayDetail.UserPayDetailDataTable dt = payAdapter.GetDataByUserID(userid, ConvertDateToSQL(start_time), ConvertDateToSQL(end_time));
        return dt;
    }
    #endregion
    
    //chen add 20140426 end
    #region "Get User limits from DB"
    /// <summary>
    /// Get User limits from DB
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    /// <Date>2010.07.23</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static dtRestrictionInformation.RestrictionInformationDataTable GetUserLimitsFromDB(int userid)
    {
        //20140606 chen update start
        //dtRestrictionInformationTableAdapters.RestrictionInformationTableAdapter resAdapter = new dtRestrictionInformationTableAdapters.RestrictionInformationTableAdapter();
        //dtRestrictionInformation.RestrictionInformationDataTable dt = resAdapter.GetDataByUserID(userid);
        int restrictid = UtilCommon.GetUserRestrictidFromDB(userid);
        dtRestrictionInformation.RestrictionInformationDataTable dt = UtilCommon.GetUserResLimitsFromDB(restrictid);
        return dt;
        //20140606 chen update end
    }
    #endregion

    #region "Get User Job Information from DB."
    /// <summary>
    /// Get User Job Information from DB.
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    /// <Date>2010.07.26</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static dtJobInformation.JobInformationDataTable GetUserJobInfoFromDB(int userid)
    {
        dtJobInformationTableAdapters.JobInformationTableAdapter jobAdapter = new dtJobInformationTableAdapters.JobInformationTableAdapter();

        DateTime start_time;
        DateTime end_time;
        UtilCommon.GetPeriodBy(DateTime.Now, out start_time, out end_time);

        dtJobInformation.JobInformationDataTable dt = jobAdapter.GetDataByUserID(userid, ConvertDateToSQL(start_time), ConvertDateToSQL(end_time));
        return dt;
    }
    #endregion
    //chen add 20140428 for Limit number base on money start
    //chen add 20140428 for Limit number base on money end
    #region "Get Restriction set limit number by jobid and function id."
    /// <summary>
    /// Get Restriction set limit number by jobid and function id.
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <returns></returns>
    /// <Date>2010.07.23</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static int GetResLimitNumByJobAndFun(dtRestrictionInformation.RestrictionInformationDataTable resdt,
        int JobId, int FunId)
    {
        if (JobId == -1)
        {
            return 0;
        }

        if (FunId == -1)
        {
            return 0;
        }
        string strSql = "JobID = {0} AND FunctionID = {1}";
        DataRow[] row = resdt.Select(string.Format(strSql, JobId, FunId));
        if (row.Length > 0)
        {
            dtRestrictionInformation.RestrictionInformationRow detailrow = (dtRestrictionInformation.RestrictionInformationRow)row[0];
            if ( detailrow.Status.Equals(UtilConst.STATUS_UNLIMITED) ){
                // UnLimited.
                return -1;
            }
            else if (detailrow.Status.Equals(UtilConst.STATUS_PROHIBITION))
            {
                return 0;
            }
            else
            {
                return int.Parse(detailrow.LimitNum);
            }
        }
        return 0;
    }
    #endregion


    #region "Get Job's Page sum by jobid and function id."
    /// <summary>
    /// Get Job's Page sum by jobid and function id.
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <returns></returns>
    /// <Date>2010.07.23</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static int GetJobPageNumByJobAndFun(dtJobInformation.JobInformationDataTable jobdt,
        int JobId, int FunId)
    {
        if (JobId == -1)
        {
            return 0;
        }

        if (FunId == -1)
        {
            return 0;
        }
        string strSql = "JobID = {0} AND FunctionID = {1}";
        DataRow[] row = jobdt.Select(string.Format(strSql, JobId, FunId));
        if (row.Length > 0)
        {
            dtJobInformation.JobInformationRow detailrow = (dtJobInformation.JobInformationRow)row[0];
            return detailrow.SumNumber;
        }
        return 0;
    }
    #endregion
    //chen add 20140426 for get job spend money start
    #region "Get Job's Spend money sum by jobid and function id."
    /// <summary>
    /// Get Job's spend money by jobid and function id.
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <returns></returns>
    /// <Date>2014.04.26</Date>
    /// <Author>SES chen you guang</Author>
    /// <Version>0.01</Version>
    public static decimal GetJobSpendMoneyFun(dtJobInformation.JobInformationDataTable jobdt, int FunId)
    {
        decimal sum = 0;
        if (FunId == -1)
        {
            return 0;
        }
        string strSql = "FunctionID = {0}";
        DataRow[] row = jobdt.Select(string.Format(strSql, FunId));
        for (int i = 0; i < row.Length;i++ )
           // if (row.Length > 0)
           {
                dtJobInformation.JobInformationRow detailrow = (dtJobInformation.JobInformationRow)row[i];
                sum+= (detailrow.IsSumMoneyNull() ? 0 : detailrow.SumMoney);
            }
        return sum;
    }
    #endregion


    #region "Get User's Limit Function"
    /// <summary>
    /// Get User's Limit num
    /// </summary>
    /// <param name="resdt"></param>
    /// <param name="jobdt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <returns></returns>
    /// <Date>2014.04.28</Date>
    /// <Author>SES chen youguang </Author>
    /// <Version>0.01</Version>
    public static int GetUserLimitStatus(
        dtRestrictionInformation.RestrictionInformationDataTable resdt,
        int JobId, int FunId)
    {

        if (JobId == -1)
        {
            return 0;
        }

        if (FunId == -1)
        {
            return 0;
        }

        // 1.Get User's Res's Limit Num.
        //获得用户的限制状态
        int limitStatue = GetResLimitStatusByJobAndFun(resdt, JobId, FunId);
        if (limitStatue.Equals(UtilConst.STATUS_UNLIMITED))
        {
            return 1;
        }
        else if (limitStatue.Equals(UtilConst.STATUS_PROHIBITION))
        {
            return 2;
        }
        else
        {
            return 0;
        }

    }
    #endregion

    #region "Get Restriction set limit number by jobid and function id."
    /// <summary>
    /// Get Restriction set limit status by jobid and function id.
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <returns></returns>
    /// <Date>2014.04.28</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public static int GetResLimitStatusByJobAndFun(
        dtRestrictionInformation.RestrictionInformationDataTable resdt,
        int JobId, int FunId)
    {
        if (JobId == -1)
        {
            return 0;
        }

        if (FunId == -1)
        {
            return 0;
        }
        string strSql = "JobID = {0} AND FunctionID = {1}";
        DataRow[] row = resdt.Select(string.Format(strSql, JobId, FunId));
        if (row.Length > 0)
        {
            dtRestrictionInformation.RestrictionInformationRow detailrow = (dtRestrictionInformation.RestrictionInformationRow)row[0];
            return detailrow.Status;
            //return UtilConst.STATUS_UNLIMITED;//允许使用
        }
        //else
        //{
        //    return UtilConst.STATUS_PROHIBITION;//禁止使用
        //}
        return 0;//不允许使用
    }
    #endregion
    //chen add 20140426 for get job spend money end

    #region "Get User's Limit num" 
    /// <summary>
    /// Get User's Limit num
    /// </summary>
    /// <param name="resdt"></param>
    /// <param name="jobdt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <returns></returns>
    /// <Date>2010.07.26</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static int GetUserLimitNum(dtRestrictionInformation.RestrictionInformationDataTable resdt,
        dtJobInformation.JobInformationDataTable jobdt,
        int JobId, int FunId)
    {
        int limitNum = 0;

        if (JobId == -1)
        {
            return 0;
        }

        if (FunId == -1)
        {
            return 0;
        }

        // 1.Get User's Res's Limit Num.
        //获得用户的限制数量
        int reslimitnum = GetResLimitNumByJobAndFun(resdt, JobId, FunId);
        
        // 2010.12.15 Delete By SES Jijianxiong Ver.1.1 Update ST
        // Cost Solution
        // Prohibition, although prohibition, in the borrow mode the job can be used.
        //if (reslimitnum == 0)
        //{
        //    limitNum = 0;
        //}
        // 2010.12.15 Delete By SES Jijianxiong Ver.1.1 Update ED

        //reslimitnum ：-1的时候，没有限制
        if (reslimitnum == -1)
        {
            limitNum = 10000;  
        }
        else
        {
            // 2.Get User's Job's Num
            //获得已经使用数量
            int jobnum = GetJobPageNumByJobAndFun(jobdt, JobId, FunId);

            //计算限制数量
            //限额 减去 已经使用的额度
            limitNum = reslimitnum - jobnum;

            // 2010.12.09 Delete By SES Jijianxiong ST
            // Cost Solution
            //if (limitNum < 0)
            //{
            //    limitNum = 0;
            //}
            // 2010.12.09 Delete By SES Jijianxiong ED
        }

        return limitNum;
    }


    /// <summary>
    /// Get User's Limit num
    /// </summary>
    /// <param name="userLimitTable"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <returns></returns>
    /// <Date>2010.07.26</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static int GetUserLimitNum(dtUserLimit.UserLimitDataTable userLimitTable, int JobId, int FunId)
    {
        int limitNum = 0;

        if (JobId == -1)
        {
            return 0;
        }

        if (FunId == -1)
        {
            return 0;
        }

        string strSql = "JobID = {0} AND FunctionID = {1}";
        DataRow[] row = userLimitTable.Select(string.Format(strSql, JobId, FunId));
        if (row.Length > 0)
        {
            dtUserLimit.UserLimitRow detailrow = (dtUserLimit.UserLimitRow)row[0];
            
            //Edited by Le Ning 2014-9-9;
            //limitNum = detailrow.LimitNum;　
            if (detailrow.LimitNum >= 0)
            {
                int ExtraLimit = UtilCommon.GetExtraLimit;
                limitNum = detailrow.LimitNum + ExtraLimit;    //ExtraLimit为防止最后一次余额不足时卡机时临时增加的补充额度,默认值为100；
            }
        }
        else
        {
            limitNum = 0;
        }


        return limitNum;
    }

    #endregion

    #region "Check Exist: MFP Information is In DB."
    /// <summary>
    /// Check Exist: MFP Information is In DB.
    /// </summary>
    /// <param name="SerialNumber"></param>
    /// <returns></returns>
    /// <Date>2010.07.26</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static Boolean IsMFPInfoExistInDB(string SerialNumber)
    {
        dtMFPInformationTableAdapters.MFPInformationTableAdapter adapter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter();
        dtMFPInformation.MFPInformationDataTable dt = adapter.GetDataBySerialNumber(SerialNumber);

        if (dt == null || dt.Rows.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion
    #region "Check Exist: MFP IP Address is In DB."
    /// <summary>
    /// Check Exist: MFP IP Address is In DB.
    /// </summary>
    /// <param name="IP"></param>
    /// <returns></returns>
    /// <Date>2014.05.13</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public static Boolean IsMFPIPExistInDB(string ip)
    {
        dtMFPInformationTableAdapters.MFPInformationTableAdapter adapter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter();
        dtMFPInformation.MFPInformationDataTable dt = adapter.GetDataByIPAddress(ip);

        if (dt == null || dt.Rows.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region "Update MFP Information."
    /// <summary>
    /// Update MFP Information.
    /// </summary>
    /// <param name="SerialNumber"></param>
    /// <param name="ModelName"></param>
    /// <param name="IPAddress"></param>
    /// <param name="Location"></param>
    /// <Date>2010.07.26</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static void UpdateMFPInfo(string SerialNumber, string ModelName, string IPAddress, string Location)
    {
        string sql = " UPDATE [MFPInformation]" +
                    "    SET [SerialNumber] = {0}" +
                    "       ,[ModelName] = {1}" +
                    "       ,[IPAddress] = {2}" +
                    //"       ,[Location] = {3}" +
                    //chen update 20140513 
                    //"  WHERE [SerialNumber] = {0};";
                    "       ,[Label] = '1'" +
                    "  WHERE [IPAddress] = {2};";
                    //end
        object[] args = new object[3];
        args[0] = ConvertStringToSQL(SerialNumber);
        args[1] = ConvertStringToSQL(ModelName);
        args[2] = ConvertStringToSQL(IPAddress);
        //args[3] = ConvertStringToSQL(Location);

        using (SqlConnection con = new SqlConnection( DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand(string.Format(sql, args), con, tran))
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
                throw ex;
            }
            finally
            {
                tran.Dispose();
                tran = null;
            }

        }

    }
    #endregion

    #region "Insert MFP Information."
    /// <summary>
    /// Insert MFP Information.
    /// </summary>
    /// <param name="SerialNumber"></param>
    /// <param name="ModelName"></param>
    /// <param name="IPAddress"></param>
    /// <param name="Location"></param>
    /// <Date>2010.07.26</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static void InsertMFPInfo(string SerialNumber, string ModelName, string IPAddress, string Location)
    {
        //string sql = " INSERT INTO [MFPInformation] " +
        //            "            ([SerialNumber] " +
        //            "            ,[ModelName] " +
        //            "            ,[IPAddress] " +
        //            "            ,[Location]) " +
        //            "            ,[Label]) " +
        //            "      VALUES " +
        //            "            ({0} " +
        //            "            ,{1} " +
        //            "            ,{2} " +
        //            "            ,{3} " +
        //            "            ,'1'); ";

        //object[] args = new object[4];
        string sql = "";
        sql = "   INSERT INTO [MFPInformation]          " + Environment.NewLine;
        sql += "             ([SerialNumber]                " + Environment.NewLine;
        sql += "             ,[ModelName]          " + Environment.NewLine;
        sql += "             ,[IPAddress]         " + Environment.NewLine;
        sql += "             ,[Location]          " + Environment.NewLine;
        sql += "             ,[AdministratorID]          " + Environment.NewLine;
        sql += "             ,[Password]          " + Environment.NewLine;
        sql += "             ,[PriceID])          " + Environment.NewLine;
        sql += "       VALUES                     " + Environment.NewLine;
        sql += "             ({0}                 " + Environment.NewLine;
        sql += "             ,{1}                 " + Environment.NewLine;
        sql += "             ,{2}                 " + Environment.NewLine;
        sql += "             ,{3}                 " + Environment.NewLine;
        sql += "             ,{4}                 " + Environment.NewLine;
        sql += "             ,{5}                 " + Environment.NewLine;
        sql += "             ,{6})                 " + Environment.NewLine;

        string[] args = new string[7];
        args[0] = ConvertStringToSQL(SerialNumber);
        args[1] = ConvertStringToSQL(ModelName);
        args[2] = ConvertStringToSQL(IPAddress);
        args[3] = ConvertStringToSQL(Location);
        args[4] = ConvertStringToSQL("admin");
        args[5] = ConvertStringToSQL("admin");
        args[6] = ConvertStringToSQL("0");

        using (SqlConnection con = new SqlConnection( DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand(string.Format(sql, args), con, tran))
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
                throw ex;
            }
            finally
            {
                tran.Dispose();
                tran = null;
            }

        }

    }
    #endregion

    //chen add for price 20140425 start
    #region "get priceid based on serience of mfp."
    public static int getMFPPriceID(string serialNumber)
    {
        int priceID = 0;
        dtMFPInformationTableAdapters.MFPInformationTableAdapter adapter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter();
        dtMFPInformation.MFPInformationDataTable dt = adapter.GetDataBySerialNumber(serialNumber);
        if (dt.Rows.Count == 0)
        {
            // The Page Type is unknow for Simple EA.
            priceID = UtilConst.CON_DEFAULT_PRICEID;
        }
        else
        {
            
            priceID = ((dtMFPInformation.MFPInformationRow)dt.Rows[0]).PriceID;
        }
        return priceID;
    }
    #endregion
    #region "get current pricedetailid based on serience of mfp."
    public static int getPriceDetailID(int priceID, int PapeTypeID, int JobID)
    {
        int priceDetailID = 0;
        dtPriceDetailTableAdapters.PriceDetailTableAdapter adapter = new PriceDetailTableAdapter();
        /*
        dtPriceDetail.PriceDetailDataTable dt = adapter.GetMaxPriceDetailID(priceID, PapeTypeID, JobID);
        if (dt.Rows.Count == 0)
        {
            priceDetailID = UtilConst.CON_DEFAULT_PRICEDETAILID;
        }
        else
        {
            priceDetailID = ((dtPriceDetail.PriceDetailRow)dt.Rows[0]).PriceDetailID;
        }*/
        priceDetailID = (int)adapter.getMaxPriceDetailIDValue(priceID, PapeTypeID, JobID);
        return priceDetailID;
    }
    #endregion
    #region "get papetypeid based on papeid."
    public static int getPapeTypeID(int paperID)
    {
        int priceTypeID = -1;
        dtPaperSizeInformationTableAdapters.PaperSizeInformationTableAdapter adapter = new PaperSizeInformationTableAdapter();
        dtPaperSizeInformation.PaperSizeInformationDataTable dt = adapter.GetDataByID(paperID);
        if (dt.Rows.Count != 0)
        {
            priceTypeID = ((dtPaperSizeInformation.PaperSizeInformationRow)dt.Rows[0]).PaperTypeID;
        }
        return priceTypeID;
    }
    public static int getPriceCalMode(int priceID)
    {
        int priceCalMode = 0;
        dtPriceMasterTableAdapters.PriceMasterTableAdapter adapter = new PriceMasterTableAdapter();
        dtPriceMaster.PriceMasterDataTable dt = adapter.GetDataByPriceID(priceID);
        if (dt.Rows.Count != 0)
        {
            priceCalMode = ((dtPriceMaster.PriceMasterRow)dt.Rows[0]).PriceCalMode;
        }


        return priceCalMode;
    }
    #endregion
    //chen add for price 20140425 end

    #region "Save Job Information into Simple's DB."
    /// <summary>
    /// Save Job Information into Simple's DB
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="mfpjob"></param>
    /// <param name="pageType"></param>
    /// <param name="pageNum"></param>
    /// <param name="serialNumber"></param>
    /// <param name="time"></param>
    /// <Date>2010.07.26</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static void SaveJobRecrod(int userid, MFPJob mfpjob, 
        string pageType, int pageNum, 
//chen 20140424 add start
        int papeCount,
        int copyCount,
        int Duplex,
//chen 20140424 add end
        string serialNumber)
    {
        // Get PageSize ID.
        int pageId = UtilConst.CON_DATE_OTHER_PAGE;
        if (pageType == "")
        {
            // Page is Unused.
            pageId = UtilConst.CON_DATE_UNUSED_PAGE;
        }
        else
        {
            dtPaperSizeInformationTableAdapters.PaperSizeInformationTableAdapter adapter = new dtPaperSizeInformationTableAdapters.PaperSizeInformationTableAdapter();
            dtPaperSizeInformation.PaperSizeInformationDataTable dt = adapter.GetDataByPageSize(pageType);
            if (dt.Rows.Count == 0)
            {
                // The Page Type is unknow for Simple EA.
                pageId = UtilConst.CON_DATE_OTHER_PAGE;
            }
            else
            {
                pageId = ((dtPaperSizeInformation.PaperSizeInformationRow)dt.Rows[0]).ID;
            }
        }

        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        int Dsp_A3_A4 = settingrow.Dis_A3_A4;
        int Dsp_Count_mode = settingrow.Dis_Count_mode;


        int tmpPapeID = pageId;
        if (tmpPapeID == UtilConst.CON_PAGE_A3) //A3
        {
            if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A4))
            {
                tmpPapeID = UtilConst.CON_PAGE_A4; //A4
            }
        }


        // Get GroupID
        dtUserInfoTableAdapters.UserInfoTableAdapter userAdapter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
        dtUserInfo.UserInfoDataTable userdt = userAdapter.GetDataByUserId(userid);
        // 2011.06.01 Update By SLC zhoumiao Ver.1.1  ST
        //int GroupId = ((dtUserInfo.UserInfoRow)userdt[0]).GroupID;
        int GroupId = -1;
        if (userdt.Rows.Count == 0)
        {
            GroupId = -1;
        }
        else
        {
            GroupId = ((dtUserInfo.UserInfoRow)userdt[0]).GroupID;
        }
        // 2011.06.01  Update By SLC zhoumiao Ver.1.1  ED

        //chen 20140424 add start
        int PaperTypeID;
        Decimal SpendMoney;
        int PriceDetailID = -1;
        int priceID;
        //int priceCalMode;
        //耗材价格
        Decimal colPrice = 0;
        Decimal paperPrice = 0;
        Decimal grayPrice = 0;
        Decimal colorPrice = 0;

        if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
        {

            //PaperTypeID = getPapeTypeID(pageId);
            PaperTypeID = getPapeTypeID(tmpPapeID);


            priceID = getMFPPriceID(serialNumber);
            //priceCalMode = getPriceCalMode(priceID);
            //Decimal paperPrice = 0;
            //Decimal grayPrice = 0;
            //Decimal colorPrice = 0;



            try
            {
                PriceDetailID = getPriceDetailID(priceID, PaperTypeID, mfpjob.JOBId);


                dtPriceDetailTableAdapters.PriceDetailTableAdapter pdAdapter = new PriceDetailTableAdapter();
                dtPriceDetail.PriceDetailDataTable pdDt = pdAdapter.GetDataByPriceDetailID(PriceDetailID);
                if (pdDt.Rows.Count != 0)
                {
                    paperPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).PaperPrice;
                    grayPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).GrayPrice;
                    colorPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).ColorPrice;
                }


            }
            catch (Exception e)
            {
                ;
            }


            if (mfpjob.FunctionId == 1)
            {
                colPrice = grayPrice;
            }
            else
            {
                colPrice = colorPrice;
            }
        }
        else
        {
            paperPrice = 0;
            grayPrice = 1;
            colorPrice = 1;
            colPrice = 1;
        }
        //if (priceCalMode == 0)
        //{
        //    //priceCalMode 0:  面数 * 单价
        //    SpendMoney = (Decimal)pageNum * paperPrice;
        //}
        //else
        //{
        //    //priceCalMode 0:  总张数*纸张价格 + 面数 * 耗材单价
        //    SpendMoney = (Decimal)papeCount * paperPrice + pageNum * colPrice;
        //}
        //chen 20140424 add end

        //priceCalMode 0:  总张数*纸张价格 + 面数 * 耗材单价


        if (pageId.Equals( UtilConst.CON_PAGE_A3 ) && Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
        {
            SpendMoney = (Decimal)papeCount / 2 * paperPrice + pageNum / 2 * colPrice;
        }
        else
        {
            SpendMoney = (Decimal)papeCount * paperPrice + pageNum * colPrice;
        }
        string sql = " INSERT INTO [JobInformation]" +
                    "            ([UserID]" +
                    "            ,[GroupID]" +
                    "            ,[JobID]" +
                    "            ,[FunctionID]" +
                    "            ,[PageID]" +
                    "            ,[Number]" +
                //chen add 20140424 for money start
                    "            ,[PapeCount]" +
                    "            ,[CopyCount]" +
                    "            ,[Duplex]" +
                    "            ,[SpendMoney]" +
                    "            ,[PriceDetailID]" +
                //chen add 20140424 for money end

                    "            ,[SerialNumber]" +
                    "            ,[Time]" +
                    "            ,[DspNumber]" +
                    "            ,[DspPapeCount])" +
                    "      VALUES" +
                    "            ({0}" +
                    "            ,{1}" +
                    "            ,{2}" +
                    "            ,{3}" +
                    "            ,{4}" +
                    "            ,{5}" +
            //chen add 20140424 for money start
                    "            ,{8}" +
                    "            ,{9}" +
                    "            ,{10}" +
                    "            ,{11}" +
                    "            ,{12}" +
            //chen add 20140424 for money end
                    "            ,{6}" +
                    "            ,{7}" +
                    "            ,{13}" +
                    "            ,{14})";

        // Update By SES JiJianXiong ST
        // Test in the SESC. MFP time is older than the server time.
        // MFP time is year 2003.


        int DspSheetCount = 0;
        int DspPapeCount = 0;
        int sheetcount = 0;

        sheetcount = (int)pageNum;

        DspSheetCount = sheetcount;
        DspPapeCount = papeCount;

        if ( pageId == UtilConst.CON_PAGE_A3
            //&& logInfo.JOBId != UtilConst.ITEM_TITLE_Scan_JobId
            //&& logInfo.JOBId != UtilConst.ITEM_TITLE_ScanSave_JobId
            && (mfpjob.JOBId == UtilConst.ITEM_TITLE_Copy_JobId
            || mfpjob.JOBId == UtilConst.ITEM_TITLE_Print_JobId
            || mfpjob.JOBId == UtilConst.ITEM_TITLE_DFPrint_JobId)
            )
        {


            if (Duplex == 1)
            {
                DspSheetCount = (int)sheetcount / 2;
                DspPapeCount = (int)sheetcount / 2;
                papeCount = (int)DspPapeCount * 2;
            }
            else
            {
                DspSheetCount = (int)sheetcount / 2;
                DspPapeCount = (int)(sheetcount / copyCount + 2) / 4;
                DspPapeCount = (int)(DspPapeCount * copyCount);

                papeCount = (int)DspPapeCount * 2;
            }

        }
        //chen add 20140424 for money start
        //object[] args = new object[8];
        object[] args = new object[15];
        //chen add 20140424 for money end
        //update 20151103 add for fax no user start
        if (userid < 0)
        {
            userid = -1;
        }
        //update 20151103 add for fax no user end

        args[0] = ConvertIntToSQL(userid.ToString());
        args[1] = ConvertIntToSQL(GroupId.ToString());
        args[2] = ConvertIntToSQL(mfpjob.JOBId.ToString());
        args[3] = ConvertIntToSQL(mfpjob.FunctionId.ToString());
        args[4] = ConvertIntToSQL(pageId.ToString());
        args[5] = ConvertIntToSQL(pageNum.ToString());
        args[6] = ConvertStringToSQL(serialNumber);
        // args[7] = ConvertDateToSQL(time);
        args[7] = ConvertDateToSQL(DateTime.Now);
        // Update By SES JiJianXiong ED

        //chen add 20140424 for money start
        args[8] = ConvertIntToSQL(papeCount.ToString());
        args[9] = ConvertIntToSQL(copyCount.ToString());
        args[10] = ConvertIntToSQL(Duplex.ToString());
        args[11] = ConvertDecimalToSQL(SpendMoney.ToString());
        args[12] = ConvertIntToSQL(PriceDetailID.ToString());

        args[12] = ConvertIntToSQL(PriceDetailID.ToString());




        //DspSheetCount = (int)pageNum;
        //DspPapeCount = papeCount;
        //if (pageId.Equals(UtilConst.CON_PAGE_A3))
        //{
        //    DspSheetCount = (int)pageNum / 2;
        //    DspPapeCount = papeCount / 2;
        //}


        args[13] = ConvertIntToSQL(DspSheetCount.ToString());
        args[14] = ConvertIntToSQL(DspPapeCount.ToString());

        //chen add 20140424 for money end
        using (SqlConnection con = new SqlConnection(DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand(string.Format(sql, args), con, tran))
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
                throw ex;
            }
            finally
            {
                tran.Dispose();
                tran = null;
            }

        }

        //chen 20140429 add 跟新用户余额　start

        //decimal remainMoney = 0;
        //decimal remainColorMoney = 0;

        //dtUserInfo.UserInfoDataTable userdatatable = GetUserInfoFromDB(userid);
        //if (userdatatable.Count > 0)
        //{
        //    dtUserInfo.UserInfoRow userRow = userdatatable[0];
        //    remainMoney = userRow.RemainMoney;
        //    remainColorMoney = userRow.RemainColorMoney;
        //}
        
        //remainMoney = remainMoney + SpendMoney;
        //if (mfpjob.FunctionId == 2)
        //{
        //    remainColorMoney = remainColorMoney + SpendMoney;
        //}

        //string strSql;
        //strSql = "   UPDATE [UserInfo]          " + Environment.NewLine;
        //strSql += "  SET                        " + Environment.NewLine;
        //strSql += "      ,[RemainMoney]  = {0} " + Environment.NewLine;
        //strSql += "      ,[RemainColorMoney] = {1} " + Environment.NewLine;
        ////if (!userid.Text.Equals(UtilConst.CON_USER_LONINNAME))
        ////{
        ////    strSql += "      ,[UpdateTime] = getdate() " + Environment.NewLine;
        ////}
        //strSql += "WHERE ID = {2}   " + Environment.NewLine;

        //string[] paramslist = new string[3];
        //paramslist[0] = UtilCommon.ConvertDecimalToSQL(remainMoney.ToString());
        //paramslist[1] = UtilCommon.ConvertDecimalToSQL(remainColorMoney.ToString());
        //paramslist[2] = ConvertIntToSQL(userid.ToString());

        //strSql = string.Format(strSql, paramslist);

        //using (SqlConnection con = new SqlConnection(DBConnectionStrings))
        //{
        //    con.Open();
        //    SqlTransaction tran = con.BeginTransaction();
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
        //        {
        //            cmd.ExecuteNonQuery();
        //        }
        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (tran.Connection != null)
        //        {
        //            tran.Rollback();
        //        }
        //        throw ex;
        //    }
        //    finally
        //    {
        //        tran.Dispose();
        //        tran = null;
        //    }

        //}
        //chen 20140429 add 跟新用户余额　end

    }

    #endregion

    #region "Get the Disp Setting" 
    /// <summary>
    /// GetDispSetting
    /// </summary>
    /// <returns></returns>
    /// <Date>2010.11.15</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.10</Version>
    /// <example>
    /// dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
    /// </example>
    public static dtSettingDisp.SettingDispRow GetDispSetting()
    {
        dtSettingDispTableAdapters.SettingDispTableAdapter query = new dtSettingDispTableAdapters.SettingDispTableAdapter();
        dtSettingDisp.SettingDispDataTable dt = query.GetData();

        dtSettingDisp.SettingDispRow result;
        if (dt.Rows.Count == 0)
        {
            // Return Default Record.
            result = dt.NewRow() as dtSettingDisp.SettingDispRow;
            result.Dis_U_UserName = 1;
            result.Dis_U_LoginName = 1;
            result.Dis_U_GroupName = 0;
            result.Dis_U_CardID = 0;
            result.Dis_U_Restrict = 0;
            result.Dis_G_GroupName = 1;
            result.Dis_G_Number = 1;
            result.Dis_G_Restrict = 0;
            result.Dis_R_Restrict = 0;
            // 2010.12.07 Update By SES.Jijianxiong ST
            // Specification_SimpleEA(V1.27)_20101203.doc Update.
            //result.Dis_R_BWCopy = 1;
            //result.Dis_R_FCCopy = 0;
            //result.Dis_R_BWPrint = 0;
            //result.Dis_R_FCPrint = 0;
            //result.Dis_R_BWScan = 0;
            //result.Dis_R_FCScan = 0;
            result.Dis_R_Copy = 1;
            result.Dis_R_Print = 0;
            result.Dis_R_Scan = 0;
            // 2010.12.07 Update By SES.Jijianxiong ED

            result.Dis_R_Fax = 0;
            result.Dis_Job_Total = 0;
            result.Dis_Job_CopyTotal = 1;
            result.Dis_Job_PrintTotal = 1;
            result.Dis_Job_ScanTotal = 0;
            result.Dis_Job_FaxTotal = 0;
            result.Dis_Result_Copy = 1;
            result.Dis_Result_Print = 1;
            result.Dis_Result_Scan = 0;
            result.Dis_Result_Fax = 0;
            result.Dis_Result_Other = 0;
            // 2010.12.10 Add By SES.Jijianxiong ST
            // Specification_SimpleEA(V1.28)_20101210.doc Update.
            // Can Borrow
            result.Dis_Avai_Borrow = 1;
            // No Limit
            result.Dis_Log_MaxCount = 0;
            // 2010.12.10 Add By SES.Jijianxiong ED
            //chen add 20140429 st
            result.Dis_Count_mode = 0;
            result.Dis_A3_A4 = 0;
            //chen add 20140429 st
        }
        else
        {
            // Return  Record.
            result = dt.Rows[0] as dtSettingDisp.SettingDispRow;
        }

        return result;

    }
    #endregion
    //chen add 20140428 for userlimit start
    #region "GetUserLimitByMoney for Cost Solution"
    /// <summary>
    /// GetUserLimitByMoney for Cost Solution
    /// </summary>
    /// <param name="resdatatable"></param>
    /// <param name="jobdatatable"></param>
    /// <returns></returns>
    /// <Date>2014.4.28</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>1.10</Version>
    internal static dtUserLimit.UserLimitDataTable GetUserLimitByMoney(
        dtRestrictionInfo.RestrictionInfoDataTable resmoneydatatable,
        dtRestrictionInformation.RestrictionInformationDataTable resdatatable,
        dtJobInformation.JobInformationDataTable jobdatatable,
        dtUserPayDetail.UserPayDetailDataTable userPaydatatable,
        string serialnumber
        )
    {

        //获得用户剩余额度
        decimal remainMoney = 0;
        decimal remainColorMoney = 0;
        decimal usedMoney = 0;
        decimal usedColorMoney = 0;

        decimal paySumMoney = 0;
        decimal payColorSumMoney = 0;

        //if (userdatatable.Count > 0)
        //{
        //    dtUserInfo.UserInfoRow userRow = userdatatable[0];
        //    usedMoney = userRow.RemainMoney;
        //    usedColorMoney = userRow.RemainColorMoney;
        //}
        usedColorMoney = GetJobSpendMoneyFun(jobdatatable, 2);
        usedMoney = usedColorMoney + GetJobSpendMoneyFun(jobdatatable, 1);

        //获得用户透支额度
        decimal upLimitMoney = 0;
        if( resmoneydatatable.Count > 0 )
        {
            dtRestrictionInfo.RestrictionInfoRow resRow = resmoneydatatable[0];
            upLimitMoney = resRow.OverLimit;
            remainMoney = resRow.AllQuota;
            remainColorMoney = resRow.ColorQuota;
        }
        //用户追加额度
        if (userPaydatatable.Count > 0)
        {
            dtUserPayDetail.UserPayDetailRow payRow = userPaydatatable[0];
            paySumMoney = payRow.SumMoney;
            payColorSumMoney = payRow.SumColorMoney;
          
        }

        //用户配额 + 透支额度 -  已用额度  作为用户可用额度
        remainMoney = remainMoney + upLimitMoney + paySumMoney - usedMoney;
        remainColorMoney = remainColorMoney + payColorSumMoney - usedColorMoney;

        //当彩色余额大于总余额时，可用的彩色额度为总余额
        if (remainColorMoney > remainMoney)
        {
            remainColorMoney = remainMoney;
        }


        int PaperTypeID = UtilConst.CON_DEFAULT_PAPERTYPE_A4;
        int PriceDetailID;
        int priceID;
        //int priceCalMode;
        decimal paperPrice = 0;
        decimal grayPrice = 0;
        decimal colorPrice = 0;
        decimal price = 0;

        priceID = UtilCommon.getMFPPriceID(serialnumber);
        //priceCalMode = UtilCommon.getPriceCalMode(priceID);

        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        int Dsp_A3_A4 = settingrow.Dis_A3_A4;
        int Dsp_Count_mode = settingrow.Dis_Count_mode;

        
        dtUserLimit.UserLimitDataTable userlimittable = new dtUserLimit.UserLimitDataTable();
        int intBWUserlimitnum = 0;
        int intFullUserlimitnum = 0;

        int status = UtilConst.CON_UNLIMIT_NUM;

        // Copy 
        //计算用户复印限制
        //***********************************************************************************/
        //计算用户复印限制 st
        //***********************************************************************************/

        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;

        paperPrice = 0;
        grayPrice = 0;
        colorPrice = 0;

        dtPriceDetailTableAdapters.PriceDetailTableAdapter pdAdapter = new dtPriceDetailTableAdapters.PriceDetailTableAdapter();
        dtPriceDetail.PriceDetailDataTable pdDt;
        if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
        {

            try
            {
                PriceDetailID = UtilCommon.getPriceDetailID(priceID, PaperTypeID, UtilConst.ITEM_TITLE_Copy_JobId);

                pdDt = pdAdapter.GetDataByPriceDetailID(PriceDetailID);
                if (pdDt.Rows.Count != 0)
                {
                    paperPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).PaperPrice;
                    grayPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).GrayPrice;
                    colorPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).ColorPrice;
                }
            }
            catch (Exception e)
            {
                ;
            }
        }
        else
        {
            paperPrice = 0;
            grayPrice = 1;
            colorPrice = 1;
        }
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        //估算用户实际的黑白限制数量
        status  = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            // 2.Get User's Job's SpendMoney
            price = 0;

            
            price = paperPrice / 2 + grayPrice;
            if (price == 0)
            {
                intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
            }
            else
            {
                intBWUserlimitnum = (int)(remainMoney / price);
            }
        }
        //估算用户实际的彩色限制数量
        status  = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2);
        if (status == 0)
        {
            intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intFullUserlimitnum = 0;
        }
        else
        {
            price = 0;
            //if (priceCalMode == UtilConst.CON_PRICE_MODE_1)
            //{
            //    price = colorPrice;
            //}
            //else
            //{
            //    price = paperPrice / 2 + colorPrice;
            //}
            
            price = paperPrice / 2 + colorPrice;
            if (price == 0)
            {
                intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
            }
            else
            {
                intFullUserlimitnum = (int)(remainColorMoney / price);
            }
        }
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户复印限制 ed
        //***********************************************************************************/

        //***********************************************************************************/
        //计算用户打印限制 st
        //***********************************************************************************/
        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;

        paperPrice = 0;
        grayPrice = 0;
        colorPrice = 0;

        if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
        {
            try
            {
                PriceDetailID = UtilCommon.getPriceDetailID(priceID, PaperTypeID, UtilConst.ITEM_TITLE_Print_JobId);

                pdDt = pdAdapter.GetDataByPriceDetailID(PriceDetailID);
                if (pdDt.Rows.Count != 0)
                {
                    paperPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).PaperPrice;
                    grayPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).GrayPrice;
                    colorPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).ColorPrice;
                }

            }
            catch (Exception e)
            {
                ;
            }
        }
        else
        {
            paperPrice = 0;
            grayPrice = 1;
            colorPrice = 1;
        }
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            // 2.Get User's Job's SpendMoney
            price = 0;
            //if (priceCalMode == UtilConst.CON_PRICE_MODE_1)
            //{
            //    price = grayPrice;
            //}
            //else
            //{
            //    price = paperPrice / 2 + grayPrice;
            //}
            price = paperPrice / 2 + grayPrice;
            if (price == 0)
            {
                intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
            }
            else
            {
                intBWUserlimitnum = (int)(remainMoney / price);
            }
        }
        //估算用户实际的彩色限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2);
        if (status == 0)
        {
            intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intFullUserlimitnum = 0;
        }
        else
        {
            price = 0;
            //if (priceCalMode == UtilConst.CON_PRICE_MODE_1)
            //{
            //    price = colorPrice;
            //}
            //else
            //{
            //    price = paperPrice / 2 + colorPrice;
            //}
            price = paperPrice / 2 + colorPrice;
            if (price == 0)
            {
                intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
            }
            else
            {
                intFullUserlimitnum = (int)(remainColorMoney / price);
            }
        }
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户打印限制 ed
        //***********************************************************************************/


        //***********************************************************************************/
        //计算用户文件归档打印限制 st
        //***********************************************************************************/
        //同打印
        //intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        //intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户文件归档打印限制 ed
        //***********************************************************************************/

        //***********************************************************************************/
        //计算用户清单打印限制 st
        //***********************************************************************************/
        //同打印
        //intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        //intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户清单打印限制 ed
        //***********************************************************************************/



        //***********************************************************************************/
        //计算用户扫描限制 st
        //***********************************************************************************/
        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;

        paperPrice = 0;
        grayPrice = 0;
        colorPrice = 0;

        if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
        {
            try
            {
                PriceDetailID = UtilCommon.getPriceDetailID(priceID, PaperTypeID, UtilConst.ITEM_TITLE_Scan_JobId);

                pdDt = pdAdapter.GetDataByPriceDetailID(PriceDetailID);
                if (pdDt.Rows.Count != 0)
                {
                    paperPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).PaperPrice;
                    grayPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).GrayPrice;
                    colorPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).ColorPrice;
                }
            }
            catch (Exception e)
            {
                ;
            }
        }
        else
        {
            paperPrice = 0;
            grayPrice = 1;
            colorPrice = 1;
        }
        //获得已经使用金额
        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            // 2.Get User's Job's SpendMoney
            price = 0;
            //if (priceCalMode == UtilConst.CON_PRICE_MODE_1)
            //{
            //    price = grayPrice;
            //}
            //else
            //{
            //    price = paperPrice / 2 + grayPrice;
            //}
            price = paperPrice / 2 + grayPrice;
            if (price == 0)
            {
                intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
            }
            else
            {
                intBWUserlimitnum = (int)(remainMoney / price);
            }
        }
        intFullUserlimitnum = intBWUserlimitnum;
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2, intFullUserlimitnum);

        //***********************************************************************************/
        //计算用户扫描限制 ed
        //***********************************************************************************/
        //***********************************************************************************/
        //计算用户扫描并保存限制 st
        //***********************************************************************************/
        //限制同扫描
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户扫描并保存限制 ed
        //***********************************************************************************/
        //***********************************************************************************/
        //计算用户网络传真限制 st
        //***********************************************************************************/
        //同扫描
        //intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1, intBWUserlimitnum);
        //***********************************************************************************/
        //计算用户网络传真限制 ed
        //***********************************************************************************/


        //***********************************************************************************/
        //计算用户传真(Channel2)限制 st
        //***********************************************************************************/
        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;

        paperPrice = 0;
        grayPrice = 0;
        colorPrice = 0;
        if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
        {
            try
            {
                PriceDetailID = UtilCommon.getPriceDetailID(priceID, PaperTypeID, UtilConst.ITEM_TITLE_FaxC2_JobId);

                pdDt = pdAdapter.GetDataByPriceDetailID(PriceDetailID);
                if (pdDt.Rows.Count != 0)
                {
                    paperPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).PaperPrice;
                    grayPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).GrayPrice;
                    colorPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).ColorPrice;
                }
            }
            catch (Exception e)
            {
                ;
            }
        }
        else
        {
            paperPrice = 0;
            grayPrice = 1;
            colorPrice = 1;
        }
        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            // 2.Get User's Job's SpendMoney
            price = 0;
            //if (priceCalMode == UtilConst.CON_PRICE_MODE_1)
            //{
            //    price = grayPrice;
            //}
            //else
            //{
            //    price = paperPrice + grayPrice;
            //}
            price = paperPrice + grayPrice;
            if (price == 0)
            {
                intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
            }
            else
            {
                intBWUserlimitnum = (int)(remainMoney / price);
            }
        }
       
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1, intBWUserlimitnum);
        //***********************************************************************************/
        //计算用户传真(Channel2)限制 ed
        //***********************************************************************************/

        //***********************************************************************************/
        //计算用户传真 限制 st
        //***********************************************************************************/
        //限制同传真(Channel2)
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1, intBWUserlimitnum);
        //***********************************************************************************/
        //计算用户传真 限制 st
        //***********************************************************************************/
        return userlimittable;
    }
    #endregion


    #region "GetUserLimitByFunct for Cost Solution"
    /// <summary>
    /// GetUserLimitByFunct for Cost Solution
    /// </summary>
    /// <param name="resdatatable"></param>
    /// <returns></returns>
    /// <Date>2014.6.05</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>1.10</Version>
    internal static dtUserLimit.UserLimitDataTable GetUserLimitByFunct(
        dtRestrictionInformation.RestrictionInformationDataTable resdatatable
        )
    {
        dtUserLimit.UserLimitDataTable userlimittable = new dtUserLimit.UserLimitDataTable();
        int intBWUserlimitnum = 0;
        int intFullUserlimitnum = 0;

        int status = 0;

        // Copy 
        //计算用户复印限制
        //***********************************************************************************/
        //计算用户复印限制 st
        //***********************************************************************************/

        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;


        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        //估算用户实际的彩色限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2);
        if (status == 0)
        {
            intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intFullUserlimitnum = 0;
        }
        else
        {
            intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户复印限制 ed
        //***********************************************************************************/

        //***********************************************************************************/
        //计算用户打印限制 st
        //***********************************************************************************/
        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        //估算用户实际的彩色限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2);
        if (status == 0)
        {
            intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intFullUserlimitnum = 0;
        }
        else
        {
            intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户打印限制 ed
        //***********************************************************************************/
        //***********************************************************************************/
        //计算用户文件归档打印限制 st
        //***********************************************************************************/
        //同打印
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户文件归档打印限制 ed
        //***********************************************************************************/

        //***********************************************************************************/
        //计算用户清单打印限制 st
        //***********************************************************************************/
        //同打印
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户清单打印限制 ed
        //***********************************************************************************/



        //***********************************************************************************/
        //计算用户扫描限制 st
        //***********************************************************************************/
        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;

        //获得已经使用金额
        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        intFullUserlimitnum = intBWUserlimitnum;
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2, intFullUserlimitnum);

        //***********************************************************************************/
        //计算用户扫描限制 ed
        //***********************************************************************************/
        //***********************************************************************************/
        //计算用户扫描并保存限制 st
        //***********************************************************************************/
        //限制同扫描
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户扫描并保存限制 ed
        //***********************************************************************************/
        //***********************************************************************************/
        //计算用户网络传真限制 st
        //***********************************************************************************/
        //同扫描
        //intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1, intBWUserlimitnum);
        //***********************************************************************************/
        //计算用户网络传真限制 ed
        //***********************************************************************************/


        //***********************************************************************************/
        //计算用户传真(Channel2)限制 st
        //***********************************************************************************/
        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;

        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }

        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1, intBWUserlimitnum);
        //***********************************************************************************/
        //计算用户传真(Channel2)限制 ed
        //***********************************************************************************/

        //***********************************************************************************/
        //计算用户传真 限制 st
        //***********************************************************************************/
        //限制同传真(Channel2)
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1, intBWUserlimitnum);
        //***********************************************************************************/
        //计算用户传真 限制 st
        //***********************************************************************************/
        return userlimittable;
    }
    #endregion


    #region "GetUserLimitByFunct for Cost Solution"
    /// <summary>
    /// GetUserLimitByFunct for Cost Solution
    /// </summary>
    /// <param name="resdatatable"></param>
    /// <returns></returns>
    /// <Date>2014.6.05</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>1.10</Version>
    internal static dtUserLimit.UserLimitDataTable GetUserLimitByFunct(
        string sn,
        dtRestrictionInformation.RestrictionInformationDataTable resdatatable
        )
    {
        dtUserLimit.UserLimitDataTable userlimittable = new dtUserLimit.UserLimitDataTable();
        int intBWUserlimitnum = 0;
        int intFullUserlimitnum = 0;

        int status = 0;

        // Copy 
        //计算用户复印限制
        //***********************************************************************************/
        //计算用户复印限制 st
        //***********************************************************************************/

        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;


        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        //估算用户实际的彩色限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2);
        if (status == 0)
        {
            intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intFullUserlimitnum = 0;
        }
        else
        {
            intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
       
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2, intFullUserlimitnum);


        //***********************************************************************************/
        //计算用户复印限制 ed
        //***********************************************************************************/

        //***********************************************************************************/
        //计算用户打印限制 st
        //***********************************************************************************/
        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        //估算用户实际的彩色限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2);
        if (status == 0)
        {
            intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intFullUserlimitnum = 0;
        }
        else
        {
            intFullUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户打印限制 ed
        //***********************************************************************************/
        //***********************************************************************************/
        //计算用户文件归档打印限制 st
        //***********************************************************************************/
        //同打印
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户文件归档打印限制 ed
        //***********************************************************************************/

        //***********************************************************************************/
        //计算用户清单打印限制 st
        //***********************************************************************************/
        //同打印
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户清单打印限制 ed
        //***********************************************************************************/



        //***********************************************************************************/
        //计算用户扫描限制 st
        //***********************************************************************************/
        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;

        //获得已经使用金额
        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        intFullUserlimitnum = intBWUserlimitnum;
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2, intFullUserlimitnum);

        //***********************************************************************************/
        //计算用户扫描限制 ed
        //***********************************************************************************/
        //***********************************************************************************/
        //计算用户扫描并保存限制 st
        //***********************************************************************************/
        //限制同扫描
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2, intFullUserlimitnum);
        //***********************************************************************************/
        //计算用户扫描并保存限制 ed
        //***********************************************************************************/
        //***********************************************************************************/
        //计算用户网络传真限制 st
        //***********************************************************************************/
        //同扫描
        //intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1, intBWUserlimitnum);
        //***********************************************************************************/
        //计算用户网络传真限制 ed
        //***********************************************************************************/


        //***********************************************************************************/
        //计算用户传真(Channel2)限制 st
        //***********************************************************************************/
        intBWUserlimitnum = 0;
        intFullUserlimitnum = 0;

        //估算用户实际的黑白限制数量
        status = GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1);
        if (status == 0)
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }
        else if (status == 2)
        {
            intBWUserlimitnum = 0;
        }
        else
        {
            intBWUserlimitnum = UtilConst.CON_UNLIMIT_NUM;
        }

        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1, intBWUserlimitnum);
        //***********************************************************************************/
        //计算用户传真(Channel2)限制 ed
        //***********************************************************************************/

        //***********************************************************************************/
        //计算用户传真 限制 st
        //***********************************************************************************/
        //限制同传真(Channel2)
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1, intBWUserlimitnum);
        //***********************************************************************************/
        //计算用户传真 限制 st
        //***********************************************************************************/
        return userlimittable;
    }
    #endregion


    //chen add 20140428 for userlimit start
    #region "GetUserLimit for Cost Solution"
    /// <summary>
    /// GetUserLimit for Cost Solution
    /// </summary>
    /// <param name="resdatatable"></param>
    /// <param name="jobdatatable"></param>
    /// <returns></returns>
    /// <Date>2010.12.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.10</Version>
    internal static dtUserLimit.UserLimitDataTable GetUserLimit(dtRestrictionInformation.RestrictionInformationDataTable resdatatable,
        dtJobInformation.JobInformationDataTable jobdatatable)
    {
        dtUserLimit.UserLimitDataTable userlimittable = new dtUserLimit.UserLimitDataTable();
        // 2011.01.10 Update By SES Jijianxiong ST
        int intBWlimitnum = 0;
        int intBWUserlimitnum = 0;
        int intFullUserlimitnum = 0;
        // Create the table
        // Copy 
        //计算用户复印限制
        // Get Restriction set limit num for B/W Job.
        //用户设定的限制数量
        intBWlimitnum = GetResLimitNumByJobAndFun(resdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1);
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        //获得用户实际的黑白限制数量
        intBWUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1);
        //获得用户实际的彩色限制数量
        intFullUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2);
        // CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum);
        CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum, intBWlimitnum);

        //把用户JOB的黑白限制和彩色限制添加到用户限制表
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2, intFullUserlimitnum);

        // Print
        //计算用户打印限制
        // Get Restriction set limit num for B/W Job.
        intBWlimitnum = GetResLimitNumByJobAndFun(resdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1);
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        intBWUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1);
        intFullUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2);
        // CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum);
        CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum, intBWlimitnum);

        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2, intFullUserlimitnum);
        // Scan
        // Get Restriction set limit num for B/W Job.
        intBWlimitnum = GetResLimitNumByJobAndFun(resdatatable, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1);
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        intBWUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1);
        intFullUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2);
        // CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum);
        CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum, intBWlimitnum);

        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2, intFullUserlimitnum);
        // Fax
        //计算用户传真限制
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        intBWUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1, intBWUserlimitnum);
        // Document Filing Print
        // Get Restriction set limit num for B/W Job.
        intBWlimitnum = GetResLimitNumByJobAndFun(resdatatable, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1);
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        intBWUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1);
        intFullUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId2);
        // CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum);
        CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum, intBWlimitnum);

        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId2, intFullUserlimitnum);

        // Scan Save
        // Get Restriction set limit num for B/W Job.
        intBWlimitnum = GetResLimitNumByJobAndFun(resdatatable, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        intBWUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
        intFullUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
        // CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum);
        CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum, intBWlimitnum);

        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2, intFullUserlimitnum);

        // List Print
        //计算用户List打印限制
        // Get Restriction set limit num for B/W Job.
        intBWlimitnum = GetResLimitNumByJobAndFun(resdatatable, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1);
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        intBWUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1);
        intFullUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId2);
        // CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum);
        CostCalculate(ref intBWUserlimitnum, ref intFullUserlimitnum, intBWlimitnum);

        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1, intBWUserlimitnum);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId2, intFullUserlimitnum);
        
        // Fax (Channel2)
        //计算用户传真(Channel2)限制
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        intBWUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1, intBWUserlimitnum);
        
        // Internet Fax
        //计算用户网络传真限制
        // Get BW Copy Limit Num and FullColor Copy Limit Num.
        intBWUserlimitnum = GetUserLimitNum(resdatatable, jobdatatable, UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1);
        userlimittable.AddUserLimitRow(UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1, intBWUserlimitnum);

        // 2011.01.10 Update By SES Jijianxiong ED

        return userlimittable;
    }

    /// <summary>
    /// Cost Calculate
    /// </summary>
    /// <param name="intBWUserlimitnum"></param>
    /// <param name="intFullUserlimitnum"></param>
    /// <Date>2010.12.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.10</Version>
    private static void CostCalculate(ref int intBWUserlimitnum, ref int intFullUserlimitnum)
    {
        // Black and Whilte Color
        int intBW = intBWUserlimitnum;
        // Full Color
        int intFC = intFullUserlimitnum;

        if (intBW < 0)
        {
            intBWUserlimitnum = intBW + intFC;
            intFullUserlimitnum = intBW + intFC;
        }
        else
        {
            intBWUserlimitnum = intBW + intFC;
        }

        if (intBWUserlimitnum < 0)
        {
            intBWUserlimitnum = 0;
        }

        if (intFullUserlimitnum < 0)
        {
            intFullUserlimitnum = 0;
        }
    }

    /// <summary>
    /// Cost Calculate
    /// </summary>
    /// <param name="intBWUserlimitnum"></param>
    /// <param name="intFullUserlimitnum"></param>
    /// <param name="intBWlimitnum"></param>
    /// <Date>2010.12.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.10</Version>
    private static void CostCalculate(ref int intBWUserlimitnum, ref int intFullUserlimitnum, int intBWlimitnum)
    {
        // Black and Whilte Color
        int intBW = intBWUserlimitnum;
        // Full Color
        int intFC = intFullUserlimitnum;


        // While the Restriction set's BW num is Prohibition.
        // Can not use the borrow mode.
        if (intBWlimitnum != 0)
        {
            if (intBW < 0)
            {
                intBWUserlimitnum = intBW + intFC;
                intFullUserlimitnum = intBW + intFC;
            }
            else
            {
                intBWUserlimitnum = intBW + intFC;
            }
        }

        if (intBWUserlimitnum < 0)
        {
            intBWUserlimitnum = 0;
        }

        if (intFullUserlimitnum < 0)
        {
            intFullUserlimitnum = 0;
        }
    }
    #endregion

    #region ResSpecialProcess
    /// <summary>
    /// ResSpecialProcess
    /// </summary>
    /// <returns></returns>
    public static dtRestrictionInformation.RestrictionInformationDataTable ResSpecialProcess()
    {
        dtRestrictionInformationTableAdapters.RestrictionInformationTableAdapter adapter = new dtRestrictionInformationTableAdapters.RestrictionInformationTableAdapter();
        dtRestrictionInformation.RestrictionInformationDataTable resdatatable = adapter.GetDataByResId(0);
        foreach (DataRow row in resdatatable.Rows)
        {
            dtRestrictionInformation.RestrictionInformationRow resrow = (dtRestrictionInformation.RestrictionInformationRow)row;
            resrow.Status = 1;
            resrow.LimitNum = "";
        }
        return resdatatable;
    }
    #endregion

    #region InitAppLog
    // create new log file if webconfig setting is 
    // <add key="AppLog" value="true" />
    /// <summary>
    /// for pull print Log
    /// </summary>
    /// <returns></returns>
    /// /// <Date>2012.02.17</Date>
    /// <Author>SLC Wei changye</Author>
    /// <Version>1.2</Version>
    public static bool InitAppLog()
    {
        bool bRet = false;
        //chen add 
        if (!Directory.Exists(AppCsvPath))
        {
            Directory.CreateDirectory(AppCsvPath);
        }
        //

        if (ConfigurationManager.AppSettings["AppLog"].ToString().Trim() == "true")
        {
            if (!Directory.Exists(AppLogPath))
                Directory.CreateDirectory(AppLogPath);
            bRet = true;
        }

        return bRet;
    }
    #endregion

    #region InitAppCsv
    /// <summary>
    /// for pull print Log
    /// </summary>
    /// <returns></returns>
    /// /// <Date>2014.06.11</Date>
    /// <Author>SLC chen youguang</Author>
    /// <Version>2.0</Version>
    public static bool InitAppCsv()
    {
        bool bRet = false;
        //chen add 
        if (!Directory.Exists(AppCsvPath))
        {
            Directory.CreateDirectory(AppCsvPath);
            bRet = true;
        }


        return bRet;
    }
    #endregion


    #region AppLogPath

    /// <summary>
    /// App log path
    /// </summary>
    /// <returns></returns>
    /// /// <Date>2012.02.17</Date>
    /// <Author>SLC Wei changye</Author>
    /// <Version>1.2</Version>
    public static String AppLogPath
    {
        get
        {
            //if (ConfigurationManager.AppSettings["AppLogPath"].ToString().Contains("~"))
            //{
            return ConfigurationManager.AppSettings["AppLogPath"].ToString();
            //}
            //else
            //return ConfigurationManager.AppSettings["AppLogPath"].ToString();
            //return  Environment.GetEnvironmentVariable("TEMP");
            //return Environment.GetEnvironmentVariable("TEMP") + ConfigurationManager.AppSettings["AppLog"].ToString();
            //return "D:\\slog\\";
        }
    }

    #endregion 

    #region AppCsvPath

    /// <summary>
    /// App Csv path
    /// </summary>
    /// <returns></returns>
    /// /// <Date>2014.06.11</Date>
    /// <Author>SLC chen youguang</Author>
    /// <Version>2.0</Version>
    public static String AppCsvPath
    {
        get
        {
            return ConfigurationManager.AppSettings["AppCsvPath"].ToString();
        }
    }

    #endregion 
    #region UseSSL

    /// <summary>
    /// transfer type choose
    /// </summary>
    /// <returns></returns>
    /// /// <Date>2012.02.17</Date>
    /// <Author>SLC Wei changye</Author>
    /// <Version>1.2</Version>
    public static String UseSSL
    {
        get { return ConfigurationManager.AppSettings["UseSSL"].ToString(); }
    }

    #endregion 

    #region GetAppSettingString

    /// <summary>
    /// Get the application setting from WebConfig
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <Date>2012.02.23</Date>
    /// <Author>SLC Wei changye</Author>
    /// <Version>1.2</Version>
    public static string GetAppSettingString(string key)
    {
        string value = string.Empty;
        if (string.IsNullOrEmpty(key))
        {
            return value;
        }
        value = ConfigurationManager.AppSettings.Get(key);

        if (null == value)
        {
            return string.Empty;
        }

        return value;
    }

    #endregion 

    #region StringToByte

    /// <summary>
    /// StringToByte
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <Date>2012.02.23</Date>
    /// <Author>SLC Wei changye</Author>
    /// <Version>1.2</Version>
    public static string StringToByte(string str)
    {
        string value = string.Empty;
        if (string.IsNullOrEmpty(str))
        {
            return value;
        }

        UnicodeEncoding uni = new UnicodeEncoding();

        byte[] tmp = uni.GetBytes(str);
        StringBuilder sb = new StringBuilder();

        foreach (byte item in tmp)
        {
            sb.Append(item.ToString());
        }
        string folder = sb.ToString();

        return folder;
    }

    #endregion 

    #region Get Follow ME Para

    /// <summary>
    /// Get Follow ME Para
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <Date>2012.04.17</Date>
    /// <Author>SLC Wei changye</Author>
    /// <Version>1.2</Version>
    public static string GetFollowME(string key)
    {
        try
        {
            string fileLocation = 
                HttpRuntime.AppDomainAppPath.Substring(0, HttpRuntime.AppDomainAppPath.LastIndexOf("\\SimpleEA")) +
                "\\RawRecvServer\\bin\\Debug\\FollowMEService.exe.config";
            
            if (!File.Exists(fileLocation))
            {
                // DLL Location
                string dirLocation = HttpRuntime.AppDomainAppPath.Substring(0, (HttpRuntime.AppDomainAppPath.IndexOf("\\SimpleEAWeb")));
                string binLocation = Path.Combine(dirLocation, "bin");
                fileLocation = Path.Combine(binLocation, "SimpleEAFollowME\\FollowMEService.exe.config");
            }

            string XPath = "/configuration/appSettings/add[@key='?']";

            //string filePath = HttpRuntime.AppDomainAppPath + "SimpleEAFollowME\\FollowMEService.exe.config";
            //string filePath = "C:\\Program Files\\Sharp\\SimpleEA_V1.2\\SimpleEAWeb\\SimpleEAFollowME\\RawRecvServer.exe.config";

            XmlDocument domWebConfig = new XmlDocument();

            domWebConfig.Load((fileLocation));
            XmlNode addKey = domWebConfig.SelectSingleNode((XPath.Replace("?", key)));
            if (addKey != null)
            {
                return addKey.Attributes["value"].InnerText;
            }

        }
        catch (Exception ex)
        {
            Global.Log("WebConfig can't find / format error:" + ex.ToString());
            throw ex;
        }
        return "";
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
    public static void ModifyFollowME(string key, string strValue)
    {
        try
        {
            // if is run in vs
            string fileLocation =
                HttpRuntime.AppDomainAppPath.Substring(0, HttpRuntime.AppDomainAppPath.LastIndexOf("\\SimpleEA")) +
                "\\RawRecvServer\\bin\\Debug\\FollowMEService.exe.config";

            // if run as install application
            if (!File.Exists(fileLocation))
            {
                // DLL Location
                string dirLocation = HttpRuntime.AppDomainAppPath.Substring(0, (HttpRuntime.AppDomainAppPath.IndexOf("\\SimpleEAWeb")));
                string binLocation = Path.Combine(dirLocation, "bin");
                fileLocation = Path.Combine(binLocation, "SimpleEAFollowME\\FollowMEService.exe.config");
            }

            string XPath = "/configuration/appSettings/add[@key='?']";

            //string filePath = HttpRuntime.AppDomainAppPath + "SimpleEAFollowME\\FollowMEService.exe.config";
            ////string filePath = "C:\\Program Files\\Sharp\\SimpleEA_V1.2\\SimpleEAWeb\\SimpleEAFollowME\\RawRecvServer.exe.config";

            XmlDocument domWebConfig = new XmlDocument();

            domWebConfig.Load((fileLocation));
            XmlNode addKey = domWebConfig.SelectSingleNode((XPath.Replace("?", key)));
            if (addKey != null)
            {
                addKey.Attributes["value"].InnerText = strValue;
                domWebConfig.Save((fileLocation));
            }
        }
        catch (Exception ex)
        {
            Global.Log("WebConfig can't find / format error:" + ex.ToString());
            throw ex;
        }
    }
    #endregion

    #region "Modify Followme and Printcopy Config"
   
    public static void ModifyFollowMEDBConfig(string folder, string DBConnectionStrings)
    {
        //ModifyFollowME("SimpleEAConnectionString", DBConnectionStrings);
        string key = "SimpleEAConnectionString";
        string strValue = DBConnectionStrings;
        try
        {
            // if is run in vs
            string fileLocation = Path.Combine(folder, "FollowMEService.exe.config");

            string XPath = "/configuration/appSettings/add[@key='?']";

            XmlDocument domWebConfig = new XmlDocument();

            domWebConfig.Load((fileLocation));
            XmlNode addKey = domWebConfig.SelectSingleNode((XPath.Replace("?", key)));
            if (addKey != null)
            {
                addKey.Attributes["value"].InnerText = strValue;
                domWebConfig.Save((fileLocation));
            }
        }
        catch (Exception ex)
        {
            Global.Log("WebConfig can't find / format error:" + ex.ToString());
            throw ex;
        }
    }
    public static void ModifyPrintCopyDBConfig(string folder, string DBConnectionStrings)
    {
        string key = "SimpleEAConnectionString";
        string strValue = DBConnectionStrings;

        try
        {
            string fileLocation = Path.Combine(folder, "PrintCopySys.exe.config");

            string XPath = "/configuration/appSettings/add[@key='?']";

            XmlDocument domWebConfig = new XmlDocument();

            domWebConfig.Load((fileLocation));
            XmlNode addKey = domWebConfig.SelectSingleNode((XPath.Replace("?", key)));
            if (addKey != null)
            {
                addKey.Attributes["value"].InnerText = strValue;
                domWebConfig.Save((fileLocation));
            }
        }
        catch (Exception ex)
        {
            Global.Log("WebConfig can't find / format error:" + ex.ToString());
            throw ex;
        }
    }
    #endregion



    #region "ModifyWebConfig"
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="strValue"></param>
    public static void ModifyWebConfig(string key, string strValue)
    {
        try
        {
            string XPath = "/configuration/appSettings/add[@key='?']";

            string filePath = HttpRuntime.AppDomainAppPath + "web.config";
            //string filePath = "C:\\Program Files\\Sharp\\SimpleEA_V1.2\\SimpleEAWeb\\SimpleEAFollowME\\RawRecvServer.exe.config";
            XmlDocument domWebConfig = new XmlDocument();

            domWebConfig.Load((filePath));
            XmlNode addKey = domWebConfig.SelectSingleNode((XPath.Replace("?", key)));
            if (addKey != null)
            {
                addKey.Attributes["value"].InnerText = strValue;
                domWebConfig.Save((filePath));
            }
        }
        catch (Exception ex)
        {
            Global.Log("WebConfig can't find / format error:" + ex.ToString());
            throw ex;
        }
    }
    #endregion

    #region CheckConnectionString

    /// <summary>
    /// syn the Connection string
    /// </summary>
    ///  <Date>2012.04.17</Date>
    /// <Author>SLC Wei changye</Author>
    /// <Version>1.2</Version>
    public static void CheckConnectionString()
    {
        if (!DBConnectionStrings.Equals(GetFollowME("SimpleEAConnectionString")))
        {
            ModifyFollowME("SimpleEAConnectionString", DBConnectionStrings);
        }
    }
    #endregion

    #region "ExecuteDataTable"
    /// <summary>
    /// ExecuteDataTable
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    /// <Date>2012.05.09</Date>
    /// <Author>SLC Wei CHangye</Author>
    /// <Version>0.01</Version>
    public static DataTable ExecuteDataTable(string sql)
    {
        DataTable data = new DataTable();

        using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
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

    public static int GetUsableID()
    {
        for (int i = 0; i < 32767; i++)
        {
            UserInfoTableAdapter UserInfoAdapter = new UserInfoTableAdapter();
            dtUserInfo.UserInfoDataTable table = UserInfoAdapter.GetDataByUserId(i);
            if (table != null && table.Count > 0)
                continue;
            else
                return i;
        }
        return -1;
    }

    public static string GetLoginName(string uid)
    {
        dtUserInfoTableAdapters.UserInfoTableAdapter adpter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
        dtUserInfo.UserInfoDataTable table = adpter.GetDataByUserId(Convert.ToInt32(uid));

        if (table != null && table.Count > 0)
            return table[0].LoginName;
        else
            return "";
    }

    public static bool IsSerialNOExsit(string sn)
    {
        dtMFPInformationTableAdapters.MFPInformationTableAdapter MfpInfoAdapter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter();

        if (MfpInfoAdapter.GetData(sn).Count > 0)
            return true;
        else
            return false;
    }

    public static void BalanceMFPCount(int currentCount,int authorityCount,string serialNumber)
    {
        if (currentCount > authorityCount)
        {
            try
            {
                using (dtMFPInformationTableAdapters.MFPInformationTableAdapter adpter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter())
                {
                    adpter.Delete(serialNumber);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public static bool IsMFPCountOverFlow(string serialNumber)
    {
        int authorityCount;
        int currentCount;
        try
        {
            string path;
            SLCRegister.RegisterHandler.Initiate("A");
            authorityCount = SLCRegister.RegisterHandler.GetOperateCount(out path);

            using (dtMFPInformationTableAdapters.MFPInformationTableAdapter adpter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter())
            {
                currentCount = (int)adpter.GetCount();
            }
        }
        catch (Exception e)
        {
            throw e;
        }

        if (currentCount >= authorityCount)
        {
            // prevent self insert by DB UI
            BalanceMFPCount(currentCount, authorityCount, serialNumber);
            return true;
        }
        else
            return false; 
    }

    public static void getRemainMoneyUserID(int intUserId, ref string username, ref decimal allMoney, ref decimal colMoney)
    {

        dtJobInformation.JobInformationDataTable jobdatatable = null;


        dtRestrictionInfo.RestrictionInfoDataTable resmoneydatatable = null;
        //private dtUserInfo.UserInfoDataTable userdatatable = null;
        dtUserPayDetail.UserPayDetailDataTable userPaydatatable = null;
        //pupeng 2014 04 26
        decimal UsedMoney = 0, UsedColorMoney = 0, PayMoney = 0, PayColorMoney = 0, RemainMoney = 0, RemainColorMoney = 0.0M;

        // UserID 

        UserInfoTableAdapter userAdapter = new UserInfoTableAdapter();
        dtUserInfo.UserInfoRow userRow = userAdapter.GetDataByUserId(intUserId)[0];

        int restrictid = UtilCommon.GetUserRestrictidFromDB(userRow.ID);

        username = userRow.UserName;
        //// chen add ed
        //获得用户的限额table，根据配额ID
        resmoneydatatable = UtilCommon.GetUserResmoneyLimitsFromDB(restrictid);
        jobdatatable = UtilCommon.GetUserJobInfoFromDB(intUserId);
        userPaydatatable = UtilCommon.GetUserPayDataFromDB(intUserId);
        UsedColorMoney = UtilCommon.GetJobSpendMoneyFun(jobdatatable, 2);
        UsedMoney = UsedColorMoney + UtilCommon.GetJobSpendMoneyFun(jobdatatable, 1);
        //获得用户配额
        if (resmoneydatatable.Count > 0)
        {
            dtRestrictionInfo.RestrictionInfoRow resRow = resmoneydatatable[0];
            //RemainMoney = resRow.IsAllQuotaNull() ? 0 : resRow.AllQuota;
            //RemainColorMoney = resRow.IsColorQuotaNull() ? 0 : resRow.ColorQuota;
            RemainMoney =  resRow.AllQuota;
            RemainColorMoney =  resRow.ColorQuota;
        }
        //用户追加额度
        if (userPaydatatable.Count > 0)
        {
            dtUserPayDetail.UserPayDetailRow payRow = userPaydatatable[0];
            PayMoney = payRow.IsSumMoneyNull() ? 0 : payRow.SumMoney;
            PayColorMoney = payRow.IsSumColorMoneyNull() ? 0 : payRow.SumColorMoney;
        }
        //用户配额 + 透支额度 -  已用额度  作为用户可用额度
        RemainMoney = RemainMoney + PayMoney - UsedMoney;
        RemainColorMoney = RemainColorMoney + PayColorMoney - UsedColorMoney;
        if (RemainColorMoney > RemainMoney)
        {
            RemainColorMoney = RemainMoney;
        }

        allMoney = RemainMoney;
        colMoney = RemainColorMoney;

    }
    #region 截取指定字数字符串

    /// <summary>
    /// 格式化字符串,取字符串前 strLength 位，其他的用...代替.
    /// 计算字符串长度。汉字两个字节，字母一个字节
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="strLength">字符串长度</param>
    /// <returns></returns>
    public static string FormatStr(string str, int len)
    {
        ASCIIEncoding ascii = new ASCIIEncoding();
        int tempLen = 0;
        string tempString = "";
        byte[] s = ascii.GetBytes(str);
        for (int i = 0; i < s.Length; i++)
        {
            if ((int)s[i] == 63)
            { tempLen += 2; }
            else
            { tempLen += 1; }
            try
            { tempString += str.Substring(i, 1); }
            catch
            { break; }
            if (tempLen > len) break;
        }
        //如果截过则加上半个省略号 
        byte[] mybyte = System.Text.Encoding.Default.GetBytes(str);
        if (mybyte.Length > len)
            tempString += "......";
        tempString = tempString.Replace("&nbsp;", " ");
        tempString = tempString.Replace("&lt;", "<");
        tempString = tempString.Replace("&gt;", ">");
        tempString = tempString.Replace('\n'.ToString(), "<br>");
        return tempString;
    }

    #endregion

    public static string RunCommand(string cmdString)
    {
        string vpath=@"~\RawRecvServer";
        
        Process p = new Process();
        //p.StartInfo.WorkingDirectory = System.Windows.Forms.Application.StartupPath;
        p.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath(vpath); 
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.CreateNoWindow = true;
        p.Start();
        p.StandardInput.WriteLine(cmdString);
        p.StandardInput.WriteLine("exit");
        p.WaitForExit(60000);
        string outPutString = p.StandardOutput.ReadToEnd();
        p.Close();
        return outPutString;
    }



    public int GetTaskPrintBW(string key)
    {
        int print_bw = 0;
        try
        {
            string LoginName = "";
            dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
            dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(Convert.ToInt32(key));

            if (table != null && table.Count > 0)
            {
                LoginName = table[0].LoginName;
            }
            else
            {
                return print_bw;
            }
            //查找用户的配额方案ID
            dtUserInfoTableAdapters.UserInfoTableAdapter userAdpter = new UserInfoTableAdapter();
            dtUserInfo.UserInfoDataTable user = userAdpter.GetDataByLoginName(LoginName);
            int restrictid = user[0].RestrictionID;

            //如果配额方案ID==-1，继承用户组配额方案，
            //从用户组的配额方案中读取配额方案ID
            if (restrictid == -1)
            {
                int groupID = user[0].GroupID;
                dtGroupInfoTableAdapters.GroupInfoTableAdapter groupAdapter = new dtGroupInfoTableAdapters.GroupInfoTableAdapter();
                dtGroupInfo.GroupInfoDataTable groupinfoDT = groupAdapter.GetGroupInfoDataById(groupID);
                if (groupinfoDT.Count == 0)
                {
                    return -1;
                }
                dtGroupInfo.GroupInfoRow grouRow = groupinfoDT[0] as dtGroupInfo.GroupInfoRow;
                restrictid = Int32.Parse(grouRow.RestrictionID);
            }

            dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter restrictAdp = new dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter();
            dtRestrictionInfo.RestrictionInfoDataTable restrict = restrictAdp.GetRestrictionInfoDataByID(restrictid);
            print_bw = restrict[0].PrintBW;

        }
        catch (Exception ex)
        {
            print_bw = 0;
        }
        return print_bw;
    }
    
}