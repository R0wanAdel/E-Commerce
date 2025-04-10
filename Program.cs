
using Microsoft.EntityFrameworkCore;

namespace ErasmusProject
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
            builder.Services.AddDbContext<Context>(options =>
            options.UseSqlServer("server=RAWAN;Database=Biedronka;Integrated Security=True;"));

            var app = builder.Build();
            
            using (var scope = app.Services.CreateScope())
            {
                //var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
                //dbContext.Database.Migrate();
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Log the error or print it
                    Console.WriteLine("Migration failed: " + ex.Message);
                    // Optional: log inner exception, stack trace, etc.
                }
            }

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
