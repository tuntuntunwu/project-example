#region Copyright SHARP Corporation
//	Copyright (c) 2010 SHARP CORPORATION. All rights reserved.
//
//	SHARP Simple EA Application
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Osa.MfpWebService;
using System.Net;

/// <summary>
/// Summary description for UtilConst
/// </summary>
public class UtilConst
{

    public const string USER_PASSWORD = "666666";
    public const int USER_SYS = 0;
    public const int USER_LDAP = 1;
    public const int USER_DB = 2;

    public const string CON_MONEY_FORMAT="{0:N2}";
    public const string CON_PAPER_FORMAT="{0:N0}";

    public const string CON_MONEY_D_FORMAT = "{0:##0.##}";
    public const string CON_PAPER_D_FORMAT = "{0:##0}";
    

    // Form Timeout
    public const int CON_FORM_TIMEOUT = 30;
    // Defulat date
    // User Information:Admin User
    // Group Information:Not belong Group
    public const string CON_DATE_ID = "0";
    public const string CON_SECUADMIN_ID = "-1";
    //chen 20140513 add
    public const string CON_INHERIT_GROUP = "-1";
    //end
    //chen add for price 20140425 start
    public const int CON_DEFAULT_PRICEID = 0;
    public const int CON_DEFAULT_PRICEDETAILID = 0;

    public const int CON_DEFAULT_PAPERTYPE_A4 = 2;


    public const int CON_PRICE_MODE_1 = 0; //面数（耗材）*单价
    public const int CON_PRICE_MODE_2 = 1; //总张数+面数*单价

    public const int CON_UNLIMIT_NUM = 10000; //无限制时，最大的复印量

    public const int CON_COUNT_MODE_MONEY = 0;
    public const int CON_COUNT_MODE_PAPER = 1;

    //2015 04 22 ADD START
    public const int CON_TONGXIN_EMAIL = 0;  //email 通信
    public const int CON_TONGXIN_WC = 1;     // 微信通信
    public const int CON_LOGIN_USERNAME = 0;     // 用户名登录
    public const int CON_LOGIN_PINCODE = 1;     // pin code登录
    //2015 04 22 ADD END


    public const int CON_DISP_A3_A3 = 0;
    public const int CON_DISP_A3_A4 = 1;

    //认证方式
    public const int CON_LOGIN_AUTH_METHOD_SYS = 0;
    public const int CON_LOGIN_AUTH_METHOD_LDAP = 1;
    public const int CON_LOGIN_AUTH_METHOD_DB = 2;

    //chen add for price 20140425 end

    // 2010.11.27 Add By SES.Jijianxiong Ver.1.1 Update ST
    public const int USER_UNKNOW = -1;
    // 2010.11.27 Add By SES.Jijianxiong Ver.1.1 Update ST
    // Group Information:Admin Group
    public const string CON_DATE_ADMIN_ID = "-1";
    public const string CON_DATE_ADMIN_NAME = "admin";
    public const string CON_USER_LONINNAME = "admin";

    //2019 ADD
    public const string CON_USER_SECUADMIN = "secuadmin";
    
    // 2011.06.01 Add By SLC zhoumiao Ver.1.1  ST
    public const string CON_DATE_SYSTEM_NAME = "system";
    // 2011.06.01 Add By SLC zhoumiao Ver.1.1  ED
    // Page Size: Others.
    public const int CON_DATE_OTHER_PAGE = 27;
    // Page Size: Unused.
    public const int CON_DATE_UNUSED_PAGE = 26;

    public const int CON_PAGE_A3 = 2;
    public const int CON_PAGE_A4 = 3;


    // WebConfig Item
    // Web Config AppSetting Item : IC Card Flg.
    public const string WEBCONFIG_ICCARD = "ICCardLogin";
    public const string WEBCONFIC_ICCARDLEN = "ICCardLen";
    public const int WEBCONFIC_ICCARDLEN_DEFULAT = 10;

    //Le Ning Added 2014-9-10
    public const string WEBCONFIC_EXTRALIMIT = "ExtraLimit";
    public const int WEBCONFIC_EXTRALIMIT_DEFULAT = 100;

    //chen add
    public const string WEBCONFIC_ICCARDLOGIN_DEFULAT = "false";

    // Web Config AppSetting Item : Normal Login Flg
    public const string WEBCONFIG_NORMAL_LOGIN = "NormalLogin";

    // Button Name Of Select Or DeSelect Button
    public const string CON_SELECT_ALL = "全部选中";
    public const string CON_DESELECT_ALL = "解除全选";

    // page name
    // User Managemetn
    public const string CON_PAGE_USERLIST = "用户管理";
    // Update By Jijianxiong 2010-08-23 ST
    // Edit User
    // public const string CON_PAGE_USEREDIT = "用户编辑";
    public const string CON_PAGE_EDIT = "编辑";
    // Add User
    // public const string CON_PAGE_USERADD = "新用户追加";
    public const string CON_PAGE_ADD = "追加";
    // Update By Jijianxiong 2010-08-23 ED
    // Group Managemetn
    public const string CON_PAGE_GROUPLIST = "用户组管理";
    // Edit Group
    public const string CON_PAGE_GROUPEDIT = "用户组编辑";
    // Add Group
    public const string CON_PAGE_GROUPADD = "新用户组追加";
    //Restriction Set Managemet

    // public const string CON_PAGE_USEREDIT = "用户编辑";
    //chen add 2015
    public const string CON_MFP_STATUS = "MFP状态";

    public const string CON_PEIE_JICHENG_GROUP = "继承用户组";


    //pupeng 2014-6-5
    public const string CON_ID_RESTRICLIST_GENERAL = "0";//通用配额ID
    public const string CON_ID_RESTRICLIST_GROUP = "-1";//继承用户组ID


    public const string CON_PAGE_RESTRICLIST = "配额管理";
    //Edit Restriction Set.
    public const string CON_PAGE_RESTRICEDIT = "配额管理编辑";
    //Add Restriction Set.
    public const string CON_PAGE_RESTRICADD = "配额管理追加";
    // Job Report
    public const string CON_PAGE_JOBREPORT = "统计";
    // User Job Report
    public const string CON_PAGE_USERJOBREPORT = "用户使用统计";
    // TotalJob Report
    public const string CON_PAGE_TOTALJOBREPORT = "总体使用统计";
    // MFP Job Report
    public const string CON_PAGE_MFPJOBREPORT = "MFP使用统计";
    // Group Job Report
    public const string CON_PAGE_GROUPJOBREPORT = "用户组使用统计";
    // Group User Job Report
    public const string CON_PAGE_GROUPUSERJOBREPORT = "用户、组汇总统计";
    // Report Total
   // public const string CON_PAGE_JOBREPORTTOTAL = "总体使用报表";
     public const string CON_PAGE_JOBREPORTTOTAL = "作业类型使用报表";
    // Report Group
    public const string CON_PAGE_JOBREPORTGROUP = "用户组使用报表";
    // Report User
    public const string CON_PAGE_JOBREPORTUSER = "用户使用报表";
    // Report MFP
    public const string CON_PAGE_JOBREPORTMFP = "MFP使用报表";
    // View Print Content 
    // Add by Wei Changye 2012.01.13
    public const string CON_PAGE_VIEWPRINTCONTENT = "查看用户打印内容";
    // Settings
    public const string CON_PAGE_SET = "系统设定";
    // Follow ME Settings
    public const string CON_PAGE_FOLLOWME_SET = "FollowME设定";
    // ImportUserInfo
    public const string CON_PAGE_USER_INFO_IMPORT = "用户信息导入";
    // ImportGroupInfo
    public const string CON_PAGE_GROUP_INFO_IMPORT = "组信息导入";

    public const string CON_PAGE_SERVERIP_SET = "ServerIP设定";

    public const string CON_PAGE_LDAP_CONNECT_SET = "LDAP连接设置";

    public const string CON_PAGE_LDAP_VERIFICATION_SET = "LDAP认证设置";

    public const string CON_PAGE_LDAP_GROUP_SET = "LDAP组设置";

    public const string CON_PAGE_LDAP_USER_SET = "LDAP用户设置";

    public const string CON_PAGE_LDAP_SYNC_SET = "LDAP同步";

    public const string CON_PAGE_SECTION_SET = "集团设置";

    public const string CON_PAGE_SECTION_GROUP_SET = "集团组设置";


    //chen add
    public const string CON_APP_NAME = "SimpleEA";
    public const string CON_APP_UI_ADDR = "Default.aspx";
    public const string CON_APP_SERVICE_ADDR = "MfpSink.asmx";

    //
    // Available
    //chen
    //public const string CON_PAGE_AVAILABLE = "用户可使用量查询";
    //public const string CON_PAGE_AVAILREPORTMFP = "用户可使用量报表";
    public const string CON_PAGE_AVAILABLE = "用户余额管理";
    public const string CON_PAGE_AVAILREPORTMFP = "用户余额报表";

    public const string CON_TARGET_GROUP = "统计对象（用户组）";
    public const string CON_TARGET_USER = "统计对象（用户）";

    public const string CON_TARGET_ALL = "统计对象（所有用户）";

    // Item name
    // User Name
    public const string CON_ITEM_USERNAME = "用户名";

    // Login Name
    public const string CON_ITEM_LOGINNAME = "登录名";

    // Group Name
    public const string CON_ITEM_GROUPNAME = "用户组名称";

    // IDCARD Name
    public const string CON_ITEM_IDCARDNAME = "IC卡编号";
    //Pincode
    public const string CON_ITEM_PINCODENAME = "Pin码";
    // Group User Count
    public const string CON_ITEM_GROUPUSERCOUNT = "所属用户数";

    // MFP Model Name
    public const string CON_ITEM_MFPModelName = "MFP型号";
    // MFP SerialNumber
    public const string CON_ITEM_MFPSerialNumber = "MFP序列号";
    // MFP IPAddress
    public const string CON_ITEM_MFPIPAddress = "IP地址";
    // MFP Location
    public const string CON_ITEM_MFPLocation = "位置";

    // MFP STATUS
    public const string CON_ITEM_MFP_STATUS_INPUT = "直接输入";
    public const string CON_ITEM_MFP_STATUS_IC = "返回";

    // Sort ASC
    public const string CON_ITEM_SORT_ASC = "▲";
    // Sort DESC
    public const string CON_ITEM_SORT_DESC = "▼";
    // Name of Check Box Control in Detail List Page.
    public const string CON_ITEM_DETAIL_CHECKBOX = "chkSelect";
    //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ST
    // Restriction Set Name
    //public const string CON_ITEM_RESTRICNAME = "限制项目设定名";
    //chen update
    //public const string CON_ITEM_RESTRICNAME = "限制项目设定名（设定内容概要）";
    public const string CON_ITEM_RESTRICNAME = "配额方案名称";
    //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ED
    //chen add st
    public const string CON_ITEM_PRICENAME = "价格方案名称";
    public const string CON_ITEM_PRINTTASK = "用户登录名";
    //chen add ed
    // ILLEGAL Check
    // public const string CON_VAL_ILLEGAL = "[^\\/:*?&quot;&lt;&gt;|]*";
    public const string CON_VAL_ILLEGAL = "[^,<>&/|]+";

    // Year List's Start year.
    public const int CON_START_YEAR = 2000;

    // Total Job Screen
    // Kind of the Total target
    public const string CON_TOTALTARGET_USER = "0";
    public const string CON_TOTALTARGET_USERNAME = "用户";
    public const string CON_TOTALTARGET_GROUP = "1";
    public const string CON_TOTALTARGET_GROUPNAME = "用户组";

    // Message

    // User/Group Edit page Update button.
    public const string MSG_UPDATE_UPDATE = "是否保存现在的更改？";
    // User/Group Edit page Cancel button.
    public const string MSG_UPDATE_CANCEL = "请注意没有保存的信息将会丢失。<BR>是否退出编辑画面？";
    // Group list page Delete button.
    public const string MSG_GROUP_DELETE = "是否删除选中的用户组？";
    // Group Edit page GroupName is Exist
    public const string MSG_GROUP_NAMEEXIST = "该用户组名称已被占用。";
    // Group Edit page GroupName is Must
    public const string MSG_GROUP_NAMEMUST = "请输入用户组名称。";
    // Can't be with illegal Char
    public const string MSG_ILLEGAL_STRING = "不能包含特殊字符(<,>,&,/,|)以及逗号。";
    // Restriction Set list page Delete button.
    public const string MSG_RESTRIC_DELETE = "是否删除选中的配额方案？";
    // PrintTask Set list page Delete button.
    public const string MSG_PRINTTask_DELETE = "是否删除选中的打印任务？";
    //ContentBackupSetting Delete configering button
    public const string MSG_COPY_DELETE = "是否删除选中的留底记录？";

    // User Edit page UserName is Must
    public const string MSG_USER_NAMEMUST = "请输入用户名。";
    // User Edit page LoginName is Must.
    public const string MSG_LOGIN_NAMEMUST = "请输入登录名。";
    // User Edit page Password is Must.
    public const string MSG_PASSWORD_NAMEMUST = "请输入密码。";
    // User Edit page PasswordConfirm is Must.
    public const string MSG_PASSWORDCONFIRM_NAMEMUST = "请再一次输入密码。";
    // Confirm Password is not Equal Password
    public const string MSG_PASSWORD_NOEQUAL = "确认密码不一致。";
    // IC CardID Must be Numberic and English.
    public const string MSG_ICCARD_CODE = "只能输入英文和数字。";
    // User Edit page ICCard is Must
    public const string MSG_ICCARD_MUST = "请输入IC卡编号。";
    // User Edit page ICCard is Exist
    public const string MSG_ICCARD_EXIST = "该IC卡编号已被占用。";

    public const string MSG_PINCODE_EXIST = "该Pin码已被占用。";
    public const string MSG_PINCODE_CODE = "只能输入数字，且长度为4-20位。";


    //2015 04 23
    public const string MSG_LOGIN_PINCODE = "请输入PIN码。";

    // 2011.03.28 Add By SES Jijianxiong ST
    // Admin Edit page Old Password is Must.
    public const string MSG_ADMIN_OLDPASSWORD_MUST = "请输入原密码。";
    public const string MSG_ADMIN_OLDPASSWORD_ERR = "原密码输入错误，请重新输入。";
    // Admin Edit page Password is Must.
    public const string MSG_ADMIN_PASSWORD_MUST = "请输入新密码。";
    // Admin Edit page PasswordConfirm is Must.
    public const string MSG_ADMIN_PASSWORDCONFIRM_MUST = "请再一次输入新密码。";
    // Admin Confirm Password is not Equal Password
    public const string MSG_ADMIN_PASSWORD_NOEQUAL = "两次输入的新密码不一致，请重新输入。";
    // 2011.03.28 Add By SES Jijianxiong ED


    // Delete process success.
    public const string MSG_DELETE_SUCCESS = "您的操作已成功，您选择的用户已删除！";
    public const string MSG_DELETE_NOTHING = "没有选择任何用户，请选择您要删除的用户。";

    //chen add 
    public const string MSG_DELETE_SUCCESS_2 = "您的操作已成功，您选择的用户部分已删除，存在部分用户有使用数据不能删除！";
    public const string MSG_DELETE_NOTHING_2 = "选择的用户都有使用数据，不能删除，请重新选择您要删除的用户。";
    //
    public const string MSG_DELETE_SUCCESS_GROUP = "您的操作已成功，您选择的用户组已删除！";
    public const string MSG_DELETE_NOTHING_GROUP = "没有选择任何用户组，请选择您要删除的用户组。";

    public const string MSG_DELETE_SUCCESS_PRICE = "您的操作已成功，您选择的价格方案已删除！";
    public const string MSG_DELETE_SUCCESS_RES = "您的操作已成功，您选择的限制项目已删除！";
    public const string MSG_DELETE_NOTHING_PRICE = "没有选择任何价格方案项目，请选择您要删除的价格方案项目。";
    public const string MSG_DELETE_NOTHING_RES = "没有选择任何限制项目，请选择您要删除的限制项目。";

    public const string MSG_SELECT_NOTHING = "没有选择任何内容，请选择您要统计的纪录。";

    public const string MSG_SELECT_EXPORT_NOTHING = "选择条件对象为空，请重新输入选择条件。";
    // User Edit page ICCard is Must
    public const string MSG_MIDDLEWARE_ERROR = "请确认SimpleEA安装目录下的App_Data文件夹asp.net用户是否有写操作权限。";

    // User Delete File Error
    public const string MSG_DELETE_FILE_ERROR = "你未选择删除文件或您选择的删除文件已不存在。";

    // User Edit page LoginName is Must.
    public const string MSG_LOGIN_NAMEMUST_EN = "Please Enter Login Name.";
    // User Edit page Password is Must.
    public const string MSG_PASSWORD_NAMEMUST_EN = "Please Enter Password.";
    // Please Input Password.
    public const string MSG_MFP_CARD_MUST_EN = "Please Put IC Card.";
    public const string MSG_LOGIN_CARD_NOTEXIST_EN = "The Card Id is not exist in the Simple EA System.";
    public const string MSG_MFP_LOGIN_ERROR_EN = "Login Failed, Please confirm your Login Name and Password!";


    // Password Must be 
    // 1.Length > 3
    // 2.Only ABC and Numberic.
    public const string MSG_PASSWORD_CHECK = "密码长度在4-10之间， 只能包含英文字符和数字。";
    public const string MSG_EMAIL_CHECK = "Email地址不合法";
    // User Edit page LoginName is Exist
    public const string MSG_LOGIN_NAMEEXIST = "该登录名已被占用。";
    // User list page Delete button.
    //public const string MSG_USER_DELETE = "删除用户后会影响计数信息，是否删除选中的用户？";
    public const string MSG_USER_DELETE = "是否删除选中的用户？";
    // Restriction information's LimitNum Must Be numberic
    public const string MSG_LIMITNUM_NUM = "请输入数字（1～9999）";
    // Restriction information's Name is Must
    public const string MSG_RESTRICT_NAMEMUST = "请输入配额方案名称。";
    //pupeng add 2014-4-14
    public const string MSG_RESTRICT_AllQuota = "请输入配额。";
    public const string MSG_RESTRICT_ColorQuota = "请输入彩色配额。";
    public const string MSG_RESTRICT_OverLimit = "请输入透支上限。";

    public const string MSG_RESTRICT_AllQuota_NUM = "配额必须为非负数，小数点后只能有2位有效数字，不能超过上限99999.99。";
    public const string MSG_RESTRICT_ColorQuota_NUM = "彩色配额必须为非负数，小数点后只能有2位有效数字，不能超过上限99999.99。";
    public const string MSG_RESTRICT_OverLimit_NUM = "透支上限必须为非负数，小数点后只能有2位有效数字，不能超过上限99999.99。";

    public const string MSG_RESTRICT_ColorQuota_CUSTOM = "彩色配额必须小于等于配额。";
    public const string MSG_RESTRICT_OverLimit_CUSTOM = "透支上限范围在0～+10。";
    // Restriction Set Edit page Restriction Set Name is Exist
    public const string MSG_RESTRICT_NAMEEXIST = "该配额方案名称已被占用。";
    // End Date Is less then Start Date
    public const string MSG_DATETIME = "终了时间不能小于开始时间，请重新选择。";
    //In Total Job Report NoDate for User.
    //2011.3.23 Update By SES zhoumiao Ver.1.1 Update  ST
    //public const string MSG_NODATE_USER = "指定时间内,指定用户使用的页数为0。";
    public const string MSG_NODATE_USER = "指定时间内,指定用户使用的面数为0。";
    // 2011.3.23 Update By SES zhoumiao Ver.1.1 Update ED
        
    //In Total Job Report NoDate for Group.
    //2011.3.23 Update By SES zhoumiao Ver.1.1 Update  ST
    //public const string MSG_NODATE_GROUP = "指定时间内,指定用户组使用的页数为0。";
    public const string MSG_NODATE_GROUP = "指定时间内,指定用户组使用的面数为0。";
    // 2011.3.23 Update By SES zhoumiao Ver.1.1 Update ED
    
    // Save Period Settings Completed.
    public const string MSG_PERIOD_SAVED = "您的设定已保存！";
    // End Date Is less then Start Date
    public const string MSG_ERROR = "您的操作没有成功，请重新操作！";
    // MFP Login Error
    public const string MSG_MFP_LOGIN_ERROR = "登录失败，请确认登录名和密码是否正确！";
    public const string MSG_LOGIN_CARD_UNSUPPORT = "非本系统支持的IC卡，请使用正确的IC卡！";
    public const string MSG_LOGIN_CARD_INVALID_CARD = "IC Card号不正确。请重新刷卡！";
    //public const string MSG_LOGIN_CARD_NOTEXIST = "The Card Id {0} is not exist in the Simple EA System. Please check your IC Card.";
    public const string MSG_LOGIN_CARD_NOTEXIST = "指定卡号不存在。";
    // Please Input Login Name and Password.
    public const string MSG_MFP_LOGIN_ERROR_BLANK = "请输入用户名和密码！";
    // Please Input Password.
    public const string MSG_MFP_CARD_MUST = "请输入IC卡号。";

    public const string MSG_MFP_ERRORMFP = "这台一体机不支持Simple EA 应用程序。|详细内容请参照Simple EA的使用说明手册。";

    public const string MSG_DELETEPRINTTASK_SUCCESS_RES = "您的操作已成功，您选择的作业已删除！";

    public const string MSG_DELETEPRINTTASK_NOTHING_RES = "没有选择任何作业，请选择您要删除的作业。";

    //  Restriction information table's Status
    // 1:Unlimited 2:Limit to 3:prohibition
//    public const int STATUS_UNLIMITED = 1;
//    public const int STATUS_LIMIT = 2;
//    public const int STATUS_PROHIBITION = 3;

    //public const int STATUS_NOTSETLIMIT = 1; //没有设定
    public const int STATUS_UNLIMITED = 1; //可以使用
    public const int STATUS_PROHIBITION = 2;     //禁止使用



    public const string STATUS_UNLIMITED_NAME = "无限制";
    // 2011.01.10 Add By SES zhoumiao Ver.1.1 Update ST
    public const string STATUS_PROHIBITION_NAME = "禁用";
    // 2011.01.10 Add By SES zhoumiao Ver.1.1 Update ED

    // Setting management Information.
    // SetPeriod    1:Every month, 2:Every week, 3:Everyday
    public const int SETPERIOD_MONTH = 1;
    public const int SETPERIOD_WEEK = 2;
    public const int SETPERIOD_DAY = 3;
    // 2014.04.28 Add By SES chen youguang Ver.1.1 Update ST
    public const int SETPERIOD_UNLIMIT = 4;
    // 2014.04.28 Add By SES chen youguang Ver.1.1 Update ED

    // SetPeriodTime
    // Case SetPeriod is 1:Every month
    //     1:5, 2:10, 3:15, 4:20, 5:25, 6:last
    public const int SETPERIODTIME_MONTH_ITME5 = 1;
    public const string SETPERIODTIME_MONTH_ITME5_NAME = "5日";
    public const int SETPERIODTIME_MONTH_ITME10 = 2;
    public const string SETPERIODTIME_MONTH_ITME10_NAME = "10日";
    public const int SETPERIODTIME_MONTH_ITME15 = 3;
    public const string SETPERIODTIME_MONTH_ITME15_NAME = "15日";
    public const int SETPERIODTIME_MONTH_ITME20 = 4;
    public const string SETPERIODTIME_MONTH_ITME20_NAME = "20日";
    public const int SETPERIODTIME_MONTH_ITME25 = 5;
    public const string SETPERIODTIME_MONTH_ITME25_NAME = "25日";
    public const int SETPERIODTIME_MONTH_ITMELAST = 6;
    public const string SETPERIODTIME_MONTH_ITMELAST_NAME = "末日";
    // Case SetPeriod is 2:Every week
    //     1:Monday, 2:Tuesday, 3:Wednesday, 4:Thursday, 5:Friday, 6:Saturday, 7:Sunday
    // 1:Monday
    public const int SETPERIODTIME_WEEK_MON = 1;
    public const string SETPERIODTIME_WEEK_MON_NAME = "星期一";
    // 2:Tuesday
    public const int SETPERIODTIME_WEEK_TUE = 2;
    public const string SETPERIODTIME_WEEK_TUE_NAME = "星期二";
    // 3:Wednesday
    public const int SETPERIODTIME_WEEK_WED = 3;
    public const string SETPERIODTIME_WEEK_WED_NAME = "星期三";
    // 4:Thursday
    public const int SETPERIODTIME_WEEK_THU = 4;
    public const string SETPERIODTIME_WEEK_THU_NAME = "星期四";
    // 5:Friday
    public const int SETPERIODTIME_WEEK_FRI = 5;
    public const string SETPERIODTIME_WEEK_FRI_NAME = "星期五";
    // 6:Saturday
    public const int SETPERIODTIME_WEEK_SAT = 6;
    public const string SETPERIODTIME_WEEK_SAT_NAME = "星期六";
    // 7:Sunday
    public const int SETPERIODTIME_WEEK_SUN = 7;
    public const string SETPERIODTIME_WEEK_SUN_NAME = "星期日";

    // Case SetPeriod is 3:Everyday
    //     0
    public const int SETPERIODTIME_DAY = 0;


    // Report Type
    // TOTAL JOB REPORT SCREEN (USER)
    public const int REPORT_TYPE_TOTAL_USER = 0;
    // TOTAL JOB REPORT SCREEN (GROUP)
    public const int REPORT_TYPE_TOTAL_GROUP = 1;
    // USER JOB REPORT SCREEN.
    public const int REPORT_TYPE_USER = 2;
    // GROUP JOB REPORT SCREEN.
    public const int REPORT_TYPE_GROUP = 3;
    // MFP JOB REPORT SCREEN.
    public const int REPORT_TYPE_MFP = 4;

    // IN XXX JOB REPORT SCREEN-> REPORT SCREEN
    // PARAM'S NAME
    // REPORT TYPE
    public const string PARAM_REPORT_TYPE = "REPORT_TYPE";
    // START TIME
    public const string PARAM_START_TIME = "START_TIME";
    // END TIME
    public const string PARAM_END_TIME = "END_TIME";
    // ID LIST
    public const string PARAM_ID_LIST = "ID_LIST";

    //TIME's FORMAT
    public const string TIME_FORMAT = "yyyy/MM/dd HH:mm:ss";

    // Result screen.
    // Job Name
    // Other
    public const string ITEM_TITLE_Other = "其他";
    public const int ITEM_TITLE_Other_JobId = 0;
    public const int ITEM_TITLE_Other_FunctionId1 = 1;
    public const int ITEM_TITLE_Other_FunctionId2 = 2;
    // Copy
    public const string ITEM_TITLE_Copy = "复印";
    public const int ITEM_TITLE_Copy_JobId = 1;
    public const int ITEM_TITLE_Copy_FunctionId1 = 1;
    public const int ITEM_TITLE_Copy_FunctionId2 = 2;
    // Print
    public const string ITEM_TITLE_Print = "打印";
    public const int ITEM_TITLE_Print_JobId = 2;
    public const int ITEM_TITLE_Print_FunctionId1 = 1;
    public const int ITEM_TITLE_Print_FunctionId2 = 2;
    // Document Filing Print
    public const string ITEM_TITLE_DFPrint = "文件归档打印";
    public const int ITEM_TITLE_DFPrint_JobId = 3;
    public const int ITEM_TITLE_DFPrint_FunctionId1 = 1;
    public const int ITEM_TITLE_DFPrint_FunctionId2 = 2;
    // Scan Save
    public const string ITEM_TITLE_ScanSave = "扫描并保存";
    public const int ITEM_TITLE_ScanSave_JobId = 4;
    public const int ITEM_TITLE_ScanSave_FunctionId1 = 1;
    public const int ITEM_TITLE_ScanSave_FunctionId2 = 2;
    // List Print
    public const string ITEM_TITLE_ListPrint = "清单打印";
    public const int ITEM_TITLE_ListPrint_JobId = 5;
    public const int ITEM_TITLE_ListPrint_FunctionId1 = 1;
    public const int ITEM_TITLE_ListPrint_FunctionId2 = 2;
    // Scan
    public const string ITEM_TITLE_Scan = "扫描";
    public const int ITEM_TITLE_Scan_JobId = 6;
    public const int ITEM_TITLE_Scan_FunctionId1 = 1;
    public const int ITEM_TITLE_Scan_FunctionId2 = 2;
    // Fax
    public const string ITEM_TITLE_Fax = "传真";
    public const int ITEM_TITLE_Fax_JobId = 8;
    public const int ITEM_TITLE_Fax_FunctionId1 = 1;
    // Fax (Channel2)
    public const string ITEM_TITLE_FaxC2 = "传真(Channel2)";
    public const int ITEM_TITLE_FaxC2_JobId = 8;
    public const int ITEM_TITLE_FaxC2_FunctionId1 = 2;
    // Internet Fax
    public const string ITEM_TITLE_IntFax = "网络传真";
    public const int ITEM_TITLE_IntFax_JobId = 7;
    public const int ITEM_TITLE_IntFax_FunctionId1 = 1;


    // CSS ITEM NAME
    public const string CSS_ITEM_EVEN = "even";
    public const string CSS_ITEM_ODD = "odd";
    public const string CSS_SMALL_ROW = "Table_top_secondbg";
    public const string CSS_FOOT_ROW = "Normalfont_black footcolor";

    public const string CSS_ITEM_TDWithTop = "tblDetailTDWithTop";

    public const string CSS_ITEM_TDWithLeft = "tblDetailTDWithLeft";


    // NAME OF COLOR
    public const string COLOR_BW = "黑白";
    public const string COLOR_FULLCOLOR = "彩色";

    //2010.11.15 Add By SES zhoumiao Ver.1.1 Update ST
    // UserInfo  
    //ICCARD ID
    public const string CON_ITEM_ICCARD = "IC卡编号";
    //RESTRICSET
    //chen update 
    //public const string CON_ITEM_RESTRICSET = "限制条件";
    public const string CON_ITEM_RESTRICSET = "配额方案";
    //CardID AND Restrict Are DIVISIBLE
    public const string CSS_U_1_USERNAME_WIDTH = "25%";
    public const string CSS_U_1_LOGINNAME_WIDTH = "25%";
    public const string CSS_U_1_GROUPAME_WIDTH = "40%";
    //Only CardID DIVISIBLE
    public const string CSS_U_2_USERNAME_WIDTH = "18%";
    public const string CSS_U_2_LOGINNAME_WIDTH = "18%";
    public const string CSS_U_2_RESTRICT_WIDTH = "19%";
    public const string CSS_U_2_GROUPAME_WIDTH = "34%";
    //Only Restrict DIVISIBLE
    public const string CSS_U_3_USERNAME_WIDTH = "18%";
    public const string CSS_U_3_LOGINNAME_WIDTH = "18%";
    public const string CSS_U_3_CARDID_WIDTH = "19%";
    public const string CSS_U_3_GROUPAME_WIDTH = "34%";
    //DIVISIBLE
    public const string CSS_ITEM_DISPLAY = "viewDisplay";
    //2010.11.15 Add By SES zhoumiao Ver.1.1 Update  ED

    //2010.11.17 Add By SES zhoumiao Ver.1.1 Update ST
    // GroupInfo  
    //Restrict Are DIVISIBLE
    public const string CSS_G_GROUPAME_WIDTH = "76%";
    //2010.11.17 Add By SES zhoumiao Ver.1.1 Update  ED

    // 2010.11.17 Add By SES Jijianxiong Ver.1.1 Update ST
    // Log Information Job Status:
    // 0:Create
    public const string JOB_STATUS_CREATE = "0";
    public const string JOB_STATUS_CREATENAME = "开始";
    // 3: Canceled
    public const string JOB_STATUS_CANCELED = "3";
    public const string JOB_STATUS_CANCELEDNAME = "取消";
    // 4: Suspended
    public const string JOB_STATUS_SUSPENDED = "4";
    public const string JOB_STATUS_SUSPENDEDNAME = "挂起";
    // 5: Finished
    public const string JOB_STATUS_FINISHED = "5";
    public const string JOB_STATUS_FINISHEDNAME = "完成";
    // 6: Error
    public const string JOB_STATUS_ERROR = "6";
    public const string JOB_STATUS_ERRORNAME = "错误";
    // 2010.11.17 Add By SES Jijianxiong Ver.1.1 Update ED

    // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ST
    // OSA Custom Error Code.
    public const string OSA_ERROR_USERCODE_CANCELJOB = "SIMPLEEA_OTHER";
    // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ED

    // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST

    // Display setting for the Total Report Job Screen.
    // No Item Count Column is visible
    public const string CSS_USERTOTAL_0_USERNAME_WIDTH = "14%";
    public const string CSS_USERTOTAL_0_GROUPNAME_WIDTH = "77%";
    public const string CSS_GROUPTOTAL_0_GROUPNAME_WIDTH = "92%";
    // Only One Count Column is visible
    public const string CSS_USERTOTAL_1_USERNAME_WIDTH = "14%";
    public const string CSS_USERTOTAL_1_GROUPNAME_WIDTH = "64%";
    public const string CSS_GROUPTOTAL_1_GROUPNAME_WIDTH = "79%";
    // Only Two Count Column is visible
    public const string CSS_USERTOTAL_2_USERNAME_WIDTH = "14%";
    public const string CSS_USERTOTAL_2_GROUPNAME_WIDTH = "51%";
    public const string CSS_GROUPTOTAL_2_GROUPNAME_WIDTH = "66%";
    // All Three Count Column is visible
    public const string CSS_USERTOTAL_3_USERNAME_WIDTH = "14%";
    public const string CSS_USERTOTAL_3_GROUPNAME_WIDTH = "38%";
    public const string CSS_GROUPTOTAL_3_GROUPNAME_WIDTH = "53%";

    // Display setting for all Count Column in all report screen.
    public const string CSS_JOB_COUNT_WIDTH = "12%";

    // 2011.3.22 Update By SES zhoumiao Ver.1.1 Update ST
    //// Total Name
    //public const string CON_ITEM_TOTAL = "总使用页数";
    //// B.W Total Name
    //public const string CON_ITEM_BWTOTAL = "黑白页合计";
    //// Full-Color Total Name
    //public const string CON_ITEM_FULLTOTAL = "彩色页合计";
    // Total Name
    public const string CON_ITEM_TOTAL = "总使用面数";
    // B.W Total Name
    public const string CON_ITEM_BWTOTAL = "黑白面合计";
    // Full-Color Total Name
    public const string CON_ITEM_FULLTOTAL = "彩色面合计";

    // 2011.3.22 Update By SES zhoumiao Ver.1.1 Update ED

    // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED

    // 2010.12.9 Delete By SES zhoumiao Ver.1.1 Update ED 
    // 2010.11.23 Add By SES zhoumiao Ver.1.1 Update ST
    //// Display setting for the Group Report Job Screen.
    //// No Item Count Column is visible
    //public const string CSS_GROUP_TOTAL_0_GROUPNAME_WIDTH = "75%";
    //// Only One Count Column is visible
    //public const string CSS_GROUP_TOTAL_1_GROUPNAME_WIDTH = "64%";
    //// Only Two Count Column is visible
    //public const string CSS_GROUP_TOTAL_2_GROUPNAME_WIDTH = "51%";
    //// All Three Count Column is visible
    //public const string CSS_GROUP_TOTAL_3_GROUPNAME_WIDTH = "38%";

    //// Display setting for the MFP Report Job Screen.
    //// No Item Count Column is visible
    //public const string CSS_MFPTOTAL_0_SERIALNUMBER_WIDTH = "20%";
    //public const string CSS_MFPTOTAL_0_MODELNAME_WIDTH = "22%";
    //public const string CSS_MFPTOTAL_0_IPADDRESS_WIDTH = "20%";
    //public const string CSS_MFPTOTAL_0_LOCATION_WIDTH = "28%";
    //// Only One Count Column is visible
    //public const string CSS_MFPTOTAL_1_SERIALNUMBER_WIDTH = "20%";
    //public const string CSS_MFPTOTAL_1_MODELNAME_WIDTH = "16%";
    //public const string CSS_MFPTOTAL_1_IPADDRESS_WIDTH = "17%";
    //public const string CSS_MFPTOTAL_1_LOCATION_WIDTH = "24%";
    //// Only Two Count Column is visible
    //public const string CSS_MFPTOTAL_2_SERIALNUMBER_WIDTH = "16%";
    //public const string CSS_MFPTOTAL_2_MODELNAME_WIDTH = "14%";
    //public const string CSS_MFPTOTAL_2_IPADDRESS_WIDTH = "15%";
    //public const string CSS_MFPTOTAL_2_LOCATION_WIDTH = "20%";
    //// All Three Count Column is visible
    //public const string CSS_MFPTOTAL_3_SERIALNUMBER_WIDTH = "13%";
    //public const string CSS_MFPTOTAL_3_MODELNAME_WIDTH = "12%";
    //public const string CSS_MFPTOTAL_3_IPADDRESS_WIDTH = "14%";
    //public const string CSS_MFPTOTAL_3_LOCATION_WIDTH = "14%";
    //// Display setting for all Count Column in all report screen.
    //public const string CSS_MFPTOTAL_COUNT_WIDTH = "11%";
    //// 2010.11.23 Add By SES zhoumiao Ver.1.1 Update ED
    // 2010.12.9 Delete By SES zhoumiao Ver.1.1 Update ED

    // 2010.11.24 Add By SES zhoumiao Ver.1.1 Update ST
    //At least one item must be selected
    // 2011.01.19 Update By SES zhoumiao Ver.1.1 Update ST
    //public const string MSG_LEAST_ONE_SELECT = "请必须至少选择一个显示项目。";
    public const string MSG_LEAST_ONE_SELECT = "请至少选择一个显示项目。";
    // 2011.01.19 Update By SES zhoumiao Ver.1.1 Update ED
    // 2010.11.24 Add By SES zhoumiao Ver.1.1 Update ED

    //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
    // User Name target
    public const string CON_ITEM_USER = "0";
    //Login Name target
    public const string CON_ITEM_LOGIN = "1";
    // Group Name target
    public const string CON_ITEM_GROUP = "2";
    // Group Name target
    public const string CON_ITEM_IDCARD = "3";
    // Pin code  target
    public const string CON_ITEM_PINCODE = "4";
    //Restriction Set Name
    public const string CON_ITEM_STRICNAME = "项目设定名";
    //Restriction Set Name target
    public const string CON_ITEM_STRIC = "5";
    //MFP Model target
    public const string CON_ITEM_MFPModel = "6";
    //MFP Serial target
    public const string CON_ITEM_MFPSerial = "7";

    //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ED

    // 2010.12.07 Add By SES Jijianxiong ST
    // Debug: MFP Hello Event Error.
    public const string MSG_MFP_HELLO_ERROR = "Debug: MFP Hello Event Error.";
    // 2010.12.07 Add By SES Jijianxiong ED

    //2010.12.9 Add By SES zhoumiao Ver.1.1 Update ED
    // Display setting for the Total Report Job Screen.
    // No Item Count Column is visible
    public const string CSS_TOTAL_0_NAME_WIDTH = "53%";
    // Only One Count Column is visible
    public const string CSS_TOTAL_1_NAME_WIDTH = "40%";
    //  Two Count Column is visible
    public const string CSS_TOTAL_2_NAME_WIDTH = "27%";
    // Display setting for the Group Report Job Screen.
    // No Item Count Column is visible
    public const string CSS_GROUP_0_NAME_WIDTH = "44%";
    // Only One Count Column is visible
    public const string CSS_GROUP_1_NAME_WIDTH = "31%";
    //  Two Count Column is visible
    public const string CSS_GROUP_2_NAME_WIDTH = "18%";
    // Display setting for the MFP Report Job Screen.
    // No Item Count Column is visible
    public const string CSS_MFPTOTAL_0_SERIALNUMBER_WIDTH = "29%";
    public const string CSS_MFPTOTAL_0_MODELNAME_WIDTH = "25%";
    // Only One Count Column is visible
    public const string CSS_MFPTOTAL_1_SERIALNUMBER_WIDTH = "23%";
    public const string CSS_MFPTOTAL_1_MODELNAME_WIDTH = "18%";
    // All Count Column is visible
    public const string CSS_MFPTOTAL_2_SERIALNUMBER_WIDTH = "16%";
    public const string CSS_MFPTOTAL_2_MODELNAME_WIDTH = "12%";
    // 2011.3.22 Update By SES Zhou Miao Ver.1.1 Update ST
    //// COPY Total Name
    //public const string CON_ITEM_COPYTOTAL = "复印使用页数合计";
    //// PRINT Total Name
    //public const string CON_ITEM_PRINTTOTAL = "打印使用页数合计";
    //// SCAN Total Name
    //public const string CON_ITEM_SCANTOTAL = "扫描使用页数合计";
    //// FAX Total Name
    //public const string CON_ITEM_FAXTOTAL = "传真使用页数合计";
    // COPY Total Name
    public const string CON_ITEM_COPYTOTAL = "复印使用面数合计";
    // PRINT Total Name
    public const string CON_ITEM_PRINTTOTAL = "打印使用面数合计";
    // SCAN Total Name
    public const string CON_ITEM_SCANTOTAL = "扫描使用面数合计";
    // FAX Total Name
    public const string CON_ITEM_FAXTOTAL = "传真使用面数合计";
    // 2011.3.22 Update By SES Zhou Miao Ver.1.1 Update ED


    //2010.12.9 Add By SES zhoumiao Ver.1.1 Update ED

    //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
    // NAME OF COLOR
    public const string COLOR_CAN_BORROW = "黑白(可借)";

    //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED
    //2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
    //Available Number Report title
    public const string CON_PAGE_AVAILREPORT = "统计-用户可使用量";
    //total Report title
    public const string CON_PAGE_TOTALREPORT = "统计-总体使用报表";
    //time  period
    public const string CON_TIME_PERIOD = "时间段";
    //MFP SERIAL NUMBER
    public const string CON_TARGET_MFP = "MFP型号（序列号）";
    //2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED
    //2010.12.14 Add By SES zhoumiao Ver.1.1 Update ST
    //user Report title
    public const string CON_PAGE_USERREPORT = "统计-用户使用报表";
    //Group Report title
    public const string CON_PAGE_GROUPREPORT = "统计-用户组使用报表";
    //Section Report title
    public const string CON_PAGE_SECTIONREPORT = "统计-集团使用报表";
    //MFP Report title
    public const string CON_PAGE_MFPREPORT = "统计-MFP使用报表";
    //2010.12.14 Add By SES zhoumiao Ver.1.1 Update ED

    // 2010.12.16 Add By SES Jijianixong Ver.1.1 Update ST
    // 1:Visible
    public const int CON_DISP_TRUE = 1;
    // 0:Unvisible
    public const int CON_DISP_FALSE = 0;
    // 2010.12.16 Add By SES Jijianixong Ver.1.1 Update ED
    // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ST
    // Log View Report Screen
    // LOG page title
    //chen
    //public const string CON_PAGE_LOG = "日志查询 ";
    public const string CON_PAGE_LOG = "使用明细查询 ";
    //Process Time
    public const string CON_ITEM_PROCESSTIME = "操作时间";
    //Job Type
    public const string CON_ITEM_JOBTYPE = "操作类型";
    //Color Mode
    public const string CON_ITEM_COLORMODE = "色彩";

    //2011.3.22 Update By SES zhoumiao Ver.1.1 Update ST
    ////Page Count
    //public const string CON_ITEM_PAGECOUNT = "页数";
    //Page Count
    public const string CON_ITEM_PAGECOUNT = "面数";
    //2011.3.22 Update By SES zhoumiao Ver.1.1 Update ED
    //Status
    public const string CON_ITEM_STATUS = "状态";


    //Status
    //0:Success 
    public const string JOB_STATUS_SUCCESSVALUE = "0";
    public const string JOB_STATUS_SUCCESS = "正常完成";
    //1:Error
    public const string JOB_STATUS_ERRORVALUE = "1";
    public const string JOB_STATUS_ERR = "发生问题";
    //2:All
    public const string JOB_STATUS_ALLVALUE = "2";
    public const string JOB_STATUS_ALL = "所有";

    //Job Type
    //0:All 
    public const string JOB_JOBTYPE_ALLVALUE = "0";
    public const string JOB_JOBTYPE_ALL = "所有";
    //1:Copy 
    public const string JOB_JOBTYPE_COPYVALUE = "1";
    public const string JOB_JOBTYPE_COPY = "复印";
    //2: Print 
    public const string JOB_JOBTYPE_PRINTVALUE = "2";
    public const string JOB_JOBTYPE_PRINT = "打印";
    //3: Scan 
    public const string JOB_JOBTYPE_SCANVALUE = "6";
    public const string JOB_JOBTYPE_SCAN = "扫描";
    //4: Fax
    public const string JOB_JOBTYPE_FAXVALUE = "8";
    public const string JOB_JOBTYPE_FAX = "传真";

    // LOG REPORT SCREEN.
    public const int REPORT_TYPE_LOG = 5;
    // Report LOG
    //chen
    //public const string CON_PAGE_JOBREPORTLOG = "用户日志报表";
    public const string CON_PAGE_JOBREPORTLOG = "使用明细报表";

    public const string MSG_LIMIT_LOG = "仅显示{0}条！";
    // 2010.12.23 Update By SES zhoumiao Ver.1.1 Update ST
    //public const string MSG_SELECT_NOTHING_CSV = "没有选择任何内容，请选择您要CSV导出的纪录。";
    public const string MSG_SELECT_NOTHING_CSV = "没有选择任何内容，请选择您要导出的纪录。";
    // 2010.12.23 Update By SES zhoumiao Ver.1.1 Update ED
    // Log Information Duplex Setting:
    // Unknown
    public const string JOB_UNKNOWN = "-";
    //1: 1SIDED
    public const string JOB_DUPLEX_1SIDED = "单面";
    //2: 2SIDED
    public const string JOB_DUPLEX_2SIDED = "双面";
    //3: 2SIDED_BOOKLET
    public const string JOB_DUPLEX_2SIDED_BOOKLET = "双面（书籍）";
    //4: 2SIDED_TABLET
    public const string JOB_DUPLEX_2SIDED_TABLET = "双面（便签）";
    //5: PAMPHLET
    public const string JOB_DUPLEX_PAMPHLET = "手册样式";

    // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ED
    // 2011.01.05 Add By SES zhoumiao Ver.1.1 Update ST
    public const string CSS_ITEM_WRAP = "Automatic_Wrap";
    // 2011.01.05 Add By SES zhoumiao Ver.1.1 Update ED
    // 2011.01.10 Add By SES zhoumiao Ver.1.1 Update ST
    public const string MSG_NOTHING_SEARCH = "根据指定条件没有查询到相关记录，\\n请修改查询条件重新查询。"; 

    // 2011.01.10 Add By SES zhoumiao Ver.1.1 Update ED




    // add by Zheng Wei 2012.03.14
    // Mfp Management 
    public const string MSG_MAN_DEL_MFPRES = "您的操作已成功，您选择的MFP已删除！";
    public const string MSG_MAN_NOTHING_MFPRES = "没有选择任何MFP，请选择您要设置的MFP。";
    public const string MSG_MAN_DEL_CONFIRM = "是否删除选中的MFP信息？";

    public const string CON_PAGE_MFP = "MFP管理";
    public const string CON_PAGE_Price = "价格管理";
    public const string CON_PAGE_PrintTask = "打印任务管理";
    // MFP Management  page MFP'SN is Exist
    public const string MSG_MFP_SNEXIST = "该MFP序列号已被占用。";

    // Mfp Resinformation
    public const string MSG_UPDATE_ALLOW_MFPRES = "您的操作已成功，您选择的MFP已经设置为可用！";
    public const string MSG_UPDATE_PORFBI_MFPRES = "您的操作已成功，您选择的MFP已经设置为禁用！";
    public const string MSG_UPDATE_NOTHING_MFPRES = "没有选择任何MFP，请选择您要设置的MFP。";
    public const string MSG_MFP_USER_NOTA = "登录失败，您没有权限使用这台机器！";
    public const string CON_PAGE_MFP_RES = "MFP使用授权";

    // MFP Register Limit
    public const string MSG_MFP_NOT_REGISTER = "此MFP没有使用授权，请联系经销商。";

    //add by Wei Changye
    public const string FOLLOWME_SPLIT_SYMBOL = ",";

}