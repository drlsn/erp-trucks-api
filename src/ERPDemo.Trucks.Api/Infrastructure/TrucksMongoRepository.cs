using ERPDemo.Trucks.Api.Entities;
using MongoDB.Driver;

namespace ERPDemo.Trucks.Api.Infrastructure;

public class TrucksMongoRepository : ITruckRepository
{
    private static readonly string CollectionName = "trucks";

    private readonly MongoClient _client;
    private readonly string _databaseName;

    public TrucksMongoRepository(
        MongoClient client, string databaseName)
    {
        _client = client;
        _databaseName = databaseName;
    }

    public async Task<bool> Add(Truck truck)
    {
        var collection = GetCollection();

        try
        {
            await collection.InsertOneAsync(truck).ConfigureAwait(false);
            return true;
        }
        catch (MongoException)
        {
            return false;
        }
    }

    public async Task<bool> Delete(TruckCode code)
    {
        var collection = GetCollection();

        try
        {
            var filter = Builders<Truck>.Filter.Eq(x => x.Id.Value, code.Value);
            var result = await collection.DeleteOneAsync(filter).ConfigureAwait(false);

            return result.DeletedCount > 0;
        }
        catch (MongoException)
        {
            return false;
        }
    }

    public async Task<Truck> Get(TruckCode code)
    {
        var filter = Builders<Truck>.Filter.Eq(x => x.Id.Value, code.Value);

        var collection = GetCollection();
        var result = await collection.FindAsync(filter);
        return await result.FirstOrDefaultAsync();
    }

    public async Task<bool> Update(Truck truck)
    {
        var collection = GetCollection();

        try
        {
            var version = truck.Version;

            truck.Version++;
            var result = await collection.ReplaceOneAsync(
                c => c.Id.Value == truck.Id.Value && c.Version == version, truck,
                new ReplaceOptions { IsUpsert = false }).ConfigureAwait(false);

            return result.ModifiedCount > 0;
        }
        catch (MongoException)
        {
            return false;
        }
    }

    private IMongoCollection<Truck> GetCollection()
    {
        var database = _client.GetDatabase(_databaseName);
        return database.GetCollection<Truck>(CollectionName);
    }
}
