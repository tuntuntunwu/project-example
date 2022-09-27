using System;
using System.Collections.Generic;
using System.Web;
using DAL;
using Model;

///// <summary>
/////BllGroup 的摘要说明
///// </summary>
//public class BLLRestrictionInfo
//{
//    public BLLRestrictionInfo()
//    {
//        //
//        //TODO: 在此处添加构造函数逻辑
//        //
//    }
//}

namespace BLL
{
    public class BLLRestrictionInfo
    {
        DalRestrictionInfo dal = new DalRestrictionInfo();

        
       
        //添加
        public int Add(RestrictionInfoEntry bean)
        {
            int ret = dal.Add(bean);
            return ret;

        }

        //更新
        public int Update(RestrictionInfoEntry bean)
        {
            int ret = dal.Update(bean);
            return ret;

        }
        
        //查询
        public int GetIDByName(string name)
        {
            int id = dal.GetIDbyName(name);
            return id;
        }



    }
}
