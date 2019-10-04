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
using System.Runtime.Serialization;
using System.IO;
using PDFtoPrinter;

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

        public static string BadgePath
        {
            get
            {
                return "D:\\TestPrint\\visitor-card.pdf";
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



        public static void Download(string url, string path)
        {
            var client = new RestClient(url);            
            var request = new RestRequest(Method.GET);

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            client.DownloadData(request).SaveAs(path);            
        }



        public static async void DownloadFile(string url, string path, int printBadge = 0)
        {
            var client      = new RestClient(BaseURL + url);
            var request     = new RestRequest(Method.GET);            
            var response    = await client.ExecuteTaskAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.RawBytes.SaveAs(path);

                if (printBadge == 1)
                {
                    var printWrapper = new PDFtoPrintWrapper();
                    await printWrapper.Print(BadgePath, AppSettings.PrinterName);
                }
            }                            
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
                request.AddParameter("job_no", AppSettings.JobNo);

                // execute the request
                var response = client.Post(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    KioskSettings = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);

                    if (KioskSettings.ContainsKey("logo"))
                    {
                        Download(KioskSettings["logo"].ToString(), LogoPath);
                    }
                }

                InProgress = false;                
            }

            return KioskSettings;
        }


        public static void GetVisitorData(string Action = "manage_preview")
        {
            var client = new RestClient(BaseURL);
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);

            var request = new RestRequest("/api/class_api.php");
            request.AddParameter("action", Action); // adds to POST or URL querystring based on Method            
            request.AddParameter("job_id", KioskSettings["job_id"]); // adds to POST or URL querystring based on Method
            request.AddParameter("barcode_data", Visitor.BarcodeData); // replaces matching token in request.Resource

            // execute the request
            var response = client.Post(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Dictionary<string, object> result = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);

                if (result.ContainsKey("success"))
                {
                    if (bool.Parse(result["success"].ToString()))
                    {
                        Visitor.SetData(result);
                    }
                    else if (result.ContainsKey("message"))
                    {
                        MessageBox.Show(result["message"].ToString());
                    }
                    else
                    {
                        MessageBox.Show("Error! Please try again.");
                    }
                }
            }                           
        }


        public static void SendVisitorData(int PrintSticker)
        {
            var client = new RestClient(BaseURL);
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);

            var request = new RestRequest("/api/class_api.php", Method.POST);
            request.AddParameter("action", "save_visitor"); // adds to POST or URL querystring based on Method                        
            request.AddParameter("print_sticker", PrintSticker);
            Visitor.SaveVisitor(request);

            if(PrintSticker == 1)
            {
                SendVisitorDataSync(request, client);
            }
            else
            {
                SendVisitorDataAsync(request, client);
            }
        }


        public static void SendVisitorDataAsync(RestRequest request, RestClient client)
        {
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


        public static void SendVisitorDataSync(RestRequest request, RestClient client)
        {
            // execute the request
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                //upload successfull
                Dictionary<string, object> result = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);

                if (result.ContainsKey("success"))
                {
                    if (result.ContainsKey("sticker"))
                    {
                        DownloadFile(result["sticker"].ToString(), BadgePath, 1);
                    }
                }
                else
                {
                    MessageBox.Show("Error! Please try again.");
                }
            }
            else
            {
                //error ocured during upload                
                MessageBox.Show(response.StatusCode + "\n" + response.StatusDescription);
            }
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
                try
                {
                    Dictionary<string, object> data = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);

                    Visitor.IsVerified = result = Boolean.Parse(data["success"].ToString());
                }
                catch(SerializationException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                Visitor.IsVerified = result = false;
                //error ocured during upload
                //MessageBox.Show(response.StatusCode + "\n" + response.StatusDescription);
            }

            return result;
        }


        public static void DownloadPDF(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseURL + url);

            request.ContentType = "application/pdf;charset=UTF-8";
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                BinaryReader bin = new BinaryReader(response.GetResponseStream());

                byte[] buffer = bin.ReadBytes((Int32)response.ContentLength);

                using (Stream writer = File.Create("D:\\MyPDF.pdf"))
                {
                    writer.Write(buffer, 0, buffer.Length);
                    writer.Flush();
                }
            }
        }


        public static void Signout()
        {
            var client = new RestClient(BaseURL);
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);

            var request = new RestRequest("/api/class_api.php", Method.POST);
            request.AddParameter("action", "api_signout");
            request.AddParameter("id", Visitor.ID);
            request.AddParameter("job_id", APIManager.KioskSettings["job_id"]);
            request.AddParameter("barcode_data", Visitor.BarcodeData);
            request.AddParameter("check_out_type", Visitor.CheckoutType);

            SendVisitorDataAsync(request, client);
        }
    }
}
