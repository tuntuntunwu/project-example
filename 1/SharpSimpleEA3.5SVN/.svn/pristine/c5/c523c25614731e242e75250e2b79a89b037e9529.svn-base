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
    public class BllJobTypeInfo 
    {
        DalJobTypeInfo dal = new DalJobTypeInfo();
        public JobTypeInfoEntry GetJobNameByID(int ID)
        {
            JobTypeInfoEntry bean = dal.GetJobNameByID(ID);
            return bean;
        }
       
    }
}
