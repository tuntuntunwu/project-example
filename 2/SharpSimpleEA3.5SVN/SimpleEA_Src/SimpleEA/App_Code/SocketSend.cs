using System;
using System.Collections.Generic;
using System.Web;
using System.Net ;
using System.IO ;
using System.Net.Sockets ;
using dtMFPPrintTaskTableAdapters;
using System.Threading;
using System.Text;

/// <summary>
/// send SPL file to MFP
/// </summary>
/// add by Wei Changye 
/// 2012.03.14
public class SocketSend : System.Web.UI.Page
{
    Socket socket;
    string hostIP;
    string port="9100";
    string fileKey;
    string strMsg = string.Empty;
    MFPPrintTaskTableAdapter adpter;


    public SocketSend(string RemoteIP)
    {
        hostIP = RemoteIP;
        port = "9100";
    }

    private string  GetFilePath(string key)
    {
        if (adpter == null)
            adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
        dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(Convert.ToInt32(key));

        if (table != null && table.Count > 0)
        {
            string pathfile = table[0].FileLocation + table[0].DiskFileName;
            return pathfile;
        }
        else
            return "";
    }

    //异步 send data
    public void Send(object key)
    {
        fileKey = (string)key;
        IPAddress ip = IPAddress.Parse(hostIP);

        //ThreadPool.QueueUserWorkItem(StartConn, ip);
        StartConn(ip);
    }

    private void StartConn(Object o)
    {
        IPAddress ip = (IPAddress)o;
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.BeginConnect(new IPEndPoint(ip, Convert.ToInt32(port)), new AsyncCallback(EndConn), null);
        
    }

    private void EndConn(IAsyncResult AR)
    {
      
        
        try
        {
            //Socket tmp = (Socket)AR.AsyncState;
            //tmp.EndConnect(AR);
            socket.EndConnect(AR);

            if (!File.Exists(GetFilePath(fileKey)))
            {

                return;
            }

            //FileStream fileSteam = new FileStream(GetFilePath(fileKey), FileMode.Open, FileAccess.Read);
            using (FileStream fileSteam = new FileStream(GetFilePath(fileKey), FileMode.Open, FileAccess.Read))
            {
                int readBytes = 0;
                while (true)
                {
                    // read
                    Byte[] fsSize = new Byte[1024];
                    readBytes = fileSteam.Read(fsSize, 0, fsSize.Length);

                    //read end
                    if (readBytes <= 0)
                    {
                        fileSteam.Close();
                        break;
                    }

                    //send
                    int resver = socket.Send(fsSize, readBytes, 0); //发送消息

                }
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch (Exception e)
        {
            Global.Log(e.ToString());
        }
        

    }
    //2018-01-09-for 打印留底

    public void PrintCopyProcess(string key)
    {
    }
    //
    //send msg
    public void SendMsg(string msg, string ip)
    {
        if (msg == "")
            return;
        strMsg = msg;

        Socket socketMsg = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socketMsg.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(UtilCommon.GetFollowME("OldRawRecvPort"))), new AsyncCallback(EndMsgConn), socketMsg);
        //socketMsg.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), 9100), new AsyncCallback(EndMsgConn), socketMsg);
    }

    private void EndMsgConn(IAsyncResult AR)
    {
        try
        {
            using (Socket tmp = (Socket)AR.AsyncState)
            {
                
                tmp.EndConnect(AR);
                byte[] bytes = Encoding.Default.GetBytes(strMsg);
                int resver = tmp.Send(bytes, bytes.Length, 0);
                tmp.Shutdown(SocketShutdown.Both);
                tmp.Close();
            }
        }
        catch (Exception e)
        {
            Global.Log(e.ToString());
        }
    }

}
