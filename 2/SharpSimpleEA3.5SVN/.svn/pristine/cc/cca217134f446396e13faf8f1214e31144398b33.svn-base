using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Net.Sockets;

namespace FollowMEService
{
    /// <summary>
    ///PrintParaModel 的摘要说明
    /// </summary>
    public class PrintParaModel
    {
        public PrintParaModel()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private string fileName;
        public string FileName
        {
            set { fileName = value; }
            get { return fileName; }
        }

        private string loginName;
        public string LoginName
        {
            set { loginName = value; }
            get { return loginName; }
        }

        private bool isLoginNameEmpty;
        public bool IsLoginNameEmpty
        {
            set { isLoginNameEmpty = value; }
            get { return isLoginNameEmpty; }
        }
    }

    public class IpAndKeyModel
    {
        public IpAndKeyModel(IPAddress tmpIP, string tmpKey)
        {
            ip = tmpIP;
            key = tmpKey;
        }
        public IPAddress ip
        {
            set;
            get;
        }

        public string key
        {
            set;
            get;
        }
    }

    public class SocketAndKeyModel
    {
        public SocketAndKeyModel(Socket tmpSoc, IpAndKeyModel tmpModel)
        {
            soc = tmpSoc;
            model = tmpModel;
        }
        public Socket soc
        {
            set;
            get;
        }

        public IpAndKeyModel model
        {
            set;
            get;
        }
    }
}