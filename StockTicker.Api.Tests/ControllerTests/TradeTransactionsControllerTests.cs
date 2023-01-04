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

public class TradeTransactionsControllerTests
{
    private readonly ITradeTransactionRepository _tradeTransactionRepository;
    private Mock<ILogger<TradeTransactionsController>> _logger;
    private ITradeTransactionsMapper _tradeTransactionsMapper;
    private readonly TradeTransactionsController _controller;
    private readonly AddTradeTransactionDto _tradeTransactionDto;
    private readonly TradeTransaction _tradeTransaction;

    public TradeTransactionsControllerTests()
    {
        _tradeTransactionDto = new AddTradeTransactionDto
        {
            Ticker = "BTC",
            Price = 16000M,
            NumberOfShares = 2,
            BrokerId = 1
        };

        _tradeTransaction = new TradeTransaction
        {
            Ticker = "BTC",
            Price = 16000M,
            NumberOfShares = 1,
            TotalCost = 16000M,
            BrokerId = 1
        };
        
        _tradeTransactionRepository = TestDbContext.CreateSUT();
        _logger = new Mock<ILogger<TradeTransactionsController>>();
        _tradeTransactionsMapper = new TradeTransactionsMapper();

        _controller = new TradeTransactionsController(_tradeTransactionRepository, _tradeTransactionsMapper,
            _logger.Object);
    }

    [Fact]
    public async Task Post_Null_Trade_Transaction_Throws_Null_Reference_Exception()
    {
        await Assert.ThrowsAsync<NullReferenceException>(async () => await _controller.Post(null));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Post_Trade_Transactions_With_Empty_Ticker_Throws_Invalid_Trade_Transaction_Exception(
        string ticker)
    {
        var dto = _tradeTransactionDto with { Ticker = ticker };
        await Assert.ThrowsAsync<InvalidTradeTransactionException>(async () => await _controller.Post(dto));
    }

    [Fact]
    public async Task Post_Trade_Transactions_With_No_Price_Throws_Invalid_Trade_Transaction_Exception()
    {
        var dto = _tradeTransactionDto with { Price = default };
        await Assert.ThrowsAsync<InvalidTradeTransactionException>(async () => await _controller.Post(dto));
    }

    [Fact]
    public async Task Post_Trade_Transactions_With_No_Number_Of_Shares_Throws_Invalid_Trade_Transaction_Exception()
    {
        var dto = _tradeTransactionDto with { NumberOfShares = default };
        await Assert.ThrowsAsync<InvalidTradeTransactionException>(async () => await _controller.Post(dto));
    }

    [Fact]
    public async Task Post_Trade_Transactions_With_No_Broker_Id_Throws_Invalid_Trade_Transaction_Exception()
    {
        var dto = _tradeTransactionDto with { BrokerId = default };
        await Assert.ThrowsAsync<InvalidTradeTransactionException>(async () => await _controller.Post(dto));
    }

    [Fact]
    public async Task Post_Trade_Transactions_Returns_OK()
    {
        var actionResult = await _controller.Post(_tradeTransactionDto);

        var result = actionResult as OkResult;
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }
}