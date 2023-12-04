using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class SecurityHandler
    {
        public bool Verify(string inputHash, string storedHash, string salt)
        {
            var result = VerifyMatch(inputHash, storedHash, salt);
            return result;
        }

        public async Task<StudentModel> SetStudentSalt(StudentModel student)
        {
            return await SetSalt(student);
        }

        public async Task<StudentModel> SetStudentHash(StudentModel student, string inputHash)
        {
           return await SetHash(student, inputHash);
        }

        private async Task<StudentModel> SetSalt(StudentModel student)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            student.StudentSalt = salt;

            return student;
        }

        private bool VerifyMatch(string inputHash, string storedHash, string salt)
        {
            string enteredHash = BCrypt.Net.BCrypt.HashPassword(inputHash, salt);
            return enteredHash == storedHash;
        }


        private async Task<StudentModel> SetHash(StudentModel student, string inputHash)
        {
            var salt = student.StudentSalt.ToString();
            string finalHashedPassword = BCrypt.Net.BCrypt.HashPassword(inputHash + salt, salt);
            student.PasswordHash = finalHashedPassword;
            return student;
        }
    }
}
