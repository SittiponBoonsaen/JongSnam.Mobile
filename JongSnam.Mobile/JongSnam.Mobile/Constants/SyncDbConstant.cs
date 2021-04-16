using System;
using System.IO;

namespace JongSnam.Mobile.Constants
{
    /// <summary>
    /// The constants of sync local db.
    /// </summary>
    public struct SyncDbConstant
    {
        /// <summary>
        /// The flags of SQL Lite
        /// </summary>
        public const SQLite.SQLiteOpenFlags Flags =

            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |

            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |

            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        /// <summary>
        /// The databasefilename
        /// </summary>
        private const string DATABASEFILENAME = "Sync.db3";

        /// <summary>
        /// Gets the database path.
        /// </summary>
        /// <value>
        /// The database path.
        /// </value>
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DATABASEFILENAME);
            }
        }
    }
}
