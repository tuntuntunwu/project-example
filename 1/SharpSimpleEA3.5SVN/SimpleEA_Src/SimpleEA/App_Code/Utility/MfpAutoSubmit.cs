using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MFPOSAautosubmit
{
    #region SeriesInfo
    public partial class SeriesInfo
    {
        #region 常量
        public static readonly string SCHEMA_HOST_FORMAT = "{0}://{1}{2}{3}";     // {http|https}://[user:pass@]hostadr[:port]/
        /// HTML及参数
        public static readonly string LOGIN_HTML = "/login.html";
        public static readonly string OSA_SET_HTML = "/app_osa_account.html";

        public static readonly string AUTHENTIFICATION_STRING_AT_LOGIN = "ggt_select(10009)=3&ggt_textbox(10003)={0}&action=loginbtn&ggt_hidden(10008)=5";
        public static readonly string Enable_OSA_SETTING = "ggt_select(1)=1&ggt_checkbox(2)=1&ggt_select(4)=1&ggt_textbox(5)={0}&ggt_textbox(6)={1}&ggt_textbox(7)={2}" +
            "&ggt_textbox(8)=20&ggt_select(14)=2&ggt_textbox(15)=&ggt_textbox(16)=&ggt_textbox(17)=20&ggt_select(22)=2&ggt_textbox(23)=&ggt_textbox(24)" +
            "=&ggt_textbox(25)=20&ggt_select(30)=2&ggt_textbox(31)=&ggt_textbox(32)=&ggt_textbox(33)=20&action=submitbtn";

        public static readonly string Disable_OSA_SETTING = "ggt_select(1)=2&ggt_selhidden(4)=20%2C&ggt_textbox(5)={0}&ggt_textbox(6)={1}&ggt_textbox(7)={2}" +
            "&ggt_textbox(8)=20&ggt_selhidden(14)=20%2C&ggt_textbox(15)=&ggt_textbox(16)=&ggt_textbox(17)=20&ggt_selhidden(22)=20%2C&ggt_textbox(23)=&ggt_textbox(24)" +
            "=&ggt_textbox(25)=20&ggt_selhidden(30)=20%2C&ggt_textbox(31)=&ggt_textbox(32)=&ggt_textbox(33)=20&action=submitbtn";

        public static readonly string MFP_REBOOT = "AppOsaAccountRebootWebchange=&action=rebootBtn";
        #endregion

        #region 变量
        protected string seriesID;
        protected string seriesNo;

        //MFP network
        protected string IPadr;
        protected string protocol;
        protected int port;

        //MFP user & password
        protected string login;
        protected string password;

        //OSA setting        
        protected string osatitle;
        protected string osaeaui;
        protected string osaeawebservices;

        //
        protected MfpAutoSubmit AutoSubmit;
        #endregion

        #region SeriesInfo
        /// <summary>
        /// SeriesInfo
        /// </summary>
        /// <param name="seriesID"></param>
        public SeriesInfo(string seriesID)
        {
            this.seriesID = seriesID;
            this.seriesNo = null;
            this.IPadr = null;
            this.protocol = null;
            this.port = 0;

            this.login = null;
            this.password = null;

            this.osatitle = null;
            this.osaeaui = null;
            this.osaeawebservices = null;

            this.AutoSubmit = null;
        }
        /// <summary>
        /// SeriesInfo
        /// </summary>
        /// <param name="seriesID">seriesID</param>
        /// <param name="IPadr">IP address</param>
        /// <param name="protocol">protocol(http/https)</param>
        /// <param name="port">port</param>
        public SeriesInfo(string seriesID, string IPadr, string protocol, int port)
            : this(seriesID)
        {
            SetNetInfo(IPadr, protocol, port);
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CertificateCallback);
        }
        #endregion

        #region CertificateCallback
        //警告回避
        private bool CertificateCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        #endregion

        #region SetNetInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IPadr">IP address</param>
        /// <param name="protocol">protocol(http/https)</param>
        /// <param name="port">port</param>
        public void SetNetInfo(string IPadr, string protocol, int port)
        {
            this.IPadr = IPadr;
            this.protocol = protocol.ToLower();
            this.port = port;
        }
        // SetNetInfo(protocol,port change)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocol">protocol(http/https)</param>
        /// <param name="port">port</param>
        public void SetNetInfo(string protocol, int port)
        {
            this.protocol = protocol.ToLower();
            this.port = port;
        }
        #endregion

        #region SetAccountInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="pass">passord</param>
        public void SetAccountInfo(string login, string pass)
        {
            this.login = login;
            this.password = pass;
        }
        #endregion

        #region SetOsaEAInfo
        public void SetOsaEAInfo(string osatitle, string osaeaui, string osaeawebservices)
        {
            this.osatitle = osatitle;
            this.osaeaui = osaeaui;
            this.osaeawebservices = osaeawebservices;
        }
        #endregion

        #region 参数
        public virtual string IPAddress
        {
            get
            {
                return this.IPadr;
            }
        }

        public virtual string Protocol
        {
            get
            {
                return this.protocol;
            }
        }

        public virtual int PortNo
        {
            get
            {
                return this.port;
            }
        }

        public virtual string SeriesNo
        {
            get
            {
                return this.seriesNo;
            }
        }

        /// <summary>
        /// GET Login URL
        /// </summary>
        public virtual string LoginPageURL
        {
            get
            {
                return this.HostProtocolURI + LOGIN_HTML;
            }
        }
        /// <summary>
        /// Login string
        /// </summary>
        public virtual string AuthentificationString
        {
            get
            {
                //URL编码
                string encodePassword = HttpUtility.UrlEncode(this.password);
                return string.Format(AUTHENTIFICATION_STRING_AT_LOGIN, encodePassword);
            }
        }

        /// <summary>
        /// GET OSA setting URL
        /// </summary>
        public virtual string OsaSettingURL
        {
            get
            {
                return this.HostProtocolURI + OSA_SET_HTML;
            }
        }
        /// <summary>
        /// OSA setting String
        /// </summary>
        public virtual string EnableOSAconfigString
        {
            get
            {
                string strOsaTitle = HttpUtility.UrlEncode(this.osatitle);
                string strOsaUI = HttpUtility.UrlEncode(this.osaeaui);
                string strOsaWebservices = HttpUtility.UrlEncode(this.osaeawebservices);

                return string.Format(Enable_OSA_SETTING, strOsaTitle, strOsaUI, strOsaWebservices);
            }
        }
        /// <summary>
        /// OSA setting String
        /// </summary>
        public virtual string DisableOSAconfigString
        {
            get
            {
                string strOsaTitle = HttpUtility.UrlEncode(this.osatitle);
                string strOsaUI = HttpUtility.UrlEncode(this.osaeaui);
                string strOsaWebservices = HttpUtility.UrlEncode(this.osaeawebservices);

                return string.Format(Disable_OSA_SETTING, strOsaTitle, strOsaUI, strOsaWebservices);
            }
        }

        /// <summary>
        /// GET REBOOT URL
        /// </summary>
        public virtual string RebootURL
        {
            get
            {
                return this.HostProtocolURI + OSA_SET_HTML;
            }
        }
        /// <summary>
        /// OSA setting postdata
        /// </summary>
        public virtual string RebootString
        {
            get
            {
                return string.Format(MFP_REBOOT);
            }
        }

        /// <summary>
        /// 连接
        /// </summary>
        public virtual string HostProtocolURI
        {
            get
            {
                //string accStr = this.login != null ? this.login+":"+this.password+"@" : "";
                string accStr = "";
                string portStr = ("http".Equals(this.protocol) && 80 == this.port) ||
                    ("https".Equals(this.protocol) && 443 == this.port) ?
                    "" : ":" + this.port.ToString();
                return string.Format(SCHEMA_HOST_FORMAT, this.protocol, accStr, this.IPadr, portStr);
            }
        }

        /// <summary>
        /// MFP get / set
        /// </summary>
        public virtual MfpAutoSubmit MfpAutoSubmit
        {
            get
            {
                if (this.AutoSubmit == null)
                {
                    this.AutoSubmit = new MfpAutoSubmit();
                }
                return this.AutoSubmit;
            }
            set
            {
                this.AutoSubmit = value;
            }
        }
        #endregion
    }
    #endregion

    #region MfpAutoSubmit
    /// <summary>
    /// MFP OSA setting page submit
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class MfpAutoSubmit
    {
        //OSA config auto submit Process
        public static bool AutoSubmitProcess(string strMFPip, string strAdmin, string strPWD, string strEA, string strEAUI, string strEAWeb, string strOsaEnableOrDisable)
        {
            try
            {
                string seriesId = "95"; //随便
                SeriesInfo series = new SeriesInfo(seriesId, strMFPip, "http", 80);

                series.SetAccountInfo(strAdmin, strPWD);
                series.SetOsaEAInfo(strEA, strEAUI, strEAWeb);

                return series.MfpAutoSubmit.OSApageSubmit(series, strOsaEnableOrDisable);
            }
            catch (Exception exMfpAutoSubmit)
            {
                exMfpAutoSubmit.ToString();
                return false;
            }
        }

        #region SimpleRequestHeader
        /// <summary>
        /// SimpleRequestHeader
        /// </summary>
        /// <remarks>利用TcpClient通信</remarks>
        protected class SimpleRequestHeader
        {
            public string Method;
            public string URL;
            public string Protocol;
            public string Version;
            public Dictionary<string, string> Headers;

            #region SimpleRequestHeader
            public SimpleRequestHeader()
            {
                this.Method = "GET";
                this.URL = "";
                this.Protocol = "HTTP";
                this.Version = "1.1";

                this.Headers = new Dictionary<string, string>();
                this.Headers.Add("Accept", "*/*");
                this.Headers.Add("Accept-Language", "zh-CN");
                this.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)");
                this.Headers.Add("Accept-Encoding", "gzip, deflate");
                this.Headers.Add("Host", "");
                this.Headers.Add("Connection", "Keep-Alive"); //使用代理的 this.Headers.Add("Proxy-Connection", "Keey-Alive")
                this.Headers.Add("Cookie", "");
            }
            #endregion

            #region SetHeaderValue
            public void SetHeaderValue(string key, string value)
            {
                if (Headers.ContainsKey(key))
                {
                    Headers[key] = value;
                }
                else
                {
                    Headers.Add(key, value);
                }
            }
            #endregion

            #region ToString
            public override string ToString()
            {
                StringBuilder msgs = new StringBuilder(1024);
                msgs.Append(string.Format("{0} {1} {2}/{3}", this.Method, this.URL, this.Protocol, this.Version));
                foreach (KeyValuePair<string, string> ent in this.Headers)
                {
                    msgs.Append("\r\n" + ent.Key + ": " + ent.Value);
                }
                msgs.Append("\r\n\r\n");

                return msgs.ToString();
            }
            #endregion
        }
        #endregion

        #region 取消请求
        /// <summary>
        /// 取消请求
        /// </summary>
        private bool _isRequestCancel = false;
        /// <summary>
        /// 取消请求（设置）
        /// </summary>
        public bool IsRequestCancel { set { _isRequestCancel = value; } }
        #endregion

        #region GetString
        /// <summary>
        /// 标头文字的获得
        /// </summary>
        /// <remarks>方法：keyword、delim</remarks>
        /// <param name="headerStr"></param>
        /// <param name="keyword"></param>
        /// <param name="delim"></param>
        /// <returns>""</returns>
        protected string GetString(string headerStr, string keyword, string delim)
        {
            string retStr = "";
            int top = headerStr.IndexOf(keyword);
            if (top >= 0)
            {
                top += keyword.Length;
                int end = headerStr.IndexOf(delim, top);
                if (end < 0)
                {
                    end = headerStr.Length;
                }
                retStr = headerStr.Substring(top, end - top);
            }
            return retStr;
        }
        #endregion

        #region GetHeaderString
        /// <summary>
        /// 标头文字中,内容取得
        /// </summary>
        /// <remarks>标头文字中的"key: value</remarks>
        /// <param name="headerStr"></param>
        /// <param name="key"></param>
        /// <returns>""</returns>
        protected string GetHeaderString(string headerStr, string key)
        {
            return GetString(headerStr, key + ": ", "\r\n");
        }
        #endregion

        #region CreateNetStream
        /// <summary>
        /// SeriesInfo的HTTP or HTTPS 配合、NetworkStream返回。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        protected Stream CreateNetStream(TcpClient client, SeriesInfo uri)
        {
            Stream stream = null;
            client.Connect(uri.IPAddress, uri.PortNo);      //http or https的端口连接
            if ("http".Equals(uri.Protocol))
            {
                // 连接口开始取得networkstream
                NetworkStream net_stream = client.GetStream();
                stream = net_stream;
            }
            else
            {
                // 连接口开始取得sslstream
                SslStream ssl_stream = new SslStream(client.GetStream(), false, OnRemodeCertificateValidationCallback);
                ssl_stream.AuthenticateAsClient("", null, SslProtocols.Ssl2 | SslProtocols.Ssl3 | SslProtocols.Tls, false);
                stream = ssl_stream;
            }
            return stream;
        }
        #endregion

        #region OnRemodeCertificateValidationCallback
        /// <summary>
        /// sslstream用函数
        /// </summary>
        protected static bool OnRemodeCertificateValidationCallback(Object aSender, X509Certificate aCertificate, X509Chain aChain, SslPolicyErrors aSslPolicyErrors)
        {
            return true;
        }
        #endregion

        #region ConvertToString
        /// <summary>
        /// Stream的内容,而且还可以转换成string。
        /// </summary>
        /// <param name="instream"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        protected string ConvertToString(Stream instream, Encoding encode)
        {
            string resMsg = "";
            MemoryStream ms = new System.IO.MemoryStream();
            byte[] resBytes = new byte[1024];
            int resSize;
            do
            {
                //数据接收。
                resSize = instream.Read(resBytes, 0, resBytes.Length);
                if (resSize != 0)
                {
                    //接收到的数据的
                    ms.Write(resBytes, 0, resSize);
                }
            } while (resSize != 0);

            //接收到的数据转换成文字列表
            resMsg = encode.GetString(ms.ToArray());
            return resMsg;
        }
        #endregion

        #region GetRelativeAddress
        /// <summary>
        /// 
        /// </summary>
        protected string GetRelativeAddress(string location)
        {
            string retStr = location;
            string[] keywords = new string[]
            {
                "https://",
                "http://"
            };

            for (int i = 0; i < keywords.Length; i++)
            {
                if (location.StartsWith(keywords[i]))
                {
                    int idx = location.IndexOf("/", keywords[i].Length);
                    if (idx >= 0)
                    {
                        retStr = location.Substring(idx);
                        break;
                    }
                }
            }
            return retStr;
        }
        #endregion

        #region SendRequest
        /// <summary>
        /// header, message,seriesURI信息发送
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="seriesURI"></param>
        /// <param name="header"></param>
        /// <param name="message"></param>
        protected void SendRequest(Stream stream, SeriesInfo seriesURI, SimpleRequestHeader header, string message)
        {
            byte[] reqMessage = null;
            if (message != null && message.Length != 0)
            {
                reqMessage = Encoding.UTF8.GetBytes(message);
                header.SetHeaderValue("Content-Length", reqMessage.Length.ToString());
            }

            byte[] reqHeader = Encoding.UTF8.GetBytes(header.ToString());

            //--------------------------------
            stream.Write(reqHeader, 0, reqHeader.Length);
            stream.Flush();

            if (reqMessage != null)
            {
                stream.Write(reqMessage, 0, reqMessage.Length);
                stream.Flush();
            }
        }
        #endregion

        #region OSApageSubmit
        /// <summary>
        /// MFP OSA config page auto submit
        /// </summary>
        /// <param name="seriesURI">MFP的SeriesInfo</param>
        /// <param name="OSAEnableOrDisable">OSA启用或禁用</param>
        public virtual bool OSApageSubmit(SeriesInfo seriesURI, string OSAEnableOrDisable)
        {
            string acceptStr = "*/*";
            if (_isRequestCancel)
            {
                return false;
            }
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                #region 第1次的请求
                //-------------------------------------------------
                // 第1次login请求(GET /login.html)
                Uri reqUri = new Uri(seriesURI.LoginPageURL);
                ServicePointManager.FindServicePoint(reqUri).Expect100Continue = false;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(reqUri);
                request.Method = "GET";
                request.ProtocolVersion = new Version(1, 1);
                request.Accept = acceptStr;

                request.Headers.Add("Accept-Language", "zh-CN");
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 1.1.4322)";

                request.UseDefaultCredentials = true;
                request.KeepAlive = false;
                CookieContainer cookieCont = new CookieContainer();
                request.CookieContainer = cookieCont;

                string sessionStr = string.Empty;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Uri getUri = response.ResponseUri;
                sessionStr = response.Headers["Set-Cookie"];

                using (Stream dataStream = response.GetResponseStream())
                {
                    while (true)
                    {
                        byte[] buff = new byte[4096];
                        int readsize = dataStream.Read(buff, 0, buff.Length);
                        if (readsize == 0)
                        {
                            break;
                        }
                    }
                    dataStream.Close();
                }

                String url = response.ResponseUri.ToString();

                if (url.StartsWith("https://") && (seriesURI.Protocol == "http"))
                {
                    seriesURI.SetNetInfo("https", 443);
                }

                response.Close();
                #endregion

                #region 第2次的请求
                //-------------------------------------------------
                // 第2次的请求(POST /login.html, GET /, GET /main.html)
                // POST回应302， GET回应200
                string postmsg = seriesURI.AuthentificationString;
                byte[] message = Encoding.UTF8.GetBytes(postmsg);

#if DEBUG
                for (int i = 0; i < message.Length; i++)
                {
                    Debug.Write(Char.ToString((char)message[i]));
                }
                Debug.WriteLine("");
#endif

                //获得编码
                Encoding encode = Encoding.Default;
                //接受的数据
                string resMsg = string.Empty;

                //
                using (Stream stream = CreateNetStream(new TcpClient(), seriesURI))
                {
                    SimpleRequestHeader reqHead = new SimpleRequestHeader();
                    reqHead.Method = "POST";
                    reqHead.URL = "/login.html";
                    reqHead.Protocol = "HTTP";
                    reqHead.Version = "1.1";
                    reqHead.SetHeaderValue("Host", seriesURI.IPAddress);
                    reqHead.SetHeaderValue("Cache-Control", "no-cache");
                    reqHead.SetHeaderValue("Cookie", sessionStr);
                    SendRequest(stream, seriesURI, reqHead, postmsg);

                    //--------------------------------
                    // post接收:服务器发送信号的数据。
                    resMsg = ConvertToString(stream, encode);
                    stream.Close();
                }

                //--------------------------------
                string StatusCode = GetString(resMsg, "HTTP/1.1 ", " ");
                if (!("302".Equals(StatusCode)))
                {
                    //error
                    throw new Exception("error");
                }

                //获得Response
                sessionStr = GetHeaderString(resMsg, "Set-Cookie");
                string Location = GetHeaderString(resMsg, "Location");

#if DEBUG
                Debug.WriteLine("Set-Cookie=" + sessionStr);
                Debug.WriteLine("Location=" + Location);
#endif

                //--------------------------------
                // 下一个get发送
                using (Stream stream = CreateNetStream(new TcpClient(), seriesURI))
                {
                    SimpleRequestHeader reqHead = new SimpleRequestHeader();
                    reqHead.URL = GetRelativeAddress(Location);
                    reqHead.Protocol = "HTTP";
                    reqHead.Version = "1.1";
                    reqHead.SetHeaderValue("Host", seriesURI.IPAddress);
                    reqHead.SetHeaderValue("Cookie", sessionStr);

                    SendRequest(stream, seriesURI, reqHead, null);

                    //--------------------------------
                    // GET
                    //服务器发送信号的数据
                    resMsg = ConvertToString(stream, encode);

                    stream.Close();
                }

                //--------------------------------
                StatusCode = GetString(resMsg, "HTTP/1.1 ", " ");
                if (StatusCode != "302")
                {
                    throw new Exception("error");
                }

                //获得Response
                Location = GetHeaderString(resMsg, "Location");

                //---------------------------------
                // GET main.html
                using (Stream stream = CreateNetStream(new TcpClient(), seriesURI))
                {
                    SimpleRequestHeader reqHead = new SimpleRequestHeader();
                    reqHead.URL = GetRelativeAddress(Location);
                    reqHead.Protocol = "HTTP";
                    reqHead.Version = "1.1";
                    reqHead.SetHeaderValue("Host", seriesURI.IPAddress);
                    reqHead.SetHeaderValue("Cookie", sessionStr);
                    SendRequest(stream, seriesURI, reqHead, null);

                    //--------------------------------
                    resMsg = ConvertToString(stream, encode);

                    stream.Close();
                }

                //--------------------------------
                StatusCode = GetString(resMsg, "HTTP/1.1 ", " ");
                if (StatusCode != "200")
                {
                    throw new Exception("error");
                }
                #endregion

                #region 第3次的请求
                //-------------------------------------------------
                // 第3次的请求OSA config in MFP(GET /app_osa_account.html)
                postmsg = seriesURI.EnableOSAconfigString;
                if (OSAEnableOrDisable == "0")
                {
                    postmsg = seriesURI.DisableOSAconfigString;
                }
                message = Encoding.UTF8.GetBytes(postmsg);

                encode = Encoding.Default;
                resMsg = string.Empty;

                using (Stream stream = CreateNetStream(new TcpClient(), seriesURI))
                {
                    SimpleRequestHeader reqHead = new SimpleRequestHeader();
                    reqHead.Method = "POST";
                    reqHead.URL = "/app_osa_account.html";
                    reqHead.Protocol = "HTTP";
                    reqHead.Version = "1.1";
                    reqHead.SetHeaderValue("Host", seriesURI.IPAddress);
                    reqHead.SetHeaderValue("Cache-Control", "no-cache");
                    reqHead.SetHeaderValue("Cookie", sessionStr);

                    SendRequest(stream, seriesURI, reqHead, postmsg);

                    //--------------------------------
                    resMsg = ConvertToString(stream, encode);
                    stream.Close();
                }

                #endregion

                #region 第4次的请求
                //-------------------------------------------------
                // 第4次的请求MFP Reboot(GET /app_osa_account.html)
                postmsg = seriesURI.RebootString;
                message = Encoding.UTF8.GetBytes(postmsg);

                encode = Encoding.Default;
                resMsg = string.Empty;

                using (Stream stream = CreateNetStream(new TcpClient(), seriesURI))
                {
                    SimpleRequestHeader reqHead = new SimpleRequestHeader();
                    reqHead.Method = "POST";
                    reqHead.URL = "/app_osa_account.html";
                    reqHead.Protocol = "HTTP";
                    reqHead.Version = "1.1";
                    reqHead.SetHeaderValue("Host", seriesURI.IPAddress);
                    reqHead.SetHeaderValue("Cache-Control", "no-cache");
                    reqHead.SetHeaderValue("Cookie", sessionStr);

                    SendRequest(stream, seriesURI, reqHead, postmsg);

                    //--------------------------------
                    // POST接收:服务器发送信号的数据
                    resMsg = ConvertToString(stream, encode);
                    stream.Close();
                }
                #endregion

                return true;
            }
            catch (WebException ex)
            {
                ex.ToString();
                return false;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
        }
        #endregion

        #region CancelHandler
        /// <summary>
        /// 
        /// </summary>
        public void CancelHandler(object sender, EventArgs e)
        {
            _isRequestCancel = true;
        }
        #endregion
    }
    #endregion

}
