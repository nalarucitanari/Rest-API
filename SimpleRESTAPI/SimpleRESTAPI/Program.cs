using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SimpleRESTAPI;
using SimpleRESTAPI.Data;
using SimpleRESTAPI.DTO;
using SimpleRESTAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//add ef core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Dependency Injection
builder.Services.AddScoped<ICategory, CategoryEF>();
builder.Services.AddScoped<IInstructor, InstructorEF>();
builder.Services.AddScoped<ICourse, CourseEF>();
builder.Services.AddScoped<IAspUser, AspUserEF>();

// ...existing code...
builder.Services.AddAutoMapper(typeof(SimpleRESTAPI.DTO.Mapping));
// ...existing code...

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.MapGet("api.v1/luas-segitiga", (double alas, double tinggi) =>
// {
//     double luas = 0.5 * alas * tinggi;
//     return $"Luas Segitia dngn alas = {alas} dan tinggi = {tinggi} adalah {luas}";
// });

// app.MapGet("api/v1/helloservice", (string id) =>
// {
//     return $"Hello ASP Web API : id ={id}!";
// });
// app.MapGet("api/v1/helloservices/{name}", (string name) => $"Hello {name}");

app.MapGet("api/v1/categories", (ICategory categoryData) =>
{
    var categories = categoryData.GetCategories();
    return categories;
});
app.MapGet("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
    var category = categoryData.GetCategoryById(id);
    return category;
});
app.MapPost("api/v1/categories", (ICategory categoryData, Category category) =>
{
    var newCategory = categoryData.AddCategory(category);
    return newCategory;
});
app.MapPut("api/v1/categories", (ICategory categoryData, Category category) =>
{
    var updateCategory = categoryData.UpdateCategory(category); 
    return updateCategory;
});
app.MapDelete("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
    categoryData.DeleteCategory(id);
    return Results.NoContent();
});

app.MapGet("api/v1/instructor", (IInstructor instructorData) =>
{
    var instructor = instructorData.GetInstructors();
    return instructor;
});
app.MapGet("api/v1/instructor/{id}", (IInstructor instructorData, int id) =>
{
    var instructor = instructorData.GetInstructorById(id);
    return instructor;
});
app.MapPost("api/v1/instructor", (IInstructor instructorData, Instructor instructor) =>
{
    var newInstructor = instructorData.AddInstructor(instructor);
    return newInstructor;
});
app.MapPut("api/v1/instructor", (IInstructor instructorData, Instructor instructor) =>
{
    var updateInstructor = instructorData.UpdateInstructor(instructor);
    return updateInstructor;
});
app.MapDelete("api/v1/instructor/{id}", (IInstructor instructorData, int id) =>
{
    instructorData.DeleteInstructor(id);
    return Results.NoContent();
});

// app.MapGet("api/v1/courses",(ICourse courseData) =>
// {
//     List<CourseDTO> courseDTOs = new List<CourseDTO>();
//     var courses = courseData.GetAllCourses();
//     //mapping to CourseDTO
//     foreach (var course in courses)
//     {
//         CourseDTO courseDTO = new CourseDTO
//         {
//             CourseId = course.CourseId,
//             CourseName = course.CourseName,
//             CourseDescription = course.CourseDescription,
//             Duration = course.Duration,
//             Category = new CategoryDTO
//             {
//                 categoryId = course.Category.categoryId,
//                 categoryName = course.Category.categoryName
//             },
//             Instructor = course.Instructor != null ? new InstructorDTO
//             {
//                 InstructorId = course.Instructor.InstructorId,
//                 InstructorName = course.Instructor.InstructorName
//             }
//             : null

//         };
//         courseDTOs.Add(courseDTO);
//     }
//     return courseDTOs;
// });
// app.MapGet("api/v1/courses/{id}", (ICourse courseData, int id) =>
// {
//     CourseDTO courseDTO = new CourseDTO();
//     var course = courseData.GetCourseByIdCourse(id);
//     if (course == null)
//     {
//         return Results.NotFound();
//     }
//     courseDTO.CourseId = course.CourseId;
//     courseDTO.CourseName = course.CourseName;
//     courseDTO.CourseDescription = course.CourseDescription;
//     courseDTO.Duration = course.Duration;
//     courseDTO.Category = new CategoryDTO
//     {
//         categoryId = course.Category.categoryId,
//         categoryName = course.Category.categoryName
//     };
//     courseDTO.Instructor = new InstructorDTO
//     {
//         InstructorId = course.Instructor.InstructorId,
//         InstructorName = course.Instructor.InstructorName
//     };
//     return Results.Ok(courseDTO);
// });
// app.MapPost("api/v1/courses", (ICourse courseData, CourseAddDTO courseAddDto) =>
// {
//      try
//     {
//         Course course = new Course
//         {
//             CourseName = courseAddDto.CourseName,
//             CourseDescription = courseAddDto.CourseDescription,
//             Duration = courseAddDto.Duration,
//             categoryId = courseAddDto.categoryId,
//             InstructorId = courseAddDto.InstructorId
//         };

//         var newCourse = courseData.AddCourse(course);

//         CourseDTO courseDTO = new CourseDTO
//         {
//             CourseId = newCourse.CourseId,
//             CourseName = newCourse.CourseName,
//             CourseDescription = newCourse.CourseDescription,
//             Duration = newCourse.Duration,
//             Category = newCourse.Category != null ? new CategoryDTO
//             {
//                 categoryId = newCourse.Category.categoryId,
//                 categoryName = newCourse.Category.categoryName
//             } : null,
//             Instructor = newCourse.Instructor != null ? new InstructorDTO
//             {
//                 InstructorId = newCourse.Instructor.InstructorId,
//                 InstructorName = newCourse.Instructor.InstructorName
//             } : null
//         };

//         return Results.Created($"/api/v1/courses/{courseDTO.CourseId}", courseDTO);
//     }
//     catch (Exception ex)
//     {
//         return Results.Problem(ex.Message);
//     }
// });
// app.MapPut("api/v1/courses", (ICourse courseData, Course course) =>
// {
//     try
//     {
//         var updatedCourse = courseData.UpdateCourse(course);

//         CourseDTO courseDTO = new CourseDTO
//         {
//             CourseId = updatedCourse.CourseId,
//             CourseName = updatedCourse.CourseName,
//             CourseDescription = updatedCourse.CourseDescription,
//             Duration = updatedCourse.Duration,
//             Category = new CategoryDTO
//             {
//                 categoryId = updatedCourse.categoryId,
//                 categoryName = updatedCourse.Category.categoryName
//             },
//             Instructor = new InstructorDTO
//             {
//                 InstructorId = updatedCourse.InstructorId,
//                 InstructorName = updatedCourse.Instructor.InstructorName
//             }
//         };

//         return Results.Ok(courseDTO);
//     }
//     catch (KeyNotFoundException knfEx)
//     {
//         return Results.NotFound(knfEx.Message);
//     }
//     catch (DbUpdateException dbex)
//     {
//         return Results.Problem("An error occurred while updating the course", statusCode: 500);
//     }
//     catch (Exception ex)
//     {
//         return Results.Problem("An unexpected error occurred", statusCode: 500);
//     }
// });
// app.MapDelete("api/v1/courses/{id}", (ICourse courseData, int id) =>
// {
//     try
//     {
//         courseData.DeleteCourse(id);
//         return Results.NoContent();
//     }
//     catch (KeyNotFoundException knfEx)
//     {
//         return Results.NotFound(knfEx.Message);
//     }
//     catch (DbUpdateException dbex)
//     {
//         return Results.Problem("An error occurred while deleting the course", statusCode: 500);
//     }
//     catch (Exception ex)
//     {
//         return Results.Problem("An unexpected error occurred", statusCode: 500);
//     }
// });
app.MapGet("api/v1/courses", (ICourse courseData, IMapper mapper) =>
{
    var courses = courseData.GetAllCourses();
    var courseDTOs = mapper.Map<List<CourseDTO>>(courses);
    return courseDTOs;
});
app.MapGet("api/v1/courses/{id}", (ICourse courseData, int id, IMapper mapper) =>
{
    try
    {
        var course = courseData.GetCourseByIdCourse(id);
        if (course == null)
        {
            return Results.NotFound();
        }
        var dto = mapper.Map<CourseDTO>(course);
        return Results.Ok(dto);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("api/v1/courses", (ICourse courseData, CourseAddDTO courseAddDto, IMapper mapper) =>
{
    try
    {
        var course = mapper.Map<Course>(courseAddDto);
        var newCourse = courseData.AddCourse(course);
        var courseDTO = mapper.Map<CourseDTO>(newCourse);
        return Results.Created($"/api/v1/courses/{courseDTO.CourseId}", courseDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPut("api/v1/courses", (ICourse courseData, Course course, IMapper mapper) =>
{
    var existingCourse = courseData.GetCourseByIdCourse(course.CourseId);
    if (existingCourse == null)
        return Results.NotFound();

    existingCourse.CourseName = course.CourseName;
    existingCourse.CourseDescription = course.CourseDescription;
    existingCourse.Duration = course.Duration;
    existingCourse.categoryId = course.categoryId;
    existingCourse.InstructorId = course.InstructorId;

    var updated = courseData.UpdateCourse(existingCourse);
    var courseDTO = mapper.Map<CourseDTO>(updated);
    return Results.Ok(courseDTO);
});

app.MapDelete("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    try
    {
        courseData.DeleteCourse(id);
        return Results.NoContent();
    }
    catch (KeyNotFoundException knfEx)
    {
        return Results.NotFound(knfEx.Message);
    }
    catch (DbUpdateException dbex)
    {
        return Results.Problem("An error occurred while deleting the course", statusCode: 500);
    }
    catch (Exception ex)
    {
        return Results.Problem("An unexpected error occurred", statusCode: 500);
    }
});

app.MapGet("api/v1/cekpassword/{password}", (string password) =>
{
    var pass = SimpleRESTAPI.Helpers.HashHelper.HashPassword(password);
    return Results.Ok($"Password: {password} Hash: {pass}");
});
app.MapPost("api/v1/register", (IAspUser aspUserData, AspUserDTO userDto, IMapper mapper) =>
{
    try
    {
        var user = mapper.Map<AspUser>(userDto);
        var newUser = aspUserData.RegisterUser(user);
        var resultDto = mapper.Map<AspUserDTO>(newUser);
        return Results.Created($"/api/v1/users/{resultDto.Username}", resultDto);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPost("api/v1/login", (IAspUser aspUserData, SimpleRESTAPI.DTO.AspUserLoginDTO loginDto) =>
{
    try
    {
        var isValid = aspUserData.Login(loginDto.Username, loginDto.Password);
        if (!isValid)
            return Results.Unauthorized();

        return Results.Ok("Login success");
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.Run();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
