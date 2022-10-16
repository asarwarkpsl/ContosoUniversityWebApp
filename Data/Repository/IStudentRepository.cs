using Data.Models;
#nullable enable
namespace Data.Repository
{
    public interface IStudentRepository: IDisposable
    {
        public Task<IEnumerable<Student>> GetStudentsAsync();
        //public Task<(IEnumerable<Student>,int totalCount)> GetStudentsAsync(string? title,string? searchQuery,int pageNum,int pageSize);
        public Task<IEnumerable<Student>> GetStudentsAsync(string? title,string? searchQuery,int pageNum,int pageSize);

        public Task<Student> GetStudentAsync(int ID);
        public Task AddAsync(Student student);
        public void Update(Student student);
        public void Delete(int studentID);
        public Task SaveAsync();
    }
}