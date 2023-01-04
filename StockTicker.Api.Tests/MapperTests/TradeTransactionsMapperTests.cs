using StockTicker.Api.Dtos;
using StockTicker.Api.Mappers;

namespace StockTicker.Api.Tests.MapperTests;

public class TradeTransactionsMapperTests
{
    private readonly ITradeTransactionsMapper _mapper;

    public TradeTransactionsMapperTests()
    {
        _mapper = new TradeTransactionsMapper();
    }

    [Fact]
    public void Dto_Correctly_Mapped_To_Trade_Transaction()
    {
        var dto = new AddTradeTransactionDto
        {
            Ticker = "BTC",
            Price = 16000M,
            NumberOfShares = 2,
            BrokerId = 1
        };

        var result = _mapper.MapAddTradeTransactionDtoToTradeTransaction(dto);
        Assert.NotNull(result);
        Assert.Equal(dto.Ticker, result.Ticker);
        Assert.Equal(dto.NumberOfShares, result.NumberOfShares);
        Assert.Equal(dto.Price, result.Price);
        Assert.Equal(dto.BrokerId, result.BrokerId);
        Assert.Equal(32000M, result.TotalCost);
    }
}