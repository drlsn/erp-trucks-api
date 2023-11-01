using MongoDB.Bson.Serialization.Attributes;

namespace ERPDemo.Trucks.Api.Entities;

public class Truck
{
    [BsonId]
    public TruckId Id { get; init; }
    public int Version { get; set; }

    public TruckCode Code { get; private set; }
    public TruckStatus Status { get; private set; }

    public string Name { get; set; }
    public string? Description { get; set; }

    public Truck(
        TruckCode code,
        TruckStatus status,
        string name,
        string? description = null)
    {
        Code = code;
        Status = status;
        Name = name;
        Description = description;

        Id = new TruckId(Code.Value);
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