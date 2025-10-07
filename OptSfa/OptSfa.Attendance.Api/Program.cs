using Asp.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using OptSfa.Attendance.Application.Interfaces;
using OptSfa.Attendance.Application.Services;
using OptSfa.Attendance.Data.Context;
using OptSfa.Attendance.Data.Repository;
using OptSfa.Attendance.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DbConnection"),
        new MySqlServerVersion(new Version(8, 0, 33))
    )
);

builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();


builder.Services.AddApiVersioning();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0); //DefaultApiVersion is used to set the default version to API
    config.AssumeDefaultVersionWhenUnspecified = true; //This flag AssumeDefaultVersionWhenUnspecified flag is used to set the default version when the client has not specified any versions
    config.ReportApiVersions = true; //To return the API version in response header.
});
builder.Services.AddApiVersioning().AddMvc().AddApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;
});

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "micro-service v1",
            Version = "v1.0",
            Description = "API for generating data"
        };
        return Task.CompletedTask;
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            // builder.WithOrigins("*") // Replace with the allowed domain(s)
            builder.AllowAnyOrigin() // instead of WithOrigins("*")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

#region(Added By Sambhav For Compression ===============================================)
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
          options.Level = System.IO.Compression.CompressionLevel.Optimal);
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
    app.MapOpenApi("/openapi/{documentName}/openapi.json");
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}
else
{
    app.MapOpenApi();
    app.MapOpenApi("/openapi/{documentName}/openapi.json");
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/attendanceapi/openapi/v1.json", "v1");
    });
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
