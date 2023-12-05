using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Infrastructure.Tests.RepositoryTests
{
    public class StudentRepositoryTests
    {
        private StudentRepository _sut;
        private Mock<ILogger<StudentRepository>> _loggerMock;
        private Mock<AppDbContext> _appDbContextMock;

        public StudentRepositoryTests()
        {
            _loggerMock = new Mock<ILogger<StudentRepository>>();
            _appDbContextMock = new Mock<AppDbContext>();
            _sut = new StudentRepository(_appDbContextMock.Object, _loggerMock.Object);
        }
    }
}
