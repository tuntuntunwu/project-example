using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Osa.MfpWebService;
using Osa.Util;

public partial class Masterpage_MFPPrintDetailsMasterPage : System.Web.UI.MasterPage
{
    
    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.01.07</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckUser();
        //InitialGridView();
    }
    #endregion

    #region "Title"
    /// <summary>
    /// Title
    /// </summary>
    /// <Date>2012.01.07</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    public string Title
    {
        set
        {
            this.Page.Header.Title = "::Simple EA Application :: " + value;
        }
    }
    #endregion

    #region "Show Message In IE."
    /// <summary>
    /// Show Message In IE.
    /// </summary>
    /// <param name="strMessage"></param>
    /// <Date>2010.06.28</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void Alert(string strMessage)
    {
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertMessage", "<script>alert('" + strMessage + "')</script>");
        return;

    }
    #endregion

    public void SetTdVisible()
    {
        leftTd.Visible = false;
        rigthTd.Visible = false;
        mainTable.Border = 0;
    }

    public void CheckUser()
    {
        //if (Application["loggedinuser"] == null || Application["loggedinuser"].ToString() == UtilConst.USER_UNKNOW.ToString())
        //{
        //    btnExit_OnClick(new object(), new EventArgs());
        //}
    }
}
