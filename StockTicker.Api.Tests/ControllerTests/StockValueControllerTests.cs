using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StockTicker.Api.Controllers;
using StockTicker.Api.Dtos;
using StockTicker.Api.Mappers;
using StockTicker.Application.Repositories;
using StockTicker.Common.Tests;
using StockTicker.Core.Domains;
using StockTicker.Core.Exceptions;

namespace StockTicker.Api.Tests.ControllerTests;

public class StockValueControllerTests
{
    private readonly ITradeTransactionRepository _tradeTransactionRepository;
    private Mock<ILogger<StockValueController>> _logger;
    private IStockValueMapper _stockValueMapper;
    private readonly StockValueController _controller;

    public StockValueControllerTests()
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
            },
        };
        
        _tradeTransactionRepository = TestDbContext.CreateSUT();
        
        foreach (var trade in tradeTransactions)
        {
            _tradeTransactionRepository.AddTradeTransaction(trade, CancellationToken.None);
        }
        
        _logger = new Mock<ILogger<StockValueController>>();
        _stockValueMapper = new StockValueMapper();
        _controller = new StockValueController(_tradeTransactionRepository, _stockValueMapper, _logger.Object);
    }
    
    [Fact]
    public async Task Get_Stock_Value_By_Ticker_Returns_OK()
    {
        
        var actionResult = await _controller.GetStockValueByTicker("BTC");

        var objectResult = actionResult as OkObjectResult;
        Assert.NotNull(objectResult);
        Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.OK);

        var result = objectResult.Value as StockValueDto;
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task Get_Stock_Value_By_Ticker_Returns_Not_Found()
    {
        var actionResult = await _controller.GetStockValueByTicker("ADA");

        var objectResult = actionResult as NotFoundResult;
        Assert.NotNull(objectResult);
        Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        
    }
    
    [Fact]
    public async Task Get_Stock_Value_By_Empty_Ticker_Returns_Not_Found()
    {
        await Assert.ThrowsAsync<InvalidTradeTransactionException>(async() =>  await _controller.GetStockValueByTicker(string.Empty));
    }

    [Fact]
    public async Task Get_All_Stock_Values_Returns_OK()
    {
       var actionResult = await _controller.GetStockValues();

        var objectResult = actionResult as OkObjectResult;
        Assert.NotNull(objectResult);
        Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.OK);

        var results = objectResult.Value as List<StockValueDto>;
        Assert.NotNull(results);
    }

    [Fact]
    public async Task Get_Stock_Values_By_Tickers_Returns_OK()
    {
       var actionResult = await _controller.GetStockValueByTickers(new []{ "BTC", "ETH"});

        var objectResult = actionResult as OkObjectResult;
        Assert.NotNull(objectResult);
        Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.OK);

        var results = objectResult.Value as List<StockValueDto>;
        Assert.NotNull(results);
    }

    [Fact]
    public async Task Get_Stock_Values_By_Tickers_Returns_Not_Found()
    {
        var actionResult = await _controller.GetStockValueByTickers(new []{ "ADA", "DOT"});

        var objectResult = actionResult as NotFoundResult;
        Assert.NotNull(objectResult);
        Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
    }
    
    [Fact]
    public async Task Get_Stock_Values_By_Empty_Tickers_Returns_Not_Found()
    {
       await Assert.ThrowsAsync<InvalidTradeTransactionException>(async() =>  await _controller.GetStockValueByTickers(Array.Empty<string>()));
    }
}