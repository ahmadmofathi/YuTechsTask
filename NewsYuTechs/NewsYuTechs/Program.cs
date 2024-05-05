using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using NewsYuTechs.DAL;
using NewsYuTechs.BL;
using System.Text;
using NewsYuTechs.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

//Allow Interact with Angular
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost",
                "https://localhost",
                "http://localhost:4200",
                "https://localhost:4200"

                ).AllowAnyOrigin() // Allow any origin
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});


//context registration 
builder.Services.AddIdentity<Admin, IdentityRole<string>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Repo and Manager Injection
builder.Services.AddScoped<IAdminRepo, AdminRepo>();
builder.Services.AddScoped<IAdminManager, AdminManager>();
builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
builder.Services.AddScoped<IAuthorManager, AuthorManager>();
builder.Services.AddScoped<INewsRepo, NewsRepo>();
builder.Services.AddScoped<INewsManager, NewsManager>();
#endregion

//JWT Auth
var secretKey = builder.Configuration.GetValue<string>("SecretKey");
var secretKeyBytes = string.IsNullOrEmpty(secretKey) ? null : Encoding.ASCII.GetBytes(secretKey);
var Key = new SymmetricSecurityKey(secretKeyBytes);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        IssuerSigningKey = Key
    };
});


//connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


var app = builder.Build();



app.Map("/FileManager/GetImage", app =>
{
    app.Run(async context =>
    {
        var fileName = context.Request.Query["ImageName"];

        // Get the file path based on the file name
        var filePath = Path.Combine(Environment.CurrentDirectory, "Uploads", "StaticContent", fileName);

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Determine the content type based on the file extension
            var contentType = GetContentType(Path.GetExtension(filePath));

            // Serve the file with appropriate content type
            context.Response.ContentType = contentType;
            await context.Response.SendFileAsync(filePath);
        }
        else
        {
            // Return 404 if the file does not exist
            context.Response.StatusCode = 404;
        }
    });
});

string GetContentType(string fileExtension)
{
    switch (fileExtension.ToLower())
    {
        case ".jpg":
        case ".jpeg":
            return "image/jpeg";
        case ".png":
            return "image/png";
        // Add more cases for other image types if needed
        default:
            return "application/octet-stream"; // Default to binary data
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
