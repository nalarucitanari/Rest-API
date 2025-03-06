using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimpleRESTAPI;
using SimpleRESTAPI.Data;
using SimpleRESTAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Dependency Injection
builder.Services.AddSingleton<ICategory, CategoryDal>();
builder.Services.AddSingleton<IInstructor, InstructorDal>();

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
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
