namespace StockTicker.Core.Exceptions;

public class InvalidTradeTransactionException : Exception
{
    public InvalidTradeTransactionException()
    {
        
    }

    public InvalidTradeTransactionException(string message) : base(message)
    {
        
    }
}