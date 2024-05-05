namespace Larkins.CSharpKatas.BlobNumbering;

public class BlobDetection2
{
    private readonly int height;
    private readonly int width;
    private readonly int[,] blobImage;
    private readonly int[,] enqueuedPixels;
    private readonly int[,] image;

    // UniqueQueue is a custom queue that only ever allows a pixel to be added once.
    // This handles the neighbour selection dealing with the same pixel multiple times.
    private readonly Queue<Pixel> pixelQueue = new();
    private int blobNumber;

    public BlobDetection2(int[,] image)
    {
        height = image.GetLength(0);
        width = image.GetLength(1);
        this.image = image;
        blobImage = new int[height, width];

        // This array indicates if a pixel has already been enqueued.
        enqueuedPixels = new int[height, width];
    }

    public int[,] GetBlobMap()
    {
        GoThroughEveryPixel();

        return blobImage;
    }

    private void GoThroughEveryPixel()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var pixel = new Pixel(y, x);

                if (!IsUnlabelledPixel(pixel))
                {
                    continue;
                }

                blobNumber++;

                pixelQueue.Enqueue(pixel);
                enqueuedPixels[y, x]++;

                FindAllPixelsInBlob();
            }
        }
    }

    private void FindAllPixelsInBlob()
    {
        while (pixelQueue.Count != 0)
        {
            var pixel = pixelQueue.Dequeue();

            blobImage[pixel.Y, pixel.X] = blobNumber;

            AddUnlabelledNeighboursToQueue(pixel);
        }
    }

    private void AddUnlabelledNeighboursToQueue(Pixel pixel)
    {
        var lowerYLimit = Math.Min(pixel.Y + 1, height - 1);
        var rightXLimit = Math.Min(pixel.X + 1, width - 1);

        for (var ny = Math.Max(pixel.Y - 1, 0); ny <= lowerYLimit; ny++)
        {
            for (var nx = Math.Max(pixel.X - 1, 0); nx <= rightXLimit; nx++)
            {
                var neighbouringPixel = new Pixel(ny, nx);

                if (CanNeighbourBeAdded(pixel, neighbouringPixel))
                {
                    pixelQueue.Enqueue(neighbouringPixel);
                    enqueuedPixels[ny, nx]++;
                }
            }
        }
    }

    private bool CanNeighbourBeAdded(Pixel pixel, Pixel neighbourPixel)
    {
        var isDifferentPixel = neighbourPixel != pixel;
        var isNotAlreadyEnqueued = enqueuedPixels[neighbourPixel.Y, neighbourPixel.X] == 0;

        return isDifferentPixel && IsUnlabelledPixel(neighbourPixel) && isNotAlreadyEnqueued;
    }

    private bool IsUnlabelledPixel(Pixel pixel)
    {
        var isBlobPixel = image[pixel.Y, pixel.X] == 1;
        var isUnlabelledPixel = blobImage[pixel.Y, pixel.X] == 0;

        return isBlobPixel && isUnlabelledPixel;
    }
}
