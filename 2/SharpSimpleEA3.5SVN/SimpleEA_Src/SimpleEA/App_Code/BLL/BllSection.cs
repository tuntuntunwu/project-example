using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.Collections.Generic;
using DAL;
using Model;
using System.Security.Cryptography;
using System.Runtime.Remoting.Contexts;
using System.Data.SqlClient;

/// <summary>
/// UserSystem 的摘要说明
/// </summary>
namespace BLL
{
    public class BllSection
    {
        DalSectionInfo dal = new DalSectionInfo();
        DalUser daluser = new DalUser();
        DalJobInformation daljob = new DalJobInformation();

        //CreateSectionReport()创建树结构，getSectionReportCSVList()获取统计值

        #region 创建树结构

        //创建集团报表管理类
        public SectionReport CreateSectionReport()
        {
            SectionReport report = new SectionReport();
            string rootID = "0";
            SectionInfoEntry root = dal.GetInfoByKey(rootID);
            report.sectioninfo = root;
            GetGroup(report);
            GetSubSection(report);
            return report;
        }

        //获得集团下的所有用户组
        public void GetGroup(SectionReport report)
        {
            string sectionid = report.sectioninfo.SectionID;
            DalSectionGroup dalsectiongroup = new DalSectionGroup();
            List<GroupEntry> grouplist = dalsectiongroup.getAllGroupList(sectionid);

            if (grouplist.Count > 0)
            {
                foreach (GroupEntry group in grouplist)
                {
                    SectionGroupReport groupReport = new SectionGroupReport();
                    groupReport.groupinfo = group;
                    report.AddGroupReport(groupReport);
                }
                //查询group下的所有用户
                foreach (SectionGroupReport groupreport in report.grouplist)
                {
                    //仅有用户携带JobInformationCSVModel信息，统计时使用
                    List<JobInformationCSVModel> userreportlist = daluser.getGroupUserVList(groupreport.groupinfo.ID);
                    groupreport.userreportlist = userreportlist;
                }
            }
            
        }

        //获得集团下的所有子集团 并 递归创建整个树结构
        public void GetSubSection(SectionReport report)
        {
            List<SectionInfoEntry> subSectionlist = dal.getSubSectionInfo(report.sectioninfo.SectionID);

            if (subSectionlist.Count > 0)
            {
                foreach (SectionInfoEntry section in subSectionlist)
                {
                    SectionReport sectionreport = new SectionReport();
                    sectionreport.sectioninfo = section;
                    report.AddSectionReport(sectionreport);
                }
                foreach (SectionReport subreport in report.sectionlist)
                {
                    GetGroup(subreport);
                    GetSubSection(subreport);
                }
            }

        }

        #endregion


        #region 获取统计值

        public List<SectionJobCSVModel> getSectionReportCSVList(SectionReport report, DateTime START_TIME, DateTime END_TIME, string SerialNumber, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            //SectionJobCSVModel意义参见定义文件
            List<SectionJobCSVModel> jobList = new List<SectionJobCSVModel>();

            SectionJobCSVModel csv_root = new SectionJobCSVModel();
            csv_root.type = "集团";
            csv_root.SectionID = report.sectioninfo.SectionID;
            csv_root.SectionName = report.sectioninfo.SectionName;
            csv_root.level = report.sectioninfo.Level;
            csv_root.ParentSectionID = report.sectioninfo.ParentSectionID;
            jobList.Add(csv_root);

            JobInformationCSVModel csv_groups = GetGroupCSV(report, jobList, START_TIME, END_TIME, SerialNumber, Dsp_Count_mode, Dsp_A3_A4);
            JobInformationCSVModel csv_subsections = GetSubSectionCSV(report, jobList, START_TIME, END_TIME, SerialNumber, Dsp_Count_mode, Dsp_A3_A4);

            //总数值 = 所有用户组数值csv_groups + 所有子集团数值csv_subsections
            AddTwoValue(csv_root.csvmodel, csv_groups, csv_subsections);

            return jobList;
        }

        //获得集团下所有用户组的统计结果
        public JobInformationCSVModel GetGroupCSV(SectionReport report, List<SectionJobCSVModel> jobList, DateTime START_TIME, DateTime END_TIME, string SerialNumber, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            JobInformationCSVModel csv_groups = new JobInformationCSVModel();

            if (report.grouplist.Count > 0)
            {
                //查询group下的所有用户
                foreach (SectionGroupReport groupreport in report.grouplist)
                {
                    SectionJobCSVModel csvgroup = new SectionJobCSVModel();
                    csvgroup.type = "用户组";
                    csvgroup.SectionID = report.sectioninfo.SectionID;
                    csvgroup.SectionName = report.sectioninfo.SectionName;
                    csvgroup.level = report.sectioninfo.Level;
                    csvgroup.ParentSectionID = report.sectioninfo.ParentSectionID;
                    csvgroup.csvmodel.GroupID = groupreport.groupinfo.ID;
                    csvgroup.csvmodel.GroupName = groupreport.groupinfo.GroupName;
                    jobList.Add(csvgroup);

                    List<JobInformationCSVModel> userreportlist = groupreport.userreportlist;
                    List<SectionJobCSVModel> list = GetUserJobDict(userreportlist, START_TIME, END_TIME, SerialNumber, Dsp_Count_mode, Dsp_A3_A4);
                    foreach (SectionJobCSVModel csvuser in list)
                    {
                        csvuser.type = "用户";
                        csvuser.SectionID = report.sectioninfo.SectionID;
                        csvuser.SectionName = report.sectioninfo.SectionName;
                        csvuser.level = report.sectioninfo.Level;
                        csvuser.ParentSectionID = report.sectioninfo.ParentSectionID;
                        jobList.Add(csvuser);
                    
                        //把用户数值csvuser加到用户组数值csvgroup
                        AddTwoValue(csvgroup.csvmodel, csvuser.csvmodel, csvgroup.csvmodel);
                    }

                    //把每个用户组数值csvgroup加到返回的数值csv_groups
                    AddTwoValue(csv_groups, csvgroup.csvmodel, csv_groups);
                }
            }

            return csv_groups;
        }

        //获得集团下所有子集团的统计结果
        public JobInformationCSVModel GetSubSectionCSV(SectionReport report, List<SectionJobCSVModel> jobList, DateTime START_TIME, DateTime END_TIME, string SerialNumber, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            JobInformationCSVModel csv_subsections = new JobInformationCSVModel();

            if (report.sectionlist.Count > 0)
            {
                //查询sub集团
                foreach (SectionReport subreport in report.sectionlist)
                {
                    SectionJobCSVModel csvsubsection = new SectionJobCSVModel();
                    csvsubsection.type = "集团";
                    csvsubsection.SectionID = subreport.sectioninfo.SectionID;
                    csvsubsection.SectionName = subreport.sectioninfo.SectionName;
                    csvsubsection.level = subreport.sectioninfo.Level;
                    csvsubsection.ParentSectionID = subreport.sectioninfo.ParentSectionID;
                    jobList.Add(csvsubsection);

                    JobInformationCSVModel tmp_csv_groups = GetGroupCSV(subreport, jobList, START_TIME, END_TIME, SerialNumber, Dsp_Count_mode, Dsp_A3_A4);
                    JobInformationCSVModel tmp_csv_subsections = GetSubSectionCSV(subreport, jobList, START_TIME, END_TIME, SerialNumber, Dsp_Count_mode, Dsp_A3_A4);

                    //子集团总数值 = 所有用户组数值tmp_csv_groups + 所有子集团数值tmp_csv_subsections
                    AddTwoValue(csvsubsection.csvmodel, tmp_csv_groups, tmp_csv_subsections);

                    //把每个子集团数值csvsubsection加到返回的csv_subsections
                    AddTwoValue(csv_subsections, csvsubsection.csvmodel, csv_subsections);
                }
            }

            return csv_subsections;
        }

        //用户统计
        public List<SectionJobCSVModel> GetUserJobDict(List<JobInformationCSVModel> idlist, DateTime START_TIME, DateTime END_TIME, string SerialNumber, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            List<SectionJobCSVModel> list = new List<SectionJobCSVModel>();
            foreach (JobInformationCSVModel bean in idlist)
            {
                JobInformationCSVModel row = daljob.getUserJobInfomation(bean.UserID, START_TIME, END_TIME, SerialNumber, Dsp_Count_mode, Dsp_A3_A4);
                if (row != null)
                {
                    row.UserID = bean.UserID;
                    row.UserName = bean.UserName;
                    row.LoginName = bean.LoginName;
                    row.GroupID = bean.GroupID;
                    row.GroupName = bean.GroupName;
                    SectionJobCSVModel csvmodel = new SectionJobCSVModel();
                    csvmodel.csvmodel = row;
                    list.Add(csvmodel);
                }
            }
            return list;
        }

        // C = A + B
        public void AddTwoValue(JobInformationCSVModel C, JobInformationCSVModel A, JobInformationCSVModel B)
        {
            C.CopyBWMoney = A.CopyBWMoney + B.CopyBWMoney;
            C.CopyColorMoney = A.CopyColorMoney + B.CopyColorMoney;
            C.CopyAllMoney = A.CopyAllMoney + B.CopyAllMoney;
            C.PrintBWMoney = A.PrintBWMoney + B.PrintBWMoney;
            C.PrintColorMoney = A.PrintColorMoney + B.PrintColorMoney;
            C.PrintAllMoney = A.PrintAllMoney + B.PrintAllMoney;
            C.ScanBWMoney = A.ScanBWMoney + B.ScanBWMoney;
            C.ScanColorMoney = A.ScanColorMoney + B.ScanColorMoney;
            C.ScanAllMoney = A.ScanAllMoney + B.ScanAllMoney;
            C.FaxBWMoney = A.FaxBWMoney + B.FaxBWMoney;
            C.FaxAllMoney = A.FaxAllMoney + B.FaxAllMoney;
            C.DFPrintBWMoney = A.DFPrintBWMoney + B.DFPrintBWMoney;
            C.DFPrintColorMoney = A.DFPrintColorMoney + B.DFPrintColorMoney;
            C.DFPrintAllMoney = A.DFPrintAllMoney + B.DFPrintAllMoney;
            C.ScanSaveBWMoney = A.ScanSaveBWMoney + B.ScanSaveBWMoney;
            C.ScanSaveColorMoney = A.ScanSaveColorMoney + B.ScanSaveColorMoney;
            C.ScanSaveAllMoney = A.ScanSaveAllMoney + B.ScanSaveAllMoney;
            C.ListPrintBWMoney = A.ListPrintBWMoney + B.ListPrintBWMoney;
            C.ListPrintColorMoney = A.ListPrintColorMoney + B.ListPrintColorMoney;
            C.ListPrintAllMoney = A.ListPrintAllMoney + B.ListPrintAllMoney;
            C.FaxChannelBWMoney = A.FaxChannelBWMoney + B.FaxChannelBWMoney;
            C.FaxChannelAllMoney = A.FaxChannelAllMoney + B.FaxChannelAllMoney;
            C.FaxNetBWMoney = A.FaxNetBWMoney + B.FaxNetBWMoney;
            C.FaxNetAllMoney = A.FaxNetAllMoney + B.FaxNetAllMoney;
            C.OtherBWMoney = A.OtherBWMoney + B.OtherBWMoney;
            C.OtherColorMoney = A.OtherColorMoney + B.OtherColorMoney;
            C.OtherAllMoney = A.OtherAllMoney + B.OtherAllMoney;
        }

        #endregion

    }
}
