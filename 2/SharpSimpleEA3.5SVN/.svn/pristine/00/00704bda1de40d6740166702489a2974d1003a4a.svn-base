using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.IO;
using dtUserInfoTableAdapters;
using SesMiddleware;
using System.Reflection;
using Model;
using  BLL;
using Model;
using common;

public partial class Settings_ImportUserInfor : ListMainPage
{
    public int ChosenMsgNum=9;//导出的csv文件有11列
   // public int ChosenMsgNum = 12;//导出的csv文件有11列
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_USER_INFO_IMPORT;
        this.Master.SubTitle(UtilConst.CON_PAGE_USER_INFO_IMPORT, "ImportUserInfor.aspx", false);
        this.Master.JobReportTitle();

        //chen add for csv path
       // UtilCommon.InitAppCsv();

    }

    #region UserImport

    protected void btnUploadUser_Click(object sender, EventArgs e)
    {

        //EDIT BY ZHENGWEI 2013.9.12
        //string savepath = ServerPath;
       //string savepath = Assembly.GetAssembly(typeof(SLCRegister.IAlgorithm)).CodeBase;
        //string savepath = Assembly.GetAssembly(typeof(ListMainPage)).CodeBase;
              // Global.Log("savapath1" + savepath);

        string savepath = UtilCommon.AppCsvPath;

        
        string needpath = "";
        string[] str = savepath.Split('/');
        //for (int i = 3; i < str.Length - 1; i++)
        for (int i = 0; i < str.Length - 1; i++)
        {
            needpath += str[i] + '/';
        }

       // string savepath = ServerPath;
            if(fileUploadUser.HasFile)
            {
                string fileName = fileUploadUser.FileName;
               // Global.Log("fileName" + fileName);
                needpath += fileName;
               // Global.Log("needpath+" + needpath);
                fileUploadUser.SaveAs(needpath);
                //Global.Log("保存完成");
                String fileExtension =
                System.IO.Path.GetExtension(fileUploadUser.FileName).ToLower();//获取文件扩展名
               // Global.Log("扩展名正确");
            String[] allowedExtensions = {".csv"}; //允许上传的文件格式
                //Global.Log("文件格式正确");
            List<string> extenstionList = new List<string> (allowedExtensions);
            string path = needpath;
            //Global.Log("needpath" + needpath);


        //String path = Server.MapPath(@"~\App_Data\") + fileUploadUser.FileName + DateTime.Now.ToString("yyMMddHHmmss");


        //if (fileUploadUser.HasFile) //判断是否有文件上来了
        //{
        //    String fileExtension =
        //        System.IO.Path.GetExtension(fileUploadUser.FileName).ToLower();//获取文件扩展名
        //    String[] allowedExtensions = {".csv"}; //允许上传的文件格式
        //    List<string> extenstionList = new List<string>(allowedExtensions);
        //    string path = fileUploadUser.PostedFile.FileName;



            // ADD BY Zhengwei 2013.9.10
            //Global.Log(path);
            //string str = "1";
            //Stream stream = fileUploadUser.FileContent;
            //StreamReader reader = new StreamReader(stream);
            //string strline="2";
            //do
            //{
            //    strline = reader.ReadLine();
            //    str += strline + "/n/r";
            //}
            //while (strline != null);



            //END
            if (extenstionList.Contains(fileExtension))
            {
                try
                {
                    //fileUploadUser.PostedFile.SaveAs(path);
                    CheckUserExistData(path);
                }
                catch (ArgumentException aex)
                {
                    ErrorAlert("您的导入文件存在重复的值，请检查后重新尝试！");
                    DeleteFile(path);
                    Global.Log(aex.ToString());
                }
                catch (UnauthorizedAccessException uaex)
                {
                    ErrorAlert("您没有上传文件的权限，请检查！");
                    Global.Log(uaex.ToString());
                }
                catch (Exception ex)
                {
                    ErrorAlert("导入发生错误，请注意检查格式是否正确！");
                    DeleteFile(path);
                    Global.Log(ex.ToString());
                }
            }
            else
                ErrorAlert("文件格式不正确！");
        }
        else
        {
            ErrorAlert("请选择上传文件！");
        }
    }

    private void CheckUserExistData(String path)
    {
        Session["ImportFilePath"] = path;
        Dictionary<string, string> existDic;
        List<UserInfoModel> newDataList;
        List<UserInfoModel> AllDataList = GetDataFromFile(path, GetAllData("UserInfo"), out existDic, out newDataList);

        if (existDic.Count > 0)
        {
            ErrorAlert("新数据与原数据存在冲突，冲突数据在列表中显示！");
            ShowUserExistData(existDic);
        }
        else
        {
            InsertUserInfo(AllDataList);
        }
    }

    private void ShowUserExistData(Dictionary<string, string> dic)
    {
        ExistDataVisible(true);

        string existUserName = string.Empty;
        string existLoginName = string.Empty;
        foreach (string item in dic.Keys)
        {
            if (existLoginName == string.Empty)
                existLoginName += UtilCommon.ConvertStringToSQL(item);
            else
                existLoginName += "," + UtilCommon.ConvertStringToSQL(item);
        }
        foreach (string item in dic.Values)
        {
            if (existUserName == string.Empty)
                existUserName += UtilCommon.ConvertStringToSQL(item);
            else
                existUserName += "," + UtilCommon.ConvertStringToSQL(item);
        }


        string sql = "SELECT "
         + "UserInfo.ID               AS Id"
         + " ,UserName                  AS UserName"
         + " ,LoginName                 AS LoginName"
         + " ,Password                  AS Password"
         + " ,ICCardID                  AS ICCardID"
         //20160119 add
         + " ,PinCode                  AS PinCode"
         //
         + " ,UserInfo.RestrictionID    AS RestrictionID"
         + " ,RestrictionName           AS RestrictionName"
         + " ,GroupID                   AS GroupID"
         + " ,GroupName                 AS GroupName"
         + " FROM [UserInfo] LEFT JOIN "
         + "  [GroupInfo] ON GroupInfo.ID = GroupID "
         + "                 LEFT JOIN "
         + "  [RestrictionInfo] ON RestrictionInfo.ID = UserInfo.RestrictionID "
         + " WHERE [UserInfo].UserName in ({0}) or [UserInfo].LoginName in ({1}) "
         + " ORDER BY UserInfo.UpdateTime DESC ";

        SqlDataListSource.SelectCommand = string.Format(sql, existUserName,existLoginName);
        SqlDataListSource.ConnectionString = UtilCommon.DBConnectionStrings;

        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;
        this.CustomersGridView.DataBind();

    }

    private void InsertUserInfo(List<UserInfoModel> list)
    {
        CustomersGridView.DataSource = null;
        string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
        using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
        {
            con.Open();
            try
            {
                foreach (UserInfoModel item in list)
                {

                    UserInfoTableAdapter UserInfoAdapter = new UserInfoTableAdapter();
                    Int64 UserId = Convert.ToInt64(UserInfoAdapter.GetMaxId()) + 1;
                    if (UserId > 32767 && UtilCommon.GetUsableID() != -1)
                        UserId = UtilCommon.GetUsableID();

                    //chen add 20140504 st
                    int groupID = 0;

                    dtGroupInfoTableAdapters.GroupInfoTableAdapter groupAdapter = new dtGroupInfoTableAdapters.GroupInfoTableAdapter();
                    dtGroupInfo.GroupInfoDataTable groupDT = groupAdapter.GetDataByGroupName(item.GroupName);
                    if (groupDT.Count != 0)
                    {
                        dtGroupInfo.GroupInfoRow groupRow = groupDT[0];
                        groupID = groupRow.ID;
                        
                    }

                    int restrictID = -1;
                    //decimal remainMoney = 0;
                    //decimal remainColorMoney = 0;
                    if (!UtilConst.CON_PEIE_JICHENG_GROUP.Equals(item.RestrictionName.Trim()))
                    {
                        //dtGroupInfoTableAdapters.GroupInfoTableAdapter groupAdapter = new dtGroupInfoTableAdapters.GroupInfoTableAdapter();
                        //dtGroupInfo.GroupInfoDataTable groupDT = groupAdapter.GetDataByGroupName(item.GroupName);
                        //if (groupDT.Count != 0)
                        //{
                        //    dtGroupInfo.GroupInfoRow groupRow = groupDT[0];
                        //    groupID = groupRow.ID;
                        //    if (groupRow.RestrictionID != null)
                        //    {
                        //        restrictID = int.Parse(groupRow.RestrictionID);
                        //    }
                        //}
                        //20180308
                        BLLRestrictionInfo bll = new BLLRestrictionInfo();
                        restrictID = bll.GetIDByName(item.RestrictionName);
                    }
                    //dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter resAdapter = new dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter();
                    //dtRestrictionInfo.RestrictionInfoDataTable dt = resAdapter.GetRestrictionInfoDataByID(restrictID);
                    //if (dt.Count != 0)
                    //{
                    //    dtRestrictionInfo.RestrictionInfoRow resRow = dt[0];
                    //    remainMoney = resRow.AllQuota;
                    //    remainColorMoney = resRow.ColorQuota;
                    //}

                    //chen add 20140504 ed

                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO UserInfo");
                    sb.Append("           ([ID]");
                    sb.Append("           ,[UserName]");
                    sb.Append("           ,[LoginName]");
                    sb.Append("           ,[Password]");
                    sb.Append("           ,[ICCardID]");
                    //chen add 20160119 st
                    sb.Append("           ,[PinCode]");
                    //chen add 20160119 ed
                    //chen add 20140504 st
                    sb.Append("           ,[Email]");
                    //chen add 20140504 ed
                    sb.Append("           ,[GroupID]");
                    sb.Append("           ,[RestrictionID]");
                    sb.Append("           ,[ComeFrom]");
                    sb.Append("           ,[CreateTime]");
                    sb.Append("           ,[UpdateTime])");
                    sb.Append("            VALUES") ;
                    sb.Append("           ({0}");
                    sb.Append("           ,{1}");
                    sb.Append("           ,{2}");
                    sb.Append("           ,{3}");
                    sb.Append("           ,{4}");
                    //chen add 20140504 st
                    //sb.Append("           ,ISNULL((SELECT ID FROM GroupInfo where GroupName={5}) , 0)");
                    sb.Append("           ,{5}");
                    sb.Append("           ,{6}");
                    //chen add 20140504 ed
                    sb.Append("           ,{7}");
                    sb.Append("           ,{8}");
                    sb.Append("           ,{9}");
                    sb.Append("           ,GetDate()");
                    sb.Append("           ,GetDate())");

                    string sql = string.Format(sb.ToString(), UserId, 
                        UtilCommon.ConvertStringToSQL(item.UserName),
                        UtilCommon.ConvertStringToSQL(item.LoginName), 
                        UtilCommon.ConvertStringToSQL(item.Password),
                        UtilCommon.ConvertStringToSQL(item.ICCardID),
                        UtilCommon.ConvertStringToSQL(item.PinCode),
                        UtilCommon.ConvertStringToSQL(item.Email),
                        UtilCommon.ConvertStringToSQL(groupID.ToString()),
                        UtilCommon.ConvertStringToSQL(restrictID.ToString()),
                        UtilCommon.ConvertStringToSQL(item.ComeFrom.ToString())
                        );
                    //using (SqlCommand cmd = new SqlCommand(string.Format(sb.ToString(), UserId, UtilCommon.ConvertStringToSQL(item.UserName),
                    //    UtilCommon.ConvertStringToSQL(item.LoginName), UtilCommon.ConvertStringToSQL(item.Password)
                    //    , UtilCommon.ConvertStringToSQL(item.ICCardID), UtilCommon.ConvertStringToSQL(item.GroupName)
                    //    ,UtilCommon.ConvertStringToSQL( null)), con))
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    if (item.ICCardID != null)
                    {
                        //XML
                        if (ICCardData.CheckUser(item.LoginName, serverPath))
                            ICCardData.DeleteICCardInfoByUserName(item.LoginName, serverPath);
                        if (ICCardData.CheckCardID(item.ICCardID, serverPath))
                            ICCardData.DeleteICCardInfoByCardID(item.ICCardID, serverPath);

                        //add new
                        ICCardData.AddICCardInfo(item.ICCardID, item.LoginName, item.Password, serverPath);
                    }
                }
                DeleteFile(Session["ImportFilePath"].ToString());
                SuccessMessage("数据已成功导入！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
    }

    private void UpdateUserInfo(List<UserInfoModel> allDataList, Dictionary<string, string> dic)
    {
        string existUserName = string.Empty;
        string existLoginName = string.Empty;
        foreach (string item in dic.Keys)
        {
            if (existLoginName == string.Empty)
                existLoginName += UtilCommon.ConvertStringToSQL(item);
            else
                existLoginName += "," + UtilCommon.ConvertStringToSQL(item);
        }
        foreach (string item in dic.Values)
        {
            if (existUserName == string.Empty)
                existUserName += UtilCommon.ConvertStringToSQL(item);
            else
                existUserName += "," + UtilCommon.ConvertStringToSQL(item);
        }

        CustomersGridView.DataSource = null;
        using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
        {
            con.Open();
            //SqlTransaction tran = con.BeginTransaction();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Delete from [UserInfo] ");
                sb.Append("        where [LoginName] in ({0})");
                sb.Append("           or [UserName] in ({1})");

                using (SqlCommand cmd = new SqlCommand(string.Format(sb.ToString(), existLoginName, existUserName), con))
                {
                    cmd.ExecuteNonQuery();
                }

                InsertUserInfo(allDataList);

                //tran.Commit();
            }
            catch (Exception ex)
            {
                //if (tran.Connection != null)
                //{
                //    tran.Rollback();
                //}
                throw ex;
            }
            finally
            {
                //tran.Dispose();
                //tran = null;
            }
        }
    }

    protected Boolean KeyCheck(DataTable dt, string strColumnName, string name)
    {
        string strSql = strColumnName + "='" + name + "'";
        DataRow[] row = dt.Select(strSql);
        if (row == null || row.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void btnOverride_OnClick(object sender, EventArgs e)
    {
        ExistDataVisible(false);
        Dictionary<string ,string> existDic =new Dictionary<string,string> ();
        List<UserInfoModel> newDataTable = new List<UserInfoModel>();
        List<UserInfoModel> allDataTable = GetDataFromFile(Session["ImportFilePath"].ToString(), GetAllData("UserInfo"), out existDic, out newDataTable);

        UpdateUserInfo(allDataTable, existDic);
    }

    protected void btnIgnore_OnClick(object sender, EventArgs e)
    {
        ExistDataVisible(false);
        Dictionary<string, string> existDic = new Dictionary<string, string>();
        List<UserInfoModel> newDataList = new List<UserInfoModel>();
        GetDataFromFile(Session["ImportFilePath"].ToString(), GetAllData("UserInfo"), out existDic, out newDataList);

        InsertUserInfo(newDataList);
    }

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        DeleteFile(Session["ImportFilePath"].ToString());
        Response.Redirect("ImportUserInfor.aspx", false);
    }

    private DataTable GetAllData(string tableName)
    {
        String strSql = "SELECT *"
                        + "  FROM  {0}";
        return UtilCommon.ExecuteDataTable(string.Format(strSql, tableName));
    }

    private List<UserInfoModel> GetDataFromFile(string path, DataTable existTable, out Dictionary<string, string> exsitDic, out List<UserInfoModel> newList)
    {
        using (StreamReader streamReader = new StreamReader(path, Encoding.Default, false))
        {
            string strline;
            exsitDic = new Dictionary<string, string>();
            List<UserInfoModel> allDataList = new List<UserInfoModel>();
            newList = new List<UserInfoModel>();

            Dictionary<string, string> isUserNameRepeatDic = new Dictionary<string, string>();
            Dictionary<string, string> isLoginNameRepeatDic = new Dictionary<string, string>();
            strline = streamReader.ReadLine();
            if (strline == null)
            {
                return allDataList;
            }
            while ((strline = streamReader.ReadLine()) != null)
            {
                string[] array = strline.Split(new char[] { ',' });
                UserInfoModel row = new UserInfoModel();
                row.UserName = array[0];
                row.LoginName = array[1];
                row.Password = array[2];
                row.ICCardID = array[3] == "" ? null : array[3];
                row.PinCode = array[4] == "" ? null : array[4];
                row.Email = array[5] == "" ? null : array[5];
                row.GroupName = array[6];
                row.strRestrictionName = array[7] == "" ? null : array[7];
                row.ComeFrom = array[8] == "" ? null : array[8];
                //20160119 end
                //chen add 20140504 end
                if (row.UserName.Equals("UserName"))
                    continue;
                else
                    if (KeyCheck(existTable, "UserName", row.UserName) || KeyCheck(existTable, "LoginName", row.LoginName))
                    {
                        //exsit
                        exsitDic.Add(row.LoginName, row.UserName);
                    }
                    else
                    {
                        // new
                        newList.Add(row);
                    }
                //all
                allDataList.Add(row);

                // file repeat check
                isUserNameRepeatDic.Add(row.UserName, "");
                isLoginNameRepeatDic.Add(row.LoginName, "");

                row = null;
            }
            return allDataList;
        }
    }

    private void ExistDataVisible(bool flag)
    {
        gridTable.Visible = flag;
        btnCancel.Visible = flag;
        btnIgnore.Visible = flag;
        btnOverride.Visible = flag;
    }

    private void DeleteFile(string path)
    {
        //if (File.Exists(path))
        //    File.Delete(path);
    }

    #endregion

    //下载函数
    public static void DownloadFile(HttpResponse argResp, StringBuilder argFileStream, string strFileName)
    {
        try
        {
            string strResHeader = "attachment; filename=" + Guid.NewGuid().ToString() + ".csv";
            if (!string.IsNullOrEmpty(strFileName))
            {
                strResHeader = "inline; filename=" + strFileName;
            }
            argResp.AppendHeader("Content-Disposition", strResHeader);//attachment说明以附件下载，inline说明在线打开
            argResp.ContentType = "application/ms-excel";
            argResp.ContentEncoding = Encoding.GetEncoding("GB2312"); // Encoding.UTF8;//
            argResp.Write(argFileStream);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private StringBuilder AppendCSVFields(StringBuilder argSource, string argFields)
    {
        return argSource.Append(argFields.Replace(",", " ").Trim()).Append(",");
    }

    public DataTable msg(List<UserInfoModel> boi)
    {
        Dictionary<int, List<UserInfoModel>> myselfList1;
        int boinum;
        boinum = boi.Count;//获取表格的行数
        //获取列名
        string[] titlelist;//将获取的列名保存在该数组中
        //  public int ID;
        //public string UserName;
        //public string LoginName;
        //public string Password;
        //public string ICCardID;
        //public string PinCode ;
        //public string Email;
        //public int GroupID;
        //public int RestrictionID;
        //public DateTime CreateTime = DateTime.Now;
        //public DateTime UpdateTime = DateTime.Now;
        string titleString = "UserName,LoginName,Password,ICCardID,PinCode,Email,GroupName,RestrictionName,ComeFrom";
        //for (int i = 0; i < ChosenMsgNum - 1; i++)
        //{
        titlelist = (titleString).Split(',');
        //}
        if (boi.Count > 0)
        {
            myselfList1 = new Dictionary<int, List<UserInfoModel>>();
            List<UserInfoModel> lst = new List<UserInfoModel>();
            BllICCard bllICCard = new BllICCard();
            lst = bllICCard.GetUserEntry();
            DataTable dt = new DataTable();
            for (int i = 0; i < ChosenMsgNum; i++)
            {
                DataColumn newcolumn = new DataColumn(i.ToString(), typeof(string));
                dt.Columns.Add(newcolumn);
            }
            //把表头加到datatable中
            DataRow row1 = dt.NewRow();
            for (int i = 0; i < ChosenMsgNum ; i++)
            {
                row1[i] = titlelist[i];
            }
            //row1[0] = titlelist[0];
            //row1[1] = titlelist[1];
            dt.Rows.Add(row1);
            foreach (UserInfoModel drop in lst)
            {                
                DataRow row = dt.NewRow();
                //ID,UserName,LoginName,Password,ICCardID,PinCode,Email,GroupID,RestrictionID,ComFrom,CreateTime,UpdateTime               
                row[0] = drop.UserName;
                row[1] = drop.LoginName;
                row[2] = drop.Password;
                row[3] = drop.ICCardID;
                row[4] = drop.PinCode;
                row[5] = drop.Email;
                row[6] = drop.GroupName;
                row[7] = drop.RestrictionName;
                row[8] = drop.ComeFrom;
                dt.Rows.Add(row);
            }
            return dt;
        }
     
        return null;
    }

    protected void btn_downloadUserInfo_Click(object sender, EventArgs e)
    {
        List<UserInfoModel> lst = new List<UserInfoModel>();
        BllICCard bllICCard = new BllICCard();

        lst = bllICCard.GetUserEntry();
       

        DataTable dt = msg(lst);

        if (dt != null)
        {
            try
            {
                StringWriter swCSV = new StringWriter();

                //遍历datatable导出数据
                foreach (DataRow drTemp in dt.Rows)
                {
                    StringBuilder sbText = new StringBuilder();
                    for (int i = 0; i < ChosenMsgNum; i++)
                    {
                        sbText = AppendCSVFields(sbText, drTemp[i].ToString());
                    }
                    if (!sbText.Equals("")) {
                        //去掉尾部的逗号
                        sbText.Remove(sbText.Length - 1, 1);
                        //写datatable的一行
                        swCSV.WriteLine(sbText.ToString());
                    }
                   
                }
                //下载文件
                DownloadFile(Response, swCSV.GetStringBuilder(), "SampleUserInfo.csv");

                swCSV.Close();
                Response.End();

            }
            catch (Exception ex)
            {
                //throw ex;
                Response.End();
            }
        }
        else
        {
            ErrorAlert("UnserInfo导出失败！");
        }

    
    }
   
    #region UserICCardImport
    protected void btnUploadUserICCard_Click(object sender, EventArgs e)
    {
        BllICCard bllICCard = new BllICCard();
        //UserModel[] beanitem;
        List<UserModel> beanitems = new List<Model.UserModel>();
        string uploadName = this.fileUploadUser.FileName;
        string CsvName = "";
        string path = "";
        try
        {
            if (fileUploadUser.HasFile)
            {
                string Extension = System.IO.Path.GetExtension(this.fileUploadUser.FileName).ToString().ToLower();
                if (Extension != ".csv")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "<script>alert('您导入的文件格式不正确，请确认后重试！');</script>");
                    return;
                }
                CsvName = uploadName;
                path = Request.PhysicalApplicationPath.ToString();
                path += "Img\\";
                path += uploadName;

                //将文件保存到服务器上OK
                fileUploadUser.SaveAs(path);
                ImportData1 ExcelData = new ImportData1();
                //读取cvs里面的数据
                beanitems = ExcelData.ImportCSV(path);
                //把读取的csv的数据放在数据库表UserInfo中（LoginName和ICCardID）
                if (beanitems == null)
                {
                    ErrorAlert("导入失败！");
                }
                int ret = bllICCard.ImportData(beanitems);

                if (ret == 1)
                {
                    ErrorAlert("导入成功！");
                    if (System.IO.File.Exists(path))//先判断文件是否存在，再执行操作
                    {
                        System.IO.File.Delete(path);
                    }
                }
                else
                {
                    ErrorAlert("导入失败！");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    #region download ICCard
    protected void btn_downloadICCard_Click(object sender, EventArgs e)
    {
        List<UserEntry> lst = new List<UserEntry>();
        BllICCard bllICCard = new BllICCard();

        lst = bllICCard.GetUserEntryByLoginName();


        DataTable dt = msg2(lst);

        int chooseMsgNum = 2;
        if (dt != null)
        {
            try
            {
                StringWriter swCSV = new StringWriter();

                //遍历datatable导出数据
                foreach (DataRow drTemp in dt.Rows)
                {
                    StringBuilder sbText = new StringBuilder();
                    for (int i = 0; i < chooseMsgNum; i++)
                    {
                        sbText = AppendCSVFields(sbText, drTemp[i].ToString());
                    }
                    if (!sbText.Equals(""))
                    {
                        //去掉尾部的逗号
                        sbText.Remove(sbText.Length - 1, 1);
                        //写datatable的一行
                        swCSV.WriteLine(sbText.ToString());
                    }

                }
                //下载文件
                DownloadFile(Response, swCSV.GetStringBuilder(), "SampleUserICCard.csv");

                swCSV.Close();
                Response.End();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            ErrorAlert("ICCard导入失败！");
        }

    }

    public DataTable msg2(List<UserEntry> boi)
    {
        Dictionary<int, List<UserEntry>> myselfList1;
        int boinum;
        boinum = boi.Count;//获取表格的行数
        //获取列名
        string[] titlelist;//将获取的列名保存在该数组中
        string titleString = "LoginName,ICCardID,";
        titlelist = (titleString).Split(',');
        if (boi.Count > 0)
        {
            myselfList1 = new Dictionary<int, List<UserEntry>>();
            List<UserEntry> lst = new List<UserEntry>();
            BllICCard bllICCard = new BllICCard();
            lst = bllICCard.GetUserEntryByLoginName();
            DataTable dt = new DataTable();
            for (int i = 0; i < 2; i++)
            {
                DataColumn newcolumn = new DataColumn(i.ToString(), typeof(string));
                dt.Columns.Add(newcolumn);
            }
            //把表头加到datatable中
            DataRow row1 = dt.NewRow();
            row1[0] = titlelist[0];
            row1[1] = titlelist[1];
            dt.Rows.Add(row1);
            foreach (UserEntry drop in lst)
            {
                DataRow row = dt.NewRow();
                row[0] = drop.LoginName;
                row[1] = drop.ICCardID; ;
                dt.Rows.Add(row);
            }
            return dt;
        }

        return null;
    }
    #endregion
}