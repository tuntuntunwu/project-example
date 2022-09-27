using System;
using System.Collections.Generic;
using System.Text;

namespace FollowMEService
{
    public interface IImport
    {
        byte[] ImportInfo(string key, string strRecv, int ID);
    }
}
