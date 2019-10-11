using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSafeID
{
    class StudentPersonalInfo
    {        
        public static List<StudentPersonalInfo> Students { get; set; }

        public string Name { get; set; }

        public int ID
        {
            get;
            set;
        }
        
        public string StudentID
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Grade
        {
            get;
            set;
        }

        public string Teacher
        {
            get;
            set;
        }

        public string Image
        {
            get;
            set;
        }

        public StudentPersonalInfo()
        {

        }        
    }
}
