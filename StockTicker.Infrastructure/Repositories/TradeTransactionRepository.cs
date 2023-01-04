using Microsoft.EntityFrameworkCore;
using StockTicker.Application.Repositories;
using StockTicker.Core.Domains;
using StockTicker.Infrastructure.DbContext;

namespace StockTicker.Infrastructure.Repositories;

public class TradeTransactionRepository : ITradeTransactionRepository
{
    private readonly StockTickerDbContext _dbContext;
    private readonly DbSet<TradeTransaction> _tradeTransactions;

    public TradeTransactionRepository(StockTickerDbContext dbContext)
    {
        _dbContext = dbContext;
        _tradeTransactions = dbContext.Set<TradeTransaction>();
    }

    //Using a specification pattern for querying the database would be ideal here,
    //it's something new to me and didn't want to go over time trying to implement
    public async Task<List<TradeTransaction>> GetTradeTransactionsByTicker(string ticker, CancellationToken cancellationToken)
    {
        return await _dbContext.TradesTransactions.Where(tradeTransaction => tradeTransaction.Ticker == ticker)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TradeTransaction>> GetAllTradeTransactions(CancellationToken cancellationToken)
    {
        return await _dbContext.TradesTransactions
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TradeTransaction>> GetTradeTransactionsByTickers(string[] tickers, CancellationToken cancellationToken)
    {
        return await _dbContext.TradesTransactions
            .Where(tradeTransaction => tickers.Contains(tradeTransaction.Ticker))
                .ToListAsync(cancellationToken);
    }

    public async Task<bool> AddTradeTransaction(TradeTransaction trade, CancellationToken cancellationToken)
    {
        _tradeTransactions.Add(trade);
        var success = await _dbContext.SaveChangesAsync(cancellationToken);
        return success != default;
    }
}