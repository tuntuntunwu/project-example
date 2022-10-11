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
    public class BllJobInformation    {
   
         DalJobInformation dal = new DalJobInformation();

        //用户统计
         public Dictionary<string, JobInformationCSVModel> GetUserJobDict(List<JobInformationCSVModel> idlist, DateTime START_TIME, DateTime END_TIME, string SerialNumber, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            Dictionary<string, JobInformationCSVModel> jobDict = new Dictionary<string, JobInformationCSVModel>();
            foreach (JobInformationCSVModel bean in idlist)
            {

                JobInformationCSVModel row = dal.getUserJobInfomation(bean.UserID, START_TIME, END_TIME, SerialNumber, Dsp_Count_mode, Dsp_A3_A4);
                if (row != null)
                {
                    row.UserID = bean.UserID;
                    row.UserName = bean.UserName;
                    row.LoginName = bean.LoginName;
                    row.GroupID = bean.GroupID;
                    row.GroupName = bean.GroupName;
                    jobDict.Add(bean.UserID.ToString(), row);
                }
            }
            return jobDict;
        }
        //用户组统计
         public Dictionary<string, JobInformationCSVModel> GetGroupJobDict(List<JobInformationCSVModel> grouplist, DateTime START_TIME, DateTime END_TIME, string SerialNumber, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            Dictionary<string, JobInformationCSVModel> jobDict = new Dictionary<string, JobInformationCSVModel>();
            foreach (JobInformationCSVModel bean in grouplist)
            {

                JobInformationCSVModel row = dal.getGroupJobInfomation(bean.GroupID, START_TIME, END_TIME, SerialNumber, Dsp_Count_mode, Dsp_A3_A4);
                if (row != null)
                {
                    row.GroupID = bean.GroupID;
                    row.GroupName = bean.GroupName;
                    row.UserCount = bean.UserCount;
                    jobDict.Add(bean.GroupID.ToString(), row);
                }
            }
            return jobDict;
        }
        //复合机统计
         public Dictionary<string, JobInformationCSVModel> GetMFPJobDict(List<JobInformationCSVModel> MFPlist, DateTime START_TIME, DateTime END_TIME, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            Dictionary<string, JobInformationCSVModel> jobDict = new Dictionary<string, JobInformationCSVModel>();
            foreach (JobInformationCSVModel bean in MFPlist)
            {

                JobInformationCSVModel row = dal.getMFPJobInfomation(bean.SerialNumber, START_TIME, END_TIME, Dsp_Count_mode, Dsp_A3_A4);
                if (row != null)
                {
                    row.SerialNumber = bean.SerialNumber;
                    row.ModelName = bean.ModelName;
                    jobDict.Add(bean.UserID.ToString(), row);
                }
            }
            return jobDict;
        }
        
        
        //作业类型统计
         public Dictionary<string, JobTypeInfoCSVModel> GetJobTypeDict(int flg, string strIDList, Dictionary<string, int> jobTypeDict, DateTime START_TIME, DateTime END_TIME, string SerialNumber, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            Dictionary<string, JobTypeInfoCSVModel> jobDict = new Dictionary<string, JobTypeInfoCSVModel>();
            foreach (string key in jobTypeDict.Keys)
            {

                int JobTypeID = jobTypeDict[key];

                JobTypeInfoCSVModel bean = dal.getJobTypeCSVInfomation(flg, strIDList, JobTypeID, START_TIME, END_TIME, SerialNumber, Dsp_Count_mode, Dsp_A3_A4);
                bean.JobType = key;
                bean.JobTypeID = JobTypeID;
                jobDict.Add(key, bean);
            }
            return jobDict;
        }
    }
}
