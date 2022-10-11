
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
using System.ComponentModel;
using Osa.MfpWebService;
using System.Diagnostics;
using SLCRegister;

public partial class _Default : System.Web.UI.Page 
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Browser.Browser.Equals("IE") || Request.Browser.Browser.Equals("Chrome") ||  Request.Browser.Browser.Equals("AppleMAC-Safari"))
        {

            //2011.03.21 Delete By SES Jijianxiong ST
            // Bug Management Sheet_SimpleEA_110321.xls No.23
            //Helper.ConfigFileInfo infolice = new Helper.ConfigFileInfo();
            //string filelice = infolice.ExistsLicenseFile();
            //if (string.IsNullOrEmpty(filelice))
            //{
            //    Response.Redirect("LicenseErrorPage.htm");
            //}
            //2011.03.21 Delete By SES Jijianxiong ED


            Response.Redirect("Login\\Login.aspx");
            //if (Request.UserAgent.IndexOf("MSIE 6.0") != -1)
            //{
            //    Response.Redirect("Login\\Login_ie.htm");
            //}
            //else
            //{
            //    Response.Redirect("Login\\Login.aspx");
            //}
            return;
        }

        //2011.03.21 Delete By SES Jijianxiong ST
        // Bug Management Sheet_SimpleEA_110321.xls No.23
        //Helper.ConfigFileInfo info = new Helper.ConfigFileInfo();
        //string file = info.ExistsLicenseFile();
        //if (string.IsNullOrEmpty(file))
        //{
        //    const string errormsg = "无法找到Liscense文件，请联系管理员。";
        //    bool HTMLerrorpage = false;

        //    for (int i = 0; i < Request.Headers.AllKeys.Length; i++)
        //    {
        //        if (string.Compare(Request.Headers.AllKeys[i], "X-BC-Use-Alternative", true) == 0)
        //        {
        //            HTMLerrorpage = true;
        //        }
        //    }

        //    if (HTMLerrorpage)
        //    {
        //        Response.Redirect(string.Format("FatalErrorHtml.aspx?msg={0}", errormsg));
        //    }
        //    else
        //    {
        //        Response.Redirect(string.Format("FatalError.aspx?msg={0}", errormsg));
        //    }
        //}
        //2011.03.21 Delete By SES Jijianxiong ED


        // 2012.06.13 Update by Wei Changye 
        // Add by Wei Changye 2012.03.13 
        // Product key check
        //if (!Verify())
        //{
        //    Response.Redirect("MFPScreen/ProductKeyInvalid.aspx", false);
        //}

        // chen update for test
        //20170605 del for develop
        //if (IsOutTrail())
        //{
        //    Response.Redirect("MFPScreen/ProductKeyInvalid.aspx", false);
        //    return;
        //}
        // end

      


        // Redirect to the ready page.
        // 2010.12.08 Update By SES Jijianxiong Ver1.1 Update ST
        //const string OSAForm = "OsaMain.aspx";
        const string OSAForm30 = "OsaMain.aspx";
        const string OSAForm20 = "OsaMain2.aspx";
        const string OSAForm21 = "OsaMain3.aspx";
        const string MAINPAGE = "Main.aspx";
        //const string HTMLBROWSERNAME = "NetFront";
        //2011.05.25 Add By SLC Zhoumiao ST
        const string OSAForm40 = "OsaMain4.aspx";
        //2011.05.25 Add By SLC Zhoumiao ED
        // string url = ERRORPAGE;
        string url = "";

        MFPModel mode = new MFPModel(Request);

        if (mode.IsHtmlBrower)
        {
            url = MAINPAGE;
            //2011.05.25 Add By SLC Zhoumiao ST
            if (mode.OSAVersion.Equals(E_EA_OSA_TYPE.OSA40))
            {
                url = OSAForm40;
            }

            //2011.05.25 Add By SLC Zhoumiao ED
        }
        else
        {
            if (mode.OSAVersion.Equals(E_EA_OSA_TYPE.OSA20))
            {
                // Not Support USB
                if (mode.ColorMode == E_EA_COLOR_TYPE.BWCOLOR)
                {
                    // Color support : Black and White Only.
                    url = OSAForm20;
                }
                else
                {
                    // Color support : 256 Color.
                    url = OSAForm21;
                }
            }
            else
            {
                // Support USB
                if (mode.ColorMode == E_EA_COLOR_TYPE.BWCOLOR)
                {
                    // Color support : Black and White Only.
                    url = OSAForm30;
                }
                else
                {
                    // Color support : 256 Color.
                    url = OSAForm30;
                }
            }
        }

        //if (Request.UserAgent.Contains(HTMLBROWSERNAME))
        //{
        //    url = MAINPAGE;
        //}
        //else
        //{
        //    for (int i = 0; i < Request.Headers.AllKeys.Length; i++)
        //    {
        //        if ((string.Compare(Request.Headers.AllKeys[i], "X-BC-Alternative", true)) == 0)
        //        {
        //            // // Check whether the HTML browser is available.
        //            if (Request.Headers.Get(i).IndexOf(HTMLBROWSERNAME) > -1)
        //            {
        //                // Navigate to the page for NetFront
        //                url = MAINPAGE;
        //            }
        //            break;
        //        }
        //    }
        //}

        //test
        //url = MAINPAGE;

        //20140618 chen update for iccard login start
        if ("iccardlogin" == Request.Params["op"])
        {
            string iccardid = Request.Params["iccard_id"];
            url = url + @"?op=iccardlogin&iccard_id=" + iccardid;
            //Session["iccardid"] = iccardid;
        }

        //20140618 chen update for iccard login end
        if (string.IsNullOrEmpty(url))
        {
            Response.Redirect(string.Format("FatalError.aspx?msg={0}", UtilConst.MSG_MFP_ERRORMFP));
        }
        else
        {
             Response.Redirect(url);
        }
    }

    #region "Verify if Product Key Exist"
    /// <summary>
    /// Verify if Product Key Exist
    /// </summary>
    /// <param name="jsfun"></param>
    /// <Date>2012.03.13</Date>
    /// <Author>SLC Wei Changye</Author>
    /// <Version>1.2</Version>
    public bool Verify()
    {
        //初始化注册模块为“SimpleEA”；   
        LKCclass.LKC_Initiate('Y');
        int CheckResult = LKCclass.LKC_Check();
        int StartResult = LKCclass.LKC_Start();

        //如果是正式版或者在试用期限内，则直接跳转到登陆界面
        if (CheckResult == Convert.ToInt32(LKCclass.ResultCode.LKC_S_OK) || (CheckResult >= 0 && CheckResult <= LKCclass.ExpDays))
            //1.正式版本已注册！   
            return true;
        else
            return false;

    }
    #endregion

    #region "Verify if Registe"
    /// <summary>
    /// Verify if Product Key can use
    /// </summary>
    /// <param name="jsfun"></param>
    /// <Date>2012.06.13</Date>
    /// <Author>SLC Wei Changye</Author>
    /// <Version>1.2</Version>
    public bool IsOutTrail()
    {

        RegisterHandler.Initiate("A");
        KeyStatus ks = RegisterHandler.Check();

        if (ks.Equals(KeyStatus.NotRegister) || ks.Equals(KeyStatus.outTrial))
            return true;
        else
            return false;
    }
    #endregion
}
