using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RegisterICCardForOSA3 : MainOSA
{
    public string cardId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["id"] == null)
                div_error = "卡号为空，请返回后重试！";
            else
                ViewState["cardId"] = Request.Params["id"].ToString();
        }

        if (ViewState["cardId"] != null)
            cardId = ViewState["cardId"].ToString();
        else
            Response.Redirect("OsaMain3.aspx");

        div_error = "";
    }


}