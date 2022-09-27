using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Data;
using System.Windows.Forms;
using System.Web;
using DAL;
using BLL;
using Model;
//using SnmpSharpNet;
using System.Net;
using System.Net.NetworkInformation;

namespace common
{
    public class LDAPHandler
    {
        //导入时使用
        public DataTable GetADALLUsers(LDAPEntry bean, String domainName, String adAdmin, String password, String ouName, String ouName1)
        {
            //int userAccountControl = 0;
            string strUserAccountControl = "";

            DataTable dt = new DataTable();
            dt.Columns.Add("sAMAccountname");//帐号
            dt.Columns.Add("Name");//姓名
            dt.Columns.Add("mail"); //邮箱地址
            dt.Columns.Add("ou"); //ou
            dt.Columns.Add("telephoneNumber"); //电话号码
            dt.Columns.Add("department");  //部门
            dt.Columns.Add("memberOf");  //隶属于
            dt.Columns.Add("givenName");//名
            dt.Columns.Add("initials");//英文缩写
            dt.Columns.Add("sn");//姓
            dt.Columns.Add("displayName");//显示名称
            dt.Columns.Add("description");//描述
            dt.Columns.Add("physicalDeliveryOfficeName");//办公室
            dt.Columns.Add("wwwHomePage");//网页
            dt.Columns.Add("userPrincipalName");//用户登录名
            dt.Columns.Add("logonHours");//登录时间
            dt.Columns.Add("logonWorkstation");//登录到
            dt.Columns.Add("userAccountControl");//用户帐户控制 userAccountControl (启用：512，禁用：514 -- 512 + 2， 密码永不过期：66048 -- 65536 + 512 ,用户禁用：66050 -- 65536 + 512 + 2)
            dt.Columns.Add("accountExpires");//帐户过期
            dt.Columns.Add("streetAddress");//街道
            dt.Columns.Add("postOfficeBox");//邮政信箱
            dt.Columns.Add("l");//市/县
            dt.Columns.Add("st");//省/自治区
            dt.Columns.Add("postalCode");//邮政编码
            dt.Columns.Add("countryCode");//国家/地区
            dt.Columns.Add("primaryGroupID");//组编号
            dt.Columns.Add("title");//职务
            //dt.Columns.Add("department");//部门
            dt.Columns.Add("company");//公司
            dt.Columns.Add("manager");//经理
            dt.Columns.Add("pager");//寻呼机
            dt.Columns.Add("mobile");//移动电话
            dt.Columns.Add("facsimileTelephoneNumber");//传真
            dt.Columns.Add("ipPhone");//IP电话
            dt.Columns.Add("info");//注释




            //添加搜索默认DN设置
            domainName = domainName + @"/";
            domainName = domainName + bean.User_DNSetting;

            string login_name = getLDAPLoginName(bean, adAdmin);
            DirectoryEntry adRoot = new DirectoryEntry("LDAP://" + domainName, login_name, password, AuthenticationTypes.None);

            //DirectoryEntry ou = adRoot.Children.Find("OU=" + ouName);
            //DirectoryEntry ou1 = ou.Children.Find("OU=" + ouName1);
            
            DirectorySearcher mySearcher = new DirectorySearcher(adRoot);
            //DirectorySearcher mySearcher = new DirectorySearcher(adRoot);
            mySearcher.Filter = ("(objectClass=user)"); //user表示用户，group表示组

            if (bean.User_Search.Equals("Base"))
            {
                mySearcher.SearchScope = System.DirectoryServices.SearchScope.Base;
            }
            else if (bean.User_Search.Equals("One Level"))
            {
                mySearcher.SearchScope = System.DirectoryServices.SearchScope.OneLevel;
            }
            else
            {
                mySearcher.SearchScope = System.DirectoryServices.SearchScope.Subtree;
            }
           //mySearcher.SearchScope = System.DirectoryServices.SearchScope.
            try
            {
                mySearcher.PageSize = 10000;
                SearchResultCollection searchResultCollection = mySearcher.FindAll();
                //int count = searchResultCollection.Count;
                foreach (System.DirectoryServices.SearchResult resEnt in searchResultCollection)
                {
                    DataRow dr = dt.NewRow();
                    dr["sAMAccountName"] = string.Empty;
                    dr["Name"] = string.Empty;
                    dr["mail"] = string.Empty;
                    dr["ou"] = string.Empty;
                    dr["department"] = string.Empty;
                    dr["memberOf"] = string.Empty;
                    dr["telephoneNumber"] = string.Empty;
                    dr["givenName"] = string.Empty;
                    dr["initials"] = string.Empty;
                    dr["sn"] = string.Empty;
                    dr["displayName"] = string.Empty;
                    dr["description"] = string.Empty;
                    dr["physicalDeliveryOfficeName"] = string.Empty;
                    dr["wwwHomePage"] = string.Empty;
                    dr["userPrincipalName"] = string.Empty;
                    dr["logonHours"] = string.Empty;
                    dr["logonWorkstation"] = string.Empty;
                    dr["userAccountControl"] = string.Empty;
                    dr["accountExpires"] = string.Empty;
                    dr["streetAddress"] = string.Empty;
                    dr["postOfficeBox"] = string.Empty;
                    dr["l"] = string.Empty;
                    dr["st"] = string.Empty;
                    dr["postalCode"] = string.Empty;
                    dr["countryCode"] = string.Empty;
                    dr["primaryGroupID"] = string.Empty;
                    dr["title"] = string.Empty;
                    dr["department"] = string.Empty;
                    dr["company"] = string.Empty;
                    dr["manager"] = string.Empty;
                    dr["pager"] = string.Empty;
                    dr["mobile"] = string.Empty;
                    dr["facsimileTelephoneNumber"] = string.Empty;
                    dr["ipPhone"] = string.Empty;
                    dr["info"] = string.Empty;

                    DirectoryEntry user = resEnt.GetDirectoryEntry();
                    if (user.Properties.Contains("sAMAccountName"))
                    {
                        dr["sAMAccountName"] = user.Properties["sAMAccountName"][0].ToString();
                    }
                    if (user.Properties.Contains("Name"))
                    {
                        dr["Name"] = user.Properties["Name"][0].ToString();
                    }
                    if (user.Properties.Contains("TelephoneNumber"))
                    {
                        dr["telephoneNumber"] = user.Properties["telephoneNumber"][0].ToString();
                    }
                    if (user.Properties.Contains("mail"))
                    {
                        dr["mail"] = user.Properties["mail"][0].ToString();
                    }
                    //获取OU
                    if (user.Properties.Contains("distinguishedName"))
                    {

                        string ouname = "";
                        if( user.Properties["distinguishedName"].Value != null )
                        {
                            ouname = user.Properties["distinguishedName"].Value.ToString();
                            string ou = "";
                            int start = 0;
                            int end = 0;
                            start = ouname.IndexOf("OU=");
                            if (start != -1)
                            {
                                char ch1 = '0';
                                start = start + 3;
                                for (int k = start; k < ouname.Length; k ++ )
                                {
                                    ch1 = ouname[k];
                                    if (ch1 == ',')
                                    {
                                        end = k;
                                        break;
                                    }
                                }
                                ou = ouname.Substring(start,end-start);
                                dr["ou"] = ou;

                            }
                        }

                    }
                    //
                    if (user.Properties.Contains("department"))
                    {
                        dr["department"] = user.Properties["department"][0].ToString();
                    }
                    if (user.Properties.Contains("memberOf"))
                    {
                        dr["memberOf"] = user.Properties["memberOf"][0].ToString();
                    }
                    if (user.Properties.Contains("givenName"))
                    {
                        dr["givenName"] = user.Properties["givenName"][0].ToString();
                    }
                    if (user.Properties.Contains("initials"))
                    {
                        dr["initials"] = user.Properties["initials"][0].ToString();
                    }
                    if (user.Properties.Contains("sn"))
                    {
                        dr["sn"] = user.Properties["sn"][0].ToString();
                    }
                    if (user.Properties.Contains("displayName"))
                    {
                        dr["displayName"] = user.Properties["displayName"][0].ToString();
                    }
                    if (user.Properties.Contains("description"))
                    {
                        dr["description"] = user.Properties["description"][0].ToString();
                    }
                    if (user.Properties.Contains("physicalDeliveryOfficeName"))
                    {
                        dr["physicalDeliveryOfficeName"] = user.Properties["physicalDeliveryOfficeName"][0].ToString();
                    }
                    if (user.Properties.Contains("wwwHomePage"))
                    {
                        dr["wwwHomePage"] = user.Properties["wwwHomePage"][0].ToString();
                    }
                    if (user.Properties.Contains("userPrincipalName"))
                    {
                        dr["userPrincipalName"] = user.Properties["userPrincipalName"][0].ToString();
                    }
                    if (user.Properties.Contains("logonHours"))
                    {
                        dr["logonHours"] = user.Properties["logonHours"][0].ToString();
                    }
                    if (user.Properties.Contains("logonWorkstation"))
                    {
                        dr["logonWorkstation"] = user.Properties["logonWorkstation"][0].ToString();
                    }

                    strUserAccountControl = "";
                    if (user.Properties.Contains("userAccountControl"))
                    {
                        strUserAccountControl = user.Properties["userAccountControl"][0].ToString();
                        dr["userAccountControl"] = strUserAccountControl;
                    }
                    if (GetUserDelete(strUserAccountControl) ) //禁用账户
                    {
                        continue;
                    }
                    if (user.Properties.Contains("accountExpires"))
                    {
                        dr["accountExpires"] = user.Properties["accountExpires"][0].ToString();
                    }
                    if (user.Properties.Contains("streetAddress"))
                    {
                        dr["streetAddress"] = user.Properties["streetAddress"][0].ToString();
                    }
                    if (user.Properties.Contains("postOfficeBox"))
                    {
                        dr["postOfficeBox"] = user.Properties["postOfficeBox"][0].ToString();
                    }
                    if (user.Properties.Contains("l"))
                    {
                        dr["l"] = user.Properties["l"][0].ToString();
                    }
                    if (user.Properties.Contains("st"))
                    {
                        dr["st"] = user.Properties["st"][0].ToString();
                    }
                    if (user.Properties.Contains("postalCode"))
                    {
                        dr["postalCode"] = user.Properties["postalCode"][0].ToString();
                    }
                    if (user.Properties.Contains("countryCode"))
                    {
                        dr["countryCode"] = user.Properties["countryCode"][0].ToString();
                    }
                    if (user.Properties.Contains("primaryGroupID"))
                    {
                        dr["primaryGroupID"] = user.Properties["primaryGroupID"][0].ToString();
                    }
                    if (user.Properties.Contains("title"))
                    {
                        dr["title"] = user.Properties["title"][0].ToString();
                    }
                    if (user.Properties.Contains("department"))
                    {
                        dr["department"] = user.Properties["department"][0].ToString();
                    }
                    if (user.Properties.Contains("company"))
                    {
                        dr["company"] = user.Properties["company"][0].ToString();
                    }
                    if (user.Properties.Contains("manager"))
                    {
                        dr["manager"] = user.Properties["manager"][0].ToString();
                    }
                    if (user.Properties.Contains("pager"))
                    {
                        dr["pager"] = user.Properties["pager"][0].ToString();
                    }
                    if (user.Properties.Contains("mobile"))
                    {
                        dr["mobile"] = user.Properties["mobile"][0].ToString();
                    }
                    if (user.Properties.Contains("facsimileTelephoneNumber"))
                    {
                        dr["facsimileTelephoneNumber"] = user.Properties["facsimileTelephoneNumber"][0].ToString();
                    }
                    if (user.Properties.Contains("ipPhone"))
                    {
                        dr["ipPhone"] = user.Properties["ipPhone"][0].ToString();
                    }
                    if (user.Properties.Contains("info"))
                    {
                        dr["info"] = user.Properties["info"][0].ToString();
                    }
                    dt.Rows.Add(dr);
                }
                searchResultCollection.Dispose();
                mySearcher.Dispose();
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                if (e.ErrorCode == -2147016646)
                {
                    //MessageBox.Show("连接服务器失败");
                }
                else if (e.ErrorCode == -2147023570)
                {
                    //MessageBox.Show("用户名或密码错误");
                }
                //MessageBox.Show(e.ToString());
                //MessageBox.Show(e.ErrorCode.ToString());
            }

            adRoot.Dispose();
            return dt;

        }

        //认证时使用
        //adAdmin :用户帐号
        //password :密码
        public DataTable GetADUsers(LDAPEntry bean, String domainName, String adAdmin, String password, string searchUserName, String ouName, String ouName1)
        {
            string strUserAccountControl = "";

            DataTable dt = new DataTable();
            dt.Columns.Add("sAMAccountname");//帐号
            dt.Columns.Add("Name");//姓名
            dt.Columns.Add("mail"); //邮箱地址
            dt.Columns.Add("ou"); //ou
            dt.Columns.Add("telephoneNumber"); //电话号码
            dt.Columns.Add("department");  //部门
            dt.Columns.Add("memberOf");  //隶属于
            dt.Columns.Add("givenName");//名
            dt.Columns.Add("initials");//英文缩写
            dt.Columns.Add("sn");//姓
            dt.Columns.Add("displayName");//显示名称
            dt.Columns.Add("description");//描述
            dt.Columns.Add("physicalDeliveryOfficeName");//办公室
            dt.Columns.Add("wwwHomePage");//网页
            dt.Columns.Add("userPrincipalName");//用户登录名
            dt.Columns.Add("logonHours");//登录时间
            dt.Columns.Add("logonWorkstation");//登录到
            dt.Columns.Add("userAccountControl");//用户帐户控制 userAccountControl (启用：512，禁用：514 -- 512 + 2， 密码永不过期：66048 -- 65536 + 512 ,用户禁用：66050 -- 65536 + 512 + 2)
            dt.Columns.Add("accountExpires");//帐户过期
            dt.Columns.Add("streetAddress");//街道
            dt.Columns.Add("postOfficeBox");//邮政信箱
            dt.Columns.Add("l");//市/县
            dt.Columns.Add("st");//省/自治区
            dt.Columns.Add("postalCode");//邮政编码
            dt.Columns.Add("countryCode");//国家/地区
            dt.Columns.Add("primaryGroupID");//组编号
            dt.Columns.Add("title");//职务
            //dt.Columns.Add("department");//部门
            dt.Columns.Add("company");//公司
            dt.Columns.Add("manager");//经理
            dt.Columns.Add("pager");//寻呼机
            dt.Columns.Add("mobile");//移动电话
            dt.Columns.Add("facsimileTelephoneNumber");//传真
            dt.Columns.Add("ipPhone");//IP电话
            dt.Columns.Add("info");//注释

            //添加搜索默认DN设置
            domainName = domainName + @"/";
            domainName = domainName + bean.User_DNSetting;

            //string login_name = getLDAPLoginName(bean, adAdmin);
            DirectoryEntry adRoot = new DirectoryEntry("LDAP://" + domainName, adAdmin, password, AuthenticationTypes.None);

            //DirectoryEntry ou = adRoot.Children.Find("OU=" + ouName);
            //DirectoryEntry ou1 = ou.Children.Find("OU=" + ouName1);

            DirectorySearcher mySearcher = new DirectorySearcher(adRoot);
            //DirectorySearcher mySearcher = new DirectorySearcher(adRoot);
            mySearcher.Filter = "(samaccountname=" + searchUserName + ")"; 

            if (bean.User_Search.Equals("Base"))
            {
                mySearcher.SearchScope = System.DirectoryServices.SearchScope.Base;
            }
            else if (bean.User_Search.Equals("One Level"))
            {
                mySearcher.SearchScope = System.DirectoryServices.SearchScope.OneLevel;
            }
            else
            {
                mySearcher.SearchScope = System.DirectoryServices.SearchScope.Subtree;
            }
            //mySearcher.SearchScope = System.DirectoryServices.SearchScope.
            try
            {
                SearchResultCollection searchResultCollection = mySearcher.FindAll();
                foreach (System.DirectoryServices.SearchResult resEnt in searchResultCollection)
                {
                    DataRow dr = dt.NewRow();
                    dr["sAMAccountName"] = string.Empty;
                    dr["Name"] = string.Empty;
                    dr["mail"] = string.Empty;
                    dr["ou"] = string.Empty;
                    dr["department"] = string.Empty;
                    dr["memberOf"] = string.Empty;
                    dr["telephoneNumber"] = string.Empty;
                    dr["givenName"] = string.Empty;
                    dr["initials"] = string.Empty;
                    dr["sn"] = string.Empty;
                    dr["displayName"] = string.Empty;
                    dr["description"] = string.Empty;
                    dr["physicalDeliveryOfficeName"] = string.Empty;
                    dr["wwwHomePage"] = string.Empty;
                    dr["userPrincipalName"] = string.Empty;
                    dr["logonHours"] = string.Empty;
                    dr["logonWorkstation"] = string.Empty;
                    dr["userAccountControl"] = string.Empty;
                    dr["accountExpires"] = string.Empty;
                    dr["streetAddress"] = string.Empty;
                    dr["postOfficeBox"] = string.Empty;
                    dr["l"] = string.Empty;
                    dr["st"] = string.Empty;
                    dr["postalCode"] = string.Empty;
                    dr["countryCode"] = string.Empty;
                    dr["primaryGroupID"] = string.Empty;
                    dr["title"] = string.Empty;
                    dr["department"] = string.Empty;
                    dr["company"] = string.Empty;
                    dr["manager"] = string.Empty;
                    dr["pager"] = string.Empty;
                    dr["mobile"] = string.Empty;
                    dr["facsimileTelephoneNumber"] = string.Empty;
                    dr["ipPhone"] = string.Empty;
                    dr["info"] = string.Empty;

                    DirectoryEntry user = resEnt.GetDirectoryEntry();
                    if (user.Properties.Contains("sAMAccountName"))
                    {
                        dr["sAMAccountName"] = user.Properties["sAMAccountName"][0].ToString();
                    }
                    if (user.Properties.Contains("Name"))
                    {
                        dr["Name"] = user.Properties["Name"][0].ToString();
                    }
                    if (user.Properties.Contains("TelephoneNumber"))
                    {
                        dr["telephoneNumber"] = user.Properties["telephoneNumber"][0].ToString();
                    }
                    if (user.Properties.Contains("mail"))
                    {
                        dr["mail"] = user.Properties["mail"][0].ToString();
                    }
                    //获取OU
                    if (user.Properties.Contains("distinguishedName"))
                    {

                        string ouname = "";
                        if (user.Properties["distinguishedName"].Value != null)
                        {
                            ouname = user.Properties["distinguishedName"].Value.ToString();
                            string ou = "";
                            int start = 0;
                            int end = 0;
                            start = ouname.IndexOf("OU=");
                            if (start != -1)
                            {
                                char ch1 = '0';
                                start = start + 3;
                                for (int k = start; k < ouname.Length; k++)
                                {
                                    ch1 = ouname[k];
                                    if (ch1 == ',')
                                    {
                                        end = k;
                                        break;
                                    }
                                }
                                ou = ouname.Substring(start, end - start);
                                dr["ou"] = ou;

                            }
                        }

                    }
                    //
                    if (user.Properties.Contains("department"))
                    {
                        dr["department"] = user.Properties["department"][0].ToString();
                    }
                    if (user.Properties.Contains("memberOf"))
                    {
                        dr["memberOf"] = user.Properties["memberOf"][0].ToString();
                    }
                    if (user.Properties.Contains("givenName"))
                    {
                        dr["givenName"] = user.Properties["givenName"][0].ToString();
                    }
                    if (user.Properties.Contains("initials"))
                    {
                        dr["initials"] = user.Properties["initials"][0].ToString();
                    }
                    if (user.Properties.Contains("sn"))
                    {
                        dr["sn"] = user.Properties["sn"][0].ToString();
                    }
                    if (user.Properties.Contains("displayName"))
                    {
                        dr["displayName"] = user.Properties["displayName"][0].ToString();
                    }
                    if (user.Properties.Contains("description"))
                    {
                        dr["description"] = user.Properties["description"][0].ToString();
                    }
                    if (user.Properties.Contains("physicalDeliveryOfficeName"))
                    {
                        dr["physicalDeliveryOfficeName"] = user.Properties["physicalDeliveryOfficeName"][0].ToString();
                    }
                    if (user.Properties.Contains("wwwHomePage"))
                    {
                        dr["wwwHomePage"] = user.Properties["wwwHomePage"][0].ToString();
                    }
                    if (user.Properties.Contains("userPrincipalName"))
                    {
                        dr["userPrincipalName"] = user.Properties["userPrincipalName"][0].ToString();
                    }
                    if (user.Properties.Contains("logonHours"))
                    {
                        dr["logonHours"] = user.Properties["logonHours"][0].ToString();
                    }
                    if (user.Properties.Contains("logonWorkstation"))
                    {
                        dr["logonWorkstation"] = user.Properties["logonWorkstation"][0].ToString();
                    }
                    //if (user.Properties.Contains("userAccountControl"))
                    //{
                    //    dr["userAccountControl"] = user.Properties["userAccountControl"][0].ToString();
                    //}

                    strUserAccountControl = "";

                    if (user.Properties.Contains("userAccountControl"))
                    {
                        strUserAccountControl = user.Properties["userAccountControl"][0].ToString();
                        dr["userAccountControl"] = strUserAccountControl;
                    }
                    //if (GetUserDelete(strUserAccountControl)) //禁用账户
                    //{
                    //    continue;
                    //}

                    if (user.Properties.Contains("accountExpires"))
                    {
                        dr["accountExpires"] = user.Properties["accountExpires"][0].ToString();
                    }
                    if (user.Properties.Contains("streetAddress"))
                    {
                        dr["streetAddress"] = user.Properties["streetAddress"][0].ToString();
                    }
                    if (user.Properties.Contains("postOfficeBox"))
                    {
                        dr["postOfficeBox"] = user.Properties["postOfficeBox"][0].ToString();
                    }
                    if (user.Properties.Contains("l"))
                    {
                        dr["l"] = user.Properties["l"][0].ToString();
                    }
                    if (user.Properties.Contains("st"))
                    {
                        dr["st"] = user.Properties["st"][0].ToString();
                    }
                    if (user.Properties.Contains("postalCode"))
                    {
                        dr["postalCode"] = user.Properties["postalCode"][0].ToString();
                    }
                    if (user.Properties.Contains("countryCode"))
                    {
                        dr["countryCode"] = user.Properties["countryCode"][0].ToString();
                    }
                    if (user.Properties.Contains("primaryGroupID"))
                    {
                        dr["primaryGroupID"] = user.Properties["primaryGroupID"][0].ToString();
                    }
                    if (user.Properties.Contains("title"))
                    {
                        dr["title"] = user.Properties["title"][0].ToString();
                    }
                    if (user.Properties.Contains("department"))
                    {
                        dr["department"] = user.Properties["department"][0].ToString();
                    }
                    if (user.Properties.Contains("company"))
                    {
                        dr["company"] = user.Properties["company"][0].ToString();
                    }
                    if (user.Properties.Contains("manager"))
                    {
                        dr["manager"] = user.Properties["manager"][0].ToString();
                    }
                    if (user.Properties.Contains("pager"))
                    {
                        dr["pager"] = user.Properties["pager"][0].ToString();
                    }
                    if (user.Properties.Contains("mobile"))
                    {
                        dr["mobile"] = user.Properties["mobile"][0].ToString();
                    }
                    if (user.Properties.Contains("facsimileTelephoneNumber"))
                    {
                        dr["facsimileTelephoneNumber"] = user.Properties["facsimileTelephoneNumber"][0].ToString();
                    }
                    if (user.Properties.Contains("ipPhone"))
                    {
                        dr["ipPhone"] = user.Properties["ipPhone"][0].ToString();
                    }
                    if (user.Properties.Contains("info"))
                    {
                        dr["info"] = user.Properties["info"][0].ToString();
                    }
                    dt.Rows.Add(dr);
                }

                searchResultCollection.Dispose();

                mySearcher.Dispose();

            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                if (e.ErrorCode == -2147016646)
                {
                    //MessageBox.Show("连接服务器失败");
                }
                else if (e.ErrorCode == -2147023570)
                {
                    //MessageBox.Show("用户名或密码错误");
                }
                //MessageBox.Show(e.ToString());
                //MessageBox.Show(e.ErrorCode.ToString());
            }

            adRoot.Dispose();
            return dt;

        }

        public string getLDAPLoginName(LDAPEntry bean, string username)
        {
            string login_name = "";
            if (bean.Ver_NTorDNS == null)
            {
                login_name = username;
            }
            else if (bean.Ver_Type.Trim().Equals("NT Domain Name"))
            {
                login_name = bean.Ver_NTorDNS + username;
            }
            else if (bean.Ver_Type.Trim().Equals("User Prinicipal"))
            {
                login_name = username + bean.Ver_NTorDNS;
            }
            else
            {
                login_name = username;
            }
            return login_name;
        }
        //登录名密码认证
        public string IDAuthentication(String username, String password)
        {
            DalLDAP dalLDAP = new DalLDAP();
            LDAPEntry bean = dalLDAP.GetInfoByKey();
            DalUser dal_user = new DalUser();

            String serverIP = bean.Con_IP;
            String port = bean.Con_Port;
            String server = serverIP + ":" + port;


            string login_name = getLDAPLoginName(bean, username);
            //if (bean.Ver_NTorDNS == null)
            //{
            //    login_name = username;
            //}
            //else if (bean.Ver_Type.Trim().Equals("NT Domain Name"))
            //{
            //    login_name = bean.Ver_NTorDNS + username;
            //}
            //else if (bean.Ver_Type.Trim().Equals("User Principal"))
            //{
            //    login_name = username + bean.Ver_NTorDNS;
            //}
            //else
            //{
            //    login_name = username;
            //}
            bool pingcheck = PingCheck(serverIP);
            if(!pingcheck)
            {
                if (bean.DB_Allowed == 1)
                {
                //查找本地DB
                    UserEntry entry1 = dal_user.GetInfoByLoginName(username);
                    if (entry1.LoginName == null)
                    {
                        return "无效用户，请联系管理员!";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    //MessageBox.Show("LDAP服务器连接失败，无法登陆，请联系管理员");
                    return "LDAP服务器连接失败，无法登陆，请联系管理员";
                }
            }
            int ret = IDAuthentication(server, login_name, password);
            if (ret != 0)
            {
                return "账户或密码错误";
            }
            //LDAP服务器验证成功

            DataTable userinfo = GetADUsers(bean, server, login_name, password, username, null, null);
            for (int i = 0; i < userinfo.Rows.Count; i++)
            {
                //string  ldap_user_name = "SIMPLEEA\\" + userinfo.Rows[i][0].ToString();
                string  ldap_user_name = userinfo.Rows[i][0].ToString();
                //if (ldap_user_name == username)
                if (ldap_user_name.ToLower() == username.ToLower())
                {
                    
                    //if( GetUserDelete(userinfo.Rows[i][16].ToString()) )
                    string userAccountControl = userinfo.Rows[i]["userAccountControl"].ToString();

                    if (GetUserDelete(userAccountControl))
                    {
                        return "该账户不可用，请联系管理员";
                    }
                    else
                    {
                        //授权使用复合机
                        updateUser(userinfo, i, bean,1, "");
                        return "";
                    }
                }
            }                
            
            return "该账户不存在，请联系管理员";
        }

        public Boolean PingCheck(string server)
        {
            bool Check = false;
            Ping ping = new Ping();
            PingReply reply = ping.Send(server, 100);
            if (reply.Status == IPStatus.Success)
            {
                Check = true;
            }
            return Check;
        }

        //测试是否成功，（连接设置界面）
        public int TestIDAuthentication(String server, String username, String password)
        {
            DalLDAP dalLDAP = new DalLDAP();
            LDAPEntry bean = dalLDAP.GetInfoByKey();
            String defaultName = getLDAPLoginName(bean, username);
            String defaultPassword = password;
            return IDAuthentication(server, defaultName, defaultPassword);
        }

        public int IDAuthentication(String server, String username, String password)
        {

            DirectoryEntry entry = new DirectoryEntry("LDAP://" + server, username, password, AuthenticationTypes.None);
            try
            {
                object native = entry.NativeObject;

                entry.Dispose();
                return 0;
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                if (e.ErrorCode == -2147016646)
                {
                    //MessageBox.Show("连接服务器失败");
                    return 1;
                }
                else if (e.ErrorCode == -2147023570)
                {
                    return 2;// MessageBox.Show("用户名或密码错误");
                }
                //下面两条弹出具体的出错信息，在实际情况中可不用
                //  MessageBox.Show(e.ToString());
                //  MessageBox.Show(e.ErrorCode.ToString());
            }
            return -1;
        }

        //public void DBupdate(String DomainName, String accountName, String LoginName, String LoginPsw)//ldapIndex域服务器的第几行,从0开始
        //{

        //    //判断该用户的信息是否可用
        //    //从dt中获取用户信息，看该用户是否disabled locked or expired
        //    //用户如果是disabled locked or expired，则退出

        //    //读取配置信息 需要读入哪些字段 displayname or cn
        //    DalLDAP ldapSettingDAL = new DalLDAP();
        //    LDAPEntry ldapSetting = ldapSettingDAL.GetInfoByKey();

        //    DataTable dt = GetADUsers(ldapSetting, DomainName, LoginName, LoginPsw, null, null);

        //    //从LDAPSetting读入要获取值得字段名称
        //    //
        //    String Group_UserAttribute_or_DN = ldapSetting.Group_UserAttribute_or_DN;
        //    String Group_Attribute = ldapSetting.Group_AttributeName;//department,memberof
        //    String Group_Verification = ldapSetting.Group_Verification;

        //    String User_Name = ldapSetting.User_Name;//display,cn
        //    String User_DNSetting = ldapSetting.User_DNSetting;
        //    String User_Email = ldapSetting.User_Email;//mail
        //    String User_ICNum = ldapSetting.User_ICNum;
        //    //String User_LDAPName = ldapSetting.User_LDAPName;
        //    //String User_LDAPPassword = ldapSetting.User_LDAPPassword;
        //    String User_Search = ldapSetting.User_Search;

        //    int RestrictionID = 0;
        //    try
        //    {
        //        RestrictionID = int.Parse(ldapSetting.Ver_Verification);
        //    }
        //    catch (Exception ex)
        //    {
        //        RestrictionID = 0;
        //    }
        //    int rowcount = dt.Rows.Count;
        //    int rowindex = 0;
        //    for (rowindex = 0; rowindex < rowcount; rowindex++)
        //    {
        //        String sAMAccountName = dt.Rows[rowindex]["sAMAccountname"].ToString();
        //        String Username2 = dt.Rows[rowindex][User_Name].ToString();
        //        //更新或新增 所有登录账户下 账号=登录名的 用户的信息
        //        if (accountName.Equals(sAMAccountName) == true)
        //        {
        //            Boolean groupExit = true;
        //            String groupName = "";

        //            //读取ldap用户组名称
        //            try
        //            {
        //                groupName = dt.Rows[rowindex][Group_Attribute].ToString();//sap memberof
        //                if (groupName == null)
        //                {
        //                    groupExit = false;
        //                }
        //                else
        //                {
        //                    groupExit = true;
        //                }
        //            }
        //            catch
        //            {
        //                groupExit = false;
        //            }

        //            if (groupExit == false)
        //            {
        //                groupName = "";
        //            }


        //            //添加用户组
        //            DalGroup dalgroup = new DalGroup();
        //            //根据组名获取组ID
        //            GroupEntry groupentry = new GroupEntry();
        //            if (groupExit)
        //            {
        //                GroupEntry groupentry2 = dalgroup.GetIdbyGroupName(groupName);
        //                if (groupentry2.GroupName == null)
        //                {
        //                    groupentry2.GroupName = groupName;
        //                    groupentry2.RestrictionID = RestrictionID;  //该值是从LDAPSetting中读入的
        //                    dalgroup.Add(groupentry2);//add
        //                    //insert
        //                    groupentry = dalgroup.GetEntrybyGroupName(groupName);
        //                }
        //                else
        //                {
        //                    groupentry2.GroupName = groupName;
        //                    groupentry2.RestrictionID = RestrictionID;  //该值是从LDAPSetting中读入的
        //                    dalgroup.Update(groupentry2);//add
        //                    //insert
        //                    groupentry = groupentry2;
        //                }
        //            }
        //            else
        //            {
        //                groupentry.ID = 0;//无所属
        //            }


        //            DalUser daluser = new DalUser();

        //            //String //如果LDAP的用户信息在表UserInfo中没有 就添加
        //            UserEntry userEntry = daluser.GetInfoByLoginName(accountName);

        //            bool hasUser = true;
        //            if (userEntry.LoginName == null)
        //            {
        //                hasUser = false;
        //            }
        //            else
        //            {
        //                hasUser = true;
        //            }

        //            userEntry.UserName = accountName + @"\" + dt.Rows[rowindex][User_Name].ToString();
        //            userEntry.LoginName = accountName;//LoginName;
        //            //userEntry.Password = "666666";
        //            userEntry.Password = UtilConst.USER_PASSWORD;
        //            userEntry.ComeFrom = UtilConst.USER_LDAP;
        //            userEntry.GroupID = groupentry.ID;
        //            if (User_ICNum.Trim() == "")
        //            {
        //                userEntry.ICCardID = "";
        //            }
        //            else
        //            {
        //                userEntry.ICCardID = dt.Rows[rowindex][User_ICNum].ToString();//域名服务器里面有没有iccard pincode信息?
        //            }
        //            userEntry.PinCode = "";
        //            String Email = dt.Rows[rowindex][User_Email].ToString();
        //            userEntry.Email = Email;
        //            if (groupentry.ID == 0)
        //            {
        //                userEntry.RestrictionID = 0;
        //            }
        //            else
        //            {
        //                userEntry.RestrictionID = -1;
        //            }


        //            if (hasUser == false)
        //            {
        //                daluser.Add(userEntry);//add
        //            }
        //            else  //更新这条数据
        //            {
        //                daluser.Update(userEntry);
        //            }

        //        }
        //    }

        //    //String groupName =dt.Rows[0][4].ToString();//人口贩卖 department
        //    //String icc = "计算中心";

        //}

        public int ImportSyncLDAP()
        {
            //DalLDAP dalLDAP = new DalLDAP();
            //LDAPEntry bean = dalLDAP.GetInfoByKey();
            //string domainName = bean.Con_IP;
            //string LDAPLoginName = bean.Con_Account;
            //string LDAPPassword = bean.Con_Password;

            ////
            //DataTable dt = GetADUsers(bean, domainName, LDAPLoginName, LDAPPassword, null, null);

            //int rowcount = dt.Rows.Count;
            //int rowindex = 0;
            //for (rowindex = 0; rowindex < rowcount; rowindex++)
            //{
            //    updateUser(dt, rowindex, bean);
            //}
            //return rowindex;

            DalLDAP dalLDAP = new DalLDAP();
            LDAPEntry bean = dalLDAP.GetInfoByKey();
            string serverIP = bean.Con_IP;
            string port = bean.Con_Port;
            string domainName = serverIP + ":" + port;

            bool pingcheck = PingCheck(serverIP);
            if (!pingcheck)
            {
                return 0;
            }

            string LDAPLoginName = bean.Con_Account;
            string LDAPPassword = bean.Con_Password;

            //string login_name = getLDAPLoginName(bean, LDAPLoginName);
            DataTable dt = this.GetADALLUsers(bean, domainName, LDAPLoginName, LDAPPassword, null, null);
            int rowcount = dt.Rows.Count;
            int rowindex = 0;
            for (rowindex = 0; rowindex < rowcount; rowindex++)
            {
                updateUser(dt, rowindex, bean,2,"");
            }
            return rowindex;

           
        }

        public void updateUser(DataTable dt, int rowindex, LDAPEntry ldapSetting, int flg, string idcard)
        {
            //flg :1 用户名密码认证， 2 批量导入 3 刷卡登录 4 注册
            //idcart: flg = 1,2,3,时为空，flg=4时，注册用IC卡号 
            //当flg=1,2时，IC卡号的修改规则为
            //     界面参数空时不更新，界面参数不为空时，取ldap的数据更新
            //当flg=3时，根据现在IC卡号的读取用户名和密码，利用该用户到ldap认证，
            //     如果认证不通过.返回错误信息 
            //     否则
            //          界面参数空时不更新，界面参数不为空时，取ldap的数据更新
            //从LDAPSetting读入要获取值得字段名称
            String Group_Attribute = ldapSetting.Group_AttributeName;//department,memberof
            String Group_UserAttribute_or_DN = ldapSetting.Group_UserAttribute_or_DN;
            String Group_Verification = ldapSetting.Group_Verification;

            String User_Name = ldapSetting.User_Name;//display,cn
            String User_DNSetting = ldapSetting.User_DNSetting;
            String User_Email = ldapSetting.User_Email;//mail
            String User_ICNum = ldapSetting.User_ICNum;

            //String User_LDAPName = ldapSetting.User_LDAPName;
            //String User_LDAPPassword = ldapSetting.User_LDAPPassword;
            String User_Search = ldapSetting.User_Search;

            int RestrictionID = 0;
            try
            {
                //RestrictionID = int.Parse(ldapSetting.Ver_Verification);
                RestrictionID = int.Parse(ldapSetting.Group_Verification);
            }
            catch (Exception ex)
            {
                RestrictionID = 0;
            }

            Boolean groupExit = true;
            String groupName = "";
            try
            {
                groupName = dt.Rows[rowindex][Group_Attribute].ToString();//sap memberof
                if (groupName == null || groupName.Trim() == "")
                {
                    groupExit = false;
                }
                else
                {
                    groupExit = true;
                }
            }
            catch
            {
                groupExit = false;
            }

            DalUser daluser = new DalUser();
            DalGroup dalgroup = new DalGroup();
            //根据组名获取组ID
            GroupEntry groupentry = new GroupEntry();
            if (groupExit)
            {
                GroupEntry groupentry2 = dalgroup.GetIdbyGroupName(groupName);
                if (groupentry2.GroupName == null)
                {
                    groupentry2.GroupName = groupName;
                    groupentry2.RestrictionID = RestrictionID;  //该值是从LDAPSetting中读入的
                    dalgroup.Add(groupentry2);//add
                    //insert
                    groupentry = dalgroup.GetEntrybyGroupName(groupName);
                }
                else
                {
                    groupentry2.GroupName = groupName;
                    groupentry2.RestrictionID = RestrictionID;  //该值是从LDAPSetting中读入的
                    dalgroup.Update(groupentry2);//add
                    //insert
                    groupentry = groupentry2;
                }
            }
            else
            {
                groupentry.ID = 0;//无所属
                RestrictionID = 0; //通用配额
            }

            //String //如果LDAP的用户信息在表UserInfo中没有 就添加
            string accountName = dt.Rows[rowindex]["sAMAccountname"].ToString();
            UserEntry userEntry = daluser.GetInfoByLoginName(accountName);

            bool hasUser = true;
            if (userEntry.LoginName == null)
            {
                hasUser = false;
            }
            else
            {
                hasUser = true;
            }
            //userEntry.UserName = accountName + @"\" + dt.Rows[rowindex][User_Name].ToString();
            string user_name = "";
            if (dt.Rows[rowindex][User_Name] == null || "".Equals(dt.Rows[rowindex][User_Name].ToString()))
            {
                user_name = accountName;
            }
            else
            {
                user_name = dt.Rows[rowindex][User_Name].ToString() + @"-" + accountName;
            }
            if (user_name.Length > 30)
            {
                user_name = user_name.Substring(0, 30);
            }
            userEntry.UserName = user_name;
            userEntry.LoginName = accountName;//LoginName;
            userEntry.Password = UtilConst.USER_PASSWORD;
            userEntry.ComeFrom = UtilConst.USER_LDAP;
            userEntry.GroupID = groupentry.ID;
            //if (!User_ICNum.Trim().Equals(""))
            //{
            //    userEntry.ICCardID = dt.Rows[rowindex][User_ICNum].ToString();//画面ICCard设置为空时，不同步ICCard
            //}
            //else
            //{
            //    if (idcard.Trim().Equals(""))
            //    {
            //        userEntry.ICCardID = "";
            //    }
            //    else
            //    {
            //        userEntry.ICCardID = idcard;
            //    }
               
            //}
            if (flg == 1)
            {
                if (!User_ICNum.Trim().Equals(""))
                {
                    userEntry.ICCardID = dt.Rows[rowindex][User_ICNum].ToString();//画面ICCard设置为空时，不同步ICCard
                }
            }
            else if (flg == 2)
            {
                if (!User_ICNum.Trim().Equals(""))
                {
                    userEntry.ICCardID = dt.Rows[rowindex][User_ICNum].ToString();//画面ICCard设置为空时，不同步ICCard
                }
            }
            else if (flg == 3)
            {
                if (!User_ICNum.Trim().Equals(""))
                {
                    userEntry.ICCardID = dt.Rows[rowindex][User_ICNum].ToString();//画面ICCard设置为空时，不同步ICCard
                }
            }
            else //flg ==4 注册
            {
                userEntry.ICCardID = idcard;
            }

            userEntry.PinCode = "";
            if (!User_Email.Trim().Equals(""))
            {
                String Email = dt.Rows[rowindex][User_Email].ToString();
                userEntry.Email = Email;
            }
            else
            {
                userEntry.Email = "";
            }
            //if (groupentry.ID == 0)
            //{
            //    userEntry.RestrictionID = 0;
            //}
            //else
            //{
            //    userEntry.RestrictionID = -1;
            //}


            if (hasUser == false)
            {
                if (userEntry.ICCardID == null)
                {
                    userEntry.ICCardID = "";
                }
                //用户追加时，继承用户组
                if (groupentry.ID == 0)
                {
                    userEntry.RestrictionID = 0;
                }
                else
                {
                    userEntry.RestrictionID = -1;
                }

                daluser.Add(userEntry);//add
            }
            else  //更新这条数据
            {
                //更新是不更新用户配额信息
                daluser.Update(userEntry);
            }


        }

        //public string DBAuthCard(string iccardid)
        //{
        //    DalUser dal_user = new DalUser();
        //    UserEntry entry1 = dal_user.GetInfoByICCard(iccardid);
        //    if (entry1.LoginName == null)
        //    {
        //        //不存在
        //        //调函数
        //        return "";

        //    }
        //    //本地服务器DB用户存在



        //    //string ret = DBConnect(config);
        //    //if (!ret.Equals("")) //第三方数据库连接失败
        //    //{
        //    //    if (config.AuthDBFlg == 1) //允许simpleEA DB认证
        //    //    {
        //    //        if (entry1.LoginName == null)
        //    //        {
        //    //            return "该卡号不存在，无法登陆!";
        //    //        }
        //    //        else
        //    //        {
        //    //            return "";
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        return "第3方数据库连接失败，无法认证、请联系管理员";
        //    //    }
        //    //}

        //    ////第三方数据库连接成功
        //    //string MTGrpVal1 = config.MTGrpVal1.Trim();
        //    //string MTGrpVal2 = config.MTGrpVal2.Trim();
        //    //int GroupID1 = config.GroupID1;
        //    //int GroupID2 = config.GroupID2;

        //    ////卡ID前面去0处理
        //    //int zero_cunt = 0;
        //    //char[] card_chars = iccardid.ToCharArray();
        //    //int k = 0;
        //    //for (k = 0; k < card_chars.Length; k++)
        //    //{
        //    //    if (card_chars[k] != '0')
        //    //    {
        //    //        break;
        //    //    }
        //    //}
        //    //string tmpcardid = iccardid.Substring(k, iccardid.Length - k);

        //    //AuthUserEntry authBean = DBSearchBaseICCard(tmpcardid);
        //    //if (authBean == null || authBean.LoginName == "")
        //    //{
        //    //    return "该卡号不存在，无法登陆!";
        //    //}

        //    ////判断SimpleEADB中该卡号存在否
        //    //UserEntry entry2 = dal_user.GetInfoByICCard(tmpcardid);
        //    //if (entry2.LoginName != null)
        //    //{
        //    //    entry2.ICCardID = "";
        //    //    //把SimpleEADB中该该卡号置空
        //    //    dal_user.Update(entry2);
        //    //}

        //    ////认证用户有效性
        //    //if (!config.DBWhereSql.Equals(""))
        //    //{
        //    //    string[] UserType = config.DBWhereSql.Split(',');

        //    //    Boolean flg = false;
        //    //    for (k = 0; k < UserType.Length; k++)
        //    //    {
        //    //        if (authBean.UserType == UserType[k])
        //    //        {
        //    //            flg = true;
        //    //            break;
        //    //        }
        //    //    }
        //    //    if (flg == false)
        //    //    {
        //    //        return "该卡号不存在，无法登陆!";
        //    //    }

        //    //}
        //    //DalUser daluser = new DalUser();
        //    //UserEntry userEntry = daluser.GetInfoByLoginName(authBean.LoginName);

        //    //bool hasUser = true;
        //    //if (userEntry.LoginName == null)
        //    //{
        //    //    hasUser = false;
        //    //}
        //    //else
        //    //{
        //    //    hasUser = true;
        //    //}

        //    //userEntry.UserName = authBean.UserName;
        //    //userEntry.LoginName = authBean.LoginName;
        //    //userEntry.Password = authBean.Password;
        //    //userEntry.ComeFrom = UtilConst.USER_DB;

        //    //if (authBean.Group == config.MTGrpVal1) //当所属部门对应字段值为2时，选择教师组 否则，为学生组
        //    //{
        //    //    userEntry.GroupID = GroupID1;
        //    //}
        //    //else
        //    //{
        //    //    userEntry.GroupID = GroupID2;
        //    //}
        //    //userEntry.ICCardID = iccardid;
        //    //userEntry.PinCode = authBean.PinCode;
        //    //userEntry.Email = authBean.Email;
        //    //userEntry.RestrictionID = -1;
        //    //userEntry.ComeFrom = 2;  //来自第三方DB

        //    //if (hasUser == false)
        //    //{
        //    //    daluser.Add(userEntry);
        //    //}
        //    //else  //更新这条数据
        //    //{
        //    //    daluser.Update(userEntry);
        //    //}

        //    return "";
        //}

        //刷卡认证
        public string LDAPAuthCard(string iccardid)
        {
            DalUser dal_user = new DalUser();
            DalLDAP dalLDAP = new DalLDAP();
            LDAPEntry bean = dalLDAP.GetInfoByKey();
            String serverIP = bean.Con_IP;
            String port = bean.Con_Port;
            String server = serverIP + ":" + port;

            //第一步：
            //判断SimpleEADB中该卡号存在否
            UserEntry entry2 = dal_user.GetInfoByICCard(iccardid);
            if (entry2 == null || entry2.LoginName == null || entry2.LoginName.Equals(""))
            {
                //return "卡号不存在，无法登陆，请联系管理员";
                return "iccardnotindb";
            }
            bool pingcheck = PingCheck(serverIP);
            if (!pingcheck)
            {
                if (bean.DB_Allowed == 1)
                {
                    return "";
                }
                else
                {
                    //MessageBox.Show("LDAP服务器连接失败，无法登陆，请联系管理员");
                    return "LDAP服务器连接失败，无法登陆，请联系管理员";
                }
            }
            //第二步：用配置文件中的帐号和密码连接ldap服务器


            //默认配置文件中的账号密码
            String defaultName = getLDAPLoginName(bean, bean.Con_Account); 
            String defaultPassword = bean.Con_Password;

            int ret = IDAuthentication(server, defaultName, defaultPassword);
            if (ret != 0)
            {
                //以默认账号密码登陆
                //MessageBox.Show("本地默认账号配置文件有误");
                return "本地默认账号配置文件有误";
            }
            else //LDAP服务器验证成功
            {
                DataTable userinfo = GetADUsers(bean, server, defaultName, defaultPassword, entry2.LoginName, null, null);
                for (int i = 0; i < userinfo.Rows.Count; i++)
                {
                    //第三步：读取ldap中的所有的帐号信息，检查卡号对应的用户名entry2.LoginName是否存在与ldap
                    //string ldap_user_name = "SIMPLEEA\\" + userinfo.Rows[i][0].ToString(); //有三种获得名字的方式，需要修改？？？？？
                    string ldap_user_name = userinfo.Rows[i][0].ToString(); //有三种获得名字的方式，需要修改？？？？？
                    //if (ldap_user_name == entry2.LoginName)
                    //if (ldap_user_name == entry2.LoginName)
                    if (ldap_user_name.ToLower() == entry2.LoginName.ToLower())
                    {
                        //第四步：看该用户状态
                        //if (userinfo.Rows[i][16].ToString() == "514" || userinfo.Rows[i][16].ToString() == "66050")
                        if (GetUserDelete(userinfo.Rows[i]["userAccountControl"].ToString()))
                        {
                            //MessageBox.Show("该账户不可用，请联系管理员");
                            return "该账户不可用，请联系管理员";
                        }
                        else
                        {
                            //更新DB
                            updateUser(userinfo, i, bean,3,"");

                            UserEntry entry3 = dal_user.GetInfoByICCard(iccardid);
                            if (entry3 == null || entry3.LoginName == null || entry3.LoginName.Equals(""))
                            {
                                return "卡号无效";
                            }
                            //授权使用复合机
                            return "";
                        }
                    }
                }
                //MessageBox.Show("账号不存在");
                return "账号不存在";
            }
            return "账号不存在";
        }


        //打印认证
        public string LDAPAuthPrint(string _loginName)
        {
            DalUser dal_user = new DalUser();
            DalLDAP dalLDAP = new DalLDAP();
            LDAPEntry bean = dalLDAP.GetInfoByKey();
            String serverIP = bean.Con_IP;
            String port = bean.Con_Port;
            String server = serverIP + ":" + port;


            bool pingcheck = PingCheck(serverIP);
            if (!pingcheck)
            {
                return "LDAP服务器连接失败，无法登陆，请联系管理员";
            }
            //第二步：用配置文件中的帐号和密码连接ldap服务器


            //默认配置文件中的账号密码
            String defaultName = getLDAPLoginName(bean, bean.Con_Account);
            String defaultPassword = bean.Con_Password;

            int ret = IDAuthentication(server, defaultName, defaultPassword);
            if (ret != 0)
            {
                //以默认账号密码登陆
                //MessageBox.Show("本地默认账号配置文件有误");
                return "本地默认账号配置文件有误";
            }
            else //LDAP服务器验证成功
            {

                DataTable userinfo = GetADUsers(bean, server, defaultName, defaultPassword, _loginName, null, null);
                for (int i = 0; i < userinfo.Rows.Count; i++)
                {
                    //第三步：读取ldap中的所有的帐号信息，检查卡号对应的用户名entry2.LoginName是否存在与ldap
                    //string ldap_user_name = "SIMPLEEA\\" + userinfo.Rows[i][0].ToString(); //有三种获得名字的方式，需要修改？？？？？
                    string ldap_user_name = userinfo.Rows[i][0].ToString(); //有三种获得名字的方式，需要修改？？？？？
                    //if (ldap_user_name == _loginName)
                    if (ldap_user_name.ToLower() == _loginName.ToLower())
                    {
                        //第四步：看该用户状态
                        //if (userinfo.Rows[i][16].ToString() == "514" || userinfo.Rows[i][16].ToString() == "66050")
                        if (GetUserDelete(userinfo.Rows[i]["userAccountControl"].ToString()))
                        {
                            //MessageBox.Show("该账户不可用，请联系管理员");
                            return "该账户不可用，请联系管理员";
                        }
                        else
                        {
                            //更新DB
                            updateUser(userinfo, i, bean,1,"");
                            //授权使用复合机
                            return "";
                        }
                    }
                }
                return "账号不存在";
            }
            return "账号不存在";
        }


        /// <summary>
        /// 根据AD域的userAccountControl属性判断用户是否禁用
        /// </summary>
        /// <param name="userAccContr"></param>
        /// <returns>是否禁用</returns>
        private bool GetUserDelete(string strUuserAccContr)
        {
            int userAccContr = 0;
             if (string.IsNullOrEmpty(strUuserAccContr))
             {
                 return true;
             }
            try{
                userAccContr = int.Parse(strUuserAccContr);
            }catch(Exception ex)
            {
                return true;
            }
            if (userAccContr >= 16777216)            //TRUSTED_TO_AUTH_FOR_DELEGATION - 允许该帐户进行委派
            {
                userAccContr = userAccContr - 16777216;
            }
            if (userAccContr >= 8388608)            //PASSWORD_EXPIRED - (Windows 2000/Windows Server 2003) 用户的密码已过期
            {
                userAccContr = userAccContr - 8388608;
            }
            if (userAccContr >= 4194304)            //DONT_REQ_PREAUTH
            {
                userAccContr = userAccContr - 4194304;
            }
            if (userAccContr >= 2097152)            //USE_DES_KEY_ONLY - (Windows 2000/Windows Server 2003) 将此用户限制为仅使用数据加密标准 (DES) 加密类型的密钥
            {
                userAccContr = userAccContr - 2097152;
            }
            if (userAccContr >= 1048576)            //NOT_DELEGATED - 设置此标志后，即使将服务帐户设置为信任其进行 Kerberos 委派，也不会将用户的安全上下文委派给该服务
            {
                userAccContr = userAccContr - 1048576;
            }
            if (userAccContr >= 524288)            //TRUSTED_FOR_DELEGATION - 设置此标志后，将信任运行服务的服务帐户（用户或计算机帐户）进行 Kerberos 委派。任何此类服务都可模拟请求该服务的客户端。若要允许服务进行 Kerberos 委派，必须在服务帐户的 userAccountControl 属性上设置此标志
            {
                userAccContr = userAccContr - 524288;
            }
            if (userAccContr >= 262144)            //SMARTCARD_REQUIRED - 设置此标志后，将强制用户使用智能卡登录
            {
                userAccContr = userAccContr - 262144;
            }
            if (userAccContr >= 131072)            //MNS_LOGON_ACCOUNT - 这是 MNS 登录帐户
            {
                userAccContr = userAccContr - 131072;
            }
            if (userAccContr >= 65536)            //DONT_EXPIRE_PASSWORD-密码永不过期
            {
                userAccContr = userAccContr - 65536;
            }
            if (userAccContr >= 2097152)            //MNS_LOGON_ACCOUNT - 这是 MNS 登录帐户
            {
                userAccContr = userAccContr - 2097152;
            }
            if (userAccContr >= 8192)            //SERVER_TRUST_ACCOUNT - 这是属于该域的域控制器的计算机帐户
            {
                userAccContr = userAccContr - 8192;
            }
            if (userAccContr >= 4096)            //WORKSTATION_TRUST_ACCOUNT - 这是运行 Microsoft Windows NT 4.0 Workstation、Microsoft Windows NT 4.0 Server、Microsoft Windows 2000 Professional 或 Windows 2000 Server 并且属于该域的计算机的计算机帐户
            {
                userAccContr = userAccContr - 4096;
            }
            if (userAccContr >= 2048)            //INTERDOMAIN_TRUST_ACCOUNT - 对于信任其他域的系统域，此属性允许信任该系统域的帐户
            {
                userAccContr = userAccContr - 2048;
            }
            if (userAccContr >= 512)            //NORMAL_ACCOUNT - 这是表示典型用户的默认帐户类型
            {
                userAccContr = userAccContr - 512;
            }

            if (userAccContr >= 256)            //TEMP_DUPLICATE_ACCOUNT - 此帐户属于其主帐户位于另一个域中的用户。此帐户为用户提供访问该域的权限，但不提供访问信任该域的任何域的权限。有时将这种帐户称为“本地用户帐户”
            {
                userAccContr = userAccContr - 256;
            }
            if (userAccContr >= 128)            //ENCRYPTED_TEXT_PASSWORD_ALLOWED - 用户可以发送加密的密码
            {
                userAccContr = userAccContr - 128;
            }
            if (userAccContr >= 64)            //PASSWD_CANT_CHANGE - 用户不能更改密码。可以读取此标志，但不能直接设置它
            {
                userAccContr = userAccContr - 64;
            }
            if (userAccContr >= 32)            //PASSWD_NOTREQD - 不需要密码
            {
                userAccContr = userAccContr - 32;
            }
            if (userAccContr >= 16)            //LOCKOUT
            {
                userAccContr = userAccContr - 16;
            }
            if (userAccContr >= 8)            //HOMEDIR_REQUIRED - 需要主文件夹
            {
                userAccContr = userAccContr - 8;
            }
            //if (userAccContr >= 2)            //ACCOUNTDISABLE - 禁用用户帐户
            //{
            //    userAccContr = userAccContr - 2;
            //}
            //if (userAccContr >= 1)            //SCRIPT - 将运行登录脚本
            //{
            //    userAccContr = userAccContr - 1;
            //}
            if (userAccContr >= 2)
            {
                return true;
            }
            return false;
        }
        //登录名密码认证
        public string ADCardRegister(String username, String password, String idcard)
        {
            DalLDAP dalLDAP = new DalLDAP();
            LDAPEntry bean = dalLDAP.GetInfoByKey();
            DalUser dal_user = new DalUser();

            String serverIP = bean.Con_IP;
            String port = bean.Con_Port;
            String server = serverIP + ":" + port;


            string login_name = getLDAPLoginName(bean, username);
            
            bool pingcheck = PingCheck(serverIP);
            if (!pingcheck)
            {
                return "LDAP服务器连接失败，无法注册，请联系管理员";
            }
            int ret = IDAuthentication(server, login_name, password);
            if (ret != 0)
            {
                return "账户或密码错误";
            }
            //LDAP服务器验证成功
            bool isExist = false;
            DataTable userinfo = GetADUsers(bean, server, login_name, password,username, null, null);
            for (int i = 0; i < userinfo.Rows.Count; i++)
            {
                string ldap_user_name = userinfo.Rows[i][0].ToString();
                if (ldap_user_name.ToLower() == username.ToLower())
                //if (ldap_user_name == username)
                {
                    isExist = true;
                    updateUser(userinfo, i, bean,4, idcard);
                    break;
                }
                
            }
            if (isExist == false)
            {
                return "该账户不可用，请联系管理员";
            }

            //查找本地DB
            UserEntry userbean = dal_user.GetInfoByLoginName(username);
            if (userbean.LoginName == null)
            {
                return "无效用户，请联系管理员!";
            }
            if (userbean.ICCardID != idcard)
            {
                return "LDAP中无此卡号，注册失败！";
            }

            return "";
        }

    }
}