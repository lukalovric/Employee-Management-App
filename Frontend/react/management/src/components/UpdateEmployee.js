import React, { useState, useEffect } from 'react';

const UpdateEmployee = ({ employee, onUpdateEmployee, onCancel }) => {
    const [firstName, setFirstName] = useState(employee.firstName);
    const [lastName, setLastName] = useState(employee.lastName);
    const [position, setPosition] = useState(employee.position);
    const [salary, setSalary] = useState(employee.salary);
    const [createdAt, setCreatedAt] = useState(employee.createdAt);

    const handleSubmit = (e) => {
        e.preventDefault();
        const updatedEmployee = {
            ...employee,
            firstName,
            lastName,
            position,
            salary: parseFloat(salary),
            createdAt
        };
        onUpdateEmployee(updatedEmployee);
    };

    return (
        <div className="form-container">
            <h2>Update Employee</h2>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="update-first-name">First Name:</label>
                    <input
                        type="text"
                        id="update-first-name"
                        value={firstName}
                        onChange={(e) => setFirstName(e.target.value)}
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="update-last-name">Last Name:</label>
                    <input
                        type="text"
                        id="update-last-name"
                        value={lastName}
                        onChange={(e) => setLastName(e.target.value)}
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="update-position">Position:</label>
                    <select
                        id="update-position"
                        value={position}
                        onChange={(e) => setPosition(e.target.value)}
                    >
                        <option value="Manager">Manager</option>
                        <option value="Worker">Worker</option>
                    </select>
                </div>
                <div className="form-group">
                    <label htmlFor="update-salary">Salary:</label>
                    <input
                        type="number"
                        id="update-salary"
                        value={salary}
                        onChange={(e) => setSalary(e.target.value)}
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="update-created-at">Created At:</label>
                    <input
                        type="date"
                        id="update-created-at"
                        value={createdAt}
                        onChange={(e) => setCreatedAt(e.target.value)}
                    />
                </div>
                <button type="submit" className="btn">Update</button>
                <button type="button" className="btn" onClick={onCancel}>Cancel</button>
            </form>
        </div>
    );
};

export default UpdateEmployee;
