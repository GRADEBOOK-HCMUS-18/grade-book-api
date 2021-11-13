using System;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class ClassService: IClassService
    {
        public Class GetClassDetail(int classId)
        {
            throw new NotImplementedException();
        }

        public Class AddNewClass(string name, DateTime startDate, string room, string description, int mainTeacherId)
        {
            throw new NotImplementedException();
        }

        public void AddStudentToClass(int classId, int studentId)
        {
            throw new NotImplementedException();
        }
    }
}