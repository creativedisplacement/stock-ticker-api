using StockTicker.Core.Domains;

namespace StockTicker.Application.Repositories;

public interface ITradeTransactionRepository
{
    Task<List<TradeTransaction>> GetTradeTransactionsByTicker(string ticker, CancellationToken cancellationToken);
    Task<List<TradeTransaction>> GetAllTradeTransactions(CancellationToken cancellationToken);
    Task<List<TradeTransaction>> GetTradeTransactionsByTickers(string[] tickers, CancellationToken cancellationToken);
    Task<bool> AddTradeTransaction(TradeTransaction trade, CancellationToken cancellationToken);
}