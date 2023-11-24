using StudentsApi.Entitys;
using StudentsApi.ViewModel;
using StudentsApi.Data;
using StudentsApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Forms;

namespace StudentsApi.Services
{
    public class StudentServices
    {
        private readonly StudentContext _studentContext;
        public StudentServices(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        public Student CreateNewStudent(CreateStudent createStudent)
        {
            var newStudent = new Student(createStudent.Name,createStudent.Registration,createStudent.Email);
            _studentContext.Students.Add(newStudent);
            _studentContext.SaveChanges();
            return newStudent;
        }
        public IEnumerable<Student> GetStudentsAll()
        {
            return _studentContext.Students
                .ToList()
                .Where(st=>st.IsActive);
        }
        public Student? GetStudentsById(Guid id)
        {
            var student = _studentContext.Students.SingleOrDefault(st=>st.Id == id);
            if (student is not null)
                return student;
                
            return null;
        }
        public void DeleteStudent(Guid id)
        {
           var studentRemove = _studentContext.Students.SingleOrDefault(st => st.Id == id);
           if(studentRemove is not null)
           {
                studentRemove.Inactivate();
           }    _studentContext.SaveChanges(); 
        }

        public Student? UpdateStudent(Guid id, UpdateStudent updateStudent)
        {
            var studentUpdate = _studentContext.Students.SingleOrDefault(st => st.Id == id);
            if( studentUpdate is not null)
            {
                studentUpdate.UpdateStudent(updateStudent.Name, updateStudent.Registration, updateStudent.Email);
                _studentContext.SaveChanges();
                return studentUpdate;
            }
            return null;
        }
    }
}
