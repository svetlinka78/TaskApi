using Microsoft.Extensions.Configuration;
using TaskApi.Data;
using TaskApi.Extension;
using TaskApi.Services;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var conString = builder.Configuration.GetConnectionString("appconn");//put it onConfiguration
builder.Services.AddSqlServer<AppDbContext>(conString);
builder.Services.AddScoped<ITaskServices,TaskService>();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{  //swagger
    app.UseSwagger();
    app.UseSwaggerUI();
    //cors
    app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}
else
{
    var originLIst = "http://localhost:3000/";
    app.UseCors(builder => builder
        .WithOrigins(originLIst)
        .AllowAnyMethod()
        .AllowAnyHeader());
}

app.UseAuthorization();

app.MapControllers();
app.CreateDbIfNotExists();
app.Run();
