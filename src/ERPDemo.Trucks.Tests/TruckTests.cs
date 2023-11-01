using ERPDemo.Trucks.Api.Entities;

namespace ERPDemo.Trucks.Entities.Tests;

public class TruckTests
{
    public static IEnumerable<TestCaseData> OutOfServiceStatus_CanBeSet_RegardlessOfStatus_TestCases =>
        AllStatuses.ToTestCaseData();

    [Test]
    [TestCaseSource(nameof(OutOfServiceStatus_CanBeSet_RegardlessOfStatus_TestCases))]
    public void OutOfServiceStatus_CanBeSet_RegardlessOfStatus(TruckStatus status)
    {
        var truck = CreateTruck(status);

        var result = truck.UpdateStatus(TruckStatus.OutOfService);

        Assert.IsTrue(result.IsSuccess);
        Assert.That(truck.Status, Is.EqualTo(TruckStatus.OutOfService));
    }

    [Test]
    public void ToJobStatus_CanBeSet_IfLoading()
    {
        var truck = CreateTruck(TruckStatus.Returning);

        var result = truck.UpdateStatus(TruckStatus.OutOfService);

        Assert.IsTrue(result.IsSuccess);
        Assert.That(truck.Status, Is.EqualTo(TruckStatus.OutOfService));
    }

    public static IEnumerable<TestCaseData> ToJobStatus_CannotBeSet_IfNotLoading_TestCases =>
        AllStatuses.Where(s => s != TruckStatus.Loading).ToTestCaseData();

    [Test]
    [TestCaseSource(nameof(ToJobStatus_CannotBeSet_IfNotLoading_TestCases))]
    public void ToJobStatus_CannotBeSet_IfNotLoading(TruckStatus status)
    {
        var truck = CreateTruck(status);

        var result = truck.UpdateStatus(TruckStatus.ToJob);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(truck.Status, Is.EqualTo(status));
    }

    private static Truck CreateTruck(TruckStatus status) => new("xyz", "Truck-1", status);

    private static IEnumerable<TruckStatus> AllStatuses { get; } = new TruckStatus[]
    {
        TruckStatus.OutOfService,
        TruckStatus.Loading,
        TruckStatus.AtJob,
        TruckStatus.ToJob,
        TruckStatus.Returning
    };
}

public static class TestCaseDataExtensions
{
    public static TestCaseData ToTestCaseData(this object @object) =>
        new TestCaseData(@object);

    public static IEnumerable<TestCaseData> ToTestCaseData(this IEnumerable<object> objects) =>
       objects.Select(@object => new TestCaseData(@object));
}
