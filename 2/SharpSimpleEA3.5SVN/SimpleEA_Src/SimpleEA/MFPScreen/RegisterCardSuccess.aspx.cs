using System;
using System.Collections.Generic;
using System.Web;
using SesMiddleware;
using System.Collections.Specialized;
using Osa.Util;
using System.Data.SqlClient;
using Osa.MfpWebService;

public partial class MFPScreen_RegisterCardSuccess : System.Web.UI.Page
{
    private string icCard;
    protected void Page_Load(object sender, EventArgs e)
    {
        SetTdVisible();

        // OSA version
        if (Request.Params["type"] != null)
        {
            if (Request.Params["type"].ToString().Equals(E_EA_OSA_TYPE.OSAMAIN.ToString()))
                hidType.Value = "UserPrintDetails.aspx";
            else
                if (Request.Params["type"].ToString().Equals(E_EA_OSA_TYPE.OSA40.ToString()))
                    hidType.Value = "UserPrintDetailsFor4.aspx";
        }

        if (Request.Params["iccard"] != null)
        {
            icCard = Request.Params["iccard"].ToString();
        }

        lblTitle.Text = "编号为：" + icCard + "的IC卡，已成功注册！";

    }

    public void SetTdVisible()
    {
        leftTd.Visible = false;
        rigthTd.Visible = false;
        mainTable.Border = 0;
    }

    protected void imgBtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(hidType.Value.ToString(),false);
    }
}