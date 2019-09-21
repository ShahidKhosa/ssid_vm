using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Extensions;

namespace SchoolSafeID
{
    class APIManager
    {
        private static int logoNumber = 0;

        private static bool IsSandBox = true;

        public static string LogoPath
        {
            get
            {
                return String.Format("{0}\\school_logo_{1}.png", Helper.GetPath("\\" + values["job_id"]), logoNumber);
            }
        }

        public static Dictionary<string, object> values = null;

        public static string baseURL = "https://www.schoolsafeid.com";

        public void SendRequest()
        {
            var client = new RestClient(baseURL);
            //client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("resource/{id}");
            request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile("file", path);

            // execute the request
            var response = client.Post(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //var response2 = client.Post<Person>(request);
            //var name = response2.Data.Name;


        }


        public static void DownloadLogo(string url)
        {
            var client = new RestClient(url);            
            var request = new RestRequest(Method.GET);

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            client.DownloadData(request).SaveAs(LogoPath);            
        }


        public static Dictionary<string, object> GetKioskSettings()
        {
            if(values != null)
            {
                return values;
            }

            ++logoNumber;
            string endURL = Properties.Settings.Default.school_url + "&api=vm";

            if(IsSandBox)
            {
                endURL = endURL.Replace("www", "dev");
            }

            var client = new RestClient(endURL);
            //var client = new RestClient("https://dev.schoolsafeid.com/visitor-management&s=test-school&api=vm");            
            var request = new RestRequest(Method.GET);
            
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            IRestResponse response = client.Get(request);

            string data = response.Content;
            values = SimpleJson.DeserializeObject<Dictionary<string, object>>(data);

            //foreach (var item in values)
            //{
            //    Console.WriteLine(item.Key + " : " + item.Value);                
            //}
            DownloadLogo(values["logo"].ToString());

            //client.DownloadData(request).SaveAs(LogoPath);
            return values;
        }

    }
}
