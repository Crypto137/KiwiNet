namespace UIArtTool
{
    public class TextureAtlasEntry
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class TextureAtlas
    {
        public string Name { get; set; }
        public List<TextureAtlasEntry> Entries { get; set; } = new();

        public override string ToString()
        {
            return Name;
        }
    }
}
