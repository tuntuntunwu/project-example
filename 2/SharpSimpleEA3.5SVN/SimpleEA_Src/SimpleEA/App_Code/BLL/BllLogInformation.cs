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
    public class BllLogInfomation 
    {
        DalLogInfomation dal = new DalLogInfomation();
      DalPrintCopy   dalCopy = new DalPrintCopy();
        public bool CheckExist(string username)
        {
            return dal.CheckExist(username);
        }

         //public void search(Page<UserEntry> page, string username)
        // public void search(Page<MFPEntry> page, Dictionary<String, String> cond)
        // {

        //     dal.search(page, cond);

        //     return;
        // }
        // public void searchM(Page<MFPListModel> page, Dictionary<String, String> cond)
        // {

        //     dal.searchM(page, cond);

        //     return;
        // }
        // public MFPEntry GetMFPInfo(string SerialNumber)
        //{
        //    MFPEntry bean = dal.GetInfoByKey(SerialNumber);            
        //    return bean;
        //}
       
        //public bool CheckExist(MFPEntry entry)
        //{
        //    return dal.CheckExist(entry);
        //}

        //public bool CheckMFPExist(string SerialNumber)
        //{
        //    return dal.CheckMFPExist(SerialNumber);
        //}

        public List<LogInfomationModel> searchNOPage(Dictionary<String, String> cond)
        {
            List<LogInfomationModel> beanlist = dal.searchNOPage(cond);
            foreach (LogInfomationModel bean in beanlist)
            {
                PrintCopyEntry copyBean = dalCopy.GetInfoByID(bean.ID);
                bean.Finished = copyBean.Finished;

                
            }
            return beanlist;
        }
        public void convertEntryToModel(LogInfomationEntry entry, LogInfomationModel bean)
        {
           
        }

    }
}
