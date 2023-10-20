namespace ERPDemo.Trucks.Entities;

public class Truck
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public TruckStatus Status { get; private set; }
    public TruckStatus StatusPrevious { get; private set; }

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

    public bool UpdateStatus(TruckStatus status)
    {
        if (status == TruckStatus.ToJob && Status != TruckStatus.Loading)
            return false;
        else
        if (status == TruckStatus.AtJob && Status != TruckStatus.ToJob)
            return false;
        else
        if (status == TruckStatus.Returning && Status != TruckStatus.ToJob)
            return false;

        StatusPrevious = Status;
        Status = status;

        return true;
    }
}

public record TruckStatus(string Value)
{
    public static readonly TruckStatus Loading = new("Loading");
    public static readonly TruckStatus ToJob = new("To Job");
    public static readonly TruckStatus AtJob = new("At Job");
    public static readonly TruckStatus Returning = new("At Job");
    public static readonly TruckStatus OutOfService = new("Out Of Service");
}
