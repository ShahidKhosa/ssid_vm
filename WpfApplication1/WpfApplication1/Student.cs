using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolSafeID
{
    class Student
    {
        private static string _imageName = "";

        public static string ImagePath
        {
            get
            {
                return Helper.GetPath("\\" + APIManager.KioskSettings["job_id"]) + "\\" + _imageName;
            }
            set
            {
                _imageName = value;
            }
        }
            //=> 

        public static int ID
        {
            get;
            set;
        }

        public static int JobID
        {
            get;
            set;
        }

        public static string StudentID
        {
            get;
            set;
        }

        public static string FirstName
        {
            get;
            set;
        }

        public static string LastName
        {
            get;
            set;
        }

        public static string Grade
        {
            get;
            set;
        }

        public static string Teacher
        {
            get;
            set;
        }

        public static string Image
        {
            get;
            set;
        }

        public static string BarcodeData
        {
            get;
            set;
        }

        public static string OfficeUseOnlyPassword
        {
            get;
            set;
        }

        public static int IsVisitor
        {
            get;
            set;
        }

        public static bool IsVerified
        {
            get;
            set;
        }

        public static bool VisitorHasNewImage
        {
            get;
            set;
        }

        public static bool IsOfficeUseOnly
        {
            get;
            set;
        }

        public static bool DigitalPass
        {
            get;
            set;
        }

        public static string EmailAddress
        {
            get;
            set;
        }

        public static string PhoneNumber
        {
            get;
            set;
        }

        public static string CheckinOption
        {
            get;
            set;
        }

        public static string CheckinOptionNumber
        {
            get;
            set;
        }

        public static string Destination
        {
            get;
            set;
        }

        public static DateTime VisitDateTime
        {
            get;
            set;
        }

        public static string PassURL
        {
            get;
            set;
        }

        public static string LiveImage
        {
            get;
            set;
        }

        public static string LicenseNo
        {
            get;
            set;
        }

        public static string Address
        {
            get;
            set;
        }

        public static string City
        {
            get;
            set;
        }

        public static string State
        {
            get;
            set;
        }

        public static string Zip
        {
            get;
            set;
        }

        public static string CheckoutType
        {
            get;
            set;
        }

        

        public static void ResetData()
        {
            ID = 0;
            JobID = 0;
            StudentID = "";
            FirstName = "";
            LastName = "";
            Grade = "";
            BarcodeData = "";
            OfficeUseOnlyPassword = "";
            IsOfficeUseOnly = false;
            DigitalPass = false;
            IsVisitor = 0;
            IsVerified = false;
            EmailAddress = "";
            PhoneNumber = "";            
            CheckinOption = "";
            CheckinOptionNumber = "";
            Destination = "";
            LiveImage = "";
            VisitorHasNewImage = false;
            PassURL = "";
            LicenseNo = "";
            Address = "";
            City = "";
            State = "";
            Zip = "";
            CheckoutType = "";
        }


        public static void SetData(Dictionary<string, object> data)
        {
            ID          = (data.ContainsKey("id") ? Int32.Parse(data["id"].ToString()) : 0);
            JobID       = (APIManager.KioskSettings.ContainsKey("job_id") ? Int32.Parse(APIManager.KioskSettings["job_id"].ToString()) : 0);
            StudentID   = (data.ContainsKey("std_id") ? data["std_id"].ToString() : "0");
            FirstName   = (data.ContainsKey("first_name") ? data["first_name"].ToString() : "");
            LastName    = (data.ContainsKey("last_name") ? data["last_name"].ToString() : "");
            Grade       = (data.ContainsKey("grade") ? data["grade"].ToString() : "");            
            LiveImage   = (data.ContainsKey("image") ? data["image"].ToString() : "");
            ImagePath   = Guid.NewGuid() + ".jpg";

            if (ID > 0 && LiveImage != string.Empty)
            {
                APIManager.DownloadFile(string.Format("/assets/membership/{0}/{1}", JobID, LiveImage), ImagePath, 0);
            }
            else
            {
                APIManager.DownloadFile("/assets/membership/placeholder.jpg", ImagePath, 0);                
            }
        }


        public static void SaveStudent(RestRequest request)
        {
            request.AddParameter("id", ID);
            request.AddParameter("job_id", (JobID > 0 ? JobID : APIManager.KioskSettings["job_id"]));
            request.AddParameter("std_id", StudentID);
            request.AddParameter("school", APIManager.KioskSettings["school"]);
            request.AddParameter("multi_print", APIManager.KioskSettings["print_student_multiple_badges"]);
            request.AddParameter("reason", CheckinOption);
            request.AddParameter("reason_no", CheckinOptionNumber);                                                        
        }

    }
}
