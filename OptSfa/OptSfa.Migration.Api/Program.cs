using Asp.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Application.Services;
using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Data.Repository;
using OptSfa.Migration.Domain.Interfaces;



var builder = WebApplication.CreateBuilder(args);

//host on local wifi

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);  // HTTP
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DbConnection"),
        new MySqlServerVersion(new Version(8, 0, 33))
    )
);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IClientRepository, ClientRepository>();


builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IHeadquarterRepository, HeadquarterRepository>();

builder.Services.AddScoped<IHeadQuarterViewRepository, HeadQuarterViewRepository>();

builder.Services.AddScoped<IHeadQuarterViewService, HeadquarterService>();

builder.Services.AddScoped<IEmployeeTargetRepository, EmployeeTargetRespository>();

builder.Services.AddScoped<IEmployeeTargetService, EmployeeTargetService>();

builder.Services.AddScoped<ITargetPercentFormulaRepository, TargetPercentFormulaRepository>();

builder.Services.AddScoped<ITargetPecentageFormulaService, TargetFormulaService>();




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
            Description = "API for generating payment data"
        };
        return Task.CompletedTask;
    });
});

//builder.Services.AddSignalR();

//By Sambhav :: Cross Domain Authentication To Access API
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowSpecificOrigin",
//         builder =>
//         {
//             // builder.WithOrigins("*") // Replace with the allowed domain(s)
//             builder.AllowAnyOrigin()
//                    .AllowAnyHeader()
//                    .AllowAnyMethod();
//         });
// });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
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
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();


