using KiwiNet.Core.Math;

namespace KiwiNet.InstanceServer.Items
{
    /// <summary>
    /// Interface for subscribing to <see cref="Inventory"/> events.
    /// </summary>
    public interface IInventorySubscriber
    {
        /// <summary>
        /// Called when an <see cref="Item"/> is added to an <see cref="Inventory"/>.
        /// </summary>
        public void OnItemAdded(Inventory inventory, Item item, Vector2Int position);

        /// <summary>
        /// Called when an <see cref="Item"/> is removed from an <see cref="Inventory"/>.
        /// </summary>
        public void OnItemRemoved(Inventory inventory, Item item);
    }
}
