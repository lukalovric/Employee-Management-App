document.addEventListener("DOMContentLoaded", function() {
    const employeeId = new URLSearchParams(window.location.search).get('id');
    const employees = JSON.parse(localStorage.getItem("employees"));
    const employee = employees.find(emp => emp.id == employeeId);


    const form = document.getElementById("update-employee-form");
    form.firstName.value = employee.firstName;
    form.lastName.value = employee.lastName;
    form.position.position = employee.position;
    form.salary.value = employee.salary;
    form.createdAt.value = employee.createdAt;

    form.addEventListener("submit", function(event) {

        employee.firstName = form.firstName.value;
        employee.lastName = form.lastName.value;
        employee.position = form.position.value;
        employee.salary = form.salary.value;
        employee.createdAt = form.createdAt.value;

        localStorage.setItem("employees", JSON.stringify(employees));

        window.location.href = 'index.html';
    });
});
