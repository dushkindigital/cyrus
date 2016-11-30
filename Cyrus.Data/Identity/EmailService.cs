using System.Net.Mail;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net;
using System;
using System.Text;

namespace Cyrus.Data.Identity
{
    public class EmailService : IIdentityMessageService
    {

        public async Task SendAsync(IdentityMessage message)
        {
            await configSendMailAsync(message);
        }
        
        private async Task configSendMailAsync(IdentityMessage message)
        {
            var mMessage = new MailMessage();

            mMessage.To.Add(message.Destination);
            mMessage.From = new MailAddress(ConfigurationManager.AppSettings["emailService:FromEmailAddress"], "Do Not Reply");
            mMessage.Subject = message.Subject;
            mMessage.Body = message.Body;
            mMessage.IsBodyHtml = true;

            // SmtpClient configuration comes from config file
            using (var client = new SmtpClient()) 
            {
                await client.SendMailAsync(mMessage);
            }
            
        }
        
        private string BuildPrettyHtmlEmail(string toEmail, string callbackUrl)
        {
            var content = new StringBuilder();

            #region Appended Html Content
            content.Append(" <div><table border='0' cellpadding='40' cellspacing='0' width='98%'>");
            content.Append(" <tbody><tr><td style='font-family: tahoma,verdana,arial,sans-serif;' bgcolor='#f7f7f7' width='100%'>");
            content.Append(" <table border='0' cellpadding='0' cellspacing='0' width='620'><tbody><tr>");
            content.Append(" <td style='padding: 4px 8px; background: rgb(59, 89, 152) none repeat scroll 0% 0%;");
            content.Append(" -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;");
            content.Append(" color: rgb(255, 255, 255); font-weight: bold; font-family: tahoma,verdana,arial,sans-serif;");
            content.Append(" vertical-align: middle; font-size: 16px; letter-spacing: -0.03em; text-align: left;'>Welcome to Project Cyrus</td></tr><tr>");
            content.Append(" <td style='border-left: 1px solid rgb(204, 204, 204); border-right: 1px solid rgb(204, 204, 204);");
            content.Append(" border-bottom: 1px solid rgb(59, 89, 152); padding: 15px; background-color: rgb(255, 255, 255)'");
            content.Append(" font-family: tahoma,verdana,arial,sans-serif;' valign='top'><table width='100%'><tbody><tr>");
            content.Append(" <td style='font-size: 12px;' align='left' valign='top' width='470px'>");
            content.Append(" <div style='margin-bottom: 15px; font-size: 13px;'>Hi,</div><div style='margin-bottom: 15px;'>");
            content.Append(" <div><div style='font-weight: bold; margin-bottom: 10px;'>To complete the sign-up process, please ");
            content.Append(" follow this link:</div><div><table cellpadding='0' cellspacing='0' width='100%'><tbody><tr><td style='width: 40px;'>");
            content.Append(" <img style='width: 30px; height: 30px;' src='http://static.ak.fbcdn.net/rsrc.php/zD0FR/hash/9f1u6nrr.png'></td><td>");
            content.Append(" <a rel='nofollow' target='_blank' href='" + callbackUrl + "'>");
            content.Append(" <span class='' id='lw_1270914822_0'>" + callbackUrl + "</span></a>");
            content.Append(" </td></tr></tbody></table></div><div style='margin-top: 10px;'><br />You may be asked to enter this <span style='border-bottom: 1px dashed rgb(0, 102, 204);");
            content.Append(" background: transparent none repeat scroll 0% 0%; cursor: pointer; -moz-background-clip: border;-moz-background-origin: padding; -moz-background-inline-policy: ");
            content.Append(" continuous;' class='' id='lw_1270914822_1'>confirmation code</span>:&nbsp;<b>" + callbackUrl + "</b></div></div></div><div style='margin-bottom: 15px;'>Welcome to <span class=''");
            content.Append(" id='lw_1270914822_2'>Cyrus</span>!</div><div style='margin: 0pt;'>The Cyrus Team</div></td><td style='padding-left: 15px;' align='left' valign='top' width='150'>");
            content.Append(" <table cellpadding='0' cellspacing='0' width='100%'><tbody><tr><td style='border: 1px solid rgb(255, 226, 34); padding: 10px; background-color: rgb(255, 248, 204);");
            content.Append(" color: rgb(51, 51, 51); font-size: 12px;'><div style='margin-bottom: 15px;'>Get started:</div><table cellpadding='0' cellspacing='0'><tbody><tr><td style='border: 1px solid rgb(59, 110, 34);'>");
            content.Append(" <table cellpadding='0' cellspacing='0'><tbody><tr><td style='border-top: 1px solid rgb(149, 191, 130); padding: 5px 15px; background-color: rgb(103, 165, 75);'>");
            content.Append(" <a rel='nofollow' target='_blank' href='" + callbackUrl + "' style='color: rgb(255, 255, 255); font-size: 13px; font-weight: bold;");
            content.Append(" text-decoration: none;'><span class='' id='lw_1270914822_3'>Complete Sign-up</span></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody>");
            content.Append(" </table><img src='http://www.workbook.com/email_open_log_pic.php?k=zj4ozoas%3D2zc&amp;t=15&amp;c=" + callbackUrl + "' alt='' style='border: 0pt none; height: 1px; width: 1px;'></td></tr><tr>");
            content.Append(" <td style='padding: 10px; color: rgb(153, 153, 153); font-size: 11px; font-family: tahoma,verdana,arial,sans-serif;'>Didn't sign up for Cyrus? Please <a rel='nofollow' target='_blank'");
            content.Append(" href='" + callbackUrl + "&amp;report=1'><span class='' id='lw_1270914822_4'>let us know</span></a>.</td></tr></tbody>");
            content.Append(" </table></td></tr></tbody></table></div>");
            #endregion

            return content.ToString();

        }



    }
}
