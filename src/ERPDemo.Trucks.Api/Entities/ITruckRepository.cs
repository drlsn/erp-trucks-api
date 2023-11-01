namespace ERPDemo.Trucks.Api.Entities;

public interface ITruckRepository
{
    Task<Truck> Get(TruckCode code);
    Task<bool> Add(Truck truck);
    Task<bool> Update(Truck truck);
    Task<bool> Delete(TruckCode code);
}
