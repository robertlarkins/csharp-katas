namespace Larkins.CSharpKatas.BlobNumbering;

public class BlobDetection
{
    private readonly int height;
    private readonly int width;
    private readonly int[,] blobImage;
    private readonly int[,] image;
    private readonly UniqueQueue<(int Y, int X)> pixelQueue = new();
    private int blobCount;

    public BlobDetection(int[,] image)
    {
        height = image.GetLength(0);
        width = image.GetLength(1);
        this.image = image;
        blobImage = new int[height, width];
    }

    public int[,] GetBlobMap()
    {
        // var pixelCount = new int[height, width];

        // This specialised queue is used so that any give pixel
        // can only ever be added once, otherwise it is ignored.
        // this handles the neighbour selection dealing with the same pixel multiple times.
        GoThroughEveryPixel();

        return blobImage;
    }

    private void GoThroughEveryPixel()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (image[y, x] == 0 || blobImage[y, x] != 0)
                {
                    continue;
                }

                blobCount++;

                pixelQueue.Enqueue((y, x));

                FindAllPixelsInBlob();
            }
        }
    }

    private void FindAllPixelsInBlob()
    {
        while (pixelQueue.Any())
        {
            var pixel = pixelQueue.Dequeue();

            // mark the pixel with identifier
            blobImage[pixel.Y, pixel.X] = blobCount;

            // pixelCount[pixel.y, pixel.x]++;
            AddNeighboursToQueue(pixel);
        }
    }

    private void AddNeighboursToQueue((int Y, int X) pixel)
    {
        // add blob neighbours to queue that aren't already marked
        for (int ny = Math.Max(pixel.Y - 1, 0); ny <= Math.Min(pixel.Y + 1, height - 1); ny++)
        {
            for (int nx = Math.Max(pixel.X - 1, 0); nx <= Math.Min(pixel.X + 1, width - 1); nx++)
            {
                var isSamePixel = ny == pixel.Y && nx == pixel.X;
                var isNonBlobPixel = image[ny, nx] == 0;
                var isAlreadyLabelledPixel = blobImage[ny, nx] != 0;

                if (isSamePixel || isNonBlobPixel || isAlreadyLabelledPixel)
                {
                    continue;
                }

                pixelQueue.Enqueue((ny, nx));
            }
        }
    }
}
