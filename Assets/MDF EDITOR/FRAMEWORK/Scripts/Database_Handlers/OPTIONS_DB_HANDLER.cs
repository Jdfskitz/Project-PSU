using UnityEngine;
// THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK

namespace MDF_EDITOR
{
    namespace DATABASE
    {
        public class OPTIONS_DB_HANDLER : ScriptableObject
        {
            public const string ASSET_NAME = "_Options"; // Name of the physical database file
            public const string ASSET_TYPE = ".asset"; // Extension of the database file
            public const string ASSET_PATH = "Assets/MDF EDITOR/FRAMEWORK"; //location of the database file

            public const string DATABASE_PATH = ASSET_PATH + "/" + ASSET_NAME + ASSET_TYPE;
                // full directory of the database file

            [SerializeField] // SerializeField allows the list to be viewable in the Inspector in Unity
            private Database_Options options_database; // this is the list that contains a list for all the values above

            private void OnEnable() // this is enabled ONCE per load
            {
                if (options_database == null) // Creates the list if it's NULL (does not exist)
                {
                    options_database = new Database_Options();
                }
            }

            // This is the function you call to access the database from scripts and other places. also note:  ElementAt() requires System.Linq 
            public Database_Options DB_Options()
            {
                return options_database;
            } // END DB_Options Method
        } // END Options_DB_Handler CLASS
    } // END OPTIONS NAMESPACE
} // END MDF_EDITOR NAMESPACE