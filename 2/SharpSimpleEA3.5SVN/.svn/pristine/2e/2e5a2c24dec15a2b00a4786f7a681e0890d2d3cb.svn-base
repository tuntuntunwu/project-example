using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Model;

namespace BLL
{
    public class BllDBAuth
    {
        DalDBThirdAuth dal = new DalDBThirdAuth();

        //public bool CheckExist(string username)
        //{
        //    return dal.CheckExist(username);
        //}

        //cui201770624
        public int Add(DBThirdAuthSettingEntry bean)
        {
            int ret = dal.Add(bean);
            return ret;

        }

        //更新(连接设置)
        public int Update(DBThirdAuthSettingEntry bean)
        {

            DBThirdAuthSettingEntry entry = dal.GetInfoByKey();
            int ret;
            if (entry == null)
            {
                ret = dal.Add(bean);
            }
            else
            {
                ret = dal.Update(bean);
            }
            return ret;

        }


        public DBThirdAuthSettingEntry GetDBAutoInfo()
        {
            DBThirdAuthSettingEntry bean = dal.GetInfoByKey();

            return bean;
        }

        

    }
}
