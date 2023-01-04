using Microsoft.EntityFrameworkCore;
using StockTicker.Common.Tests;
using StockTicker.Core.Domains;
using StockTicker.Infrastructure.DbContext;
using StockTicker.Infrastructure.Repositories;

namespace StockTicker.Infrastructure.Tests;

public class TradeTransactionRepositoryTests
{
    private readonly TradeTransactionRepository _tradeTransactionRepository;

    public TradeTransactionRepositoryTests()
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
                BrokerId = 1,
            },
            new()
            {
                Ticker = "ETH",
                Price = 4000M,
                NumberOfShares = 1,
                TotalCost = 4000M,
                BrokerId = 1,
            },
            new()
            {
                Ticker = "XRP",
                Price = 500M,
                NumberOfShares = 1,
                TotalCost = 500M,
                BrokerId = 1,
            },
            new()
            {
                Ticker = "XRP",
                Price = 550M,
                NumberOfShares = 1,
                TotalCost = 550M,
                BrokerId = 1,
            }

        };
        
        _tradeTransactionRepository = TestDbContext.CreateSUT();
        
        foreach (var trade in tradeTransactions)
        {
            _tradeTransactionRepository.AddTradeTransaction(trade, CancellationToken.None);
        }
    }
    

    [Fact]
    public async Task Get_Trade_Transaction_By_Ticker_Returns_Trade_Transaction_List()
    {

        var results = await _tradeTransactionRepository.GetTradeTransactionsByTicker("BTC", default);
        Assert.Equal(2, results.Count);
        Assert.Collection(results,
            transaction => Assert.Equal("BTC", transaction.Ticker),
            transaction => Assert.Equal("BTC", transaction.Ticker));

    }
    
    [Fact]
    public async Task Get_All_Trade_Transactions_Returns_Trade_Transaction_List()
    {

        var results = await _tradeTransactionRepository.GetAllTradeTransactions(default);
        Assert.Equal(5, results.Count);
        Assert.Collection(results, 
            transaction => Assert.Equal("BTC", transaction.Ticker),
                                transaction => Assert.Equal("BTC", transaction.Ticker),
                                transaction => Assert.Equal("ETH", transaction.Ticker),
                                transaction => Assert.Equal("XRP", transaction.Ticker),
                                transaction => Assert.Equal("XRP", transaction.Ticker));
    }
    
    [Fact]
    public async Task Get_Trade_Transactions_For_Requested_Tickers_Returns_Trade_Transaction_List()
    {

        var results = await _tradeTransactionRepository.GetTradeTransactionsByTickers(new []{"ETH", "XRP"},default);
        Assert.Equal(3, results.Count);
        Assert.Collection(results,
            transaction => Assert.Equal("ETH", transaction.Ticker),
                                transaction => Assert.Equal("XRP", transaction.Ticker),
                                transaction => Assert.Equal("XRP", transaction.Ticker));
    }
}