using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Osa.Util;
using Osa.MfpWebService;
using System.Text;
using System.Net;

public partial class MFPScreen_UPD2 : System.Web.UI.Page
{
    public string content = string.Empty;
    FollowMEHandler handler;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if ("validate" == Request.Params["status"])
            {
                //filter selected
                List<string> selectList = new List<string>();
                for (int i = 0; i < Request.Params.Count - 50; i++)
                {
                    if (Request.Params["input" + i] != null)
                        selectList.Add(Request.Params["input" + i].ToString());
                }

                // action type
                if (Request.Params["id_print"] != null)
                {
                    if (handler == null)
                        handler = new FollowMEHandler();

                    // 2012.04.28 Update By Wei CHangye ST
                    //handler.PrintTask(selectList, Request.Params["REMOTE_ADDR"].ToString(), GetUserName(Request.Params["uid"].ToString()));
                    IPHostEntry ipHostInfo = Dns.Resolve(Request.Url.Host);
                    IPAddress ipAddress = ipHostInfo.AddressList[0];
                    if (ipAddress.ToString().Equals("127.0.0.1"))
                    {
                        ipHostInfo = Dns.Resolve(Dns.GetHostName());
                        ipAddress = ipHostInfo.AddressList[0];
                    }
                    handler.SendPrintTaskID(selectList, Request.Params["REMOTE_ADDR"].ToString(), ipAddress.ToString());
                    // 2012.04.28 Update By Wei CHangye ED

                    // 2012.05.29 add by Wei Changye
                    handler.DeleteTask(selectList);
                    //end
                }
                else
                    if (Request.Params["id_delete"] != null)
                    {
                        if (handler == null)
                            handler = new FollowMEHandler();
                        handler.DeleteTask(selectList);
                    }
                    else
                        if (Request.Params["id_use_mfp"] != null)
                        {
                            if (handler == null)
                                handler = new FollowMEHandler();
                            handler.MFPEntry(Request.Params["sn"].ToString(), Request.Params["uid"].ToString());
                        }
                        else
                            if (Request.Params["id_logout"] != null)
                            {
                                //if (handler == null)
                                //    handler = new FollowMEHandler();
                                //handler.MFPExit(Request.Params["sn"].ToString());
                                if (Request.Params["sn"] != null)
                                {
                                    MFPModel mode = new MFPModel(Request);
                                    if (mode.ColorMode == E_EA_COLOR_TYPE.BWCOLOR)
                                        Response.Redirect("../OsaMain3.aspx");
                                    else
                                        Response.Redirect("../OsaMain2.aspx");
                                }
                                else
                                    Response.Redirect("../OsaMain2.aspx");
                            }
            }

            if (Request.Params["selectall"] != null)
            {
                content = GenerateHtml(Request.Params["selectall"].ToString().Trim() == "1");
            }
            else
                content = GenerateHtml(false);
        }
        catch (Exception ex)
        {
            Global.Log(ex.ToString());
        }
    }

    private string GenerateHtml(bool isSelectedAll)
    {
        string uid;
        string sn;
        //if (Request.Params["uid"] != null)
        //    uid = Request.Params["uid"].ToString();
        //else
        //{
        //    uid = Application["loggedinuser"].ToString();
        //}

        if (Request.Params["sn"] != null)
            sn = Request.Params["sn"].ToString();
        else
            sn = Application["strserialNumber"].ToString();


        if (Request.Params["uid"] != null)
            uid = Request.Params["uid"].ToString();
        else
        {
            uid = Application[sn].ToString();
        }


        dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
        dtMFPPrintTask.MFPPrintTaskDataTable taskTable = adpter.GetDataByLoginName(GetLoginName(uid));

        int count = 0;
        StringBuilder sb = new StringBuilder();
        sb.Append("<input id='id_flash' title='刷新' type='submit'/>");

        if (isSelectedAll)
        {
            sb.Append("<input type='checkbox' id='selectall' title='全选' checked='checked' onclick='UserPrintDetailsFor2.aspx?selectAll=0'/>");
            foreach (dtMFPPrintTask.MFPPrintTaskRow item in taskTable.Rows)
            {
                sb.Append("<input id='input" + count + "' checked='checked' type='checkbox' title='" + item.CreateTime + "      " + item.PrintFileName + "' value='" + item.MFPPrintTaskID.ToString() + "'/>");
                count++;
            }
        }
        else
        {
            sb.Append("<input type='checkbox' id='selectall' title='全选' onclick='UserPrintDetailsFor2.aspx?selectAll=1'/>");
            foreach (dtMFPPrintTask.MFPPrintTaskRow item in taskTable.Rows)
            {
                sb.Append("<input id='input" + count + "' type='checkbox' title='" + item.CreateTime + "      " + item.PrintFileName + "' value='" + item.MFPPrintTaskID.ToString() + "'/>");
                count++;
            }
        }
        sb.Append("<input id='sn'  type='hidden' value='" + sn + "'/>");
        sb.Append("<input id='uid'  type='hidden' value='" + uid + "'/>");

        return sb.ToString();
    }

    private string GetLoginName(string uid)
    {
        dtUserInfoTableAdapters.UserInfoTableAdapter adpter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
        dtUserInfo .UserInfoDataTable table = adpter.GetDataByUserId(Convert.ToInt32( uid));

        if (table != null && table.Count > 0)
            return table[0].LoginName;
        else
            return "";
    }
}