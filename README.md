# Stock Ticker API

- CQRS pattern configured in the Application Layer (using Mediatr) would have been a better way to implement. Did not implement due to speed.
- A generic repository with the specification pattern would have been cleaner are more extensible. The specification pattern is new to me and due to time limits did not implement.
- Mapping was done manually for speed rather than use AutoMapper.
- Stock and TradeTransaction as one to many entity would be better in terms of database optimisation. For speed just used the TradeTransaction entity and took the penalty on redundancy.

## Enchancements

The system isn't particularly scalable in it's current guise. High traffic will be an issue due to the redundancy in the database design, and the way the average ticker values are calculated. The fact that a response from the database, which is somewhat coupled being awaited only adds to this.

In Azure, for example we could use queues. This way we fire of a message from the api to the queue and not wait for a response. Then we have another application who will be responsible for processing the message.

Queues have a first in first out model so they will be processed in order. I believe there is also a retry mechanism here.

We could also store the stock object in a cache and update with each successfully processed message from the queue. Which may be faster than a database.

We could also use scaling to ramp up the number of api instances, or the application consuming the messages indepdantly based on metrics like cpu, ram, etc.

Using records rather than classes should also be more performant

## Requirement

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
