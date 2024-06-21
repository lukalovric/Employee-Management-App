import React from 'react';
import EmployeeRow from './EmployeeRow';

const EmployeeList = ({ employees, onEdit, onDelete }) => {
    return (
        <table>
            <thead>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Position</th>
                    <th>Salary</th>
                    <th>Created At</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {employees.map(employee => (
                    <EmployeeRow
                        key={employee.id}
                        employee={employee}
                        onEdit={onEdit}
                        onDelete={onDelete}
                    />
                ))}
            </tbody>
        </table>
    );
};

export default EmployeeList;
