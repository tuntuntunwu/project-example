using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.IO;

public partial class Report_ViewPrintContentResult : System.Web.UI.Page
{
    private string MFPPrintTaskID;
    private dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (InitialPara(Request.Params) && CheckAuth())
            ReturnFile();
        else
            Response.Write("参数错误！");
        Response.End();
    }

    private bool InitialPara(NameValueCollection collection)
    {
        MFPPrintTaskID = collection["MFPPrintTaskID"] == null ? "" : collection["MFPPrintTaskID"].ToString();

        if (MFPPrintTaskID == null || MFPPrintTaskID == "")
            return false;
        else
            return true;
    }

    private bool CheckAuth()
    {
        return true;
    }

    private void ReturnFile()
    {
        dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(Convert.ToInt32(MFPPrintTaskID));

        if (table != null && table.Count > 0)
        {
            string FilePath = table[0].FileLocation + table[0].DiskFileName;
            FileInfo file = new FileInfo(FilePath);//用于获得文件信息
            Response.Clear();//清空输出
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
            string fileName = table[0].DiskFileName;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileName));
            // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
            Response.AddHeader("Content-Length", file.Length.ToString());

            // 指定返回的是一个不能被客户端读取的流，必须被下载 
            Response.ContentType = "application/ms-txt";
            Response.Flush();

            // 把文件流发送到客户端 
            Response.WriteFile(file.FullName);
        }
        else
            Response.Write("无此文件或文件已被删除！");
    }
}