using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


/// <summary>
/// this page is for OSA Direct Print（OSA API Print） Http download
/// not use at present
/// </summary>
public partial class MFPScreen_PrintFileDownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (null != Request.QueryString["download"])
        {
            string filePath = Request.QueryString["filePath"].ToString();
            filestream(filePath);
            Response.End();
        }
    }

    #region getfilestream
    /// <summary>
    /// file transfer to the client by streaming the file and using HTTP file transfer.
    /// </summary>
    public void filestream(string downloadFile)
    {
        bool IsFileExists = File.Exists(downloadFile);
        if (IsFileExists)
        {
            Global.Log("file exists check past.");
            string fileName= Path.GetFileName(downloadFile);
            System.IO.Stream iStream = null;
            byte[] buffer = new Byte[2048];
            int length;
            long dataToRead;

            try
            {
                iStream = new System.IO.FileStream(downloadFile, System.IO.FileMode.Open,
                            System.IO.FileAccess.Read, System.IO.FileShare.Read);
                dataToRead = iStream.Length;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

                while (dataToRead > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 2048);
                        Response.OutputStream.Write(buffer, 0, length);
                        Response.Flush();

                        buffer = new Byte[2048];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        dataToRead = -1;
                    }
                }
                Global.Log("Response End.");
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message);
                Global.Log("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    iStream.Close();
                }
            }
        }
    }

    #endregion
}