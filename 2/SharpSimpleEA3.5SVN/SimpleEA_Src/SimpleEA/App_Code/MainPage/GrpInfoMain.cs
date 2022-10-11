#region Copyright SHARP Corporation
//	Copyright (c) 2010 SHARP CORPORATION. All rights reserved.
//
//	SHARP Simple EA Application
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Simple EA Application
/// MainPage for group information
///     [Edit] And [Add]
/// </summary>
/// <Date>2010.06.07</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public class GrpInfoMain:MainPage
{

    // JavaScript Function Name for Confirm dialog.
    // When Update Button Click
    private const string CON_JSCRIPT_FUN_UPDATE = "ScriptConfirmUpdate";
    // When Cancel Button Click
    private const string CON_JSCRIPT_FUN_CANCEL = "ScriptConfirmCancel";

	public GrpInfoMain()
	{
		//
		// TODO: Add constructor logic here
		//
        Page.ClientScript.RegisterStartupScript(this.GetType(), "SetFocusInFirstItemTxt", "<script>SetFocusInFirstItemTxt();</script>");
    }

    #region "the confirmation dialog with msg Item.(Update Button)"
    /// <summary>
    /// the confirmation dialog with msg Item.(Update Button)
    /// </summary>
    /// <seealso cref="ConfirmFunctionUpd"/>
    /// <seealso cref="ScriptConfirm"/>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.02</Version>
    private void ScriptConfirmListUpd(string strValidationGroup)
    {
        ScriptConfirm(CON_JSCRIPT_FUN_UPDATE , strValidationGroup);
    }
    #endregion

    #region "message for page.(Update Button)"
    /// <summary>
    /// message for page.(Update Button)
    /// </summary>
    /// <seealso cref="ScriptConfirmListUpd"/>
    /// <param name="msg"></param>
    /// <returns>OnClientClick Function</returns>
    /// <seealso cref="ScriptConfirmList"/>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected string ConfirmFunctionUpd(string msg, string strValidationGroup)
    {
        string strMsg;
        strMsg = CON_JSCRIPT_FUN_UPDATE + "('{0}')";
        strMsg = string.Format(strMsg, msg);

        ScriptConfirmListUpd(strValidationGroup);
        return strMsg;
    }
    #endregion

    #region "the confirmation dialog with msg Item.(Cancel Button)"
    /// <summary>
    /// the confirmation dialog with msg Item.(Cancel Button)
    /// </summary>
    /// <seealso cref="ConfirmFunctionCancel"/>
    /// <seealso cref="ScriptConfirm"/>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void ScriptConfirmListCancel()
    {
        ScriptConfirm(CON_JSCRIPT_FUN_CANCEL,"");
    }
    #endregion

    #region "message for page.(Cancel Button)"
    /// <summary>
    /// message for page.(Cancel Button)
    /// </summary>
    /// <seealso cref="ScriptConfirmListCancel"/>
    /// <param name="msg"></param>
    /// <returns>OnClientClick Function</returns>
    /// <seealso cref="ScriptConfirmList"/>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected string ConfirmFunctionCancel(string msg)
    {
        string strMsg;
        strMsg = CON_JSCRIPT_FUN_CANCEL + "('{0}')";
        strMsg = string.Format(strMsg, msg);
        ScriptConfirmListCancel();
        return strMsg;
    }
    #endregion

    #region "btnCancel_Click"
    /// <summary>
    /// Back to Group Management Screen
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <Date>2010.06.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("GroupList.aspx", false);
    }
    #endregion

    #region "Move Item from FromList to ToList."
    protected void MoveList(ListBox lstFrom, ListBox lstTo)
    {
        ListItem item;
        for (int i = 0; i < lstFrom.Items.Count; i++)
        {
            if (lstFrom.Items[i].Selected)
            {
                item = new ListItem();
                item.Value = lstFrom.Items[i].Value;
                item.Text = lstFrom.Items[i].Text;
                lstFrom.Items.Remove(item);
                lstTo.Items.Add(item);
                i = i - 1;
            }
        }
    }
    #endregion

}
