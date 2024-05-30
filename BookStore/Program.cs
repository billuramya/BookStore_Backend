
using ManagerLayer.Interface;
using ManagerLayer.Service;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Context>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddTransient<IUserManager, UserManager>();

builder.Services.AddTransient<IBookRepo, BookRepo>();
builder.Services.AddTransient<IBookManager, BookManager>();

builder.Services.AddTransient<IAdminRepo, AdminRepo>();
builder.Services.AddTransient<IAdminManager, AdminManager>();

builder.Services.AddTransient<ICartManager,CartManager>();
builder.Services.AddTransient<ICartRepo, CartRepo>();

builder.Services.AddTransient<IAddressManager, AddressManager>();
builder.Services.AddTransient<IAddressRepo, OrderRepo>();

builder.Services.AddTransient<IOrderRepo, OrdersRepo>();
builder.Services.AddTransient<IOrderManager, OrderManager>();

builder.Services.AddTransient<IWishRepo, WishRepo>();
builder.Services.AddTransient<IWishManager, WishManager>();

builder.Services.AddTransient<IReviewRepo, ReviewRepo>();
builder.Services.AddTransient<IReviewManager, ReviewManager>();

builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
{
    builder.WithOrigins("http://localhost:4200/", "https://localhost:71286/").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
}));

builder.Services.AddSwaggerGen(
    option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    });
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddDistributedMemoryCache();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseCors("ApiCorsPolicy");
app.UseAuthorization();


app.MapControllers();

app.Run();