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
    public class BllUser 
    {
        DalUser dal = new DalUser();
      
        public bool CheckExist(string username)
        {
            return dal.CheckExist(username);
        }

      
        public int Add(UserModel bean)
        {
            //cui20170620
            UserEntry user = new UserEntry();

            user.UserName = bean.UserName;
            user.LoginName = bean.LoginName;
            user.Email = bean.Email;
            user.ComeFrom = bean.ComeFrom;
            //user.ICCardID = bean.ICCardID;
           // user.GroupID = bean.GroupID;
            

            //user.ID = bean.ID;
            //user.UserName = bean.UserName;
            //user.LoginName = bean.LoginName;
            //user.Password = bean.Password;
            //user.ICCardID = bean.ICCardID;
            //user.PinCode = bean.PinCode;
            //user.Email = bean.Email;
            //user.GroupID = bean.GroupID;
            //user.RestrictionID = bean.RestrictionID;

            //user.CreateTime = DateTime.Now;
            //user.UpdateTime = DateTime.Now;
                       
            int ret = dal.Add(user);
            return ret;

        }
        public int Add(UserEntry bean)
        {
            int ret = dal.Add(bean);
            return ret;
        }

        public int Update(UserModel bean)
        {
            //cui20170620
            UserEntry user = new UserEntry();
            user.ID = bean.ID;
            user.UserName = bean.UserName;
            user.LoginName = bean.LoginName;
            user.Password = bean.Password;
            user.ICCardID = bean.ICCardID;
            user.PinCode = bean.PinCode;
            user.Email = bean.Email;
            user.GroupID = bean.GroupID;
            user.RestrictionID = bean.RestrictionID;
            user.ComeFrom = bean.ComeFrom;
            user.CreateTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;
            int ret = dal.Update(user);
            return ret;

        }
        public int Update(UserEntry bean)
        {
            int ret = dal.Update(bean);
            return ret;

        }

        // public int Delete(int id)
        //{
        //    int ret = dal.Delete(id);
        //    return ret;
        //}

        public int Delete(int id)
        {
            UserEntry user = dal.GetInfoByID(id);
            //user.ModifyDate = DateTime.Now;
            //user.Valid = '0';////cui20170620
            int ret = dal.Update(user);
            return ret;
        }

         //public void search(Page<UserEntry> page, string username)
        //cui20170708
         //public void search(Page<UserEntry> page, Dictionary<String, String> cond)
         //{

         //    dal.search(page, cond);

         //    return;
         //}
         //public void searchM(Page<UserListModel> page, Dictionary<String, String> cond)
         //{

         //    dal.searchM(page, cond);

         //    return;
         //}

        public UserModel GetUserInfo(int id)
        {
           UserEntry entry =  dal.GetInfoByID(id);
           UserModel model = new UserModel();
           ////cui20170620
           model.ID = entry.ID;
           model.UserName = entry.UserName;
           model.LoginName = entry.LoginName;
           model.Password = entry.Password;
           model.ICCardID = entry.ICCardID;
           model.PinCode = entry.PinCode;
           model.Email = entry.Email;
           model.GroupID = entry.GroupID;
           model.RestrictionID = entry.RestrictionID;
           model.CreateTime = entry.CreateTime;
           model.UpdateTime = entry.UpdateTime;
           return model;
        }
        public UserEntry GetInfoByJobID(string JobID)
        {
            UserEntry entry = dal.GetInfoByGroupID(JobID);//GetInfoByJobID(JobID);


            return entry;
        }
        public UserEntry GetInfoByName(string UName)
        {
            UserEntry entry = dal.GetInfoByName(UName);
            return entry;
        }        

        public bool CheckExist(UserEntry user)
        {
            return dal.CheckExist(user);
        }

        /// <summary>
        /// 登录逻辑  1--成功，3--用户名密码不匹配，2--用户不存在
        /// </summary>
        /// <param name="bns"></param>
        /// <returns>1--成功，3--用户名密码不匹配，2--用户不存在</returns>
        public int CheckLogin(UserModel user)
        {
            int id = user.ID;
            UserEntry bean = dal.GetInfoByID(id);
            if (bean != null)
            {
                if (bean.Password == user.Password)
                    return 1;
                else
                    return 3;
            }
            else
            {
                return 2;
            }

        }

        public int GetCount(string JobID)
        {
            int count = 0;
            string condition = "";
            condition = " JobID = '" + JobID + "'";
            count = dal.GetCount(condition);
            return count;
        }

        public List<JobInformationCSVModel> getUserReportCSVList(string SearchTxt)
        {
            return dal.getUserReportCSVList(SearchTxt);
        }
        public List<JobInformationCSVModel> getGroupUserVList(int groupID)
        {
            return dal.getGroupUserVList(groupID);
        }
    }
}
