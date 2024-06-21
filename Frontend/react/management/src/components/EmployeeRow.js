import React from 'react';

const EmployeeRow = ({ employee, onEdit, onDelete }) => {
    return (
        <tr>
            <td>{employee.firstName}</td>
            <td>{employee.lastName}</td>
            <td>{employee.position}</td>
            <td>{employee.salary}</td>
            <td>{employee.createdAt}</td>
            <td>
                <button className="btn" onClick={() => onEdit(employee)}>Edit</button>
                <button className="btn" onClick={() => onDelete(employee.id)}>Delete</button>
            </td>
        </tr>
    );
};

export default EmployeeRow;
