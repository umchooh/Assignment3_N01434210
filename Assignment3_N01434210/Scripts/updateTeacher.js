//This is a script for form validation before submit update form
function validateTeacher() {
    var f_TeacherFname = document.getElementById("TeacherFname");
    var f_TeacherLname = document.getElementById("TeacherLname");
    var f_EmployeeNumber = document.getElementById("EmployeeNumber");
    var regEn = /^[Tt][0-9]{3}$/;
    var f_HireDate = document.getElementById("HireDate");
    var f_Salary = document.getElementById("Salary");

    var isFormValid = true;
    if (f_TeacherFname.value === "" || f_TeacherFname.value === null) {
        f_TeacherFname.style.borderColor = "red";
        f_TeacherFname.focus();
        isFormValid = false;
    }
    if (f_TeacherLname.value === "" || f_TeacherLname.value === null) {
        f_TeacherLname.style.borderColor = "red";
        f_TeacherLname.focus();
        isFormValid = false;
    }
    if (!regEn.test(f_EmployeeNumber.value)) {
        f_EmployeeNumber.style.borderColor = "red";
        f_EmployeeNumber.focus();
        isFormValid = false;
    }
    if (f_HireDate.value === "" || f_HireDate.value === null) {
        f_HireDate.style.borderColor = "red";
        f_HireDate.focus();
        isFormValid = false;
    }
    if (f_Salary.value === "" || f_Salary.value === null) {
        f_Salary.style.borderColor = "red";
        f_Salary.focus();
        isFormValid = false;
    }
    return isFormValid;
}