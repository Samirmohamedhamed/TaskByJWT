
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WepApiDemo.Model;
using WepApiDemo.Repositories;
using WepApiDemo.Repositories.IRepositories;
namespace WepApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefaultConnection"));
            });
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                // إعداد التوثيق باستخدام Bearer Token
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token.\n\nExample: Bearer abcdef12345"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            //[Authoriz] used JWT Token in Check Authantication
            builder.Services.AddAuthentication(options =>
            {
                //if user has token or not only
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//Token valid Signtaur or not 
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//if token is not valid redirect to loginPage
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(Options =>
            {
                //token from Provider and consumer
                Options.SaveToken = true;//Expiration is not end
                Options.RequireHttpsMetadata = false; //if token get from https
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //to determine issuer and  Audience
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),//check Sectet Key

                };
            });
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(CorsOptions =>
            {
                CorsOptions.AddPolicy("MyPolicy", CorsPolicyBuilder =>
                {

                    CorsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseAuthentication();//Defult in Cookies But i want it to check JWT Token // Configer Service 
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
