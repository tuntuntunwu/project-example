using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

namespace FollowMEService
{
    /// <summary>
    ///ExtractUserName 的摘要说明
    /// </summary>
    public class ExtractUserName : AbstractExtract
    {
        public ExtractUserName()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// Extract name from SPL file
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        protected override string ExtractUserInfo(string[] array)
        {
            return Extract("USERNAME", array);
        }
    }
}