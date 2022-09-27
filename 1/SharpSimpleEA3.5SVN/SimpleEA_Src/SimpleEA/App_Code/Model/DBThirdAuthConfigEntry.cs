using System;
using System.Data;
using System.Configuration;


/// <summary>
/// config 的摘要说明
/// </summary>
namespace Model
{
    public class DBThirdAuthConfigEntry
    {
        #region Model
        //DBServer
        public string DBConnectStr = "";
        public string DBSearchSql = "";
        public string DBWhereSql = "";
        //用户信息匹配
        public string MTGrpVal1 = "";
        public string MTGrpVal2 = "";
        public int GroupID1 = 0;
        public int GroupID2 = 0;
        public int AuthDBFlg = 0;

        #endregion Model
    }
}
