using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IClassroomService, ClassroomsService>();
builder.Services.AddSingleton<IStudentService, StudentsService>();
builder.Services.AddDbContext<StudentsDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();