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
    public class BllMFP 
    {
        DalMFP dal = new DalMFP();
      
        public bool CheckExist(string username)
        {
            return dal.CheckExist(username);
        }


        public int Add(MFPEntry bean)
        {
            //MFPEntry entry = new MFPEntry();
            //entry.SerialNumber = bean.SerialNumber;
            //entry.ModelName = bean.ModelName;
            //entry.IPAddress = bean.IPAddress;
            //entry.Location = bean.Location;
            //entry.AdministratorID = bean.AdministratorID;
            //entry.Password = bean.Password;
            //entry.PriceID = bean.PriceID;
            //entry.Label = bean.Label;
            //entry.Password = bean.Password;
            //entry.Monitor = bean.Monitor;
            //entry.Prompt = bean.Prompt;

            int ret = dal.Add(bean);
            return ret;

        }


        public int Update(MFPEntry bean)
        {
            //MFPEntry entry = new MFPEntry();
            //entry.SerialNumber = bean.SerialNumber;
            //entry.ModelName = bean.ModelName;
            //entry.IPAddress = bean.IPAddress;
            //entry.Location = bean.Location;
            //entry.AdministratorID = bean.AdministratorID;
            //entry.Password = bean.Password;
            //entry.PriceID = bean.PriceID;
            //entry.Label = bean.Label;
            //entry.Password = bean.Password;
            //entry.Monitor = bean.Monitor;
            //entry.Prompt = bean.Prompt;

            int ret = dal.Update(bean);
            return ret;

        }
        // public int Delete(int id)
        //{
        //    int ret = dal.Delete(id);
        //    return ret;
        //}

        public int Delete(string SerialNumber)
        {
            //MFPEntry entry = dal.GetInfoByKey(SerialNumber);
            //entry.Valid = '0';
            //int ret = dal.Update(entry);
            int ret = dal.Delete(SerialNumber); 
            return ret;
        }

         //public void search(Page<UserEntry> page, string username)
         public void search(Page<MFPEntry> page, Dictionary<String, String> cond)
         {

             dal.search(page, cond);

             return;
         }
         public void searchM(Page<MFPListModel> page, Dictionary<String, String> cond)
         {

             dal.searchM(page, cond);

             return;
         }
         public MFPEntry GetMFPInfo(string SerialNumber)
        {
            MFPEntry bean = dal.GetInfoByKey(SerialNumber);
            //MFPModel model = new MFPModel();
            //model.SerialNumber = bean.SerialNumber;
            //model.ModelName = bean.ModelName;
            //model.IPAddress = bean.IPAddress;
            //model.Location = bean.Location;
            //model.AdministratorID = bean.AdministratorID;
            //model.Password = bean.Password;
            //model.PriceID = bean.PriceID;
            //model.Label = bean.Label;
            //model.Password = bean.Password;
            //model.Monitor = bean.Monitor;
            //model.Prompt = bean.Prompt;
            return bean;
        }
       
        public bool CheckExist(MFPEntry entry)
        {
            return dal.CheckExist(entry);
        }

        public bool CheckMFPExist(string SerialNumber)
        {
            return dal.CheckMFPExist(SerialNumber);
        }
        public List<JobInformationCSVModel> getMFPReportCSVList(string SearchTxt)
        {
            return dal.getMFPReportCSVList(SearchTxt);
        }

    }
}
