using Microsoft.EntityFrameworkCore;
using StockTicker.Infrastructure.DbContext;

namespace StockTicker.Api.Configuration;

public static class DbContextConfig
{
    public static void AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StockTickerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("StockTickerDB")));
    }
}