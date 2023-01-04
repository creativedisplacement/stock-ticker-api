using StockTicker.Api.Dtos;
using StockTicker.Core.Domains;

namespace StockTicker.Api.Mappers;

public interface ITradeTransactionsMapper
{
    TradeTransaction MapAddTradeTransactionDtoToTradeTransaction(AddTradeTransactionDto addTradeTransactionDto);
}

public class TradeTransactionsMapper : ITradeTransactionsMapper
{
    public TradeTransaction MapAddTradeTransactionDtoToTradeTransaction(AddTradeTransactionDto addTradeTransactionDto)
    {
        return new TradeTransaction
        {
            Ticker = addTradeTransactionDto.Ticker,
            Price = addTradeTransactionDto.Price,
            NumberOfShares = addTradeTransactionDto.NumberOfShares,
            TotalCost = addTradeTransactionDto.Price * addTradeTransactionDto.NumberOfShares,
            BrokerId = addTradeTransactionDto.BrokerId,
            Timestamp = DateTime.Now
        };
    }
}