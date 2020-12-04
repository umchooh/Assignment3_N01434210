using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Web.Http.Cors;
using Assignment3_N01434210.Models;  //Add reference to access the models including SchoolDBContect.cs and Teacher.cs
using MySql.Data.MySqlClient; //Add MySQL connection reference to connect to database 

//***************************************************************************************
//*Reference for educational purposes-build MVC
//*Title: Blog Project 2
//*Author: Christine Bittle
//* Date: Nov 5, 2020
//* Version :   https://github.com/christinebittle/BlogProject_2/commit/da1b948c83a10c5380621834412b9179a878ace0
//*Availability: https://github.com/christinebittle/BlogProject_2
//***************************************************************************************/

namespace Assignment3_N01434210.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The SchoolDbcontext class allows us to access our MySQL Database with 'secret' properties.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of Teachers from MySQL database.
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of Teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();
            //v2: modified SQL to accept lowercase during search in TeacherList
            //SQL QUERY- request data of interest to MySQL databse server
            //v1: cmd.CommandText = "Select * from Teachers";
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather requested 'Result Set' of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index INDIVIDUALLY with appropriate data type
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                decimal Salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }


            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }
        ///<example>Select a particular teacher from ListTeacher(/Teacher/List) and click, particular teacher's infromation will rendered on /Teacher/show/{Teacherid}.</example>
        /// <summary>
        /// This method will access school database connection and return a full 'ResultSet', however, we only required a single teacher's information, so parameter {id} placed to be more specified.
        /// </summary>
        /// <param name="Teacherid">represent a unique id for each teacher while looping thru the school database (id is added when rendering the data, to increase its specificity of teacherid).</param>
        /// <returns>One specific teacher's profile</returns>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{Teacherid}")]
        public Teacher FindTeacher(int Teacherid)
        {
            Teacher OneTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY : request information about teacher and classes information
            //Assignment 3:USING GROUP_CONCAT to group class name and class code based on same teacherid in MySQL (DO NOT use groupconcat, use loop in List to listed out multiple courses)
            //Assignment 3:cmd.CommandText = "Select teachers.*, GROUP_CONCAT(classes.classcode)AS 'Class_Code', GROUP_CONCAT(classes.classname) AS 'Class_Name' from teachers join classes on classes.teacherid = teachers.teacherid  where teachers.teacherid =" + Teacherid;

            cmd.CommandText = "Select teachers.*, classes.classcode, classes.classname from teachers join classes on teachers.teacherid = classes.teacherid where teachers.teacherid = @id";
            cmd.Parameters.AddWithValue("@id", Teacherid);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            OneTeacher.ClassTeachBy = new List<Class> { };

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                decimal Salary = (decimal)ResultSet["salary"];

                string ClassCode = ResultSet["classcode"].ToString(); 
                string ClassName = ResultSet["classname"].ToString(); 

                OneTeacher.TeacherId = TeacherId;
                OneTeacher.TeacherFname = TeacherFname;
                OneTeacher.TeacherLname = TeacherLname;
                OneTeacher.EmployeeNumber = EmployeeNumber;
                OneTeacher.HireDate = HireDate;
                OneTeacher.Salary = Salary;

               
                Class NewClass = new Class();
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;


                OneTeacher.ClassTeachBy.Add(NewClass);

            }
            Connection.Close();

            return OneTeacher;
        }

        /// <summary>
        /// Deletes a Teacherfrom the connected MySQL Database if the ID of that teacher exists. Does NOT maintain relational integrity. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the Teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/5</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

        /// <summary>
        /// Adds an Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Sam",
        ///	"TeacherLname":"Smith",
        ///	"Hire Date":"20 Nov 2021",
        ///	"Salary":"55.20",
        ///	"Employee Number": "T123"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, hiredate, salary, employeenumber) values (@TeacherFname,@TeacherLname, CURRENT_DATE(), @Salary, @EmployeeNumber)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
