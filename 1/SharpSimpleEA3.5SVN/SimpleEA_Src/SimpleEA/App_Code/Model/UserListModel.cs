using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Model
{
    public class UserListModel
    {
        //cui20170620
        #region Model
        public int ID;
        public string UserName;
        public string LoginName;
        public string Password;
        public string ICCardID;
        public string PinCode;
        public string Email;
        public int GroupID;
        public int RestrictionID;
        public int ComeFrom;
        public DateTime CreateTime;
        public DateTime UpdateTime;
        #endregion Model
    }
}
