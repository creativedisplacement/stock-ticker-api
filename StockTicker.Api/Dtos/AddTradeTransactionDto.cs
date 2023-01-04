namespace StockTicker.Api.Dtos;

public record AddTradeTransactionDto
{
    public string Ticker { get; init; }
    public decimal Price { get; init; }
    public decimal NumberOfShares { get; init; }
    public int BrokerId { get; init; }
};