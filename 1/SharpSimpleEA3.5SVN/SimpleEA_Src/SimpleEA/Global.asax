<%--
// ==============================================================================
// File Name           : Global.asax
// Description         : Global.asax
// Author(s)           : Ji Jianxiong
//                       Build No: 1.0.3.2: UI Update.
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>
<%@ Application Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e) 
    {
        // edit by Wei Changye 2012.03.19 
        ////Add by Wei Changye 2012.02.27 cycle delete the print file------86400000 millisecond = 1 day
        //System.Timers.Timer myTimer = new System.Timers.Timer(86400000);
        //myTimer.Elapsed += new System.Timers.ElapsedEventHandler(DeleteSplFile);
        //myTimer.Enabled = true;

        //UtilCommon.CheckConnectionString();
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        if (Application[Helper.MFPApplication.MFPAPP_NAME] != null)
        {
            Application[Helper.MFPApplication.MFPAPP_NAME] = null;
        }
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        Exception objError = Server.GetLastError().GetBaseException();
        string strError = "Error Caught in Application_Error event\n" +
                "Error in:" + Request.Url.ToString() +
                "\nError Message:" + objError.Message.ToString() +
                "\nStach Trace:" + objError.StackTrace.ToString();
        if (User.Identity.Name != "" )
        {
            strError = strError + "\nLogin Name:" + User.Identity.Name;
        }
        System.Diagnostics.EventLog.WriteEntry("Simple EA System", strError);
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    
    // edit by Wei Changye 2012.03.19
    ///// <summary>
    ///// delete the print file
    ///// </summary>
    ///// <Date>2012.03.12</Date>
    ///// <Author>SLC Wei Changye</Author>
    ///// <Version>1.2</Version>
    //private static void DeleteSplFile(object source, System.Timers.ElapsedEventArgs e)
    //{
    //    Global.CheckCycle(DateTime.Now);
    //}
    
    
       
</script>
