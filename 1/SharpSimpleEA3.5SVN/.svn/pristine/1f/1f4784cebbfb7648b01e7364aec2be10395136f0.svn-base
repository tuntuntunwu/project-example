using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

//using System.Runtime.InteropServices;
/// <summary>
///LKCclass 的摘要说明
/// </summary>
public class LKCclass
{
    //ADD BY SLC LIJING 2011.10.26 ST
    [DllImport("LKC.dll", CharSet = CharSet.Auto)]
    public static extern int LKC_Check();
    [DllImport("LKC.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LKC_Register(string szKey);
    [DllImport("LKC.dll", CharSet = CharSet.Auto)]
    public static extern int LKC_Start();
    [DllImport("LKC.dll", CharSet = CharSet.Auto)]
    public static extern int LKC_UnRegister();
    [DllImport("LKC.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void LKC_Initiate(Char programMode);

    public enum ResultCode { LKC_RESULT = 1000, LKC_S_OK, LKC_S_KEY_PURCHASED, LKC_S_KEY_EVALUATION, LKC_S_EXPIRED };
    public enum ErrorCode { LKC_ERROR = 2000, LKC_E_NO_MEMORY, LKC_E_INVALID_PARAMS, LKC_E_NO_PERMISSION, LKC_E_NO_REGISTERED_KEY, LKC_E_KEY_INVALID, LKC_E_ALREADY_IN_EVALUATION };
    public const int ExpDays = 30;

    public LKCclass()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

}