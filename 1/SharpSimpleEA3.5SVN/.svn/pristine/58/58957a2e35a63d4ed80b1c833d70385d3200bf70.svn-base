using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Net.Mail;

namespace MFPCheckService
{
    /// <summary>
    /// 用户抽象类;
    /// </summary>
    public class UserAbstract
    {
        /// <summary>
        /// 用户邮件地址信息;
        /// </summary>
        private MailAddress mailAddress;
        public MailAddress Mailaddress
        {
            get { return this.mailAddress; }
            set { this.mailAddress = value; }
        }
        /// <summary>
        /// 发送方的密码信息;
        /// </summary>
        private string password;
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        /// <summary>
        /// 收件人抽象类构造函数;
        /// </summary>
        /// <param name="name">收件人姓名（可置空）</param>
        /// <param name="address">收件人邮箱地址（不可置空）</param>
        public UserAbstract(string name, string address)
        {
            this.mailAddress = new MailAddress(address, name);
        }
        /// <summary>
        /// 发送方抽象类构造函数
        /// </summary>
        /// <param name="name">发送方姓名（可置空）</param>
        /// <param name="address">发送方邮箱地址（不可置空）</param>
        /// <param name="password">发送方密码（不可置空）</param>
        public UserAbstract(string name, string address, string password)
        {
            this.mailAddress = new MailAddress(address, name);
            this.password = password;
        }
    }
}