using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using Assignment3_N01434210.Models; //Add reference to access the models including SchoolDBContect.cs and Student.cs
using MySql.Data.MySqlClient; //Add MySQL connection reference to connect to database 
//***************************************************************************************
//*Reference for educational purposes-build MVC
//*Title: Blog Project 2
//*Author: Christine Bittle
//* Date: Nov 5, 2020
//* Version :   https://github.com/christinebittle/BlogProject_2/commit/da1b948c83a10c5380621834412b9179a878ace0
//*Availability: https://github.com/christinebittle/BlogProject_2
//*
//***************************************************************************************/
namespace Assignment3_N01434210.Controllers
{
    public class StudentDataController : ApiController
    {
        // The SchoolDbcontext class allows us to access our MySQL Database with 'secret' properties.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the students table of our school database.
        /// <summary>
        /// Returns a list of Students from MySQL database.
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of Students (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Student> ListStudents()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY - request data of interest to MySQL databse server
            cmd.CommandText = "Select * from Students";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Students Names
            List<Student> Students = new List<Student>{};

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index INDIVIDUALLY with appropriate data type
                //Datatype matters, studentid in MySQL showed 'unsigned', it required different casting type, such Uint32.
                UInt32 StudentId = (UInt32)ResultSet["studentid"];
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];
                string StudentNumber = ResultSet["studentnumber"].ToString();

                Student EachStudent = new Student();
                EachStudent.StudentId = StudentId;
                EachStudent.StudentFname = StudentFname;
                EachStudent.StudentLname = StudentLname;
                EachStudent.StudentNumber = StudentNumber;
                EachStudent.EnrolDate = EnrolDate;

                //Add the Student Name to the List
                Students.Add(EachStudent);

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of student 
            return Students;
        }
        ///<example>Select a particular student from ListStudent(/Student/List.cshtml) and click, his/her full infromation will rendered on /Student/show/{id}.</example>
        /// <summary>
        /// This method will access school database connection and return a full 'ResultSet', however, we only required a single student's information, so parameter {id} placed to be more specified.
        /// </summary>
        /// <param name="id">represent a unique id for each studentId while looping thru the school database (id is added when rednering the data, to increase its specificity of studentid).</param>
        /// <returns>One specific student's profile</returns>
        [HttpGet]
        public Student FindStudent(int Studentid)
        {
            Student NewStudent = new Student();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY - request data of interest to MySQL databse server
            cmd.CommandText = "Select * from Students where studentid = " +Studentid;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index individually
                //Note: studentid in databse has 'Unsigned' attributes, UInt32 data type was use to cast the data here.
                UInt32 StudentId = (UInt32)ResultSet["studentid"];
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];
                string StudentNumber = ResultSet["studentnumber"].ToString();

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;
            }
            return NewStudent;
        }
    }
}
