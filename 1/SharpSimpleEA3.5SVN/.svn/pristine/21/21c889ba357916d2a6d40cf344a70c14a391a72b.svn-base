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
    public class BllCopyConfig 
    {
        DalCopyConfig dal = new DalCopyConfig();



        public int Add(CopyConfigEntry bean)
        {
            int ret = dal.Add(bean);
            return ret;

        }


        public int Update(CopyConfigEntry bean)
        {
            if (dal.CheckExist())
            {
                int ret = dal.Update(bean);
                return ret;
            }
            else
            {
                int ret = dal.Add(bean);
                return ret;
            }

        }
      

        public int Delete()
        {
            
            int ret = dal.Delete(); 
            return ret;
        }


        public CopyConfigEntry GetCopyConfigInfo()
        {
            CopyConfigEntry bean = dal.GetCopyConfigInfo();
           
           return bean;
        }
       
       
    }
}
