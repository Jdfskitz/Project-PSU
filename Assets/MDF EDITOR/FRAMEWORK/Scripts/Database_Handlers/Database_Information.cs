using System;
using System.Collections.Generic;
// THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK
// Needed for Lists/Dictionaries/SortedLists - And lots of other useful collection types.

/* This is the Scriptable Object and Handler for the Database Information Database.
 * This is used to store database information so that databases can be easily Created/Edited/Deleted and have it be reflected in here. This also serves as
 * Backup file to preserve database structure to re-create databases later. This is utilized in the DATABASE_EDITOR script and is also refereed to by the MANAGE_DATABASE script
 * If you want to modify the information stored for each database please feel free to add it here!
 * We will explain how each variable is used for easy reference.
 * 
 * */

namespace MDF_EDITOR
{
    namespace DATABASE
    {
        /// <summary>
        /// This class is for tab information for each database.
        /// </summary>
        [Serializable]
        public class DB_Tabs
        {
            /// <summary>
            /// This is the name of the tab.
            /// </summary>
            public string tab_name = ""; 

            /// <summary>
            /// This is a list of <see cref="DB_Variables"/> for the current tab.
            /// </summary>
            public List<DB_Variables> tab_variables = new List<DB_Variables>(); // 
        }

        /// <summary>
        /// This class handles the variables in the <see cref="DB_Database_INFO"/>.
        /// </summary>
        [Serializable]
        public class DB_Variables
        {
            /// <summary>
            /// The name of the variable
            /// </summary>
            public string var_name;
            //			public REFERENCE_DATATYPE second_ref_variable_type; // this stores an ENUM value of a Second Variable Reference Type to be filtered into second_variable_type
            /// <summary>
            /// This stores an ENUM value for the currently selected first variable type.
            /// </summary>
            public SELECTED_TYPE selected_variable_type;

            /// <summary>
            /// This stores an ENUM value of the type of LIST or (NONE) for no list, for the variable
            /// </summary>
            public LIST_TYPE var_list_type;


            /// <summary>
            /// The second variable type for SortedLists/Dictionaries/SortedDictionaries
            /// </summary>
            public string var_second_type; // The second variable type for SortedLists/Dictionaries/SortedDictionaries 

 //			public VALUE_DATATYPE second_value_variable_type; // This stores an ENUM value of a Second Variable Value Type to be filtered into the second_variable_type


            /// <summary>
            /// The type of variable
            /// </summary>
            public string var_type;

            /// <summary>
            /// This stores an ENUM value of a Variable Reference Type to be filtered into <see cref="var_type"/>
            /// </summary>
            public REFERENCE_DATATYPE variable_reference_type;

            /// <summary>
            /// This stores an ENUM value of a Variable Value Type to be filtered into <see cref="var_type"/>
            /// </summary>
            public VALUE_DATATYPE variable_value_type;

//			public SELECTED_TYPE second_selected_variable_type;// This stores an ENUM value for the currently selected second variable type.

            /// <summary>
            /// this class constructor sets all it's variables.
            /// </summary>
            public DB_Variables()
            {
                var_name = "";
                var_type = "";
                var_second_type = "";// This is only used when a SortedList/Dictionary/SortedDictionary are the current List Type
                variable_value_type = VALUE_DATATYPE.NONE; // Defaults to NONE (This will set variable_type = "")
//				second_value_variable_type = VALUE_DATATYPE.NONE; // Defaults to NONE (This will set second_variable_type = "")
                variable_reference_type = REFERENCE_DATATYPE.NONE;
//				second_ref_variable_type = REFERENCE_DATATYPE.NONE; // Defaults to NONE (This will set second_variable_type = "")
                var_list_type = LIST_TYPE.NONE;
                selected_variable_type = SELECTED_TYPE.NONE;
//				second_selected_variable_type = SELECTED_TYPE.NONE; // Defaults to NONE (This means NO TYPE has been selected yet for the second variable, VALUE or REFERNECE or Another Type)
            }

            /// <summary>
            /// this class constructor overload that sets all it's variables based on the passed in <see cref="DB_Variables"/>
            /// </summary>
            public DB_Variables(DB_Variables _db_variables)
            {
                var_name = _db_variables.var_name;
                var_type = _db_variables.var_type;
                var_second_type = _db_variables.var_second_type;// This is only used when a SortedList/Dictionary/SortedDictionary are the current List Type
                variable_value_type = _db_variables.variable_value_type; // Defaults to NONE (This will set variable_type = "")
                //				second_value_variable_type = VALUE_DATATYPE.NONE; // Defaults to NONE (This will set second_variable_type = "")
                variable_reference_type = _db_variables.variable_reference_type;
                //				second_ref_variable_type = REFERENCE_DATATYPE.NONE; // Defaults to NONE (This will set second_variable_type = "")
                var_list_type = _db_variables.var_list_type;
                selected_variable_type = _db_variables.selected_variable_type;
                //				second_selected_variable_type = SELECTED_TYPE.NONE; // Defaults to NONE (This means NO TYPE has been selected yet for the second variable, VALUE or REFERNECE or Another Type)
            }
        } 

        /// <summary>
        /// This class handles database information that is setup in the DATABASE_MANAGER_GUI. This tracks all the changes made within the editor and heavily used when code generation takes place.
        /// </summary>
        [Serializable]
        public class DB_Database_INFO
        {
            /// <summary>
            /// REQUIRED - This is the Name of the Database
            /// </summary>
            public string NAME;

            /// <summary>
            /// REQUIRED - This is the ID of the Database. Also used for index purposes.
            /// </summary>
            public int ID;

            /// <summary>
            /// This value is for when the database is created successfully it will set to true. When false the database has yet to be generated.
            /// </summary>
            public bool SAVED;

            /// <summary>
            /// This is the name of the database that was stored when the database was generated last.
            /// </summary>
            public string SAVED_DATABASE_NAME;

            /// <summary>
            /// This is the Asset type, by Default this will be .asset but later will be expanded to support for file format types!
            /// </summary>
            public string asset_type;

            /// <summary>
            /// This is the name of a CUSTOM DATABASE CLASS, this is the script access for the database DEFAULT IS DATABASE, by doing DATABASE.TestDatabase
            /// </summary>
            public string database_class;

            /// <summary>
            /// The location to be used as a Custom Location, [Visible when <see cref="enable_custom_location"/> == true]
            /// </summary>
            public string database_location;

            /// <summary>
            /// // This stores the Prefix that will be used.
            /// </summary>
            public string database_prefix;

            /// <summary>
            /// This stores the Suffix that will be used.
            /// </summary>
            public string database_suffix; 

            /// <summary>
            /// TODO:Add good description for dropdown_bool_list
            /// </summary>
            public List<bool> dropdown_bool_list;

            /// <summary>
            /// this toggle allows users to customize the asset extension.
            /// </summary>
            public bool enable_custom_asset_type;

            /// <summary>
            /// This toggle enables a Custom class to be used for the database!
            /// </summary>
            public bool enable_custom_database_class; 

            /// <summary>
            /// This enables a custom location to be selected for the Database, set to false by default.
            /// </summary>
            public bool enable_custom_location;
             
            /// <summary>
            /// This allows you to have a custom GUI skin for the database, set to false by default.
            /// </summary>
            public bool enable_custom_skin;

            /// <summary>
            /// This toggle allows the user to choose if they want a prefix for the Database Script. Example: DB_ will show DB_ExampleDatabase
            /// </summary>
            public bool enable_database_prefix;

            /// <summary>
            /// This toggle allows the user to choose if they want a suffix for the Database Handler Script. Example: _DB will show ExampleDatabase_DB
            /// </summary>
            public bool enable_database_suffix;

            /// <summary>
            /// When enabled will allow the Database to appear in Unities Menu bar under MDF EDITOR, based on the <see cref="show_in_menu_name"/> variable for the name!
            /// </summary>
            public bool enable_shortcut;

            /// <summary>
            /// This toggle enables or disables tabs
            /// </summary>
            public bool enable_tabs;

            /// <summary>
            /// when <see cref="enable_shortcut"/> is true; options: NONE,CTRL_or_CMD, SHIFT, ALT
            /// </summary>
            public HOTKEY hotkey1;

            /// <summary>
            /// when <see cref="enable_shortcut"/> is true; options: NONE,CTRL_or_CMD, SHIFT, ALT
            /// </summary>
            public HOTKEY hotkey2;

            /// <summary>
            /// This is for setting information to display for the database.
            /// </summary>
            public string information; 

            /// <summary>
            /// This is the shortcut keys to quickly access the database if <see cref="show_in_menu"/> is enabled.
            /// </summary>
            public string shortcut_key;

            /// <summary>
            /// The keyboard letter used for shortcuts
            /// </summary>
            public string shortcut_letter;

            /// <summary>
            /// When enabled will allow the database to appear in Unity's menu bar under <see cref="MDF_EDITOR"/>, based on the <see cref="show_in_menu_name"/> variable for the name.
            /// </summary>
            public bool show_in_menu;

            /// <summary>
            /// This is the name to show for the <see cref="MDF_EDITOR"/> drop-down menu to access the database
            /// </summary>
            public string show_in_menu_name;

            /// <summary>
            /// This is the location of the GUI skin that you can have for a custom skin.
            /// </summary>
            public string skin_location;

            /// <summary>
            /// This enables the database to be standalone which means that it will not be automatically added as a tab to the DATABASE_GUI
            /// </summary>
            public bool standalone;

            /// <summary>
            /// This list contains <see cref="DB_Tabs"/>, to reference all the Tabs and everything inside them
            /// </summary>
            public List<DB_Tabs> tabs;

            /// <summary>
            /// This is the number of tabs inside the database Per each horizontal row, This is just the Limit of tabs per row to display.
            /// </summary>
            public int tabs_per_row;

            /// <summary>
            /// This list contains <see cref="DB_Variables"/> for each variable, (USED FOR ALL VARIABLES WITHIN A DATABASE)
            /// </summary>
            public List<DB_Variables> variables;

            /// <summary>
            /// This is the class constructor that will set defaults for the all the variables in <see cref="DB_Database_INFO"/>
            /// </summary>
            public DB_Database_INFO() 
            {
                // BELOW SETS THE DEFAULTS for ALL the VARIABLES ABOVE!
                ID = 0;
                NAME = "";
                SAVED_DATABASE_NAME = "";
                SAVED = false;
                information = "SAMPLE INFO! PLEASE REPLACE THIS WITH YOUR OWN INFORMATION FOR YOUR DATABASE!";
                enable_tabs = false;
                enable_database_prefix = true;
                database_prefix = "DB_";
                enable_database_suffix = false;
                database_suffix = "_DB";
                enable_custom_location = false;
                database_location = "Database"; // set the default to the OPTIONS Default databse location
                enable_custom_asset_type = false;
                asset_type = ".asset"; // set this to the default in the OPTIONS default asset type.
                enable_custom_database_class = false;
                database_class = "DATABASE"; // set this to the default in the OPTIONS DEFAULT HANDLER
                standalone = false;
                show_in_menu = false;
                show_in_menu_name = NAME;
                enable_shortcut = false;
                hotkey1 = HOTKEY.NONE;
                hotkey2 = HOTKEY.NONE;
                shortcut_letter = "";
                shortcut_key = ""; // Allow the user to be able to configure their shortchuts by dropdowns
                enable_custom_skin = false;
                skin_location = "Assets/MDF EDITOR/FRAMEWORK/Skin/DatabaseGUISkin.guiskin";
                variables = new List<DB_Variables>();
                tabs_per_row = 5;
                tabs = new List<DB_Tabs>();
                dropdown_bool_list = new List<bool>();
            }
        }
    } 
}