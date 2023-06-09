
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductInventoryMgt.ProductDbContext;
using ProductInventoryMgt.Repo;
using ProductInventoryMgt.Validations;

namespace ProductInventoryMgt
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
            builder.Services.AddSwaggerGen();
            builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Programs>());
            builder.Services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IProducts, ProductsQueries>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}