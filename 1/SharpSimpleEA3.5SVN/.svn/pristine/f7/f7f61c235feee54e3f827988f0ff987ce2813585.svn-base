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
    public class BllPrintCopy 
    {
        DalPrintCopy dal = new DalPrintCopy();
      
        public bool CheckExist(string username)
        {
            return dal.CheckExist(username);
        }


        public int Add(PrintCopyEntry entry)
        {

            int ret = dal.Add(entry);
            return ret;

        }


        public int Update(PrintCopyEntry entry)
        {

            int ret = dal.Update(entry);
            return ret;

        }
        // public int Delete(int id)
        //{
        //    int ret = dal.Delete(id);
        //    return ret;
        //}

        public int Delete(int id)
        {
            int ret = dal.Delete(id);
            return ret;
        }

         //public void search(Page<UserEntry> page, string username)
        public void search(Page<PrintCopyEntry> page, Dictionary<String, String> cond)
         {

             dal.search(page, cond);

             return;
         }
        public void searchM(Page<PrintCopyModel> page, Dictionary<String, String> cond)
         {

             dal.searchM(page, cond);

             return;
         }
        public PrintCopyModel GetPrintCopyInfo(int id)
        {
            PrintCopyEntry entry = dal.GetInfoByID(id);
            PrintCopyModel model = new PrintCopyModel();
           model.ID = entry.ID;
           model.UserID = entry.UserID;
           model.CopyType = entry.CopyType;
           model.CopyFile = entry.CopyFile;
           model.Finished = entry.Finished;
           model.CopyTimes=entry.CopyTimes;
           return model;
        }

        public bool CheckExist(PrintCopyEntry user)
        {
            return dal.CheckExist(user);
        }

       
    }
}
