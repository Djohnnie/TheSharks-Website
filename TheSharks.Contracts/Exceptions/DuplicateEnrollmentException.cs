namespace TheSharks.Contracts.Exceptions;

public class DuplicateEnrollmentException : AppException
{
    public DuplicateEnrollmentException() : base() { }

    public DuplicateEnrollmentException(string message) : base(message) { }
}