using Datalayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using ServiceLayer.Repository;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.OpenApi.Models;
using System.Reflection;
using static System.Net.WebRequestMethods;
using EshopAPI.Formatters;

namespace EshopAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
           //builder.Services.AddControllers(options =>
           // {
           //     options.OutputFormatters.Insert(0, new CustomOutputFormatter());
           // });

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICreateService, CreateService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddDbContext<EShopContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
            //builder.Services.AddControllers().AddJsonOptions(x =>
            //                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "test swagger",
                    Description = "this is a test description",
                    Contact = new OpenApiContact() { Url = new Uri("https://www.google.dk/?gws_rd=ssl") }
                });

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlfile));


            });


            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "AllowedOrigins",
            //        policy =>
            //        {
            //            policy.WithOrigins("https://localhost:7272")
            //            .AllowAnyHeader()
            //            .AllowAnyMethod();
            //        });
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
           


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            //app.UseCors("AllowedOrigins");
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapFallbackToFile("index.html");

            app.MapControllers();

            app.Run();
        }
    }
}