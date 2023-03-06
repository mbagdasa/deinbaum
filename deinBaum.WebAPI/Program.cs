global using deinBaum.DAL;
global using Microsoft.EntityFrameworkCore;
global using deinBaum.WebAPI.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace deinBaum.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
           
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddHttpContextAccessor();
            
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description= "Standard Authorization header using the Bearer scheme (\" bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Read JWT 
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration.GetSection("AuthSetting:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddEntityFrameworkNpgsql()
                .AddDbContext<AppDbContext>(opt =>
                    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            //opt.UseNpgsql());

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }

            app.Run();
        }
    }
}