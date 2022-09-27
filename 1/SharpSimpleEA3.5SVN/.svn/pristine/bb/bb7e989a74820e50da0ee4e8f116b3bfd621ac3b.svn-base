using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using common;

/// <summary>
/// add by Wei Changye 2011.12.27 use template pattern to format the register process
/// </summary>
public partial class RegisterICCard : AbstractICCardRegister
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialParam(Request.Params);
        }
        span_msg.Text = "";
    }

    protected override void InitialParam(NameValueCollection collection)
    {
        if (collection["id"] == null)
        {
            Response.Redirect("Default.aspx");
            span_msg.Text = "卡号为空，请返回后重试！";
        }
        else
            ViewState["cardId"] = collection["id"];
    }

    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        //RegisterProcessTemplate(txtLoginName.Text, txtPassword.Text, ViewState["cardId"].ToString());
        dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
        int Login_auth_method = settingDispRow.Login_Auth_method;
        if (Login_auth_method == 0)
        {
            RegisterProcessTemplate(txtLoginName.Text, txtPassword.Text, ViewState["cardId"].ToString());
        }
        else if (Login_auth_method == 1)
        {
            LDAPHandler ldapAuth = new LDAPHandler();
            //string ret = ldapAuth.IDAuthentication(txtLoginName.Text, txtPassword.Text);
            //if (!ret.Equals(""))
            //{
            //    RegisterProcessTemplate(txtLoginName.Text, "666666", ViewState["cardId"].ToString());
            //}
            string ret = ldapAuth.ADCardRegister(txtLoginName.Text, txtPassword.Text, ViewState["cardId"].ToString());
            if (ret.Equals(""))
            {
                LDAPRegisterProcessTemplate(txtLoginName.Text, txtPassword.Text, ViewState["cardId"].ToString());
            }
            else
            {
                RegisterError();
            }
        }
        else
        {
            //DBAuthHandler dbAuth = new DBAuthHandler();
            //Boolean ret = dbAuth.DBLogin(txtLoginName.Text, txtPassword.Text);
            //if (ret)
            //{
            //    RegisterProcessTemplate(txtLoginName.Text, "666666", ViewState["cardId"].ToString());

            //}
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //RedirectToLogin();
        //Response.Redirect("MFPScreen/RecordCard.aspx?type=" + E_EA_OSA_TYPE.OSAMAIN.ToString());
        Response.Redirect("main.aspx");
    }

    protected override void RegisterComplete()
    {
        //string strScript = "";
        //strScript =
        //    "    <script language='javascript' type='text/javascript'>" + "\r\n" +
        //    "        function register() {" + "\r\n" +
        //    "            window.parent.location.href=Main.aspx'\r\n" +
        //    "        }" + "\r\n" +
        //    "        // Registermsg." + "\r\n" +
        //    "        if ( window.attachEvent)  window.attachEvent('onload',function() { register();});" + "\r\n" +
        //    "        else window.addEventListener('load',function() { register();},true);" + "\r\n" +
        //    "    </script>" + "\r\n";
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "yew", strScript);

        Response.Redirect("main.aspx");
    }

    protected override void RegisterError()
    {
        span_msg.Text = "您输入的账号或密码错误，请重新输入";
    }
}