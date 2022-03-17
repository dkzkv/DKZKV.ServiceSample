# Book store service
### What is this project and why is it needed?
This is a test training project designed to demonstrate DDD, CQRS with a separate model for each layer.
+ Pure domain, without any correlation with context or database
+ Separeted commands and queries in defferent projects 
+ Separate models for each layer. Which allows you to freely denormalize data between layers and not be tied to technologies
    + Presentation model  
    + Query/View model 
    + Domain model 
    + Persisntece model 
    + Validation in corruption layer
    + Transactional commands 
***A single write/read model is used, but it can be easily separated***

#### Important!!!
The repository is exclusively educational in nature and serves as an example, you should not use it as a template in a real project. Some points in the code are controversial and serve for better clarity.

If we are going to make a simple service that will never be changed, then of course we should not do DDD, prescribe domain mapping models and implement several projects
+ API
+ Application
+ Domain
+ Infrastructure

If the service is *stupid*, then calling dbcontext from the controller, which in turn returns a dynamic object, **is not a crime**. Such service does not scale, but it is written in 5 real minutes, not in *5 minutes of a programmer*

#### Synopsis
Robert Martin at his [speech](https://www.youtube.com/watch?v=Nsjsiz2A9mg&t=2535s) said the following words:
> If there is a word has defined our industry for the last 30 years, if there is a word that strikes fear into the hearts of developers for the last 30 years...that word would probably be **DATABASES**

Yes, and we have long gone away from this, but now most projects have the following problem, they are too closely related to the context (Entity framework) and this leads to the fact that the model itself is changing just to fit it to the possibility of saving to the database. And this is already wrong!

Referring to the following topic, we quote the following fragment
> I think 90% of times I see a DDD question on StackOverflow, it's something like this: 'I have this domain object' and the code shows an EF (Entity Framework) or NH (NHibernate) entity.
> NO! This is WRONG (99% of times anyway)!

But, lets loook what Microsoft [says](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice) about this:
> Following the Persistence Ignorance and the Infrastructure Ignorance principles, this layer must completely ignore data persistence details. These persistence tasks should be performed by the infrastructure layer. 

And another very important case:
> In some entity models, the model might fit, but usually it does not.

I completely agree, although Microsoft contradicts itself in some places by linking the model to Entity Core in theirs [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers) service.

##### What if domain model does not fit?
Lets look at common order book (financial term)
```CSharp
public class OrderBook : IAggregateRoot
{
    public string ClassCode { get; }
    public string SecurityCode { get; }
    public DateTimeOffset SnapshotTime { get; }
    public IEnumerable<OrderbookEntry> Bids { get; }
    public IEnumerable<OrderbookEntry> Asks { get; }

    public OrderBook(string classCode, string securityCode,
        DateTimeOffset snapshotTime,
        OrderbookEntry[] asks,
        OrderbookEntry[] bids)
    {
        if (string.IsNullOrEmpty(classCode) || string.IsNullOrEmpty(securityCode))
            throw new ArgumentException("Invalid order book identifier");

        if (snapshotTime.Equals(DateTimeOffset.MinValue))
            throw new ArgumentException("Invalid order book snapshot time");

        ClassCode = classCode;
        SecurityCode = securityCode;
        SnapshotTime = snapshotTime;
        ValidateOrderBookEntries(bids);
        ValidateOrderBookEntries(asks);

        Asks = asks;
        Bids = bids;
    }

    private void ValidateOrderBookEntries(IEnumerable<OrderbookEntry> entries)
    {
        if (entries.GroupBy(o => o.Price).Any(x => x.Count() > 1))
            throw new ArgumentException("Price in orderbook entries must be unique!");
    }
}

public class OrderbookEntry : ValueObject
{
    public decimal Quantity { get; }
    public decimal Price { get; }

    public OrderbookEntry(decimal price, decimal quantity)
    {
        Price = price;

        if (quantity == 0)
            throw new ArgumentException("Price can not be zero");
        Quantity = quantity;
    }
}
```
For example we want to store data as column storage, as so we should denormalize domain model to persistence model.
```CSharp
public class OrderbookPriceEntry : IEntityTypeConfiguration<OrderbookPriceEntry>
{
    public string ClassCode { get; set; }
    public string SecurityCode { get; set; }
    public string Price { get; set; }
    public decimal Quantity { get; set; }
    public OrderSide OrderSide { get; set; }
    public DateTime UtcOrderBookSnapshot { get; set; }

    public void Configure(EntityTypeBuilder<OrderbookPriceEntry> builder)
    {
        builder.ToTable("OrderbookPriceEntries")
            .HasNoKey(); // for bulk savings in sql server

        builder.Property(o => o.ClassCode)
            .HasColumnType("varchar(32)")
            .IsRequired();
        
        //Other definitions here
        
        builder.Property(o => o.OrderSide)
            .HasColumnType("bit")
            .HasConversion(side => side == 0, side => side ? OrderSide.Buy : OrderSide.Sell)
            .IsRequired();
    }
}
```
At first glance it seems quite obvious that we should always separate the house model and the n model, then is it so? Consider the [pros and cons of separating](https://stackoverflow.com/questions/24703756/having-separate-domain-model-and-persistence-model-in-ddd)
##### Pros and Cons:
###### Pros
- Completely free to refactor the domain
- It'll get easy to dig into another topics of DDD like Bounded Context.

###### Cons
- More efforts with data conversions between the layers. Development time (maybe also runtime) will get slower.
- But the principal and, believe me, what will hurt more: You will lose the main beneffits of using an ORM! Like tracking changes. Maybe you will end up using frameworks like GraphDiff or even abandon ORM's and go to the pure ADO.NET.

#### In conclusion
Should we now abandon Entity Core in the name of pure domain models? Of course not! Entity Core is a very powerful tool that greatly simplifies development. A further comparative analysis is described in this [article](https://enterprisecraftsmanship.com/posts/having-the-domain-model-separate-from-the-persistence-model/).
Here we can see a correlation between the complexity and purity of domain models. That at least do not tracked in EF Core.

![](https://raw.githubusercontent.com/dkzkv/DKZKV.ServiceSample/main/assets/2016-04-05-2.png)

### Structure
**Api** 
- Controllers 
- Presentation models
 
**Application** 
+ **Application.Commands** 
    + Commands 
    + Commands handlers 
    + Validation 
    + Extensions for commands pipeline behavior such as transactional behaviour 

- **Application.Queries**
    + Queries 
    + Query models aka View models 

**Domain** 
+ AgrettionRoots 
+ Entityies 
+ ValueObjects 
+ Enumerations 
+ Repositories 
+ Seeds 

**Persistence / Infrastrucure**
+ Persistence models 
+ Repository implemetations 
+ Query handlers 
+ DbContext
