using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using DAL;
using BLL;
using Model;
using System.Data.SqlClient;
using System.Text;
namespace common
{
    public class DBAuthHandler2
    {
        
       
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
        public string DBTestSearch(DBThirdAuthSettingEntry bean)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("select ");
            sb.Append(bean.MTFullName);
            sb.Append(", ");
            if (!bean.MTPwd.Equals(""))
            {
                sb.Append(bean.MTPwd);
                sb.Append(", ");
            }
            if (!bean.MTIDCard.Equals(""))
            {
                sb.Append(bean.MTIDCard);
                sb.Append(", ");
            }
            if (!bean.MTGroup.Equals(""))
            {
                sb.Append(bean.MTGroup);
                sb.Append(", ");
            }
            if (!bean.MTEmail.Equals(""))
            {
                sb.Append(bean.MTEmail);
                sb.Append(", ");
            }
            if (!bean.MTPINCode.Equals(""))
            {
                sb.Append(bean.MTPINCode);
                sb.Append(", ");
            }
            sb.Append(bean.MTLoginName);

            sb.Append(" from  ");
            sb.Append(bean.DBTableNM);

            string format3 = "server={0};database={1};uid={2};pwd={3}";
            string connectionString = "";
            connectionString = String.Format(format3, bean.DBAuthServerIP, bean.DBAuthDBNM, bean.DBUserName, bean.DBPassword);

            // Ex SQL.
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    if (ConnectionState.Open == con.State)
                    {
                        SqlCommand cmd = new SqlCommand(sb.ToString(), con);
                        cmd.CommandTimeout = 180;

                        //读取数据
                        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dataReader != null)
                        {
                            dataReader.Close();
                            return "";
                        }
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    string errormsg = ex.Message;
                    return errormsg;
                }
                return "";

            }

        }
        public string DBTestSearch2(DBThirdAuthSettingEntry bean)
        {
            string[] val = bean.UserJudgeFeildVal.Split(',');
            StringBuilder sb = new StringBuilder("");
            sb.Append("select * ");
            sb.Append(" from  ");
            sb.Append(bean.DBTableNM);
            sb.Append(" where ");
            if (val.Length == 1)
            {
                sb.Append(bean.UserJudgeFeild);
                sb.Append(" ='");
                sb.Append(bean.UserJudgeFeildVal);
                sb.Append("' ");
            }
            else
            {
                for(int k = 0; k <val.Length; k ++ )
                {
                    if (k != 0)
                    {
                        sb.Append(" or ");
                    }
                    sb.Append(bean.UserJudgeFeild);
                    sb.Append(" ='");
                    sb.Append(val[k]);
                    sb.Append("' ");
                }
            }

            string format3 = "server={0};database={1};uid={2};pwd={3}";
            string connectionString = "";
            connectionString = String.Format(format3, bean.DBAuthServerIP, bean.DBAuthDBNM, bean.DBUserName, bean.DBPassword);

            // Ex SQL.
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    if (ConnectionState.Open == con.State)
                    {
                        SqlCommand cmd = new SqlCommand(sb.ToString(), con);
                        cmd.CommandTimeout = 180;

                        //读取数据
                        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dataReader != null)
                        {
                            dataReader.Close();
                            return "";
                        }
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    string errormsg = ex.Message;
                    return errormsg;
                }
                return "";

            }

        }
        public string DBConnect(DBThirdAuthSettingEntry bean)
        {
            //string connectionString="Data Source=(local)\SQLEXPRESS;Initial Catalog=SimpleEA;Persist Security Info=True;User ID=sa;Password=chenygls@163.com;pooling=false;";
             //string connectionString = "data source=202.120.84.198;uid=sa;pwd=chen@123;database=NorthGlassDB;pooling=true;min pool size=5;max pool size=512;Connect Timeout=500;";
             //string format1 = "data source={0}:{1};uid={2};pwd={3};database={4};pooling=true;min pool size=5;max pool size=512;Connect Timeout=100;";
             //string format2 = "data source={0};uid={1};pwd={2};database={3};pooling=true;min pool size=5;max pool size=512;Connect Timeout=100;";
             string format3 = "server={0};database={1};uid={2};pwd={3}"; 
             string connectionString = "";
             //if (DBAuthServerPort.Equals(""))
             //{
             //    connectionString = String.Format(format2, DBAuthServerIP, DBUserName, DBPassword, DBAuthDBNM);
             //}
             //else
             //{

             //    connectionString = String.Format(format1, DBAuthServerIP, DBAuthServerPort, DBUserName, DBPassword, DBAuthDBNM);
             //}
             connectionString = String.Format(format3, bean.DBAuthServerIP, bean.DBAuthDBNM, bean.DBUserName, bean.DBPassword);
       
             // Ex SQL.
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    if (ConnectionState.Open == con.State)
                    {
                        con.Close();
                        return "";
                    }
                    else
                    {
                        return "连接数据库失败！";
                    }
                }
                catch (Exception ex)
                {
                    string errormsg = ex.Message;
                    return errormsg;
                }
                return "";

            }
        }
        public Boolean DBLogin(String LoginName,  String LoginPsw)
        {

            //读取配置信息 需要读入哪些字段 displayname or cn
            DalDBThirdAuth authDBDAL = new DalDBThirdAuth();
            DBThirdAuthSettingEntry bean = authDBDAL.GetInfoByKey();
            //从DBAutoSetting读入要获取值得字段名称
             //第三方Database相关
             string DBAuthServerIP = bean.DBAuthServerIP.Trim();
             string DBAuthServerPort = bean.DBAuthServerPort.Trim();
             string DBUserName = bean.DBUserName.Trim();
             string DBPassword = bean.DBPassword.Trim();
             string DBAuthDBNM = bean.DBAuthDBNM.Trim();
             string DBTableNM = bean.DBTableNM.Trim();
             //用户信息匹配
             string MTFullName = bean.MTFullName.Trim();
             string MTLoginName = bean.MTLoginName.Trim();
             string MTPwd = bean.MTPwd.Trim();
             string MTIDCard = bean.MTIDCard.Trim();
             string MTGroup = bean.MTGroup.Trim();
             string MTEmail = bean.MTEmail.Trim();
             string MTPINCode = bean.MTPINCode.Trim();
             int GroupID1 = bean.GroupID1;
             int GroupID2 = bean.GroupID2;
             //卡同步规则
             int IDCordRule = bean.IDCordRule;
             string IDCardDataLen = bean.IDCardDataLen.Trim();
             string IDCardADDPos = bean.IDCardADDPos.Trim();
             string IDCardADDCHR = bean.IDCardADDCHR.Trim();
            //用户信息判断规则
             int UserInfoJudgeRule = bean.UserInfoJudgeRule;
             string UserJudgeFeild = bean.UserJudgeFeild.Trim();
             string UserJudgeFeildVal = bean.UserJudgeFeildVal.Trim();

             StringBuilder sb = new StringBuilder("");
             sb.Append(" SELECT * FROM ");
             sb.Append(DBTableNM );
             sb.Append(" WHERE ");
             sb.Append(MTLoginName);
             sb.Append( "= '" );
             sb.Append(LoginName);
             sb.Append("' and '");
             sb.Append(MTPwd);
             sb.Append("= '");
             sb.Append(LoginPsw);
             sb.Append("' ");

             string strsql = sb.ToString();


            //string connectionString="Data Source=(local)\SQLEXPRESS;Initial Catalog=SimpleEA;Persist Security Info=True;User ID=sa;Password=chenygls@163.com;pooling=false;";
             //string connectionString = "data source=202.120.84.198;uid=sa;pwd=chen@123;database=NorthGlassDB;pooling=true;min pool size=5;max pool size=512;Connect Timeout=500;";
             //string format1 = "data source={0}:{1};uid={2};pwd={3};database={4};pooling=true;min pool size=5;max pool size=512;Connect Timeout=500;";
             //string format2 = "data source={0};uid={1};pwd={2};database={3};pooling=true;min pool size=5;max pool size=512;Connect Timeout=500;";
             string format3 = "server={0};database={1};uid={2};pwd={3}";
             string format4 = "server={0}:{1};database={2};uid={3};pwd={4}";
             string connectionString = "";
             //if (DBAuthServerPort.Equals(""))
             //{
             //    connectionString = String.Format(format2, DBAuthServerIP, DBUserName, DBPassword, DBAuthDBNM);
             //}
             //else
             //{

             //    connectionString = String.Format(format1, DBAuthServerIP, DBAuthServerPort, DBUserName, DBPassword, DBAuthDBNM);
             //}
             if (DBAuthServerPort.Trim().Equals(""))
             {
                 connectionString = String.Format(format3, DBAuthServerIP, DBAuthDBNM, DBUserName, DBPassword);
             }
             else
             {
                 connectionString = String.Format(format4, DBAuthServerIP, DBAuthServerPort, DBAuthDBNM, DBUserName, DBPassword);
             }
             
            
            AuthUserEntry entry = null;

             // Ex SQL.
             using (SqlConnection con = new SqlConnection(connectionString))
             {
                 con.Open();
                 try
                 {
                     using (SqlCommand cmd = new SqlCommand(strsql, con))
                     {
                         //读取数据
                         SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                         if (dataReader.Read())
                         {
                             entry = new AuthUserEntry();
                             entry.UserName = ConvertObjToString(dataReader[MTFullName]);
                             entry.LoginName = ConvertObjToString(dataReader[MTLoginName]);
                             entry.ICCardID = ConvertObjToString(dataReader[MTIDCard]);
                             entry.Group = ConvertObjToString(dataReader[MTGroup]);
                             if (!MTEmail.Equals(""))
                             {
                                 entry.Email = ConvertObjToString(dataReader[MTEmail]);
                             }
                             else
                             {
                                 entry.Email = "";
                             }
                             if (!MTPINCode.Equals(""))
                             {
                                 entry.PinCode = ConvertObjToString(dataReader[MTPINCode]);
                             }
                             else
                             {
                                 entry.PinCode = "";
                             }
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     return false;
                 }
                 finally
                 {
                     //tran.Dispose();
                     //tran = null;
                     ;
                 }
             }

            if( entry == null )
            {
                return false;
            }

             DalUser daluser = new DalUser();

             UserEntry userEntry = daluser.GetInfoByLoginName(entry.LoginName);

            bool hasUser = true;
            if (userEntry.LoginName == null)
            {
                hasUser = false;
            }
            else 
            { 
                hasUser = true;
            }

            userEntry.UserName = entry.UserName;
            userEntry.LoginName = entry.LoginName;
            userEntry.Password = UtilConst.USER_PASSWORD;
            userEntry.ComeFrom = UtilConst.USER_DB;

            if( entry.Group  == "2" ) //当所属部门对应字段值为2时，选择教师组 否则，为学生组
            {
                userEntry.GroupID = GroupID1;
            }else{
                userEntry.GroupID = GroupID2;
            }
            userEntry.ICCardID = entry.ICCardID;
            userEntry.PinCode = entry.PinCode;
            userEntry.Email = entry.Email;
            userEntry.RestrictionID = -1;


            if (hasUser == false)
            {
                daluser.Add(userEntry);//add
            }
            else  //更新这条数据
            {
                daluser.Update(userEntry);
            }

            return true;
        }


    }
}