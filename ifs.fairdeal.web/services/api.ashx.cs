using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
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
            else if(method == "SendEmail")
                retval = SendEmail(context);
            
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