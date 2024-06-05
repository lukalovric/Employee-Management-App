using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Management.Model;
using Management.Repository.Common;

namespace Management.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = new List<Employee>();
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand("SELECT \"Id\", \"FirstName\", \"LastName\", \"Position\", \"Salary\" FROM \"Employee\"", conn);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                employees.Add(new Employee
                {
                    Id = reader.GetGuid(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Position = reader.GetString(3),
                    Salary = reader.GetDouble(4)
                });
            }
            return employees;
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand("SELECT \"Id\", \"FirstName\", \"LastName\", \"Position\", \"Salary\" FROM \"Employee\" WHERE \"Id\" = @Id", conn);
            cmd.Parameters.AddWithValue("Id", id);
            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Employee
                {
                    Id = reader.GetGuid(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Position = reader.GetString(3),
                    Salary = reader.GetDouble(4)
                };
            }
            return null;
        }

        public async Task AddAsync(Employee employee)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand("INSERT INTO \"Employee\" (\"Id\", \"FirstName\", \"LastName\", \"Position\", \"Salary\") VALUES (@Id, @FirstName, @LastName, @Position, @Salary)", conn);
            cmd.Parameters.AddWithValue("Id", employee.Id);
            cmd.Parameters.AddWithValue("FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("LastName", employee.LastName);
            cmd.Parameters.AddWithValue("Position", employee.Position);
            cmd.Parameters.AddWithValue("Salary", employee.Salary);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand("UPDATE \"Employee\" SET \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"Position\" = @Position, \"Salary\" = @Salary WHERE \"Id\" = @Id", conn);
            cmd.Parameters.AddWithValue("Id", employee.Id);
            cmd.Parameters.AddWithValue("FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("LastName", employee.LastName);
            cmd.Parameters.AddWithValue("Position", employee.Position);
            cmd.Parameters.AddWithValue("Salary", employee.Salary);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand("DELETE FROM \"Employee\" WHERE \"Id\" = @Id", conn);
            cmd.Parameters.AddWithValue("Id", id);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
