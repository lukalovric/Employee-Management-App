document.addEventListener("DOMContentLoaded", function() {
    const employeeList = document.getElementById("employee-list");
    const employees = JSON.parse(localStorage.getItem("employees")) || [];

    function getEmployees() {
        employeeList.innerHTML = '';
        employees.forEach(employee => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${employee.firstName}</td>
                <td>${employee.lastName}</td>
                <td>${employee.position}</td>
                <td>$${employee.salary}</td>
                <td>${employee.createdAt}</td>
                <td>
                    <button class="btn edit" data-id="${employee.id}">Edit</button>
                    <button class="btn delete" data-id="${employee.id}">Delete</button>
                    <button class="btn view" data-id="${employee.id}">View</button>
                </td>
            `;
            employeeList.appendChild(row);
        });
    }

    employeeList.addEventListener("click", function(event) {
        if (event.target.classList.contains("edit")) {
            const id = event.target.getAttribute("data-id");
            window.location.href = `update.html?id=${id}`;
        }

        if (event.target.classList.contains("delete")) {
            const id = event.target.getAttribute("data-id");
            const index = employees.findIndex(employee => employee.id == id);
            if (index !== -1) {
                employees.splice(index, 1);
                localStorage.setItem("employees", JSON.stringify(employees));
                getEmployees();
            }
        }
        if (event.target.classList.contains("view")) {
            const id = event.target.getAttribute("data-id");
            window.location.href = `view.html?id=${id}`;
        }
    });

    getEmployees();
});
