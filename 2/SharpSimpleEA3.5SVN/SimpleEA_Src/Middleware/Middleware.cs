using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace SesMiddleware
{
    /// <summary>
    /// ICCardData
    /// </summary>
    public class ICCardData
    {
        // Added By Ji Jianxion 2010-07-20
        private const string XML_PATH = "\\App_Data\\UserInfo.XML";

        //Added by Le Ning 2010-7-7;
        public static int AddICCardInfo(string ICCardID, string UserName, string Password, string ServerPath)
        {
            string strInfo;
            try
            {
                string sss = ServerPath + XML_PATH;
                bool bExist = File.Exists(sss);
                if (bExist == true)
                {
                    FileStream fs = new FileStream(sss, FileMode.Open);
                    StreamReader srt = new StreamReader(fs, Convert.ToBoolean(FileMode.Open));
                    strInfo = srt.ReadToEnd();
                    srt.Close();

                    int nStart = strInfo.LastIndexOf("</ICCardList>");
                    //ADD BY ZHENGWEI 2013.10.15
                    if (ICCardID.IndexOf("\n") != -1)
                    {
                        ICCardID = ICCardID.Replace("\n", "");
                    }
                    if (ICCardID.IndexOf("\r") != -1)
                    {
                        ICCardID = ICCardID.Replace("\r", "");
                    }
                    if (nStart >= 0)
                    {
                        strInfo = strInfo.Substring(0, nStart);
                        strInfo = strInfo + "<CardInfo>\r\n";
                        strInfo = strInfo + "<ID>" + ICCardID + "</ID>\r\n";
                        strInfo = strInfo + "<User>" + UserName + "</User>\r\n";
                        strInfo = strInfo + "<Password>" + Password + "</Password>\r\n";
                        strInfo = strInfo + "</CardInfo>\r\n";
                        strInfo = strInfo + "</ICCardList>";

                        FileStream fst = new FileStream(sss, FileMode.Create);
                        StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                        swt.WriteLine(strInfo);
                        swt.Close();
                        fst.Close();
                    }
                }
                else
                {
                    //strInfo = "<?xml version="1.0"?>\r\n" + "<ICCardList>\r\n";
                    //strInfo = strInfo + "<CardInfo>\r\n";
                    //strInfo = strInfo + "<ID>" + ICCardID + "</ID>\r\n";
                    //strInfo = strInfo + "<User>" + UserName + "</User>\r\n";
                    //strInfo = strInfo + "<Password>" + Password + "</Password>\r\n";
                    //strInfo = strInfo + "</CardInfo>\r\n";
                    //strInfo = strInfo + "</ICCardList>";

                    //FileStream fst = new FileStream(sss, FileMode.Create);
                    //StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                    //swt.WriteLine(strInfo);
                    //swt.Close();
                    //fst.Close();
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                return (-1009);
            }

            return (0);
        }

        //Updated by Le Ning 2010-7-7;
        public static int DeleteICCardInfo(string ICCardIDList, string ServerPath)
        {
            try
            {
                string sss = ServerPath + XML_PATH;
                FileStream fs = new FileStream(sss, FileMode.Open);
                StreamReader srt = new StreamReader(fs, Convert.ToBoolean(FileMode.Open));
                string strInfo = srt.ReadToEnd();
                srt.Close();

                string[] ICCardID = ICCardIDList.Split(',');

                int nStart;
                int nEnd;
                int nPos = 0;

                int nFlag = 1;
                do
                {
                    if (nPos >= ICCardID.Length)
                    {
                        nFlag = 0;
                        break;
                    }

                    string ID = ICCardID[nPos];

                    // Update By Ji Jianxiong 2010-08-06 ST
                    // nStart = strInfo.IndexOf(ID);
                    nStart = strInfo.IndexOf("<ID>" + ID + "</ID>");
                    // Update By Ji Jianxiong 2010-08-06 ED
                    if (nStart >= 0)
                    {
                        nEnd = strInfo.IndexOf("<CardInfo>", nStart);

                        string tempLeft = strInfo.Substring(0, nStart);
                        if (nEnd < 0)
                        {
                            nStart = tempLeft.LastIndexOf("</CardInfo>");

                            if (nStart >= 0)
                            {
                                strInfo = strInfo.Substring(0, nStart);
                                strInfo = strInfo + "</CardInfo>\r\n";
                                strInfo = strInfo + "</ICCardList>";
                            }
                            else
                            {
                                nStart = tempLeft.LastIndexOf("<ICCardList>");
                                tempLeft = strInfo.Substring(0, nStart + 14);

                                nEnd = strInfo.LastIndexOf("</CardInfo>") + 13;
                                string tempRight = strInfo.Substring(nEnd, strInfo.Length - nEnd);

                                strInfo = tempLeft + tempRight;
                            }
                        }
                        else
                        {
                            nStart = tempLeft.LastIndexOf("<CardInfo>");
                            tempLeft = strInfo.Substring(0, nStart);
                            string tempRight = strInfo.Substring(nEnd);

                            strInfo = tempLeft + tempRight;
                        }
                    }

                    nPos++;
                } while (nFlag == 1);

                FileStream fst = new FileStream(sss, FileMode.Create);
                StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                swt.WriteLine(strInfo);
                swt.Close();
                fst.Close();
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                return (-1009);
            }

            return (0);
        }

        //Added by Le Ning 2010-6-22;
        public static int UpdateICCardInfo(string ICCardID, string UserName, string Password, string ServerPath)
        {
            try
            {
                string sss = ServerPath + XML_PATH;
                FileStream fs = new FileStream(sss, FileMode.Open);
                StreamReader srt = new StreamReader(fs, Convert.ToBoolean(FileMode.Open));
                string strInfo = srt.ReadToEnd();
                srt.Close();

                // Update By Ji Jianxiong 2010-08-06 ST
                // int nStart = strInfo.IndexOf(ICCardID);
                int nStart = strInfo.IndexOf("<ID>" + ICCardID + "</ID>");
                // Update By Ji Jianxiong 2010-08-06 ED

                // Update By Ji Jianxiong 2010-07-07 ST
                // int nEnd = strInfo.IndexOf("</CardInfo>");
                int nEnd = strInfo.IndexOf("</CardInfo>", nStart);
                // Update By Ji Jianxiong 2010-07-07 ED

                if (nStart >= 0 && nEnd >= 0)
                {
                    string temp = strInfo.Substring(nEnd);
                    strInfo = strInfo.Substring(0, nStart);

                    // Update By Ji Jianxiong 2010-08-06 ST
                    // strInfo = strInfo + ICCardID + "</ID>\r\n";
                    strInfo = strInfo + "<ID>" + ICCardID + "</ID>\r\n";
                    // Update By Ji Jianxiong 2010-08-06 ED
                    strInfo = strInfo + "<User>" + UserName + "</User>\r\n";
                    strInfo = strInfo + "<Password>" + Password + "</Password>\r\n";
                    strInfo = strInfo + temp;

                    FileStream fst = new FileStream(sss, FileMode.Create);
                    StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                    swt.WriteLine(strInfo);
                    swt.Close();
                    fst.Close();
                }

            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                return (-1009);
            }

            return (0);
        }

        //Added by Le Ning 2010-6-21;
        public static string GetUserInfo(string ICCardID, string ServerPath)
        {
            string resInfo;
            string UserName;
            string Password;

            resInfo = string.Empty;

            try
            {
                string sss = ServerPath + XML_PATH;
                FileStream fs = new FileStream(sss, FileMode.Open);
                StreamReader srt = new StreamReader(fs, Convert.ToBoolean(FileMode.Open));
                string strInfo = srt.ReadToEnd();
                srt.Close();

                // int nStart = strInfo.IndexOf(ICCardID);
                //Add by Zhengwei 2013.7.29
                if (ICCardID.IndexOf("\n") != -1 )
                {
                    ICCardID = ICCardID.Replace("\n", "");
                }
                if (ICCardID.IndexOf("\r") != -1 )
                {
                    ICCardID = ICCardID.Replace("\r", "");
                }
                //End

                // Edit by Zhengwei 2013.8.2
                //int a=ICCardID.Length;
                //if (a != 10)
                //{
                //    new ICCardID.Length == a;
                //}
                int nStart = strInfo.IndexOf("<ID>" + ICCardID + "</ID>");

                int nEnd = nStart;
                if (nStart >= 0)
                {
                    nStart = strInfo.IndexOf("<User>", nStart);
                    if (nStart >= 0)
                    {
                        nEnd = strInfo.IndexOf("</User>", nStart + 1);
                        if (nEnd >= 0)
                        {
                            UserName = strInfo.Substring(nStart + 6, nEnd - nStart - 6);
                            resInfo = resInfo + UserName;
                        }
                    }
                }

                if (resInfo.Length > 0)
                {
                    nStart = strInfo.IndexOf("<Password>", nEnd);
                    if (nStart >= 0)
                    {
                        nEnd = strInfo.IndexOf("</Password>", nStart + 1);
                        if (nEnd >= 0)
                        {
                            Password = strInfo.Substring(nStart + 10, nEnd - nStart - 10);
                            resInfo = resInfo + "," + Password;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }

            return (resInfo);
        }

        public static int CheckICCardStatus(string ServerPath)
        {
            int resStatus = 0;
            return(resStatus);
        }

        public static string GetICCardID(string ServerPath)
        {
            string resID;
            resID = string.Empty;
  
            return(resID);
        }

        #region "List All User."
        /// <summary>
        /// List All User
        /// </summary>
        /// <param name="ServerPath"></param>
        /// <returns>ICardId and LoginID as string , split with ','</returns>
        public static List<String> ListAllUser(string ServerPath) {
            // Return Value
            List<String> resList = new List<string>();
            // User information(ICardId and LoginID), split with ','.
            string userInfo;


            try
            {
                // Get File Path.
                string sss = ServerPath + XML_PATH;

                // Get All text from file
                string strInfo;
                using (FileStream fs = new FileStream(sss, FileMode.Open))
                {
                    using (StreamReader srt = new StreamReader(fs, Convert.ToBoolean(FileMode.Open)))
                    {
                        strInfo = srt.ReadToEnd();
                        srt.Close();
                    }
                }

                while (!string.IsNullOrEmpty(strInfo))
                {
                    
                    int nStart = strInfo.IndexOf("<CardInfo>");

                    userInfo = string.Empty;


                    int nEnd = nStart;

                    if (nStart >= 0)
                    {
                        string UserName;
                        string ICCardId;

                        // Get IC Card ID
                        nStart = strInfo.IndexOf("<ID>", nStart);
                        if (nStart >= 0)
                        {
                            nEnd = strInfo.IndexOf("</ID>", nStart + 1);
                            if (nEnd >= 0)
                            {
                                ICCardId = strInfo.Substring(nStart + 4, nEnd - nStart - 4);
                                userInfo = ICCardId;
                            }
                        }

                        // Get Login ID
                        nStart = strInfo.IndexOf("<User>", nStart);
                        if (!string.IsNullOrEmpty(userInfo) && nStart >= 0)
                        {
                            nEnd = strInfo.IndexOf("</User>", nStart + 1);
                            if (nEnd >= 0)
                            {
                                UserName = strInfo.Substring(nStart + 6, nEnd - nStart - 6);
                                userInfo = userInfo + "," + UserName;
                            }
                        }


                        if (!string.IsNullOrEmpty(userInfo))
                        {
                            resList.Add(userInfo);
                        }

                        strInfo = strInfo.Substring(nStart);
                    }
                    else
                    {
                        break;
                    }
                } 


            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }

            return (resList);
        }
        #endregion

        #region "Check User"
        /// <summary>
        /// Check User
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ServerPath"></param>
        /// <returns></returns>
        public static bool CheckUser(string UserName, string ServerPath)
        {
            bool retVal = false;

            try
            {
                string sss = ServerPath + XML_PATH;
                string strInfo;
                using (FileStream fs = new FileStream(sss, FileMode.Open))
                {
                    using (StreamReader srt = new StreamReader(fs, Convert.ToBoolean(FileMode.Open)))
                    {
                        strInfo = srt.ReadToEnd();
                        srt.Close();
                    }
                }

                int nStart = strInfo.IndexOf("<User>" + UserName + "</User>");

                if (nStart >= 0)
                {
                    retVal = true;
                }

            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }

            return (retVal);
        }
        #endregion

        //Add by Wei Changye 2012-03-05;
        public static int DeleteICCardInfoByUserName(string UserName, string ServerPath)
        {
            try
            {
                string sss = ServerPath + XML_PATH;
                FileStream fs = new FileStream(sss, FileMode.Open);
                StreamReader srt = new StreamReader(fs, Convert.ToBoolean(FileMode.Open));
                string strInfo = srt.ReadToEnd();
                srt.Close();


                int nStart;
                int nEnd;
                int nPos = 0;

                int nFlag = 1;
                do
                {
                    if (nPos >= UserName.Length)
                    {
                        nFlag = 0;
                        break;
                    }


                    // Update By Ji Jianxiong 2010-08-06 ST
                    // nStart = strInfo.IndexOf(ID);
                    nStart = strInfo.IndexOf("<User>" + UserName + "</User>");
                    // Update By Ji Jianxiong 2010-08-06 ED
                    if (nStart >= 0)
                    {
                        nEnd = strInfo.IndexOf("<CardInfo>", nStart);

                        string tempLeft = strInfo.Substring(0, nStart);
                        if (nEnd < 0)
                        {
                            nStart = tempLeft.LastIndexOf("</CardInfo>");

                            if (nStart >= 0)
                            {
                                strInfo = strInfo.Substring(0, nStart);
                                strInfo = strInfo + "</CardInfo>\r\n";
                                strInfo = strInfo + "</ICCardList>";
                            }
                            else
                            {
                                nStart = tempLeft.LastIndexOf("<ICCardList>");
                                tempLeft = strInfo.Substring(0, nStart + 14);

                                nEnd = strInfo.LastIndexOf("</CardInfo>") + 13;
                                string tempRight = strInfo.Substring(nEnd, strInfo.Length - nEnd);

                                strInfo = tempLeft + tempRight;
                            }
                        }
                        else
                        {
                            nStart = tempLeft.LastIndexOf("<CardInfo>");
                            tempLeft = strInfo.Substring(0, nStart);
                            string tempRight = strInfo.Substring(nEnd);

                            strInfo = tempLeft + tempRight;
                        }
                    }

                    nPos++;
                } while (nFlag == 1);

                FileStream fst = new FileStream(sss, FileMode.Create);
                StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                swt.WriteLine(strInfo);
                swt.Close();
                fst.Close();
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                return (-1009);
            }

            return (0);
        }

        #region "Check Card ID"
        /// <summary>
        /// CheckCardID
        /// </summary>
        /// <param name="cardID"></param>
        /// <param name="ServerPath"></param>
        /// <returns></returns>
        /// Add by Wei Changye 2012-03-29;
        public static bool CheckCardID(string cardID, string ServerPath)
        {
            bool retVal = false;

            try
            {
                string sss = ServerPath + XML_PATH;
                string strInfo;
                using (FileStream fs = new FileStream(sss, FileMode.Open))
                {
                    using (StreamReader srt = new StreamReader(fs, Convert.ToBoolean(FileMode.Open)))
                    {
                        strInfo = srt.ReadToEnd();
                        srt.Close();
                    }
                }

                int nStart = strInfo.IndexOf("<ID>" + cardID + "</ID>");

                if (nStart >= 0)
                {
                    retVal = true;
                }

            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }

            return (retVal);
        }
        #endregion

        //Add by Wei Changye 2012-03-29;
        public static int DeleteICCardInfoByCardID(string cardID, string ServerPath)
        {
            try
            {
                string sss = ServerPath + XML_PATH;
                FileStream fs = new FileStream(sss, FileMode.Open);
                StreamReader srt = new StreamReader(fs, Convert.ToBoolean(FileMode.Open));
                string strInfo = srt.ReadToEnd();
                srt.Close();


                int nStart;
                int nEnd;
                int nPos = 0;

                int nFlag = 1;
                do
                {
                    if (nPos >= cardID.Length)
                    {
                        nFlag = 0;
                        break;
                    }


                    nStart = strInfo.IndexOf("<ID>" + cardID + "</ID>");
                    
                    if (nStart >= 0)
                    {
                        nEnd = strInfo.IndexOf("<CardInfo>", nStart);

                        string tempLeft = strInfo.Substring(0, nStart);
                        if (nEnd < 0)
                        {
                            nStart = tempLeft.LastIndexOf("</CardInfo>");

                            if (nStart >= 0)
                            {
                                strInfo = strInfo.Substring(0, nStart);
                                strInfo = strInfo + "</CardInfo>\r\n";
                                strInfo = strInfo + "</ICCardList>";
                            }
                            else
                            {
                                nStart = tempLeft.LastIndexOf("<ICCardList>");
                                tempLeft = strInfo.Substring(0, nStart + 14);

                                nEnd = strInfo.LastIndexOf("</CardInfo>") + 13;
                                string tempRight = strInfo.Substring(nEnd, strInfo.Length - nEnd);

                                strInfo = tempLeft + tempRight;
                            }
                        }
                        else
                        {
                            nStart = tempLeft.LastIndexOf("<CardInfo>");
                            tempLeft = strInfo.Substring(0, nStart);
                            string tempRight = strInfo.Substring(nEnd);

                            strInfo = tempLeft + tempRight;
                        }
                    }

                    nPos++;
                } while (nFlag == 1);

                FileStream fst = new FileStream(sss, FileMode.Create);
                StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                swt.WriteLine(strInfo);
                swt.Close();
                fst.Close();
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                return (-1009);
            }

            return (0);
        }


    }
}
