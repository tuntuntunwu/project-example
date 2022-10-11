using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using BLL;
using Model;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.NetworkInformation;

namespace DAL
{
    public class DBAuthHandler
    {
        
        private SQLAuthHelper sqlHelper = new SQLAuthHelper();
        private DBThirdAuthConfigEntry authConfig  = null;
        public DBAuthHandler()
        {
            //读取配置信息 需要读入哪些字段 displayname or cn
            DalDBThirdConfig authDBDAL = new DalDBThirdConfig();
            try{
                authConfig = authDBDAL.GetInfoByKey();
                sqlHelper.setDBServerInfo(authConfig.DBConnectStr);

            }catch(Exception ex)
            {
                authConfig = null;
            }
        }


        public int ConvertObjToInt(Object val)
        {
            int ret = 0;
            try
            {
                if (val == null)
                {
                    ret = 0;
                }
                else
                {
                    ret = Convert.ToInt32(val);
                }
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public string  ConvertObjToString(Object val)
        {
            string ret = "";
            try
            {
                if (val == null)
                {
                    ret = "";
                }
                else
                {
                    ret = Convert.ToString(val);
                }
            }
            catch
            {
                ret = "";
            }
            return ret;
        }
        //第三方DB Server在线检测
        public  bool DBPonlineCheck(string strDBIP)
        {
            bool boolOnlineCheck = false;

            try
            {
                Ping ping = new Ping();
                string ip = strDBIP;
                PingReply reply = ping.Send(ip, 100);
                if (reply.Status == IPStatus.Success)
                {
                    boolOnlineCheck = true;
                }
                return boolOnlineCheck;
            }
            catch (Exception exMFPonlineCheck)
            {
                exMFPonlineCheck.ToString();
                return boolOnlineCheck;
            }
        }
        

        public string DBConnect(DBThirdAuthConfigEntry bean)
        {
            Regex r = new Regex(@"(\d+.\d+.\d+.\d+)");
            //string DBConnectStr = @"Data Source=202.120.87.237\SQLEXPRESS;Initial Catalog=SimpleEA;Persist Security Info=True;User ID=admin;Password=admin;pooling=false;";

            string DBConnectStr = bean.DBConnectStr;

            MatchCollection mc = r.Matches(DBConnectStr);
            if (mc.Count <= 0)
            {
                return "IP地址不正确！";
            }
            string IP = mc[0].Value; //将匹配的字符串添在字符串数组中

            if (DBPonlineCheck(IP) == false)
            {
                return "DBServer 连接不上！";
            }


             sqlHelper.setDBServerInfo(bean.DBConnectStr);
            
            try
            {
                 SqlConnection con = sqlHelper.getDBConnection();
                 con.Close();
            }
            catch(Exception ex )
            {
                return "连接数据库失败！";
            }
            return "";

        }
        public string DBTestSearch(DBThirdAuthConfigEntry bean)
        {
            sqlHelper.setDBServerInfo(bean.DBConnectStr);
            StringBuilder sb = new StringBuilder("");
            sb.Append(bean.DBSearchSql);
            string cmdText = sb.ToString();
            SqlDataReader rec;
            sqlHelper.RunSQL(cmdText, out rec);
            if( rec != null )
            {
                return "";
            }else{
                return "SQL语句有错误！";
            }
        }
        public string DBTestSearch2(DBThirdAuthConfigEntry bean)
        {
            //DBThirdAuthConfigEntry bean = this.authConfig;
            sqlHelper.setDBServerInfo(bean.DBConnectStr);
            StringBuilder sb = new StringBuilder("");
            sb.Append(bean.DBSearchSql);
            string card_id = "1111111111";
            SqlParameter[] paramList = { sqlHelper.CreateInParam("@CARDID", SqlDbType.Char, 10, card_id) };

            SqlDataReader rec;
            try
            {
                sqlHelper.RunSQL(sb.ToString(), paramList, out rec);
                if (rec.Read())
                {
                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "检索SQL语句或Where语句有错误！"; 
            }


        }


        public AuthUserEntry DBSearchBaseICCard(string iccardid)
        {
        
            DBThirdAuthConfigEntry bean = this.authConfig;
            sqlHelper.setDBServerInfo(bean.DBConnectStr);

            StringBuilder sb = new StringBuilder("");
            sb.Append(bean.DBSearchSql);
            SqlParameter[] paramList = { sqlHelper.CreateInParam("@CARDID", SqlDbType.Char, 10, iccardid) };

            
            SqlDataReader rec = null;
            sqlHelper.RunSQL(sb.ToString(),paramList, out rec);
            if (rec.Read())
            {
                AuthUserEntry  entry = Bind(rec);
                return entry;
            }else{
                return null;
            }

        }
        public  AuthUserEntry Bind(SqlDataReader dataReader)
        {
            AuthUserEntry entry = new AuthUserEntry();
            //GroupInfo里面的两个字段
            entry.UserName = ConvertObjToString(dataReader["UserName"]);
            entry.LoginName = ConvertObjToString(dataReader["LoginName"]);
            entry.ICCardID = ConvertObjToString(dataReader["ICCardID"]);
            entry.Password = ConvertObjToString(dataReader["Password"]);
            entry.Group = ConvertObjToString(dataReader["GroupID"]);
            entry.Email = ConvertObjToString(dataReader["Email"]);
            entry.PinCode = ConvertObjToString(dataReader["PINCode"]);
            entry.UserType = ConvertObjToString(dataReader["UserType"]);

            dataReader.Close();

            return entry;
        }
        public string DBAuthCard(string iccardid)
        {
            DBThirdAuthConfigEntry config = this.authConfig;
            sqlHelper.setDBServerInfo(config.DBConnectStr);
            DalUser dal_user = new DalUser();

            string ret = DBConnect(config);
            if (!ret.Equals("")) //第三方数据库连接失败
            {
                if (config.AuthDBFlg == 1) //允许simpleEA DB认证
                {
                    UserEntry entry1 = dal_user.GetInfoByICCard(iccardid);
                    if (entry1.LoginName == null)
                    {
                        return "该卡号不存在，无法登陆!";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "第3方数据库连接失败，无法认证、请联系管理员";
                }
            }
            
            //第三方数据库连接成功
            string MTGrpVal1 = config.MTGrpVal1.Trim();
            string MTGrpVal2 = config.MTGrpVal2.Trim();
            int GroupID1 = config.GroupID1;
            int GroupID2 = config.GroupID2;

             //卡ID前面去0处理
            int zero_cunt = 0;
            char[] card_chars = iccardid.ToCharArray();
            int k = 0;
            for (k = 0; k < card_chars.Length; k++)
            {
                if (card_chars[k] != '0')
                {
                    break;
                }
            }
            string tmpcardid = iccardid.Substring(k, iccardid.Length - k);

            AuthUserEntry authBean = DBSearchBaseICCard(tmpcardid);
            if (authBean == null ||  authBean.LoginName == "")
             {
                 return "该卡号不存在，无法登陆!";
             }

            //判断SimpleEADB中该卡号存在否
             UserEntry entry2 = dal_user.GetInfoByICCard(tmpcardid);
             if (entry2.LoginName != null)
             {
                 entry2.ICCardID = "";
                 //把SimpleEADB中该该卡号置空
                 dal_user.Update(entry2);
             }

            //认证用户有效性
             if (!config.DBWhereSql.Equals(""))
             {
                 string[] UserType = config.DBWhereSql.Split(',');

                 Boolean flg = false;
                 for ( k = 0; k < UserType.Length; k++)
                 {
                     if (authBean.UserType == UserType[k])
                     {
                         flg = true;
                         break;
                     }
                 }
                 if (flg == false)
                 {
                     return "该卡号状态无效，无法登陆!";
                 }

             }
            DalUser daluser = new DalUser();
            UserEntry userEntry = daluser.GetInfoByLoginName(authBean.LoginName);

            bool hasUser = true;
            if (userEntry.LoginName == null)
            {
                hasUser = false;
            }
            else 
            { 
                hasUser = true;
            }

            userEntry.UserName = authBean.UserName;
            userEntry.LoginName = authBean.LoginName;
            //userEntry.Password = authBean.Password;
            userEntry.ComeFrom = UtilConst.USER_DB;

            //当所属部门对应字段值为2时，选择教师组 否则，为学生组
            string[] grpValArray = config.MTGrpVal1.Split(',');
            int  groupflg = 1;
            for( k = 0; k < grpValArray.Length; k ++ )
            {
                if (authBean.Group == grpValArray[k]) 
                {
                    groupflg = 0;
                    break;
                }
            }
            if (groupflg == 0 ) 
            {
                userEntry.GroupID = GroupID1;
            }else{
                userEntry.GroupID = GroupID2;
            }
            userEntry.ICCardID = iccardid;
            userEntry.ComeFrom = 2;  //来自第三方DB

            if (hasUser == false)
            {
                userEntry.Password = authBean.Password;
                userEntry.PinCode = authBean.PinCode;
                userEntry.Email = authBean.Email;
                userEntry.RestrictionID = -1;
                daluser.Add(userEntry);
            }
            else  //更新这条数据
            {
                if (authBean.PinCode != null && authBean.PinCode != "")
                {
                    userEntry.PinCode = authBean.PinCode;
                }
                if (authBean.Email != null && authBean.Email != "")
                {
                    userEntry.Email = authBean.Email;
                }
                daluser.Update(userEntry);
            }

            return "";
        }

        //public string SimpleEADBAuthCard(string iccardid)
        //{
        //    DalUser dal_user = new DalUser();
        //    UserEntry entry = dal_user.GetInfoByICCard(iccardid);
        //    if (entry == null)
        //    {
        //        return "该卡号不存在，无法登陆";
        //    }
        //    return "";
        //}


    }
}