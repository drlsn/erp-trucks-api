using MongoDB.Bson.Serialization.Attributes;

namespace ERPDemo.Trucks.Api.Entities;

public class Truck
{
    public static readonly string CollectionName = "trucks";

    [BsonId]
    public TruckId Id { get; init; }
    public int Version { get; set; }

    public TruckCode Code { get; private set; }
    public TruckStatus Status { get; private set; } = TruckStatus.OutOfService;

    public string Name { get; set; }
    public string? Description { get; set; }

    public Truck(
        TruckCode code,
        string name,
        string? description = null)
    {
        Code = code;
        Name = name;
        Description = description;

        Id = new TruckId(Code.Value);
    }

    public Truck(
        TruckCode code,
        TruckStatus status,
        string name,
        string? description = null) : this(code, name, description)
    {
        Status = status;
    }

    public Result UpdateStatus(TruckStatus status)
    {
        var result = Status.Update(status);
        if (result.IsSuccess)
            Status = result.Value;

        return result;
    }
}

public record TruckId(string Value);