using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Repository
{
    public interface IDepartmentRepository : IDisposable
    {
        public Task<IEnumerable<Department>> GetDepartmentsAsync();
        public Task<(IEnumerable<Department>, int totalCount)> GetDepartmentsAsync(string? name, string? searchQuery, int pageNum, int pageSize);
        public Task<Department> GetDepartmentAsync(int ID);
        public Task AddAsync(Department department);
        public void Update(Department department);
        public void Delete(int departmentID);
        public Task SaveAsync();
    }
}
