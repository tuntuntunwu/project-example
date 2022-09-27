using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Model;

namespace BLL
{
    public class BllLDAPTotal
    {
        DalLDAPTotal dal = new DalLDAPTotal();
            
        //更新(连接设置)
        public int Update(LDAPModel bean)
        {
            LDAPEntry entry = new LDAPEntry();
            //连接设置
            entry.Con_IP = bean.Con_IP;
            entry.Con_Port = bean.Con_Port;
            entry.Con_Verification = bean.Con_Verification;
            entry.Con_Account = bean.Con_Account;
            entry.Con_Password = bean.Con_Password;

            //DB使用规则 Duqinyi
            entry.DB_Allowed = bean.DB_Allowed;

            //认证设置
            entry.Ver_Verification = bean.Ver_Verification;
            entry.Ver_Login = bean.Ver_Login;
            entry.Ver_Type = bean.Ver_Type;
            entry.Ver_NTorDNS = bean.Ver_NTorDNS;
            //组设置
            entry.Group_Verification = bean.Group_Verification;
            entry.Group_UserAttribute_or_DN = bean.Group_UserAttribute_or_DN;
            entry.Group_AttributeName = bean.Group_AttributeName;

            //用户设置
            entry.User_DNSetting = bean.User_DNSetting;
            entry.User_Search = bean.User_Search;
            entry.User_Name = bean.User_Name;
            entry.User_Email = bean.User_Email;
            entry.User_ICNum = bean.User_ICNum;
            entry.User_LDAPName = bean.User_LDAPName;
            entry.User_LDAPPassword = bean.User_LDAPPassword;

            int ret = dal.Update(entry);
            return ret;

        }


        public LDAPModel GetLDAPInfo()
        {
            LDAPEntry bean = dal.GetInfoByKey();
            LDAPModel model = new LDAPModel();

            //连接设置
            model.Con_IP = bean.Con_IP;
            model.Con_Port = bean.Con_Port;
            model.Con_Verification = bean.Con_Verification;
            model.Con_Account = bean.Con_Account;
            model.Con_Password = bean.Con_Password;

            //DB使用规则
            model.DB_Allowed = bean.DB_Allowed;

            //认证设置
            model.Ver_Verification = bean.Ver_Verification;
            model.Ver_Login = bean.Ver_Login;
            model.Ver_Type = bean.Ver_Type;
            model.Ver_NTorDNS = bean.Ver_NTorDNS;

            //组设置
            model.Group_Verification = bean.Group_Verification;
            model.Group_UserAttribute_or_DN = bean.Group_UserAttribute_or_DN;
            model.Group_AttributeName = bean.Group_AttributeName;

            //用户设置
            model.User_DNSetting = bean.User_DNSetting;
            model.User_Search = bean.User_Search;
            model.User_Name = bean.User_Name;
            model.User_Email = bean.User_Email;
            model.User_ICNum = bean.User_ICNum;
            model.User_LDAPName = bean.User_LDAPName;
            model.User_LDAPPassword = bean.User_LDAPPassword;


            return model;
        }

       

    }
}
