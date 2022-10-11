using System;
using System.Collections.Generic;
using System.Web;
using DAL;
using Model;


/// <summary>
///BllDropAndPlus 的摘要说明
/// </summary>

namespace BLL
{

    public class BllDropAndPlus
    {
       //DalLDAP dal = new DalLDAP();
       DalDropAndPlus dal = new DalDropAndPlus();
        //cui201770723
       public int Add1(DropAndPlusModel bean)       {
           DropAndPlusEntry entry = new DropAndPlusEntry();     
           entry.CD = bean.CD;
           entry.code = bean.code;
           entry.name = bean.name;

           int ret = dal.Add1(entry);
           return ret;
       }
       public int Add2(DropAndPlusModel bean)
       {
           DropAndPlusEntry entry = new DropAndPlusEntry();
           entry.CD = bean.CD;
           entry.code = bean.code;
           entry.name = bean.name;

           int ret = dal.Add2(entry);
           return ret;
       }

       public int Add3(DropAndPlusModel bean)
       {
           DropAndPlusEntry entry = new DropAndPlusEntry();
           entry.CD = bean.CD;
           entry.code = bean.code;
           entry.name = bean.name;

           int ret = dal.Add3(entry);
           return ret;
       }
       public int Add4(DropAndPlusModel bean)
       {
           DropAndPlusEntry entry = new DropAndPlusEntry();
           entry.CD = bean.CD;
           entry.code = bean.code;
           entry.name = bean.name;

           int ret = dal.Add4(entry);
           return ret;
       }
       public int Add5(DropAndPlusModel bean)
       {
           DropAndPlusEntry entry = new DropAndPlusEntry();
           entry.CD = bean.CD;
           entry.code = bean.code;
           entry.name = bean.name;

           int ret = dal.Add5(entry);
           return ret;
       }
       public int Add6(DropAndPlusModel bean)
       {
           DropAndPlusEntry entry = new DropAndPlusEntry();
           entry.CD = bean.CD;
           entry.code = bean.code;
           entry.name = bean.name;

           int ret = dal.Add6(entry);
           return ret;
       }
       public int Query(DropAndPlusModel bean)
       {
           DropAndPlusEntry entry = new DropAndPlusEntry();
         
           //entry.ID = bean.ID;
           entry.CD = bean.CD;
           entry.code = bean.code;
           entry.name = bean.name;

           int ret = dal.Query(entry);
           return ret;

       }
       public List<DropAndPlusEntry> GetDropPlusListByCD(int cd)
       {
           return dal.GetDropPlusListByCD(cd);
       }
    }
}