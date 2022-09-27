using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.IO;
using System.Xml;
using System.Management;
using System.Security.Cryptography;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SimpleEAregistration
{
    public partial class frmMain : Form
    {
        #region Load DLL
        [DllImport("LKC.dll", CharSet = CharSet.Auto)]
        private static extern int LKC_Check();
        [DllImport("LKC.dll", CharSet = CharSet.Auto)]
        public static extern unsafe void LKC_Format(int** ppiLength);
        [DllImport("LKC.dll", CharSet = CharSet.Auto)]
        public static extern int LKC_Register(string szKey);
        [DllImport("LKC.dll", CharSet = CharSet.Auto)]
        public static extern int LKC_Start();
        [DllImport("LKC.dll", CharSet = CharSet.Auto)]
        public static extern unsafe int LKC_Validate(string szKey, int* piLicenses, char* pcType);
        [DllImport("LKC.dll", CharSet = CharSet.Auto)]
        public static extern unsafe int LKC_ValidateEx(string szKey, int* piLicenses, char* pcType, char* productType);
        [DllImport("LKC.dll", CharSet = CharSet.Auto)]
        public static extern unsafe void LKC_Initiate(char programMode);
        [DllImport("LKC.dll", CharSet = CharSet.Auto)]
        public static extern unsafe int LKC_UnRegister();
        [DllImport("LKC.dll", CharSet = CharSet.Auto)]
        public static extern int LKC_GetKeyString(StringBuilder szKey);

        checkLKCErrorStr checkLKCErrorStr = new checkLKCErrorStr();
        #endregion

        #region 常量定义
        /// <summary>
        /// 常量定义
        /// </summary>
        //public string gstrfilename = "registration.dat";
        public string gstrfilename = "slca.ini";
        public int gSerialNumberLen = 20;
        #endregion

        #region 变量定义
        /// <summary>
        /// 变量定义
        /// </summary>
        public XmlDocument gXmlDoc = new XmlDocument();

        public ArrayList gSerialNumber = new ArrayList();
        public int gAuthorizationCNT = 0;
        public string gCompany = "";
        public string gAddress = "";
        public string gRegisterCode = "";
        public string gActiveCode = "";

        public string gActiveKEY = "0";
        #endregion


        public frmMain()
        {
            InitializeComponent();
        }

        private void frmSimpleEAReg_Load(object sender, EventArgs e)
        {
            try
            {
                //Check OS
                string sFullFilePath = "";
                sFullFilePath = GetOSBit().ToString();
                if (sFullFilePath == "32")
                {
                    gstrfilename = @"C:\Windows\System32\" + gstrfilename;
                }
                else
                {
                    gstrfilename = @"C:\Windows\SysWOW64\" + gstrfilename;
                }

                CheckRegistrationfile();
                FileDeSecurity();

                DataSet myDataSet = new DataSet();
                myDataSet.ReadXml(gstrfilename);

                XmlDocument doc = new XmlDocument();
                doc.Load(gstrfilename);
                XmlNodeList list = doc.DocumentElement.GetElementsByTagName("SerialNo");

                foreach (XmlNode node in list)
                {
                    gSerialNumber.Add(node.ChildNodes[0].InnerText);
                }

                for (int i = 0; i < myDataSet.Tables["RegistrationInfo"].Rows.Count; i++)
                {
                    gAuthorizationCNT = int.Parse(myDataSet.Tables["RegistrationInfo"].Rows[i][1].ToString());
                    gCompany = myDataSet.Tables["RegistrationInfo"].Rows[i][2].ToString();
                    gAddress = myDataSet.Tables["RegistrationInfo"].Rows[i][3].ToString();
                    gRegisterCode = myDataSet.Tables["RegistrationInfo"].Rows[i][4].ToString();
                    gActiveCode = myDataSet.Tables["RegistrationInfo"].Rows[i][5].ToString();
                }

                txtAuthorizationCNT.Text = gAuthorizationCNT.ToString();
                listboxSNlist.DataSource = gSerialNumber;
                txtCompanyName.Text = gCompany;
                txtAddress.Text = gAddress;
                richTxt_RegisterCode.Text = gRegisterCode;
                txtActiveCode.Text = gActiveCode;


                if (richTxt_RegisterCode.Text != "" && txtActiveCode.Text != "")
                {
                    string strDe = "";
                    string strDecToHex = "";
                    string strHexToString = "";

                    string strBase = "";
                    strBase = richTxt_RegisterCode.Text;

                    for (int i = 0; i < strBase.Length / 3; i++)
                    {
                        int bbb = int.Parse(strBase.Substring(i * 3, 3));
                        string aa = EnDeSecurity.EnDeSecurity.ConvertDecToHex(bbb, 16);
                        strDecToHex = strDecToHex + aa.ToUpper().ToString();
                    }

                    strHexToString = EnDeSecurity.EnDeSecurity.HexUnicodeToString(strDecToHex);
                    strDe = EnDeSecurity.EnDeSecurity.GetDecryptedValue(txtActiveCode.Text);

                    if (EnDeSecurity.EnDeSecurity.GetDecryptedValue(txtActiveCode.Text).Substring(2, 1) == "1"
                        && strHexToString.Substring(4, strHexToString.Length - 4) == strDe.Substring(4, strDe.Length - 4))
                    {
                        txtActiveCode.Enabled = false;
                        btnActivate.Enabled = false;
                    }
                }

                FileEnSecurity();
            }
            catch (Exception exLoad)
            {
                if (Utility.untCommon.WarningMsg("注册文件已损坏！", "　注册文件已损坏，\r\n\r\n　是否重新创建？"))
                {
                    exLoad.ToString();
                    //Utility.untCommon.WriteLog("注册文件已损坏", "原因：\r\n" + exLoad.ToString());

                    File.Delete(gstrfilename);

                    CheckRegistrationfile();
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;

                    this.Close();
                    this.Dispose();
                }
            }
        }

        // 添加“序列号”
        private unsafe void btnSNadd_Click(object sender, EventArgs e)
        {
            try
            {
                string strSerialNumber = txtSN.Text.ToUpper();
                bool strCHK_SN_Result = checkSerialNumber(strSerialNumber);

                if (strCHK_SN_Result)
                {
                    LKC_Initiate('Y');

                    int iLicenses = 0;
                    char pcType = '0';
                    char productType = '0';

                    int ret = LKC_ValidateEx(strSerialNumber, &iLicenses, &pcType, &productType);
                    string retStr = checkLKCErrorStr.lkcGetErrorString(ret);

                    gAuthorizationCNT = gAuthorizationCNT + iLicenses;
                    txtAuthorizationCNT.Text = gAuthorizationCNT.ToString();

                    if (retStr == "LKC_S_KEY_PURCHASED")
                    {
                        Utility.untCommon.InfoMsg("【序列号】添加成功！");
                        gSerialNumber.Add(strSerialNumber);

                        listboxSNlist.DataSource = null;
                        listboxSNlist.DataSource = gSerialNumber;
                        //listboxSNlist.Refresh();

                        txtSN.Focus();
                        txtSN.Clear();

                        btnActivate.Enabled = true;
                        txtActiveCode.Enabled = true;
                    }
                    else
                    {
                        Utility.untCommon.InfoMsg("序列号无效，请确认！");
                        txtSN.Focus();
                        txtSN.SelectAll();
                        return;
                    }
                }
                else
                {
                    txtSN.Focus();
                    txtSN.SelectAll();
                    return;
                }
            }
            catch (Exception exSNadd)
            {
                exSNadd.ToString();
                Utility.untCommon.ErrorMsg("序列号错误，请确认！");
                //Utility.untCommon.WriteLog("添加【序列号】失败", exSNadd.ToString());
            }
        }

        // 创建“生成码”
        private void btnBuildRegisterCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkPara01())
                {
                    return;
                }

                string strSTATUS = "000";
                string strRegisterCode = "";

                string strCPUID = GetCPUid();
                string strSNlist = "";

                for (int i = 0; i < gSerialNumber.Count; i++)
                {
                    strSNlist = strSNlist + gSerialNumber[i].ToString();
                }

                strRegisterCode = strSTATUS + "|" + strCPUID + "|" + gAuthorizationCNT + "|" + strSNlist;

                richTxt_RegisterCode.Text = EnDeSecurity.EnDeSecurity.StringToHexUnicode(strRegisterCode);

                string strBase = richTxt_RegisterCode.Text;
                string straaa = "";

                for (int i = 0; i < strBase.Length / 4; i++)
                {
                    string aa = EnDeSecurity.EnDeSecurity.ConvertHexToDec(strBase.Substring(i * 4, 4), 16);
                    straaa = straaa + aa.ToString();
                }

                richTxt_RegisterCode.Text = straaa;

                Utility.untCommon.InfoMsg("生成码创建成功。");


                FileDeSecurity();
                WriteRegistrationfile();
                FileEnSecurity();
            }
            catch (Exception exBuildRegisterCode)
            {
                exBuildRegisterCode.ToString();
                Utility.untCommon.ErrorMsg("创建失败", "可能原因：程序运行权限过低，无法写入数据。");
                //Utility.untCommon.WriteLog("创建生成码失败", "可能原因：\r\n" + exBuildRegisterCode.ToString());
            }
        }

        // 生成PDF文件
        private void btnBuildPDF_Click(object sender, EventArgs e)
        {
            //检查序列号、公司名、地址
            if (checkPara01() == true)
            {
                return;
            }
            //检查“生成码”是否存在
            if (checkPara02() == true)
            {
                return;
            }

            CreatePdf();
        }

        // 激活
        private void btnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkPara02())
                {
                    return;
                }

                string strRegisterCode = richTxt_RegisterCode.Text;
                string strActiveCode = txtActiveCode.Text;

                if (strActiveCode == "")
                {
                    Utility.untCommon.ErrorMsg("激活码不能为空，请确认！");
                    return;
                }

                string strDe = "";
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

                strDe = EnDeSecurity.EnDeSecurity.GetDecryptedValue(strActiveCode);

                gActiveKEY = strDe.Substring(2, 1);

                if (gActiveKEY == "1" &&
                    strHexToString.Substring(4, strHexToString.Length - 4) == strDe.Substring(4, strDe.Length - 4))
                {
                    Utility.untCommon.InfoMsg("注册成功");
                    btnActivate.Enabled = false;
                    txtActiveCode.Enabled = false;

                    FileDeSecurity();
                    WriteRegistrationfile();
                    FileEnSecurity();
                }
                else
                {
                    Utility.untCommon.ErrorMsg("激活码无效，请重新输入");
                }
            }
            catch (Exception exbtnActivate)
            {
                exbtnActivate.ToString();
                Utility.untCommon.ErrorMsg("激活码无效，请重新输入");
                //Utility.untCommon.WriteLog("激活码无效", "可能原因：\r\n" + exbtnActivate.ToString());
            }
        }

        // 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
            this.Dispose();
        }

        // 写注册文件
        private void WriteRegistrationfile()
        {
            try
            {
                gAuthorizationCNT = int.Parse(txtAuthorizationCNT.Text);

                gCompany = txtCompanyName.Text;
                gAddress = txtAddress.Text;

                gRegisterCode = richTxt_RegisterCode.Text;
                gActiveCode = txtActiveCode.Text;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(gstrfilename);
                XmlNode xnRoot = xmlDoc.SelectSingleNode("RegistrationSetting/RegistrationInfo");

                XmlNodeList nodelist = xnRoot.ChildNodes;

                foreach (XmlNode xn in nodelist)
                {
                    if (xn.Name == "SerialNumber")
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.RemoveAll();

                        for (int i = 0; i < gSerialNumber.Count; i++)
                        {
                            XmlNode xnSN = xn.SelectSingleNode(xn.Name);
                            XmlElement SN = xmlDoc.CreateElement("SerialNo");
                            SN.InnerText = gSerialNumber[i].ToString();
                            xn.AppendChild(SN);
                        }
                    }
                    if (xn.Name == "AuthorizationCNT")
                    {
                        xn.InnerText = gAuthorizationCNT.ToString();
                    }
                    if (xn.Name == "Company")
                    {
                        xn.InnerText = gCompany;
                    }
                    if (xn.Name == "Address")
                    {
                        xn.InnerText = gAddress;
                    }
                    if (xn.Name == "RegisterCode")
                    {
                        xn.InnerText = gRegisterCode;
                    }
                    if (xn.Name == "ActiveCode")
                    {
                        xn.InnerText = gActiveCode;
                    }
                }

                xmlDoc.Save(gstrfilename);
            }
            catch (Exception exWriteRegistrationfile)
            {
                exWriteRegistrationfile.ToString();
                Utility.untCommon.ErrorMsg("注册文件写入失败");
                //Utility.untCommon.WriteLog("写注册文件失败", "可能原因：\r\n" + exWriteRegistrationfile.ToString());
            }
        }

        #region 事务处理
        //1.数据检查

        // 检测Registration文件是否存在——不存在则自动创建
        private void CheckRegistrationfile()
        {
            if (!File.Exists(gstrfilename))
            {
                XmlTextWriter writer = new XmlTextWriter(gstrfilename, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;

                // start writing!
                writer.WriteStartDocument();
                writer.WriteStartElement("RegistrationSetting");

                // Creating the ＜RegistrationInfo＞ element
                writer.WriteStartElement("RegistrationInfo");

                writer.WriteStartElement("SerialNumber");
                writer.WriteEndElement();

                writer.WriteElementString("AuthorizationCNT", "0");

                writer.WriteElementString("Company", "");
                writer.WriteElementString("Address", "");

                writer.WriteElementString("RegisterCode", "");
                writer.WriteElementString("ActiveCode", "");

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();

                FileEnSecurity();
            }
        }

        // 校验“序列号”
        private bool checkSerialNumber(string strSN)
        {
            bool RtnResult;
            RtnResult = true;

            //0.序列号是否为空
            if (strSN == "")
            {
                Utility.untCommon.ErrorMsg("序列号不能为空，请确认！");
                RtnResult = false;
            }
            //1.序列号长度检查
            else if (strSN.Length != gSerialNumberLen)
            {
                Utility.untCommon.ErrorMsg("序列号长度不正确，请确认！");
                RtnResult = false;
            }

            //2.序列号是否已存在
            for (int i = 0; i < gSerialNumber.Count; i++)
            {
                if (strSN == gSerialNumber[i].ToString())
                {
                    Utility.untCommon.ErrorMsg("序列号已存在，请确认！");
                    RtnResult = false;
                }
            }

            return RtnResult;
        }

        //【创建“生成码”】——检查“序列号”
        private bool checkPara01()
        {
            bool RtnResult;
            RtnResult = false;

            if (gSerialNumber.Count == 0 || txtCompanyName.Text == "" || txtAddress.Text == "")
            {
                Utility.untCommon.InfoMsg("操作取消", "序列号、公司名或地址 必须填写！");
                RtnResult = true;
            }

            return RtnResult;
        }

        //【生成PDF文件】——检查“生成码”
        private bool checkPara02()
        {
            bool RtnResult;
            RtnResult = false;

            //检测“序列号”
            if (richTxt_RegisterCode.Text == "")
            {
                Utility.untCommon.InfoMsg("生成码尚未生成，请先“创建生成码”！");
                RtnResult = true;
            }

            return RtnResult;
        }

        //2.数据处理
        //
        private void FileEnSecurity()
        {
            EnDeSecurity.EncDecFile.EncryptFile(gstrfilename);
        }

        //
        private void FileDeSecurity()
        {
            EnDeSecurity.EncDecFile.DecryptFile(gstrfilename);
        }


        //==
        //获得CPU序列号
        static String GetCPUid()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();

            string StrCpuID = null;

            foreach (ManagementObject mo in moc)
            {
                StrCpuID = mo.Properties["ProcessorId"].Value.ToString();
                break;
            }
            return StrCpuID;
        }

        //使用MD5CryptoServiceProvider类长生哈希值
        static string MD5Hasher(string ssHashText)
        {
            byte[] MD5Data = System.Text.Encoding.UTF8.GetBytes(ssHashText);

            MD5 Md5 = new MD5CryptoServiceProvider();

            byte[] Result = Md5.ComputeHash(MD5Data);

            return Convert.ToBase64String(Result);
        }

        public static int GetOSBit()
        {
            try
            {
                string addressWidth = String.Empty;
                ConnectionOptions mConnOption = new ConnectionOptions();
                ManagementScope mMs = new ManagementScope(@"\\localhost", mConnOption);
                ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
                ManagementObjectCollection mObjectCollection = mSearcher.Get();
                foreach (ManagementObject mObject in mObjectCollection)
                {
                    addressWidth = mObject["AddressWidth"].ToString();
                }
                return Int32.Parse(addressWidth);
            }
            catch (Exception exGetOSBit)
            {
                exGetOSBit.ToString();
                //Utility.untCommon.WriteLog("GetOSBit失败", "可能原因：\r\n" + exGetOSBit.ToString());
                return 32;
            }
        }

        // 生成PDF文件
        private void CreatePdf()
        {
            try
            {
                string strYYYYMMDDms = String.Format("{0:yyyyMMdd}", DateTime.Now);
                string strFileName = string.Empty;

                System.Windows.Forms.SaveFileDialog dlg = new SaveFileDialog();
                strFileName = strYYYYMMDDms + "_" + "SimpleEA_RegisterCode.pdf";

                dlg.FileName = strFileName;
                dlg.DefaultExt = ".pdf";
                dlg.Filter = "Text documents (.pdf)|*.pdf";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //定义一个Document，并设置页面大小为A4，竖向 
                    iTextSharp.text.Document doc = new Document(PageSize.A4);

                    Document document = new Document();
                    PdfWriter.getInstance(document, new FileStream(dlg.FileName, FileMode.Create));

                    #region 设置PDF的头信息，一些属性设置，在Document.Open 之前完成
                    //document.AddAuthor("SESC");
                    //document.AddCreationDate();
                    //document.AddCreator("SESC");
                    //document.AddSubject("SimpleEA regtistration");
                    //document.AddTitle("This PDF file building by SESC solution");
                    //document.AddKeywords("");
                    ////自定义头 
                    //document.AddHeader("Expires", "0");
                    #endregion //打开document

                    document.Open();

                    //载入字体
                    //Font黑体
                    BaseFont bfHei = BaseFont.createFont(@"c:\windows\fonts\SIMHEI.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    //Font宋体
                    BaseFont bfSun = BaseFont.createFont(@"c:\windows\fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//宋体

                    iTextSharp.text.Font font = new iTextSharp.text.Font(bfHei, 24, 1);
                    iTextSharp.text.Font fontNote = new iTextSharp.text.Font(bfSun, 10, 1);

                    iTextSharp.text.Font fontbody0A = new iTextSharp.text.Font(bfHei, 14, 1);   // 标准字体
                    iTextSharp.text.Font fontbody00 = new iTextSharp.text.Font(bfHei, 10, 0);   // 标准字体
                    iTextSharp.text.Font fontbody01 = new iTextSharp.text.Font(bfHei, 10, 1);   // 粗体
                    iTextSharp.text.Font fontbody02 = new iTextSharp.text.Font(bfHei, 10, 2);   // 斜体
                    iTextSharp.text.Font fontbody03 = new iTextSharp.text.Font(bfHei, 12, 3);   // 粗斜体
                    iTextSharp.text.Font fontbody04 = new iTextSharp.text.Font(bfHei, 10, 4);   // 标准字体 + 下划线
                    iTextSharp.text.Font fontbody05 = new iTextSharp.text.Font(bfHei, 10, 5);   // 粗体 + 下划线
                    iTextSharp.text.Font fontbody06 = new iTextSharp.text.Font(bfHei, 10, 6);   // 斜体 + 下划线
                    iTextSharp.text.Font fontbody07 = new iTextSharp.text.Font(bfHei, 10, 7);   // 粗斜体 + 下划线

                    iTextSharp.text.Font fontbody08 = new iTextSharp.text.Font(bfHei, 10, 8);   // 标准字体 + 删除线
                    iTextSharp.text.Font fontbody09 = new iTextSharp.text.Font(bfHei, 10, 9);   // 粗体 + 删除线
                    iTextSharp.text.Font fontbody10 = new iTextSharp.text.Font(bfHei, 10, 10);  // 斜体 + 删除线

                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph("　　　　　SimpleEA 用户注册申请信息", font));

                    DateTime dt = DateTime.Now;
                    string strYMD = dt.ToShortDateString().ToString();
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph("　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　申请日期：" + strYMD, fontbody00));

                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph("公司名：　" + "" + this.txtCompanyName.Text.ToString(), fontbody0A));
                    document.Add(new Paragraph("地　址：　" + "" + this.txtAddress.Text.ToString(), fontbody0A));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));

                    int intAccessCNT = gSerialNumber.Count;
                    for (int i = 0; i < intAccessCNT; i++)
                    {
                        if (i == 0)
                        {
                            document.Add(new Paragraph("序列号：　" + "" + gSerialNumber[i].ToString(), fontbody00));
                        }
                        else
                        {
                            document.Add(new Paragraph("　　　　　" + "" + gSerialNumber[i].ToString(), fontbody00));
                        }
                    }

                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph("授权数：" + this.txtAuthorizationCNT.Text.ToString(), fontbody03));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph("生成码：", fontbody01));
                    document.Add(new Paragraph(this.richTxt_RegisterCode.Text.ToString(), fontbody07));

                    //小于10条目的，自动补空
                    if (intAccessCNT < 10)
                    {
                        for (int i = 0; i < 10 - intAccessCNT; i++)
                        {
                            document.Add(new Paragraph(" "));
                        }
                    }

                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));

                    document.Add(new Paragraph("注：生成PDF文件后，请将附件发送至【夏普商贸解决方案部】，我们将为您提供“激活码”。", fontNote));
                    document.Add(new Paragraph("　　电话：(86)021-6104888-2804　传真：(86)021-61066015-2804　邮箱：solution@cn.sharp-world.com", fontNote));
                    document.Add(new Paragraph(" "));

                    //close document
                    document.Close();

                    Utility.untCommon.InfoMsg("　PDF文件生成完毕，请及时将附件发送至【夏普商贸解决方案部】。\r\n\r\n　邮箱地址：solution@cn.sharp-world.com\r\n\r\n\r\n　谢谢！");
                }
            }
            catch (DocumentException de)
            {
                de.ToString();
                Utility.untCommon.ErrorMsg("生成PDF时发生文档错误");
                //Utility.untCommon.WriteLog("生成PDF时发生文档错误", "原因如下：\r\n" + de.ToString());
            }
            catch (IOException ioe)
            {
                ioe.ToString();
                Utility.untCommon.ErrorMsg("生成PDF时发生IO错误");
                //Utility.untCommon.WriteLog("生成PDF时发生IO错误", "原因如下：\r\n" + ioe.ToString());
            }
        }
        #endregion
    }

    public class checkLKCErrorStr
    {
        private const int LKC_RESULT = 1000;
        private const int LKC_S_OK = ((LKC_RESULT) + 1);
        private const int LKC_S_KEY_PURCHASED = ((LKC_RESULT) + 2);
        private const int LKC_S_KEY_EVALUATION = ((LKC_RESULT) + 3);
        private const int LKC_S_EXPIRED = ((LKC_RESULT) + 4);
        // Definition of Error Codes
        private const int LKC_ERROR = 2000;
        private const int LKC_E_NO_MEMORY = ((LKC_ERROR) + 1);
        private const int LKC_E_INVALID_PARAMS = ((LKC_ERROR) + 2);
        private const int LKC_E_NO_PERMISSION = ((LKC_ERROR) + 3);
        private const int LKC_E_NO_REGISTERED_KEY = ((LKC_ERROR) + 4);
        private const int LKC_E_KEY_INVALID = ((LKC_ERROR) + 5);
        private const int LKC_E_ALREADY_IN_EVALUATION = ((LKC_ERROR) + 6);

        public string lkcGetErrorString(int errorNo)
        {
            switch (errorNo)
            {
                case LKC_RESULT:
                    return "LKC_RESULT";
                case LKC_S_OK:
                    return "LKC_S_OK";
                case LKC_S_KEY_PURCHASED:
                    return "LKC_S_KEY_PURCHASED";
                case LKC_S_KEY_EVALUATION:
                    return "LKC_S_KEY_EVALUATION";
                case LKC_S_EXPIRED:
                    return "LKC_S_EXPIRED";
                case LKC_ERROR:
                    return "LKC_ERROR";
                case LKC_E_NO_MEMORY:
                    return "LKC_E_NO_MEMORY";
                case LKC_E_INVALID_PARAMS:
                    return "LKC_E_INVALID_PARAMS";
                case LKC_E_NO_PERMISSION:
                    return "LKC_E_NO_PERMISSION";
                case LKC_E_NO_REGISTERED_KEY:
                    return "LKC_E_NO_REGISTERED_KEY";
                case LKC_E_KEY_INVALID:
                    return "LKC_E_KEY_INVALID";
                case LKC_E_ALREADY_IN_EVALUATION:
                    return "LKC_E_ALREADY_IN_EVALUATION";
                default:
                    return "LKC_RESULT";
            }
        }
    }

}
