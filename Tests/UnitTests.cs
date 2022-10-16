using Xunit;
using Data.Repository;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Linq;
using Microsoft.Extensions.Logging;
using ContosoUniversity.Data.Tests;
using Xunit.Abstractions;

namespace Tests
{
    public class StudentTests
    {
        SqliteConnection _connection;
        private readonly ITestOutputHelper _output;
        DbContextOptions<SchoolContext> options;
        SchoolContext context;

        public StudentTests(ITestOutputHelper output)
        {
            _output = output;

            _connection = new SqliteConnection("Data Source=:memory:");

            options = new DbContextOptionsBuilder<SchoolContext>()
                                .UseLoggerFactory(new LoggerFactory(
                                    new[]{ new LogProvider((log) =>
                                        {
                                            _output.WriteLine(log);
                                        })})
                                )
                                .UseSqlite(_connection)
                                .Options;

            //Context creation

            context = new SchoolContext(options);
            //Arrange
            context.Database.OpenConnection();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            DbInitializer.Initialize(context);
        }

        [Fact]
        public void GetStudents_TotalStudents_ReturnEightStudents()
        {
           // using (var context = new SchoolContext(options))
            {
                //Arrange
                //context.Database.OpenConnection();
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                //DbInitializer.Initialize(context);

                var studentRepository = new StudentRepository(context);

                //Act
                var students = studentRepository.GetStudentsAsync(null, null, 1, 10);

                //Assert
                Assert.Equal(6, students.Result.Count());
            }
        }

        [Fact]
        public void GetStudents_PageSizeIsThree_ReturnThreeStudents()
        {            
           // using (var context = new SchoolContext(options))
            {
                //Arrange
                //context.Database.OpenConnection();
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

               // DbInitializer.Initialize(context);

                var studentRepository = new StudentRepository(context);

                //Act
                var students = studentRepository.GetStudentsAsync(null,null,1,3);

                //Assert
                Assert.Equal(3, students.Result.Count());
            }
        }

        internal void Dispose()
        {
            _connection.Dispose();
            context.Dispose();
        }
    }
}