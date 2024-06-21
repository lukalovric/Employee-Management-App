document.addEventListener("DOMContentLoaded", function() {
    const employeeDetails = document.getElementById("employee-details");
    const employees = JSON.parse(localStorage.getItem("employees"));
    const employeeId = new URLSearchParams(window.location.search).get('id');
    const employee = employees.find(emp => emp.id == employeeId);

    if (employee) {
        document.getElementById("first-name").textContent = employee.firstName;
        document.getElementById("last-name").textContent = employee.lastName;
        document.getElementById("position").textContent = employee.position; 
        document.getElementById("salary").textContent = employee.salary;
        document.getElementById("created-at").textContent = employee.createdAt; 
    } else {
        employeeDetails.innerHTML = "<h2>Employee not found</h2>";
    }
});


