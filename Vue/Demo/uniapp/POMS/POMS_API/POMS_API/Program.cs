
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace POMS_API
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
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "POMS API",
                    Version = "v1",
                    Description = "API接口文档"
                });

                // 添加XML注释文件
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.UseCors(options =>
            {
                //options.AllowAnyOrigin()  // 或者指定前端地址
                //       .AllowAnyMethod()
                //       .AllowAnyHeader();

                options.WithOrigins("http://localhost:5173")  // 或者指定前端地址
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });


            app.MapControllers();

            app.Run();
        }
    }
}
