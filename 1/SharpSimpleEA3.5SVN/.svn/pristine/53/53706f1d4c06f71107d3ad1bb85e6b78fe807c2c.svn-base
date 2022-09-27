using System;
using System.Collections.Generic;
using System.Web;
using Osa.MfpWebService;
using System.Windows.Forms;
using System.IO;
using System.Web.Services.Protocols;

/// <summary>
///MFPPrintDirect 通过OSA的WEB API进行打印
/// </summary>
public class MFPPrintDirect : System.Web.UI.Page
{
    // webservice variable
    private MFPCoreWS _webService;
    dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter;

	public MFPPrintDirect()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    #region CreateWS
    /// <summary>
    /// CreateWS
    /// </summary>
    /// <param name="sMFPIP"></param>
    /// <returns></returns>
    private bool CreateWS(string sMFPIP)
    {
        bool Ret = true;
        try
        {
            if (_webService == null)
            {
                _webService = Global.CreateWS(sMFPIP);

            }
        }
        catch (Exception)
        {
            Ret = false;
        }

        return Ret;
    }

    #endregion

    #region RunWSCalls
    /// <summary>
    /// RunWSCalls
    /// </summary>
    /// <returns></returns>
    public bool RunWSCalls(List<string> lst)
    {
        bool Ret = true;
        OSA_JOB_ID_TYPE tempJID = null;
        try
        {
            //subscribe data , monitor event				
            ACCESS_POINT_TYPE aType = new ACCESS_POINT_TYPE();
            aType.URLType = E_ADDRESSPOINT_TYPE.SOAP;
            string sLocalAddrs = Session["LOCAL_ADDR"].ToString();
            string sRequestCurAppPath = Session["ApplicationPath"].ToString();
            if (String.Compare(UtilCommon.UseSSL, "true", true) == 0)
            {
                aType.Value = @"https://";
            }
            else
            {
                aType.Value = @"http://";
            }
            aType.Value = aType.Value + @sLocalAddrs + @sRequestCurAppPath + @"/MFPEventsCapture.asmx";

            object str = Session["REMOTE_ADDR"];
            //1. create web servers
            Ret = CreateWS(Session["REMOTE_ADDR"].ToString());


            //if create webServer success
            if (Ret)
            {
                foreach (string item in lst)
                {
                    // create a new printing job
                    tempJID = (OSA_JOB_ID_TYPE)CreateJob(_webService);

                    

                    //2. data to be set up for SetDeviceElement()
                    // set up all print parameters obtained from GetSettableElements()dynamically
                    SetJobPara(_webService, tempJID, item);
                    //3. subscribe for ON_HKEY_PRESSED event
                    _webService.Subscribe(tempJID, E_EVENT_ID_TYPE.ON_HKEY_PRESSED, aType, true, ref  Global.g_WSDLGeneric);
                    //4. execute print job
                    ExcutePrintJob(_webService, tempJID);
                    Global.Log("excute the print job");
                }
            }
            else
            {
                Global.Log("Failed to create Web service");
            }

        }
        catch (SoapException soapErr)
        {
            Ret = false;
            Global.Log("Page_Load: SoapException");

            //This sample displays SoapFault code and message as error message.
            //We recommend to show the custom error message suitable for your solution.  
            string sTextCode = "faultcode:  " + soapErr.Code.Name;
            string sTextErr = "faultstring:  " + soapErr.Message;
            string errormsg = "Error has occured";

            Global.Log(sTextCode + "\n" + sTextErr + "\n" + errormsg);
            if (tempJID != null)
                UtilCommon.dicWebServerJob.Remove(tempJID.uid);
        }
        catch (Exception sErr)
        {
            Ret = false;
            Global.Log("Page_Load: Exception");
            string sMsgText = "Exception Occurred\n";
            sMsgText += sErr.Message.ToString();
            string sMsgText1 = "";
            sMsgText1 += sErr.Source;
            Global.Log(sMsgText + "\n" + sMsgText1);
        }

        return Ret;
    }

    #endregion

    #region getFilePath
    /// <summary>
    /// getFilePath
    /// </summary>
    /// <returns></returns>
    string getFilePath(int taskID)
    {
        if (adpter == null)
            adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
        dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(taskID);

        string pathfile = table[0].FileLocation + table[0].DiskFileName;
        return pathfile;
    }

    #endregion

    #region getFileName
    /// <summary>
    /// getFileName
    /// </summary>
    /// <returns></returns>
    string getFileName(int taskID)
    {
        if (adpter == null)
            adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
        dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(taskID);

        return table[0].PrintFileName;
    }

    #endregion

    #region geturlpath
    /// <summary>
    /// geturlpath
    /// </summary>
    /// <returns></returns>
    public string geturlpath(string backUrl)
    {

        string ipaddress = Session["LOCAL_ADDR"] != null ? Session["LOCAL_ADDR"].ToString() : "";
        string session = "/(S(" + Session.SessionID + "))";
        if (String.Compare(UtilCommon.UseSSL.ToString(), "true", true) == 0)
        {
            string urlPath = @"https://" + ipaddress + Session["ApplicationPath"].ToString() + "/MFPScreen" + session + "/PrintFileDownload.aspx?download=yes&filePath=" + backUrl;
            return urlPath;
        }
        else
        {
            string urlPath = @"http://" + ipaddress + Session["ApplicationPath"].ToString() + "/MFPScreen" + session + "/PrintFileDownload.aspx?download=yes&filePath=" + backUrl;
            return urlPath;
        }
    }

    #endregion

    #region encapsulation the web server method

    private OSA_JOB_ID_TYPE CreateJob(MFPCoreWS _webService)
    {
        string[] strTmp = Session["UISESSIONID"].ToString().Split(',');
        OSA_JOB_ID_TYPE tempJID = (OSA_JOB_ID_TYPE)_webService.CreateJob(E_MFP_JOB_TYPE.PRINT, strTmp[0], ref Global.g_WSDLGeneric);
        return tempJID;
    }

    private void SetJobPara(MFPCoreWS _webService, OSA_JOB_ID_TYPE tempJID, string taskID)
    {
        XML_DOC_SET_TYPE setData = GetSettablePara(_webService);

        string filePath = getFilePath(Convert.ToInt32(taskID));

        //_webService.SetJobElements(tempJID, setData, ref  Global.g_WSDLGeneric);
        bool bret = Global.SetThePropValueInXMLDOCSETType(ref setData, "file-name", Path.GetFileName(filePath));
        bret = Global.SetThePropValueInXMLDOCSETType(ref setData, "url", geturlpath(filePath));


        _webService.SetJobElements(tempJID, setData, ref  Global.g_WSDLGeneric);

        if (UtilCommon.dicWebServerJob.ContainsKey(Session["LoginName"].ToString()))
        {
            UtilCommon.dicWebServerJob[Session["LoginName"].ToString()].Clear();
            UtilCommon.dicWebServerJob[Session["LoginName"].ToString()].Add(Path.GetFileName(filePath), taskID);
        }
        else
        {
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add(getFileName(Convert.ToInt32(taskID)), taskID);
            UtilCommon.dicWebServerJob.Add(Session["LoginName"].ToString(), tmp);
        }
    }

    private XML_DOC_SET_TYPE GetSettablePara(MFPCoreWS _webService)
    {
        XML_DOC_DSC_TYPE xmlDoc = new XML_DOC_DSC_TYPE();
        ARG_SETTABLE_TYPE arg = new ARG_SETTABLE_TYPE();
        arg.Item = (E_MFP_JOB_TYPE)E_MFP_JOB_TYPE.PRINT;
        xmlDoc = _webService.GetJobSettableElements(arg, ref Global.g_WSDLGeneric);
        XML_DOC_SET_TYPE SETxmlDoc = null;
        SETxmlDoc = Global.ConvertDSCToSETType(xmlDoc);
        return SETxmlDoc;
    }

    private void ExcutePrintJob(MFPCoreWS _webService, OSA_JOB_ID_TYPE tempJID)
    {
        _webService.ExecuteJob(tempJID, ref  Global.g_WSDLGeneric);
    }

    #endregion 
}