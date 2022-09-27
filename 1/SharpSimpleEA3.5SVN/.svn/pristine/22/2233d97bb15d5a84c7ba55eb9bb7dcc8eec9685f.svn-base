using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Model;

namespace BLL
{
    public class BllDBAuthConfig
    {
        DalDBThirdConfig dal = new DalDBThirdConfig();


        //cui201770624
        public int Add(DBThirdAuthConfigEntry bean)
        {
            int ret = dal.Add(bean);
            return ret;

        }

        //更新(连接设置)
        public int Update(DBThirdAuthConfigEntry bean)
        {

            DBThirdAuthConfigEntry entry = dal.GetInfoByKey();
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


        public DBThirdAuthConfigEntry GetDBAutoInfo()
        {
            DBThirdAuthConfigEntry bean = dal.GetInfoByKey();

            return bean;
        }

        

    }
}
