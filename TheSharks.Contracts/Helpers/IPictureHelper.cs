namespace TheSharks.Contracts.Helpers;

public interface IPictureHelper
{
    Task<byte[]> PreparePicture(byte[] originalPictureBytes, int sizeLimit);
}