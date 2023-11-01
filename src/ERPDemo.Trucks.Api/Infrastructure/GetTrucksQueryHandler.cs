using ERPDemo.Trucks.Api.Entities;
using ERPDemo.Trucks.Api.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ERPDemo.Trucks.Api.Infrastructure;

public class GetTrucksQueryHandler
{
    private const int MaxItemLimit = 100;

    private readonly MongoClient _client;

    public GetTrucksQueryHandler(MongoClient client) =>
        _client = client;
    
    public async Task<GetTrucksQueryResponse> Handle(GetTrucksQuery query)
    {
        var database = _client.GetDatabase(Configuration.DatabaseName);
        var collection = database.GetCollection<Truck>(Truck.CollectionName);

        var filter = Builders<Truck>.Filter.Empty;
        if (!query.Status.IsNullOrEmpty())
            filter &= Builders<Truck>.Filter.Eq(x => x.Status.Value, query.Status);
        if (!query.Name.IsNullOrEmpty())
            filter &= Builders<Truck>.Filter.Eq(x => x.Name, query.Name);

        var hintDict = new Dictionary<string, object>();
        if (!query.SortBy.IsNullOrEmpty())
        {
            hintDict.Add(nameof(Truck.Status), 1);
            hintDict.Add(nameof(Truck.Name), 1);
        }

        var trucks = await collection
            .Aggregate(new AggregateOptions() { Hint = new BsonDocument(hintDict) })
            .Match(filter)
            .Limit(MaxItemLimit)
            .ToListAsync();

        return new(trucks.ToArray());
    }
}

public record GetTrucksQuery(
    string? SortBy = null,
    string? Status = null,
    string? Name = null);

public record GetTrucksQueryResponse(
    Truck[] Trucks);
