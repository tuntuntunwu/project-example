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

/// <summary>
/// [product-family] is indicates the unique ID per MFP model. 
/// The application can change its processing per MFP by referencing this value.
/// </summary>
public class MFPModel
{
    private int intProduct_family = -1;
    // MFP Model Only for the M363/M453/M503
    private string strMFPModel = "";

    private const string CON_MX_M363 = "MX-M363";
    private const string CON_MX_M453 = "MX-M453";
    private const string CON_MX_M503 = "MX-M503";


    // Is MFP Brower?
    private bool bolIsMFPBrower = true;
    public bool IsMFPBrower
    {
        get
        {
            return bolIsMFPBrower;
        }
    }
    // Is HTML Brower?
    private bool bolIsHtmlBrower = true;
    public bool IsHtmlBrower
    {
        get
        {
            return bolIsHtmlBrower;
        }
    }
    // While MFP Brower , the Color mode
    // BW       : gif (B/W 2Color)
    // FullColor: gif (32bit)
    // 256Color : gif (256 Color)
    private E_EA_COLOR_TYPE eColorMode = E_EA_COLOR_TYPE.FULLCOLOR;
    public E_EA_COLOR_TYPE ColorMode
    {
        get
        {
            return eColorMode;
        }
    }

    // OSA Version.
    private E_EA_OSA_TYPE eOSAVersion = E_EA_OSA_TYPE.OSA30;
    public E_EA_OSA_TYPE OSAVersion
    {
        get
        {
            return eOSAVersion;
        }
    }

    private const string HTMLBROWSERNAME = "NetFront";

    public MFPModel(HttpRequest request)
    {
        //
        // TODO: Add constructor logic here
        //
        // Get the product_family
        GetProduct_family(request.UserAgent);
        // Get the Brower
        CheckMFPBrower(request);
        // Get MFP Model
        SpecialMFPProcess(request);

        // Get the Color Mode
        GetColorMode(intProduct_family);
        // Get OSA Version.
        GetOSAVersion(intProduct_family);

    }

    #region "CheckMFPBrower"
    /// <summary>
    /// CheckMFPBrower
    /// </summary>
    /// <param name="request"></param>
    /// <Date>2010.12.8</Date>
    /// <Author>SES Jijianxiong</Author>
    /// <Version>1.1</Version>
    private void CheckMFPBrower(HttpRequest request)
    {
        bolIsHtmlBrower = false;
        // for all MFP always support the MFP Brower 
        bolIsMFPBrower = true;

        if (request.UserAgent.Contains(HTMLBROWSERNAME))
        {
            bolIsHtmlBrower = true;
        }
        else
        {
            for (int i = 0; i < request.Headers.AllKeys.Length; i++)
            {
                if ((string.Compare(request.Headers.AllKeys[i], "X-BC-Alternative", true)) == 0)
                {
                    // // Check whether the HTML browser is available.
                    if (request.Headers.Get(i).IndexOf(HTMLBROWSERNAME) > -1)
                    {
                        // Navigate to the page for NetFront
                        bolIsHtmlBrower = true;
                    }
                    break;
                }
            }
        }
    }
    #endregion

    #region "GetProduct_family"
    /// <summary>
    ///GetProduct_family
    /// </summary>
    /// <param name="UserAgent"></param>
    /// <Date>2010.12.8</Date>
    /// <Author>SES Jijianxiong</Author>
    /// <Version>1.1</Version>
    private void GetProduct_family(string UserAgent)
    {
        intProduct_family = -1;
        if (string.IsNullOrEmpty(UserAgent))
        {
            // UnKnow MFP
            intProduct_family = -1;
            return;
        }

        if (!UserAgent.Contains("product-family"))
        {
            // UnKnow MFP
            intProduct_family = -1;
            return;
        }

        int intStart = UserAgent.IndexOf("product-family=\"");
        intStart = intStart + "product-family=\"".Length;
        int endStart = UserAgent.IndexOf("\";", intStart);
        try
        {
            intProduct_family = Convert.ToInt32(UserAgent.Substring(intStart, endStart - intStart));
            return;

        }
        catch 
        {

            intProduct_family = -1;
            return;
        }
    }
    #endregion

    #region "Get The Color Mode by product-family"
    /// <summary>
    /// Get The Color Mode by product-family
    /// </summary>
    /// <param name="intfamilyNum"></param>
    /// <Date>2010.12.8</Date>
    /// <Author>SES Jijianxiong</Author>
    /// <Version>1.1</Version>
    private void GetColorMode(int intfamilyNum) 
    {
        if (IsHtmlBrower) {
            // for all Html Brower the Color is Full Color.
            eColorMode = E_EA_COLOR_TYPE.FULLCOLOR;
            return;
        }


        switch (intfamilyNum)
        {
            //MX-M550 / M620 / M700
            case 51:
            //MX-M350 / M450
            case 52:
            //MX-230x / 270x
            case 32:
            //MX-350x / 450x
            case 33:
            //MX-260x / 310x
            case 63:
            //MX-M363 / M453 / M503 (Wide-VGA)
            //MX-M363 / M453 / M503 (Half-VGA) ?
            case 71:
                eColorMode = E_EA_COLOR_TYPE.BWCOLOR;
                break;
            //MX-M85x / M95x / M110x
            case 45:
            //MX-550x / 620x / 700x
            case 37:
                eColorMode = E_EA_COLOR_TYPE.COLOR256;
                break;
            //MX-6201N / 7001N
            case 57:
            //MX-360x / 410x / 500x
            case 64:
            //MX-C311 / C381 / C401 (Wide-VGA)
            //MX-C310 / C380 / C400 (480X272)
            case 65:
            //MX-B38x / B40x
            case 73:
            default:
                eColorMode = E_EA_COLOR_TYPE.FULLCOLOR;
                break;
        }

    }
    #endregion 

    #region "Get OSA Version"
    /// <summary>
    /// Get OSA Version
    /// </summary>
    /// <param name="intfamilyNum"></param>
    /// <Date>2010.12.8</Date>
    /// <Author>SES Jijianxiong</Author>
    /// <Version>1.1</Version>
    private void GetOSAVersion(int intfamilyNum)
    {
        E_EA_OSA_TYPE rGetOSAVersion = E_EA_OSA_TYPE.OSA20;

        switch (intfamilyNum)
        {
            //MX-M350 / M450
            case 52:
            //MX-M550 / M620 / M700
            case 51:
            //MX-M85x / M95x / M110x
            case 45:
            //MX-230x / 270x
            case 32:
            //MX-350x / 450x
            case 33:
            //MX-550x / 620x / 700x
            case 37:
            //MX-6201N / 7001N
            case 57:
                rGetOSAVersion = E_EA_OSA_TYPE.OSA20;
                break;
            //MX-260x / 310x
            case 63:
            //MX-360x / 410x / 500x
            case 64:
            //MX-C311 / C381 / C401 (Wide-VGA)
            //MX-C310 / C380 / C400 (480X272)
            case 65:
            //MX-B38x / B40x
            case 73:
            //MX-M363 / M453 / M503 (Wide-VGA)
            //MX-M363 / M453 / M503 (Half-VGA) ?
            case 71:            
            //2011.05.25 Update By SLC Zhoumiao ST           
            //default:
            //    rGetOSAVersion = E_EA_OSA_TYPE.OSA30;
            //    break;
            //MX-C380P/C400P
            case 79:
            //MX-B380P/B400P
            case 80:
            //MX-M623/M753
            case 81:
            //MX-2310
            case 82:
                rGetOSAVersion = E_EA_OSA_TYPE.OSA30;
                break;
            //MX-2610/3110/3610(Wide-SVGA[1024*544])
            case 83:
            //MX-411x/511x(Wide-SVGA[1024*544])
            case 85:
            default:
                rGetOSAVersion = E_EA_OSA_TYPE.OSA40;
                break;
            //2011.05.25 Update By SLC Zhoumiao ED
           
            //2016.03.09 Update By SLC Zhengwei
            //MX-3138NC For SESC
            case 94:
                rGetOSAVersion = E_EA_OSA_TYPE.OSA30;
                break;
            //2016.03.09 End
        }

        eOSAVersion =  rGetOSAVersion;
    }
    #endregion

    #region "SpecialMFPProcess"
    /// <summary>
    /// SpecialMFPProcess
    /// For the MX-M363 / M453 / M503 have Half-VGA and Wide-VGA
    ///  can not to devide by the product_family.
    /// It can be devide by the MFP Series Number.
    /// MX-M363U / M453U / M503U is Half-VGA
    /// MX-M363N / M453N / M503N is Wide-VGA
    /// </summary>
    /// <param name="request"></param>
    /// <Date>2010.12.8</Date>
    /// <Author>SES Jijianxiong</Author>
    /// <Version>1.1</Version>
    private void SpecialMFPProcess(HttpRequest request)
    {
        object DeviceId = request.Params["DeviceId"];
        if ( DeviceId == null ) {
            strMFPModel = "";
            return;
        }

        string strDeviceId = DeviceId.ToString();
        string strMid = "";

        if (strDeviceId.Contains(CON_MX_M363))
        {
            strMid = CON_MX_M363;
        }
        else if (strDeviceId.Contains(CON_MX_M453))
        {
            strMid = CON_MX_M453;
        }
        else if (strDeviceId.Contains(CON_MX_M503))
        {
            strMid = CON_MX_M503;
        }
        else
        {
            strMFPModel = "";
            return;
        }

        string strSpecial = strDeviceId.Substring(strDeviceId.IndexOf(strMid) + 1, 1);
        if (strSpecial.ToLower().Equals("u"))
        {
            strMFPModel = strSpecial + "U";
            bolIsMFPBrower = true;
            bolIsHtmlBrower = false;
        }
        else if (strSpecial.ToLower().Equals("n"))
        {
            strMFPModel = strSpecial + "N";
            bolIsMFPBrower = false;
            bolIsHtmlBrower = true;
        }
    }
    #endregion
}
