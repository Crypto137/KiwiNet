using System.Text;

namespace UIArtTool
{
    public static class MetadataParser
    {
        public static void Parse(string directory, List<TextureAtlas> atlases)
        {
            foreach (string filePath in Directory.GetFiles(directory, "*.ui", SearchOption.AllDirectories))
                ParseFile(filePath, atlases);

            Console.WriteLine("Done UI parsing");
        }

        private static void ParseFile(string filePath, List<TextureAtlas> atlases)
        {
            string fileName = Path.GetFileName(filePath);
            Console.WriteLine($"Parsing {fileName}...");

            using StreamReader reader = new(filePath, Encoding.Unicode);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("define images in", StringComparison.OrdinalIgnoreCase))
                {
                    string atlasName = line.Split('\"', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1];
                    Console.WriteLine($"Found image definitions for {atlasName} in {fileName}");

                    TextureAtlas atlas = atlases.Find(atlas => atlas.Name == atlasName);
                    if (atlas == null)
                    {
                        atlas = new() { Name = atlasName };
                        atlases.Add(atlas);
                    }

                    ParseImageDefinitions(reader, atlas);
                }
            }
        }

        private static void ParseImageDefinitions(StreamReader reader, TextureAtlas atlas)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();

                if (line.StartsWith("end", StringComparison.OrdinalIgnoreCase))
                    break;

                if (line.StartsWith("//", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (line.Contains('=', StringComparison.OrdinalIgnoreCase) == false)
                    continue;

                string[] tokens = line.Split('=', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                string name = tokens[0];
                string size = tokens[1].TrimEnd(';');

                int x, y, width, height;

                if (size.StartsWith("by_size", StringComparison.OrdinalIgnoreCase))
                {
                    size = size.Split('(', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1];
                    size = size.Split(')', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0];
                    string[] coords = size.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    // hacky formula evaluation
                    if (coords[1].Contains(" + 108 * "))
                    {
                        string[] subTokens = coords[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        int baseValue = int.Parse(subTokens[0]);
                        int multiplier = int.Parse(subTokens[4]);
                        coords[1] = $"{baseValue + (108 * multiplier)}";
                    }

                    x = int.Parse(coords[0]);
                    y = int.Parse(coords[1]);
                    width = int.Parse(coords[2]);
                    height = int.Parse(coords[3]);
                }
                else
                {
                    string[] coords = size.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    x = int.Parse(coords[0]);
                    y = int.Parse(coords[1]);
                    width = int.Parse(coords[2]) - x + 1;
                    height = int.Parse(coords[3]) - y + 1;
                }

                TextureAtlasEntry entry = new()
                {
                    Name = name,
                    X = x,
                    Y = y,
                    Width = width,
                    Height = height,
                };

                atlas.Entries.Add(entry);

                Console.WriteLine($"Found and parsed definition for {name} ({width}x{height} at {x},{y})");
            }
        }
    }
}
