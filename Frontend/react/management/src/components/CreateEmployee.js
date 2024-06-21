import React, { useState } from 'react';

const CreateEmployee = ({ onAddEmployee }) => {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [position, setPosition] = useState('Manager');
    const [salary, setSalary] = useState('');
    const [createdAt, setCreatedAt] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        const newEmployee = {
            id: Date.now(),
            firstName,
            lastName,
            position,
            salary: parseFloat(salary),
            createdAt
        };
        onAddEmployee(newEmployee);
        setFirstName('');
        setLastName('');
        setPosition('Manager');
        setSalary('');
        setCreatedAt('');
    };

    return (
        <div className="form-container">
            <h2>Create Employee</h2>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="first-name">First Name:</label>
                    <input
                        type="text"
                        id="first-name"
                        value={firstName}
                        onChange={(e) => setFirstName(e.target.value)}
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="last-name">Last Name:</label>
                    <input
                        type="text"
                        id="last-name"
                        value={lastName}
                        onChange={(e) => setLastName(e.target.value)}
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="position">Position:</label>
                    <select
                        id="position"
                        value={position}
                        onChange={(e) => setPosition(e.target.value)}
                    >
                        <option value="Manager">Manager</option>
                        <option value="Worker">Worker</option>
                    </select>
                </div>
                <div className="form-group">
                    <label htmlFor="salary">Salary:</label>
                    <input
                        type="number"
                        id="salary"
                        value={salary}
                        onChange={(e) => setSalary(e.target.value)}
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="created-at">Created At:</label>
                    <input
                        type="date"
                        id="created-at"
                        value={createdAt}
                        onChange={(e) => setCreatedAt(e.target.value)}
                    />
                </div>
                <button type="submit" className="btn">Create</button>
            </form>
        </div>
    );
};

export default CreateEmployee;
