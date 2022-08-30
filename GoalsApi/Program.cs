using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

var jwtSecret = builder.Configuration["jwtSecret"];
var key = Encoding.UTF8.GetBytes(jwtSecret);
var tokenValidationParameters = new TokenValidationParameters() {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey         = new SymmetricSecurityKey(key),
    ValidateIssuer           = false,
    ValidateAudience         = false,
};

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services
    .AddAuthentication(opts => {
        opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opts.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opts => {
        opts.RequireHttpsMetadata      = false;
        opts.SaveToken                 = false;
        opts.TokenValidationParameters = tokenValidationParameters;
    });

var app = builder.Build();

app.UseCors(builder => 
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
