namespace TheSharks.Contracts.Exceptions;

public class RoleException : AppException
{
    public RoleException() : base() { }

    public RoleException(string message) : base(message) { }
}