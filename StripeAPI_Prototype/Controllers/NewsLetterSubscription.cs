using Newtonsoft.Json.Linq;
using StripeAPI_Prototype.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StripeAPI_Prototype.Controllers
{
    public class NewsLetterSubscription
    {
        public NewsLetterSubscription() { }

        public int UseWebHook(JObject param)
        {

            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(Environment.GetEnvironmentVariable("newsLetterWebhookURL"));
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Utils.getInstance().newsLetterWebhookURL);


            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                string json = param.ToString();

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            return 0;
        }


    }
}
