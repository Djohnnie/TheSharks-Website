namespace TheSharks.Contracts.Exceptions;

public class StorageException : AppException
{
    public StorageException() : base() { }

    public StorageException(string message) : base(message) { }
}