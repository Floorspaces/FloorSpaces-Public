using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpClient();
builder.Services.AddHttpLogging(o => { });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Floorspaces API", Version = "v1" }); });

// Configure JSON Web Tokens
var supabaseSignatureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("<Removed>"));
var validIssuers = "<Removed>";
var validAudiences = new List<string>() { "authenticated" };
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = supabaseSignatureKey,
        ValidAudiences = validAudiences,
        ValidIssuer = validIssuers
    };
});

// Configure CORS Headers
builder.Services.AddCors(options =>
{;
    // Policy for general API access
    options.AddPolicy("GeneralApiPolicy", policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyMethod()
              .AllowAnyHeader();
              //.AllowCredentials(); // Keeps credentials allowed
    });

    // More permissive policy for Swagger UI
    options.AddPolicy("OpenSwaggerPolicy", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure Kestrel Web Host based on environment
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenLocalhost(5269, listenOptions =>
        {
            listenOptions.UseHttps(); // Automatically use the ASP.NET Core development certificate
        });
    });
}
else
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        // When running in Azure App Service, no additional HTTPS configuration is necessary.
        // Azure handles HTTPS termination at the load balancer.
        serverOptions.ListenAnyIP(443); // Listen on port 443 without specifying HTTPS configuration.
        serverOptions.Limits.MaxRequestBufferSize = 302768;
        serverOptions.Limits.MaxRequestLineSize = 302768;
    });
}

// Middleware configurations go here
// Order heavily matters
var app = builder.Build();

// Middleware configurations go here
app.UseHttpsRedirection();

// First apply routing
app.UseRouting();

// Then CORS
app.UseCors();

// Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// Configure endpoints
app.UseEndpoints(endpoints =>
{
    // Map controllers with General API CORS policy
    endpoints.MapControllers().RequireCors("GeneralApiPolicy");

    // Swagger UI with Open CORS Policy
    endpoints.MapSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
});

// Since Swagger UI is middleware, place it outside UseEndpoints and use it without RequireCors
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;  // To serve the Swagger UI at the app's root
});

app.Run();

