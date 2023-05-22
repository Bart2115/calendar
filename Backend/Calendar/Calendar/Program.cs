using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Calendar.Database.Repositories;
using Calendar.Database.DbContexts;
using Calendar.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<CalendarDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("DatabaseConnectionString").Get<string>());
});

builder.Services.AddScoped<INotesRepository, NotesRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var mapper = AutoMapperConfiguration.GetMapperConfiguration().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddCors();
builder.Services.AddMvc();

var app = builder.Build();

app.UseRouting();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(opt => opt.WithOrigins("http://localhost:4200").AllowAnyMethod());

app.UseEndpoints(endpoints => endpoints.MapControllers());

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<CalendarDbContext>();
    dbContext?.Database.EnsureCreated();
}

app.Run();



