
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
//using tcb.services;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace ifs.fairdeal.web.services
{
    /// <summary>
    /// Summary description for api
    /// </summary>
    public class api : IHttpHandler
    {
        public string error() { return "{\"success\":\"0\",\"message\":\"Failed\"}"; }
        public string success(string message) { return "{\"success\":\"1\",\"message\":\""+message+"\"}"; }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string retval = string.Empty;
            var method = context.Request["method"];
            if (method == "save")
                retval = save(context);
            else if (method == "SendEmail")
                retval = SendEmail(context);
            else if (method == "sendcareer")
                retval = SendCareer(context);
            
            context.Response.Write(retval);
        }

        public Dictionary<string,string> jsondata(HttpContext context)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var jsonString = String.Empty;

            context.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(context.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        }

        public string save(HttpContext context)
        {
            var post = jsondata(context);
            return success(post.ToString());
            //return retval;
        }

        public string SendCareer(HttpContext context)
        {
            try
            {
                var retVal = string.Empty;
                var post = jsondata(context);
                var username = post["username"].ToString();
                var useremail = post["useremail"].ToString();
                var usermobile = post["usermobile"].ToString();
                var usersubject = post["usersubject"].ToString();
                var userposition = post["userposition"].ToString();
                var usersalary = post["usersalary"].ToString();
                var userlocation = post["userlocation"].ToString();
                var usermessage = post["usermessage"].ToString();

                string gmailid = ConfigurationManager.AppSettings["emailid"].ToString();
                string password = ConfigurationManager.AppSettings["password"].ToString();
                NetworkCredential nc = new NetworkCredential(gmailid, password);
                //System.Security.Authentication.SslProtocols = System.Security.Authentication.SslProtocols.Tls;
                MailAddress from = new MailAddress(gmailid, "The Fair Deal Team");
                MailAddress to = new MailAddress("mfurquankhan7@gmail.com", "Furquan Khan");
                MailMessage mailMessage2 = new MailMessage(from, to);
                mailMessage2.Body = @"<h1>This is the body</h1>";
                mailMessage2.Subject = "This is the subject";
                mailMessage2.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Credentials = nc;
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Send(mailMessage2);

                return retVal;
            }
            catch(Exception ex)
            {
                return ex.Message;

            }
            //catch(WebException wex)
            //{
            //    return wex.Message;
            //    return wex.StackTrace;

            //}
        }

        public string SendEmail(HttpContext context)
        {
            var post = jsondata(context);
            var email = post;
            //var 
            //MailMessage m = new MailMessage("Uma<test@gmail.com>", To);
            //m.Subject = Subject;
            //m.Body = Body;
            //m.IsBodyHtml = true;
            //m.From = new MailAddress(From);

            //m.To.Add(new MailAddress(To));
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";

            //NetworkCredential authinfo = new NetworkCredential("test@gmail.com", "password");
            //smtp.UseDefaultCredentials = false;
            //smtp.Credentials = authinfo;
            //smtp.Send(m);
            //return true;
            return success(post.ToString());
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}