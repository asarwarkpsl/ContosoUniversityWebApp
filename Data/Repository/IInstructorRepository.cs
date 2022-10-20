using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Repository
{
    public interface IInstructorRepository : IDisposable
    {
        public Task<IEnumerable<Instructor>> GetInstructorsAsync();
        public Task<(IEnumerable<Instructor>, int totalCount)> GetInstructorsAsync(string? name, string? searchQuery, int pageNum, int pageSize);
        public Task<Instructor> GetInstructorAsync(int ID);
        public Task AddAsync(Instructor instructor);
        public void Update(Instructor instructor);
        public void Delete(int instructorID);
        public Task SaveAsync();
    }
}