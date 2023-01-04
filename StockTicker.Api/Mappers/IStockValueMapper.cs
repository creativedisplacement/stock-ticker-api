using StockTicker.Api.Dtos;
using StockTicker.Core.Domains;

namespace StockTicker.Api.Mappers;

public interface IStockValueMapper
{
    StockValueDto MapTradeTransactionsToStockValueDto(IEnumerable<TradeTransaction> tradeTransactions);

    IEnumerable<StockValueDto> MapTradeTransactionsToStockValueDtos(IEnumerable<TradeTransaction> tradeTransactions);
}

public class StockValueMapper : IStockValueMapper
{
    public StockValueDto MapTradeTransactionsToStockValueDto(IEnumerable<TradeTransaction> tradeTransactions)
    {
        if (tradeTransactions.Any())
        {
            return MapTradeTransactionsToStockValueDtos(tradeTransactions)
                .SingleOrDefault();
        }

        return null;
    }

    public IEnumerable<StockValueDto> MapTradeTransactionsToStockValueDtos(IEnumerable<TradeTransaction> tradeTransactions)
    {
        return tradeTransactions.GroupBy(stock => stock.Ticker)
            .Select(x => new StockValueDto
            {
                Ticker = x.Key,
                Value = Math.Round(x.ToList()
                    .Average(y => y.TotalCost), 2)
            }).ToList();
    }
}