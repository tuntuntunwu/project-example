using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using System.Collections;
using DAL;
using Model;

/// <summary>
///BllICCard 的摘要说明
/// </summary>
/// 
namespace BLL
{
    public class BllICCard:CommonDal
    {
        public BllICCard()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        //更新
        public int ImportData(UserModel[] beanitem11)
        {
            DalICCard dal = new DalICCard();
            ArrayList sqlList = new ArrayList();
            string sql = "";
            
            if (beanitem11 != null)
            {
                int ret = 0;
                for (int i = 0; i < beanitem11.Length; i++)
                {
                    if (beanitem11[i] != null)
                    {
                        ret=  dal.getImportSql(beanitem11[i]);
                    }
                }
                return ret;
            }          

            return 0;
        }
        public int ImportData(List<UserModel> lst)
        {
            DalICCard dal = new DalICCard();
            ArrayList sqlList = new ArrayList();
            string sql = "";

            if (lst != null)
            {
                int ret = 0;
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i] != null)
                    {
                        ret = dal.getImportSql(lst[i]);
                    }
                }
                return ret;
            }

            return 0;
        }
        //查询数据库表UserInfo的LoginName和IICardID
        public List<UserEntry> GetUserEntryByLoginName()
        {
            DalICCard dal = new DalICCard();
            return dal.GetUserEntryByLoginName();
        }

        //查询数据库表UserInfo的所以信息
        public List<UserInfoModel> GetUserEntry()
        {
            DalICCard dal = new DalICCard();
            return dal.GetUserEntry();
        }

    }
}