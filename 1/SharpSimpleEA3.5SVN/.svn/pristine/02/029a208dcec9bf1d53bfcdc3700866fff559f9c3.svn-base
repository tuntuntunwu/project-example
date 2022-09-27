using System;
using System.Collections.Generic;
using System.Web;
using DAL;
using Model;

///// <summary>
/////BllGroup 的摘要说明
///// </summary>
//public class BllGroup
//{
//    public BllGroup()
//    {
//        //
//        //TODO: 在此处添加构造函数逻辑
//        //
//    }
//}

namespace BLL
{
    public class BllGroup
    {
        DalGroup dal = new DalGroup();

        
        //cui201770709
        //添加
        public int Add(GroupModel bean)
        {
            GroupEntry entry = new GroupEntry();
          
            entry.ID = bean.ID;
            entry.GroupName = bean.GroupName;
           
          
            int ret = dal.Add(entry);
            return ret;

        }

        //更新
        public int Update_Connection(GroupModel bean)
        {
            GroupEntry entry = new GroupEntry();

            entry.ID = bean.ID;
            entry.GroupName = bean.GroupName;
          
            int ret = dal.Update_Connection(entry);
            return ret;

        }
        
        //查询
        public GroupModel GetLDAPInfo()
        {
            GroupEntry bean = dal.GetInfoByKey();
            GroupModel model = new GroupModel();

            model.ID = bean.ID;
            model.GroupName = bean.GroupName;



            return model;
        }

        public List<JobInformationCSVModel> getGroupReportCSVList(string SearchTxt)
        {
            return dal.getGroupReportCSVList(SearchTxt);
        }


    }
}
