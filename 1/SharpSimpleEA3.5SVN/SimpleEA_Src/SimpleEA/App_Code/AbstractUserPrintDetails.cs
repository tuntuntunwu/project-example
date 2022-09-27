using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Web.UI.WebControls;
using dtMFPPrintTaskTableAdapters;
using common;
using DAL;
using Model;
/// <summary>
///AbstractUserPrintDetails 的摘要说明
/// </summary>
/// <Date>2012.08.31</Date>
/// <Author>Wei Changye</Author>
public abstract class AbstractUserPrintDetails : MFPListMainPage
{
    protected FollowMEHandler handler;

    MFPPrintTaskTableAdapter adpter;

	public AbstractUserPrintDetails()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    #region Botton Event

    #region btnPrint_Click
    private string GetFilePath(string key)
    {
        if (adpter == null)
            adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
        dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(Convert.ToInt32(key));

        if (table != null && table.Count > 0)
        {
            string pathfile = table[0].FileLocation + table[0].DiskFileName;
            return pathfile;
        }
        else
            return "";
    }



    /// <summary>
    /// btnPrint_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (IsTimeOut())
        {
            return;
        }

        try
        {
            string loginname = this.GetHidLoginName();

            dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
            dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByLoginName(loginname);

            List<string> alllist = new List<string>();
            if (table != null && table.Count > 0)
            {
                for (int k = 0; k < table.Count; k++)
                {
                    string ID = table[k].MFPPrintTaskID.ToString();
                    alllist.Add(ID);
                }
            }

            if (alllist.Count == 0)
            {
                return;
            }
            printSelectDoc(alllist);
            handler.DeleteTask(alllist);

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            return;
        }
    }

    #endregion

    #region btnPrintAndDelete_Click
    /// <summary>
    /// btnPrintAndDelete_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrintAndDelete_Click(object sender, EventArgs e)
    {
        if (IsTimeOut())
            return;

        try
        {
            //btnPrint_Click(sender, e);
            List<string> list = SelectedItem();
            printSelectDoc(list);
            //handler.DeleteTask(SelectedItem());
            handler.DeleteTask(list);
        }
        catch (Exception ex)
        {
            ErrorAlert("打印过程中出现错误，请联系管理员！");
            Global.Log(ex.ToString());
        }
    }

    protected void printSelectDoc(List<string> list)
    {
        try
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Request.Url.Host);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            if (ipAddress.ToString().Equals("127.0.0.1"))
            {
                ipHostInfo = Dns.Resolve(Dns.GetHostName());
                ipAddress = ipHostInfo.AddressList[0];
            }

            //2018-01-04
            //打印留底判断
            //此处修改代印留底
            //List<string> list = SelectedItem();

            string IP = GetHidRemote();

            SPLHandler splHandler = new SPLHandler();
            DalMFP mfp = new DalMFP();
            MFPEntry bean = mfp.GetInfoByIP(IP);
            int monitor = bean.Monitor;
            int printMonitor = monitor % 10;
            int copyMonitor = monitor / 10 % 10;
            int bw = monitor / 100;

            if (printMonitor == 1 )
            {
                for (int k = 0; k < list.Count; k++)
                {
                    string key = list[k];
                    string filepath = GetFilePath(key);
                    splHandler.changeSplFile(filepath);
                }
            }

            //强制黑白
            UtilCommon util = new UtilCommon();
            for (int k = 0; k < list.Count; k++)
            {
                string key = list[k];
                string filepath = GetFilePath(key);
                int print_bw = util.GetTaskPrintBW(key);
                if (print_bw == 1 || bw == 1)
                {
                    splHandler.forcePrintBWSplFile(filepath);
                }
            }
            //此处修改打印文档

            //handler.SendPrintTaskID(SelectedItem(), GetHidRemote(), ipAddress.ToString());
            handler.SendPrintTaskID(list, GetHidRemote(), ipAddress.ToString());
        }
        catch (Exception ex)
        {
            ErrorAlert("打印过程中出现错误，请联系管理员！");
            Global.Log(ex.ToString());
        }
    }
    #endregion

    #region btnDelete_Click
    /// <summary>
    /// btnDelete_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsTimeOut())
            return;
        handler.DeleteTask(SelectedItem());

    }

    #endregion

    #region "BtnExit_OnClick"
    /// <summary>
    /// BtnExit_OnClick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.01.07</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    public void btnExit_OnClick(object sender, EventArgs e)
    {
        //chen 20160427 update start
        //Application.Clear();
        Application[GetHidSN()] = null;
        //end
        Session.Clear();
        handler.MFPExit(GetHidSN());
    }
    #endregion
    public void OnTestTimeout()
    {
        //chen 20160427 update start
        //Application.Clear();
        Application[GetHidSN()] = null;
        //end
        Session.Clear();
        handler.MFPExit(GetHidSN());
        //Server.Transfer
        //Response.Redirect("../OsaMain4.aspx");
    }

    #region "BtnEntry_OnClick"
    /// <summary>
    /// BtnEntry_OnClick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.01.07</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    public void btnEntry_OnClick(object sender, EventArgs e)
    {
        //Application["loggedinuser"] = Session["loggedinuser"];
        Application[GetHidSN()] = GetHidUID();
        if (IsTimeOut())
            return;
        handler.MFPEntry(GetHidSN(), GetHidUID());
    }
    #endregion

    #region "btnFlash_Click"
    /// <summary>
    /// BtnEntry_OnClick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.01.07</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    public void btnFlash_Click(object sender, EventArgs e)
    {
        if (IsTimeOut())
            return;
    }
    #endregion

    #region "imgBtnRegister"
    /// <summary>
    /// imgBtnRegister_OnClick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.15</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    public virtual void imgBtnRegister_OnClick(object sender, EventArgs e)
    {
        if (IsTimeOut())
            return;
        Response.Redirect("~/MFPScreen/RecordCard.aspx?type=" + E_EA_OSA_TYPE.OSA40.ToString() + "&loginid=" + GetHidUID());
    }
    #endregion

    #endregion

    #region PartSubString
    /// <summary>
    /// PartSubString
    /// </summary>
    public string PartSubString(string s)
    {
        if (s.Length > 29)
        {

            return s.Substring(0, 29) + "...";//这里也可以控制的判断被截取的字符有几个就输出几个点
        }
        return s;
    }

    #endregion

    #region "Occurs when a data row is bound to data in GridView."
    /// <summary>
    /// Reset CheckBox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CustomView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            e.Row.CssClass = "UnselectedTR";
        }
    }
    #endregion

    protected abstract bool IsTimeOut();

    protected abstract List<string> SelectedItem();

    protected abstract string GetHidRemote();

    protected abstract string GetHidSN();

    protected abstract string GetHidUID();

    protected abstract string GetHidLoginName();

}