namespace TheSharks.Contracts.Helpers;

public interface IEncryptionHelper
{
    string EncryptString(string data, string password);

    string DecryptString(string encrypted, string password);
}