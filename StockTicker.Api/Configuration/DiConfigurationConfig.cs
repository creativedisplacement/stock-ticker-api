using Microsoft.EntityFrameworkCore;
using StockTicker.Api.Mappers;
using StockTicker.Application.Repositories;
using StockTicker.Infrastructure.DbContext;
using StockTicker.Infrastructure.Repositories;


namespace StockTicker.Api.Configuration;

public static class DiConfigurationConfig
{
    public static void AddDependencyInjectionConfig(this IServiceCollection services)
    {
        services.AddScoped<DbContext, StockTickerDbContext>();
        services.AddScoped<ITradeTransactionRepository, TradeTransactionRepository>();
        services.AddScoped<IStockValueMapper, StockValueMapper>();
        services.AddScoped<ITradeTransactionsMapper, TradeTransactionsMapper>();
    }
}