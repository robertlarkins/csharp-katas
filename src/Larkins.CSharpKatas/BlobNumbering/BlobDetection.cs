namespace Larkins.CSharpKatas.BlobNumbering;

public class BlobDetection
{
    private readonly int height;
    private readonly int width;
    private readonly int[,] blobImage;
    private readonly int[,] image;
    private readonly UniqueQueue<Pixel> pixelQueue = new();
    private int blobNumber;

    public BlobDetection(int[,] image)
    {
        height = image.GetLength(0);
        width = image.GetLength(1);
        this.image = image;
        blobImage = new int[height, width];
    }

    public int[,] GetBlobMap()
    {
        // This specialised queue is used so that any give pixel
        // can only ever be added once, otherwise it is ignored.
        // this handles the neighbour selection dealing with the same pixel multiple times.
        // Alternatively, we could create a third image that indicates if a pixel has already been enqueued.
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
                }
            }
        }
    }

    private bool CanNeighbourBeAdded(Pixel pixel, Pixel neighbourPixel)
    {
        var isDifferentPixel = neighbourPixel != pixel;

        return isDifferentPixel && IsUnlabelledPixel(neighbourPixel);
    }

    private bool IsUnlabelledPixel(Pixel pixel)
    {
        var isBlobPixel = image[pixel.Y, pixel.X] == 1;
        var isUnlabelledPixel = blobImage[pixel.Y, pixel.X] == 0;

        return isBlobPixel && isUnlabelledPixel;
    }
}
