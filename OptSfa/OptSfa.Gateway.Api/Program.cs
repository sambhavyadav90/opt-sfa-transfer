using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
//.AddJwtBearer(options => {
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true, // Ensure that the token's lifetime is validated
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = "http://auth.optsfa.in/api/auth/login",
//        ValidAudience = "http://web.optsfa.in",
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("241AA96F-206B-4514-B31A-6775F0338F8A")),
//        ClockSkew = TimeSpan.Zero // Ensure no additional time is added to the token expiration
//    };
//});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("authenticated", policy =>
        policy.RequireAuthenticatedUser());
});

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 20; //in 10 seconds only accept 20 request
    });
});

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

app.MapReverseProxy();

app.MapControllers();

app.Run();
