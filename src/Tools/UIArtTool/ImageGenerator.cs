using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;

namespace UIArtTool
{
    public static class ImageGenerator
    {
        private static PatternBrush Brush;

        public static void Generate(IReadOnlyList<TextureAtlas> atlases, string outputDirectory)
        {
            foreach (TextureAtlas atlas in atlases)
            {
                string filePath = Path.Combine(outputDirectory, atlas.Name);
                string directory = Path.ChangeExtension(filePath, null);
                if (Directory.Exists(directory) == false)
                    Directory.CreateDirectory(directory);
                
                try
                {
                    GenerateForAtlas(atlas, directory);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine("Done generating");
        }

        private static void GenerateForAtlas(TextureAtlas atlas, string outputDirectory)
        {
            Console.WriteLine($"Generating images for atlas {atlas.Name}...");
            foreach (TextureAtlasEntry entry in atlas.Entries)
            {
                string filePath = Path.Combine(outputDirectory, $"{entry.Name}.png");
                GenerateImage(entry, GetBrush(), filePath);
            }
        }

        private static void GenerateImage(TextureAtlasEntry entry, Brush brush, string filePath)
        {
            using Image<Rgba32> image = new(entry.Width, entry.Height);
            image.Mutate(x => x.Paint(canvas =>
            {
                canvas.Fill(brush);
            }));

            image.SaveAsPng(filePath);
        }

        private static Brush GetBrush()
        {
            const string ForeColorHex = "FF00DC";
            const string BackColorHex = "CC00B4";
            const int PatternSize = 32;

            if (Brush == null)
            {
                Color foreColor = Color.ParseHex(ForeColorHex);
                Color backColor = Color.ParseHex(BackColorHex);

                bool[,] pattern = new bool[PatternSize, PatternSize];
                for (int y = 0; y < PatternSize; y++)
                {
                    for (int x = 0; x < PatternSize; x++)
                    {
                        int checkerX = x / (PatternSize / 2);
                        int checkerY = y / (PatternSize / 2);

                        pattern[y, x] = (checkerX + checkerY) % 2 == 0;
                    }
                }

                Brush = new PatternBrush(foreColor, backColor, pattern);
            }

            return Brush;
        }
    }
}
