using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3_N01434210.Models
{
    public class Student
    {
        //The following fields define a Student
        // Note: in MySQL database, the StudentId is 'bigint' type; for casting purposes, data type Int32 should be use.
        public UInt32 StudentId;
        public string StudentFname;
        public string StudentLname;
        public string StudentNumber;
        public DateTime EnrolDate;    
    }
}