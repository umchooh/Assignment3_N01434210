using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3_N01434210.Models
{
    public class Teacher
    {
        //The following fields define a Teacher 
        //(not an object, don't use properties, each of the line below is a field)
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public DateTime HireDate;
        public decimal Salary;
        public string EmployeeNumber;


        //The following fields define course(s) taught by Teacher
        public List<Class> ClassTeachBy;

        //parameter-less constructor function
        public Teacher() { }
        

    }
}