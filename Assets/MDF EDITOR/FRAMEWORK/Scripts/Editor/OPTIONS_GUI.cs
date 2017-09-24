/* This handles the GUI for the Options Database, this will display all the information and config for the options database
 * if you would like to add anything here feel free!
 */

using MDF_EDITOR.DATABASE; // THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEditorInternal;

namespace MDF_EDITOR 
{

	public class OPTIONS_GUI : EditorWindow 
	{
        /// <summary>
        /// This contains a string that stores the name of the tab, this is used to navigate the GUI
        /// </summary>
		private string current_tab;

        //		private static OPTIONS_DB_HANDLER DATABASE.DATABASE.Options = DATABASE.DATABASE.Options;  // Assigning this class to a name to be used for reference. instead of (this)

        /// <summary>
        /// when true this will show in the Unity tool-bar drop down menu
        /// </summary>
        private const bool show_menu = false;
        /// <summary>
        /// this is the menu name that will appear as a drop down from the unity tool-bar
        /// </summary>
		private const string menu_name = "MDF EDITOR/Options %#o";
        /// <summary>
        /// This is the GUISKIN variable that contains the skin selected
        /// </summary>
		private static GUISkin custom_skin;
		private static string custom_skin_path = "Assets/MDF EDITOR/FRAMEWORK/Skin/DatabaseGUISkin.guiskin"; // Location of GUISKIN

        /// <summary>
        /// This is like Update, and runs constantly just at different intervals 
        /// </summary>
	    public void OnGUI()
		{
			if( custom_skin == null ){ custom_skin = (GUISkin)(AssetDatabase.LoadAssetAtPath(custom_skin_path, typeof(GUISkin))); } GUI.skin = custom_skin;
			EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)); // This creates a new expanding Horizontal row
			MainDisplay(); // This calls the DisplayMainOptions to display everything inside that method here
			EditorGUILayout.EndHorizontal(); // Ends Horizontal row

		}

		private void MainDisplay()
		{
			EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true)); // Creates Expanding Vertical Slot
			EditorGUILayout.Space(); // Adds an auto space before the next slot

			OptionsView();

			EditorGUILayout.Space(); // Adds an auto space before the next slot
			EditorGUILayout.EndVertical(); // Ends vertical slot

		}

	private void OptionsView()
		{
			EditorGUIUtility.labelWidth = 190; // this sets the EdtiorGUI Label width to 190 pixels

			//This is the GUI toggle button that is used to toggle the setting for USE_DEFAULT_DATABASE_DIRECTORY in the Options Database
			DATABASE.DATABASE.Options.DB_Options().USE_DEFAULT_DATABASE_DIRECTORY = EditorGUILayout.Toggle("Use Default Database Directory:",DATABASE.DATABASE.Options.DB_Options().USE_DEFAULT_DATABASE_DIRECTORY);
			if (DATABASE.DATABASE.Options.DB_Options().USE_DEFAULT_DATABASE_DIRECTORY) // Checks to see if USE_DEFAULT_DATABASE_DIRECTORY is true
			{
				EditorGUILayout.BeginHorizontal(); // Begins a Horizontal slot
				// This is a button that opens up a folder selector to pick a folder for the DEFAULT_DATABASE_DIRECTORY
				if (GUILayout.Button("Default Database Location",EditorStyles.miniButton,GUILayout.Width(175)))
				{
					GUI.FocusControl("Clear"); // Deselects all texboxs and focus
					DATABASE.DATABASE.Options.DB_Options().DEFAULT_DATABASE_DIRECTORY = EditorUtility.OpenFolderPanel("Select Default Database Location", DATABASE.DATABASE.Options.DB_Options().DEFAULT_DATABASE_DIRECTORY,"Database");
//					database_source_info = new DirectoryInfo(Options_Handler.DB_Options().DEFAULT_DATABASE_DIRECTORY);
				}
				// This the text field that shows the DEFAULT_DATABASE_DIRECTORY and can be edited
				DATABASE.DATABASE.Options.DB_Options().DEFAULT_DATABASE_DIRECTORY = EditorGUILayout.TextField(DATABASE.DATABASE.Options.DB_Options().DEFAULT_DATABASE_DIRECTORY);
				
				EditorGUILayout.EndHorizontal(); // Ends Horizontal row
			}

			//This is the GUI toggle button that is used to toggle the setting for USE_DEFAULT_BACKUP_DIRECTORY in the Options Database
			DATABASE.DATABASE.Options.DB_Options().USE_DEFAULT_BACKUP_DIRECTORY = EditorGUILayout.Toggle("Use Default Backup Directory:",DATABASE.DATABASE.Options.DB_Options().USE_DEFAULT_BACKUP_DIRECTORY);
			if (DATABASE.DATABASE.Options.DB_Options().USE_DEFAULT_BACKUP_DIRECTORY) // Checks to see if USE_DEFAULT_BACKUP_DIRECTORY is true
			{
				EditorGUILayout.BeginHorizontal(); // Begins a Horizontal slot

				// This is a button that opens up a folder selector to pick a folder for the DEFAULT_BACKUP_DIRECTORY
				if (GUILayout.Button("Default Backup Location",EditorStyles.miniButton,GUILayout.Width(175)))
				{
					GUI.FocusControl("Clear"); // Deselects all textboxs and focus
					DATABASE.DATABASE.Options.DB_Options().DEFAULT_BACKUP_DIRECTORY = EditorUtility.OpenFolderPanel("Select Default Backup Location",DATABASE.DATABASE.Options.DB_Options().DEFAULT_BACKUP_DIRECTORY,"Backup");
				}
				// This the text field that shows the DEFAULT_BACKUP_DIRECTORY and can be edited
				DATABASE.DATABASE.Options.DB_Options().DEFAULT_BACKUP_DIRECTORY = EditorGUILayout.TextField(DATABASE.DATABASE.Options.DB_Options().DEFAULT_BACKUP_DIRECTORY);
				
				EditorGUILayout.EndHorizontal(); // Ends Horizontal row
			}
			
			if (DATABASE.DATABASE.Options.DB_Options().ENABLE_DEFAULT_BACKUP_FOLDER_NAME)
			{
//				backup_folder_name = EditorGUILayout.TextField("Backup Folder Name:",backup_folder_name);
			}
			//This is the GUI toggle button that is used to toggle the setting for ENABLE_DEFAULT_BACKUP_FOLDER_NAME in the Options Database
			DATABASE.DATABASE.Options.DB_Options().ENABLE_DEFAULT_BACKUP_FOLDER_NAME = EditorGUILayout.Toggle("Default Backup Folder Name:",DATABASE.DATABASE.Options.DB_Options().ENABLE_DEFAULT_BACKUP_FOLDER_NAME);
			//This is the GUI toggle button that is used to toggle the setting for SHOW_DELETE_POPUP in the Options Database
			DATABASE.DATABASE.Options.DB_Options().SHOW_DELETE_POPUP = EditorGUILayout.Toggle("Show Delete Popup:",DATABASE.DATABASE.Options.DB_Options().SHOW_DELETE_POPUP);
			//This is the GUI toggle button that is used to toggle the setting for ENABLE_DATABASE_PREFIX in the Options Database
			DATABASE.DATABASE.Options.DB_Options().ENABLE_DATABASE_PREFIX = EditorGUILayout.Toggle("Enable Default Prefix:",DATABASE.DATABASE.Options.DB_Options().ENABLE_DATABASE_PREFIX);
			//This is the GUI toggle button that is used to toggle the setting for ENABLE_DATABASE_SUFFIX in the Options Database
			DATABASE.DATABASE.Options.DB_Options().ENABLE_DATABASE_SUFFIX = EditorGUILayout.Toggle("Enable Default Suffix:",DATABASE.DATABASE.Options.DB_Options().ENABLE_DATABASE_SUFFIX);
			// This the text field that shows the DEFAULT_DATABASE_PREFIX and can be edited
			DATABASE.DATABASE.Options.DB_Options().DEFAULT_DATABASE_PREFIX = EditorGUILayout.TextField("Default Prefix:",DATABASE.DATABASE.Options.DB_Options().DEFAULT_DATABASE_PREFIX);
			// This the text field that shows the DEFAULT_DATABASE_SUFFIX and can be edited
			DATABASE.DATABASE.Options.DB_Options().DEFAULT_DATABASE_SUFFIX = EditorGUILayout.TextField("Default Suffix:",DATABASE.DATABASE.Options.DB_Options().DEFAULT_DATABASE_SUFFIX);
			
			EditorGUILayout.Space(); // Adds an auto space before the next slot

//			if (GUILayout.Button("Save", GUILayout.Width(100))) //Save Button for the Add New Database Object
//			{
//				GUI.FocusControl("Clear"); // Deselects all textboxs and focus
//				Save(); //Calls the save Method
//				
//			}

		} 

//		private void Save (){
//			EditorUtility.SetDirty(DATABASE.DATABASE.Options);  //File I/O saves the Database file. EditorUtility.SetDirty(template) replace template, with your current database variable you set.
//		}
	} 
} 
