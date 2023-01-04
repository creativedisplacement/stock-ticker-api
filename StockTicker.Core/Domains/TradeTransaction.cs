using System.ComponentModel.DataAnnotations;

namespace StockTicker.Core.Domains;

/// <summary>
/// Would usually do this in fluent assertion
/// </summary>
public record TradeTransaction
{
    public int TradeTransactionId { get; init; }
    
    [MaxLength(10)]
    public string Ticker { get; init; }

    [DataType(DataType.Currency)]
    public decimal Price { get; init; }
    
    public decimal NumberOfShares { get; init; }
    
    [DataType(DataType.Currency)]
    public decimal TotalCost { get; init; }
    
    public int BrokerId { get; init; }
    
    public DateTime Timestamp { get; init; }
    
}