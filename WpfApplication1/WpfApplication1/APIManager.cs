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
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Configuration;

namespace SchoolSafeID
{
    class APIManager
    {
        private static RestClient restClient = null;
        private static string _badgePath = "";

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
                return Helper.GetPath() + "\\"+ _badgePath + ".pdf";
            }
            set
            {
                _badgePath = value;
            }
        }        

        public static string BaseURL
        {
            get
            {
                return String.Format("https://{0}.schoolsafeid.com", (IsSandBox ? "dev" : "www"));
            }
        }

        public static string Username => ConfigurationSettings.AppSettings["Username"];

        public static string Password => ConfigurationSettings.AppSettings["Password"];

        public static Dictionary<string, object> KioskSettings = null;


        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        public static void Download(string url, string path)
        {
            Helper.log.Info("Download School Logo " + url);

            try
            {
                var client  = GetClient(url);
                var request = new RestRequest(Method.GET);
                client.DownloadData(request).SaveAs(path);
            }
            catch(Exception ex)
            {
                Helper.log.Error("Download School Logo Exception ", ex);
            }         
        }
        

        public static async void DownloadFile(string url, string path, int printBadge = 0)
        {
            var client      = GetClient(BaseURL + url, 10000);
            var request     = new RestRequest(Method.GET);            
            var response    = await client.ExecuteTaskAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    response.RawBytes.SaveAs(path);
                }                
                catch(Exception ex)
                {
                    Helper.log.Error("Download File Exception ", ex);
                }

                if (printBadge == 1)
                {
                    var printWrapper = new PDFtoPrintWrapper();
                    await printWrapper.Print(BadgePath, AppSettings.PrinterName);
                }
            } 
            else
            {                
                Helper.log.Error("Download File Status " + response.StatusCode + "\n" + response.StatusDescription);
            }
        }


        private static RestClient GetClient(string fullURL = "", int timeOut = 10000)
        {
            if (restClient != null) return restClient;

            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.MaxServicePointIdleTime = 5000;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

            var client = new RestClient((fullURL == string.Empty ? BaseURL : fullURL))
            {
                Authenticator = new HttpBasicAuthenticator(Username, Password),
                //Timeout = timeOut // 10000 milliseconds == 10 seconds
            };

            client.ConfigureWebRequest((r) => { r.ServicePoint.Expect100Continue = false; r.KeepAlive = true; });

            return client;
        }


        public static Dictionary<string, object> GetKioskSettings()
        {            
            if(!InProgress && KioskSettings == null)
            {
                InProgress  = true;
                var client  = GetClient();
                var request = new RestRequest("/api/class_api.php");
                request.AddParameter("action", "home_page"); 
                request.AddParameter("job_no", AppSettings.JobNo);

                Helper.log.Info("Fetch Kiosk Settings for Job No " + AppSettings.JobNo);

                // execute the request
                var response = client.Post(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    KioskSettings = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);

                    if (KioskSettings.ContainsKey("logo"))
                    {
                        //++logoNumber;
                        Download(KioskSettings["logo"].ToString(), LogoPath);
                    }

                    GetAllStudents("get_all_students");
                }
                else
                {
                    
                    Helper.log.Error("Kiosk Settings Error Message " + response.ErrorMessage, response.ErrorException);
                    Helper.log.Error("Kiosk Settings Status " + response.StatusCode + "\n" + response.StatusDescription);
                }

                InProgress = false;                
            }

            return KioskSettings;
        }


        public static void GetScheduleKioskSettings()
        {
            if (KioskSettings != null)
            {                            
                var client = GetClient();

                var request = new RestRequest("/api/class_api.php", Method.POST);
                request.AddParameter("action", "home_page");
                request.AddParameter("job_no", AppSettings.JobNo);
                request.AddParameter("date_time", KioskSettings["created_datetime"]);
                
                // execute the request
                client.ExecuteAsync(request, (response) =>
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Dictionary<string, object> newKioskSettings = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);

                        if (newKioskSettings.ContainsKey("success"))
                        {
                            if (bool.Parse(newKioskSettings["success"].ToString()))
                            {
                                KioskSettings = newKioskSettings;
                            }
                        }
                    }
                    else
                    {
                        Helper.log.Error("Get Schedule Kiosk Settings Error Message " + response.ErrorMessage, response.ErrorException);
                        Helper.log.Error("Get Schedule Kiosk Settings Status " + response.StatusCode + "\n" + response.StatusDescription);
                    }
                });                          
            }            
        }


        public static void GetVisitorData(string Action = "manage_preview")
        {
            var client  = GetClient();
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
                        Helper.log.Error("Get Visitor Data Error " + request);
                        MessageBox.Show(result["message"].ToString());
                    }
                    else
                    {
                        Helper.log.Error("Get Visitor Data Error " + result);
                        MessageBox.Show("Error! Please try again.");
                    }
                }
            }
            else
            {
                Helper.log.Error("Get Visitor Data Status Error Message " + response.ErrorMessage, response.ErrorException);
                Helper.log.Error("Get Visitor Data Status " + response.StatusCode + "\n" + response.StatusDescription);
            }
        }


        public static void SendVisitorData(int PrintSticker)
        {
            var client  = GetClient();
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
                    //MessageBox.Show("Upload completed succesfully...\n" + response.Content);
                }
                else
                {
                    Helper.log.Error("Send Visitor Data Async Error Message " + response.ErrorMessage, response.ErrorException);
                    Helper.log.Error("Send Visitor Data Async " + response.StatusCode + "\n" + response.StatusDescription);
                    //MessageBox.Show("Error, please try again");
                }
            });
        }


        public static void SendVisitorDataSync(RestRequest request, RestClient client)
        {
            try
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
                            BadgePath = Guid.NewGuid().ToString();
                            Helper.log.Info("Download Badge from " + result["sticker"]);
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
                    Helper.log.Error("Send Visitor Data sync Error Message " + response.ErrorMessage, response.ErrorException);
                    Helper.log.Error("Send Visitor Data Sync " + response.StatusCode + "\n" + response.StatusDescription);
                    MessageBox.Show("Application Error! Please try again.");                    
                }
            }
            catch (Exception ex)
            {
                Helper.log.Error("Send Visitor Data Sync Exception " + ex.Message, ex);
            }
        }


        public static void SendLogData()
        {
            string LogFilePath = Directory.GetCurrentDirectory() + "\\Logs\\SSIDLog.log";
            string LogFileCopy = Directory.GetCurrentDirectory() + "\\Logs\\SSIDLogCopy.log";

            if (KioskSettings != null && File.Exists(LogFilePath))
            {                                
                var client = GetClient();
                var request = new RestRequest("/api/class_api.php", Method.POST);
                request.AddParameter("action", "save_api_log");
                request.AddParameter("job_id", KioskSettings["job_id"]);

                try
                {
                    File.Copy(LogFilePath, LogFileCopy, true);
                    string content = File.ReadAllText(LogFileCopy, Encoding.UTF8);
                    File.Delete(LogFileCopy);
                    request.AddParameter("log_data", content);                    
                }
                catch (System.IO.IOException e)
                {
                    Helper.log.Error("Send Log File " + e.Message, e);                    
                    return;
                }

                //request.AlwaysMultipartFormData = true;
                //request.AddHeader("Content-Type", "multipart/form-data");
                //request.AddFile("file", LogFilePath, "text/plain");
                //request.AddParameter("multipart/form-data", Path.GetFileName(LogFilePath), ParameterType.RequestBody);

                SendVisitorDataAsync(request, client);
            }
        }


        public static int VerifyVisitorData()
        {
            int result = -1;
            var client = GetClient();
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

                    Visitor.IsVerified = result = (Boolean.Parse(data["success"].ToString()) ? 1 : 0);
                }
                catch(SerializationException ex)
                {
                    Helper.log.Error("Verify Visitor Data Exception ", ex);                    
                }
                catch (Exception ex)
                {
                    Helper.log.Error("Verify Visitor Data Exception ", ex);                    
                }                
            }
            else
            {
                Visitor.IsVerified = result = -1;
                Helper.log.Error("Verify Visitor Data Error Message " + response.ErrorMessage, response.ErrorException);
                Helper.log.Error("Verify Visitor Data " + response.StatusCode + "\n" + response.StatusDescription);                
            }

            return result;
        }


        public static void DownloadPDF(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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
            var client = GetClient();
            var request = new RestRequest("/api/class_api.php", Method.POST);
            request.AddParameter("action", "api_signout");
            request.AddParameter("id", Visitor.ID);
            request.AddParameter("job_id", APIManager.KioskSettings["job_id"]);
            request.AddParameter("barcode_data", Visitor.BarcodeData);
            request.AddParameter("check_out_type", Visitor.CheckoutType);

            SendVisitorDataAsync(request, client);
        }


        public static void GetStudentData(string Action = "manage_student_preview")
        {
            var client = GetClient();
            var request = new RestRequest("/api/class_api.php");
            request.AddParameter("action", Action); // adds to POST or URL querystring based on Method            
            request.AddParameter("job_id", KioskSettings["job_id"]); // adds to POST or URL querystring based on Method
            request.AddParameter("barcode_data", Student.BarcodeData); // replaces matching token in request.Resource

            // execute the request
            var response = client.Post(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Dictionary<string, object> result = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content);

                if (result.ContainsKey("success"))
                {
                    if (bool.Parse(result["success"].ToString()))
                    {
                        Student.SetData(result);
                    }
                    else if (result.ContainsKey("error"))
                    {
                        Helper.log.Error("Get Student Data Server Error " + result["error"]);
                        MessageBox.Show(result["error"].ToString());
                    }
                    else
                    {
                        Helper.log.Error("Get Student Data General Error " + request);
                        MessageBox.Show("Error! Please try again.");
                    }
                }
            }
            else
            {
                Helper.log.Error("Get Student Data Error Message " + response.ErrorMessage, response.ErrorException);
                Helper.log.Error("Get Student Data " + response.StatusCode + "\n" + response.StatusDescription);
            }
        }


        public static void SendStudentData(int PrintSticker, string BadgeType)
        {
            var client = GetClient();
            var request = new RestRequest("/api/class_api.php", Method.POST);
            request.AddParameter("action", "complete_student_signin");
            request.AddParameter("print_sticker", PrintSticker);
            request.AddParameter("card_type", BadgeType);
            
            Student.SaveStudent(request);

            if (PrintSticker == 1)
            {
                SendVisitorDataSync(request, client);
            }
            else
            {
                SendVisitorDataAsync(request, client);
            }
        }


        public static void GetAllStudents(string Action = "get_all_students")
        {
            var client = GetClient();
            var request = new RestRequest("/api/class_api.php", Method.POST);
            request.AddParameter("action", Action);
            request.AddParameter("job_id", KioskSettings["job_id"]);

            if(KioskSettings.ContainsKey("show_teacher_name"))
            {
                request.AddParameter("show_teacher_name", KioskSettings["show_teacher_name"]);                
            }
            
            client.ExecuteAsync(request, (response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StudentPersonalInfo.Students = SimpleJson.DeserializeObject<List<StudentPersonalInfo>>(response.Content);                    
                }
                else
                {
                    //error ocured during upload
                    Helper.log.Error("Get All Students Error Message " + response.ErrorMessage, response.ErrorException);
                    Helper.log.Error("Get All Students Request Error " + response.StatusCode + "\n" + response.StatusDescription);                    
                }
            });
        }


        public static void ParentStudentSignout(ObservableCollection<StudentPersonalInfo> selectedStudentsList, SignoutReasons reason)
        {
            var client = GetClient();
            var request = new RestRequest("/api/class_api.php", Method.POST);
            request.AddParameter("action", "parent_student_signout");
            Visitor.SaveVisitor(request);

            request.AddParameter("signout_reason_no", reason.ID);
            request.AddParameter("signout_reason", reason.Reason);

            // Json to post.            
            request.AddParameter("selected_students", JsonConvert.SerializeObject(selectedStudentsList));

            SendVisitorDataAsync(request, client);
        }

    }
}
