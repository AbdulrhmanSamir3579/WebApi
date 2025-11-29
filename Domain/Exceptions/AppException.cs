// Domain/Exceptions/AppExceptions.cs
namespace Domain.Exceptions;

/// <summary>
/// Base application exception
/// </summary>
public abstract class AppException : Exception
{
    public List<string>? Errors { get; }

    protected AppException(string message, List<string>? errors = null) 
        : base(message)
    {
        Errors = errors;
    }
}

/// <summary>
/// 404 - Resource not found
/// </summary>
public class NotFoundException : AppException
{
    public NotFoundException(string message = "Resource not found")
        : base(message)
    {
    }

    public NotFoundException(string resourceName, object key)
        : base($"{resourceName} with id '{key}' was not found")
    {
    }
}

/// <summary>
/// 400 - Validation error
/// </summary>
public class ValidationException : AppException
{
    public ValidationException(string message, List<string> errors)
        : base(message, errors)
    {
    }

    public ValidationException(List<string> errors)
        : base("Validation failed", errors)
    {
    }

    public ValidationException(string singleError)
        : base("Validation failed", new List<string> { singleError })
    {
    }
}

/// <summary>
/// 400 - Bad request
/// </summary>
public class BadRequestException : AppException
{
    public BadRequestException(string message)
        : base(message)
    {
    }
}

/// <summary>
/// 409 - Conflict
/// </summary>
public class ConflictException : AppException
{
    public ConflictException(string message)
        : base(message)
    {
    }
}

/// <summary>
/// 401 - Unauthorized
/// </summary>
public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message = "Unauthorized")
        : base(message)
    {
    }
}

/// <summary>
/// 403 - Forbidden
/// </summary>
public class ForbiddenException : AppException
{
    public ForbiddenException(string message = "Forbidden")
        : base(message)
    {
    }
}