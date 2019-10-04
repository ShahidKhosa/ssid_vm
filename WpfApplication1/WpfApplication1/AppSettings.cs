using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSafeID
{
    class AppSettings
    {
        public static string JobNo { get; set; } = "18872";


        public static string PrinterName { get; set; } = "DYMO LabelWriter 450 Turbo";


        public static void SaveAppSettings()
        {            
            string fileName     = String.Format("{0}\\app_settings.json", Helper.GetPath());

            JObject appSettings = new JObject(
            new JProperty("JobNo", JobNo),
            new JProperty("PrinterName", PrinterName));

            File.WriteAllText(fileName, appSettings.ToString());
        }


        public static void GetAppSettings()
        {
            string fileName = String.Format("{0}\\app_settings.json", Helper.GetPath());

            if(File.Exists(fileName))
            {
                JObject jObject = JObject.Parse(File.ReadAllText(fileName));

                if(jObject.Count > 0)
                {
                    foreach (JProperty property in jObject.Properties())
                    {
                        if(property.Name.Equals("JobNo"))
                        {
                            JobNo = property.Value.ToString();                                        
                        }
                        if (property.Name.Equals("PrinterName"))
                        {
                            PrinterName = property.Value.ToString();
                        }                        
                    }
                }
            }            
        }
    }
}
