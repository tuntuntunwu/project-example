using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
///MFPListMainPage 的摘要说明
/// </summary>
public class MFPListMainPage : System.Web.UI.Page
{
	public MFPListMainPage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
    }    
    private const string CON_JSCRIPT_FUN = "scriptConfirm";

    // GridView
    private GridView detailGridView;
    public GridView CustomGridView
    {
        get { return detailGridView; }
    }

    // The count page Control of this GridView.
    private Label currentpageLabel;
    public Label CurrentpageLabel
    {
        get { return currentpageLabel; }
    }

    // The count page Control of this GridView.
    private Label totalpageLabel;
    public Label CustomTotalpageLabel
    {
        get { return totalpageLabel; }
    }

    // The DataKeys in this GridView.
    private string strFieldNames;
    public string FieldNames
    {
        get { return strFieldNames; }
    }

    #region "ConnectionStrings For SimpleEA"
    /// <summary>
    /// ConnectionStrings For SimpleEA
    /// </summary>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public String DBConnectionStrings
    {
        get { return UtilCommon.DBConnectionStrings; }
    }

    #endregion

    #region "Simple EA GridView"

    /// <summary>
    /// Simple EA GridView
    /// For Group List page and User List page
    /// </summary>
    /// <param name="_detailGridView">GridView in this page</param>
    /// <param name="_numperpage">The PageIndex property to display that page in GridView.</param>
    /// <param name="_pageList">The number of records which show per page by the user.</param>
    /// <param name="_totalpageLabel">The count page Control of this GridView.</param>
    /// <param name="_selectbtn">The Select/DeSelect All Button.</param>
    /// <param name="_strFieldNames">The DataKeys in this GridView.</param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void SetListMainPgae(GridView _detailGridView,
            Label _currentpageLabel,
            Label _totalpageLabel,
            string _strFieldNames)
    {
        // GridView in this page
        detailGridView = _detailGridView;
        detailGridView.DataBound += new EventHandler(CustomView_DataBound);
        detailGridView.PreRender += new EventHandler(CustomView_PreRender);
        detailGridView.AllowPaging = true;
        detailGridView.AutoGenerateColumns = false;

        // AllowPaging
        detailGridView.AllowPaging = true;
        // PageList Control for GridView
        // The number of records which show per page by the user.
        currentpageLabel = _currentpageLabel;
        // Total page Control for GridView
        totalpageLabel = _totalpageLabel;
        // the DataKeys in this GridView
        strFieldNames = _strFieldNames;
    }
    #endregion

    #region "Show Next page."
    /// <summary>
    /// Show Next page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void IndexOfPage_Next(object sender, EventArgs e)
    {
        if (this.CustomGridView.PageIndex < this.CustomGridView.PageCount - 1)
        {
            currentpageLabel.Text = Convert.ToString(Convert .ToInt32( currentpageLabel.Text) + 1);
            // Show Next page.
            this.CustomGridView.PageIndex = this.CustomGridView.PageIndex + 1;
            
        }
    }
    #endregion

    #region "Show Pre Page."
    /// <summary>
    /// Show Pre Page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void IndexOfPage_Pre(object sender, EventArgs e)
    {
        if (this.CustomGridView.PageIndex > 0)
        {
            currentpageLabel.Text = Convert.ToString(Convert.ToInt32(currentpageLabel.Text) - 1);
            // Show Pre Page.
            this.CustomGridView.PageIndex = this.CustomGridView.PageIndex - 1;
            
        }
    }
    #endregion

    #region "Occurs when a data is bound to data in this GridView."
    /// <summary>
    /// Occurs when a data is bound to data in this GridView.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void CustomView_DataBound(object sender, EventArgs e)
    {
        GridView CustomGridView = (GridView)sender;

        if (this.CustomTotalpageLabel != null)
        {
            this.CustomTotalpageLabel.Text = CustomGridView.PageCount.ToString();

        }
    }
    #endregion

    #region "Raises the GridView.PreRender event."
    /// <summary>
    /// Raises the GridView.PreRender event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <seealso cref="renderEmptyGridView"/>
    /// <seealso cref="CustomView_OnUnload"/>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void CustomView_PreRender(object sender, EventArgs e)
    {
        GridView CustomGridView = (GridView)sender;
        // When DataSource is Null Or row's Count is 0.
        // Show the GridView Header In page;
        if (CustomGridView.Rows.Count == 0)
        {
            // Add A Null Row In GridView
            //renderEmptyGridView(CustomGridView, "GroupName,UserCount");
            renderEmptyGridView(CustomGridView);
        }
    }
    #endregion

    #region "Raises the GridView.PreRender event."
    /// <summary>
    /// Raises the GridView.Unload event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <seealso cref="CustomView_PreRender"/>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void CustomView_OnUnload(object sender, EventArgs e)
    {
        GridView CustomGridView = (GridView)sender;

        // When DataSource is Null Or row's Count is 0.
        // In CustomView_PreRender.renderEmptyGridView 
        // Set DataSourceID is null.
        if (CustomGridView.DataSourceID == null)
        {
            CustomGridView.DataSource = null;
        }
    }
    #endregion

    #region "Add A Null Row In GridView"
    /// <summary>
    /// Add A Null Row In GridView
    /// </summary>
    /// <param name="EmptyGridView">GridView In page</param>
    /// <seealso cref="CustomView_PreRender"/>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void renderEmptyGridView(GridView EmptyGridView)
    {
        DataTable dTable = new DataTable();
        char[] delimiterChars = { ',' };
        string[] colName = FieldNames.Split(delimiterChars);

        foreach (string myCol in colName)
        {
            DataColumn dColumn = new DataColumn(myCol.Trim());
            dTable.Columns.Add(dColumn);
        }

        DataRow dRow = dTable.NewRow();
        foreach (string myCol in colName)
        {
            dRow[myCol.Trim()] = DBNull.Value;

        }

        dTable.Rows.Add(dRow);
        EmptyGridView.DataSourceID = null;
        EmptyGridView.DataSource = dTable;
        EmptyGridView.DataBind();
        EmptyGridView.Rows[0].Visible = false;
    }
    #endregion

    #region "the confirmation dialog with msg Item."
    /// <summary>
    /// the confirmation dialog with msg Item.
    /// </summary>
    /// <seealso cref="ConfirmFunction"/>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void ScriptConfirmDel()
    {
        ScriptConfirm(CON_JSCRIPT_FUN, "");
    }
    #endregion

    #region "message for page"
    /// <summary>
    /// message for page
    /// </summary>
    /// <param name="DelectConfirmmsg">delect Confirm Message</param>
    /// <returns>OnClientClick Function</returns>
    /// <seealso cref="ScriptConfirmList"/>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected string ConfirmFunction(string DelectConfirmmsg)
    {
        string strMsg;
        strMsg = CON_JSCRIPT_FUN + "('{0}')";
        strMsg = string.Format(strMsg, DelectConfirmmsg);
        // confirmation dialog
        ScriptConfirmDel();

        return strMsg;
    }
    #endregion

    #region "the confirmation dialog with msg Item."
    /// <summary>
    /// the confirmation dialog with msg Item.
    /// </summary>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void ScriptConfirm(string name, string strValidationGroup)
    {
        string strScript = "";
        strScript = "    <script language='javascript' type='text/javascript'>" + "\r\n";
        strScript += "    function " + name + "(msg) {" + "\r\n";

        if (!"".Equals(strValidationGroup))
        {
            strScript += "        if (typeof(Page_ClientValidate) == 'function') {" + "\r\n";
            strScript += "            if ( !Page_ClientValidate('" + strValidationGroup + "') )" + "\r\n";
            strScript += "            { " + "\r\n";
            strScript += "                return false;" + "\r\n";
            strScript += "            }" + "\r\n";
            strScript += "        }" + "\r\n";
        }

        strScript += "        msg = msg.replace('<BR>','\\n')" + "\r\n";
        // Update By SES.JiJianXiong 2010.08.20 ST
        //strScript += "        if ( window.confirm(msg) ) {" + "\r\n";
        //strScript += "            return true;" + "\r\n";
        //strScript += "        } else {" + "\r\n";
        //strScript += "            if ( window.event ) {" + "\r\n";
        //strScript += "                window.event.returnValue = false;" + "\r\n";
        //strScript += "            }" + "\r\n";
        //strScript += "            return false;" + "\r\n";
        //strScript += "        }" + "\r\n";

        strScript += "        var fun =null;" + "\r\n";
        strScript += "        if ( window.event.srcElement ) {" + "\r\n";
        strScript += "            var buttonid = window.event.srcElement.name;" + "\r\n";

        strScript += "            fun = function (e) {" + "\r\n";
        strScript += "                __doPostBack(buttonid,'');" + "\r\n";
        strScript += "            };" + "\r\n";

        strScript += "        };" + "\r\n";
        //strScript += "        fun = function (e) {" + "\r\n";
        //strScript += "            window.document.forms[0].submit();" + "\r\n";
        //strScript += "        };" + "\r\n";
        strScript += "        window.confirm(msg , null, fun);" + "\r\n";
        strScript += "        if ( window.event ) {" + "\r\n";
        strScript += "            window.event.returnValue = false;" + "\r\n";
        strScript += "        }" + "\r\n";
        strScript += "        return false;" + "\r\n";
        // Update By SES.JiJianXiong 2010.08.20 ED
        strScript += "    }" + "\r\n";
        strScript += "    </script>" + "\r\n";

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), name, strScript);
    }
    #endregion

    #region ErrorAlert
    /// <summary>
    /// ErrorAlert
    /// </summary>
    /// <param name="message"></param>
    /// <Date>2012.02.27</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>1.2</Version>
    public void ErrorAlert(string message)
    {
        string strScript = "";
        strScript =
            "    <script language='javascript' type='text/javascript'>" + "\r\n" +
            "        function yew() {" + "\r\n" +
            "            alert('" + message + "');" + "\r\n" +
            "        }" + "\r\n" +
            "        // Registermsg." + "\r\n" +
            "        if ( window.attachEvent)  window.attachEvent('onload',function() { yew();});" + "\r\n" +
            "        else window.addEventListener('load',function() { yew();},true);" + "\r\n" +
            "    </script>" + "\r\n";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "yew", strScript);
    }
    #endregion

}
