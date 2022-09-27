using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace SLCRegister
{
    public class Helper
    {
        //因为作为DLL使用，无法使用XML进行关系映射，只能直接写在程序中
        public static string GetXmlAlgorithm(string typeID)
        {
            if (typeID.Equals("A"))
                return "DESAlgorithm";
            else
                return "";
        }

        public static string GetCPUSN()
        {
            //string strCpu = null;
            //ManagementClass myCpu = new ManagementClass("win32_Processor");
            //ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            //foreach (ManagementObject myObject in myCpuConnection)
            //{
            //    strCpu = myObject.Properties["Processorid"].Value.ToString();
            //    break;
            //}
            //return StringToByte(strCpu);
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

        #region StringToByte

        /// <summary>
        /// StringToByte
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <Date>2012.02.23</Date>
        /// <Author>SLC Wei changye</Author>
        /// <Version>1.2</Version>
        public static string StringToByte(string str)
        {
            string value = string.Empty;
            if (string.IsNullOrEmpty(str))
            {
                return value;
            }

            UnicodeEncoding uni = new UnicodeEncoding();

            byte[] tmp = uni.GetBytes(str);
            StringBuilder sb = new StringBuilder();

            foreach (byte item in tmp)
            {
                sb.Append(item.ToString());
            }
            string folder = sb.ToString();

            return folder;
        }

        #endregion 
    }
}
