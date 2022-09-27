using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Net;
using dtMFPPrintTaskTableAdapters;
using DAL;
using Model;
using common;
/// <summary>
///FtpSend 的摘要说明
/// </summary>
public class FtpSend : System.Web.UI.Page
{

    MFPPrintTaskTableAdapter adpter;

    public FtpSend()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    private string GetFilePath(string key)
    {
        if (adpter == null)
            adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
        dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(Convert.ToInt32(key));

        string pathfile = table[0].FileLocation + table[0].DiskFileName;
        return pathfile;
    }


    // Del by Zhengwei 2013.5.20
   // #region getFileName
    /// <summary>
    /// getFileName
    /// </summary>
    /// <returns></returns>
    //string getFileName(string key)
    //{
    //    if (adpter == null)
    //        adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
    //    dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(Convert.ToInt32(key));

    //    return table[0].PrintFileName;
    //}
    //#endregion
    //End
    public void UploadFile(object o)
    {

        try
        {
            IpAndKeyModel model = (IpAndKeyModel)o;

            //2018-01-04 
            //此处修改代印留底
            string IP = model.IP;
            DalMFP mfp = new DalMFP();
            MFPEntry bean = mfp.GetInfoByIP(IP);
            int monitor = bean.Monitor;
            int printMonitor = monitor % 10;
            int copyMonitor = monitor / 10 % 10;
            int bw = monitor / 100;

            //if (bean.Monitor == 1 || bean.Monitor == 3)
            if (printMonitor == 1)
            {
                string filepath = GetFilePath(model.Key);
                SPLHandler splHandler = new SPLHandler();
                splHandler.changeSplFile(filepath);
            }
            //判断该打印机是否留底

            //using (FileStream fs = new FileStream(GetFilePath(model.Key), FileMode.Open, FileAccess.Read))
            using (FileStream fs = new FileStream(GetFilePath(model.Key), FileMode.Open, FileAccess.Read))
            {
                long length = fs.Length;
                //Edit by Zhengwei, 2013.5.20
                //FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://" + model.IP + "/" + getFileName(model.Key));
                Guid gdName = Guid.NewGuid();
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://" + model.IP + "/" + gdName.ToString());
                // End
              
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.UseBinary = true;
                req.ContentLength = length;
                req.Timeout = 10 * 1000;
                Stream stream = req.GetRequestStream();
                int BufferLength = 2048;
                byte[] b = new byte[BufferLength];
                int i;
                while ((i = fs.Read(b, 0, BufferLength)) > 0)
                {
                    stream.Write(b, 0, i);
                }
                stream.Close();
                stream.Dispose();
            }
        }
        catch (Exception ex)
        {
            Global.Log(ex.ToString());
        }

    }

}