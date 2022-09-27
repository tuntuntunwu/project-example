using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SnmpSharpNet;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using System.IO;
using System.Timers;
using System.Collections;

namespace MFPCheckService
{
    class RunCheck
    {
        //ArrayList statusArray5 = new ArrayList();
        //ArrayList statusArray3 = new ArrayList();
        //ArrayList statusArray2 = new ArrayList();
        List<Status> statusArray5 = new List<Status>();
        List<Status> statusArray3 = new List<Status>();
        List<Status> statusArray2 = new List<Status>();
        

        List<ErrorBitCode> errorBitList = new List<ErrorBitCode>();

        public void TaskStart()
        {
            //Add by Wei Changye 2012.02.27 cycle delete the print file------86400000 millisecond = 1 day
            System.Timers.Timer myTimer = new System.Timers.Timer(Convert.ToDouble(ServiceCommon.GetAppSettingString("RunCycle")));
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(CheckCycle);
            myTimer.Enabled = true;
        }

        //Set Cause of Down(array of 5);
        public void InfoSet()
        {
            statusArray5.Clear();
            statusArray5 = new List<Status>();
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
            statusArray3 = new List<Status>();

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
            statusArray2 = new List<Status>();

            Status s1 = new Status();
            s1.printerStatus = 4;
            s1.causeOfDown = "Receiving data\n";
            s1.causeOfDown += "Printing in progress\n";
            statusArray2.Add(s1);

            s1 = new Status();
            s1.printerStatus = 1;
            s1.causeOfDown = "Power save\n";
            statusArray2.Add(s1);

            s1 = new Status();
            s1.printerStatus = 5;
            s1.causeOfDown = "Warming up\n";
            statusArray2.Add(s1);

            s1 = new Status();
            s1.printerStatus = 3;
            s1.causeOfDown = "READY\n";
            statusArray2.Add(s1);
        }

        //Set ErrorBitCode;
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

        public string Check(string MFPip, int TonerThreshold, int PaperThreshold)
        {
            string Message = "";//表示发送的信息内容;

            InfoSet();
            InfoSet1();
            InfoSet2();
            InitErrorBitCode();

            try
            {
                //1、故障检测：测试打印机的ip是否在线；
                bool status;
                status = MFPonlineCheck(MFPip);

                if (status == false)
                {
                    Message += "MFP不在线\n";//故障信息;
                }
                else
                {
                    //判断是黑白机还是彩色机
                    string isBlackOid = "1.3.6.1.2.1.43.12.1.1.4.1.1";
                    string strBlack = GetMIBvalue(MFPip, isBlackOid);
                    if (strBlack.Trim().Equals("cyan"))
                    {

                        //2、墨粉状态检测：测试打印机的墨粉状态；
                        //青色墨盒(Cyan Toner)：1.3.6.1.2.1.43.11.1.1.9.1.1；
                        PoweredInk cyanToner = new PoweredInk();
                        cyanToner.Oid = "1.3.6.1.2.1.43.11.1.1.9.1.1";
                        cyanToner.Instruction = "青色墨粉";
                        cyanToner.Value = int.Parse(GetMIBvalue(MFPip, cyanToner.Oid));
                        //检测青色墨盒的阈值;
                        if (cyanToner.Value < TonerThreshold)
                        {
                            Message += cyanToner.Instruction + "墨量低\n";
                        }

                        //品红墨盒(Magenta Toner)：1.3.6.1.2.1.43.11.1.1.9.1.2；
                        PoweredInk magentaToner = new PoweredInk();
                        magentaToner.Oid = "1.3.6.1.2.1.43.11.1.1.9.1.2";
                        magentaToner.Instruction = "品红色墨粉";
                        magentaToner.Value = int.Parse(GetMIBvalue(MFPip, magentaToner.Oid));
                        //检测品红墨盒的阈值;
                        if (magentaToner.Value < TonerThreshold)
                        {
                            Message += magentaToner.Instruction + "墨量低\n";
                        }

                        //黄色墨盒(Yellow Toner)：1.3.6.1.2.1.43.11.1.1.9.3；
                        PoweredInk yellowToner = new PoweredInk();
                        yellowToner.Oid = "1.3.6.1.2.1.43.11.1.1.9.1.3";
                        yellowToner.Instruction = "黄色墨粉";
                        yellowToner.Value = int.Parse(GetMIBvalue(MFPip, yellowToner.Oid));
                        //检测黄色墨盒的阈值;
                        if (yellowToner.Value < TonerThreshold)
                        {
                            Message += yellowToner.Instruction + "墨量低\n";
                        }
                        //黑色墨盒(Black Toner)：1.3.6.1.2.1.43.11.1.1.9.1.4；
                        PoweredInk blackToner = new PoweredInk();
                        blackToner.Oid = "1.3.6.1.2.1.43.11.1.1.9.1.4";
                        blackToner.Instruction = "黑色墨粉";
                        blackToner.Value = int.Parse(GetMIBvalue(MFPip, blackToner.Oid));
                        //检测黑色墨盒的阈值;
                        if (blackToner.Value < TonerThreshold)
                        {
                            Message += blackToner.Instruction + "墨量低\n";
                        }
                    }
                    else
                    {
                        //黑色墨盒(Black Toner)：1.3.6.1.2.1.43.11.1.1.9.1.4；
                        PoweredInk blackToner = new PoweredInk();
                        blackToner.Oid = "1.3.6.1.2.1.43.11.1.1.9.1.1";
                        blackToner.Instruction = "黑色墨粉";
                        blackToner.Value = int.Parse(GetMIBvalue(MFPip, blackToner.Oid));
                        //检测黑色墨盒的阈值;
                        if (blackToner.Value < TonerThreshold)
                        {
                            Message += blackToner.Instruction + "墨量低\n";
                        }
                    }
                    //3、纸盒状态检测：测试打印机的纸张状态；
                    //Tray1(纸盒1)：
                    PaperInfo tray1 = new PaperInfo();
                    tray1.TypeoOid = "1.3.6.1.2.1.43.8.2.1.12.1.2";
                    tray1.MaxVolumeOid = "1.3.6.1.2.1.43.8.2.1.9.1.2";
                    tray1.EstimateVolumeOid = "1.3.6.1.2.1.43.8.2.1.10.1.2";
                    tray1.Type = GetMIBvalue(MFPip, tray1.TypeoOid);
                    tray1.MaxValue = int.Parse(GetMIBvalue(MFPip, tray1.MaxVolumeOid));
                    //tray1.EstimateValue = int.Parse(GetMIBvalue(MFPip, tray1.EstimateVolumeOid)) / (tray1.MaxValue * 1.0) * 100;
                    tray1.EstimateValue = 0;
                    if (tray1.MaxValue != 0 && tray1.MaxValue != -1)
                    {
                        tray1.EstimateValue = (int)(int.Parse(GetMIBvalue(MFPip, tray1.EstimateVolumeOid)) / (tray1.MaxValue * 1.0) * 100);
                    }
                    //检测纸盒1的阈值;
                    if (tray1.EstimateValue < PaperThreshold)
                    {
                        if (tray1.EstimateValue < 1)
                        {
                            Message += "纸张类型为" + tray1.Type + ",纸张用完\n";
                        }
                        else
                        {
                            Message += "纸张类型为" + tray1.Type + ",纸量少\n";
                        }
                    }
                    //Tray2(纸盒2)：
                    PaperInfo tray2 = new PaperInfo();
                    tray2.TypeoOid = "1.3.6.1.2.1.43.8.2.1.12.1.3";
                    tray2.MaxVolumeOid = "1.3.6.1.2.1.43.8.2.1.9.1.3";
                    tray2.EstimateVolumeOid = "1.3.6.1.2.1.43.8.2.1.10.1.3";
                    tray2.Type = GetMIBvalue(MFPip, tray2.TypeoOid);
                    tray2.MaxValue = int.Parse(GetMIBvalue(MFPip, tray2.MaxVolumeOid));
                    //tray2.EstimateValue = int.Parse(GetMIBvalue(MFPip, tray2.EstimateVolumeOid)) / (tray2.MaxValue * 1.0) * 100;
                    tray2.EstimateValue = 0;
                    if (tray2.MaxValue != 0 && tray2.MaxValue != -1)
                    {
                        tray2.EstimateValue = (int)(int.Parse(GetMIBvalue(MFPip, tray2.EstimateVolumeOid)) / (tray2.MaxValue * 1.0) * 100);
                    }

                    //检测纸盒2的阈值;
                    if (tray2.EstimateValue < PaperThreshold)
                    {
                        if (tray2.EstimateValue < 1)
                        {
                            Message += "纸张类型为" + tray2.Type + ",纸张用完\n";
                        }
                        else
                        {
                            Message += "纸张类型为" + tray2.Type + ",纸量少\n";
                        }
                    }

                    //Tray3(纸盒3)：
                    PaperInfo tray3 = new PaperInfo();
                    tray3.TypeoOid = "1.3.6.1.2.1.43.8.2.1.12.1.4";
                    tray3.MaxVolumeOid = "1.3.6.1.2.1.43.8.2.1.9.1.4";
                    tray3.EstimateVolumeOid = "1.3.6.1.2.1.43.8.2.1.10.1.4";
                    tray3.Type = GetMIBvalue(MFPip, tray3.TypeoOid);
                    tray3.MaxValue = int.Parse(GetMIBvalue(MFPip, tray3.MaxVolumeOid));
                    tray3.EstimateValue = 0;
                    if (tray3.MaxValue != 0 && tray3.MaxValue != -1)
                    {
                        tray3.EstimateValue = (int)(int.Parse(GetMIBvalue(MFPip, tray3.EstimateVolumeOid)) / (tray3.MaxValue * 1.0) * 100);
                    }
                    //tray3.EstimateValue = int.Parse(GetMIBvalue(MFPip, tray3.EstimateVolumeOid)) / (tray3.MaxValue * 1.0) * 100;
                    
                    //检测纸盒3的阈值
                    if (tray3.EstimateValue < PaperThreshold)
                    {
                        if (tray3.EstimateValue < 1)
                        {
                            Message += "纸张类型为" + tray3.Type + ",纸张用完\n";
                        }
                        else
                        {
                            Message += "纸张类型为" + tray3.Type + "纸量少\n";
                        }
                    }

                    //Tray4(纸盒4)：
                    PaperInfo tray4 = new PaperInfo();
                    tray4.TypeoOid = "1.3.6.1.2.1.43.8.2.1.12.1.5";
                    tray4.MaxVolumeOid = "1.3.6.1.2.1.43.8.2.1.9.1.5";
                    tray4.EstimateVolumeOid = "1.3.6.1.2.1.43.8.2.1.10.1.5";
                    tray4.Type = GetMIBvalue(MFPip, tray4.TypeoOid);
                    tray4.MaxValue = int.Parse(GetMIBvalue(MFPip, tray4.MaxVolumeOid));
                   
                    tray4.EstimateValue = 0;
                    if (tray4.MaxValue != 0 && tray4.MaxValue != -1)
                    {
                        tray4.EstimateValue = (int)(int.Parse(GetMIBvalue(MFPip, tray4.EstimateVolumeOid)) / (tray4.MaxValue * 1.0) * 100);
                    }

                    //tray4.EstimateValue = int.Parse(GetMIBvalue(MFPip, tray4.EstimateVolumeOid)) / (tray4.MaxValue * 1.0) * 100;
                    //检测纸盒4的阈值;
                    if (tray4.EstimateValue < PaperThreshold)
                    {
                        if (tray4.EstimateValue < 1)
                        {
                            Message += "纸张类型为" + tray4.Type + ",纸张用完\n";
                        }
                        else
                        {
                            Message += "纸张类型为" + tray4.Type + "纸量少\n";
                        }
                    }

                    //Add April 10th 2015
                    int deviceStatus;
                    deviceStatus = int.Parse(GetMIBvalue(MFPip, "1.3.6.1.2.1.25.3.2.1.5.1"));
                    
                    int printerStatus;
                    printerStatus = int.Parse(GetMIBvalue(MFPip, "1.3.6.1.2.1.25.3.5.1.1.1"));

                    string errorState;
                    errorState = GetMIBvalue(MFPip, "1.3.6.1.2.1.25.3.5.1.2.1");
                    //errorState = "F9 70";

                    switch (deviceStatus)
                    {
                        case 5:
                            {
                                if (printerStatus == 1)
                                {
                                    Message += getErrorMsg(errorState, statusArray5);
                                }

                                //InfoSet();

                                //if (printerStatus == 1)
                                //{
                                //    foreach (Status s in statusArray5)
                                //    {
                                //        if (s.errorState == errorState)
                                //        {
                                //            Message += s.causeOfDown;
                                //            break;
                                //        }
                                //    }
                                //}
                            };break;
                        case 3:
                            {
                                //InfoSet1();

                                if (printerStatus == 1)
                                {
                                    Message += getErrorMsg(errorState, statusArray3);
                                //    foreach (Status s in statusArray3)
                                //    {
                                //        if (s.errorState == errorState)
                                //        {
                                //            Message += s.causeOfDown;
                                //            break;
                                //        }
                                //    }
                                }
                                else if (printerStatus == 3)
                                {
                                    Message += getErrorMsg(errorState, statusArray3);
                                //    foreach (Status s in statusArray3)
                                //    {
                                //        if (s.errorState == errorState)
                                //        {
                                //            Message += s.causeOfDown;
                                //            break;
                                //        }
                                 }
                            };break;
                        case 2:
                            {
                                //infoset2();

                                //foreach (status s in statusarray3)
                                //{
                                //    if (s.printerstatus == printerstatus)
                                //    {
                                //        message += s.causeofdown;
                                //        break;
                                //    }
                                //}
                                Message += getErrorMsg(errorState,statusArray2);
                            };break;
                    }
                }
                if (Message.Length == 0)
                {
                    Message = "无异常\n";
                }
                return Message;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string getErrorMsg(string errorState, List<Status> statusArray)
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
                            switch (k)
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
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return errormsg;
        }

        //public string Send()
        public void CheckCycle(object source, System.Timers.ElapsedEventArgs e)
        {
            string constr = ServiceCommon.DBConnectionStrings;
            SqlConnection con = new SqlConnection(constr);

            string Message = "";//表示发送的邮件或微信内容;

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("select * from SimpleEA.dbo.SettinServerIP", con);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                string TongxinType = reader[1].ToString();//表示通信方式;
                string TongxinAddr = reader[2].ToString();//表示通信地址;
                int tonerThreshold = int.Parse(reader[4].ToString());//设置墨盒内的墨粉量阈值;
                int paperThreshold = int.Parse(reader[5].ToString());//设置纸张数量的阈值;
                string companInfo = reader[13].ToString();//获取公司的信息；

                //init;
                string emailServer = "";
                string emailUserName = "";
                string emailAddress = "";
                string emailPassword = "";

                string corpId = "";
                string secret = "";
                int applicationId = -1;
                if (TongxinType == "0")
                {
                    emailServer = reader[6].ToString();//Send Server Ip address;
                    emailAddress = reader[7].ToString();
                    emailUserName = reader[8].ToString();
                    emailPassword = reader[9].ToString();
                }
                else
                {
                    corpId = reader[10].ToString();
                    secret = reader[11].ToString();
                    applicationId = int.Parse(reader[12].ToString());
                }
                reader.Close();

                //Message += "信息源自" + companInfo + ".\n\n";

                string modelName;//表示MFP的型号;
                string MFPip;//表示MFP的IP地址;
                string location;//表示MFP所在位置;
                SqlCommand cmd = new SqlCommand("select * from SimpleEA.dbo.MFPInformation", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    modelName = reader[1].ToString();
                    MFPip = reader[2].ToString();
                    location = reader[3].ToString();
                    //20150915 chen update start
                    //Message += "型号:" + modelName + "\n" + "位于:" + location + "\n";
                    //Message += "检测结果:\n" + Check(MFPip, tonerThreshold, paperThreshold) + "\n";

                    string check_result = Check(MFPip, tonerThreshold, paperThreshold);
                    if (check_result != "")
                    {
                        Message += "型号:" + modelName + "\n" + "位于:" + location + "\n";
                        Message += "检测结果:\n" + check_result + "\n";
                    }
                    //20150915 chen update end
                }
                //20150915 chen update start

                if (Message == "")
                {
                    return;
                }

                Message = "信息源自" + companInfo + ".\n\n" + Message;

                //20150915 chen update end


                //TongxinType=1,使用微信进行异常提醒；
                //TongxinType=0,使用邮件进行异常提醒；
                if (TongxinType == "1")
                {
                    string postdata = "{\"touser\":\"" + TongxinAddr + "\",\"msgtype\":\"text\",\"agentid\":\"" + applicationId.ToString() + "\",\"text\":{\"content\":\"" + Message + "\"},\"safe\":\"0\"}";
                    bool alert = SendMessage(postdata, corpId, secret);
                    //if (alert == true)
                    //{
                    //    return "微信提醒成功!";
                    //}
                    //else
                    //{
                    //    return "微信提醒失败!";
                    //}
                }
                else
                {
                    bool alert = SendMail(emailServer,emailUserName, emailAddress, emailPassword, "", TongxinAddr, "MFP提醒", Message);
                    //if (alert == true)
                    //{
                    //    return "邮件发送成功!";
                    //}
                    //else
                    //{
                    //    return "邮件发送失败!";
                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

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

        public bool SendMail(string emailServer, string fromName, string fromAddress, string password,
            string toName, string toAddress, string subject, string body)
        {
            UserAbstract from = new UserAbstract(fromName, fromAddress, password);
            UserAbstract to = new UserAbstract(toName, toAddress);
            MailAbstract mail = new MailAbstract(subject, body, from);
            mail.GetInformation();
            MailHelper mailHelper = new MailHelper(mail, to);
            bool warn = mailHelper.send(emailServer);
            return warn;
        }

        public bool SendMessage(string postdata, string corpID, string secret)
        {
            string geturl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corpID + "&corpsecret=" + secret;
            string content = GetAPI(geturl);
            string message = string.Empty;//设置空字符串;

            if (content != string.Empty)
            {
                ErrorMsg e = new ErrorMsg();
                e.errcode = string.Empty;
                e.errmsg = string.Empty;
                e = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMsg>(content);
                if (e.errcode == null)
                {
                    Token accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(content);//序列化字符串,获取accesss_token;
                    string posturl = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=" + accessToken.access_token;
                    message = PostAPI(posturl, postdata);
                    ErrorMsg error = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMsg>(message);//序列化字符串;

                    if (error.errcode == "0")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            return false;
        }

        public string PostAPI(string posturl, string postData)
        {
            //数据初始化;
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);

            //开始请求;
            try
            {
                request = WebRequest.Create(posturl) as HttpWebRequest;//创建一个Http请求;
                CookieContainer cookiecontainer = new CookieContainer();
                request.CookieContainer = cookiecontainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";//设置请求的媒体类型;
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();

                response = request.GetResponse() as HttpWebResponse;//获取来自Internet资源的响应;
                instream = response.GetResponseStream();//读取来自服务器响应的体;
                sr = new StreamReader(instream, encoding);//以一种特定的编码方式读取字符流;

                string content = sr.ReadToEnd();//读取StreamReader内的字符流;
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;//返回空字符串;
            }
        }

        public string GetAPI(string posturl)
        {
            //数据初始化;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;

            //开始请求;
            try
            {
                request = WebRequest.Create(posturl) as HttpWebRequest;//创建一个Http请求;
                CookieContainer cookiecontainer = new CookieContainer();
                request.CookieContainer = cookiecontainer;
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";//设置请求的媒体类型;

                response = request.GetResponse() as HttpWebResponse;//获取来自Internet资源的响应;
                instream = response.GetResponseStream();//读取来自服务器响应的体;
                sr = new StreamReader(instream, encoding);//以一种特定的编码方式读取字符流;

                string content = sr.ReadToEnd();//读取StreamReader内的字符流;
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }
    }
}
