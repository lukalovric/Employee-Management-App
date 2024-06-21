import React, { useEffect, useState } from 'react';
import './EmployeePage.css';
import MockEmployees from './MockEmployees';
import CreateEmployee from './components/CreateEmployee';
import EmployeeList from './components/EmployeeList';
import UpdateEmployee from './components/UpdateEmployee';

const EmployeePage = () => {
    const [employees, setEmployees] = useState([]);
    const [editingEmployee, setEditingEmployee] = useState(null);

    useEffect(() => {
        const storedEmployees = JSON.parse(localStorage.getItem('employees')) || MockEmployees;
        setEmployees(storedEmployees);
    }, []);

    useEffect(() => {
        localStorage.setItem('employees', JSON.stringify(employees));
    }, [employees]);

    const addEmployee = (employee) => {
        setEmployees([...employees, employee]);
    };

    const deleteEmployee = (id) => {
        setEmployees(employees.filter(employee => employee.id !== id));
    };

    const updateEmployee = (updatedEmployee) => {
        setEmployees(employees.map(employee => 
            employee.id === updatedEmployee.id ? updatedEmployee : employee
        ));
        setEditingEmployee(null);
    };

    const editEmployee = (employee) => {
        setEditingEmployee(employee);
    };

    const cancelEdit = () => {
        setEditingEmployee(null);
    };

    return (
        <div className="container">
            <h1>Employee Management</h1>

            <CreateEmployee onAddEmployee={addEmployee} />

            <EmployeeList employees={employees} onEdit={editEmployee} onDelete={deleteEmployee} />

            {editingEmployee && (
                <UpdateEmployee
                    employee={editingEmployee}
                    onUpdateEmployee={updateEmployee}
                    onCancel={cancelEdit}
                />
            )}
        </div>
    );
};

export default EmployeePage;
