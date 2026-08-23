using BCnEncoder.Encoder;
using BCnEncoder.ImageSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace UIArtTool
{
    public static class AtlasBuilder
    {
        public static void Build(IReadOnlyList<TextureAtlas> atlases, string inputDirectory)
        {
            foreach (TextureAtlas atlas in atlases)
            {
                string outputFilePath = Path.Combine(inputDirectory, atlas.Name);
                string imageDirectory = Path.Combine(inputDirectory, Path.ChangeExtension(atlas.Name, null));

                string directory = Path.GetDirectoryName(outputFilePath);
                if (Directory.Exists(directory) == false)
                    Directory.CreateDirectory(directory);

                try
                {
                    BuildAtlas(atlas, imageDirectory, outputFilePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine("Done building");
        }

        private static void BuildAtlas(TextureAtlas atlas, string imageDirectory, string outputFilePath)
        {
            int size = GetAtlasSize(atlas);
            Console.WriteLine($"Building atlas for {atlas.Name} ({size}x{size})...");

            using Image<Rgba32> atlasImage = new(size, size);

            foreach (TextureAtlasEntry entry in atlas.Entries)
            {
                string filePath = Path.Combine(imageDirectory, $"{entry.Name}.png");
                AddImage(entry, filePath, atlasImage);
            }

            BcEncoder encoder = new();
            encoder.OutputOptions.Format = BCnEncoder.Shared.CompressionFormat.Rgba;
            encoder.OutputOptions.Quality = CompressionQuality.BestQuality;
            encoder.OutputOptions.FileFormat = BCnEncoder.Shared.OutputFileFormat.Dds;

            using FileStream fs = File.Create(outputFilePath);
            encoder.EncodeToStream(atlasImage, fs);
        }

        private static void AddImage(TextureAtlasEntry entry, string filePath, Image<Rgba32> atlasImage)
        {
            if (Path.Exists(filePath) == false)
            {
                Console.WriteLine($"WARN: No image file for {entry.Name}");
                return;
            }

            using Image image = Image.Load(filePath);
            if (image.Width != entry.Width || image.Height != entry.Height)
            {
                Console.WriteLine($"WARN: Incorrect size for {entry} name (expected {entry.Width}x{entry.Height}, got {image.Width}x{image.Height})");
                return;
            }

            atlasImage.Mutate(x => x.Paint(canvas =>
            {
                Rectangle source = new(0, 0, entry.Width, entry.Height);
                RectangleF destination = new(entry.X, entry.Y, entry.Width, entry.Height);

                canvas.DrawImage(image, source, destination);
            }));
        }

        private static int GetAtlasSize(TextureAtlas atlas)
        {
            int xMax = int.MinValue;
            int yMax = int.MinValue;

            foreach (TextureAtlasEntry entry in atlas.Entries)
            {
                int xEnd = entry.X + entry.Width;
                xMax = Math.Max(xMax, xEnd);

                int yEnd = entry.Y + entry.Height;
                yMax = Math.Max(yMax, yEnd);
            }

            int size = 2;

            while (size < xMax || size < yMax)
                size *= 2;

            return size;
        }
    }
}
