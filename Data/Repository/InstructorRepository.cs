using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private bool disposedValue;
        private readonly SchoolContext _context;

        public InstructorRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
        }

        public async void Delete(int? instructorID)
        {
            Instructor instructor = await GetInstructorAsync(instructorID);
            _context.Instructors.Remove(instructor);
        }

        public async Task<(IEnumerable<Instructor>, int totalCount)> GetInstructorsAsync(string lastName, string searchQuery, int pageNum, int pageSize)
        {
            IQueryable<Instructor> collection = _context.Instructors as IQueryable<Instructor>;

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                lastName = lastName.Trim();
                collection = collection.Where(c => c.LastName == lastName);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(c => c.FullName.Contains(searchQuery)
                                            || c.FullName.Contains(searchQuery));
            }

            var result = await collection.OrderBy(c => c.LastName)
                                    .Skip(pageSize * (pageNum - 1))
                                    .Take(pageSize)
                                    .ToListAsync();

            int totalCount = await collection.CountAsync();

            return (result, totalCount);
        }

        public async Task<Instructor> GetInstructorAsync(int? ID)
        {
           return await _context.Instructors.FindAsync(ID);
        }

        public async Task<IEnumerable<Instructor>> GetInstructorsAsync()
        {
            return await _context.Instructors.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
        }

        public async Task<bool> isInstructorExists(int ID)
        {
            var data = await _context.Instructors.FindAsync(ID);

            if (data != null)
                return true;
            else
                return false;
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

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
