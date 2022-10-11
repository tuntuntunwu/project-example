using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace EnDeSecurity
{
    public class EnDeSecurity
    {
        #region 常量定义
        private static readonly string IV = "SuFjcEmp/TE=";
        private static readonly string Key = "KIPSToILGp6fl+3gXJvMsN4IajizYBBT";
        #endregion

        /// <summary>
        /// 获取加密后的字符串
        /// </summary>
        /// <param name="inputValue">输入值.</param>
        /// <returns></returns>
        public static string GetEncryptedValue(string inputValue)
        {
            TripleDESCryptoServiceProvider provider = GetCryptoProvider();

            // 创建内存流来保存加密后的流
            MemoryStream mStream = new MemoryStream();

            // 创建加密转换流
            CryptoStream cStream = new CryptoStream(mStream,
            provider.CreateEncryptor(), CryptoStreamMode.Write);

            // 使用UTF8编码获取输入字符串的字节。
            byte[] toEncrypt = new UTF8Encoding().GetBytes(inputValue);

            // 将字节写到转换流里面去。
            cStream.Write(toEncrypt, 0, toEncrypt.Length);
            cStream.FlushFinalBlock();

            // 在调用转换流的FlushFinalBlock方法后，内部就会进行转换了,此时mStream就是加密后的流了。
            byte[] ret = mStream.ToArray();

            // Close the streams.
            cStream.Close();
            mStream.Close();

            //将加密后的字节进行64编码。
            return Convert.ToBase64String(ret);
        }
        /// <summary>
        /// 获取解密后的值
        /// </summary>
        /// <param name="inputValue">经过加密后的字符串.</param>
        /// <returns></returns>
        public static string GetDecryptedValue(string inputValue)
        {
            TripleDESCryptoServiceProvider provider = GetCryptoProvider();

            byte[] inputEquivalent = Convert.FromBase64String(inputValue);

            // 创建内存流保存解密后的数据
            MemoryStream msDecrypt = new MemoryStream();

            // 创建转换流。
            CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                                                        provider.CreateDecryptor(),
                                                        CryptoStreamMode.Write);

            csDecrypt.Write(inputEquivalent, 0, inputEquivalent.Length);

            csDecrypt.FlushFinalBlock();
            csDecrypt.Close();

            //获取字符串。
            return new UTF8Encoding().GetString(msDecrypt.ToArray());
        }
        /// <summary>
        /// 获取加密服务类
        /// </summary>
        /// <returns></returns>
        private static TripleDESCryptoServiceProvider GetCryptoProvider()
        {
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();

            provider.IV = Convert.FromBase64String(IV);
            provider.Key = Convert.FromBase64String(Key);

            return provider;
        }


        /// <summary>
        /// <函数：StringToHexUnicode>
        /// 作用：将字符串内容转化为16进制数据编码，其逆过程是Decode
        /// 参数说明：
        /// strEncode 需要转化的原始字符串
        /// 转换的过程是直接把字符转换成Unicode字符,比如数字"3"-->0033,汉字"我"-->U+6211
        /// 函数decode的过程是encode的逆过程.
        /// </summary>
        /// <param name="strEncode"></param>
        /// <returns></returns>
        public static string StringToHexUnicode(string strEncode)
        {
            string strReturn = "";//  存储转换后的编码
            foreach (short shortx in strEncode.ToCharArray())
            {
                strReturn += shortx.ToString("X4");
            }
            return strReturn;
        }
        /// <summary>
        /// <函数：HexUnicodeToString>
        ///作用：将16进制数据编码转化为字符串源码天空，是Encode的逆过程
        /// </summary>
        /// <param name="strDecode"></param>
        /// <returns></returns>
        public static string HexUnicodeToString(string strDecode)
        {
            string sResult = "";
            for (int i = 0; i < strDecode.Length / 4; i++)
            {
                sResult += (char)short.Parse(strDecode.Substring(i * 4, 4), global::System.Globalization.NumberStyles.HexNumber);
            }
            return sResult;
        }
        /// <summary>
        /// 字符串转十六进制（带%分号）
        /// </summary>
        /// <param name="inputValue">输入值.</param>
        /// <param name="encode">Encoding.UTF8</param>
        /// <returns></returns>
        public static string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += "%" + Convert.ToString(b[i], 16);
            }
            return result;
        }
        /// <summary>
        /// 十六进制（带%分号）转字符串
        /// </summary>
        /// <param name="inputValue">输入值.</param>
        /// <param name="encode">Encoding.UTF8</param>
        /// <returns></returns>
        public static string HexStringToString(string hs, Encoding encode)
        {
            //以%分割字符串，并去掉空字符
            string[] chars = hs.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] b = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                b[i] = Convert.ToByte(chars[i], 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }

        /// <summary>
        /// 字符串转二进制数字（0101）
        /// </summary>
        /// <param name="inputValue">输入值.</param>
        /// <param name="strEncode">UTF-8或其他</param>
        /// <returns></returns>
        public static string StringToBinary(string s, String strEncode)
        {
            string str = s;
            byte[] byteArr = Encoding.Unicode.GetBytes(str);
            string binStr = "";
            string tempStr = "";
            int length = 0;
            //转成二进制            
            for (int i = 0; i < byteArr.Length; i++)
            {
                tempStr = Convert.ToString(byteArr[i], 2);
                length = tempStr.Length;
                for (int j = 0; j < 8 - length; j++)
                {
                    tempStr = "0" + tempStr;
                }
                binStr += tempStr;
            }

            return (binStr);
        }
        /// <summary>
        /// 二进制数字（0101）转字符串
        /// </summary>
        /// <param name="inputValue">输入值.</param>
        /// <param name="strDecode">UTF-8或其他</param>
        /// <returns></returns>
        public static string BinaryToString(string binString,String strDecode)
        {
            string binStr = binString;
            byte[] byteArr = Encoding.Unicode.GetBytes(binStr);
            //转成字符串
            byte[] newByteArr = new byte[binStr.Length / 8];
            for (int i = 0; i < binStr.Length / 8; i++)
            {
                newByteArr[i] = (byte)Convert.ToInt32(binStr.Substring(i * 8, 8), 2);
            }

            return (Encoding.Unicode.GetString(newByteArr));
        }

        //十六进制转十进制
        public static string ConvertHexToDec(string value, int toBase)
        {
            //Console.WriteLine(Convert.ToInt32("FF", 16)); 
            return Convert.ToInt32(value, toBase).ToString().PadLeft(3, '0');
        }

        //十进制转十六进制
        public static string ConvertDecToHex(int value, int toBase)
        {
            //Console.WriteLine(Convert.ToString(69, 16)); 
            return Convert.ToString(value, toBase).ToString().PadLeft(4, '0');
        }

        ///
        /// //十进制转二进制
        /// Console.WriteLine("十进制166的二进制表示: "+Convert.ToString(166, 2));
        /// //十进制转八进制
        /// Console.WriteLine("十进制166的八进制表示: "+Convert.ToString(166, 8));
        /// //十进制转十六进制
        /// Console.WriteLine("十进制166的十六进制表示: "+Convert.ToString(166, 16));

        /// //二进制转十进制
        /// Console.WriteLine("二进制 111101 的十进制表示: "+Convert.ToInt32("111101", 2));
        /// //八进制转十进制
        /// Console.WriteLine("八进制 44 的十进制表示: "+Convert.ToInt32("44", 8));
        /// //十六进制转十进制
        /// Console.WriteLine("十六进制 CC的十进制表示: "+Convert.ToInt32("CC", 16)); 
        ///

    }
}
