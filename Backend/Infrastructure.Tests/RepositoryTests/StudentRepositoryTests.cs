using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.RepositoryTests
{
    public class StudentRepositoryTests
    {
        private StudentRepository _sut;

        public StudentRepositoryTests()
        {
            _sut = new StudentRepository();
        }

        [Fact]
        public void Should_MatchTypeOf_Repository()
        {
            var sut = _sut.GetType();
            Assert.Equal(typeof(StudentRepository), sut);
        }

        [Fact]
        public void ShouldReturn_OK_FromGetOK()
        {
            var sut = _sut.GetOk();
            Assert.Equal("OK", sut);
        }



    }
}
