namespace ERPDemo.Trucks.Api.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? str) =>
        str is null or [];
}
