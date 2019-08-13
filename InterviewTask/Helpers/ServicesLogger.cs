using System;
using System.IO;
using System.Net;
using System.Web;

namespace InterviewTask.Helpers
{
    //Will use log4net if have time to change.
    public static class ServicesLogger
    {
        static string path = HttpContext.Current.Server.MapPath("~/App_Data/log.txt");

        public static void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                streamWriter.WriteLine($"{message} : {DateTime.Now} : {host}");
                streamWriter.Close();
            }
        }
    }
}