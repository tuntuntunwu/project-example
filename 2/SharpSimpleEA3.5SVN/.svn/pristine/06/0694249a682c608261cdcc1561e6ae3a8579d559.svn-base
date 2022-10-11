using System;
using System.Collections.Generic;
using System.Text;

namespace FollowMEService
{
    class ImportToPCName : IImport
    {
        /// <summary>
        /// 将数据库MFPprintTask的ID主键植入SPL文件的PCName字段中，以便在Event事件中能够明确Event中的事件为哪个SPL文件
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="strRecv"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public byte[] ImportInfo(string key, string strRecv, int ID)
        {
            string[] array = strRecv.Split('@');
            strRecv = string.Empty;
            for (int item = 0; item < array.Length; item++)
            {
                if (array[item].Contains(key))
                {
                    int oldLength = array[item].Length;
                    int newLength = string.Format("PJL SET " + key + "=\"" + ID + "\"\r\n").Length;
                    string strID = ID.ToString();
                    if (oldLength > newLength)
                        for (int i = 0; i < oldLength - newLength; i++)
                            strID += " ";

                    array[item] = "PJL SET " + key + "=\"" + strID + "\"\r\n";
                    break;
                }
            }
            foreach (string item in array)
            {
                if (strRecv == string.Empty)
                    strRecv = item;
                else
                    strRecv += "@" + item;
            }

            byte[] tmp = Encoding.Default.GetBytes(strRecv);
            return tmp;
        }
    }
}
