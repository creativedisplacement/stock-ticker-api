using StockTicker.Core.Exceptions;

namespace StockTicker.Core.Validation;

public static class Guard
{
    public static void AgainstNullReferenceException(object obj, string? message = "")
    {
        if (obj == null)
        {
            throw new NullReferenceException(message ?? nameof(obj));
        }
    }

    public static void AgainstEmptyTicker(object obj, string? message = "")
    {
        if (string.IsNullOrEmpty((string)obj))
        {
            throw new InvalidTradeTransactionException(message ?? nameof(obj));
        }
    }
    
    public static void AgainstEmptyTickers(object[] obj, string? message = "")
    {
        if (obj.Length == 0)
        {
            throw new InvalidTradeTransactionException(message ?? nameof(obj));
        }
    }

    public static void AgainstDefaultDecimal(object obj, string? message = "")
    {
        if ((decimal)obj == default)
        {
            throw new InvalidTradeTransactionException(message ?? nameof(obj));
        }
    }
    
    public static void AgainstDefaultInteger(object obj, string? message = "")
    {
        if ((int)obj == default)
        {
            throw new InvalidTradeTransactionException(message ?? nameof(obj));
        }
    }
}