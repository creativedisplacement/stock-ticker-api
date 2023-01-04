using StockTicker.Api.Mappers;
using StockTicker.Core.Domains;

namespace StockTicker.Api.Tests.MapperTests;

public class StockValueMapperTests
{
    private readonly IStockValueMapper _mapper;

    public StockValueMapperTests()
    {
        _mapper = new StockValueMapper();
    }

    [Fact]
    public void Trade_Transaction_Correctly_Mapped_To_Dto()
    {
        var tradeTransactions = new List<TradeTransaction>
        {
            new()
            {
                Ticker = "BTC",
                Price = 16000M,
                NumberOfShares = 1,
                TotalCost = 16000M,
                BrokerId = 1
            },
            new()
            {
                Ticker = "BTC",
                Price = 16000M,
                NumberOfShares = 1,
                TotalCost = 16000M,
                BrokerId = 1
            },
            new()
            {
                Ticker = "ETH",
                Price = 4000M,
                NumberOfShares = 4,
                TotalCost = 16000M,
                BrokerId = 1
            },
            new()
            {
                Ticker = "XRP",
                Price = 500M,
                NumberOfShares = 10,
                TotalCost = 50000M,
                BrokerId = 1
            },
            new()
            {
                Ticker = "XRP",
                Price = 600M,
                NumberOfShares = 1,
                TotalCost = 600M,
                BrokerId = 1
            }
        };

        var results = _mapper.MapTradeTransactionsToStockValueDtos(tradeTransactions);
        Assert.NotNull(results);
        Assert.Equal(3, results.Count());
        
        Assert.Equal("BTC", results.FirstOrDefault()?.Ticker);
        Assert.Equal(16000M, results.FirstOrDefault()?.Value);
        
        Assert.Equal("ETH", results.Skip(1).FirstOrDefault()?.Ticker);
        Assert.Equal(16000M, results.Skip(1).FirstOrDefault()?.Value);
        
        Assert.Equal("XRP", results.Skip(2).FirstOrDefault()?.Ticker);
        Assert.Equal(25300, results.Skip(2).FirstOrDefault()?.Value);
    }
}