using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSafeID
{
    class Visitor
    {
        public static string NextURL
        {
            get;
            set;
        }

        public static string PreviousURL
        {
            get;
            set;
        }

        public static string FullImagePath => Helper.GetPath("\\" + APIManager.KioskSettings["job_id"]) + "\\visitor.jpg";

        public static string CroppedImagePath => String.Format("{0}\\{1}", Helper.GetPath("\\" + APIManager.KioskSettings["job_id"]), "visitor_croped.jpg");

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

        public static string UNIQID
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

        public static string DateOfBirth
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

        public static bool IsVisitor
        {
            get;
            set;
        }

        public static bool IsVerified
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

        public static string VisitorLiveImage
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


        public static void ResetData()
        {
            ID = 0;
            JobID = 0;
            UNIQID = "";
            FirstName = "";
            LastName = "";
            DateOfBirth = "";
            BarcodeData = "";
            OfficeUseOnlyPassword = "";
            IsOfficeUseOnly = false;
            DigitalPass = false;
            IsVisitor = false;
            EmailAddress = "";
            PhoneNumber = "";            
            CheckinOption = "";
            Destination = "";            
        }


        public static void SetData(Dictionary<string, object> data)
        {            
            ID          = (data["id"].ToString() == string.Empty ? 0 : Int32.Parse(data["id"].ToString()));
            JobID       = (APIManager.KioskSettings["job_id"].ToString() == string.Empty ? 0 : Int32.Parse(APIManager.KioskSettings["job_id"].ToString()));
            UNIQID      = (data["uniqid"].ToString() == string.Empty ? Guid.NewGuid().ToString() : data["uniqid"].ToString());
            FirstName   = (data["first_name"].ToString() == string.Empty ? "" : data["first_name"].ToString());
            LastName    = (data["last_name"].ToString() == string.Empty ? "" : data["last_name"].ToString());
            DateOfBirth = (data["dob"].ToString() == string.Empty ? "" : data["dob"].ToString());
            PassURL     = (data["pass_url"].ToString() == string.Empty ? "" : data["pass_url"].ToString());
            LicenseNo   = (data["license_no"].ToString() == string.Empty ? "" : data["license_no"].ToString());
            Address     = (data["address"].ToString() == string.Empty ? "" : data["address"].ToString());
            City        = (data["city"].ToString() == string.Empty ? "" : data["city"].ToString());
            State       = (data["state"].ToString() == string.Empty ? "" : data["state"].ToString());
            Zip         = (data["zip"].ToString() == string.Empty ? "" : data["zip"].ToString());
            Destination = (data["destination"].ToString() == string.Empty ? "" : data["destination"].ToString());

            VisitorLiveImage        = (data["image"].ToString() == string.Empty ? "" : data["image"].ToString());
            IsOfficeUseOnly         = false;                        
            OfficeUseOnlyPassword   = "";

            if(ID > 0 && VisitorLiveImage != string.Empty)
            {                
                APIManager.DownloadFile(string.Format("/assets/visitors/{0}", VisitorLiveImage));
            }
        }


        public static void SaveVisitor(RestRequest request)
        {
            request.AddParameter("id", ID);
            request.AddParameter("job_id", (JobID > 0 ? JobID : APIManager.KioskSettings["job_id"]));
            request.AddParameter("uniqid", UNIQID);
            request.AddParameter("first_name", FirstName);
            request.AddParameter("last_name", LastName);
            request.AddParameter("dob", DateOfBirth);
            request.AddParameter("barcode_data", BarcodeData);
            request.AddParameter("is_office_use_only", IsOfficeUseOnly);
            request.AddParameter("digital_pass", DigitalPass);
            request.AddParameter("is_visitor", IsVisitor);            
            request.AddParameter("email", EmailAddress);
            request.AddParameter("phone", PhoneNumber);
            request.AddParameter("checkin_option", CheckinOption);
            request.AddParameter("destination", Destination);
            request.AddParameter("visit_date_time", VisitDateTime);

            request.AlwaysMultipartFormData = true;
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("file", CroppedImagePath, "image/jpg");
            request.AddParameter("multipart/form-data", Path.GetFileName(CroppedImagePath), ParameterType.RequestBody);            
        }

    }
}
