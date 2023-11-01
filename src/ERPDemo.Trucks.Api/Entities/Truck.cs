namespace ERPDemo.Trucks.Api.Entities;

public class Truck
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public TruckStatus Status { get; private set; }

    public Truck(
        string code,
        string name,
        TruckStatus status,
        string? description = null)
    {
        Code = code;
        Name = name;
        Status = status;
        Description = description;
    }

    public Result UpdateStatus(TruckStatus status)
    {
        var result = Status.Update(status);
        if (result.IsSuccess)
            Status = result.Value;

        return result;
    }
}
