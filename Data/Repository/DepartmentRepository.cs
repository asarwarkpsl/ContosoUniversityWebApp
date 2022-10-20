using Microsoft.EntityFrameworkCore;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace ContosoUniversity.Data.Repository
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private bool disposedValue;
        private readonly SchoolContext _context;

        public DepartmentRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Department department)
        {
            await _context.AddAsync(department);
        }

        public async void Delete(int departmentID)
        {
            Department dept = await GetDepartmentAsync(departmentID);
            _context.Departments.Remove(dept);
        }

        public async Task<Department> GetDepartmentAsync(int ID)
        {
            return await _context.Departments.FindAsync(ID);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<(IEnumerable<Department>, int totalCount)> GetDepartmentsAsync(string name, string searchQuery, int pageNum, int pageSize)
        {
            IQueryable<Department> collection = _context.Departments as IQueryable<Department>;            

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(c => c.Name.Contains(searchQuery)
                                            || c.Name.Contains(searchQuery));
            }

            var result = await collection.OrderBy(c => c.Name)
                                    .Skip(pageSize * (pageNum - 1))
                                    .Take(pageSize)
                                    .ToListAsync();

            int totalCount = await collection.CountAsync();

            return (result, totalCount);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                _context.Dispose();
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Department()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
