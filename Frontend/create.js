document.getElementById("create-employee-form").addEventListener("submit", function(event) {
    event.preventDefault();
    
    const employees = JSON.parse(localStorage.getItem("employees")) || [];
    
    const newEmployee = {
        id: this.id.value,
        firstName: this.firstName.value,
        lastName: this.lastName.value,
        position: this.position.value,
        salary: this.salary.value,
        createdAt: this.createdAt.value
    };
    
    employees.push(newEmployee);
    localStorage.setItem("employees", JSON.stringify(employees));
    
    window.location.href = 'index.html';
});
