using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Model
{
    //每个SectionJobCSVModel里存放CSV报表中的一行记录
    //type有3种：集团、用户组、用户，其对应的JobInformationCSVModel中存放各自的统计数据
    //输出CSV报表时，根据SectionID和type来生成输出
    public class SectionJobCSVModel
    {
        public string SectionID = "";
        public string SectionName = "";
        public int level = 0;
        public string ParentSectionID = "";
        public string type = "";
        public JobInformationCSVModel csvmodel = new JobInformationCSVModel();
        public string getUserReportData(int Dsp_Count_mode)
        {
            string strOutPut = csvmodel.getUserReportData(Dsp_Count_mode);
            strOutPut = SectionName + "," + csvmodel.GroupName + "," + strOutPut;
            return strOutPut;
        }

        public string getSectionReportData(int Dsp_Count_mode)
        {
            StringBuilder sb = new StringBuilder("");
            
            if (type == "集团")
            {
                for (int i = 0; i < level; i++)
                {
                    sb.Append(" ");
                }
                sb.Append(SectionName);
                sb.Append(",");
                sb.Append("集团");
                sb.Append(",");
            }
            else if (type == "用户组")
            {
                for (int i = 0; i < 6; i++)
                {
                    sb.Append(" ");
                }
                sb.Append(csvmodel.GroupName);
                sb.Append(",");
                sb.Append("用户组");
                sb.Append(",");
            }
            else if (type == "用户")
            {
                for (int i = 0; i < 7; i++)
                {
                    sb.Append(" ");
                }
                sb.Append("- ");
                sb.Append(csvmodel.LoginName);
                sb.Append(",");
                sb.Append("用户");
                sb.Append(",");
            }

            sb.Append(UtilCommon.doubleToMoney(csvmodel.CopyBWMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.CopyColorMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.CopyAllMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.PrintBWMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.PrintColorMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.PrintAllMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.ScanBWMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.ScanColorMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.ScanAllMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.FaxBWMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.FaxAllMoney, Dsp_Count_mode));
            sb.Append(",");

            sb.Append(UtilCommon.doubleToMoney(csvmodel.DFPrintBWMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.DFPrintColorMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.DFPrintAllMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.ScanSaveBWMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.ScanSaveColorMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.ScanSaveAllMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.ListPrintBWMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.ListPrintColorMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.ListPrintAllMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.FaxChannelBWMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.FaxChannelAllMoney, Dsp_Count_mode));
            sb.Append(",");

            sb.Append(UtilCommon.doubleToMoney(csvmodel.FaxNetBWMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.FaxNetAllMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.OtherBWMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.OtherColorMoney, Dsp_Count_mode));
            sb.Append(",");
            sb.Append(UtilCommon.doubleToMoney(csvmodel.OtherAllMoney, Dsp_Count_mode));
            sb.Append(",");

            return sb.ToString();
        }

    }
}
