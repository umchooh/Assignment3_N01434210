using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using Assignment3_N01434210.Models;
using MySql.Data.MySqlClient;
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
    public class ClassesDataController : ApiController
    {
        // The SchoolDbcontext class allows us to access our MySQL Database with 'secret' properties.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the classes table of our school database.
        /// <summary>
        /// Returns a list of Classes from MySQL
        /// </summary>
        /// <example>GET api/ClassesData/ListClasses</example>
        /// <returns>
        /// A list of classes (class code and class name)
        /// </returns>
        [HttpGet]
        [Route("api/ClassesData/ListClasses")]
        public IEnumerable<Class> ListClasses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY- request data of interest to MySQL databse server
            cmd.CommandText = "Select * from Classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Class> Classes = new List<Class> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index INDIVIDUALLY with appropriate data type

                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                string ClassName = ResultSet["classname"].ToString();

                Class EachClass = new Class();
                EachClass.TeacherId = TeacherId;
                EachClass.ClassId = ClassId;
                EachClass.ClassCode = ClassCode;
                EachClass.StartDate = StartDate;
                EachClass.FinishDate = FinishDate;
                EachClass.ClassName = ClassName;

                //Add Each Class to the List
                Classes.Add(EachClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of classes
            return Classes;
        }
        ///<example>Select a particular class from ListClasses (/Classes/List) and click,full infromation of the class will rendered on /Classes/show/{Classid}.</example>
        /// <summary>
        /// This method will access school database connection and return a full 'ResultSet', however, we only required a class's information, so parameter {id} placed to be more specified.
        /// </summary>
        /// <param name="Classid">represent a unique id for each classes while looping thru the school database (id is added when rendering the data, to increase its specificity of classid).</param>
        /// <returns>One specific teacher's profile</returns>
        [HttpGet]
        [Route("api/ClassesData/FindClass/{Classid}")]
        public Class FindClass(int Classid)
        {
            Class EachClass = new Class();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY :
            cmd.CommandText = "Select classes.*, teachers.teacherfname, teachers.teacherlname from classes JOIN teachers ON classes.teacherid = teachers.teacherid where classid = " + Classid;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                Int64 TeacherId = (Int64)ResultSet["teacherid"];
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                string ClassName = ResultSet["classname"].ToString();
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();

                EachClass.TeacherId = TeacherId;
                EachClass.ClassId = ClassId;
                EachClass.ClassCode = ClassCode;
                EachClass.StartDate = StartDate;
                EachClass.FinishDate = FinishDate;
                EachClass.ClassName = ClassName;
                EachClass.TeacherFname = TeacherFname;
                EachClass.TeacherLname = TeacherLname;
            }


            return EachClass;
        }
    }
}
