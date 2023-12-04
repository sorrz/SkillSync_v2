﻿using Entity.Models;

namespace Infrastructure.Repositories
{
    public interface ISecureRepository
    {
        Task<bool> VerifyPasswordAsync(int id, string inputHash);
        Task<bool> CreateStudentHashAsync(StudentModel student, string inputHash);
    }
}