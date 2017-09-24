/* This is the Database Options Handler, this will manage all the variables that can be accessed anywhere using the MDF_EDITOR namespace and accessing the options
 * feel free to add or remove whatever you would like if you want to change something please feel free to do so.
 * 
 * */

using System;

namespace MDF_EDITOR
{
    /// <summary>
    /// This class handles the database options that can be controlled through the OPTIONS_GUI
    /// </summary>
    [Serializable]
    public class Database_Options
    {
        /// <summary>
        /// This is the current Backup Location Selected
        /// </summary>
        public string BACKUP_LOCATION;

        /// <summary>
        /// This is the current Database Location Selected
        /// </summary>
        public string DATABASE_LOCATION;

        /// <summary>
        /// This is the Default asset type to be used for databases
        /// </summary>
        public string DEFAULT_ASSET_TYPE;

        /// <summary>
        /// This is the Default Backup Location for Databases
        /// </summary>
        public string DEFAULT_BACKUP_DIRECTORY; 

        /// <summary>
        /// This is the name of the DEFAULT Database Handler, that will handle all database operations
        /// </summary>
        public string DEFAULT_DATABASE_CLASS;
           
        /// <summary>
        /// This is the Default Database Directory where databases will be stored!
        /// </summary>
        public string DEFAULT_DATABASE_DIRECTORY;

        /// <summary>
        /// This is the Prefix to be used if it is enabled, example: the databse name is Tacos, and the prefix is DB_ it will be DB_Tacos
        /// </summary>
        public string DEFAULT_DATABASE_PREFIX;

        /// <summary>
        /// This is the Suffix to be used if it is enabled, example: the databse name is Tacos, and the Suffix is _DB it will be Tacos_DB
        /// </summary>
        public string DEFAULT_DATABASE_SUFFIX;

        /// <summary>
        /// DEFAULT_ENABLE_STANDALONE when enabled will NOT appear in the Database Gui, but can be opened directly by the GUI file it creates.
        /// Also if you choose to have the database appear in the menu it will appear and be accessed that way.
        /// </summary>
        public bool DEFAULT_ENABLE_STANDALONE;

        /// <summary>
        /// This will set the default for database creation to have Tabs if true, else no tabs is set to false by default.
        /// </summary>
        public bool DEFAULT_HAS_TABS;

        /// <summary>
        /// This sets the default amount of Tabs per row for the database
        /// </summary>
        public int DEFAULT_TABS_IN_DATABASE;

        /// <summary>
        /// This will enable a prefix to be added to the script naming conventions and access for the database by default
        /// </summary>
        public bool ENABLE_DATABASE_PREFIX;

        /// <summary>
        /// This will enable a suffix to be added to the script naming conventions and access for the database by default
        /// </summary>
        public bool ENABLE_DATABASE_SUFFIX;

        /// <summary>
        /// when set to false, you can set your own backup naming convention
        /// </summary>
        public bool ENABLE_DEFAULT_BACKUP_FOLDER_NAME;

        /// <summary>
        /// when true, this enables a warning before deleting database objects, for all databases EXCEPT the Database Manager
        /// </summary>
        public bool SHOW_DELETE_POPUP;

        /// <summary>
        /// This a toggle to enable the default backup directory, when set to false you can set a custom directory
        /// </summary>
        public bool USE_DEFAULT_BACKUP_DIRECTORY;

        /// <summary>
        /// This a toggle to use the default database directory, when set to false you can set a custom directory
        /// </summary>
        public bool USE_DEFAULT_DATABASE_DIRECTORY;

        /// <summary>
        /// This constructor sets the defaults for each of the options you can modify this constructor if you want to change the default options.
        /// </summary>
        public Database_Options()
        {
            //SETS DEFAULTS HERE//
            SHOW_DELETE_POPUP = true;
            DEFAULT_BACKUP_DIRECTORY = "Assets/MDF EDITOR/Backup";
            DATABASE_LOCATION = DEFAULT_DATABASE_DIRECTORY;
            BACKUP_LOCATION = DEFAULT_BACKUP_DIRECTORY;
            DEFAULT_DATABASE_DIRECTORY = "Database";
            USE_DEFAULT_BACKUP_DIRECTORY = true;
            USE_DEFAULT_DATABASE_DIRECTORY = true;
            ENABLE_DEFAULT_BACKUP_FOLDER_NAME = true;
            DEFAULT_ASSET_TYPE = "asset";
            DEFAULT_DATABASE_CLASS = "DATABASE";
            ENABLE_DATABASE_PREFIX = true;
            DEFAULT_DATABASE_PREFIX = "DB_";
            ENABLE_DATABASE_SUFFIX = false;
            DEFAULT_DATABASE_SUFFIX = "_DB";
            DEFAULT_HAS_TABS = true;
            DEFAULT_TABS_IN_DATABASE = 5;
            DEFAULT_ENABLE_STANDALONE = false;
        } 
    } 
} 