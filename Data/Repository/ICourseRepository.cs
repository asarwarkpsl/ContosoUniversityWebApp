using Data.Models;

#nullable enable
namespace Data.Repository
{
    public interface ICourseRepository: IDisposable
    {
        public Task<IEnumerable<Course>> GetCoursesAsync();
        public  Task<(IEnumerable<Course>,int totalCount)> GetCoursesAsync(string? name,string? searchQuery,int pageNum,int pageSize);
        public Task<Course> GetCourseAsync(int? ID);
        public Task AddAsync(Course course);
        public void Update(Course course);
        public void Delete(int? courseID);

        public bool CourseExists(int courseID);
        public Task SaveAsync();
    }
}