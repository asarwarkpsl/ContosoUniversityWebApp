using Data.Models;
#nullable enable
namespace Data.Repository
{
    public interface IStudentRepository: IDisposable
    {
        public Task<IList<Student>> GetStudentsAsync();
        public Task<(IList<Student>,int totalCount)> GetStudentsAsync(string? searchQuery,int pageNum,int pageSize);
        public Task<bool> StudentExistsAsync(int ID);
        public Task<Student> GetStudentAsync(int? ID);
        public Task AddAsync(Student student);
        public void Update(Student student);
        public void Delete(int studentID);
        public void Delete(Student student);
        public Task SaveAsync();
    }
}