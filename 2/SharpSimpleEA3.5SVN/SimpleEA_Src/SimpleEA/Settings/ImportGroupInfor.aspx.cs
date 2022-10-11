using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;

/// <summary>
/// 插入数据前先进行主键检查，列出相同主键的记录，并为用户提供“覆盖”，“不覆盖”，“取消导入”功能
/// </summary>
public partial class Settings_ImportGroupInfor : ListMainPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_GROUP_INFO_IMPORT;
        this.Master.SubTitle(UtilConst.CON_PAGE_GROUP_INFO_IMPORT, "ImportGroupInfor.aspx", false);
        this.Master.JobReportTitle();

        //chen add for csv path
        UtilCommon.InitAppCsv();

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //ADD BY Zhengwei 2013.9.12
        //string savepath = Assembly.GetAssembly(typeof(ListMainPage)).CodeBase;
        string savepath = UtilCommon.AppCsvPath;


        string needpath = "";
        string[] str = savepath.Split('/');
        //for (int i = 3; i < str.Length - 1; i++)
        for (int i = 0; i < str.Length - 1; i++)
        {
            needpath += str[i] + '/';
        }
        //string savepath = ServerPath;
        if(fileUploadGroup.HasFile)
        {
            string fileName = fileUploadGroup.FileName;
            needpath+= fileName;
            fileUploadGroup.SaveAs(needpath);
             String fileExtension =
                System.IO.Path.GetExtension(fileUploadGroup.FileName).ToLower();//获取文件扩展名
            String[] allowedExtensions = { ".csv" }; //允许上传的文件格式
            List<string> extenstionList = new List<string>(allowedExtensions);
            string path = needpath;
            // END
        //String path = Server.MapPath(@"~\App_Data\") + fileUploadGroup.FileName + DateTime.Now.ToString("yyMMddHHmmss");
        //if (fileUploadGroup.HasFile) //判断是否有文件上来了
        //{
        //    String fileExtension =
        //        System.IO.Path.GetExtension(fileUploadGroup.FileName).ToLower();//获取文件扩展名
        //    String[] allowedExtensions = { ".csv" }; //允许上传的文件格式
        //    List<string> extenstionList = new List<string>(allowedExtensions);
        //    string path = fileUploadGroup.PostedFile.FileName;

            if (extenstionList.Contains(fileExtension))
            {
                try
                {
                    //fileUploadGroup.PostedFile.SaveAs(path);
                    CheckGroupInfor(path);
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
                    Global.Log(ex.ToString());
                    DeleteFile(path);
                    ErrorAlert("导入发生错误，请注意检查格式是否正确！");
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

    private void CheckGroupInfor(string path)
    {
        Session["ImportFilePath"] = path;
        Dictionary<string, string> existDic;
        List<dtGroupInfo.GroupInfoRow> newDataList;
        List<dtGroupInfo.GroupInfoRow> AllDataList = GetDataFromFile(path, GetAllData("GroupInfo"), out existDic, out newDataList);

        if (existDic.Count > 0)
        {
            ErrorAlert("新数据与原数据存在冲突，冲突数据在列表中显示！");
            ShowExistData(existDic);
        }
        else
        {
            InsertInfo(AllDataList);
        }
    }

    private void ShowExistData(Dictionary<string, string> dic)
    {
        ExistDataVisible(true);

        string selectKey = string.Empty;
        foreach (string item in dic.Keys)
        {
            if (selectKey == string.Empty)
                selectKey += UtilCommon.ConvertStringToSQL(item);
            else
                selectKey += "," + UtilCommon.ConvertStringToSQL(item);
        }


        string sql = "   SELECT A.GroupName AS GroupName    " + Environment.NewLine +
                      "         ,A.ID AS Id                         " + Environment.NewLine +
                      "         ,A.CreateTime AS CreateTime         " + Environment.NewLine +
                      "         ,ISNULL( (SELECT COUNT(U.GroupID)   " + Environment.NewLine +
                      "                     FROM [UserInfo] U       " + Environment.NewLine +
                      "                    WHERE U.GroupID = A.ID   " + Environment.NewLine +
                      "                   ) ,0)  AS UserCount       " + Environment.NewLine +
                      "       ,R.RestrictionName  AS RestrictionName" + Environment.NewLine +
                      "    FROM  [GroupInfo] A                      " + Environment.NewLine +
                      "LEFT JOIN [RestrictionInfo] R ON             " + Environment.NewLine +
                      "             R.ID = A.RestrictionID          " + Environment.NewLine +
                      "   WHERE  A.GroupName in ({0})                        " + Environment.NewLine +
                      " ORDER BY A.UpdateTime DESC                  " + Environment.NewLine;

        SqlDataListSource.SelectCommand = string.Format(sql, selectKey);
        SqlDataListSource.ConnectionString = UtilCommon.DBConnectionStrings;

        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;
        this.CustomersGridView.DataBind();

    }

    private void InsertInfo(List<dtGroupInfo.GroupInfoRow> lst)
    {
        CustomersGridView.DataSource = null;
        using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                foreach (dtGroupInfo.GroupInfoRow item in lst)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO GroupInfo");
                    sb.Append("           ([ID]");
                    sb.Append("           ,[GroupName]");
                    sb.Append("           ,[RestrictionID]");
                    sb.Append("           ,[CreateTime]");
                    sb.Append("           ,[UpdateTime])");
                    sb.Append("     SELECT (ISNULL((SELECT MAX(ID) FROM [GroupInfo]) , 0) + 1)");
                    sb.Append("           ,{0}");
                    sb.Append("           ,ISNULL((SELECT ID FROM [SimpleEA].[dbo].[RestrictionInfo] where RestrictionInfo .RestrictionName={1}) , 0)");
                    sb.Append("           ,GetDate()");
                    sb.Append("           ,GetDate();");

                    using (SqlCommand cmd = new SqlCommand(string.Format(sb.ToString(), UtilCommon.ConvertStringToSQL(item.GroupName), UtilCommon.ConvertStringToSQL(item.RestrictionID)), con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                }

                tran.Commit();
                DeleteFile(Session["ImportFilePath"].ToString());
                SuccessMessage("数据已成功导入！");
            }
            catch (Exception ex)
            {
                if (tran.Connection != null)
                {
                    tran.Rollback();
                }
                throw ex;
            }
            finally
            {
                tran.Dispose();
                tran = null;
            }
        }
    }

    private void UpdateGroupInfo(List<dtGroupInfo.GroupInfoRow> lst,Dictionary<string, string> dic)
    {
        string existGroupName = string.Empty;
        foreach (string item in dic.Keys)
        {
            if (existGroupName == string.Empty)
                existGroupName += UtilCommon.ConvertStringToSQL(item);
            else
                existGroupName += "," + UtilCommon.ConvertStringToSQL(item);
        }

        CustomersGridView.DataSource = null;
        using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
        {
            con.Open();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Delete from [GroupInfo] ");
                sb.Append("        where [GroupName] in ({0})");

                using (SqlCommand cmd = new SqlCommand(string.Format(sb.ToString(), existGroupName), con)) 
                {
                    cmd.ExecuteNonQuery();
                }

                InsertInfo(lst);
            }
            catch (Exception ex)
            {
                throw ex;
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
        Dictionary<string, string> existDic = new Dictionary<string, string>();
        List<dtGroupInfo.GroupInfoRow> newDataTable = new List<dtGroupInfo.GroupInfoRow>();
        List<dtGroupInfo.GroupInfoRow> allDataTable = GetDataFromFile(Session["ImportFilePath"].ToString(), GetAllData("GroupInfo"), out existDic, out newDataTable);

        UpdateGroupInfo(allDataTable, existDic);
    }

    protected void btnIgnore_OnClick(object sender, EventArgs e)
    {
        ExistDataVisible(false);
        Dictionary<string, string> existDic = new Dictionary<string, string>();
        List<dtGroupInfo.GroupInfoRow> newDataList = new List<dtGroupInfo.GroupInfoRow>();
        GetDataFromFile(Session["ImportFilePath"].ToString(), GetAllData("GroupInfo"), out existDic, out newDataList);

        InsertInfo(newDataList);
    }

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        DeleteFile(Session["ImportFilePath"].ToString());
        Response.Redirect("ImportGroupInfor.aspx", false);
    }

    private List<dtGroupInfo.GroupInfoRow> GetDataFromFile(string path, DataTable existTable, out Dictionary<string, string> exsitDic, out List<dtGroupInfo.GroupInfoRow> newList)
    {
        using (StreamReader streamReader = new StreamReader(path, Encoding.Default, false))
        {
            string strline;
            exsitDic = new Dictionary<string, string>();
            newList = new List<dtGroupInfo.GroupInfoRow>();
            dtGroupInfo.GroupInfoDataTable table = new dtGroupInfo.GroupInfoDataTable();
            

            List<dtGroupInfo.GroupInfoRow> allDataList = new List<dtGroupInfo.GroupInfoRow>();
            bool isGroupImport = false;
            Dictionary<string, string> isRepeatDic = new Dictionary<string, string>();

            while ((strline = streamReader.ReadLine()) != null)
            {
                string[] array = strline.Split(new char[] { ',' });
                dtGroupInfo.GroupInfoRow row = table.NewGroupInfoRow();
                row.GroupName = array[0];
                row.RestrictionID = array[1];

                if (array[0].Equals("GroupName"))
                {
                    isGroupImport = true;
                    continue;
                }
                else
                    if (!isGroupImport)
                        throw new Exception();
                    else
                        if (KeyCheck(existTable, "GroupName", array[0]))
                        {
                            //exsit
                            exsitDic.Add(row.GroupName, row.RestrictionID);
                        }
                        else
                        {
                            // new
                            newList.Add(row);
                        }
                //all
                allDataList.Add(row);
                // check if the file has repeat record
                // if repeat will throw an exception
                isRepeatDic.Add(row.GroupName, row.RestrictionID); 
            }
            return allDataList;
        }
    }

    private DataTable GetAllData(string tableName)
    {
        String strSql = "SELECT *"
                        + "  FROM  {0}";
        return UtilCommon.ExecuteDataTable(string.Format(strSql, tableName));
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
}