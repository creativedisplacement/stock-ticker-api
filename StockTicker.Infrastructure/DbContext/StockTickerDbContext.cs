using Microsoft.EntityFrameworkCore;
using StockTicker.Core.Domains;

namespace StockTicker.Infrastructure.DbContext;

public class StockTickerDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public StockTickerDbContext(DbContextOptions<StockTickerDbContext> options) : base(options)
    {
    }
    
    public DbSet<TradeTransaction> TradesTransactions { get; set; }
}