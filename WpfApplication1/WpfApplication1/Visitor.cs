﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolSafeID
{
    class Visitor
    {
        private static string _visitorImage = "";

        private static string _visitorFullImage = "";

        public static string FullImagePath
        {
            get
            {
                return Helper.GetPath("\\" + APIManager.KioskSettings["job_id"]) + "\\" + _visitorFullImage;
            }
            set
            {
                _visitorFullImage = value;
            }
        }                        

        public static string CroppedImagePath
        {
            get
            {
                return String.Format("{0}\\{1}", Helper.GetPath("\\" + APIManager.KioskSettings["job_id"]), _visitorImage);
            }
            set
            {
                _visitorImage = value;
            }
        }            

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

        public static int IsVisitor
        {
            get;
            set;
        }

        public static int IsVerified
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


        public static string CheckoutType
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
            IsVisitor = 0;
            IsVerified = -1;
            EmailAddress = "";
            PhoneNumber = "";            
            CheckinOption = "";
            CheckinOptionNumber = "";
            Destination = "";
            VisitorLiveImage = "";
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
            ID = (data.ContainsKey("id") ? Int32.Parse(data["id"].ToString()) : 0);
            JobID = (APIManager.KioskSettings.ContainsKey("job_id") ? Int32.Parse(APIManager.KioskSettings["job_id"].ToString()) : 0);
            UNIQID = (data.ContainsKey("uniqid") ? data["uniqid"].ToString() : Guid.NewGuid().ToString());
            FirstName = (data.ContainsKey("first_name") ? data["first_name"].ToString() : "");
            LastName = (data.ContainsKey("last_name") ? data["last_name"].ToString() : "");
            DateOfBirth = (data.ContainsKey("dob") ? data["dob"].ToString() : "");
            PassURL = (data.ContainsKey("pass_url") ? data["pass_url"].ToString() : "");
            LicenseNo = (data.ContainsKey("license_no") ? data["license_no"].ToString() : "");
            Address = (data.ContainsKey("address") ? data["address"].ToString() : "");
            City = (data.ContainsKey("city") ? data["city"].ToString() : "");
            State = (data.ContainsKey("state") ? data["state"].ToString() : "");
            Zip = (data.ContainsKey("zip") ? data["zip"].ToString() : "");
            Destination = (data.ContainsKey("destination") ? data["destination"].ToString() : "");
            CheckoutType = (data.ContainsKey("check_out_type") ? data["check_out_type"].ToString() : "");
            VisitorLiveImage = (data.ContainsKey("image") ? data["image"].ToString() : "");
            IsOfficeUseOnly = false;
            OfficeUseOnlyPassword = "";

            if (ID > 0 && VisitorLiveImage != string.Empty)
            {
                CroppedImagePath = Guid.NewGuid() + ".jpg";

                APIManager.DownloadFile(string.Format("/assets/visitors/{0}", VisitorLiveImage), CroppedImagePath, 0);
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
            request.AddParameter("visitor_type", CheckinOptionNumber);            
            request.AddParameter("destination", Destination);
            request.AddParameter("visit_date_time", VisitDateTime);            

            if(VisitorHasNewImage)
            {
                request.AlwaysMultipartFormData = true;
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddFile("file", CroppedImagePath, "image/jpg");
                request.AddParameter("multipart/form-data", Path.GetFileName(CroppedImagePath), ParameterType.RequestBody);
            }                        
        }

    }
}
