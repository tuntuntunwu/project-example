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
//using System.Data.SqlClient;
//using dtPriceMasterTableAdapters;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using SnmpSharpNet;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// Edit Group screen
/// </summary>
/// <Date>2012.03.09</Date>
/// <Author>SLC Zheng wei</Author>
/// <Version>0.01</Version>
public partial class LookMfpStatus : GrpInfoMain
{
    struct Status
    {
        public int deviceStatus;
        public int printerStatus;
        public string errorState;
        public string causeOfDown;
    };
    ArrayList statusArray5 = new ArrayList();
    ArrayList statusArray3 = new ArrayList();
    ArrayList statusArray2 = new ArrayList();

    const int CELL_W_COLUM_1 = 150;
    const int CELL_W_COLUM_2 = 250;
    const int CELL_W_COLUM_3 = 250;
    const string PERCENT_MARK = "%";

    public class ErrorBitCode
    {
        public string errorBitCode;

        public List<string> errorBitList = new List<string>();
        public List<string> ErrorBitList
        {
            get
            {
                return errorBitList;
            }
        }
        public void AddErrorBitCode(string errorCode)
        {
            errorBitList.Add(errorCode);
        }
    };

    List<ErrorBitCode> errorBitList = new List<ErrorBitCode>();
    
    private int ConvertObjToInt(Object val)
    {
        int ret = 0;
        try
        {
            if (val == null)
            {
                ret = 0;
            }
            else
            {
                ret = Convert.ToInt32(val);
            }
        }
        catch
        {
            ret = 0;
        }
        return ret;
    }
    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    /// <Version>0.02</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Change to the Sub Title
        this.Master.Title = UtilConst.CON_PAGE_MFP;
        Master.SubTitle(UtilConst.CON_MFP_STATUS, "MFPRestrictionList.aspx", true);

        //Get IP Address;
        string ipAddress = Page.Request.Params["SerialNumber"].ToString();

        // Check Access Role
        CheckUser();

        if (!IsPostBack)
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["SimpleEAConnectionString"].ToString();
            SqlConnection con = new SqlConnection(constr);
            int tonerThreshold = 0;
            int paperThreshold = 0;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("select * from SimpleEA.dbo.SettinServerIP", con);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader != null)
                {
                    reader.Read();
                    tonerThreshold = ConvertObjToInt(reader["ColLimit"]);//设置墨盒内的墨粉量阈值;
                    paperThreshold = ConvertObjToInt(reader["PaperLimit"]);//设置纸张数量的阈值;
                }
            }
            catch (Exception)
            {
                ;// throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            try
            {
                Check(ipAddress, tonerThreshold, paperThreshold);
            }
            catch (Exception)
            {
                throw;
            }


        }
        // Cancel Button's Confirm Msg
        btnCancel.OnClientClick = ConfirmFunctionCancel(UtilConst.MSG_UPDATE_CANCEL);

    }
    #endregion

    #region Check
    //Set Cause of Down(array of 5);
    public void InfoSet()
    {
        statusArray5.Clear();
        Status s1 = new Status();
        s1.errorState = "01 00";
        s1.causeOfDown = "Error\n";
        s1.causeOfDown += "Staple impossible\n";
        s1.causeOfDown += "Finisher detached\n";
        s1.causeOfDown += "Hard Disk Error\n";
        s1.causeOfDown += "Account Limit\n";
        statusArray5.Add(s1);

        s1 = new Status(); 
        s1.errorState = "08 00";
        s1.causeOfDown = "Cover Open\n";
        statusArray5.Add(s1);

        s1 = new Status();
        s1.errorState = "04 00";
        s1.causeOfDown = "Paper Jam\n";
        statusArray5.Add(s1);

        s1 = new Status();
        s1.errorState = "40 00";
        s1.causeOfDown = "Empty for specified paper\n";
        s1.causeOfDown += "Empty for specified input tray\n";
        s1.causeOfDown += "Specified Input Tray Open\n";
        statusArray5.Add(s1);

        s1 = new Status();
        s1.errorState = "00 08";
        s1.causeOfDown = "Full of paper in the specified output tray\n";
        statusArray5.Add(s1);

        s1 = new Status();
        s1.errorState = "00 20";
        s1.causeOfDown = "Toner Unequipped\n";
        statusArray5.Add(s1);

        s1 = new Status();
        s1.errorState = "10 00";
        s1.causeOfDown = "Toner Empty\n";
        statusArray5.Add(s1);

        s1 = new Status();
        s1.errorState = "00 02";
        s1.causeOfDown = "Overdue Service Maintenance\n";
        statusArray5.Add(s1);
    }

    //Set Cause of Warning(array of 3);
    public void InfoSet1()
    {
        statusArray3.Clear();
        Status s1 = new Status();
        s1.errorState = "00 40";
        s1.causeOfDown = "No Truck of Stacker\n";
        statusArray3.Add(s1);

        s1 = new Status();
        s1.errorState = "01 00";
        s1.causeOfDown = "SPF trouble\n";
        s1.causeOfDown += "Staple trouble\n";
        s1.causeOfDown += "Stapler jam\n";
        s1.causeOfDown += "Stapler empty\n";
        statusArray3.Add(s1);

        s1 = new Status();
        s1.errorState = "20 00";
        s1.causeOfDown = "Toner Low\n";
        statusArray3.Add(s1);

        s1 = new Status();
        s1.errorState = "00 02";
        s1.causeOfDown = "Maintenance required\n";
        statusArray3.Add(s1);

    }

    //Set Cause of Warning(array of 2);
    public void InfoSet2()
    {
        statusArray2.Clear();
        Status s1 = new Status();
        //chen add
        s1.deviceStatus = 0;
        //
        s1.printerStatus = 4;
        s1.causeOfDown = "Receiving data\n";
        s1.causeOfDown += "Printing in progress\n";
        statusArray2.Add(s1);

        s1 = new Status();
        //chen add
        s1.deviceStatus = 0;
        //
        s1.printerStatus = 1;
        s1.causeOfDown = "Power save\n";
        statusArray2.Add(s1);

        s1 = new Status();
        //chen add
        s1.deviceStatus = 0;
        //
        s1.printerStatus = 5;
        s1.causeOfDown = "Warming up\n";
        statusArray2.Add(s1);

        s1 = new Status();
        //chen add
        s1.deviceStatus = 0;
        //
        s1.printerStatus = 3;
        s1.causeOfDown = "READY\n";
        statusArray2.Add(s1);
    }

    public void InitErrorBitCode()
    {
        errorBitList = new List<ErrorBitCode>();

        ErrorBitCode errBit = new ErrorBitCode();
        errBit.errorBitCode = "F";
        errBit.AddErrorBitCode("8");
        errBit.AddErrorBitCode("4");
        errBit.AddErrorBitCode("2");
        errBit.AddErrorBitCode("1");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "E";
        errBit.AddErrorBitCode("8");
        errBit.AddErrorBitCode("4");
        errBit.AddErrorBitCode("2");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "D";
        errBit.AddErrorBitCode("8");
        errBit.AddErrorBitCode("4");
        errBit.AddErrorBitCode("1");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "C";
        errBit.AddErrorBitCode("8");
        errBit.AddErrorBitCode("4");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "B";
        errBit.AddErrorBitCode("8");
        errBit.AddErrorBitCode("2");
        errBit.AddErrorBitCode("1");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "A";
        errBit.AddErrorBitCode("8");
        errBit.AddErrorBitCode("2");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "9";
        errBit.AddErrorBitCode("8");
        errBit.AddErrorBitCode("1");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "8";
        errBit.AddErrorBitCode("8");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "7";
        errBit.AddErrorBitCode("4");
        errBit.AddErrorBitCode("2");
        errBit.AddErrorBitCode("1");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "6";
        errBit.AddErrorBitCode("4");
        errBit.AddErrorBitCode("2");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "5";
        errBit.AddErrorBitCode("4");
        errBit.AddErrorBitCode("1");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "4";
        errBit.AddErrorBitCode("4");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "3";
        errBit.AddErrorBitCode("2");
        errBit.AddErrorBitCode("1");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "2";
        errBit.AddErrorBitCode("2");
        errorBitList.Add(errBit);

        errBit = new ErrorBitCode();
        errBit.errorBitCode = "1";
        errBit.AddErrorBitCode("1");
        errorBitList.Add(errBit);

    }

    private string getIPStatus(Boolean status)
    {
        if (status == false)
        {
            return "脱机";
        }
        else
        {
            return "联机";
        }
    }
    public void Check(string MFPip, int TonerThreshold, int PaperThreshold)
    {

        InfoSet();
        InfoSet1();
        InfoSet2();
        InitErrorBitCode();

        try
        {
            //1、故障检测：测试打印机的ip是否在线；
            bool status;
            status = MFPonlineCheck(MFPip);

            //chen add start
            //联机情况 标题行
            TableRow tRow_t1 = new TableRow();
            tRow_t1.BackColor = System.Drawing.Color.Gray;
            //tRow2.HorizontalAlign = HorizontalAlign.Center;
            //tRow2.VerticalAlign = VerticalAlign.Middle;
            Table1.Rows.Add(tRow_t1);

            //第一行第一列
            TableCell tCell_t11 = new TableCell();
            tCell_t11.Text = "联机状态";
            //tCell2.Height = 50;
            tCell_t11.Width = CELL_W_COLUM_1;
            tCell_t11.HorizontalAlign = HorizontalAlign.Left;
            tCell_t11.VerticalAlign = VerticalAlign.Middle;
            tRow_t1.Cells.Add(tCell_t11);
            //Table1.Rows.Add(tRow_t1);

            //第一行第二列
            TableCell tCell_t12 = new TableCell();
            tCell_t12.Text = "状态";
            tCell_t12.Width = CELL_W_COLUM_2;
            tCell_t12.HorizontalAlign = HorizontalAlign.Center;
            tCell_t12.VerticalAlign = VerticalAlign.Middle;
            tRow_t1.Cells.Add(tCell_t12);
            //Table1.Rows.Add(tRow_t1);

            //第一行第三列
            TableCell tCell_t13 = new TableCell();
            tCell_t13.Text = "";
            tCell_t13.Width = CELL_W_COLUM_3;
            tCell_t13.HorizontalAlign = HorizontalAlign.Center;
            tCell_t13.VerticalAlign = VerticalAlign.Middle;
            tRow_t1.Cells.Add(tCell_t13);
            //Table1.Rows.Add(tRow_t1);


            //第一行第四列
            TableCell tCell_t14 = new TableCell();
            tCell_t14.Text = "";
            tCell_t14.Width = CELL_W_COLUM_3;
            tCell_t14.HorizontalAlign = HorizontalAlign.Center;
            tCell_t14.VerticalAlign = VerticalAlign.Middle;
            tRow_t1.Cells.Add(tCell_t14);
            //Table1.Rows.Add(tRow_t1);

            //chen add end

            //第二行
            TableRow tRow = new TableRow();
            tRow.BackColor = System.Drawing.Color.LightSteelBlue;
            Table1.Rows.Add(tRow);
            //第二行第一列
            TableCell tCell = new TableCell();
            tCell.Text = "联机情况";
            tCell.Width = CELL_W_COLUM_1;
            //tCell.BackColor = System.Drawing.Color.LightSteelBlue;
            tCell.HorizontalAlign = HorizontalAlign.Right;
            tCell.VerticalAlign = VerticalAlign.Bottom;
            tRow.Cells.Add(tCell);
            //tCell.Height = 20;
            //tRow.HorizontalAlign = HorizontalAlign.Center;
            //tRow.VerticalAlign = VerticalAlign.Middle;
            Table1.Rows.Add(tRow);
            Table1.HorizontalAlign = HorizontalAlign.Justify;
            Table1.CellSpacing = 15;

            //第二行第二列
            TableCell tCell1 = new TableCell();
            tCell1.Text = getIPStatus(status);
            //tCell.BackColor = System.Drawing.Color.LightCyan;

            tCell1.Width = CELL_W_COLUM_2;
            tCell1.HorizontalAlign = HorizontalAlign.Center;
            tCell1.VerticalAlign = VerticalAlign.Bottom;
            tRow.Cells.Add(tCell1);
            //Table1.Rows.Add(tRow);

            //第二行第三列
            TableCell tCell_t23 = new TableCell();
            tCell_t23.Text = "";
            tCell_t23.Width = CELL_W_COLUM_3;
            tCell_t23.HorizontalAlign = HorizontalAlign.Center;
            tCell_t23.VerticalAlign = VerticalAlign.Middle;
            tRow.Cells.Add(tCell_t23);
            //Table1.Rows.Add(tRow_t1);

            //第二行第四列
            TableCell tCell_t24 = new TableCell();
            tCell_t24.Text = "";
            tCell_t24.Width = CELL_W_COLUM_3;
            tCell_t24.HorizontalAlign = HorizontalAlign.Center;
            tCell_t24.VerticalAlign = VerticalAlign.Middle;
            tRow.Cells.Add(tCell_t24);
            //Table1.Rows.Add(tRow_t1);

            if (status == false)
            {
                return;
            }
            //第三行
            TableRow tRow2 = new TableRow();
            tRow2.BackColor = System.Drawing.Color.Gray;
            Table1.Rows.Add(tRow2);
            //tRow2.HorizontalAlign = HorizontalAlign.Center;
            //tRow2.VerticalAlign = VerticalAlign.Middle;
            //第三行第一列
            TableCell tCell2 = new TableCell();
            tCell2.Text = "墨粉状态";
            //tCell2.Height = 50;
            tCell2.Width = CELL_W_COLUM_1;
            tCell2.HorizontalAlign = HorizontalAlign.Left;
            tCell2.VerticalAlign = VerticalAlign.Middle;
            tRow2.Cells.Add(tCell2);
            //Table1.Rows.Add(tRow2);

            //第三行第二列
            TableCell tCell4 = new TableCell();
            tCell4.Text = "墨粉数量";
            tCell4.Width = CELL_W_COLUM_2;
            tCell4.HorizontalAlign = HorizontalAlign.Center;
            tCell4.VerticalAlign = VerticalAlign.Middle;
            tRow2.Cells.Add(tCell4);
            //Table1.Rows.Add(tRow2);

            //第三行第三列
            TableCell tCell5 = new TableCell();
            tCell5.Text = "状态";
            tCell5.Width = CELL_W_COLUM_3;
            tCell5.HorizontalAlign = HorizontalAlign.Center;
            tCell5.VerticalAlign = VerticalAlign.Middle;
            tRow2.Cells.Add(tCell5);
            //Table1.Rows.Add(tRow2);

            TableCell tCell_3_4 = new TableCell();
            tCell_3_4.Text = "";
            tRow2.Cells.Add(tCell_3_4);
            //Table1.Rows.Add(tRow2);


            //2、墨粉状态检测：测试打印机的墨粉状态；
            //青色墨盒(Cyan Toner)：1.3.6.1.2.1.43.11.1.1.9.1.1；
            
            //判断是黑白机还是彩色机
            string isBlackOid = "1.3.6.1.2.1.43.12.1.1.4.1.1";
            string  strBlack = GetMIBvalue(MFPip, isBlackOid);


            string TonerOid;
            string TonerInstruction;
            int TonerValue;

            if (strBlack.Trim().Equals("cyan"))
            {

                //第四行
                TonerOid = "1.3.6.1.2.1.43.11.1.1.9.1.1";
                TonerInstruction = "青色墨粉";
                TonerValue = int.Parse(GetMIBvalue(MFPip, TonerOid));
                if (TonerValue == -1)
                {
                    TonerValue = 0;
                }

                TableCell tCell6 = new TableCell();

                TableRow tRow3 = new TableRow();
                tRow3.BackColor = System.Drawing.Color.Cyan;
                Table1.Rows.Add(tRow3);

                //第四行第一列 - 青色墨粉
                tCell6.Text = TonerInstruction;
                tCell6.Width = CELL_W_COLUM_1;
                tCell6.HorizontalAlign = HorizontalAlign.Right;
                tCell6.VerticalAlign = VerticalAlign.Middle;
                tRow3.Cells.Add(tCell6);
                //tRow3.HorizontalAlign = HorizontalAlign.Center;
                //tRow3.VerticalAlign = VerticalAlign.Middle;
                //Table1.Rows.Add(tRow3);

                //第四行第二列 - 青色墨粉的数量
                TableCell tCell7 = new TableCell();
                //tCell7.Text = TonerValue.ToString() + PERCENT_MARK;
                tCell7.Text = getInkValueShow(TonerValue);
                tCell7.Width = CELL_W_COLUM_2;
                tCell7.HorizontalAlign = HorizontalAlign.Center;
                tCell7.VerticalAlign = VerticalAlign.Middle;
                tRow3.Cells.Add(tCell7);
                //Table1.Rows.Add(tRow3);

                //第四行第三列 - 青色墨粉的状态
                //检测青色墨盒的阈值;
                TableCell tCell8 = new TableCell();
                tCell8.Text = getInkStatus(TonerValue, TonerThreshold);
                tCell8.Width = CELL_W_COLUM_3;
                tCell8.HorizontalAlign = HorizontalAlign.Center;
                tCell8.VerticalAlign = VerticalAlign.Middle;
                tRow3.Cells.Add(tCell8);
                //Table1.Rows.Add(tRow3);

                TableCell tCell_4_4 = new TableCell();
                tCell_4_4.Text = "";
                tRow3.Cells.Add(tCell_4_4);
                //Table1.Rows.Add(tRow3);

                //if (TonerValue < TonerThreshold)
                //{
                //    TableCell tCell8 = new TableCell();
                //    tCell8.Text = "墨量低";
                //    tCell8.Width = CELL_W_COLUM_3;
                //    tCell8.HorizontalAlign = HorizontalAlign.Center;
                //    tCell8.VerticalAlign = VerticalAlign.Middle;
                //    tRow3.Cells.Add(tCell8);
                //    Table1.Rows.Add(tRow3);
                //}
                //else
                //{
                //    TableCell tCell8 = new TableCell();
                //    tCell8.Text = "墨量正常";
                //    tCell8.Width = CELL_W_COLUM_3;
                //    tCell8.HorizontalAlign = HorizontalAlign.Center;
                //    tCell8.VerticalAlign = VerticalAlign.Middle;
                //    tRow3.Cells.Add(tCell8);
                //    Table1.Rows.Add(tRow3);
                //}

                //第五行

                //品红墨盒(Magenta Toner)：1.3.6.1.2.1.43.11.1.1.9.1.2；
                TonerOid = "1.3.6.1.2.1.43.11.1.1.9.1.2";
                TonerInstruction = "品红墨粉";
                TonerValue = int.Parse(GetMIBvalue(MFPip, TonerOid));
                if (TonerValue == -1)
                {
                    TonerValue = 0;
                }
                TableRow tRow4 = new TableRow();
                tRow4.BackColor = System.Drawing.Color.RosyBrown;
                Table1.Rows.Add(tRow4);

                //第五行第一列 - 品红墨粉
                TableCell tCell9 = new TableCell();
                tCell9.Text = TonerInstruction;
                tCell9.Width = CELL_W_COLUM_1;
                tCell9.HorizontalAlign = HorizontalAlign.Right;
                tCell9.VerticalAlign = VerticalAlign.Middle;
                tRow4.Cells.Add(tCell9);
                //tRow4.HorizontalAlign = HorizontalAlign.Center;
                //tRow4.VerticalAlign = VerticalAlign.Middle;
                //Table1.Rows.Add(tRow4);

                //第五行第二列 - 品红墨粉的数量
                TableCell tCell10 = new TableCell();
                tCell10.Text = getInkValueShow(TonerValue); //TonerValue.ToString() + PERCENT_MARK;
                tCell10.Width = CELL_W_COLUM_2;
                tCell10.HorizontalAlign = HorizontalAlign.Center;
                tCell10.VerticalAlign = VerticalAlign.Middle;
                tRow4.Cells.Add(tCell10);
                //Table1.Rows.Add(tRow4);

                //第五行第三列 - 品红墨粉的状态
                //检测品红墨盒的阈值;

                TableCell tCell11 = new TableCell();
                tCell11.Text = this.getInkStatus(TonerValue, TonerThreshold);
                tCell11.Width = CELL_W_COLUM_3;
                tCell11.HorizontalAlign = HorizontalAlign.Center;
                tCell11.VerticalAlign = VerticalAlign.Middle;
                tRow4.Cells.Add(tCell11);
                //Table1.Rows.Add(tRow4);

                TableCell tCell_5_4 = new TableCell();
                tCell_5_4.Text = "";
                tRow4.Cells.Add(tCell_5_4);
                //Table1.Rows.Add(tRow4);

                //if (TonerValue < TonerThreshold)
                //{
                //    TableCell tCell11 = new TableCell();
                //    tCell11.Text = "墨量低";
                //    tCell11.Width = CELL_W_COLUM_3;
                //    tCell11.HorizontalAlign = HorizontalAlign.Center;
                //    tCell11.VerticalAlign = VerticalAlign.Middle;
                //    tRow4.Cells.Add(tCell11);
                //    Table1.Rows.Add(tRow4);
                //}
                //else
                //{
                //    TableCell tCell11 = new TableCell();
                //    tCell11.Text = "墨量正常";
                //    tCell11.Width = CELL_W_COLUM_3;
                //    tCell11.HorizontalAlign = HorizontalAlign.Center;
                //    tCell11.VerticalAlign = VerticalAlign.Middle;
                //    tRow4.Cells.Add(tCell11);
                //    Table1.Rows.Add(tRow4);
                //}

                //第六行
                //黄色墨盒(Yellow Toner)：1.3.6.1.2.1.43.11.1.1.9.3；
                TonerOid = "1.3.6.1.2.1.43.11.1.1.9.1.3";
                TonerInstruction = "黄色墨粉";
                TonerValue = int.Parse(GetMIBvalue(MFPip, TonerOid));
                if (TonerValue == -1)
                {
                    TonerValue = 0;
                }
                TableRow tRow5 = new TableRow();
                tRow5.BackColor = System.Drawing.Color.Yellow;
                Table1.Rows.Add(tRow5);
                //第六行第一列 - 黄色墨粉
                TableCell tCell12 = new TableCell();
                tCell12.Text = TonerInstruction;
                tCell12.Width = CELL_W_COLUM_1;

                tCell12.HorizontalAlign = HorizontalAlign.Right;
                tCell12.VerticalAlign = VerticalAlign.Middle;
                tRow5.Cells.Add(tCell12);
                //tRow5.HorizontalAlign = HorizontalAlign.Center;
                //tRow5.VerticalAlign = VerticalAlign.Middle;
                //Table1.Rows.Add(tRow5);

                //第六行第二列 - 黄色墨粉的数量
                TableCell tCell13 = new TableCell();
                tCell13.Text = getInkValueShow(TonerValue); //TonerValue.ToString() + PERCENT_MARK;
                tCell13.Width = CELL_W_COLUM_2;

                tCell13.HorizontalAlign = HorizontalAlign.Center;
                tCell13.VerticalAlign = VerticalAlign.Middle;
                tRow5.Cells.Add(tCell13);
                //Table1.Rows.Add(tRow5);

                //检测黄色墨盒的阈值;
                //第六行第三列 - 黄色墨粉的状态
                TableCell tCell14 = new TableCell();
                tCell14.Text = this.getInkStatus(TonerValue, TonerThreshold);
                tCell14.Width = CELL_W_COLUM_3;

                tCell14.HorizontalAlign = HorizontalAlign.Center;
                tCell14.VerticalAlign = VerticalAlign.Middle;
                tRow5.Cells.Add(tCell14);
                //Table1.Rows.Add(tRow5);

                TableCell tCell_6_4 = new TableCell();
                tCell_6_4.Text = "";
                tRow5.Cells.Add(tCell_6_4);
                //Table1.Rows.Add(tRow5);

                //if (TonerValue < TonerThreshold)
                //{
                //    TableCell tCell14 = new TableCell();
                //    tCell14.Text = "墨量低";
                //    tCell14.Width = CELL_W_COLUM_3;

                //    tCell14.HorizontalAlign = HorizontalAlign.Center;
                //    tCell14.VerticalAlign = VerticalAlign.Middle;
                //    tRow5.Cells.Add(tCell14);
                //    Table1.Rows.Add(tRow5);
                //}
                //else
                //{
                //    TableCell tCell14 = new TableCell();
                //    tCell14.Text = "墨量正常";
                //    tCell14.Width = CELL_W_COLUM_3;

                //    tCell14.HorizontalAlign = HorizontalAlign.Center;
                //    tCell14.VerticalAlign = VerticalAlign.Middle;
                //    tRow5.Cells.Add(tCell14);
                //    Table1.Rows.Add(tRow5);
                //}


                //第七行
                //黑色墨盒(Black Toner)：1.3.6.1.2.1.43.11.1.1.9.1.4；
                TonerOid = "1.3.6.1.2.1.43.11.1.1.9.1.4";
                TonerInstruction = "黑色墨粉";
                TonerValue = int.Parse(GetMIBvalue(MFPip, TonerOid));
                if (TonerValue == -1)
                {
                    TonerValue = 0;
                }
                TableRow tRow6 = new TableRow();
                tRow6.BackColor = System.Drawing.Color.LightGray;
                Table1.Rows.Add(tRow6);
                //第七行第一列 - 黑色墨粉
                TableCell tCell15 = new TableCell();
                tCell15.Text = TonerInstruction;
                tCell15.Width = CELL_W_COLUM_1;

                tCell15.HorizontalAlign = HorizontalAlign.Right;
                tCell15.VerticalAlign = VerticalAlign.Middle;
                tRow6.Cells.Add(tCell15);
                //tRow4.HorizontalAlign = HorizontalAlign.Center;
                //tRow4.VerticalAlign = VerticalAlign.Middle;
                //Table1.Rows.Add(tRow6);

                //第七行第二列 - 黑色墨粉的数量
                TableCell tCell16 = new TableCell();
                tCell16.Text = getInkValueShow(TonerValue); //TonerValue.ToString() + PERCENT_MARK;
                tCell16.Width = CELL_W_COLUM_2;

                tCell16.HorizontalAlign = HorizontalAlign.Center;
                tCell16.VerticalAlign = VerticalAlign.Middle;
                tRow6.Cells.Add(tCell16);
                //Table1.Rows.Add(tRow6);

                //第七行第三列 - 黑色墨粉的状态
                //检测黑色墨盒的阈值;
                TableCell tCell17 = new TableCell();
                tCell17.Text = this.getInkStatus(TonerValue, TonerThreshold);
                tCell17.Width = CELL_W_COLUM_3;

                tCell17.HorizontalAlign = HorizontalAlign.Center;
                tCell17.VerticalAlign = VerticalAlign.Middle;
                tRow6.Cells.Add(tCell17);
                //Table1.Rows.Add(tRow6);

                TableCell tCell_7_4 = new TableCell();
                tCell_7_4.Text = "";
                tRow6.Cells.Add(tCell_7_4);
                //Table1.Rows.Add(tRow6);

                //if (TonerValue < TonerThreshold)
                //{
                //    TableCell tCell17 = new TableCell();
                //    tCell17.Text = "墨量低";
                //    tCell17.Width = CELL_W_COLUM_3;

                //    tCell17.HorizontalAlign = HorizontalAlign.Center;
                //    tCell17.VerticalAlign = VerticalAlign.Middle;
                //    tRow6.Cells.Add(tCell17);
                //    Table1.Rows.Add(tRow6);
                //}
                //else
                //{
                //    TableCell tCell17 = new TableCell();
                //    tCell17.Text = "墨量正常";
                //    tCell17.Width = CELL_W_COLUM_3;

                //    tCell17.HorizontalAlign = HorizontalAlign.Center;
                //    tCell17.VerticalAlign = VerticalAlign.Middle;
                //    tRow6.Cells.Add(tCell17);
                //    Table1.Rows.Add(tRow6);
                //}

            } //以上为彩色墨盒状态
            else
            {

                //第七行
                //黑色墨盒(Black Toner)：1.3.6.1.2.1.43.11.1.1.9.1.4；
                TonerOid = "1.3.6.1.2.1.43.11.1.1.9.1.1";
                TonerInstruction = "黑色墨粉";
                TonerValue = int.Parse(GetMIBvalue(MFPip, TonerOid));
                if (TonerValue == -1)
                {
                    TonerValue = 0;
                }
                TableRow tRow6 = new TableRow();
                tRow6.BackColor = System.Drawing.Color.LightGray;
                Table1.Rows.Add(tRow6);
                //第七行第一列 - 黑色墨粉
                TableCell tCell15 = new TableCell();
                tCell15.Text = TonerInstruction;
                tCell15.Width = CELL_W_COLUM_1;

                tCell15.HorizontalAlign = HorizontalAlign.Right;
                tCell15.VerticalAlign = VerticalAlign.Middle;
                tRow6.Cells.Add(tCell15);
                //tRow4.HorizontalAlign = HorizontalAlign.Center;
                //tRow4.VerticalAlign = VerticalAlign.Middle;
                //Table1.Rows.Add(tRow6);

                //第七行第二列 - 黑色墨粉的数量
                TableCell tCell16 = new TableCell();
                tCell16.Text = getInkValueShow(TonerValue); //TonerValue.ToString() + PERCENT_MARK;
                tCell16.Width = CELL_W_COLUM_2;

                tCell16.HorizontalAlign = HorizontalAlign.Center;
                tCell16.VerticalAlign = VerticalAlign.Middle;
                tRow6.Cells.Add(tCell16);
                //Table1.Rows.Add(tRow6);

                //第七行第三列 - 黑色墨粉的状态
                //检测黑色墨盒的阈值;
                TableCell tCell17 = new TableCell();
                tCell17.Text = this.getInkStatus(TonerValue, TonerThreshold);
                tCell17.Width = CELL_W_COLUM_3;

                tCell17.HorizontalAlign = HorizontalAlign.Center;
                tCell17.VerticalAlign = VerticalAlign.Middle;
                tRow6.Cells.Add(tCell17);
                //Table1.Rows.Add(tRow6);

                TableCell tCell_7_4 = new TableCell();
                tCell_7_4.Text = "";
                tRow6.Cells.Add(tCell_7_4);
                //Table1.Rows.Add(tRow6);

                //if (TonerValue < TonerThreshold)
                //{
                //    TableCell tCell17 = new TableCell();
                //    tCell17.Text = "墨量低";
                //    tCell17.Width = CELL_W_COLUM_3;

                //    tCell17.HorizontalAlign = HorizontalAlign.Center;
                //    tCell17.VerticalAlign = VerticalAlign.Middle;
                //    tRow6.Cells.Add(tCell17);
                //    Table1.Rows.Add(tRow6);
                //}
                //else
                //{
                //    TableCell tCell17 = new TableCell();
                //    tCell17.Text = "墨量正常";
                //    tCell17.Width = CELL_W_COLUM_3;

                //    tCell17.HorizontalAlign = HorizontalAlign.Center;
                //    tCell17.VerticalAlign = VerticalAlign.Middle;
                //    tRow6.Cells.Add(tCell17);
                //    Table1.Rows.Add(tRow6);
                //}
            }

            //第八行
            TableRow tRow7 = new TableRow();
            tRow7.BackColor = System.Drawing.Color.Gray;
            Table1.Rows.Add(tRow7);
            //第八行标题行 第一列 - 纸盒状态
            TableCell tCell18 = new TableCell();
            tCell18.Text = "纸盒名称";
            //tCell18.Height = 50;
            tCell18.Width = CELL_W_COLUM_1;

            tCell18.HorizontalAlign = HorizontalAlign.Left;
            tCell18.VerticalAlign = VerticalAlign.Middle;
            tRow7.Cells.Add(tCell18);
            //tRow4.HorizontalAlign = HorizontalAlign.Center;
            //tRow4.VerticalAlign = VerticalAlign.Middle;
            //Table1.Rows.Add(tRow7);

            //第八行标题行 第二列 - 纸张尺寸
            TableCell tCell18_2 = new TableCell();
            tCell18_2.Text = "纸张尺寸";
            //tCell18.Height = 50;
            tCell18_2.Width = CELL_W_COLUM_1;

            tCell18_2.HorizontalAlign = HorizontalAlign.Left;
            tCell18_2.VerticalAlign = VerticalAlign.Middle;
            tRow7.Cells.Add(tCell18_2);
            //tRow4.HorizontalAlign = HorizontalAlign.Center;
            //tRow4.VerticalAlign = VerticalAlign.Middle;
            //Table1.Rows.Add(tRow7);


            //第八行标题行 第二列 - 纸张数量
            TableCell tCell19 = new TableCell();
            tCell19.Text = "纸张数量";
            tCell19.Width = CELL_W_COLUM_2;

            tCell19.HorizontalAlign = HorizontalAlign.Center;
            tCell19.VerticalAlign = VerticalAlign.Middle;
            tRow7.Cells.Add(tCell19);
            //tRow4.HorizontalAlign = HorizontalAlign.Center;
            //tRow4.VerticalAlign = VerticalAlign.Middle;
            //Table1.Rows.Add(tRow7);

            //第八行标题行 第三列 - 状态
            TableCell tCell20 = new TableCell();
            tCell20.Text = "状态";
            tCell20.Width = CELL_W_COLUM_3;

            tCell20.HorizontalAlign = HorizontalAlign.Center;
            tCell20.VerticalAlign = VerticalAlign.Middle;
            tRow7.Cells.Add(tCell20);
            //tRow4.HorizontalAlign = HorizontalAlign.Center;
            //tRow4.VerticalAlign = VerticalAlign.Middle;
           // Table1.Rows.Add(tRow7);

            //3、纸盒状态检测：测试打印机的纸张状态；
            //Tray1(纸盒1)：
            string TypeoOid = "1.3.6.1.2.1.43.8.2.1.12.1.2";
            string MaxVolumeOid = "1.3.6.1.2.1.43.8.2.1.9.1.2";
            string EstimateVolumeOid = "1.3.6.1.2.1.43.8.2.1.10.1.2";
            string Type = GetMIBvalue(MFPip, TypeoOid);
            string NameOid = "1.3.6.1.2.1.43.8.2.1.13.1.2";
            string Name = GetMIBvalue(MFPip, NameOid);
            if (Type != "-1" && Type != "0" && Name != "Auto Select")
            {
                int tray1MaxValue = int.Parse(GetMIBvalue(MFPip,MaxVolumeOid));
                int tray1VolumeValue = int.Parse(GetMIBvalue(MFPip, EstimateVolumeOid));
                //int tray1EstimateValue = (int)(int.Parse(GetMIBvalue(MFPip, tray1EstimateVolumeOid)) / (tray1MaxValue * 1.0) * 100);
                
                int tray1EstimateValue = 0;
                if (tray1MaxValue != 0 && tray1MaxValue != -1)
                {
                    tray1EstimateValue = (int)( (tray1VolumeValue ) / (tray1MaxValue * 1.0) * 100);
                }
                if (tray1VolumeValue == -2)
                {
                    tray1EstimateValue = -2;
                }
                //纸盒明细第一行 纸盒一
                TableRow tRow8 = new TableRow();
                tRow8.BackColor = System.Drawing.Color.LightSteelBlue;
                Table1.Rows.Add(tRow8);
                //纸盒明细第一行第一列 纸盒名称
                TableCell tCell21 = new TableCell();
                tCell21.Text = getPaperName(Name);
                tCell21.Width = CELL_W_COLUM_1;

                tCell21.HorizontalAlign = HorizontalAlign.Right;
                tCell21.VerticalAlign = VerticalAlign.Middle;
                tRow8.Cells.Add(tCell21);
                //tRow7.HorizontalAlign = HorizontalAlign.Center;
                //tRow7.VerticalAlign = VerticalAlign.Middle;
                //Table1.Rows.Add(tRow8);

                //纸盒明细第一行第二列 纸张尺寸
                TableCell tCell9_2 = new TableCell();
                tCell9_2.Text = getPaperSize(Type);
                tCell9_2.Width = CELL_W_COLUM_2;

                tCell9_2.HorizontalAlign = HorizontalAlign.Center;
                tCell9_2.VerticalAlign = VerticalAlign.Middle;
                tRow8.Cells.Add(tCell9_2);
                //Table1.Rows.Add(tRow8);

                //纸盒明细第一行第三列 纸盒数量
                TableCell tCell22 = new TableCell();
                tCell22.Text = getPaperNumShow(tray1EstimateValue);
                tCell22.Width = CELL_W_COLUM_2;

                tCell22.HorizontalAlign = HorizontalAlign.Center;
                tCell22.VerticalAlign = VerticalAlign.Middle;
                tRow8.Cells.Add(tCell22);
                //Table1.Rows.Add(tRow8);

                //纸盒明细第一行第四列 纸盒状态
                //检测纸盒1的阈值;

                    TableCell tCell23 = new TableCell();
                    tCell23.Text = this.getPaperStatus(tray1EstimateValue , PaperThreshold);
                    tCell23.Width = CELL_W_COLUM_3;

                    tCell23.HorizontalAlign = HorizontalAlign.Center;
                    tCell23.VerticalAlign = VerticalAlign.Middle;
                    tRow8.Cells.Add(tCell23);
                    //Table1.Rows.Add(tRow8);


            //    if (tray1EstimateValue < PaperThreshold)
            //    {
            //        TableCell tCell23 = new TableCell();
            //        tCell23.Text = "纸量少";
            //        tCell23.Width = CELL_W_COLUM_3;

            //        tCell23.HorizontalAlign = HorizontalAlign.Center;
            //        tCell23.VerticalAlign = VerticalAlign.Middle;
            //        tRow8.Cells.Add(tCell23);
            //        Table1.Rows.Add(tRow8);
            //    }
            //    else
            //    {
            //        TableCell tCell23 = new TableCell();
            //        tCell23.Text = "纸量正常";
            //        tCell23.Width = CELL_W_COLUM_3;

            //        tCell23.HorizontalAlign = HorizontalAlign.Center;
            //        tCell23.VerticalAlign = VerticalAlign.Middle;
            //        tRow8.Cells.Add(tCell23);
            //        Table1.Rows.Add(tRow8);
            //    }
            }
            //Tray2(纸盒2)：
            TypeoOid = "1.3.6.1.2.1.43.8.2.1.12.1.3";
            MaxVolumeOid = "1.3.6.1.2.1.43.8.2.1.9.1.3";
            EstimateVolumeOid = "1.3.6.1.2.1.43.8.2.1.10.1.3";
            Type = GetMIBvalue(MFPip, TypeoOid);
            NameOid = "1.3.6.1.2.1.43.8.2.1.13.1.3";
            Name = GetMIBvalue(MFPip, NameOid);

            if (Type != "-1" && Type != "0" && Name != "Auto Select")
            {
                int tray2MaxValue = int.Parse(GetMIBvalue(MFPip, MaxVolumeOid));
                int tray2VolumeValue = int.Parse(GetMIBvalue(MFPip, EstimateVolumeOid));
                //int tray2EstimateValue = (int)(int.Parse(GetMIBvalue(MFPip, tray2EstimateVolumeOid)) / (tray2MaxValue * 1.0) * 100);
                int tray2EstimateValue = 0;
                if (tray2MaxValue != 0 && tray2MaxValue != -1)
                {
                    tray2EstimateValue = (int)(tray2VolumeValue / (tray2MaxValue * 1.0) * 100);
                }
                if (tray2VolumeValue == -2)
                {
                    tray2EstimateValue = -2;
                }

                //纸盒明细第二行 纸盒二
                TableRow tRow9 = new TableRow();
                tRow9.BackColor = System.Drawing.Color.LightSteelBlue;
                Table1.Rows.Add(tRow9);
                //纸盒明细第二行第一列 纸盒二名称
                TableCell tCell24 = new TableCell();
                tCell24.Text = getPaperName(Name); 
                tCell24.Width = CELL_W_COLUM_1;

                tCell24.HorizontalAlign = HorizontalAlign.Right;
                tCell24.VerticalAlign = VerticalAlign.Middle;
                tRow9.Cells.Add(tCell24);
                //tRow6.HorizontalAlign = HorizontalAlign.Center;
                //tRow6.VerticalAlign = VerticalAlign.Middle;
                //Table1.Rows.Add(tRow9);

                //纸盒明细第一行第二列 纸张尺寸
                TableCell tCell10_2 = new TableCell();
                tCell10_2.Text = getPaperSize(Type);
                tCell10_2.Width = CELL_W_COLUM_2;

                tCell10_2.HorizontalAlign = HorizontalAlign.Center;
                tCell10_2.VerticalAlign = VerticalAlign.Middle;
                tRow9.Cells.Add(tCell10_2);
               // Table1.Rows.Add(tRow9);


                //纸盒明细第二行第三列 纸盒二数量
                TableCell tCell25 = new TableCell();
                tCell25.Text = getPaperNumShow(tray2EstimateValue);
                tCell25.Width = CELL_W_COLUM_2;

                tCell25.HorizontalAlign = HorizontalAlign.Center;
                tCell25.VerticalAlign = VerticalAlign.Middle;
                tRow9.Cells.Add(tCell25);
                //Table1.Rows.Add(tRow9);

                //纸盒明细第二行第四列 纸盒二状态
                //检测纸盒2的阈值;

                    TableCell tCell26 = new TableCell();
                    tCell26.Text = this.getPaperStatus(tray2EstimateValue , PaperThreshold);
                    tCell26.Width = CELL_W_COLUM_3;

                    tCell26.HorizontalAlign = HorizontalAlign.Center;
                    tCell26.VerticalAlign = VerticalAlign.Middle;
                    tRow9.Cells.Add(tCell26);
                   // Table1.Rows.Add(tRow9);
                
                
                //if (tray2EstimateValue < PaperThreshold)
                //{
                //    TableCell tCell26 = new TableCell();
                //    tCell26.Text = "纸量少";
                //    tCell26.Width = CELL_W_COLUM_3;

                //    tCell26.HorizontalAlign = HorizontalAlign.Center;
                //    tCell26.VerticalAlign = VerticalAlign.Middle;
                //    tRow9.Cells.Add(tCell26);
                //    Table1.Rows.Add(tRow9);
                //}
                //else
                //{
                //    TableCell tCell26 = new TableCell();
                //    tCell26.Text = "纸量正常";
                //    tCell26.Width = CELL_W_COLUM_3;

                //    tCell26.HorizontalAlign = HorizontalAlign.Center;
                //    tCell26.VerticalAlign = VerticalAlign.Middle;
                //    tRow9.Cells.Add(tCell26);
                //    Table1.Rows.Add(tRow9);
                //}
            }
            //Tray3(纸盒3)：
            TypeoOid = "1.3.6.1.2.1.43.8.2.1.12.1.4";
            MaxVolumeOid = "1.3.6.1.2.1.43.8.2.1.9.1.4";
            EstimateVolumeOid = "1.3.6.1.2.1.43.8.2.1.10.1.4";
            Type = GetMIBvalue(MFPip, TypeoOid);
            NameOid = "1.3.6.1.2.1.43.8.2.1.13.1.4";
            Name = GetMIBvalue(MFPip, NameOid);

            if (Type != "-1" && Type != "0" && Name != "Auto Select")
            {
                int tray3MaxValue = int.Parse(GetMIBvalue(MFPip, MaxVolumeOid));
                int tray3VolumeValue = int.Parse(GetMIBvalue(MFPip, EstimateVolumeOid));
                //int tray3EstimateValue = (int)(int.Parse(GetMIBvalue(MFPip, tray3EstimateVolumeOid)) / (tray3MaxValue * 1.0) * 100);
                int tray3EstimateValue = 0;
                if (tray3MaxValue != 0 && tray3MaxValue != -1)
                {
                    tray3EstimateValue = (int)(tray3VolumeValue / (tray3MaxValue * 1.0) * 100);
                }
                if (tray3VolumeValue == -2)
                {
                    tray3EstimateValue = -2;
                }


                //纸盒明细第三行 纸盒三
                TableRow tRow10 = new TableRow();
                tRow10.BackColor = System.Drawing.Color.LightSteelBlue;
                Table1.Rows.Add(tRow10);
                //纸盒明细第三行第一列 纸盒三名称
                TableCell tCell27 = new TableCell();
                tCell27.Text = getPaperName(Name); 
                tCell27.Width = CELL_W_COLUM_1;

                tCell27.HorizontalAlign = HorizontalAlign.Right;
                tCell27.VerticalAlign = VerticalAlign.Middle;
                tRow10.Cells.Add(tCell27);
                //tRow10.HorizontalAlign = HorizontalAlign.Center;
                //tRow10.VerticalAlign = VerticalAlign.Middle;
                //Table1.Rows.Add(tRow10);


                //纸盒明细第一行第二列 纸张尺寸
                TableCell tCell11_2 = new TableCell();
                tCell11_2.Text = getPaperSize(Type);
                tCell11_2.Width = CELL_W_COLUM_2;

                tCell11_2.HorizontalAlign = HorizontalAlign.Center;
                tCell11_2.VerticalAlign = VerticalAlign.Middle;
                tRow10.Cells.Add(tCell11_2);
                //Table1.Rows.Add(tRow10);

                //纸盒明细第三行第三列 纸盒三数量
                TableCell tCell28 = new TableCell();
                tCell28.Text = getPaperNumShow(tray3EstimateValue);
                tCell28.Width = CELL_W_COLUM_2;

                tCell28.HorizontalAlign = HorizontalAlign.Center;
                tCell28.VerticalAlign = VerticalAlign.Middle;
                tRow10.Cells.Add(tCell28);
                //Table1.Rows.Add(tRow10);

                //纸盒明细第三行第四列 纸盒三状态
                //检测纸盒3的阈值
                TableCell tCell29 = new TableCell();
                tCell29.Text = this.getPaperStatus(tray3EstimateValue , PaperThreshold);
                tCell29.Width = CELL_W_COLUM_3;

                tCell29.HorizontalAlign = HorizontalAlign.Center;
                tCell29.VerticalAlign = VerticalAlign.Middle;
                tRow10.Cells.Add(tCell29);
                //Table1.Rows.Add(tRow10);

                
                //if (tray3EstimateValue < PaperThreshold)
                //{
                //    TableCell tCell29 = new TableCell();
                //    tCell29.Text = "纸量少";
                //    tCell29.Width = CELL_W_COLUM_3;

                //    tCell29.HorizontalAlign = HorizontalAlign.Center;
                //    tCell29.VerticalAlign = VerticalAlign.Middle;
                //    tRow10.Cells.Add(tCell29);
                //    Table1.Rows.Add(tRow10);
                //}
                //else
                //{
                //    TableCell tCell29 = new TableCell();
                //    tCell29.Text = "纸量正常";
                //    tCell29.Width = CELL_W_COLUM_3;

                //    tCell29.HorizontalAlign = HorizontalAlign.Center;
                //    tCell29.VerticalAlign = VerticalAlign.Middle;
                //    tRow10.Cells.Add(tCell29);
                //    Table1.Rows.Add(tRow10);
                //}
            }

            //Tray4(纸盒4)：
            TypeoOid = "1.3.6.1.2.1.43.8.2.1.12.1.5";
            MaxVolumeOid = "1.3.6.1.2.1.43.8.2.1.9.1.5";
            EstimateVolumeOid = "1.3.6.1.2.1.43.8.2.1.10.1.5";
            Type = GetMIBvalue(MFPip, TypeoOid);
            NameOid = "1.3.6.1.2.1.43.8.2.1.13.1.5";
            Name = GetMIBvalue(MFPip, NameOid);

            if (Type != "-1" && Type != "0" && Name != "Auto Select")
            {
                int tray4VolumeValue = int.Parse(GetMIBvalue(MFPip, EstimateVolumeOid));
                int tray4MaxValue = int.Parse(GetMIBvalue(MFPip, MaxVolumeOid));
                //int tray4EstimateValue = (int)(int.Parse(GetMIBvalue(MFPip, tray4EstimateVolumeOid)) / (tray4MaxValue * 1.0) * 100);

                int tray4EstimateValue = 0;
                if (tray4MaxValue != 0 && tray4MaxValue != -1)
                {
                    tray4EstimateValue = (int)(tray4VolumeValue / (tray4MaxValue * 1.0) * 100);
                }
                if (tray4VolumeValue == -2)
                {
                    tray4EstimateValue = -2;
                }


                //纸盒明细第四行 纸盒四
                TableRow tRow11 = new TableRow();
                tRow11.BackColor = System.Drawing.Color.LightGreen;
                Table1.Rows.Add(tRow11);
                //纸盒明细第四行第一列 纸盒四名称
                TableCell tCell30 = new TableCell();
                tCell30.Text = getPaperName(Name); 
                tCell30.Width = CELL_W_COLUM_1;

                tCell30.HorizontalAlign = HorizontalAlign.Right;
                tCell30.VerticalAlign = VerticalAlign.Middle;
                tRow11.Cells.Add(tCell30);
                //tRow11.HorizontalAlign = HorizontalAlign.Center;
                //tRow11.VerticalAlign = VerticalAlign.Middle;
                //Table1.Rows.Add(tRow11);

                //纸盒明细第四行第二列 纸张尺寸
                TableCell tCell12_2 = new TableCell();
                tCell12_2.Text = getPaperSize(Type);
                tCell12_2.Width = CELL_W_COLUM_2;

                tCell12_2.HorizontalAlign = HorizontalAlign.Center;
                tCell12_2.VerticalAlign = VerticalAlign.Middle;
                tRow11.Cells.Add(tCell12_2);
                //Table1.Rows.Add(tRow11);


                //纸盒明细第四行第三列 纸盒四数量
                TableCell tCell31 = new TableCell();
                tCell31.Text = getPaperNumShow(tray4EstimateValue);
                tCell31.Width = CELL_W_COLUM_2;

                tCell31.HorizontalAlign = HorizontalAlign.Center;
                tCell31.VerticalAlign = VerticalAlign.Middle;
                tRow11.Cells.Add(tCell31);
                //Table1.Rows.Add(tRow11);

                //纸盒明细第四行第四列 纸盒四状态
                //检测纸盒4的阈值;

                TableCell tCell32 = new TableCell();
                tCell32.Text = this.getPaperStatus(tray4EstimateValue , PaperThreshold);
                tCell32.Width = CELL_W_COLUM_3;

                tCell32.HorizontalAlign = HorizontalAlign.Center;
                tCell32.VerticalAlign = VerticalAlign.Middle;
                tRow11.Cells.Add(tCell32);
                //Table1.Rows.Add(tRow11);


                //if (tray4EstimateValue < PaperThreshold)
                //{
                //    TableCell tCell32 = new TableCell();
                //    tCell32.Text = "纸量少";
                //    tCell32.Width = CELL_W_COLUM_3;

                //    tCell32.HorizontalAlign = HorizontalAlign.Center;
                //    tCell32.VerticalAlign = VerticalAlign.Middle;
                //    tRow11.Cells.Add(tCell32);
                //    Table1.Rows.Add(tRow11);
                //}
                //else
                //{
                //    TableCell tCell32 = new TableCell();
                //    tCell32.Text = "纸量正常";
                //    tCell32.Width = CELL_W_COLUM_3;

                //    tCell32.HorizontalAlign = HorizontalAlign.Center;
                //    tCell32.VerticalAlign = VerticalAlign.Middle;
                //    tRow11.Cells.Add(tCell32);
                //    Table1.Rows.Add(tRow11);
                //}
            }

            //20150915 add start


            //Tray4(手送纸盒)：
            TypeoOid = "1.3.6.1.2.1.43.8.2.1.12.1.1";
            MaxVolumeOid = "1.3.6.1.2.1.43.8.2.1.9.1.1";
            EstimateVolumeOid = "1.3.6.1.2.1.43.8.2.1.10.1.1";
            Type = GetMIBvalue(MFPip, TypeoOid);
            NameOid = "1.3.6.1.2.1.43.8.2.1.13.1.1";
            Name = GetMIBvalue(MFPip, NameOid);

            if (Type != "-1" && Type != "0")
            {
                int tray5VolumeValue = int.Parse(GetMIBvalue(MFPip, EstimateVolumeOid));
                int tray5MaxValue = int.Parse(GetMIBvalue(MFPip, MaxVolumeOid));
                //int tray4EstimateValue = (int)(int.Parse(GetMIBvalue(MFPip, tray4EstimateVolumeOid)) / (tray4MaxValue * 1.0) * 100);

                int tray5EstimateValue = 0;
                if (tray5MaxValue != 0 && tray5MaxValue != -1)
                {
                    tray5EstimateValue = (int)(tray5VolumeValue / (tray5MaxValue * 1.0) * 100);
                }
                if (tray5VolumeValue == -2)
                {
                    tray5EstimateValue = -2;
                }


                //纸盒明细第五行 纸盒四
                TableRow tRow501 = new TableRow();
                tRow501.BackColor = System.Drawing.Color.LightGreen;
                Table1.Rows.Add(tRow501);
                //纸盒明细第四行第一列 纸盒四名称
                TableCell tCell500 = new TableCell();
                tCell500.Text = getPaperName(Name);
                tCell500.Width = CELL_W_COLUM_1;

                tCell500.HorizontalAlign = HorizontalAlign.Right;
                tCell500.VerticalAlign = VerticalAlign.Middle;
                tRow501.Cells.Add(tCell500);
                //tRow11.HorizontalAlign = HorizontalAlign.Center;
                //tRow11.VerticalAlign = VerticalAlign.Middle;
                //Table1.Rows.Add(tRow11);

                //纸盒明细第四行第二列 纸张尺寸
                TableCell tCell150_2 = new TableCell();
                tCell150_2.Text = getPaperSize(Type);
                tCell150_2.Width = CELL_W_COLUM_2;

                tCell150_2.HorizontalAlign = HorizontalAlign.Center;
                tCell150_2.VerticalAlign = VerticalAlign.Middle;
                tRow501.Cells.Add(tCell150_2);
                //Table1.Rows.Add(tRow11);


                //纸盒明细第四行第三列 纸盒四数量
                TableCell tCell50_3 = new TableCell();
                if (tray5EstimateValue == -3)
                {
                    tCell50_3.Text = "非空";
                }
                else
                {
                    tCell50_3.Text = "为空";
                }
                tCell50_3.Width = CELL_W_COLUM_2;

                tCell50_3.HorizontalAlign = HorizontalAlign.Center;
                tCell50_3.VerticalAlign = VerticalAlign.Middle;
                tRow501.Cells.Add(tCell50_3);
                //Table1.Rows.Add(tRow11);

                //纸盒明细第四行第四列 纸盒四状态
                //检测纸盒4的阈值;

                TableCell tCell50_4 = new TableCell();
                //tCell50_4.Text = this.getPaperStatus(tray5EstimateValue, PaperThreshold);
                tCell50_4.Text = "未知";
                tCell50_4.Width = CELL_W_COLUM_3;

                tCell50_4.HorizontalAlign = HorizontalAlign.Center;
                tCell50_4.VerticalAlign = VerticalAlign.Middle;
                tRow501.Cells.Add(tCell50_4);
                //Table1.Rows.Add(tRow11);


                //if (tray4EstimateValue < PaperThreshold)
                //{
                //    TableCell tCell32 = new TableCell();
                //    tCell32.Text = "纸量少";
                //    tCell32.Width = CELL_W_COLUM_3;

                //    tCell32.HorizontalAlign = HorizontalAlign.Center;
                //    tCell32.VerticalAlign = VerticalAlign.Middle;
                //    tRow11.Cells.Add(tCell32);
                //    Table1.Rows.Add(tRow11);
                //}
                //else
                //{
                //    TableCell tCell32 = new TableCell();
                //    tCell32.Text = "纸量正常";
                //    tCell32.Width = CELL_W_COLUM_3;

                //    tCell32.HorizontalAlign = HorizontalAlign.Center;
                //    tCell32.VerticalAlign = VerticalAlign.Middle;
                //    tRow11.Cells.Add(tCell32);
                //    Table1.Rows.Add(tRow11);
                //}
            }

            //20150915 add end


            //Add April 10th 2015
            //chen add start
            //故障行
            TableRow tRow_t2 = new TableRow();
            tRow_t2.BackColor = System.Drawing.Color.Gray;
            Table1.Rows.Add(tRow_t2);
            //tRow2.HorizontalAlign = HorizontalAlign.Center;
            //tRow2.VerticalAlign = VerticalAlign.Middle;
            //故障行标题行 第一列 故障状态
            TableCell tCell_t21 = new TableCell();
            tCell_t21.Text = "故障状态";
            //tCell2.Height = 50;
            tCell_t21.Width = CELL_W_COLUM_1;
            tCell_t21.HorizontalAlign = HorizontalAlign.Left;
            tCell_t21.VerticalAlign = VerticalAlign.Middle;
            tRow_t2.Cells.Add(tCell_t21);
            //Table1.Rows.Add(tRow_t2);

            //故障行标题行 第二列 状态
            TableCell tCell_t22 = new TableCell();
            tCell_t22.Text = "状态";
            tCell_t22.Width = 250;
            tCell_t22.HorizontalAlign = HorizontalAlign.Center;
            tCell_t22.VerticalAlign = VerticalAlign.Middle;
            tRow_t2.Cells.Add(tCell_t22);
            //Table1.Rows.Add(tRow_t2);

            //故障行标题行 第三列 空白列
            TableCell tCell_t20_3 = new TableCell();
            tCell_t20_3.Text = "";
            tCell_t20_3.Width = 250;
            tCell_t20_3.HorizontalAlign = HorizontalAlign.Center;
            tCell_t20_3.VerticalAlign = VerticalAlign.Middle;
            tRow_t2.Cells.Add(tCell_t20_3);
            //Table1.Rows.Add(tRow_t2);

            //故障行标题行 第四列 空白列
            TableCell tCell_t20_4 = new TableCell();
            tCell_t20_4.Text = "";
            tCell_t20_4.Width = 250;
            tCell_t20_4.HorizontalAlign = HorizontalAlign.Center;
            tCell_t20_4.VerticalAlign = VerticalAlign.Middle;
            tRow_t2.Cells.Add(tCell_t20_4);
            //Table1.Rows.Add(tRow_t2);

            //chen add end

            //故障行详细行 第一列 情况标题
            TableRow tRow13 = new TableRow();
            tRow13.BackColor = System.Drawing.Color.LightSteelBlue;
            Table1.Rows.Add(tRow13);

            TableCell tCell34 = new TableCell();
            tCell34.Text = "情况";
            tCell34.HorizontalAlign = HorizontalAlign.Right;
            tCell34.VerticalAlign = VerticalAlign.Middle;
            tRow13.Cells.Add(tCell34);
            //tRow12.HorizontalAlign = HorizontalAlign.Center;
            //tRow12.VerticalAlign = VerticalAlign.Middle;
            //Table1.Rows.Add(tRow13);

            int deviceStatus;
            deviceStatus = int.Parse(GetMIBvalue(MFPip, "1.3.6.1.2.1.25.3.2.1.5.1"));

            int printerStatus;
            printerStatus = int.Parse(GetMIBvalue(MFPip, "1.3.6.1.2.1.25.3.5.1.1.1"));

            string errorState;
            errorState = GetMIBvalue(MFPip, "1.3.6.1.2.1.25.3.5.1.2.1");
            //errorState = "F9 70";

            string strErrorMsg = "";
            //strErrorMsg += getErrorMsg(errorState, statusArray5);

            //bool isok = true;

            switch (deviceStatus)
            {
                case 5:
                    {
                        //InfoSet();
                        //if (printerStatus == 1)
                        //{
                        //    foreach (Status s in statusArray5)
                        //    {
                        //        if (s.errorState == errorState)
                        //        {
                        //            isok = false;
                        //            str += s.causeOfDown;
                        //            break;
                        //        }
                        //    }
                        //}
                        if (printerStatus == 1)
                        {
                            strErrorMsg += getErrorMsg(errorState, statusArray5);
                        }
                    }; break;
                case 3:
                    {
                        //InfoSet1();

                        if (printerStatus == 1)
                        {
                            //foreach (Status s in statusArray3)
                            //{
                            //    if (s.errorState == errorState)
                            //    {
                            //        isok = false;
                            //        str += s.causeOfDown;
                            //        break;
                            //    }
                            //}
                            strErrorMsg += getErrorMsg(errorState, statusArray3);

                        }
                        else if (printerStatus == 3)
                        {
                            //foreach (Status s in statusArray3)
                            //{
                            //    if (s.errorState == errorState)
                            //    {
                            //        isok = false;
                            //        str += s.causeOfDown;
                            //        break;
                            //    }
                            //}
                            strErrorMsg += getErrorMsg(errorState, statusArray3);
                        }
                    }; break;
                case 2:
                    {
                        //InfoSet2();

                        //foreach (Status s in statusArray2)
                        //{
                        //    if (s.printerStatus == printerStatus)
                        //    {
                        //        isok = false;
                        //        str += s.causeOfDown;
                        //        break;
                        //    }
                        //}
                        strErrorMsg += getErrorMsg(errorState, statusArray2);
                    }; break;
            }

            //if (isok == true)
            //{
            //    str = "正常";
            //}
            if (strErrorMsg.Equals(""))
            {
                strErrorMsg = "正常";
            }

            //故障行详细行 第二列 故障内容
            TableCell tCell33 = new TableCell();
            tCell33.Text = strErrorMsg;
            tCell33.HorizontalAlign = HorizontalAlign.Center;
            tCell33.VerticalAlign = VerticalAlign.Middle;
            tRow13.Cells.Add(tCell33);
            //tRow12.HorizontalAlign = HorizontalAlign.Center;
            //tRow12.VerticalAlign = VerticalAlign.Middle;
            //Table1.Rows.Add(tRow13);

            TableCell tCell36 = new TableCell();
            tCell36.Text = "";
            tRow13.Cells.Add(tCell36);
            //Table1.Rows.Add(tRow13);

            TableCell tCell36_2 = new TableCell();
            tCell36_2.Text = "";
            tRow13.Cells.Add(tCell36_2);
            //Table1.Rows.Add(tRow13);
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    private string getInkValueShow(int inkvalue)
    {
        if (inkvalue == -2)
        {
            return "未知";
        }

        if (inkvalue == -3)
        {
            return "低";
        }

        return inkvalue.ToString() + PERCENT_MARK;
    }

    private string getInkStatus(int inkvalue, int limit)
    {
        string status = "";
        if (inkvalue == -2)
        {
            return "状态未知";
        }
        if (inkvalue >= 50)
        {
            status = "墨粉充足";
        }
        else if (inkvalue < 50 && inkvalue > limit)
        {
            status = "墨粉正常";
        }
        else
        {
            status = "墨粉量低";
        }
        return status;
    }

    private string getPaperSize(string type)
    {
        string papersize = "";
        if (type.Trim() == "iso-a4-white")
        {
            papersize = "A4";
        }
        else if (type.Trim() == "iso-a3-white")
        {
            papersize = "A3";
        }
        else
        {
            papersize = "未知";
        }
        return papersize;
    }

    private string getPaperNumShow(int papervalue)
    {
        if (papervalue == -2)
        {
            return "未知";
        }
        else
        {
            return papervalue.ToString() + PERCENT_MARK;
        }
    }

    private string getPaperName(string name_type)
    {
        string papersize = "";
        if (name_type.Trim() == "Bypass Tray")
        {
            papersize = "手送纸盒";
        }
        else if (name_type.Trim() == "Auto Select")
        {
            papersize = "Auto Select";
        }
        else if (name_type.Trim() == "Tray 1")
        {
            papersize = "纸盒 1";
        }
        else if (name_type.Trim() == "Tray 2")
        {
            papersize = "纸盒 2";
        }
        else if (name_type.Trim() == "Tray 3")
        {
            papersize = "纸盒 3";
        }
        else if (name_type.Trim() == "Tray 4")
        {
            papersize = "纸盒 4";
        }
        else
        {
            papersize = "未知";
        }
        return papersize;
    }

    private string getPaperStatus(int papervalue, int limit)
    {
        string status = "";
        if (papervalue == -2)
        {
            return "状态未知";
        }
        if (papervalue >= 50)
        {
            status = "纸张充足";
        }
        else if (papervalue < 50 && papervalue > limit)
        {
            status = "纸张正常";
        }
        else
        {
            status = "纸量少";
        }
        return status;
    }

    private string getErrorMsg(string errorState, ArrayList statusArray)
    {
        string errormsg = "";

        if (errorState.Length != 5)
        {
            return "未知异常\n";
        }
        for (int k = 0; k < 5; k++)
        {
            if (k == 2) continue;
            string bit1 = errorState.Substring(k, 1);
            if (bit1 == "0") continue;
            foreach (ErrorBitCode errbit in errorBitList)
            {
                if (bit1 == errbit.errorBitCode)
                {
                    foreach (string errbitcode in errbit.ErrorBitList)
                    {
                        string errcode = "00 00";
                        switch(k)
                        {
                            case 0:
                                errcode = string.Format("{0}0 00", errbitcode);
                                break;
                            case 1:
                                errcode = string.Format("0{0} 00", errbitcode);
                                break;
                            case 3:
                                errcode = string.Format("00 {0}0", errbitcode);
                                break;
                            case 4:
                                errcode = string.Format("00 0{0}", errbitcode);
                                break;
                        }
                        foreach (Status s in statusArray)
                        {
                            if (s.errorState == errcode)
                             {
                                errormsg += s.causeOfDown;
                                errormsg += ";";
                                break;
                            }
                        }
                    }
                }
            }
        }
        return errormsg;
    }

    #region GetMIBValue
    public string GetMIBvalue(string strDevIP, string oid)
    {
        try
        {
            string returnVal = "";

            OctetString community = new OctetString("public");
            AgentParameters param = new AgentParameters(community);
            param.Version = SnmpVersion.Ver1;
            IpAddress agent = new IpAddress(strDevIP);
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
            Pdu pdu = new Pdu(PduType.Get);
            pdu.VbList.Add(oid);

            SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);
            if (result != null)
            {
                if (result.Pdu.ErrorStatus != 0)
                {
                    returnVal = "0";
                    pdu.VbList.RemoveAt(0);
                    return returnVal;
                }
                else
                {
                    returnVal = result.Pdu.VbList[0].Value.ToString();
                    pdu.VbList.RemoveAt(0);
                    return returnVal;
                }
            }
            else
            {
                returnVal = "0";
                pdu.VbList.RemoveAt(0);
                return returnVal;
            }
        }
        catch
        {
            return "-1";
        }
    }
    #endregion

    #region online check
    public bool MFPonlineCheck(string strMFPip)
    {
        bool boolMFPoblineCheck = false;

        try
        {
            Ping ping = new Ping();
            string ip = strMFPip;
            PingReply reply = ping.Send(ip, 100);
            if (reply.Status == IPStatus.Success)
            {
                boolMFPoblineCheck = true;
            }
            return boolMFPoblineCheck;
        }
        catch (Exception exMFPonlineCheck)
        {
            exMFPonlineCheck.ToString();
            return boolMFPoblineCheck;
        }
    }
    #endregion

    #region "btnCancel_Click"
    protected void btnCancelMFP_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("MFPRestrictionList.aspx", false);
    }
    #endregion

}
