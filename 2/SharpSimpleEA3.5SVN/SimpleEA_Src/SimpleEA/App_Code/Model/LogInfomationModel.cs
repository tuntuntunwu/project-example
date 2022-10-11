using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Data;

/// <summary>
/// config 的摘要说明
/// </summary>
namespace Model
{
    public class LogInfomationModel
    {
      
        #region Model
        public int ID;
        public string PriceDetailID;
        public string FileName;
        public string Number;
        public string MFPPrintTaskID;
        public DateTime ProcessTime;//操作时间--Time
        public string UserName; //用户名--UserName    
        public string MFPModelName;//MFP型号--MFPModel
        public string MFPSerialNumber;//MFP序列号--SerialNumber
        public int JobType;//操作类型--JobID
        public string PageID;  //纸张--PageID
        public string ColorMode;//色彩   ??????????????(未在entry里找到对应的)
        public string Duplex; //单双面--Duplex
        public string SheetCount;//面数    ????????????????(未在entry里找到对应的)
        public string PapeCount; // 张数--PapeCount
        public string CopyCount;// 份数--CopyCount
        public string Cost;//总计--SpendMoney
        public string Status; //状态--Status
        public string CheckCopy;//查看
        public int Finished; //留底是否完成
        #endregion Model
    }
}
