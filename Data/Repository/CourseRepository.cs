using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;

namespace Data.Repository
{
    public class CourseRepository: ICourseRepository
    {
        private readonly SchoolContext _context;

        public CourseRepository(SchoolContext context)
        {
            _context = context;
        }        

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses
                                .Include(c => c.Department)
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<(IEnumerable<Course>,int totalCount)> GetCoursesAsync(string? title,string? searchQuery,int pageNum,int pageSize)
        {
            IQueryable<Course> collection = _context.Courses as IQueryable<Course>;

            if(!string.IsNullOrWhiteSpace(title))
            {
                title = title.Trim();
                collection = collection.Where(c => c.Title == title);
            }

            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(c=> c.Title.Contains(searchQuery)
                                            || c.Title.Contains(searchQuery));
            }

            var result = await collection.OrderBy(c => c.Title)
                                    .Include(c => c.Department)
                                    .Skip(pageSize * (pageNum-1))
                                    .Take(pageSize)
                                    .ToListAsync();

            int totalCount = await collection.CountAsync();

            return (result,totalCount);
        }

        public async Task<Course> GetCourseAsync(int? ID)
        {
            //  return await _context.Courses.FindAsync(ID);

            return await _context.Courses
                            .Include(c => c.Department).FirstOrDefaultAsync(m => m.ID == ID);
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments
                                .OrderBy(d => d.Name).ToListAsync();
        }
        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }
        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }
        public void Delete(int? courseID)
        {
            Task<Course> course = GetCourseAsync(courseID);
            _context.Courses.Remove(course.Result);
        }
        public bool CourseExists(int courseID)
        {
            return _context.Courses.Any(e => e.ID == courseID);
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