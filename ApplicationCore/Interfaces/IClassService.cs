using System;
using System.Collections.Generic;
using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IClassService
    {
        Class GetClassDetail(int classId);
        List<Class> GetAllClassWithUser(int userId);
        Class AddNewClass(string name, DateTime startDate, string room, string description, int mainTeacherId);
        void AddStudentToClass(int classId, int studentId);
    }
}