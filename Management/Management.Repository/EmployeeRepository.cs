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

        public IEnumerable<Employee> GetAll()
        {
            var employees = new List<Employee>();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT \"Id\", \"FirstName\", \"LastName\", \"Position\", \"Salary\" FROM \"Employee\"", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
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

        public Employee GetById(Guid id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT \"Id\", \"FirstName\", \"LastName\", \"Position\", \"Salary\" FROM \"Employee\" WHERE \"Id\" = @Id", conn);
            cmd.Parameters.AddWithValue("Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
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

        public void Add(Employee employee)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("INSERT INTO \"Employee\" (\"Id\", \"FirstName\", \"LastName\", \"Position\", \"Salary\") VALUES (@Id, @FirstName, @LastName, @Position, @Salary)", conn);
            cmd.Parameters.AddWithValue("Id", employee.Id);
            cmd.Parameters.AddWithValue("FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("LastName", employee.LastName);
            cmd.Parameters.AddWithValue("Position", employee.Position);
            cmd.Parameters.AddWithValue("Salary", employee.Salary);
            cmd.ExecuteNonQuery();
        }

        public void Update(Employee employee)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("UPDATE \"Employee\" SET \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"Position\" = @Position, \"Salary\" = @Salary WHERE \"Id\" = @Id", conn);
            cmd.Parameters.AddWithValue("Id", employee.Id);
            cmd.Parameters.AddWithValue("FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("LastName", employee.LastName);
            cmd.Parameters.AddWithValue("Position", employee.Position);
            cmd.Parameters.AddWithValue("Salary", employee.Salary);
            cmd.ExecuteNonQuery();
        }

        public void Delete(Guid id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("DELETE FROM \"Employee\" WHERE \"Id\" = @Id", conn);
            cmd.Parameters.AddWithValue("Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
