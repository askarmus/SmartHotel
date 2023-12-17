using Microsoft.EntityFrameworkCore;
using RoomService.Data;
using RoomService.Repositories;
using SmartTicket.Infrastructure.Services;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddValidatorsFromAssemblyContaining<AddRoomtValidator>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<IRoomAvailabilityRepository, RoomAvailabilityRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<RoomDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
