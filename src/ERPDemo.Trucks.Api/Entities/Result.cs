﻿namespace ERPDemo.Trucks.Api.Entities;

public class Result
{
    public static Result Success() => new Result(true);
    public static Result Success(object value) => new Result(true, value);
    public static Result Failure() => new Result(false);

    public Result(bool isSuccess, object value = null)
    {
        IsSuccess = isSuccess;
        ValueObject = value;
    }

    public bool IsSuccess { get; protected set; }
    public object ValueObject { get; protected set; }
    public string? Message { get; protected set; }

    public static implicit operator Result(bool isSuccess) => new Result(isSuccess);

    public Result Fail(string? message = null)
    {
        IsSuccess = false;
        Message = message;
        return this;
    }
}

public class Result<T> : Result
{
    public static new Result<T> Success() => new Result<T>(true);
    public static Result<T> Success(T value) => new Result<T>(true, value);
    public static new Result<T> Failure() => new Result<T>(false);

    public Result(bool isSuccess, T value = default) : base(isSuccess, value) {}

    public T Value => (T) ValueObject;

    public static implicit operator Result<T>(bool isSuccess) => new Result<T>(isSuccess);
    public static implicit operator Result<T>(T value) => new Result<T>(true, value);

    public new Result<T> Fail(string? message = null)
    {
        IsSuccess = false;
        Message = message;
        return this;
    }
}
