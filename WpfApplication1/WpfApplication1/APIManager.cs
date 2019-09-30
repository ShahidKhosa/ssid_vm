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
using System.Windows.Forms;

namespace SchoolSafeID
{
    class APIManager
    {
        private static int logoNumber = 0;

        private static bool InProgress = false;

        private static bool IsSandBox = true;

        public static string LogoPath
        {
            get
            {
                return String.Format("{0}\\school_logo_{1}.png", Helper.GetPath("\\" + KioskSettings["job_id"]), logoNumber);
            }
        }

        public static Dictionary<string, object> KioskSettings = null;

        public static string BaseURL
        {
            get
            {
                return String.Format("https://{0}.schoolsafeid.com", (IsSandBox ? "dev" : "www"));
            }
        }

        public static string Username
        {
            get
            {
                return "wpf_client";
            }
        }

        public static string Password
        {
            get
            {
                return "schoolSafeid";
            }
        }

        public void SendRequest()
        {
            var client = new RestClient(BaseURL);
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);

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


        public static async void DownloadFile(string url)
        {
            var client      = new RestClient(BaseURL + url);
            var request     = new RestRequest(Method.GET);            
            var response    = await client.ExecuteTaskAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Unable to download file");
            }
                
            response.RawBytes.SaveAs(Visitor.CroppedImagePath);
        }


        public static Dictionary<string, object> GetKioskSettings()
        {            
            if(!InProgress && KioskSettings == null)
            {
                InProgress = true;
                ++logoNumber;                

                var client = new RestClient(BaseURL);
                client.Authenticator = new HttpBasicAuthenticator(Username, Password);

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var request = new RestRequest("/api/class_api.php");
                request.AddParameter("action", "home_page"); 
                request.AddParameter("job_no", Properties.Settings.Default.job_no);

                // execute the request
                var response = client.Post(request);
                KioskSettings = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);

                DownloadLogo(KioskSettings["logo"].ToString());


                //foreach (var item in values)
                //{
                //    Console.WriteLine(item.Key + " : " + item.Value);                
                //}
                //client.DownloadData(request).SaveAs(LogoPath);

                InProgress = false;                
            }

            return KioskSettings;
        }


        public static void GetVisitorData()
        {
            var client = new RestClient(BaseURL);
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);

            var request = new RestRequest("/api/class_api.php");
            request.AddParameter("action", "manage_preview"); // adds to POST or URL querystring based on Method
            request.AddParameter("IsAPI", "wpf_client"); // adds to POST or URL querystring based on Method
            request.AddParameter("job_id", KioskSettings["job_id"]); // adds to POST or URL querystring based on Method
            request.AddParameter("barcode_data", Visitor.BarcodeData); // replaces matching token in request.Resource

            // execute the request
            var response = client.Post(request);            
            Dictionary <string, object> result = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);
            Visitor.SetData(result);            
        }


        public static void SendVisitorData()
        {            
            var client = new RestClient(BaseURL);
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);

            var request = new RestRequest("/api/class_api.php", Method.POST);
            request.AddParameter("action", "save_visitor"); // adds to POST or URL querystring based on Method                        
            Visitor.SaveVisitor(request);

            // execute the request
            client.ExecuteAsync(request, (response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //upload successfull
                    MessageBox.Show("Upload completed succesfully...\n" + response.Content);
                }
                else
                {
                    //error ocured during upload
                    MessageBox.Show(response.StatusCode + "\n" + response.StatusDescription);
                }
            });
        }


        public static void SendLogData()
        {
            var client = new RestClient(BaseURL);
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);

            var request = new RestRequest("/api/class_api.php");
            request.AddParameter("action", "save_log"); // adds to POST or URL querystring based on Method                                    

            // execute the request
            var response = client.Post(request);
            Dictionary<string, object> data = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);            
        }


        public static bool VerifyVisitorData()
        {
            bool result = false; 
            var client = new RestClient(BaseURL);
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);

            var request = new RestRequest("/api/class_api.php", Method.POST);
            request.AddParameter("action", "verify_visitor");
            request.AddParameter("first_name", Visitor.FirstName);
            request.AddParameter("last_name", Visitor.LastName);
            request.AddParameter("date_birth", Visitor.DateOfBirth);
            request.AddParameter("is_visitor", Visitor.IsVisitor);
            request.AddParameter("job_id", KioskSettings["job_id"]);
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Dictionary<string, object> data = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);

                Visitor.IsVerified = result = Boolean.Parse(data["success"].ToString());
            }
            else
            {
                result = false;
                //error ocured during upload
                //MessageBox.Show(response.StatusCode + "\n" + response.StatusDescription);
            }

            return result;
        }

    }
}
