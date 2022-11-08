using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Repository
{
    public interface IEnrollmentRepository : IDisposable
    {
        public Task<IEnumerable<Enrollment>> GetEnrollmentsAsync();
        public Task<(IEnumerable<Enrollment>, int totalCount)> GetEnrollmentsAsync(int courceID, string? searchQuery, int pageNum, int pageSize);
        public Task<Enrollment> GetEnrollmentAsync(int ID);
        public Task AddAsync(Enrollment enrollment);
        public void Update(Enrollment enrollment);
        public void Delete(int enrollmentID);
        public Task SaveAsync();
    }
}