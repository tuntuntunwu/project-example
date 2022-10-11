<%--
==============================================================================
//
//	Extended Sharp OSA SDK	
//
//	Copyright (c) 2010-2014 SHARP CORPORATION. All rights reserved.
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER "AS IS" AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
//
// Global.asax
--%>
<%@ Application Language="C#" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Security" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        Debug.WriteLine("Application_Start");
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        // Code that runs on application shutdown
        Debug.WriteLine("Application_End");
    }
        
    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        Debug.WriteLine("Session_Start");
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Debug.WriteLine("Session_End");
        try
        {
            Helper.Save(Request);
        }
        catch (IOException ex)
        {
            Debug.WriteLine(ex.Message);
        }
        catch (SecurityException ex)
        {
            Debug.WriteLine(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
    {
        // globalication readiness code
        string strCharset = string.Empty;
        if (null != Request.UserAgent)
        {
            if (Request.UserAgent.IndexOf("OpenSystems") >= 0)
            {
                // if accessed from MFP, return back Accept-Charset passed
                strCharset = GetAcceptCharset();
            }
        }

        if (string.IsNullOrEmpty(strCharset))
        {
            // otherwise set to default
            strCharset = "utf-8";
        }
        Response.ContentEncoding =
            System.Text.Encoding.GetEncoding(strCharset);
    }

    // retrieve Accpet-Charset passed from MFP browser in
    // http request headers
    string GetAcceptCharset()
    {
        string strAcceptCharset = "utf-8";

        for (int i = 0; i < Request.Headers.AllKeys.GetLength(0); i++)
        {
            if ("Accept-Charset" == Request.Headers.AllKeys[i])
            {
                // the first entry is the value we want
                // (if multiple values are present, they are delimited
                // by ',' or ';' characters
                char[] delims = { ',', ';' };
                string[] strValues = Request.Headers.Get(i).Split(delims);

                if (strValues.Length > 0)
                {
                    strAcceptCharset = strValues[0];
                }
                break;
            }
        }

        return strAcceptCharset;
    }
    
       
</script>
