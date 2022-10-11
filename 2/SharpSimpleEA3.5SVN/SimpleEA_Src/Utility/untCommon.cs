using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Utility
{
    public class untCommon
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
        /// 消息对话框
        /// </summary>
        /// <param name="strText">文本</param>
        /// <param name="strTitle">标题</param>
        public static void InfoMsg(string strTitle, string strText)
        {
            MessageBox.Show(strText, strTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// 错误对话框
        /// </summary>
        /// <param name="strText">文本</param>
        /// <param name="strTitle">标题</param>
        public static void ErrorMsg(string strTitle, string strText)
        {
            MessageBox.Show(strText, strTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 问题对话框
        /// </summary>
        /// <param name="strText">文本</param>
        /// <param name="strTitle">标题</param>
        public static bool QuestionMsg(string strTitle, string strText)
        {
            if (MessageBox.Show(strText, strTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 警告对话框
        /// </summary>
        /// <param name="txt">文本</param>
        /// <param name="title">标题</param>
        public static bool WarningMsg(string txt)
        {
            if (MessageBox.Show(txt, "确认请求", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 警告对话框
        /// </summary>
        /// <param name="strText">文本</param>
        /// <param name="strTitle">标题</param>
        public static bool WarningMsg(string strTitle, string strText)
        {
            if (MessageBox.Show(strText, strTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
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
        /// public static void WriteLog(string strLogfile, string strTitle, string strContent)
        public static void WriteLog(string strTitle, string strContent)
        {
            string strLogfile = @"SimpleEA_registration.log";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(strLogfile, true))
            {
                sw.WriteLine("＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝");
                sw.WriteLine("＝＝＝＝＝＝＝＝＝＝＝＝ LOG ＝＝＝＝＝＝＝＝＝＝＝＝");
                sw.WriteLine("Start @ " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
                sw.WriteLine(strTitle);
                sw.WriteLine(strContent);
                sw.Close();
                sw.Dispose();
            }
        }

    }
}
