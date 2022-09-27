using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

public partial class RegisterKeyPage : System.Web.UI.Page
{

    public int CheckResult = 0;
    public string LoginUrl = "~/UserInfo/UserList.aspx";
  //  const string StartAdminUrl = "~/UserInfo/UserList.aspx";
    string RegisterKey = string.Empty;
    public int RegisterResult = 0;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ViewState["retu"] = Request.UrlReferrer.ToString();   
        string MessageTips = Request.QueryString["MessageTips"];     
       // CheckResult =Convert.ToInt32(Request.QueryString["CheckResult"]);
        System.Web.UI.WebControls.Label RegisterMessage = (System.Web.UI.WebControls.Label)this.FindControl("LabelMessage");
        RegisterMessage.Text = MessageTips;     

    }

    //进行注册码的注册！
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
        RegisterKey = this.txtRegisterKey.Text.Trim();

        if ( RegisterKey != null&& RegisterKey!=string.Empty )
        {
            LKCclass.LKC_Initiate('Y');
            RegisterResult = LKCclass.LKC_Register(RegisterKey);
            int SC = LKCclass.LKC_Start();
            int CR = LKCclass.LKC_Check();
            //int SC = LKCclass.LKC_Check();
            if (RegisterResult == Convert.ToInt32(LKCclass.ResultCode.LKC_S_KEY_EVALUATION) || RegisterResult == Convert.ToInt32(LKCclass.ResultCode.LKC_S_KEY_PURCHASED))
            {
                if(SC == Convert.ToInt32(LKCclass.ResultCode.LKC_S_OK))
                {
                //出现注册成功，重新登录确认界面！
                //确认后，重新返回login界面。                
                    Response.Redirect("RegisterSuccess.aspx", true);
                     //Response.Redirect("Login\\Login.aspx",true);

                }
                else if (SC == Convert.ToInt32(LKCclass.ErrorCode.LKC_E_ALREADY_IN_EVALUATION))
                {
                    if (CR == Convert.ToInt32(LKCclass.ResultCode.LKC_S_EXPIRED))
                    {
                        this.LabelTips.Visible = true;
                        this.LabelTips.Text = "此注册码已过期，请输入有效注册码";
                    }
                    else
                    {
                        this.LabelTips.Visible = true;
                        this.LabelTips.Text = "此注册码已存在，请输入有效注册码";
                    }
                }
            }          
            else
            {
                //注册码输入错误，请重新输入
                //提示label出现！
                this.LabelTips.Visible = true;
                this.LabelTips.Text = "注册码错误，请重新输入！";
            }
        }
        else
        {
           //注册码不能为空
           //提示label出现！
            this.LabelTips.Visible = true;
            this.LabelTips.Text = "注册码不能为空，请重新输入！";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //返回上一页：即注册信息提示页
        Response.Redirect(ViewState["retu"].ToString());   
    }

}