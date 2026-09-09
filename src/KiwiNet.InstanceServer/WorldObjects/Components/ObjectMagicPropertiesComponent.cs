using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.WorldObjects.Components
{
    public sealed class ObjectMagicPropertiesComponent : WorldComponent
    {
        public byte Field0 { get; set; }
        public int Field1 { get; set; }
        public List<object> Field2 { get; } = new();

        public override void Serialize(NetworkConnection connection)
        {
            // same structure as ModsComponent?
            connection.Write(Field0);
            connection.Write(Field1);
            connection.Write(Field2.Count);
            foreach (object obj in Field2)
            {
                // TODO
            }
        }
    }
}
