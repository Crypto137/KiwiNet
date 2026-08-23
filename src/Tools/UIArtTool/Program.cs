using System.Text.Json;

namespace UIArtTool
{
    internal class Program
    {
        const string TextureAtlasMetadataFile = "TextureAtlases.json";

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Available arguments:");
                Console.WriteLine("--parse [metadata_dir]               Parses UI metadata extracted from GGPK");
                Console.WriteLine("--generate [output_dir]              Generates placeholder images");
                Console.WriteLine("--build [input_dir] [output_dir]     Builds atlas textures from individual files");
                return;
            }

            TextureAtlas[] atlases;
            switch (args[0])
            {
                case "--parse":
                    List<TextureAtlas> parsedAtlases = new();
                    MetadataParser.Parse(args[1], parsedAtlases);
                    SaveAtlases(parsedAtlases);
                    break;

                case "--generate":
                    atlases = LoadAtlases();
                    ImageGenerator.Generate(atlases, args[1]);
                    break;

                case "--build":
                    atlases = LoadAtlases();
                    AtlasBuilder.Build(atlases, args[1]);
                    break;

                default:
                    Console.WriteLine($"Unknown argument '{args[0]}'");
                    break;
            }
        }

        static TextureAtlas[] LoadAtlases()
        {
            string metadataFileName = Path.Combine(AppContext.BaseDirectory, TextureAtlasMetadataFile);
            if (File.Exists(metadataFileName) == false)
            {
                Console.WriteLine($"{TextureAtlasMetadataFile} not found");
                return Array.Empty<TextureAtlas>();
            }

            using FileStream fs = File.OpenRead(metadataFileName);
            TextureAtlas[] atlases;
            try
            {
                atlases = JsonSerializer.Deserialize<TextureAtlas[]>(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Array.Empty<TextureAtlas>();
            }

            Console.WriteLine($"Loaded {atlases.Length} texture atlases");
            return atlases;
        }

        static void SaveAtlases(List<TextureAtlas> atlases)
        {
            string metadataFileName = Path.Combine(AppContext.BaseDirectory, TextureAtlasMetadataFile);
            using FileStream fs = File.Create(metadataFileName);
            JsonSerializer.Serialize(fs, atlases, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
