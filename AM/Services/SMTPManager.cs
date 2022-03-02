using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AM.Services {
    public class SMTPManager {
        private static SMTPManager instance;
        public static SMTPManager Instance { get { return instance; } }
        public static SMTPManager Init(string mail, string password, bool IsInstance = false) {
            return Init("smtp.gmail.com", mail, password, 587, true, false, SmtpDeliveryMethod.Network, IsInstance);
        }
        public static SMTPManager Init(string smtpSever, string mail, string password, int port, bool enableSsl, bool useDefaultCredential, SmtpDeliveryMethod method, bool IsInstance = false) {
            var obj = new SMTPManager() {
                SMTPServer = smtpSever,
                MailFrom = mail,
                MailPassword = password,
                Port = port,
                Method = method,
                EnableSsl = enableSsl,
                UseDefaultCredentials = useDefaultCredential,
                Credentials = new System.Net.NetworkCredential(mail.Split('@')[0], password),
                MailFromClient = new SmtpClient(smtpSever) { Port = port, EnableSsl = enableSsl, UseDefaultCredentials = useDefaultCredential, DeliveryMethod = method },
            };
            obj.MailFromClient.Credentials = obj.Credentials;
            if (IsInstance) {
                instance = obj;
            }
            return obj;
        }

        public string SMTPServer { get; private set; }
        public string MailFrom { get; private set; }
        public string MailPassword { get; private set; }
        public SmtpClient MailFromClient { get; private set; }
        public System.Net.NetworkCredential Credentials { get; private set; }
        public int Port { get; private set; }
        public SmtpDeliveryMethod Method { get; private set; }
        public bool EnableSsl { get; private set; }
        public bool UseDefaultCredentials { get; private set; }

        public void SendAsync(string mailTo, string subject, string body, object userToken, string[] attachFileList = null) {
            SendAsync(mailToArray: new string[] { mailTo }, subject, body, userToken, attachFileList);
        }
        public void SendAsync(string[] mailToArray, string subject, string body, object userToken, string[] attachFileList = null) {

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(MailFrom);
            for (int i = 0; i < mailToArray.Length; i++) {
                mail.To.Add(new MailAddress(mailToArray[i]));
            };
            mail.Subject = subject;
            mail.Body = body;
            if (attachFileList != null) {
                for (int i = 0; i < attachFileList.Length; i++) {
                    if (!string.IsNullOrEmpty(attachFileList[i])) {
                        mail.Attachments.Add(new Attachment(attachFileList[i]));
                    }
                }
            }

            MailFromClient.SendAsync(mail, userToken);
            mail.Dispose();
        }
        public void Send(string mailTo, string subject, string body, string[] attachFileList = null) {
            Send(new string[] { mailTo }, subject, body, attachFileList);
        }
        public void Send(string[] mailToArray, string subject, string body, string[] attachFileList = null) {

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(MailFrom);
            for (int i = 0; i < mailToArray.Length; i++) {
                mail.To.Add(new MailAddress(mailToArray[i]));
            };
            mail.Subject = subject;
            mail.Body = body;
            if (attachFileList != null) {
                for (int i = 0; i < attachFileList.Length; i++) {
                    if (!string.IsNullOrEmpty(attachFileList[i])) {
                        mail.Attachments.Add(new Attachment(attachFileList[i]));
                    }
                }
            }

            MailFromClient.Send(mail);
            mail.Dispose();
        }
        public void Send(string mailTo, string subject, string body, KeyValuePair<MemoryStream, string>[] streams = null) {
            Send(new string[] { mailTo }, subject, body, streams);
        }
        public void Send(string[] mailToArray, string subject, string body, KeyValuePair<MemoryStream, string>[] streams = null) {

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(MailFrom);
            for (int i = 0; i < mailToArray.Length; i++) {
                mail.To.Add(new MailAddress(mailToArray[i]));
            };
            mail.Subject = subject;
            mail.Body = body;
            if (streams != null) {
                for (int i = 0; i < streams.Length; i++) {
                    if (streams[i].Key != null) {
                        mail.Attachments.Add(new Attachment(streams[i].Key, streams[i].Value));
                    }
                }
            }

            MailFromClient.Send(mail);
            mail.Dispose();
        }
    }
}
