namespace ERPDemo.Trucks.Api.Entities;

public class TruckStatus
{
    public static readonly TruckStatus Loading = new("Loading");
    public static readonly TruckStatus ToJob = new("To Job");
    public static readonly TruckStatus AtJob = new("At Job");
    public static readonly TruckStatus Returning = new("At Job");
    public static readonly TruckStatus OutOfService = new("Out Of Service");

    public string Value { get; init; }

    public TruckStatus(string value) => Value = value;
    public TruckStatus(TruckStatus status) => Value = status.Value;

    public Result<TruckStatus> Update(TruckStatus status)
    {
        var result = Result<TruckStatus>.Success();

        if (!status.IsValid())
            return result.Fail("The status is wrong");

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

    public bool IsValid() =>
        Value == Loading.Value ||
        Value == ToJob.Value ||
        Value == AtJob.Value ||
        Value == Returning.Value ||
        Value == OutOfService.Value;

    public static bool operator ==(TruckStatus left, TruckStatus right) =>
        left.Equals(right);

    public static bool operator !=(TruckStatus left, TruckStatus right) =>
        !left.Equals(right);

    public override bool Equals(object? obj)
    {
        if (obj is TruckStatus status)
            return status.Value == Value;

        return base.Equals(obj);
    }

    public override int GetHashCode() => Value.GetHashCode();
}
