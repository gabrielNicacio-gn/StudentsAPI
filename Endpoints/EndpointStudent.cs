using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentsApi.DTOs;
using StudentsApi.Services;
using StudentsApi.ViewModel;
using StudentsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentsApi.Endpoints
{
    public static class EndpointStudent
    {
        public static void StartEndpoints(this WebApplication app)
        {
            var endpoint = app.MapGroup("estudantes");

            endpoint.MapPost("",async (CreateStudent create, StudentServices _studentsServices) =>
            {
                var student = await _studentsServices.CreateNewStudentAsync(create);
                if(!string.IsNullOrEmpty(student.ErrorMessage))
                {
                    return Results.Conflict(student.ErrorMessage);
                }
                var result = new {student.Id};
                return Results.Ok(result);
            });
            
            endpoint.MapGet("",async (StudentServices _studentServices) =>
            {
                var students = await _studentServices.GetStudentsAll();
                return Results.Ok(students);
            });
            
            endpoint.MapGet("{id}", async (Guid id, StudentServices _studentServices) =>
            {
                var student = await _studentServices.GetStudentsByIdAsync(id);
                if(student is not null)
                    return Results.Ok(student);

                return Results.NotFound(id);
            });
            
            endpoint.MapPut("{id}", async(Guid id, UpdateStudent updateStudent,StudentServices _studentsServices) =>
            {
                var studentExist = await _studentsServices.UpdateStudentAsync(id,updateStudent);
                if (!string.IsNullOrEmpty(studentExist.ErrorMessage))
                {
                    return Results.NotFound(studentExist.ErrorMessage);
                }
                var result = new {studentExist.Return.Id,studentExist.Return.Name,studentExist.Return.Registration,studentExist.Return.Email};
                return Results.Ok(result);
            });
            
            endpoint.MapDelete("{id}",async (Guid id,StudentServices _studentServices)=> 
            {
                var studentExist = await _studentServices.DeleteStudentAsync(id);
                if (!string.IsNullOrEmpty(studentExist.ErrorMessage))
                {
                    return Results.NotFound(id);
                }
                return Results.NoContent();
            });
            
        }
    }
}
