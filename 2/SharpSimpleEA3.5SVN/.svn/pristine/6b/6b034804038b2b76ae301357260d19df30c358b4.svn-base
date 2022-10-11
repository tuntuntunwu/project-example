using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MFPOSAautosubmit
{
    class untCommon
    {
        /// <summary>
        /// 消息对话框
        /// </summary>
        /// <param name="txt">文本</param>
        /// <param name="title">标题</param>
        public static void InfoMsg(string txt)
        {
            MessageBox.Show(txt, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 错误对话框
        /// </summary>
        /// <param name="txt">文本</param>
        /// <param name="title">标题</param>
        public static void ErrorMsg(string txt)
        {
            MessageBox.Show(txt, " 错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 问题对话框
        /// </summary>
        /// <param name="txt">文本</param>
        /// <param name="title">标题</param>
        public static bool QuestionMsg(string txt)
        {
            if (MessageBox.Show(txt, "确认请求", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="strLogfile">Log文件名</param>
        /// <param name="strTitle">标题</param>
        /// <param name="strContent">内容</param>
        public static void WriteLog(string strLogfile, string strTitle, string strContent)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(strLogfile, true))
            {
                sw.WriteLine("＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝");
                sw.WriteLine("== LOG日志 ==");
                sw.WriteLine("Start @ " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
                sw.WriteLine(strTitle);
                sw.WriteLine(strContent);
            }
        }

    }
}
