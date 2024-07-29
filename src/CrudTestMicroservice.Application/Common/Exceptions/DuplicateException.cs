namespace CrudTestMicroservice.Application.Common.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException()
        : base()
    {
    }

    public DuplicateException(string message)
        : base(message)
    {
    }

    public DuplicateException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public DuplicateException(string name, object key)
        : base($"Entity \"{name}\" ({key}) already exists.")
    {
    }

    public DuplicateException(string name, string field, object? key)
        : base($"Entity \"{name}\" field \"{field}\" ({key}) already exists.")
    {
    }
}
