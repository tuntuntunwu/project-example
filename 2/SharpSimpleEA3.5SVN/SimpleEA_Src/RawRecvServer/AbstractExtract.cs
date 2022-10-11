using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;

namespace FollowMEService
{
    /// <summary>
    ///AbstractExtract 的摘要说明 
    ///Use Template Pattern 
    /// </summary>
    public abstract class AbstractExtract
    {
        public AbstractExtract()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public PrintParaModel para
        {
            get;
            protected set;
        }
        public string LanChange(string str)
        {
            Encoding utf8;
            Encoding gb2312;
            utf8 = Encoding.GetEncoding("UTF-8");
            gb2312 = Encoding.GetEncoding("GB2312");
            byte[] gb = gb2312.GetBytes(str);
            gb = Encoding.Convert(gb2312, utf8, gb);
            return utf8.GetString(gb);
        } 
        public PrintParaModel ExtractInfo(byte[] b)
        {
            string str = Encoding.Default.GetString(b);
            string[] array = str.Split('\n');
            para = new PrintParaModel();

            para.FileName = Extract("JOB NAME", array);
            //2019-03-20  add start
            // 苹果电脑打印时，苹果spl没有@PJL SET JOBNAME参数
            // 苹果的参数是@PJL SET JOB NAMEW
            //if (para.FileName == null || para.FileName == " " || para.FileName.Trim() == "")
            //{
            //    string strFileName = Extract("JOBNAME", array);
            //    para.FileName = LanChange(strFileName);
            //}
            if (para.FileName == null || para.FileName == " " || para.FileName.Trim() == "")
            {
                    string str2 = Encoding.UTF8.GetString(b);
                    string[] array2 = str2.Split('\n');
                    string strFileName = Extract("JOBNAMEW", array2);
                    para.FileName = strFileName;
            
            }
            //end end
            para.IsLoginNameEmpty = Extract("ACCOUNTLOGIN", array) == string.Empty ? true : false;
           
            //Add by Zhengwei,2013.5.22
            if (para.IsLoginNameEmpty == true)
            {
                if (str.Contains("ENTER LANGUAGE"))
                    Log.log("all of the SPL file head has been read");
                else
                    Log.log("can't read all of the SPL file head");
            }
            para.LoginName = "";
            //End

            // Template Pattern         
            para.LoginName = ExtractUserInfo(array);

            if (para.LoginName == null || para.LoginName == " " || para.LoginName.Trim() == "")
            {
                para.LoginName = "未知用户";
            }

            if (para.FileName == null || para.FileName == " " || para.FileName.Trim() == "")
            {
                para.FileName = para.LoginName + "的打印作业";
            }
            return para;
        }

        //private  bool isUtf8(byte[] buff)
        //{
        //    for (int i = 0; i < buff.Length; i++)
        //    {
        //        if ((buff[i] & 0xE0) == 0xC0)    // 110x xxxx 10xx xxxx
        //        {
        //            if ((buff[i + 1] & 0x80) != 0x80)
        //            {
        //                return false;
        //            }
        //        }
        //        else if ((buff[i] & 0xF0) == 0xE0)  // 1110 xxxx 10xx xxxx 10xx xxxx
        //        {
        //            if ((buff[i + 1] & 0x80) != 0x80 || (buff[i + 2] & 0x80) != 0x80)
        //            {
        //                return false;
        //            }
        //        }
        //        else if ((buff[i] & 0xF8) == 0xF0)  // 1111 0xxx 10xx xxxx 10xx xxxx 10xx xxxx
        //        {
        //            if ((buff[i + 1] & 0x80) != 0x80 || (buff[i + 2] & 0x80) != 0x80 || (buff[i + 3] & 0x80) != 0x80)
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}


        //Edit by Zhengwei 2013.5.21
        protected string Extract(string key, string[] array)
        {
          
            string result = string.Empty;
            foreach (string item in array)
            {
                 if (item.Contains(key))
                {

                    // Added by ZW, 2013.5.17
                    string tempStr = item;
                    if (item.Length > 60)
                    {
                        tempStr = item.Substring(0, item.Length - 1) + "\"";

                    }
                    
                    Regex reg = new Regex("\".*?\"");
                    MatchCollection mv = reg.Matches(tempStr);

                    for (int i = 0; i < mv.Count; i++)
                    {
                        //Edited by Le Ning 2014.2.11
                        int nTempLen = mv[i].Value.Length - 2;
                        if(nTempLen>0)
                        {
                            result = mv[i].Value.Substring(1, nTempLen); 
                        }
                        //End 
                    }
  
                    break;
                 }
            }

            // Added by ZW, 2013.5.17
            //对于文件名比较长的情况将仅保留其前面22个字符；
            if (result.Length - 17 > 6)
                result = result.Substring(0, 22);
            return result;
        }

        protected abstract string ExtractUserInfo(string[] array);

    }
     
}