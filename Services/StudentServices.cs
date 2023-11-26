using StudentsApi.Entitys;
using StudentsApi.ViewModel;
using StudentsApi.Data;
using StudentsApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace StudentsApi.Services
{
    public class StudentServices
    {
        private readonly StudentContext _studentContext;
        public StudentServices(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        public async Task<ResultOfCreatingStudents> CreateNewStudentAsync(CreateStudent createStudent)
        {
            var newStudent = new Student(createStudent.Name,createStudent.Registration,createStudent.Email);
            var exist = _studentContext.Students.Any(studentExist => studentExist.Name == newStudent.Name && 
            studentExist.Registration == newStudent.Registration && 
            studentExist.Email == newStudent.Email);
            if (exist)
            {
                return new ResultOfCreatingStudents("Esse estudante já existe");
            }
            _studentContext.Students.Add(newStudent);
            await _studentContext.SaveChangesAsync();
            return new ResultOfCreatingStudents(newStudent.Id);
        }
        public async Task<IQueryable<ReturnStudents> >GetStudentsAll()
        {
            var listStudent = await _studentContext.Students
                .Where(st => st.IsActive)
                .Select(studentResult => new ReturnStudents(studentResult.Id, studentResult.Name, studentResult.Registration, studentResult.Email))
                .ToListAsync();
            return listStudent.AsQueryable();
        }
        public async Task<ReturnStudents?> GetStudentsByIdAsync(Guid id)
        {
            var student = await _studentContext.Students
                .Where(st => st.Id == id && st.IsActive)
                .Select(student => new ReturnStudents(student.Id, student.Name, student.Registration, student.Email))
                .SingleOrDefaultAsync();

            return student;
        }
        public async Task<ResultOfStudentsDeletion> DeleteStudentAsync(Guid id)
        {
            var studentRemove = await _studentContext.Students
                 .Where(st=>st.Id == id && st.IsActive)
                 .SingleOrDefaultAsync();
            if(studentRemove is null)
            {
                return new ResultOfStudentsDeletion("Este aluno não foi encontrado");
            }
            studentRemove.Inactivate();
            await _studentContext.SaveChangesAsync();
            return new ResultOfStudentsDeletion("");
        }

        public async Task<ResultOfStudentsUpdate> UpdateStudentAsync(Guid id, UpdateStudent updateStudent)
        {
            var studentUpdate = await _studentContext.Students
                .Where(st => st.Id == id && st.IsActive)
                .SingleOrDefaultAsync();
            if(studentUpdate is null) 
            {
                return new ResultOfStudentsUpdate("Esse estudante não foi encontrado");
            }
            studentUpdate.UpdateStudent(updateStudent.Name,updateStudent.Registration,studentUpdate.Email);
            await _studentContext.SaveChangesAsync();
            var studentUpdateReturn = new ReturnStudents(studentUpdate.Id, studentUpdate.Name, studentUpdate.Registration, studentUpdate.Email);
            return new ResultOfStudentsUpdate(studentUpdateReturn);
        }
    }
}
