using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//***************************************************************************************
//*Reference for educational purposes-build MVC
//*Title: Blog Project 2
//*Author: Christine Bittle
//* Date: Nov 5, 2020
//* Version :   https://github.com/christinebittle/BlogProject_2/commit/da1b948c83a10c5380621834412b9179a878ace0
//*Availability: https://github.com/christinebittle/BlogProject_2
//*
//***************************************************************************************/
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

        //Thefollowing fields define course(s) taught by Teacher
        public string ClassesCode;
        public string ClassesName;

    }
}