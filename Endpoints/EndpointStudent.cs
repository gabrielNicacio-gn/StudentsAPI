using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentsApi.DTOs;
using StudentsApi.Services;
using StudentsApi.ViewModel;
using StudentsApi.Data;

namespace StudentsApi.Endpoints
{
    public static class EndpointStudent
    {
        public static void StartEndpoints(this WebApplication app)
        {
            var endpoint = app.MapGroup("estudantes");

            endpoint.MapPost("",(CreateStudent create, StudentServices _studentsServices) =>
            {
                var exist = _studentsServices.GetStudentsAll().Any(st => st.Name == create.Name && st.Registration == create.Registration && st.Email == create.Email);
                if (exist)
                {
                    return Results.Conflict("Já existe");
                }
                var student =_studentsServices.CreateNewStudent(create);
                var studentCreate = new StudentDTO(student.Id,student.Name,student.Registration,student.Email);
                return Results.Ok(studentCreate);
            });
            endpoint.MapGet("",(StudentServices _studentServices) =>
            {
                var students = _studentServices.GetStudentsAll()
                .Select(studentResult=>new StudentDTO(studentResult.Id,studentResult.Name,studentResult.Registration,studentResult.Email));
                return Results.Ok(students);
            });
            endpoint.MapGet("{id}", (Guid id, StudentServices _studentServices) =>
            {
                var student = _studentServices.GetStudentsById(id);
                if(student is not null)
                {
                    var studentResult = new StudentDTO(student.Id, student.Name, student.Registration, student.Email);
                    return Results.Ok(studentResult);
                }
                return Results.NotFound(id);
            });
            endpoint.MapPut("{id}",(Guid id, UpdateStudent updateStudent,StudentServices _studentsServices) =>
            {
                var studentExist = _studentsServices.GetStudentsById(id);
                if(studentExist is not null)
                {
                    var studentUpdate = _studentsServices.UpdateStudent(id,updateStudent);
                    if(studentUpdate is not null)
                    {
                        var studentResult = new StudentDTO(studentUpdate.Id, studentUpdate.Name, studentUpdate.Registration, studentUpdate.Email);
                        return Results.Ok(studentResult);
                    }
                }
                return Results.NotFound(id);
            });

            endpoint.MapDelete("{id}",(Guid id,StudentServices _studentServices)=> 
            {
                var studentExist = _studentServices.GetStudentsById(id);
                if(studentExist is not null)
                {
                    _studentServices.DeleteStudent(id);
                    return Results.NoContent();
                }
                return Results.NotFound(id);
            });

        }
    }
}
