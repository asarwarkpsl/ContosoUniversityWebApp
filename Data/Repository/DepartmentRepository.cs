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

        public Task AddAsync(Department department)
        {
            throw new NotImplementedException();
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

        public Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<Department>, int totalCount)> GetDepartmentsAsync(string name, string searchQuery, int pageNum, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Department department)
        {
            throw new NotImplementedException();
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
                // TODO: set large fields to null
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
