using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Security;
using SLCRegister;

public partial class RegisterTipsPage : System.Web.UI.Page
{
    public const string StartAdminUrl = "~/UserInfo/UserList.aspx";
    public const string StartUserUrl = "~/Report/AvailableReport.aspx";
    public string RedirectUrl = string.Empty;
    public int CheckResult = 0;
    public int StartResult = 0;
    string RegisterKey = string.Empty;
    public int RegisterResult = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["txtLoginID"].ToString() == UtilConst.CON_USER_LONINNAME)
            RedirectUrl = StartAdminUrl;
        else
            RedirectUrl = StartUserUrl;

        RegisterHandler.Initiate("A");
        KeyStatus ks = (KeyStatus)Session["KeyStatus"];
        int leftDays = (int)Session["leftDays"];

        if (ks.Equals(KeyStatus.inTrial) && leftDays < 30)
        {
            this.labelDaysleft.Visible = true;
            this.labelDaysleft.Text = "Simple EA将在" + leftDays + "天后过期";
            this.labelexpire.Text = "过期后，Simple EA将无法继续使用。";
        }
        else
            if (ks.Equals(KeyStatus.NotRegister) || ks.Equals(KeyStatus.outTrial))
            {
                labelMessage.Text = "您尚未注册或没有授权，请重新进行注册！";
                //RegisterHandler.SetOutTrail();
                this.btnCancel.Enabled = false;
            }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        FormsAuthenticationTicket authenticationTicket;
        authenticationTicket = new FormsAuthenticationTicket(1, Session["txtLoginID"].ToString(), DateTime.Now, DateTime.Now.AddMinutes(UtilConst.CON_FORM_TIMEOUT), false, Session["txtLoginID"].ToString());
        // Encrypt the ticket.
        string encTicket = FormsAuthentication.Encrypt(authenticationTicket);
        // Create the cookie.
        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        Response.Redirect(RedirectUrl, false);
    }

}