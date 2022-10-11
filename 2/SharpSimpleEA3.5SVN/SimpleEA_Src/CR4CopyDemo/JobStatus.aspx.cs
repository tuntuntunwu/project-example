
#region Copyright SHARP Corporation
//	Copyright (c) 2010-2014 SHARP CORPORATION. All rights reserved.
//
//	Extended Sharp OSA SDK
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
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonOSA;

public partial class JobStatus : BasePage
{
    protected string requestGetJobStatusURL;
    protected string requestCancelJobURL;

    protected string detailStarted = string.Empty;
    protected string detailQueued = string.Empty;
    protected string detailFinished = string.Empty;
    protected string detailCanceled = string.Empty;
    protected string detailError = string.Empty;
    protected string detailSuspended = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        id_Label_Title.Text = Resources.OSAStrings.TITLE_LABEL_MAIN + OSACopy.PropName.SEPARATOR +
            Resources.OSAStrings.TITLE_LABEL_STATUS;

        //--- Session is restored from file ---
        Helper.Load(Request);
        DeviceSession s = Helper.GetSession(Request);
        s.jid = Request.Form[0];
        //--- Set session information ---
        Helper.SetSession(Request, s);

        requestGetJobStatusURL = s.GetJobStatusURL("GetJobStatusCallback", s.jid);
        requestCancelJobURL = s.CancelJobURL("CancelJobCallback", s.jid);

        activeStyleSheet.Text = "<link rel='stylesheet' type='text/css' href='" + s.cssFile + ".css'>";

        detailStarted = string.Format("<p>{0}</p>", Resources.OSAStrings.STATUS_STARTED);
        detailSuspended = string.Format("<p>{0}</p>", Resources.OSAStrings.STATUS_SUSPENDED);
        detailQueued = string.Format("<p>{0}</p>", Resources.OSAStrings.STATUS_QUEUED);
        detailFinished = string.Format("<p>{0}</p>", Resources.OSAStrings.STATUS_FINISHED);
        detailCanceled = string.Format("<p>{0}</p>", Resources.OSAStrings.STATUS_CANCELED);
        detailError = string.Format("<p>{0}</p><p>{1}</p>", Resources.OSAStrings.ERR_OCCURRED, Resources.OSAStrings.ERR_RETRY);
        //--- Save session information to file ---
        Helper.Save(Request);
    }
}
