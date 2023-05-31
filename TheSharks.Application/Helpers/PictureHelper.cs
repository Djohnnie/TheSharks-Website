using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using TheSharks.Contracts.Helpers;

namespace TheSharks.Application.Helpers;

public class PictureHelper : IPictureHelper
{
    public async Task<byte[]> PreparePicture(byte[] originalPictureBytes, int sizeLimit)
    {
        var originalPicture = Image.Load(originalPictureBytes);

        var originalPictureWidth = originalPicture.Width;
        var originalPictureHeight = originalPicture.Height;
        var targetPictureWidth = sizeLimit;
        var targetPictureHeight = sizeLimit;

        if (originalPictureWidth > targetPictureWidth && originalPictureWidth >= originalPictureHeight)
        {
            var resizePercentage = (float)targetPictureWidth / originalPictureWidth;
            targetPictureHeight = (int)(originalPictureHeight * resizePercentage);
        }

        if (originalPictureHeight > targetPictureHeight && originalPictureHeight >= originalPictureWidth)
        {
            var resizePercentage = (float)targetPictureHeight / originalPictureHeight;
            targetPictureWidth = (int)(originalPictureWidth * resizePercentage);
        }

        if (originalPictureWidth == targetPictureWidth && originalPictureHeight == targetPictureHeight)
        {
            return originalPictureBytes;
        }

        var targetPicture = originalPicture.Clone(x => x.Resize(targetPictureWidth, targetPictureHeight));

        await using var preparedPictureStream = new MemoryStream();
        await targetPicture.SaveAsJpegAsync(preparedPictureStream, new JpegEncoder { Quality = 75 });
        return preparedPictureStream.GetBuffer();
    }
}