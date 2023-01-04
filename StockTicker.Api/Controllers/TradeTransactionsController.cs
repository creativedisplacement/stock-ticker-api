using Microsoft.AspNetCore.Mvc;
using StockTicker.Api.Dtos;
using StockTicker.Api.Mappers;
using StockTicker.Application.Repositories;
using StockTicker.Core.Exceptions;
using StockTicker.Core.Validation;

namespace StockTicker.Api.Controllers;

[ApiController]
[Route("api/trade")]
public class TradeTransactionsController : ControllerBase
{
    private readonly ITradeTransactionRepository _tradeTransactionRepository;
    private readonly ITradeTransactionsMapper _tradeTransactionsMapper;
    private readonly ILogger<TradeTransactionsController> _logger;

    public TradeTransactionsController(ITradeTransactionRepository tradeTransactionRepository,
        ITradeTransactionsMapper tradeTransactionsMapper,
        ILogger<TradeTransactionsController> logger)
    {
        _tradeTransactionRepository = tradeTransactionRepository;
        _tradeTransactionsMapper = tradeTransactionsMapper;
        _logger = logger;
    }

    [HttpPost(Name = "AddTransaction")]
    public async Task<IActionResult> Post([FromBody] AddTradeTransactionDto addTradeTransactionDto)
    {
        Guard.AgainstNullReferenceException(addTradeTransactionDto, "No transaction supplied");
        Guard.AgainstEmptyTicker(addTradeTransactionDto.Ticker, "No ticker symbol supplied");
        Guard.AgainstDefaultDecimal(addTradeTransactionDto.Price, "Trade price is zero");
        Guard.AgainstDefaultDecimal(addTradeTransactionDto.NumberOfShares, "Number of shares bought is zero");
        Guard.AgainstDefaultInteger(addTradeTransactionDto.BrokerId, "Broker Id not supplied");

        var transaction = _tradeTransactionsMapper
            .MapAddTradeTransactionDtoToTradeTransaction(addTradeTransactionDto);
            
        var result = await _tradeTransactionRepository.AddTradeTransaction(transaction, CancellationToken.None);

        if (result)
        {
            return Ok();
        }

        const string message = "Cannot save trade";
        _logger.LogError(message);
        throw new TradeTransactionSaveException(message);
    }
}