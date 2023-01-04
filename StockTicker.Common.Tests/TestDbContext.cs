using Microsoft.EntityFrameworkCore;
using StockTicker.Infrastructure.DbContext;
using StockTicker.Infrastructure.Repositories;

namespace StockTicker.Common.Tests;

public abstract class TestDbContext
{
    public static TradeTransactionRepository CreateSUT()
    {
        var dbOptions = new DbContextOptionsBuilder<StockTickerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new StockTickerDbContext(dbOptions);

        return new TradeTransactionRepository(context);
    }
}