using MDF_EDITOR.DATABASE; // THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK
using UnityEngine;
using UnityEngine.Windows;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

//TODO: Add handling for Remove Items with ID/Name/Reference.
//TODO: Add item cloning and duplication. a constructor with a db reference passed in to generate a clone.
//TODO: Add Indexer handling for ID/NAME/Object to access the database like so. Database.Actor[0] or Database.Actor["Bob"] Or Database.Actor[actor]
//TODO: Add Template Code Generation using #tag# with a hashset or hashtable to reference values and lists to queue to generate.
//TODO: Add support for different save types. like Json/XML or other types of serialization.
//TODO: Add support for Variable Re-Ordering
//TODO: Add support for tab Re-ordering
//TODO: Add ID/NAME fields to be automatically handled and locked in to the generation. When tabs are in place set in the main Tab



namespace MDF_EDITOR
{
	/// <summary>
	/// This class handles the Creation/Deletion/Modification of the databases. Handles code generation and error checking databases.
	/// </summary>
	public static class MANAGE_DATABASE
	{
		/// <summary>
		/// This is the current version of MDF Database.
		/// </summary>
		public const string VERSION = "0.8.8.1";
		/// <summary>
		/// This is the current database that is being generated/changed/deleted
		/// </summary>
		public static DB_Database_INFO current_database;
		public static string full_database_name;
		public static string full_folder_path;
		public static string full_db_script_path;
		public static string full_db_editor_path;
		public static string database_script_script;
		public static string database_handler_script;
		public static string database_gui_script;
		public static string database_controller_script;

		/// <summary>
		/// This method handles initializing the variables for <see cref="current_database"/>, <see cref="full_folder_path"/>, <see cref="full_db_script_path"/>, <see cref="full_db_editor_path"/>, <see cref="database_handler_script"/>
		/// </summary>
		public static void Init(DB_Database_INFO _database_info)
		{
			current_database = _database_info;
			full_folder_path = "Assets/MDF EDITOR/GENERATED_DATABASES" + "/" + current_database.NAME;
			full_db_script_path = full_folder_path + "/Scripts/";
			full_db_editor_path = full_folder_path + "/Editor/";
			database_handler_script = current_database.NAME + "_DB_Handler";
		}

		/// <summary>
		/// This method handles the deletion of the <see cref="current_database"/> Checks to see if the file exists and removes the references and files.
		/// </summary>
		public static void DeleteDatabase(DB_Database_INFO _database_info)
		{
			Init(_database_info);
			string old_db_full_path = "Assets/MDF EDITOR/GENERATED_DATABASES" + "/" + current_database.SAVED_DATABASE_NAME;
			string asset_location = "Assets/Resources/" + current_database.database_location + "/" + current_database.NAME + "." + current_database.asset_type;
			string folder_metafile = "Assets/MDF EDITOR/GENERATED_DATABASES"+ "/" + current_database.NAME + ".meta";
			string asset_metafile = asset_location + "." + ".meta";
			//current_database = DATABASE_MANAGER_GUI.current_database;
			Debug.Log(folder_metafile);
			if (File.Exists(asset_location))
			{
				Debug.Log("File Exists: " + asset_location);
				FileUtil.DeleteFileOrDirectory(asset_location);
				FileUtil.DeleteFileOrDirectory(asset_metafile);
			}
			else
			{
				Debug.Log("File Does Not Exist: " + asset_location);
			}

			if (current_database.SAVED && Directory.Exists(old_db_full_path))
			{
				Debug.Log ("Deleting Old Folder! : " + old_db_full_path);
				FileUtil.DeleteFileOrDirectory(old_db_full_path);
			}

			if (!Directory.Exists(full_folder_path) || !current_database.SAVED || current_database.NAME == string.Empty)
			{
				Debug.Log ("Deleting Unsaved Database!");
			}
			
			if (current_database.SAVED && Directory.Exists(full_folder_path))
			{
				Debug.Log ("Deleting Folder! : " + full_folder_path);
				FileUtil.DeleteFileOrDirectory(full_folder_path);
				//Create_Script_Database_Class();
				//Create_MAIN_Database_GUI();
				//FileUtil.DeleteFileOrDirectory(folder_metafile);
				if (File.Exists(folder_metafile)) { FileUtil.DeleteFileOrDirectory(folder_metafile); }
				AssetDatabase.Refresh();
			}
			DATABASE_GUI.CLOSE();
			//DATABASE.DATABASE.DB_Info.Remove(current_database);

		}

		/// <summary>
		/// This handles the generation of databases. Also has conditional handling for modifying databases and will create all the database structures when needed.
		/// </summary>
		public static void Generate_All_Databases(DB_Database_INFO _database_info)
		{
			Init(_database_info);
			if (current_database == null)
			{
				for (int i = 0; i < DATABASE.DATABASE.DB_Info.COUNT; i++)
				{
					DB_Database_INFO _currrent_database = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i);
					string temp_name = _currrent_database.NAME;

					if (_currrent_database.SAVED_DATABASE_NAME != _currrent_database.NAME)
					{
						_currrent_database.NAME = _currrent_database.SAVED_DATABASE_NAME;
						DeleteDatabase(_currrent_database);
						_currrent_database.NAME = temp_name;
					}
					_currrent_database.SAVED_DATABASE_NAME = _currrent_database.NAME;
					Create_Database_Structure(_database_info);
					_currrent_database.SAVED = true;
				}
				Create_Script_Database_Class();
				Create_MAIN_Database_GUI();
				DATABASE_GUI.CLOSE();
				AssetDatabase.Refresh();
				return;
			}

			if (current_database.SAVED && current_database.SAVED_DATABASE_NAME == current_database.NAME)
			{
				Debug.Log("This is modifying, because the name is the same as the stored name, and is marked as saved.");
				Create_Database_Structure(_database_info);
			}
			
			if(current_database.SAVED && current_database.SAVED_DATABASE_NAME != current_database.NAME)
			{
				Debug.Log("This is modifying, but the names changed so it needs to do more!");
				for (int i = 0; i < DATABASE.DATABASE.DB_Info.COUNT; i++) 
				{
					DB_Database_INFO _current_database = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i);
					string temp_name = _current_database.NAME;
							
					if (_current_database.SAVED_DATABASE_NAME != _current_database.NAME)
					{
						_current_database.NAME = _current_database.SAVED_DATABASE_NAME;
						DeleteDatabase(_current_database);
						_current_database.NAME = temp_name;
					}
					_current_database.SAVED_DATABASE_NAME = _current_database.NAME;
						Create_Database_Structure(_database_info);

					_current_database.SAVED = true;
				}
						//Create_Script_Database_Class();
						//Create_MAIN_Database_GUI();
			}

			if (!current_database.SAVED)
			{
				for (int i = 0; i < DATABASE.DATABASE.DB_Info.COUNT; i++) 
				{
					DB_Database_INFO _currrent_database = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i);					
					string temp_name = _currrent_database.NAME;
					
					if (_currrent_database.SAVED_DATABASE_NAME != _currrent_database.NAME)
					{
						_currrent_database.NAME = _currrent_database.SAVED_DATABASE_NAME;
						DeleteDatabase(_currrent_database);
						_currrent_database.NAME = temp_name;
					}
					_currrent_database.SAVED_DATABASE_NAME = _currrent_database.NAME;
					Create_Database_Structure(_database_info);
					_currrent_database.SAVED = true;
				}

				//Create_Script_Database_Class();
				//Create_MAIN_Database_GUI();				
			}
			Create_Script_Database_Class();
			Create_MAIN_Database_GUI();
			DATABASE_GUI.CLOSE();
			AssetDatabase.Refresh();
		}
		
		/// <summary>
		/// This method first calls <see cref="Init"/> to grab the references and then validates to make sure the name is not empty and then calls the following generation scripts.
		/// <para><see cref="Create_Script_Database_List"/>, <see cref="Create_Script_Database_Object_Handler"/>, <see cref="Create_Script_Database_GUI"/></para>
		/// </summary>
		public static void Create_Database_Structure(DB_Database_INFO _database_info)
		{
			Init(_database_info);
			if (current_database.NAME != string.Empty && DATABASE.DATABASE.DB_Info.Find(current_database.NAME) != null)
			{
				Create_Script_Database_List();
				AssetDatabase.Refresh();

				Create_Script_Database_Object_Handler();
				AssetDatabase.Refresh();

				Create_Script_Database_GUI();
				AssetDatabase.Refresh();

				Create_Script_Database_List();
				AssetDatabase.Refresh();

			}
			else
			{
				Debug.Log("Error Locating current database: " + current_database.NAME);
			}
			
			//Create_Script_Database_List(); //BUG:Not sure if this needs to be called again sometimes....

		} 

		/// <summary>
		/// This method creates the database script file. This file holds all the variables created and creates both an optional and empty constructor.
		/// </summary>
		public static void Create_Script_Database_List()
		{
			string create_script_file_location = full_db_script_path + full_database_name + ".cs";
			if (current_database.enable_database_prefix && !current_database.enable_database_suffix) { full_database_name = current_database.database_prefix + current_database.NAME; }
			if (!current_database.enable_database_prefix && current_database.enable_database_suffix) { full_database_name = current_database.NAME + current_database.database_suffix; }
			if (current_database.enable_database_prefix && current_database.enable_database_suffix) { full_database_name = current_database.database_prefix +  current_database.NAME + current_database.database_suffix; }
			if (!current_database.enable_database_prefix && !current_database.enable_database_suffix) { full_database_name = current_database.NAME; }
			if (!Directory.Exists(full_db_script_path)) { Directory.CreateDirectory(full_db_script_path); Debug.Log("Creating Script Directory: \n" + create_script_file_location); }

			StreamWriter file = new StreamWriter(create_script_file_location);
			//This makes the header for the database
			file.Write(
				"using UnityEngine; \n" +
				"using System.Collections; \n" +
				"using System.Collections.Generic; \n \n" +
				"\n [System.Serializable] \n \n" +
				"\t public partial class " + full_database_name + "\n {\n \n" +
				"\t public int ID; \n" +
				"\t public string NAME; \n");
			
			//This iterates through the variables and will add the variables at the top of the class.
			for (int i = 0; i < current_database.variables.Count; i++)
			{
				
				if(current_database.variables[i].var_list_type == LIST_TYPE.NONE) { file.Write("\t public " + current_database.variables[i].var_type + " " + current_database.variables[i].var_name + "; \n"); }
				if(current_database.variables[i].var_list_type == LIST_TYPE.ARRAY) { file.Write("\t public " + current_database.variables[i].var_type + "[] " + current_database.variables[i].var_name + "; \n"); }
				if(current_database.variables[i].var_list_type == LIST_TYPE.LIST) { file.Write("\t public List<" + current_database.variables[i].var_type + "> " + current_database.variables[i].var_name + "; \n"); }

				//TODO: Add back in once Sorted Lists/Dictionaries/Sorted Dictionaries are added.
//				if(current_database.variables[i].var_list_type == LIST_TYPE.SORTED_LIST) { file.Write("\t public SortedList<" + current_database.variables[i].var_type  + ", " + current_database.variables[i].var_second_type + "> " + current_database.variables[i].var_name + "; \n"); }
//				if(current_database.variables[i].var_list_type == LIST_TYPE.DICTIONARY) { file.Write("\t public Dictionary<" + current_database.variables[i].var_type  + ", " + current_database.variables[i].var_second_type + "> " + current_database.variables[i].var_name + "; \n"); }
//				if(current_database.variables[i].var_list_type == LIST_TYPE.SORTED_DICTIONARY) { file.Write("\t public SortedDictionary<" + current_database.variables[i].var_type  + ", " + current_database.variables[i].var_second_type + "> " + current_database.variables[i].var_name + "; \n"); }

				if (i == current_database.variables.Count - 1)
				{
					file.Write("\n");
				}
			}
			//This writes the blank open constructor.
			file.Write(
				"\t public " + full_database_name + "() \n" 
				+"\t { \n\n" 
				//"\t\t ID = MDF_EDITOR.DATABASE.DATABASE." + current_database.NAME + ".COUNT;\n" +
				+"\t } \n" );

			//This writes a constructor overload that allows to pass in a database object to copy. Copy Constructor.
			file.Write("\t public " + full_database_name + "(" + full_database_name + " _object) \n"
					   + "\t { \n\n" +
					   "\t NAME = _object.NAME; \n" );

			for (int i = 0; i < current_database.variables.Count; i++)
			{
				{
					file.Write("\t " + current_database.variables[i].var_name + " = " + "_object." + current_database.variables[i].var_name + "; \n"); 
					//if (current_database.variables[i].var_name != "ID") { file.Write("\t " + current_database.variables[i].var_name + " = " + "_object." + current_database.variables[i].var_name + "; \n"); } //OLD
				}
			}
			file.Write("\t} \n\n ");

			//This writes a constructor overload that allows the variables to be passed in as optional values to be assigned.
			file.Write("\t public " + full_database_name + "( \n" +
					   "\t string _NAME = default(string)");

			if (current_database.variables.Count > 0) { file.Write(",\n"); }
			else { file.Write(")\n"); }
			
			for (int i = 0; i < current_database.variables.Count; i++)
			{
				string type_default = string.Empty;

				if (current_database.variables[i].var_list_type == LIST_TYPE.NONE)
				{
					type_default = "default(" + current_database.variables[i].var_type + ")";
					if (i == current_database.variables.Count - 1) { file.Write("\t " + current_database.variables[i].var_type + " _" + current_database.variables[i].var_name + " = " + type_default + ") \n"); }
					else { file.Write("\t " + current_database.variables[i].var_type + " _" + current_database.variables[i].var_name + " = " + type_default + ", \n"); }
				}
				
				if(current_database.variables[i].var_list_type == LIST_TYPE.ARRAY)//TODO: ADD HANDLING FOR DEFAULT TYPES FOR ARRAY
				{
					type_default = "default(" + current_database.variables[i].var_type + "[])";
					if (i == current_database.variables.Count - 1) { file.Write("\t " + current_database.variables[i].var_type + "[] _" + current_database.variables[i].var_name + " = " + type_default + ") \n"); }
					else { file.Write("\t " + current_database.variables[i].var_type + "[] _" + current_database.variables[i].var_name + " = " + type_default + ", \n"); }
				}
				
				if(current_database.variables[i].var_list_type == LIST_TYPE.LIST)//TODO: ADD HANDLING FOR DEFAULT TYPES FOR LIST
				{
					type_default = "default(List<" + current_database.variables[i].var_type + ">)";

					if (i == current_database.variables.Count - 1) { file.Write("\t List<" + current_database.variables[i].var_type + "> _" + current_database.variables[i].var_name + " = " + type_default + ") \n"); }
					else { file.Write("\t List<" + current_database.variables[i].var_type + "> _" + current_database.variables[i].var_name + " = " + type_default + ", \n"); }
				}
				
//				if(current_database.variables[i].var_list_type == LIST_TYPE.SORTED_LIST) //TODO: ADD SUPPORTED SORTED_LIST TYPE
//				{
//					if (i == current_database.variables.Count - 1)
//					{
//						file.Write("\t SortedList<" + current_database.variables[i].var_type + ", " + current_database.variables[i].var_second_type + "> _" + current_database.variables[i].var_name + ") \n");
//					}
//					else
//					{
//						file.Write("\t SortedList<" + current_database.variables[i].var_type + ", " + current_database.variables[i].var_second_type + "> _" + current_database.variables[i].var_name + ", \n");
//					}
//				}
//				
//				if(current_database.variables[i].var_list_type == LIST_TYPE.DICTIONARY)//TODO: ADD SUPPORTED DICTIONARY TYPE
//				{
//					if (i == current_database.variables.Count - 1)
//					{
//						file.Write("\t Dictionary<" + current_database.variables[i].var_type + ", " + current_database.variables[i].var_second_type + "> _" + current_database.variables[i].var_name + ") \n");
//					}
//					else
//					{
//						file.Write("\t Dictionary<" + current_database.variables[i].var_type + ", " + current_database.variables[i].var_second_type + "> _" + current_database.variables[i].var_name + ", \n");
//					}
//				}
//				
//				if(current_database.variables[i].var_list_type == LIST_TYPE.SORTED_DICTIONARY) //TODO: ADD SUPPORTED SORTED_DICTIONARY TYPE
//				{
//					if (i == current_database.variables.Count - 1)
//					{
//						file.Write("\t SortedDictionary<" + current_database.variables[i].var_type + ", " + current_database.variables[i].var_second_type + "> _" + current_database.variables[i].var_name + ") \n");
//					}
//					else
//					{
//						file.Write("\t SortedDictionary<" + current_database.variables[i].var_type + ", " + current_database.variables[i].var_second_type + "> _" + current_database.variables[i].var_name + ", \n");
//					}
//				}
				
				
			}
			
			file.Write("\t { \n \n");
			
			
			for (int i = 0; i < current_database.variables.Count; i++) 
			{
				{
					file.Write("\t " + current_database.variables[i].var_name + " = " + "_" + current_database.variables[i].var_name + "; \n");
					//if (current_database.variables[i].var_name != "ID") { file.Write("\t " + current_database.variables[i].var_name + " = " + "_" + current_database.variables[i].var_name + "; \n"); }
				}
			}
			file.Write("\t} \n\n ");
			file.Write(
				"\t public " + full_database_name + " GetCopy() { return new "+ full_database_name + "(this); }\n" 
				+ "}");
			file.Close();

		}

		/// <summary>
		/// This method creates the DB_Handler class for the database this class is used for accessing methods like Add/Remove/Find etc for the database objects within the created database class. 
		/// </summary>
		public static void Create_Script_Database_Object_Handler()
		{
			Debug.Log("FULL DB PATH FOR OBJECT: " + full_db_script_path);

			if (Directory.Exists(full_db_script_path) != true)
			{
				Directory.CreateDirectory(full_db_script_path);
				Debug.Log("Creating Script Directory!");
			}


			string database_handler_script_file = full_db_script_path + database_handler_script + ".cs";
			StreamWriter file = new StreamWriter(database_handler_script_file);
			//This makes the header for the database
			file.Write(
				"using UnityEngine; \n"
				+ "using System.Collections; \n"
				+ "using System.Collections.Generic; \n"
				+ "using System.Linq; \n\n"

				+ "\t public partial class " + current_database.NAME + "_DB_Handler : ScriptableObject, IEnumerable<" + full_database_name + "> "
				+ "\n{ \n \n"
				+ "\t public const string ASSET_NAME = " + current_database.NAME.AddQuotes() + "; // Physical File name of the Database \n"
				+ "\t public const string ASSET_TYPE = " + ".asset".AddQuotes() + "; // Type of Database \n"
				+ "\t public const string ASSET_PATH = " + current_database.database_location.AddQuotes() + "; // Location of the Database \n"
				+ "\t public const string DATABASE_PATH = " + "Assets/Resources/".AddQuotes() + " + ASSET_PATH" + " + " + "/".AddQuotes() + " + ASSET_NAME + ASSET_TYPE; \n \n"
				+ "\t [SerializeField] \n" + "\t private List<" + full_database_name + "> " + current_database.NAME.ToLower() + "_database; \n \n"

				+ "\t void OnEnable() \n" + "\t { \n" + "\t\t if ("
				+ current_database.NAME.ToLower() + "_database == null) \n" + "\t\t { \n" + "\t\t\t" + current_database.NAME.ToLower()
				+ "_database = new List<" + full_database_name + "> (); \n" + "\t \t } \n" + "\t } \n \n"

				+ "\t public " + full_database_name + " this" + "[int _ID] \n"
				+ "\t { \n"
				+ "\t\t get \n"
				+ "\t\t { \n"
				+ "\t\t\t return Find(_ID);\n"
				+ "\t\t } \n"
				+ "\t } \n \n"

				+ "\t public " + full_database_name + " this" + "[string _NAME] \n"
				+ "\t { \n"
				+ "\t\t get \n"
				+ "\t\t { \n"
				+ "\t\t\t return Find(_NAME);\n"
				+ "\t\t } \n"
				+ "\t } \n \n"

				+ "\t public " + full_database_name + " this" + "[" + full_database_name + " _DB] \n"
				+ "\t { \n"
				+ "\t\t get \n"
				+ "\t\t { \n"
				+ "\t\t\t return Find(_DB.ID);\n"
				+ "\t\t } \n"
				+ "\t } \n \n"

				+ "\t public void ADD(" + full_database_name + " _database) \n"
				+ "\t { \n"
				+ "\t\t " + "_database.ID = COUNT; \n \t\t " + current_database.NAME.ToLower() + "_database" + ".Add(_database); \n"
				+ "\t } \n \n"

				+ "\t public void ADD_AT( int _index, " + full_database_name + " _database) \n"
				+ "\t { \n"
				+ "\t\t" + "_database.ID = COUNT; "
				+ "\n \t\t" + current_database.NAME.ToLower() + "_database" + ".Insert(_index, _database); \n"
				+ "\t } \n \n"

				+ "\t public void REMOVE(" + full_database_name + " _database) \n"
				+ "\t { \n"
				+ "\t\t " + current_database.NAME.ToLower() + "_database" + ".Remove(_database); \n"
				+ "\t } \n \n"

				+ "\t public void REMOVE(string _name) \n"
				+ "\t { \n"
				+ "\t\tfor (int i = 0; i < " + current_database.NAME.ToLower() + "_database.Count;" + " i++)\n"
				+ "\t\t\tif (" + current_database.NAME.ToLower() + "_database[i].NAME == _name)\n"
				+ "\t\t\t{\n"
				+ "\t\t\t\t" + current_database.NAME.ToLower() + "_database.RemoveAt(i);\n"
				+ "\t\t\t\tbreak;\n"
				+ "\t\t\t}\n"
				+ "\t } \n \n"

				+ "\t public void REMOVE(int _id) \n"
				+ "\t { \n"
				+ "\t\tfor (int i = 0; i < " + current_database.NAME.ToLower() + "_database.Count;" + " i++)\n"
				+ "\t\t\tif (" + current_database.NAME.ToLower() + "_database[i].ID == _id)\n"
				+ "\t\t\t{\n"
				+ "\t\t\t\t" + current_database.NAME.ToLower() + "_database.RemoveAt(i);\n"
				+ "\t\t\t\tbreak;\n"
				+ "\t\t\t}\n"
				+ "\t } \n \n"

				+ "\t public void REMOVE_AT( int _index) \n"
				+ "\t { \n"
				+ "\t\t " + current_database.NAME.ToLower() + "_database" + ".RemoveAt(_index); \n"
				+ "\t } \n \n"

				+ "\t public bool MoveUp(int _index) \n"
				+ "\t { \n"
				+ "\t\tif (_index <= 0 || _index >= COUNT)\n\t\t\treturn false;\n\n"
				+ "\t\t" + full_database_name + " temp = " + current_database.NAME.ToLower() + "_database[_index - 1];\n"
				+ "\t\t" + current_database.NAME.ToLower() + "_database[_index - 1] = " + current_database.NAME.ToLower() + "_database[_index];\n"
				+ "\t\t" + current_database.NAME.ToLower() + "_database[_index] = temp;\n"
				+ "\t\treturn true;\n"
				+ "\t } \n \n"

				+ "\t public bool MoveDown(int _index) \n"
				+ "\t { \n"
				+ "\t\tif (_index == COUNT - 1 || _index < 0 || _index >= COUNT)\n\t\t\treturn false;\n\n"
				+ "\t\t" + full_database_name + " temp = " + current_database.NAME.ToLower() + "_database[_index + 1];\n"
				+ "\t\t" + current_database.NAME.ToLower() + "_database[_index + 1] = " + current_database.NAME.ToLower() + "_database[_index];\n"
				+ "\t\t" + current_database.NAME.ToLower() + "_database[_index] = temp;\n"
				+ "\t\treturn true;\n"
				+ "\t } \n \n"

				+ "\t public int COUNT \n"
				+ "\t { \n"
				+ "\t\t get { return " + current_database.NAME.ToLower() + "_database.Count;} \n"
				+ "\t } \n \n"

				+ "\t public " + full_database_name + " Find (string _name) \n"
				+ "\t { \n"
				+ "\t\tfor (int i = 0; i < " + current_database.NAME.ToLower() + "_database.Count;" + " i++)\n"
				+ "\t\t\tif (" + current_database.NAME.ToLower() + "_database[i].NAME == _name)\n"
				+ "\t\t\t\t return " + current_database.NAME.ToLower() + "_database[i];\n\n"
				+ "\t\tDebug.LogError(\"The name: \" + _name + \" was not found in the \" + ASSET_NAME + \" database.\");\n"
				+ "\t\treturn null;\n"
				+ "\t } \n \n"

				+ "\t public " + full_database_name + " Find (int _id) \n"
				+ "\t { \n"
				+ "\t\tif (_id >= 0 && _id < COUNT)\n"
				+ "\t\t\treturn " + current_database.NAME.ToLower() + "_database[_id];\n\n"
				+ "\t\tDebug.LogError(\"The ID: \" + _id + \" was not found in the \" + ASSET_NAME + \" database.\");\n"
				+ "\t\treturn null;\n"
				+ "\t } \n \n"

				+ "\t public void SortAlphabetically() \n"
				+ "\t { \n"
				+ "\t\t " + current_database.NAME.ToLower() + "_database" + ".Sort((x,y) => string.Compare(x.NAME, y.NAME)); \n"
				+ "\t } \n \n"

				+ "\t public void SortAlphabeticallyReverse() \n"
				+ "\t { \n"
				+ "\t \t " + current_database.NAME.ToLower() + "_database" + ".Sort((x,y) => string.Compare(y.NAME, x.NAME)); \n"
				+ "\t } \n \n"

				+ "\t public IEnumerator<" + full_database_name + ">" + "GetEnumerator() \n"
				+ "\t { \n"
				+ "\t\t return " + current_database.NAME.ToLower() + "_database" + ".GetEnumerator(); \n"
				+ "\t } \n \n"

				+ "\t IEnumerator IEnumerable.GetEnumerator() \n"
				+ "\t { \n"
				+ "\t\t return " + current_database.NAME.ToLower() + "_database" + ".GetEnumerator(); \n"
				+ "\t } \n \n"

				+ "\n }");
			file.Close();
		}

		/// <summary>
		/// This method will create the GUI for the database that is created and will accordingly add tabs and variables and fields that represent the types the require.
		/// </summary>
		public static void Create_Script_Database_GUI()
		{
			int varcount = -1;
			Debug.Log("FULL DB PATH FOR OBJECT: " + full_db_editor_path);
			if (Directory.Exists(full_db_editor_path) != true)
			{
				Directory.CreateDirectory(full_db_editor_path);
				Debug.Log("Creating Editor Directory!");
			}

			database_gui_script = full_database_name + "_GUI";

			string database_gui_script_file = full_db_editor_path + database_gui_script + ".cs";
			StreamWriter file = new StreamWriter(database_gui_script_file);
			//write header
			file.Write("using MDF_EDITOR.DATABASE; // THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK \n" + "using UnityEngine; \n" + "using UnityEditor; \n" + "using System.Collections; \n" + "using System.Collections.Generic; \n \n" + "namespace MDF_EDITOR \n" + "{ \n" + "\t namespace DATABASE \n" + "\t { \n" + "\t\t public class " + database_gui_script + ": EditorWindow \n" + "\t\t { \n");

			//this adds the variable current_tab if tabs are enabled
			if (current_database.enable_tabs == true)
			{
				file.Write("\t\t\t private string current_tab; // This contains a string that stores the name of the tab, this is used to navigate the GUI \n");
			}

			if (current_database.dropdown_bool_list.Count > 0)
			{
//				for (int i = 0; i < current_database.dropdown_bool_list.Count; i++)
//				{
				if (current_database.enable_tabs == true)
				{
					foreach (var tab in current_database.tabs)
					{
						foreach (var variable in tab.tab_variables)
						{
							if (variable.var_list_type == LIST_TYPE.LIST || variable.var_list_type == LIST_TYPE.ARRAY)
							{
								file.Write("\t\t\t private Vector2 " + variable.var_name + "_List_Index; \n");
								Debug.Log("Variable Bool Header Added");
							}
						}
					}
				}
				if (current_database.enable_tabs == false)
				{
					foreach (var variable in current_database.variables)
					{
						if (variable.var_list_type == LIST_TYPE.LIST || variable.var_list_type == LIST_TYPE.ARRAY)
						{
							file.Write("\t\t\t private Vector2 " + variable.var_name + "_List_Index; \n");
							Debug.Log("Variable Bool Header Added");
						}
					}
				}

//				}

				file.Write("\t\t\t private List<bool> foldout_expanded = new List<bool>{");

				for (int i = 0; i < current_database.dropdown_bool_list.Count; i++)
				{
					file.Write("false");
					Debug.Log("bool init value added");


					if (i != current_database.dropdown_bool_list.Count - 1)
					{
						file.Write(",");
						Debug.Log("bool init , added");
					}
				}
				file.Write("}; \n");
			}

			//this writes some of the opening variables
			file.Write(
				"\t\t\t private int current_index; \n" 
				+ "\t\t\t private static " + full_database_name + " current_" + current_database.NAME + "; \n" 
				+ "\t\t\t private static "+ database_gui_script + " _" + database_gui_script + "; \n" 
				+ "\t\t\t private const string menu_shortcut = "+ current_database.shortcut_key.AddQuotes() + "; \n" 
				+ "\t\t\t private const string menu_name = " + "MDF EDITOR/".AddQuotes() + " + "+ current_database.show_in_menu_name.AddQuotes() + " + " + " ".AddQuotes() + " + menu_shortcut; \n" 
				+ "\t\t\t private const bool show_menu  = " + current_database.show_in_menu.ToString().ToLower() + "; \n" 
				+ "\t\t\t private static string custom_skin_path = " + current_database.skin_location.AddQuotes() + "; \n" 
				+ "\t\t\t private static GUISkin custom_skin; \n" 
				+ "\t\t\t private Vector2 database_scroll_position; \n" 
				+ "\t\t\t private int selected_id; \n" 
				+ "\t\t\t private State state; \n" 
				+ "\t\t\t private enum State {BLANK, EDIT} \n \n" 

				+ "\t\t\t [MenuItem(menu_name, !show_menu)] \n" 
				+ "\t\t\t public static void Init() \n" 
				+ "\t\t\t { \n" 
				+ "\t\t\t\t " + " _" + database_gui_script + " = GetWindow<" + database_gui_script + ">(); \n" 
				+ "\t\t\t\t " + " _" + database_gui_script + ".minSize = new Vector2(1100, 400); \n" + "\t\t\t\t " + " _" + database_gui_script + ".Show(); \n" 
				+ "\t\t\t } \n \n" 

				+ "\t\t\t public void OnGUI() \n" 
				+ "\t\t\t { \n"
				+"\t\t\t\t if( custom_skin == null ){ custom_skin = (GUISkin)(AssetDatabase.LoadAssetAtPath(custom_skin_path, typeof(GUISkin))); } GUI.skin = custom_skin; \n" 
				+ "\t\t\t\t EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)); \n" 
				+ "\t\t\t\t DisplayListArea(); \n" 
				+ "\t\t\t\t DisplayMainArea(); \n" 
				+ "\t\t\t\t EditorGUILayout.EndHorizontal(); \n" 
				+ "\t\t\t } \n \n" 

				+ "\t\t\t private void DisplayListArea() \n" 
				+ "\t\t\t { \n" 
				+ "\t\t\t\t EditorGUILayout.BeginVertical(GUILayout.Width(400)); \n" 
				+ "\t\t\t\t EditorGUILayout.Space(); \n" 
				+ "\t\t\t\t database_scroll_position = EditorGUILayout.BeginScrollView(database_scroll_position," + "box".AddQuotes() + ",GUILayout.ExpandHeight(true)); \n" 
				+ "\t\t\t\t GUILayout.BeginHorizontal(GUILayout.MaxWidth(250)); \n" 
				+ "\t\t\t\t EditorStyles.toolbar.alignment = TextAnchor.MiddleLeft; \n" 
				+ "\t\t\t\t GUILayout.Label(" + "Delete".AddQuotes() + ",EditorStyles.toolbar,GUILayout.Width(45)); \n" 
				+ "\t\t\t\t EditorStyles.toolbar.alignment = TextAnchor.MiddleCenter; \n" 
				+ "\t\t\t\t GUILayout.Label(" + "ID".AddQuotes() + ",EditorStyles.toolbar,GUILayout.Width(25)); \n" 
				+ "\t\t\t\t GUILayout.Label(" + current_database.NAME.AddQuotes() + " + " + " Name".AddQuotes() + ",EditorStyles.toolbar,GUILayout.Width(322)); \n" 
				+ "\t\t\t\t GUILayout.EndHorizontal(); \n \n" 
				+ "\t\t\t\t Populate_List_Area(); // This populates the database fields to appear in the list \n \n" 
				+ "\t\t\t\t EditorGUILayout.EndScrollView(); \n" 
				+ "\t\t\t\t EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)); \n" 
				+ "\t\t\t\t EditorGUILayout.LabelField(" + "Total: ".AddQuotes() + " + " + ": ".AddQuotes() + " + " + current_database.database_class + "." + current_database.NAME + ".COUNT, GUILayout.Width(100)); \n \n" 
				+ "\t\t\t\t if (GUILayout.Button(" + "New ".AddQuotes() + " + " + current_database.NAME.AddQuotes() + ")) \n" 
				+ "\t\t\t\t { \n"
				+ "\t\t\t\t\t GUI.FocusControl(" + "Clear".AddQuotes() + "); \n" 
				+ "\t\t\t\t\t " + current_database.database_class + "." + current_database.NAME + ".ADD(new " + full_database_name + "(");

			//this will fill all the parameters for adding the new database
			file.Write("".AddQuotes()); //For Name Field

			if(current_database.variables.Count > 0) { file.Write(",");}
			for (int i = 0; i < current_database.variables.Count; i++)
			{
				if (current_database.variables[i].var_list_type == LIST_TYPE.NONE)
				{
					if (current_database.variables[i].selected_variable_type == SELECTED_TYPE.VALUE)
					{
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.STRING)
						{
							file.Write("".AddQuotes());
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.BOOL)
						{
							file.Write("false");
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.COLOR)
						{
							file.Write("new Color(0,0,0)");
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.COLOR32)
						{
							file.Write("new Color32(0,0,0,0)");
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.DOUBLE)
						{
							file.Write("0");
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.FLOAT)
						{
							file.Write("0");
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.INT)
						{
							file.Write("0");
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.LONG)
						{
							file.Write("0");
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.RECT)
						{
							file.Write("new Rect(0,0,0,0)");
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.VECTOR2)
						{
							file.Write("new Vector2(0,0)");
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.VECTOR3)
						{
							file.Write("new Vector3(0,0,0)");
						}
						if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.VECTOR4)
						{
							file.Write("new Vector4(0,0,0,0)");
						}

							//BELOW are Data Types that are currently disabled until we find a easy solutin to handle them

							//					if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.LONG){file.Write("0");}
							//					if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.QUATERNION){file.Write("new Quaternion(0,0,0,0)");}
							//					if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.RAY){file.Write("new Ray(new Vector3(0,0,0), new Vector3(0,0,0))");}
							//					if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.RAY2D){file.Write("new Ray2D(new Vector2(0,0), new Vector2(0,0))");}
							//					if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.SHORT){file.Write("0");}
							//					if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.BYTE){file.Write("0");}
							//					if (current_database.variables[i].variable_value_type == VALUE_DATATYPE.CHAR){file.Write("''");}
					  }

					if (current_database.variables[i].selected_variable_type == SELECTED_TYPE.REFERENCE)
					{
						file.Write("null");
					}
				}

				if (current_database.variables[i].var_list_type == LIST_TYPE.ARRAY)
				{
					file.Write("new " + current_database.variables[i].var_type + "[0]");
				}

				if (current_database.variables[i].var_list_type == LIST_TYPE.LIST)
				{
					file.Write("new List<" + current_database.variables[i].var_type + ">()");
				}

				if (i != current_database.variables.Count - 1)
				{
					file.Write(",");

				}
			}

			file.Write(
				")); \n" 
				+ "\t\t\t\t current_index = " + current_database.database_class + "." + current_database.NAME + ".COUNT - 1; \n" +
				"\t\t\t\t selected_id = current_index;\n" 
				+ "\t\t\t\t " + "current_" + current_database.NAME + " = " + current_database.database_class + "." + current_database.NAME + "[current_index]; \n" 
				+ "\t\t\t\t state = State.EDIT; \n" + "\t\t\t } \n \n" + "\t\t\t EditorGUILayout.EndHorizontal(); \n" 
				+ "\t\t\t EditorGUILayout.Space(); \n" 
				+ "\t\t\t EditorGUILayout.EndVertical(); \n" 
				+ "\t\t\t } ////END DisplayListArea METHOD \n \n");

			if (current_database.enable_tabs == true)
			{
				file.Write("\t\t\t private void DisplayDBTabArea() \n" 
					+ "\t\t\t { \n" 
					+ "\t\t\t\t EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(false)); \n");

				// This creates the tabs for the database
				for (int i = 0; i < current_database.tabs.Count; i++)
				{
					file.Write("\t\t\t\t if (GUILayout.Toggle(" + "current_tab == " + current_database.tabs[i].tab_name.AddQuotes() + "," + current_database.tabs[i].tab_name.AddQuotes() + ", " + "button".AddQuotes() + ")){ current_tab = " + current_database.tabs[i].tab_name.AddQuotes() + ";} \n");
				}

				file.Write("\t\t\t\t EditorGUILayout.EndHorizontal(); \n" 
					+ "\t\t\t\t EditorGUILayout.Space(); \n" 
					+ "\t\t\t } \n \n ");
			}


			file.Write(
				"\t\t\t private void Populate_List_Area() \n" 
				+ "\t\t\t { \n" 
				+ "\t\t\t\t if (" + current_database.database_class + "." + current_database.NAME + ".COUNT > 0) \n" 
				+ "\t\t\t\t { \n" 
				+ "\t\t\t\t\t for (current_index = 0; current_index < " + current_database.database_class  + "." + current_database.NAME + ".COUNT; current_index++) \n" 
				+ "\t\t\t\t\t { \n"
				
				+ "\t\t\t\t\t\tEditorGUILayout.BeginHorizontal(); \n" 
				+ "\t\t\t\t\t\tif (GUILayout.Button(new GUIContent(\"\u25b2\", \"Moves this entry up\"), EditorStyles.miniButtonLeft, GUILayout.Width(35)))\n"
				+ "\t\t\t\t\t\t\tif (" + current_database.database_class + "." + current_database.NAME + ".MoveUp(current_index))\n"
				+ "\t\t\t\t\t\t\t{\n"
				+ "\t\t\t\t\t\t\t\tselected_id = selected_id == current_index ? selected_id - 1 : selected_id;\n" 
				+ "\t\t\t\t\t\t\t\tcurrent_index--;\n"
				+ "\t\t\t\t\t\t\t}\n"

				+ "\n\t\t\t\t\t\tif (GUILayout.Button(new GUIContent(\"\u25bc\", \"Moves this entry down\"), EditorStyles.miniButtonRight, GUILayout.Width(35)))\n"
				+ "\t\t\t\t\t\t\tif (" + current_database.database_class + "." + current_database.NAME + ".MoveDown(current_index))\n"
				+ "\t\t\t\t\t\t\t{\n"
				+ "\t\t\t\t\t\t\t\tselected_id = selected_id == current_index ? selected_id + 1 : selected_id;\n"
				+ "\t\t\t\t\t\t\t\tcurrent_index++;\n"
				+ "\t\t\t\t\t\t\t}\n\n"

				+ "\t\t\t\t\t\t " +"if(" + current_database.database_class + "." + current_database.NAME + "[current_index] == null )\n" 
				+" \t\t\t\t\t\t\t continue;\n\n"
				+ "\t\t\t\t\t\t " + current_database.database_class + "." + current_database.NAME + "[current_index].ID = current_index; \n"
				+ "\t\t\t\t\t\t if (GUILayout.Button(" + "DEL".AddQuotes() + ", GUILayout.Width (35))) \n" 
				+ "\t\t\t\t\t\t { \n" 
				+ "\t\t\t\t\t\t\t if (DATABASE.Options.DB_Options().SHOW_DELETE_POPUP == true) \n" 
				+ "\t\t\t\t\t\t\t { \n" 
				+ "\t\t\t\t\t\t\t\t if (EditorUtility.DisplayDialog(" + "Remove ".AddQuotes() + " + " + current_database.database_class + "." + current_database.NAME + "[current_index].NAME + " + "?".AddQuotes() + "," + "This will remove ".AddQuotes() + " + " + current_database.database_class + "." + current_database.NAME + "[current_index].NAME + " + " from the DATABASE".AddQuotes() + "," + "Delete".AddQuotes() + "," + "Keep".AddQuotes() 
				+ ")) \n" + "\t\t\t\t\t\t\t\t { \n" + "\t\t\t\t\t\t\t\t\t GUI.FocusControl(" + "Clear".AddQuotes() + "); \n" + "\t\t\t\t\t\t\t\t\t " + current_database.database_class + "." + current_database.NAME + ".REMOVE_AT(current_index); \n" 
				+ "\t\t\t\t\t\t\t\t\t state = State.BLANK; \n" 
				+ "\t\t\t\t\t\t\t\t\t return; \n" 
				+ "\t\t\t\t\t\t\t\t } \n" 
				+ "\t\t\t\t\t\t\t } \n \n" 
				+ "\t\t\t\t\t\t\t if (DATABASE.Options.DB_Options().SHOW_DELETE_POPUP == false) \n" 
				+ "\t\t\t\t\t\t\t { \n" + "\t\t\t\t\t\t\t\t GUI.FocusControl(" + "Clear".AddQuotes() + "); \n" 
				+ "\t\t\t\t\t\t\t\t "  + current_database.database_class + "." + current_database.NAME + ".REMOVE_AT(current_index); \n" 
				+ "\t\t\t\t\t\t\t\t state = State.BLANK; \n" 
				+ "\t\t\t\t\t\t\t\t return; \n" 
				+ "\t\t\t\t\t\t\t } \n" 
				+ "\t\t\t\t\t\t } \n \n" 
				+ "\t\t\t\t\t\t GUILayout.Label(" + current_database.database_class + "." + current_database.NAME + "[current_index].ID.ToString()," + "box".AddQuotes() + ",GUILayout.Width(37)); \n" 
				+ "\t\t\t\t\t\t if (GUILayout.Toggle(" + "selected_id == current_index," + current_database.database_class + "." + current_database.NAME + "[current_index].NAME," + "box".AddQuotes() + ",GUILayout.ExpandWidth(true))) \n" 
				+ "\t\t\t\t\t\t { \n" 
				+ "\t\t\t\t\t\t\t if (selected_id != current_index) \n" 
				+ "\t\t\t\t\t\t\t { \n" 
				+ "\t\t\t\t\t\t\t\t GUI.FocusControl(" + "Clear".AddQuotes() + "); \n");

			if (current_database.enable_tabs == true)
			{
				file.Write("\t\t\t\t\t\t\t\t current_tab = " + "".AddQuotes() + "; \n");
			}


			file.Write(
				"\t\t\t\t\t\t\t } \n"
				+ "\t\t\t\t\t\t\t selected_id = current_index; \n" 
				+ "\t\t\t\t\t\t\t current_" + current_database.NAME + " = " + current_database.database_class + "." + current_database.NAME + "[current_index]; \n" 
				+ "\t\t\t\t\t\t\t state = State.EDIT; \n" 
				+ "\t\t\t\t\t\t } \n" 
				+ "\t\t\t\t\t\t EditorGUILayout.EndHorizontal(); \n" 
				+ "\t\t\t\t\t } \n \n" 
				+ "\t\t\t\t } \n \n" 
				+ "\t\t\t } \n \n");

			file.Write(
				"\t\t\t private void DisplayMainArea() \n" 
				+ "\t\t\t { \n" 
				+ "\t\t\t\t EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true)); \n" 
				+ "\t\t\t\t EditorGUILayout.Space(); \n" 
				+ "\t\t\t\t switch(state) \n" 
				+ "\t\t\t\t { \n" 
				+ "\t\t\t\t\t case State.EDIT: \n");

			if (current_database.enable_tabs == true)
			{
				file.Write("\t\t\t\t\t\t DisplayDBTabArea(); \n" + "\t\t\t\t\t\t switch (current_tab) \n" + "\t\t\t\t\t\t { \n");

				for (int i = 0; i < current_database.tabs.Count; i++)
				{
					file.Write("\t\t\t\t\t\t\t case " + current_database.tabs[i].tab_name.AddQuotes() + ": " + current_database.tabs[i].tab_name.NoSpecialCharacters() + "Tab(); break; \n");
				}

				file.Write("\t\t\t\t\t\t\t default: " + "current_tab = " + current_database.tabs[0].tab_name.AddQuotes() + "; " + current_database.tabs[0].tab_name.NoSpecialCharacters() + "Tab(); break; \n " + "\t\t\t\t\t\t } \n");
			}
			if (current_database.enable_tabs == false)
			{
				file.Write("\t\t\t\t\t\t MainArea(); \n");
			}

			file.Write("\t\t\t\t\t\t\t break; \n" 
				+ "\t\t\t\t } \n" 
				+ "\t\t\t\t EditorGUILayout.Space(); \n" 
				+ "\t\t\t\t EditorGUILayout.EndVertical(); \n" 
				+ "\t\t\t } \n \n");

			if (current_database.enable_tabs == true)
			{
				for (int i = 0; i < current_database.tabs.Count; i++)
				{
					file.Write("\t\t\t private void " + current_database.tabs[i].tab_name.NoSpecialCharacters() + "Tab() \n" + "\t\t\t { \n");
					if (i == 0)
					{
						file.Write("\t\t\t\t GUILayout.Label(" + " ID:".AddQuotes() +" + current_" + current_database.NAME +".ID" + " ,GUILayout.Width(50)); \n");
						file.Write("\t\t\t\t " + "current_" + current_database.NAME + ".NAME = " + "EditorGUILayout.TextField(" + "NAME: ".AddQuotes() + ", current_" + current_database.NAME + ".NAME" + "); \n");
					}

					for (int v = 0; v < current_database.tabs[i].tab_variables.Count; v++)
					{
						if (current_database.tabs[i].tab_variables[v].var_list_type == LIST_TYPE.NONE)
						{
							file.Write("\t\t\t\t " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + " = ");

							if (current_database.tabs[i].tab_variables[v].selected_variable_type == SELECTED_TYPE.VALUE)
							{
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.STRING)
								{
									file.Write("EditorGUILayout.TextField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.BOOL)
								{
									file.Write("EditorGUILayout.Toggle(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.INT)
								{
									file.Write("EditorGUILayout.IntField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.FLOAT)
								{
									file.Write("EditorGUILayout.FloatField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.COLOR)
								{
									file.Write("EditorGUILayout.ColorField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.COLOR32)
								{
									file.Write("EditorGUILayout.ColorField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR2)
								{
									file.Write("EditorGUILayout.Vector2Field(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR3)
								{
									file.Write("EditorGUILayout.Vector3Field(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR4)
								{
									file.Write("EditorGUILayout.Vector4Field(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.RECT)
								{
									file.Write("EditorGUILayout.RectField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.DOUBLE)
								{
									file.Write("EditorGUILayout.DoubleField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								}
								//							if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.STRING)
								//							{
								//								file.Write("EditorGUILayout.TextField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() +", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								//							}
								//							if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.STRING)
								//							{
								//								file.Write("EditorGUILayout.TextField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() +", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								//							}
								//							if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.STRING)
								//							{
								//								file.Write("EditorGUILayout.TextField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() +", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								//							}
								//							if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.STRING)
								//							{
								//								file.Write("EditorGUILayout.TextField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() +", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "); \n");
								//							}
							}
							if (current_database.tabs[i].tab_variables[v].selected_variable_type == SELECTED_TYPE.REFERENCE)
							{
								file.Write("(" + current_database.tabs[i].tab_variables[v].var_type + ") EditorGUILayout.ObjectField(" + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + ", typeof(" + current_database.tabs[i].tab_variables[v].var_type + ") , true" + "); \n");
							}
						}

						if (current_database.tabs[i].tab_variables[v].var_list_type == LIST_TYPE.ARRAY || current_database.tabs[i].tab_variables[v].var_list_type == LIST_TYPE.LIST)
						{
//							int varcount = -1;
							Debug.Log("Current Varcount: " + varcount);
							varcount += 1;

//							for (int b = 0; b < current_database.dropdown_bool_list.Count; b++)
//							{
							Debug.Log(varcount);
							file.Write(
								"\t\t\t\t foldout_expanded[" + varcount + "] = EditorGUILayout.Foldout(foldout_expanded[" + varcount + "]," + current_database.tabs[i].tab_variables[v].var_name.AddQuotes() + "); \n" 
								+ "\t\t\t\t if (foldout_expanded[" + varcount + "] == true) \n" 
								+ "\t\t\t\t { \n" 
								+ "\t\t\t\t\t EditorGUILayout.BeginVertical(); \n" 
								+ "\t\t\t\t\t GUILayout.BeginHorizontal(); \n" 
								+ "\t\t\t\t\t EditorStyles.toolbar.alignment = TextAnchor.MiddleLeft; \n" 
								+ "\t\t\t\t\t GUILayout.Label(" + "Delete".AddQuotes() + ",EditorStyles.toolbar,GUILayout.Width(45)); \n" 
								+ "\t\t\t\t\t EditorStyles.toolbar.alignment = TextAnchor.MiddleCenter; \n" 
								+ "\t\t\t\t\t GUILayout.Label(" + "Index".AddQuotes() + ",EditorStyles.toolbar,GUILayout.Width(45)); \n" 
								+ "\t\t\t\t\t GUILayout.Label(" + current_database.tabs[i].tab_variables[v].var_type.AddQuotes() + ",EditorStyles.toolbar); \n" 
								+ "\t\t\t\t\t GUILayout.Label(" + "".AddQuotes() + ",EditorStyles.toolbar); \n" 
								+ "\t\t\t\t\t GUILayout.EndHorizontal(); \n" 
								+ "\t\t\t\t\t " + current_database.tabs[i].tab_variables[v].var_name + "_List_Index = EditorGUILayout.BeginScrollView(" + current_database.tabs[i].tab_variables[v].var_name + "_List_Index," + "box".AddQuotes() + ",GUILayout.MaxHeight(200),GUILayout.Width(350)); \n");

							if (current_database.tabs[i].tab_variables[v].var_list_type == LIST_TYPE.ARRAY)
							{
								file.Write("\t\t\t\t\t for(int i = 0; i < " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + ".Length; i++) \n");
							}

							if (current_database.tabs[i].tab_variables[v].var_list_type == LIST_TYPE.LIST)
							{
								file.Write("\t\t\t\t\t for(int i = 0; i < " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + ".Count; i++) \n");
							}

							file.Write(
								"\t\t\t\t\t { \n" 
								+ "\t\t\t\t\t\t GUILayout.Space(2.5f); \n" 
								+ "\t\t\t\t\t\t EditorGUILayout.BeginHorizontal(); \n\n" 
								+ "\t\t\t\t\t\t if(GUILayout.Button(" + "DEL".AddQuotes() + ",EditorStyles.toolbarButton,GUILayout.Width(45))) \n" 
								+ "\t\t\t\t\t\t { \n" 
								+ "\t\t\t\t\t\t GUI.FocusControl(" + "Clear".AddQuotes() + "); \n");


							if (current_database.tabs[i].tab_variables[v].var_list_type == LIST_TYPE.LIST)
							{
								file.Write("\t\t\t\t\t\t current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + ".Remove(" + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i]); \n");
							}
							if (current_database.tabs[i].tab_variables[v].var_list_type == LIST_TYPE.ARRAY)
							{
								file.Write("\t\t\t\t\t\t ArrayUtility.RemoveAt(ref " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + ", i); \n");
							}

							file.Write(
								"\t\t\t\t\t\t return; \n" 
								+ "\t\t\t\t\t } \n" 
								+ "\t\t\t\t\t GUILayout.Space(5.0f); \n" 
								+ "\t\t\t\t\t GUILayout.Label(i.ToString(),EditorStyles.miniButtonMid, GUILayout.Width(35)); \n");


							//TODO: ADD the FOR loop for each of the field area types
							if (current_database.tabs[i].tab_variables[v].selected_variable_type == SELECTED_TYPE.VALUE)
							{
								file.Write("\t\t\t\t\t " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i] = EditorGUILayout.");

								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.STRING)
								{
									file.Write("TextField");
									file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.BOOL)
								{
									file.Write("Toggle");
									file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.COLOR)
								{
									file.Write("ColorField");
									file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.COLOR32)
								{
									file.Write("ColorField");
									file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.DOUBLE)
								{
									file.Write("DoubleField");
									file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.FLOAT)
								{
									file.Write("FloatField");
									file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.INT)
								{
									file.Write("IntField");
									file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.RECT)
								{
									file.Write("RectField");
									file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR2)
								{
									file.Write("Vector2Field");
									file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR3)
								{
									file.Write("Vector3Field");
									file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
								if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR4)
								{
									file.Write("Vector4Field");
									file.Write("(" + "".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], GUILayout.Width(238)); \n");
								}
							}
							if (current_database.tabs[i].tab_variables[v].selected_variable_type == SELECTED_TYPE.REFERENCE)
							{
								file.Write("\t\t\t\t\t " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i] = " + "(" + current_database.tabs[i].tab_variables[v].var_type + ")" + "EditorGUILayout.ObjectField");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + "[i], typeof(" + current_database.tabs[i].tab_variables[v].var_type + ") , true, GUILayout.Width(238));");
							}


							file.Write(
								"\t\t\t\t\t EditorGUILayout.EndHorizontal(); \n" 
								+ "\t\t\t\t\t } \n" 
								+ "\t\t\t\t\t EditorGUILayout.EndScrollView(); \n" 
								+ "\t\t\t\t\t if(GUILayout.Button(" + "New".AddQuotes() + "," + "box".AddQuotes() + ", GUILayout.Width(350))) \n" 
								+ "\t\t\t\t\t { \n");
							if (current_database.tabs[i].tab_variables[v].var_list_type == LIST_TYPE.ARRAY)
							{
								file.Write("\t\t\t\t\t\t ArrayUtility.Add(ref " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + ",");

								if (current_database.tabs[i].tab_variables[v].selected_variable_type == SELECTED_TYPE.VALUE)
								{
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.STRING)
									{
										file.Write("".AddQuotes());
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.BOOL)
									{
										file.Write("false");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.COLOR)
									{
										file.Write("new Color(0,0,0)");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.COLOR32)
									{
										file.Write("new Color32(0,0,0,0)");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.DOUBLE)
									{
										file.Write("0");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.FLOAT)
									{
										file.Write("0");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.INT)
									{
										file.Write("0");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.RECT)
									{
										file.Write("new Rect(0,0,0,0)");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR2)
									{
										file.Write("new Vector2(0,0)");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR3)
									{
										file.Write("new Vector3(0,0,0)");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR4)
									{
										file.Write("new Vector4(0,0,0,0)");
									}
								}
								if (current_database.tabs[i].tab_variables[v].selected_variable_type == SELECTED_TYPE.REFERENCE)
								{
									file.Write("null");
								}
								file.Write("); \n");
							}
							if (current_database.tabs[i].tab_variables[v].var_list_type == LIST_TYPE.LIST)
							{
								file.Write("\t\t\t\t\t\t " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + ".Add(");

								if (current_database.tabs[i].tab_variables[v].selected_variable_type == SELECTED_TYPE.VALUE)
								{
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.STRING)
									{
										file.Write("".AddQuotes());
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.BOOL)
									{
										file.Write("false");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.COLOR)
									{
										file.Write("new Color(0,0,0)");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.COLOR32)
									{
										file.Write("new Color32(0,0,0,0)");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.DOUBLE)
									{
										file.Write("0");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.FLOAT)
									{
										file.Write("0");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.INT)
									{
										file.Write("0");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.LONG)
									{
										file.Write("0");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.RECT)
									{
										file.Write("new Rect(0,0,0,0)");
									}

									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR2)
									{
										file.Write("new Vector2(0,0)");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR3)
									{
										file.Write("new Vector3(0,0,0)");
									}
									if (current_database.tabs[i].tab_variables[v].variable_value_type == VALUE_DATATYPE.VECTOR4)
									{
										file.Write("new Vector4(0,0,0,0)");
									}
								}
								if (current_database.tabs[i].tab_variables[v].selected_variable_type == SELECTED_TYPE.REFERENCE)
								{
									file.Write("null");
								}
								file.Write("); \n");
							}

							file.Write(
								"" + "\t\t\t\t\t } \n" 
								+ "\t\t\t\t\t EditorGUILayout.EndVertical(); \n" 
								+ "\t\t\t\t\t EditorGUILayout.Separator(); \n");
//								file.Write("\t\t\t\t " + "current_" + current_database.NAME + "." + current_database.tabs[i].tab_variables[v].var_name + " = ");	


							file.Write("\t\t\t\t } \n \n");
//							}

//							varcount++;
						}
					}

					file.Write("\t\t\t } \n" + "");
				}
			}

			if (current_database.enable_tabs == false)
			{
				file.Write("\t\t\t private void MainArea() \n" 
					+ "\t\t\t { \n");

				file.Write("\t\t\t\t GUILayout.Label(" + " ID:".AddQuotes() + " + current_" + current_database.NAME + ".ID" + " ,GUILayout.Width(50)); \n");
				file.Write("\t\t\t\t" + "current_" + current_database.NAME + ".NAME = " + "EditorGUILayout.TextField(" + "NAME: ".AddQuotes() + ", current_" + current_database.NAME + ".NAME" + "); \n");

				for (int v = 0; v < current_database.variables.Count; v++)
				{
					if (current_database.variables[v].var_list_type == LIST_TYPE.NONE)
					{
						file.Write("\t\t\t\t current_" + current_database.NAME + "." + current_database.variables[v].var_name + " = ");

						if (current_database.variables[v].selected_variable_type == SELECTED_TYPE.VALUE)
						{
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.STRING)
							{
								file.Write("EditorGUILayout.TextField(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.BOOL)
							{
								file.Write("EditorGUILayout.Toggle(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.INT)
							{
								file.Write("EditorGUILayout.IntField(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.LONG)
							{
								file.Write("EditorGUILayout.LongField(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.FLOAT)
							{
								file.Write("EditorGUILayout.FloatField(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.DOUBLE)
							{
								file.Write("EditorGUILayout.DoubleField(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.COLOR)
							{
								file.Write("EditorGUILayout.ColorField(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.COLOR32)
							{
								file.Write("EditorGUILayout.ColorField(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.RECT)
							{
								file.Write("EditorGUILayout.RectField(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR2)
							{
								file.Write("EditorGUILayout.Vector2Field(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR3)
							{
								file.Write("EditorGUILayout.Vector3Field(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR4)
							{
								file.Write("EditorGUILayout.Vector4Field(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "); \n");
							}
						}

						if (current_database.variables[v].selected_variable_type == SELECTED_TYPE.REFERENCE)
						{
							file.Write("(" + current_database.variables[v].var_type + ") EditorGUILayout.ObjectField(" + current_database.variables[v].var_name.AddQuotes() + " + " + ": ".AddQuotes() + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + ", typeof(" + current_database.variables[v].var_type + ") , true" + "); \n");
						}
					}

					if (current_database.variables[v].var_list_type == LIST_TYPE.ARRAY || current_database.variables[v].var_list_type == LIST_TYPE.LIST)
					{
//						for (int b = 0; b < current_database.dropdown_bool_list.Count; b++)
//						{
						varcount += 1;

						//							Debug.Log("Bool Count: " + current_database.dropdown_bool_list.Count);
						file.Write("\t\t\t\t foldout_expanded[" + varcount + "] = EditorGUILayout.Foldout(foldout_expanded[" + varcount + "]," + current_database.variables[v].var_name.AddQuotes() + "); \n" + "\t\t\t\t if (foldout_expanded[" + varcount + "] == true) \n" + "\t\t\t\t { \n" + "\t\t\t\t\t EditorGUILayout.BeginVertical(); \n" + "\t\t\t\t\t GUILayout.BeginHorizontal(); \n" + "\t\t\t\t\t EditorStyles.toolbar.alignment = TextAnchor.MiddleLeft; \n" + "\t\t\t\t\t GUILayout.Label(" + "Delete".AddQuotes() + ",EditorStyles.toolbar,GUILayout.Width(45)); \n" + "\t\t\t\t\t EditorStyles.toolbar.alignment = TextAnchor.MiddleCenter; \n" + "\t\t\t\t\t GUILayout.Label(" + "Index".AddQuotes() + ",EditorStyles.toolbar,GUILayout.Width(45)); \n" + "\t\t\t\t\t GUILayout.Label(" + current_database.variables[v].var_type.AddQuotes() + ",EditorStyles.toolbar); \n" + "\t\t\t\t\t GUILayout.Label(" + "".AddQuotes() + ",EditorStyles.toolbar); \n" + "\t\t\t\t\t GUILayout.EndHorizontal(); \n" + "\t\t\t\t\t " + current_database.variables[v].var_name + "_List_Index = EditorGUILayout.BeginScrollView(" + current_database.variables[v].var_name + "_List_Index," + "box".AddQuotes() + ",GUILayout.MaxHeight(200),GUILayout.Width(350)); \n");

						if (current_database.variables[v].var_list_type == LIST_TYPE.ARRAY)
						{
							file.Write("\t\t\t\t\t for(int i = 0; i < " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + ".Length; i++) \n");
						}

						if (current_database.variables[v].var_list_type == LIST_TYPE.LIST)
						{
							file.Write("\t\t\t\t\t for(int i = 0; i < " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + ".Count; i++) \n");
						}

						file.Write("\t\t\t\t\t { \n" + "\t\t\t\t\t\t GUILayout.Space(2.5f); \n" + "\t\t\t\t\t\t EditorGUILayout.BeginHorizontal(); \n\n" + "\t\t\t\t\t\t if(GUILayout.Button(" + "DEL".AddQuotes() + ",EditorStyles.toolbarButton,GUILayout.Width(45))) \n" + "\t\t\t\t\t\t { \n" + "\t\t\t\t\t\t GUI.FocusControl(" + "Clear".AddQuotes() + "); \n");


						if (current_database.variables[v].var_list_type == LIST_TYPE.LIST)
						{
							file.Write("\t\t\t\t\t\t current_" + current_database.NAME + "." + current_database.variables[v].var_name + ".Remove(" + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i]); \n");
						}
						if (current_database.variables[v].var_list_type == LIST_TYPE.ARRAY)
						{
							file.Write("\t\t\t\t\t\t ArrayUtility.RemoveAt(ref " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + ", i); \n");
						}

						file.Write("\t\t\t\t\t\t return; \n" + "\t\t\t\t\t } \n" + "\t\t\t\t\t GUILayout.Space(5.0f); \n" + "\t\t\t\t\t GUILayout.Label(i.ToString(),EditorStyles.miniButtonMid, GUILayout.Width(35)); \n");


						//TODO: ADD the FOR loop for each of the field area types
						if (current_database.variables[v].selected_variable_type == SELECTED_TYPE.VALUE)
						{
							file.Write("\t\t\t\t\t current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i] = EditorGUILayout.");

							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.STRING)
							{
								file.Write("TextField");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.BOOL)
							{
								file.Write("Toggle");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.COLOR)
							{
								file.Write("ColorField");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.COLOR32)
							{
								file.Write("ColorField");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.DOUBLE)
							{
								file.Write("DoubleField");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.FLOAT)
							{
								file.Write("FloatField");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.INT)
							{
								file.Write("IntField");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.RECT)
							{
								file.Write("RectField");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR2)
							{
								file.Write("Vector2Field");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR3)
							{
								file.Write("Vector3Field");
								file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
							if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR4)
							{
								file.Write("Vector4Field");
								file.Write("(" + "".AddQuotes() + "," + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], GUILayout.Width(238));");
							}
						}
						if (current_database.variables[v].selected_variable_type == SELECTED_TYPE.REFERENCE)
						{
							file.Write("\t\t\t\t\t current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i] = " + "(" + current_database.variables[v].var_type + ")" + "EditorGUILayout.ObjectField");
							file.Write("(" + "new GUIContent(" + "".AddQuotes() + ")" + ", " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + "[i], typeof(" + current_database.variables[v].var_type + ") , true, GUILayout.Width(238));");
						}


						file.Write(
							"\t\t\t\t\t EditorGUILayout.EndHorizontal(); \n" 
							+ "\t\t\t\t\t } \n" 
							+ "\t\t\t\t\t EditorGUILayout.EndScrollView(); \n" 
							+ "\t\t\t\t\t if(GUILayout.Button(" + "New".AddQuotes() + "," + "box".AddQuotes() + ", GUILayout.Width(350))) \n" 
							+ "\t\t\t\t\t { \n");
						if (current_database.variables[v].var_list_type == LIST_TYPE.ARRAY)
						{
							file.Write("\t\t\t\t\t\t ArrayUtility.Add(ref " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + ",");

							if (current_database.variables[v].selected_variable_type == SELECTED_TYPE.VALUE)
							{
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.STRING)
								{
									file.Write("".AddQuotes());
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.BOOL)
								{
									file.Write("false");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.COLOR)
								{
									file.Write("new Color(0,0,0)");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.COLOR32)
								{
									file.Write("new Color32(0,0,0,0)");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.DOUBLE)
								{
									file.Write("0");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.FLOAT)
								{
									file.Write("0");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.INT)
								{
									file.Write("0");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.LONG)
								{
									file.Write("0");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.RECT)
								{
									file.Write("new Rect(0,0,0,0)");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR2)
								{
									file.Write("new Vector2(0,0)");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR3)
								{
									file.Write("new Vector3(0,0,0)");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR4)
								{
									file.Write("new Vector4(0,0,0,0)");
								}
							}
							if (current_database.variables[v].selected_variable_type == SELECTED_TYPE.REFERENCE)
							{
								file.Write("null");
							}
							file.Write("); \n");
						}
						if (current_database.variables[v].var_list_type == LIST_TYPE.LIST)
						{
							file.Write("\t\t\t\t\t\t " + "current_" + current_database.NAME + "." + current_database.variables[v].var_name + ".Add(");

							if (current_database.variables[v].selected_variable_type == SELECTED_TYPE.VALUE)
							{
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.STRING)
								{
									file.Write("".AddQuotes());
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.BOOL)
								{
									file.Write("false");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.COLOR)
								{
									file.Write("new Color(0,0,0)");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.COLOR32)
								{
									file.Write("new Color32(0,0,0,0)");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.DOUBLE)
								{
									file.Write("0");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.FLOAT)
								{
									file.Write("0");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.INT)
								{
									file.Write("0");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.RECT)
								{
									file.Write("new Rect(0,0,0,0)");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR2)
								{
									file.Write("new Vector2(0,0)");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR3)
								{
									file.Write("new Vector3(0,0,0)");
								}
								if (current_database.variables[v].variable_value_type == VALUE_DATATYPE.VECTOR4)
								{
									file.Write("new Vector4(0,0,0,0)");
								}
							}
							if (current_database.variables[v].selected_variable_type == SELECTED_TYPE.REFERENCE)
							{
								file.Write("null");
							}
							file.Write("); \n");
						}

						file.Write(
							"" + "\t\t\t\t\t } \n" 
							+ "\t\t\t\t\t EditorGUILayout.EndVertical(); \n" 
							+ "\t\t\t\t\t EditorGUILayout.Separator(); \n");


						file.Write("\t\t\t\t } \n \n");
//						}
					}
				}

				file.Write("\t\t\t } \n");
			}

			//write closing
			file.Write("\t\t } \n" + "\t } \n" + "} \n");

			file.Close();
			AssetDatabase.Refresh();
		}

		/// <summary>
		/// This method will create the MAIN_GUI so that any databases added/removed will be reflected accordingly and will have access from the DATABASE_MANAGER_GUI
		/// </summary>
		public static void Create_MAIN_Database_GUI()
		{
			string database_main_gui_location = "Assets/MDF EDITOR/GENERATED_DATABASES/MAIN_GUI/Editor";
			string main_gui_name = "DATABASE_GUI";
			if (Directory.Exists(database_main_gui_location) != true)
			{
				Directory.CreateDirectory(database_main_gui_location);
				Debug.Log("Creating MAIN GUI SCRIPT Directory!");
			}
			string database_main_gui_file = database_main_gui_location + "/" + main_gui_name + ".cs";
			StreamWriter file = new StreamWriter(database_main_gui_file);
			// This writes the Header and required setup for the Main GUI
			file.Write("using MDF_EDITOR.DATABASE; // THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK \n" + "using UnityEngine; \n" + "using UnityEditor; \n" + "using System.Collections; \n" + "using System.Collections.Generic; \n \n" + "namespace MDF_EDITOR \n" + "{ \n" + "\t namespace DATABASE \n" + "\t { \n" + "\t\t public class " + main_gui_name + ": EditorWindow \n" + "\t\t { \n" + "\t\t\t private string current_tab; // This contains a string that stores the name of the tab, this is used to navigate the GUI \n" + "\t\t\t private static int current_index; \n" + "\t\t\t private static " + main_gui_name + " _" + main_gui_name.ToLower() + ";  // Assigning this class to a name to be used for reference. instead of (this)\n" + "\t\t\t private const string menu_shortcut = " + "%#d".AddQuotes() + "; // This is the keyboard shortcut to open the menu if enabled\n" + "\t\t\t private const string menu_name = " + "MDF EDITOR/Database ".AddQuotes() + " + menu_shortcut; // this is the menu name that will appear as a dropdown from the unity toolbar\n" + "\t\t\t private string current_version = " + VERSION.AddQuotes() + ";  // This is the Version of the MDF EDITOR\n " + "\t\t\t private const bool show_menu = true; // when true this will show in the Unity Toolbar dropdown menu \n" + "\t\t\t private static string custom_skin_path = " + "Assets/MDF EDITOR/FRAMEWORK/Skin/DatabaseGUISkin.guiskin".AddQuotes() + "; // Location of GUISKIN \n" + "\t\t\t private static GUISkin custom_skin; // This is the GUISKIN variable that contains the skin selected \n" + "\t\t\t private static OPTIONS_GUI options_gui; \n" +
					   //"\t\t\t private static OPTIONS_GUI options_gui = (OPTIONS_GUI)CreateInstance(" + "OPTIONS_GUI".AddQuotes() + "); \n" +
					   //"\t\t\t private static DATABASE_MANAGER_GUI database_manager_gui = (DATABASE_MANAGER_GUI)CreateInstance(" + "DATABASE_MANAGER_GUI".AddQuotes() + "); \n");
					   "\t\t\t private static DATABASE_MANAGER_GUI database_manager_gui; \n");

			for (int i = 0; i < DATABASE.DATABASE.DB_Info.COUNT; i++)
			{
				if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).SAVED == true)
				{
					if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).standalone == false)
					{
						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == true && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == false)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_prefix + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME;
						}

						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == false && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == true)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_suffix;
						}

						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == true && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == true)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_prefix + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_suffix;
						}

						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == false && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == false)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME;
						}
						string gui_name = full_database_name + "_GUI";

						//					file.Write("\t\t\t public static " + gui_name + " _" +  gui_name + "; \n");     

						file.Write("\t\t\t public static " + gui_name + " _" + gui_name + ";\n");
						//file.Write("\t\t\t public static " + gui_name + " _" + gui_name + "; = (" + gui_name + ") CreateInstance(" + gui_name.AddQuotes() + "); \n");
					}
				}
			}

			// This writes the the main structure for the GUI, window, onGUI, etc
			file.Write(
				"\t\t\t [MenuItem(menu_name,!show_menu,1)] // This will show the menu. the Name, and Toggle, and the 1 means top priority on top \n" 
				+ "\t\t\t public static void Init() // This is when the script is initialized \n" 
				+ "\t\t\t { \n" 
				+ "\t\t\t\t options_gui = (OPTIONS_GUI)CreateInstance(" + "OPTIONS_GUI".AddQuotes() + "); \n" 
				+ "\t\t\t\t database_manager_gui = (DATABASE_MANAGER_GUI)CreateInstance(" + "DATABASE_MANAGER_GUI".AddQuotes() + "); \n");

			for (int i = 0; i < DATABASE.DATABASE.DB_Info.COUNT; i++)
			{
				if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).SAVED == true)
				{
					if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).standalone == false)
					{
						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == true && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == false)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_prefix + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME;
						}

						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == false && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == true)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_suffix;
						}

						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == true && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == true)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_prefix + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_suffix;
						}

						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == false && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == false)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME;
						}
						string gui_name = full_database_name + "_GUI";

						//					file.Write("\t\t\t public static " + gui_name + " _" +  gui_name + "; \n");     

						file.Write("\t\t\t\t _" + gui_name + " = (" + gui_name + ") CreateInstance(" + gui_name.AddQuotes() + "); \n");
					}
				}
			}


			file.Write(
				"\t\t\t\t " + "_" + main_gui_name.ToLower() + " = GetWindow<" + main_gui_name + "> ();  // This assigns the DB_GUI (this script) to the Window \n" 
				+ "\t\t\t\t " + "_" + main_gui_name.ToLower() + ".minSize = new Vector2(1100, 400); // Sets the Minimum size for the window (This can be tweaked to preference) \n" 
				+ "\t\t\t\t " + "_" + main_gui_name.ToLower() + ".Show(); // This will show the window \n" 
				+ "\t\t\t } \n \n" 

				+ "\t\t\t public static void REPAINT () \n" 
				+ "\t\t\t { \n" 
				+ "\t\t\t\t " + " _" + main_gui_name.ToLower() + " = GetWindow<" + main_gui_name + ">(); \n" 
				+ "\t\t\t\t " + " _" + main_gui_name.ToLower() + ".Repaint(); \n" 
				+ "\t\t\t } \n \n" 

				+ "\t\t\t public static void CLOSE () \n" 
				+ "\t\t\t { \n" 
				+ "\t\t\t\t " + " _" + main_gui_name.ToLower() + " = GetWindow<" + main_gui_name + ">(); \n" 
				+ "\t\t\t\t " + " _" + main_gui_name.ToLower() + ".Close(); \n" 
				+ "\t\t\t } \n \n" 

				+ "\t\t\t public void OnGUI() // This is like Update, and runs constantly just at different intervals \n" 
				+ "\t\t\t{ \n" 
				+ "\t\t\t\t if( custom_skin == null ){ custom_skin = (GUISkin)(AssetDatabase.LoadAssetAtPath(custom_skin_path, typeof(GUISkin))); } GUI.skin = custom_skin; \n" 
				+ "\t\t\t\t GUI.SetNextControlName(" + "Clear".AddQuotes() + "); // This can be called to de-select something, like when a button is pressed \n" 
				+ "\t\t\t\t GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)); \n" 
				+ "\t\t\t\t if (GUILayout.Button(" + "DATABASES".AddQuotes() + ",EditorStyles.toolbarButton, GUILayout.Width(120))) { current_tab = " + "DATABASES".AddQuotes() + "; } \n" 
				+ "\t\t\t\t if (GUILayout.Button(" + "Manage Databases".AddQuotes() + ",EditorStyles.toolbarButton, GUILayout.Width(120))) { current_tab = " + "Manage Databases".AddQuotes() + "; } \n" 
				+ "\t\t\t\t if (GUILayout.Button(" + "Options".AddQuotes() + ",EditorStyles.toolbarButton, GUILayout.Width(100))) { current_tab = " + "Options".AddQuotes() + "; } \n" 
				+ "\t\t\t\t if (GUILayout.Button(" + "About".AddQuotes() + ",EditorStyles.toolbarButton, GUILayout.Width(100))) \n" 
				+ "\t\t\t\t { \n" 
				+ "\t\t\t\t\t EditorUtility.DisplayDialog( \n" 
				+ "\t\t\t\t\t\t" + "(MDF) Modular Database Framework Version: ".AddQuotes() + " + current_version, \n" 
				+ "\t\t\t\t\t\t" + "The Modular Database Framework was developed by Dragon Lens Studios INC. ".AddQuotes() + " + \n" 
				+ "\t\t\t\t\t\t" + "We hope you enjoy!".AddQuotes() + ", \n" 
				+ "\t\t\t\t\t\t" + "Continue".AddQuotes() + "); \n" 
				+ "\t\t\t\t } \n" 
				+ "\t\t\t\t EditorGUILayout.EndHorizontal(); \n" 
				+ "\t\t\t\t if (current_tab != " + "Options".AddQuotes() + " && current_tab != " + "Manage Databases".AddQuotes() + " ) \n" 
				+ "\t\t\t\t { \n" 
				+ "\t\t\t\t\t EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(false)); \n"
				+ "\t\t\t\t\t EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(false)); \n");

			for (int i = 0; i < DATABASE.DATABASE.DB_Info.COUNT; i++)
			{
				if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).SAVED == true)
				{
					if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).standalone == false)
					{
						file.Write("\t\t\t\t\t if (GUILayout.Button(" + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME.AddQuotes() + ")) { current_tab = " + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME.AddQuotes() + "; } \n");
					}
				}
			}

			// This will end the current_tab DATABASES check and format the required tabs before custom database tabs
			file.Write(
				"\t\t\t\t\t EditorGUILayout.EndHorizontal(); \n" 
				+ "\t\t\t\t\t EditorGUILayout.EndVertical(); \n" 
				+ "\t\t\t\t } \n" 
				+ "\t\t\t\t EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)); \n \n" 
				+ "\t\t\t\t switch(current_tab) // This is a Switch case statement that will check the current tab string and switch tabs \n" 
				+ "\t\t\t\t { \n" 
				+ "\t\t\t\t\t case " + "Manage Databases".AddQuotes() + ": database_manager_gui.OnGUI(); break; \n" 
				+ "\t\t\t\t\t case " + "Options".AddQuotes() + ": options_gui.OnGUI(); break; \n");

			for (int i = 0; i < DATABASE.DATABASE.DB_Info.COUNT; i++)
			{
				if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).SAVED == true)
				{
					if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).standalone == false)
					{
						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == true && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == false)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_prefix + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME;
						}

						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == false && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == true)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_suffix;
						}

						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == true && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == true)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_prefix + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).database_suffix;
						}

						if (DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_prefix == false && DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).enable_database_suffix == false)
						{
							full_database_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME;
						}

						file.Write("\t\t\t\t\t case " + DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME.AddQuotes() + ": " + "_" + full_database_name + "_GUI" + ".Repaint(); " + "_" + full_database_name + "_GUI" + ".OnGUI();" + " break; \n");
					}
				}
			}

			// This writes the closing for the Main GUI and swtich case and OnGUI
			file.Write(
				"\t\t\t\t } \n \n" 
				+ "\t\t\t\t EditorGUILayout.EndHorizontal(); \n \n" 
				+ "\t\t\t} \n" 
				+ "\t\t } \n" 
				+ "\t } \n" 
				+ "} \n");
			file.Close();
//			AssetDatabase.Refresh();
		} 	

		/// <summary>
		/// This will create the main Accessors class. This will be the main class you interact with while handling the databases. For Example Database.Actors. Actors is a property in Database that will check for the scriptable object and then create it if it does not exist in editor time or in runtime will load the resource.
		/// </summary>
		public static void Create_Script_Database_Class()
		{
			string database_manager_class_location = "Assets/MDF EDITOR/GENERATED_DATABASES/_Manager Scripts";
			if (!Directory.Exists(database_manager_class_location))
			{
				Directory.CreateDirectory(database_manager_class_location);
				Debug.Log("Creating Script Directory!");
			}

			string database_script_file = database_manager_class_location + "/" + current_database.database_class + ".cs";

			StreamWriter file = new StreamWriter(database_script_file);
			//This makes the header for the database
			file.Write(
				"using UnityEngine; \n" 
				+ "using System.Collections; \n" 
				+ "using System.Collections.Generic; \n" 
				+ "using System.IO; \n" 
				+ "#if UNITY_EDITOR \n" 
				+ "using UnityEditor; \n" 
				+ "#endif \n \n" 
				
				+ "namespace MDF_EDITOR \n" 
				+ "{ \n" + "namespace DATABASE \n" 
				+ "{ \n" + "\t public static partial class " + current_database.database_class 
				+ "{ \n \n" 
				+ "\t\t private static OPTIONS_DB_HANDLER options; \n" 
				+ "\t\t public static OPTIONS_DB_HANDLER Options \n" 
				+ "\t\t { \n" 
				+ "\t\t\t get\n" 
				+ "\t\t\t { \n #if UNITY_EDITOR \n" 
				+ "\t\t\t\t if(Directory.Exists(OPTIONS_DB_HANDLER.ASSET_PATH) == false){Directory.CreateDirectory(OPTIONS_DB_HANDLER.ASSET_PATH);} \n" 
				+ "\t\t\t\t if (File.Exists(OPTIONS_DB_HANDLER.DATABASE_PATH) == false){AssetDatabase.CreateAsset((OPTIONS_DB_HANDLER)ScriptableObject.CreateInstance(" + "OPTIONS_DB_HANDLER".AddQuotes() + "),OPTIONS_DB_HANDLER.DATABASE_PATH);AssetDatabase.SaveAssets();} \n" 
				+ "\t\t\t\t options = (OPTIONS_DB_HANDLER)AssetDatabase.LoadAssetAtPath(OPTIONS_DB_HANDLER.DATABASE_PATH, typeof(OPTIONS_DB_HANDLER)); \n" 
				+ "\t\t\t\t EditorUtility.SetDirty(options); \n" +
				" #endif \n"
				+ "\t\t\t\t return options; \n" 
				+ "\t\t\t }\n" 
				+  "\t\t } \n" 

				+ "\t\t private static DB_INFO_HANDLER db_info; \n" 
				+ "\t\t public static DB_INFO_HANDLER DB_Info \n" 
				+ "\t\t { \n" 
				+ "\t\t\t get\n" 
				+ "\t\t\t { \n" +
				" #if UNITY_EDITOR \n" 
				+ "\t\t\t\t if(Directory.Exists(DB_INFO_HANDLER.ASSET_PATH) == false){Directory.CreateDirectory(DB_INFO_HANDLER.ASSET_PATH);} \n" 
				+ "\t\t\t\t if (File.Exists(DB_INFO_HANDLER.DATABASE_PATH) == false){AssetDatabase.CreateAsset((DB_INFO_HANDLER)ScriptableObject.CreateInstance(" + "DB_INFO_HANDLER".AddQuotes() + "),DB_INFO_HANDLER.DATABASE_PATH);AssetDatabase.SaveAssets();} \n" 
				+ "\t\t\t\t db_info = (DB_INFO_HANDLER)AssetDatabase.LoadAssetAtPath(DB_INFO_HANDLER.DATABASE_PATH, typeof(DB_INFO_HANDLER)); \n" 
				+ "\t\t\t\t EditorUtility.SetDirty(db_info); \n #endif \n" 
				+ "\t\t\t\t return db_info; \n" 
				+ "\t\t\t } \n" 
				+ "\t\t } \n");

			for (int i = 0; i < DATABASE.DATABASE.DB_Info.COUNT; i++)
			{
				DB_Database_INFO _current_database = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i);
				if (_current_database.SAVED)
				{
					string _database_handler_script = _current_database.NAME + "_DB_Handler";
					file.Write(
						"\t\t private static " + _database_handler_script + " _" + _current_database.NAME + "; \n" 
						+"\t\t public static " + _database_handler_script + " " + _current_database.NAME+ " \n" 
						+ "\t\t { \n" 
						+ "\t\t\t get\n" 
						+ "\t\t\t { \n " 
						+ "#if UNITY_EDITOR \n" 
						+ "\t\t\t\t if(Directory.Exists(" + _database_handler_script + ".ASSET_PATH) == false){Directory.CreateDirectory("+ _database_handler_script + ".ASSET_PATH);} \n" 
						+ "\t\t\t\t if (File.Exists(" +_database_handler_script + ".DATABASE_PATH) == false){AssetDatabase.CreateAsset((" +_database_handler_script+ ")ScriptableObject.CreateInstance(" + _database_handler_script.AddQuotes() + ")," +_database_handler_script + ".DATABASE_PATH);" 
						+ "AssetDatabase.SaveAssets();} \n" 
						+ "\t\t\t\t _" + _current_database.NAME + " = (" + _database_handler_script + ")AssetDatabase.LoadAssetAtPath(" +_database_handler_script + ".DATABASE_PATH, typeof(" + _database_handler_script + ")); \n" 
						+ "\t\t\t\t EditorUtility.SetDirty(_"+ _current_database.NAME + "); \n"
						+"  #endif \n" + "\t\t\t\t if(_" + _current_database.NAME +" == null)\n" 
						+ "\t\t\t\t\t _" + _current_database.NAME + " = (" + _database_handler_script +")Resources.Load("+ _database_handler_script + ".ASSET_PATH +" + "/".AddQuotes() + "+ " +_database_handler_script + ".ASSET_NAME);\n" 
						+ "\t\t\t\t return _" + _current_database.NAME +"; \n"
						+ "\t\t\t } \n" 
						+ "\t\t } \n");
				}
				else
				{
					Debug.Log(_current_database.NAME + " at INDEX: " + _current_database.ID +" IS NOT SAVED and did not generate the database handle.");
				}
			}

			file.Write("\n }" + "\n }" + "\n }");
//	        string test_me_please = @"
////MY DUDE!            
//    //ANOHTER DUDE
//        //SERIOUSLY DUDE
//            //This is a super test
//                //Seriously testing
//                //WOW
//            //LOLOL
//        //ME GUSTA
//    //WOW MAN
////MY Test
//            ";
			//file.Write(test_me_please);

			file.Close();
			Debug.Log("Ran: Create_Script_Database_Class NAME OF CURRENT DB: " + current_database.NAME);
			//			AssetDatabase.Refresh();
		}
	} 
}
