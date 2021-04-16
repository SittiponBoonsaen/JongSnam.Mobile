using SQLite;

namespace JongSnam.Mobile.Models
{
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the synchronize identifier.
        /// </summary>
        /// <value>
        /// The synchronize identifier.
        /// </value>
        [PrimaryKey, AutoIncrement]
        public int SyncId { get; set; }
    }
}
