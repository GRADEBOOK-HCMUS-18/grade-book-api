using System;
using System.Collections.Generic;
using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IClassService
    {
        Class GetClassDetail(int classId);
        List<Class> GetAllClassWithUserBeingMainTeacher(int userId);
        List<Class> GetAllClassWithUserBeingSubTeacher(int userId);
        List<Class> GetAllClassWithUserBeingStudent(int userId);
        Class AddNewClass(string name, DateTime startDate, string room, string description, int mainTeacherId);
        void AddStudentToClass(int classId, int studentId);
        void AddTeacherToClass(int classId, int teacherId);
    }
}