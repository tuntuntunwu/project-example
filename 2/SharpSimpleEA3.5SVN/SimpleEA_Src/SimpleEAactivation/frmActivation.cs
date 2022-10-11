using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SimpleEAactivation
{
    public partial class frmActivation : Form
    {
        public frmActivation()
        {
            InitializeComponent();
        }

        private void frmActivation_Load(object sender, EventArgs e)
        {

        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                bool CHKpara = chkParaCode();
                
                if (CHKpara)
                {
                    string strRegisterCode = richTxt_RegisterCode.Text;

                    string strEn = "";
                    string strDecToHex = "";
                    string strHexToString = "";

                    string strBase = "";
                    strBase = strRegisterCode;

                    for (int i = 0; i < strBase.Length / 3; i++)
                    {
                        int bbb = int.Parse(strBase.Substring(i * 3, 3));
                        string aa = EnDeSecurity.EnDeSecurity.ConvertDecToHex(bbb, 16);
                        strDecToHex = strDecToHex + aa.ToUpper().ToString();
                    }


                    strHexToString = EnDeSecurity.EnDeSecurity.HexUnicodeToString(strDecToHex);

                    string[] attr = strHexToString.Split('|');
                    attr[0] = attr[2].PadLeft(2, '0') + "1";

                    int iLen = strHexToString.Length-3;
                    strHexToString = attr[0] + strHexToString.Substring(3, iLen);
                    richTxtRegisterKEY.Text = strHexToString;


                    strEn = EnDeSecurity.EnDeSecurity.GetEncryptedValue(strHexToString);

                    richTxt_ActivationCode.Text = strEn;

                    Utility.untCommon.InfoMsg("激活码已生成，请及时激活，并妥善保存！");

                    // save register information
                    string strYYYYMMDDms = String.Format("{0:yyyyMMdd}", DateTime.Now);

                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(strYYYYMMDDms + "_SimpleEA_activation.txt", true))
                    {
                        sw.WriteLine("＝＝＝＝＝＝＝＝＝＝Simple EA 客户激活信息＝＝＝＝＝＝＝＝＝＝");
                        sw.WriteLine("Start Date:" + DateTime.Now.ToString());
                        sw.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - -");
                        sw.WriteLine("客户名称：" + txtCustomer.Text );
                        sw.WriteLine("地址：" + txtAddress.Text );
                        sw.WriteLine("所属经销商：" + txtDealer.Text );
                        sw.WriteLine("－－－－－－－－－－－－－－－－－－－－－－－－－－");
                        sw.WriteLine("生成码：" + richTxt_RegisterCode.Text );
                        sw.WriteLine("激活码：" + richTxt_ActivationCode.Text );
                        sw.WriteLine("－－－－－－－－－－－－－－－－－－－－－－－－－－");
                        sw.WriteLine("注册码：" + richTxtRegisterKEY.Text);
                        sw.WriteLine("＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝");
                        sw.WriteLine("");
                        sw.WriteLine("");
                    }
                }

            }
            catch(Exception exActive)
            {
                //exActive.ToString();
                Utility.untCommon.ErrorMsg("生成码不正确，无法创建“激活码”。");

                Utility.untCommon.WriteLog("激活码出错","原因如下：\r\n" + exActive.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
            this.Dispose();
        }

        private bool chkParaCode()
        {
            bool RTNcheckResult;
            RTNcheckResult = false;

            if (richTxt_RegisterCode.Text == "")
            {
                Utility.untCommon.ErrorMsg("生成码不能为空，请重新输入！");
                richTxt_RegisterCode.Focus();
                return RTNcheckResult;
            }

            RTNcheckResult = true;
            return RTNcheckResult;
        }


    }
}
