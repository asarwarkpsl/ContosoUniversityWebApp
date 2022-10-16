using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;
using System.Collections.Generic;

namespace Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }        

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        //public async Task<(IEnumerable<Student>,int totalCount)> GetStudentsAsync(string? lastName,string? searchQuery,int pageNum,int pageSize)
        public async Task<IEnumerable<Student>> GetStudentsAsync(string? lastName,string? searchQuery,int pageNum,int pageSize)
        {
            IQueryable<Student> collection = _context.Students as IQueryable<Student>;

            if(!string.IsNullOrWhiteSpace(lastName))
            {
                lastName = lastName.Trim();
                collection = collection.Where(c => c.LastName == lastName);
            }

            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(c=> c.LastName.Contains(searchQuery)
                                            || (c.FirstMidName != null && c.FirstMidName.Contains(searchQuery)));
            }

            var result = await collection.OrderBy(c => c.LastName)
                                    .Skip(pageSize * (pageNum-1))
                                    .Take(pageSize)
                                    .ToListAsync();

            int totalCount = await collection.CountAsync();
            
            //////////return (result,totalCount);
            return result;
        }
        public async Task<Student> GetStudentAsync(int ID)
         {
            return await _context.Students.FindAsync(ID);
         }
        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }
        public void Update(Student student)
        {
            _context.Students.Update(student);
        }
        public void Delete(int studentID)
        {
            Task<Student> student = GetStudentAsync(studentID);
            _context.Students.Remove(student.Result);
        }
        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }

        private bool disposed = false;  
        protected virtual void Dispose(bool disposing)  
        {  
            if (!this.disposed)  
            {  
                if (disposing)  
                {  
                    _context.Dispose();  
                }  
            }  
            this.disposed = true;  
        }  
        public void Dispose()  
        {  
            Dispose(true);  
            GC.SuppressFinalize(this);  
        }  
    }
}