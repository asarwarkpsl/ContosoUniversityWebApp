using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Repository
{
    internal class EnrollmentRepository : IEnrollmentRepository
    {
        private bool disposedValue;
        private readonly SchoolContext _context;

        public EnrollmentRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
        }

        public async void Delete(int enrollmentID)
        {
            Enrollment enrollment = await GetEnrollmentAsync(enrollmentID);
            _context.Enrollments.Remove(enrollment);
        }

        public async Task<Enrollment> GetEnrollmentAsync(int ID)
        {
            return await _context.Enrollments.FindAsync(ID);
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsAsync()
        {
            return await _context.Enrollments.ToListAsync();
        }

        public async Task<(IEnumerable<Enrollment>, int totalCount)> GetEnrollmentsAsync(int courceID, string searchQuery, int pageNum, int pageSize)
        {
            IQueryable<Enrollment> collection = _context.Enrollments as IQueryable<Enrollment>;

            collection = collection.Where(c => c.CourseID == courceID);

            //if (!string.IsNullOrWhiteSpace(courseID))
            //{
            //    courseID = courseID.Trim();
            //    collection = collection.Where(c => c.CourseID == courseID);
            //}

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(c => c.Course.Title.Contains(searchQuery)
                                            || c.Course.Title.Contains(searchQuery));
            }

            var result = await collection.OrderBy(c => c.CourseID)
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

        public void Update(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);
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
        // ~EnrollmentRepository()
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
