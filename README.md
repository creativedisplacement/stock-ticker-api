# Stock Ticker API

We are the London Stock Exchange and we are creating an API to receive notification of trades from authorised brokers and expose the updated price to them.

We need to receive every exchange of shares happening in real-time, and we need to know:

- What stock has been exchanged (using the ticker symbol)
- At what price (in pound)
- Number of shares (can be a decimal number)
- ID of broker managing the trade

A relational database is used to store all transactions happening. Consider a minimal schema and data structure. 
We need to expose the current value of a stock, values for all the stocks on the market and values for a range of them (as a list of ticker symbols).

For simplicity, consider the value of a stock as the average price of the stock through all transactions.
Assume you can use SDKs and middleware for common functionalities.

You task is to define a simple version of the API, including a definition of data model. Describe shortly the system design for an MVP.

## Notes

- CQRS pattern configured in the Application Layer (using Mediatr) would have been a better way to implement. 
- A generic repository with the specification pattern would have been cleaner are more extensible, this is a new pattern for me and due to time limits did not implement.
- Mapping was done manually for speed rather than use AutoMapper.
- Stock and TradeTransaction as one to many entity would be better in terms of database optimisation. For speed just used the TradeTransaction entity and took the penalty on redundancy.