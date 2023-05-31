namespace TheSharks.Contracts.Exceptions;

public class IdentityException : AppException
{
    public IdentityException() : base() { }

    public IdentityException(string message) : base(message) { }
}