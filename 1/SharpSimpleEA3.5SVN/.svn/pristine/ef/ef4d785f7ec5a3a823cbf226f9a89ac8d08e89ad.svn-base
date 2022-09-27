using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Net.Mail;

namespace MFPCheckService
{
    /// <summary>
    /// 邮件助手抽象类;
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// Smtp协议发送邮件;
        /// </summary>
        private SmtpClient client;
        /// <summary>
        /// 邮件正体;
        /// </summary>
        private MailAbstract message;
        public MailAbstract Meaage
        {
            get { return message; }
            set { this.message = value; }
        }
        /// <summary>
        /// 收件人信息;
        /// </summary>
        private UserAbstract to;
        public UserAbstract To
        {
            get { return to; }
            set { this.to = value; }
        }
        /// <summary>
        /// 邮件助手构造函数;
        /// </summary>
        /// <param name="message">邮件正体</param>
        /// <param name="user">收件人信息</param>
        public MailHelper(MailAbstract message, UserAbstract user)
        {
            client = new SmtpClient();
            this.message = message;
            this.to = user;
        }
        /// <summary>
        /// 发送邮件方法;
        /// </summary>
        public bool send(string serverIp)
        {
            MailMessage mail;
            client.Credentials = new System.Net.NetworkCredential(message.From.Mailaddress.Address, message.From.Password);//验证发送方身份;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;//发送方式;
            //服务器的ip地址;
            client.Host = serverIp;
            //服务器的端口号;
            //client.Port = 21;

            try
            {
                mail = message.GetInformation();
                mail.To.Add(to.Mailaddress);
                client.Send(mail);
                return true;
            }
            catch (SmtpException err)
            {
                string ss = err.ToString();
                return false;
            }
        }
    }
}