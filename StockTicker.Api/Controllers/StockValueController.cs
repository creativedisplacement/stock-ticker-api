using Microsoft.AspNetCore.Mvc;
using StockTicker.Api.Mappers;
using StockTicker.Application.Repositories;
using StockTicker.Core.Validation;

namespace StockTicker.Api.Controllers;

[ApiController]
[Route("api/stockvalue")]
public class StockValueController : ControllerBase
{
    private readonly ITradeTransactionRepository _tradeTransactionRepository;
    private readonly IStockValueMapper _stockValueMapper;
    private readonly ILogger<StockValueController> _logger;

    public StockValueController(ITradeTransactionRepository tradeTransactionRepository,
        IStockValueMapper stockValueMapper,
        ILogger<StockValueController> logger)
    {
        _tradeTransactionRepository = tradeTransactionRepository;
        _stockValueMapper = stockValueMapper;
        _logger = logger;
    }

    [HttpGet]
    [Route("getstockvaluebyticker")]
    public async Task<IActionResult> GetStockValueByTicker([FromQuery]string ticker)
    {
        Guard.AgainstEmptyTicker(ticker, "No tickers for stocks supplied");
        
        var transactions = await _tradeTransactionRepository
            .GetTradeTransactionsByTicker(ticker, default);
        
        var stock = _stockValueMapper
            .MapTradeTransactionsToStockValueDto(transactions);

        if (stock is null)
        {
            return NotFound();
        }

        return Ok(stock);
    }
    
    [HttpGet]
    [Route("getallstockvalues")]
    public async Task<IActionResult> GetStockValues()
    {
        var transactions = await _tradeTransactionRepository
            .GetAllTradeTransactions(default);

        var stocks = _stockValueMapper
            .MapTradeTransactionsToStockValueDtos(transactions);

        if (stocks is null)
        {
            return NotFound();
        }

        return Ok(stocks);
    }
    
    [HttpGet]
    [Route("getstockvaluesbytickers")]
    public async Task<IActionResult> GetStockValueByTickers([FromQuery]string[] tickers)
    {
        Guard.AgainstEmptyTickers(tickers, "No tickers for stocks supplied");
        
        var transactions = await _tradeTransactionRepository
            .GetTradeTransactionsByTickers(tickers, default);
        
        var stocks = _stockValueMapper.MapTradeTransactionsToStockValueDtos(transactions);

        if (stocks.Any())
        {
            return Ok(stocks);
            
        }

        return NotFound();
    }
}