using ERPDemo.Trucks.Api.Entities;
using MongoDB.Driver;

namespace ERPDemo.Trucks.Api.Infrastructure;

public static class TrucksStartupExtensions
{

    public static async Task AddTrucksService(this IServiceCollection services)
    {
        string? connectionString =
            Environment.GetEnvironmentVariable($"{Configuration.DatabaseName}_cs") ?? "mongodb://localhost:27017/";

        services.AddScoped(sp =>
            new MongoClient(connectionString));

        services.AddScoped<ITruckRepository>(sp =>
            new TrucksMongoRepository(
                sp.GetRequiredService<MongoClient>(),
                Configuration.DatabaseName));

        services.AddScoped<GetTrucksQueryHandler>();

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(Configuration.DatabaseName);

        await database.CreateIndex<Truck>(Truck.CollectionName, keys => keys
            .Ascending(x => x.Status)
            .Ascending(x => x.Name));
    }

    private static async Task CreateIndex<T>(
        this IMongoDatabase database,
        string collectionName,
        Func<IndexKeysDefinitionBuilder<T>, IndexKeysDefinition<T>> buildIndex)
    {
        var indexKeys = buildIndex(Builders<T>.IndexKeys);
        var indexModel = new CreateIndexModel<T>(indexKeys);
        var eventsCollection = database.GetCollection<T>(collectionName);
        await eventsCollection.Indexes.CreateOneAsync(indexModel);
    }
}
