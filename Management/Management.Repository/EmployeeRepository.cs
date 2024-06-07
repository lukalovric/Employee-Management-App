using System;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Management.Model;
using Management.Repository.Common;
using Management.Common;

namespace Management.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(Filter filter, Paging paging, Sorting sorting)
        {
            var employees = new List<Employee>();

            if (filter == null)
            {
                filter = new Filter();
            }
            if (paging == null)
            {
                paging = new Paging { RecordsPerPage = 10, PageNumber = 1 };
            }
            if (sorting == null)
            {
                sorting = new Sorting { OrderBy = "CreatedAt", SortOrder = "ASC" };
            }

            var query = new StringBuilder("SELECT \"Id\", \"FirstName\", \"LastName\", \"Position\", \"Salary\", \"CreatedAt\" FROM \"Employee\" WHERE 1=1");

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(query.ToString(), conn);

            if (!string.IsNullOrEmpty(filter.SearchSurname) ||
                !string.IsNullOrEmpty(filter.SearchName) ||
                filter.StartDate.HasValue ||
                filter.EndDate.HasValue ||
                filter.ProjectId.HasValue)
            {
                if (!string.IsNullOrEmpty(filter.SearchSurname))
                {
                    query.Append(" AND \"LastName\" ILIKE @SearchSurname");
                    cmd.Parameters.AddWithValue("SearchSurname", $"%{filter.SearchSurname}%");
                }

                if (!string.IsNullOrEmpty(filter.SearchName))
                {
                    query.Append(" AND \"FirstName\" ILIKE @SearchName");
                    cmd.Parameters.AddWithValue("SearchName", $"%{filter.SearchName}%");
                }

                if (filter.StartDate.HasValue)
                {
                    query.Append(" AND \"CreatedAt\" >= @StartDate");
                    cmd.Parameters.AddWithValue("StartDate", filter.StartDate.Value);
                }

                if (filter.EndDate.HasValue)
                {
                    query.Append(" AND \"CreatedAt\" <= @EndDate");
                    cmd.Parameters.AddWithValue("EndDate", filter.EndDate.Value);
                }

                if (filter.ProjectId.HasValue)
                {
                    query.Append(" AND \"ProjectId\" = @ProjectId");
                    cmd.Parameters.AddWithValue("ProjectId", filter.ProjectId.Value);
                }
            }

            query.Append($" ORDER BY \"{sorting.OrderBy}\" {sorting.SortOrder}");
            query.Append(" OFFSET @Offset LIMIT @Limit");

            cmd.CommandText = query.ToString();
            cmd.Parameters.AddWithValue("Offset", (paging.PageNumber - 1) * paging.RecordsPerPage);
            cmd.Parameters.AddWithValue("Limit", paging.RecordsPerPage);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                employees.Add(new Employee
                {
                    Id = reader.GetGuid(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Position = reader.GetString(3),
                    Salary = reader.GetDouble(4),
                    CreatedAt = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
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
            // First, retrieve the existing employee from the database
            var existingEmployee = await GetByIdAsync(employee.Id);

            if (existingEmployee == null)
            {
                throw new Exception("Employee not found."); // or handle this case based on your application logic
            }

            // Update the existing employee object with the new values if they are not null
            if (employee.FirstName != null)
            {
                existingEmployee.FirstName = employee.FirstName;
            }
            if (employee.LastName != null)
            {
                existingEmployee.LastName = employee.LastName;
            }
            if (employee.Position != null)
            {
                existingEmployee.Position = employee.Position;
            }
            if (employee.Salary != 0)
            {
                existingEmployee.Salary = employee.Salary;
            }

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand("UPDATE \"Employee\" SET \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"Position\" = @Position, \"Salary\" = @Salary WHERE \"Id\" = @Id", conn);
            cmd.Parameters.AddWithValue("Id", existingEmployee.Id);
            cmd.Parameters.AddWithValue("FirstName", existingEmployee.FirstName);
            cmd.Parameters.AddWithValue("LastName", existingEmployee.LastName);
            cmd.Parameters.AddWithValue("Position", existingEmployee.Position);
            cmd.Parameters.AddWithValue("Salary", existingEmployee.Salary);
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
