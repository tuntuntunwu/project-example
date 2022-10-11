using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EAED;
using EnDeSecurity;
namespace SLCRegister
{
    public class RegisterHandler
    {
        private static string TypeID;
        //private static string sysFolder = "D:\\";
        private static String sysFolder = System.Environment.SystemDirectory;
        //private static String sysFolder = "C:\\";
        //2014-7-1 pupeng 
        private static string sysFolder64 = "";
        private static DirectoryInfo di;
        //2014-7-1 pupeng 

        //20140718 add st
        public RegisterHandler()
        {
            //2014-7-1 pupeng 
            sysFolder = System.Environment.SystemDirectory;
            di = new DirectoryInfo(sysFolder);
            sysFolder64 = Path.Combine(di.Parent.FullName.ToString(), "SysWOW64");
            if (Directory.Exists(sysFolder64))
            {
                if (File.Exists(sysFolder64 + "\\slca.ini"))
                {
                    sysFolder = sysFolder64;
                }

            }
            //2014-7-1 pupeng 
        }
        //20140718 add ed

        public static void Initiate(string type)
        {
            TypeID = type;
            //2014-7-1 pupeng 
            sysFolder = System.Environment.SystemDirectory;
            di = new DirectoryInfo(sysFolder);
            sysFolder64 = Path.Combine(di.Parent.FullName.ToString(), "SysWOW64");
            if (Directory.Exists(sysFolder64))
            {
                if (File.Exists(sysFolder64 + "\\slca.ini"))
                {
                    sysFolder = sysFolder64;
                }

            }
            //2014-7-1 pupeng 
        }

        public static KeyStatus Check()
        {
            /*

            if (!File.Exists(sysFolder + "\\slca.ini"))
            {
                return KeyStatus.NotRegister;
            }
            else
            {//如果注册文件存在，读取文件内容跟密码比较
                string strContent = string.Empty;
                try
                {
                    using(FileStream fs = new FileStream(sysFolder + "\\slca.ini", FileMode.Open, FileAccess.Read))
                    {
                        int BufferLength = 10;
                        byte[] b = new byte[BufferLength];
                        int i;
                        while ((i = fs.Read(b, 0, BufferLength)) > 0)
                        {
                            //strContent += System.Text.Encoding.Default.GetString(b, 0, i);
                            strContent += System.Text.Encoding.ASCII.GetString(b, 0, i);
                            b = new byte[BufferLength];
                        }

                        fs.Close();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                strContent = AlgorithmFacade.Decrypt(strContent, TypeID);
                string[] strArray = strContent.Split('#');

                //if (strArray[0] == Helper.GetCPUSN())
                //{
                    //一旦检测到过期，则无论是现在时间是几天，都不可以使用
                    if (strArray[3].Equals("1"))
                        return KeyStatus.outTrial;
                    else
                        if (strArray[1].Equals("9999-9-9"))
                            return KeyStatus.Forerver;
                        else
                            if (DateTime.Compare(Convert.ToDateTime(strArray[1]), DateTime.Now) > 0)
                                return KeyStatus.inTrial;
                            else
                                if (DateTime.Compare(Convert.ToDateTime(strArray[1]), DateTime.Now) < 0)
                                    return KeyStatus.outTrial;
                                else
                                    return KeyStatus.NotRegister;
                //}
                //else
                //{
                //    return KeyStatus.NotRegister;
                //}
            }
            */

            string gstrfilename = sysFolder + "\\slca.ini";
            if (!File.Exists(gstrfilename))
            {
                return KeyStatus.NotRegister;
            }
            string strActiveCodeString = EAED.EAEDSecurity.GetEnString(gstrfilename);
            string activecodeHeader = strActiveCodeString.Substring(0, 3);
            string[] strArray = strActiveCodeString.Split('|');

            //正确“  strActiveCodeString(0:3) = “101” 
            //错误①：strActiveCodeString(0:3) = “100”                    UNACTIVATED
            //错误②：strActiveCodeString(0:3) = “201”                    FILE NOT EXIST
            //错误③：strActiveCodeString(0:3) = “202”                    FILE FORMAT ERROR
            //错误④：strActiveCodeString(0:3) = “203”                    ACTIVATE CODE ERROR
            if (activecodeHeader != "101")
            {
                return KeyStatus.NotRegister;
            }

            //CPU SN check
            if (strArray[1] != Helper.GetCPUSN())
            {
                return KeyStatus.NotRegister;
            }

            //check MFP count
            int count = Convert.ToInt32(strArray[2]);
            if (count <= 0)
            {
                return KeyStatus.NotRegister;
            }
            return KeyStatus.Forerver;
        }

        public static int GetDeadline()
        {
            // 2012.09.063 delete by wei changye
            //if (!File.Exists(sysFolder + "\\slca.ini"))
            //{
            //    return -1;
            //}
            //else
            //{//如果注册文件存在，读取文件内容跟密码比较
            //    string strContent = string.Empty;

            //    try
            //    {
            //        using (FileStream fs = new FileStream(sysFolder + "\\slca.ini", FileMode.Open, FileAccess.Read))
            //        {
            //            int BufferLength = 10;
            //            byte[] b = new byte[BufferLength];
            //            int i;
            //            while ((i = fs.Read(b, 0, BufferLength)) > 0)
            //            {
            //                strContent += System.Text.Encoding.Default.GetString(b, 0, i);
            //                b = new byte[BufferLength];
            //            }
            //            fs.Close();
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        throw e;
            //    }

            //    strContent = AlgorithmFacade.Decrypt(strContent, TypeID);
            //    string[] strArray = strContent.Split('#');
                //if (strArray[0] == Helper.GetCPUSN())
                //{
                    //DateTime deadLineDate = GetDeadlineDate();
                    //TimeSpan t1 = new TimeSpan(DateTime.Now.Ticks);
                    //TimeSpan t2 = new TimeSpan(Convert.ToDateTime(strArray[1]).Ticks);

                    //TimeSpan ts = t1.Subtract(t2).Duration();
                    //if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(strArray[1])) > 0)
                    //    return -ts.Days;
                    //else
                    //    return ts.Days;
                //}
            //}
            //return -1;

            // end modify

            DateTime deadLineDate = GetDeadlineDate();
            TimeSpan t1 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan t2 = new TimeSpan(Convert.ToDateTime(deadLineDate).Ticks);

            TimeSpan ts = t1.Subtract(t2).Duration();
            if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(deadLineDate)) > 0)
                return -ts.Days;
            else
                return ts.Days;
        }

        public static DateTime GetDeadlineDate()
        {
            //if (!File.Exists(sysFolder + "\\slca.ini"))
            //{
            //    return new DateTime(1900, 1, 1);
            //}
            //else
            //{//如果注册文件存在，读取文件内容跟密码比较
            //    string strContent = string.Empty;

            //    try
            //    {
            //        using (FileStream fs = new FileStream(sysFolder + "\\slca.ini", FileMode.Open, FileAccess.Read))
            //        {
            //            int BufferLength = 10;
            //            byte[] b = new byte[BufferLength];
            //            int i;
            //            while ((i = fs.Read(b, 0, BufferLength)) > 0)
            //            {
            //               // strContent += System.Text.Encoding.Default.GetString(b, 0, i);
            //                strContent += System.Text.Encoding.ASCII.GetString(b, 0, i);
            //                b = new byte[BufferLength];
            //            }
            //            fs.Close();
            //        }

            //        strContent = AlgorithmFacade.Decrypt(strContent, TypeID);
            //        string[] strArray = strContent.Split('#');
            //        //if (strArray[0] == Helper.GetCPUSN())
            //        //{
            //            return Convert.ToDateTime(strArray[1]);
            //        //}
            //    }
            //    catch (Exception e)
            //    {
            //        return Convert.ToDateTime("0000-00-00");
            //        throw e;
            //    }

            //}
            return Convert.ToDateTime("5000-01-01");
        }

        public static int GetOperateCount(out string path)
        {
            //path = sysFolder + "\\slca.ini";
            //if (!File.Exists(sysFolder + "\\slca.ini"))
            //{
            //    return -1;
            //}
            //else
            //{//如果注册文件存在，读取文件内容跟密码比较
            //    string strContent = string.Empty;

            //    try
            //    {
            //        using (FileStream fs = new FileStream(sysFolder + "\\slca.ini", FileMode.Open, FileAccess.Read))
            //        {
            //            int BufferLength = 10;
            //            byte[] b = new byte[BufferLength];
            //            int i;
            //            while ((i = fs.Read(b, 0, BufferLength)) > 0)
            //            {
            //               // strContent += System.Text.Encoding.Default.GetString(b, 0, i);
            //                strContent += System.Text.Encoding.ASCII.GetString(b, 0, i);
            //                b = new byte[BufferLength];
            //            }
            //            fs.Close();
            //        }
            //        strContent = AlgorithmFacade.Decrypt(strContent, TypeID);
            //        string[] strArray = strContent.Split('#');
            //        //if (strArray[0] == Helper.GetCPUSN())
            //        //{
            //            return Convert.ToInt32(strArray[2]);
            //        //}
            //    }
            //    catch (Exception e)
            //    {
            //        return -1;
            //        throw e;
            //    }
            //}
            //return -1;

            path = sysFolder + "\\slca.ini";
            string gstrfilename = sysFolder + "\\slca.ini";
            if (!File.Exists(gstrfilename))
            {
                return -1;
            }
            string strActiveCodeString = EAED.EAEDSecurity.GetEnString(gstrfilename);
            string activecodeHeader = strActiveCodeString.Substring(0, 3);
            string[] strArray = strActiveCodeString.Split('|');

            //正确“  strActiveCodeString(0:3) = “101” 
            //错误①：strActiveCodeString(0:3) = “100”                    UNACTIVATED
            //错误②：strActiveCodeString(0:3) = “201”                    FILE NOT EXIST
            //错误③：strActiveCodeString(0:3) = “202”                    FILE FORMAT ERROR
            //错误④：strActiveCodeString(0:3) = “203”                    ACTIVATE CODE ERROR

            if (activecodeHeader != "101")
            {
                return -1;
            }

            //CPU SN check
            if (strArray[1] != Helper.GetCPUSN())
            {
                return -1;
            }

            return Convert.ToInt32(strArray[2]);
        }

        /// <summary>
        /// format:CPU Serial#DateTime#Machine Count#Is Out Trail
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static RegisterResult Register(string key)
        {
            try
            {
                if (TypeID == null)
                {
                    throw new Exception();
                }
                else
                {
                    //防止未加密字段直接输入，导致验证成功
                    //if (key.Equals(Helper.GetCPUSN()))
                    //    return RegisterResult.fail;

                    string strContent = AlgorithmFacade.Decrypt(key, TypeID).Trim();
                    string[] strArray = strContent.Split('#');


                    if (strArray[0].Trim() == Helper.GetCPUSN().Trim())
                    {
                        File.WriteAllText(sysFolder + @"\slca.ini", key);
                        return RegisterResult.success;
                    }
                    else
                        return RegisterResult.fail;
                }
            }
            catch (Exception e)
            {
                return RegisterResult.fail;
            }

        }
  
        /*

        public static void SetOutTrail()
        {
            if (!File.Exists(sysFolder + "\\slca.ini"))
            {
                return;
            }
            else
            {//如果注册文件存在，读取文件内容跟密码比较
                string strContent = string.Empty;

                try
                {
                    using (FileStream fs = new FileStream(sysFolder + "\\slca.ini", FileMode.Open, FileAccess.Read))
                    {
                        int BufferLength = 10;
                        byte[] b = new byte[BufferLength];
                        int i;
                        while ((i = fs.Read(b, 0, BufferLength)) > 0)
                        {
                            //strContent += System.Text.Encoding.Default.GetString(b, 0, i);
                            strContent += System.Text.Encoding.ASCII.GetString(b, 0, i);
                            b = new byte[BufferLength];
                        }
                        fs.Close();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                strContent = AlgorithmFacade.Decrypt(strContent, TypeID);
                string[] strArray = strContent.Split('#');
                //if (strArray[0] == Helper.GetCPUSN())
                //{
                strArray[3] = "1";
                string key = strArray[0] + "#" + strArray[1] + "#" + strArray[2] + "#" + strArray[3];
                key = AlgorithmFacade.Encrypt(key, TypeID);
                Register(key);
                //}
            }
        }
        */
        public static string GetActiveSN(string registerKey)
        {
            return AlgorithmFacade.Encrypt(registerKey, TypeID);
        }
        

    }
}
