using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Net.Mail;

namespace MFPCheckService
{
    /// <summary>
    /// 邮件信息抽象类;
    /// </summary>
    public class MailAbstract
    {
        /// <summary>
        /// 邮件主题;
        /// </summary>
        private string subject;
        public string Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }
        /// <summary>
        /// 邮件正文;
        /// </summary>
        private string body;
        public string Body
        {
            get { return this.body; }
            set { this.body = value; }
        }
        /// <summary>
        /// 邮件相关信息;
        /// </summary>
        private MailMessage message;
        public MailMessage Message
        {
            get { return this.message; }
            set { this.message = value; }
        }
        /// <summary>
        /// 寄件人邮箱信息;
        /// </summary>
        private UserAbstract from;
        public UserAbstract From
        {
            get { return this.from; }
            set { this.from = value; }
        }
        /// <summary>
        /// 获取邮件信息类;
        /// </summary>
        /// <returns>邮件信息类</returns>
        public MailMessage GetInformation()
        {
            this.message.Subject = this.subject;//邮件主题
            this.message.SubjectEncoding = System.Text.Encoding.UTF8;//邮件主题编码方式;
            this.message.Body = this.body;//邮件主体;
            this.message.BodyEncoding = System.Text.Encoding.UTF8;//邮件主体编码方式;
            this.message.From = this.from.Mailaddress;//邮件发送方邮件地址信息;
            return message;
        }
        /// <summary>
        /// 邮件抽象类构造函数;
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="from">邮件发件人</param>
        public MailAbstract(string subject,string body,UserAbstract from)
        {
            this.subject = subject;
            this.body = body;
            this.from = from;
            this.message = new MailMessage();//实例化对象;
        }
    }
}