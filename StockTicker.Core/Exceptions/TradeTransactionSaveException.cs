namespace StockTicker.Core.Exceptions;

public class TradeTransactionSaveException : Exception
{
    public TradeTransactionSaveException()
    {
        
    }

    public TradeTransactionSaveException(string message): base(message)
    {
        
    }
}