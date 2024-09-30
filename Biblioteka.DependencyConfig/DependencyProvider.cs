using Biblioteka.BLL;
using Biblioteka.DAL;
using Biblioteka.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteka.DependencyConfig;

public static class DependencyProvider
{
    public static IServiceProvider RegisterServices(IServiceCollection services)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddSingleton<IConfiguration>(builder);
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.GetConnectionString("Default"));
        });
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAvtorRepository, AvtorRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAvtorService, AvtorService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IValidatable<Book>, BookValidator>();
        services.AddScoped<IValidatable<Avtor>, AvtorValidator>();
        services.AddScoped<IValidatable<Polzak>, AvtorValidator.PolzakValidator>();  
        return services.BuildServiceProvider();
        
    }
}