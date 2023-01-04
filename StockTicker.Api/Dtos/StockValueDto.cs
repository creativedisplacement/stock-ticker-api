namespace StockTicker.Api.Dtos;

public record StockValueDto
{
    public string Ticker { get; init; }
    public decimal Value { get; init; }
}