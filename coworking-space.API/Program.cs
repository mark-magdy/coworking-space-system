
using coworking_space.API.MappingProfiles;
using coworking_space.BAL.Interaces;
using coworking_space.BAL.Services;
using coworking_space.DAL.Data;
using coworking_space.DAL.Repository.Implementations;
using coworking_space.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using CO_Working_Space;
//using AutoMapper.Extensions.Microsoft.DependencyInjection;
namespace coworking_space.API

{
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            builder.Services.AddControllers()
      .AddJsonOptions(options =>
      {
          options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
          options.JsonSerializerOptions.WriteIndented = true;
      });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("coworking-space.DAL"))); 




            // ----------------------------------------
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IRoomService, RoomService>();

            builder.Services.AddScoped<ITotalReservationsService, TotalReservationService>();
            builder.Services.AddScoped<ITotalReservationsRepository, TotalReservationsRepository>();

            builder.Services.AddScoped<IReservationsRepository, ReservationsRepository>();

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


            builder.Services.AddScoped<IAuthService, AuthService>();
            //-----------------------------------------
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<Context>();

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(options =>
            //    {
            //        var jwtSettings = builder.Configuration.GetSection("Jwt");
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = jwtSettings["Issuer"],
            //            ValidAudience = jwtSettings["Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
            //        };
            //    });

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "mahmoud";
                option.DefaultChallengeScheme = "mahmoud";
            }).AddJwtBearer("mahmoud", options =>
            {
                var securitykeystring = builder.Configuration.GetSection("Jwt:Key").Value;
                var securtykeyByte = Encoding.ASCII.GetBytes(securitykeystring);
                SecurityKey securityKey = new SymmetricSecurityKey(securtykeyByte);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = securityKey,
                    //ValidAudience = "url" ,
                    //ValidIssuer = "url",
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
           // builder.Services.AddAuthorization();
          

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await DbInitializer.SeedRolesAsync(services); // 👈 calls the method to add roles
            }
            app.UseCors("AllowReactApp");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
