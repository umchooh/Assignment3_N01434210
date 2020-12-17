﻿﻿// AJAX for author Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml


function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : http://localhost:61243/api/TeacherData/AddTeacher/
	//with POST data of teacher's information etc.

	var URL = "http://localhost:61243/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var Hiredate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;


	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"Hiredate": Hiredate,
		"Salary": Salary,
		"EmployeeNumber": EmployeeNumber
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.

			
		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}

function UpdateTeacher(TeacherId) {

	//goal: send a request which looks like this:
	//POST : http://localhost:61243/api/TeacherData/UpdateTeacher/{id}
	//with POST data of teachername, bio, email, etc.

	var URL = "http://localhost:61243/api/TeacherData/UpdateTeacher/" + TeacherId;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var Hiredate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"Hiredate": Hiredate,
		"Salary": Salary,
		"EmployeeNumber": EmployeeNumber
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}