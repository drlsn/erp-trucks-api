namespace ERPDemo.Trucks.Api.Entities;

public class TruckStatus
{
    public static readonly TruckStatus Loading = new("Loading");
    public static readonly TruckStatus ToJob = new("To Job");
    public static readonly TruckStatus AtJob = new("At Job");
    public static readonly TruckStatus Returning = new("At Job");
    public static readonly TruckStatus OutOfService = new("Out Of Service");

    private readonly string _value;

    public TruckStatus(string value) => _value = value;
    public TruckStatus(TruckStatus status) => _value = status._value;

    public Result<TruckStatus> Update(TruckStatus status)
    {
        var result = Result<TruckStatus>.Success();

        if (status == TruckStatus.ToJob && this != TruckStatus.Loading)
            return result.Fail("ToJob state can set only if currently Loading");
        else
        if (status == TruckStatus.AtJob && this != TruckStatus.ToJob)
            return result.Fail("AtJob state can set only if currently ToJob");
        else
        if (status == TruckStatus.Returning && this != TruckStatus.AtJob)
            return result.Fail("Returning state can set only if currently AtJob");

        return status;
    }
}
