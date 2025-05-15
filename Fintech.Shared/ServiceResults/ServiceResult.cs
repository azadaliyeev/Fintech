using System.Net;
using Newtonsoft.Json;

namespace Fintech.Shared.ServiceResults;


public class ServiceResult<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessage { get; set; }

    public string? SuccessMessage { get; set; }

    [Newtonsoft.Json.JsonIgnore] public HttpStatusCode Status { get; set; }

    [Newtonsoft.Json.JsonIgnore] public string? UrlAsCreated { get; set; }


    public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            Status = status
        };
    }

    public static ServiceResult<T>
        Success(T data, string successMessage, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            Status = status,
            SuccessMessage = successMessage
        };
    }

    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            Status = HttpStatusCode.Created,
            UrlAsCreated = urlAsCreated
        };
    }

    public static ServiceResult<T> SuccessAsCreated(T data, string successMessage, string urlAsCreated)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            Status = HttpStatusCode.Created,
            UrlAsCreated = urlAsCreated,
            SuccessMessage = successMessage
        };
    }

    public static ServiceResult<T> Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = errorMessages,
            Status = status
        };
    }

    public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = [errorMessage],
            Status = status
        };
    }
}

public class ServiceResult
{
    public List<string>? ErrorMessage { get; set; }

     public string? SuccessMessage { get; set; }

    [Newtonsoft.Json.JsonIgnore] public HttpStatusCode Status { get; set; }

    [Newtonsoft.Json.JsonIgnore] public string? UrlAsCreated { get; set; }


    public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult()
        {
            Status = status
        };
    }

    public static ServiceResult Success(string successMessage, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult()
        {
            SuccessMessage = successMessage,
            Status = status
        };
    }


    public static ServiceResult Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessage = errorMessages,
            Status = status
        };
    }

    public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessage = [errorMessage],
            Status = status
        };
    }
}