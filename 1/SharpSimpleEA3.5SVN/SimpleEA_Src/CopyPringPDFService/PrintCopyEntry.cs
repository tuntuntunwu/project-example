using System;
using System.Data;
using System.Configuration;
using System.Web;

/// <summary>
/// config 的摘要说明
/// </summary>
namespace CopyPringPDFService
{
    public class PrintCopyEntry
    {
        #region Model

        public int ID;
        public DateTime Time;
        public int UserID;
        public string IpAddress;
        public int CopyType;
        public string OrigFile;
        public string CopyFile;
        public int Finished;
        public int CopyTimes;

        #endregion Model
    }
}
