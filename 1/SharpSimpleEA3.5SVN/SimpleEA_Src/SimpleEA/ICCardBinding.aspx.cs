using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using SesMiddleware;

/// <summary>
/// add by Wei Changye 2011.12.27 use template pattern to format the register process
/// </summary>
public partial class ICCardBinding : AbstractICCardRegister
{
    private string strLoginName;
    private string strPassword;
    private string strIcCard;
    protected void Page_Load(object sender, EventArgs e)
    {
        InitialParam(Request.Form);
        RegisterProcessTemplate(strLoginName, strPassword, strIcCard);
    }

    protected override void InitialParam(NameValueCollection collection)
    {
        strLoginName = collection["LoginName"].ToString();
        strPassword = collection["Password"].ToString();
        strIcCard = collection["ICCard"].ToString();
    }

    protected override void RegisterComplete()
    {
        Response.Write("1");
        Response.End();
    }

    protected override void RegisterError() 
    {
        Response.Write("2");
        Response.End();
    }
}