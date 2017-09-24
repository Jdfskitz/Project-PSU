using MDF_EDITOR.DATABASE; // THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK 
using UnityEngine; 
using UnityEditor; 
using System.Collections; 
using System.Collections.Generic; 
 
namespace MDF_EDITOR 
{ 
	 namespace DATABASE 
	 { 
		 public class DATABASE_GUI: EditorWindow 
		 { 
			 private string current_tab; // This contains a string that stores the name of the tab, this is used to navigate the GUI 
			 private static int current_index; 
			 private static DATABASE_GUI _database_gui;  // Assigning this class to a name to be used for reference. instead of (this)
			 private const string menu_shortcut = "%#d"; // This is the keyboard shortcut to open the menu if enabled
			 private const string menu_name = "MDF EDITOR/Database " + menu_shortcut; // this is the menu name that will appear as a dropdown from the unity toolbar
			 private string current_version = "0.8.8.1";  // This is the Version of the MDF EDITOR
 			 private const bool show_menu = true; // when true this will show in the Unity Toolbar dropdown menu 
			 private static string custom_skin_path = "Assets/MDF EDITOR/FRAMEWORK/Skin/DatabaseGUISkin.guiskin"; // Location of GUISKIN 
			 private static GUISkin custom_skin; // This is the GUISKIN variable that contains the skin selected 
			 private static OPTIONS_GUI options_gui; 
			 private static DATABASE_MANAGER_GUI database_manager_gui; 
			 public static DB_Prefabs_GUI _DB_Prefabs_GUI;
			 [MenuItem(menu_name,!show_menu,1)] // This will show the menu. the Name, and Toggle, and the 1 means top priority on top 
			 public static void Init() // This is when the script is initialized 
			 { 
				 options_gui = (OPTIONS_GUI)CreateInstance("OPTIONS_GUI"); 
				 database_manager_gui = (DATABASE_MANAGER_GUI)CreateInstance("DATABASE_MANAGER_GUI"); 
				 _DB_Prefabs_GUI = (DB_Prefabs_GUI) CreateInstance("DB_Prefabs_GUI"); 
				 _database_gui = GetWindow<DATABASE_GUI> ();  // This assigns the DB_GUI (this script) to the Window 
				 _database_gui.minSize = new Vector2(1100, 400); // Sets the Minimum size for the window (This can be tweaked to preference) 
				 _database_gui.Show(); // This will show the window 
			 } 
 
			 public static void REPAINT () 
			 { 
				  _database_gui = GetWindow<DATABASE_GUI>(); 
				  _database_gui.Repaint(); 
			 } 
 
			 public static void CLOSE () 
			 { 
				  _database_gui = GetWindow<DATABASE_GUI>(); 
				  _database_gui.Close(); 
			 } 
 
			 public void OnGUI() // This is like Update, and runs constantly just at different intervals 
			{ 
				 if( custom_skin == null ){ custom_skin = (GUISkin)(AssetDatabase.LoadAssetAtPath(custom_skin_path, typeof(GUISkin))); } GUI.skin = custom_skin; 
				 GUI.SetNextControlName("Clear"); // This can be called to de-select something, like when a button is pressed 
				 GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)); 
				 if (GUILayout.Button("DATABASES",EditorStyles.toolbarButton, GUILayout.Width(120))) { current_tab = "DATABASES"; } 
				 if (GUILayout.Button("Manage Databases",EditorStyles.toolbarButton, GUILayout.Width(120))) { current_tab = "Manage Databases"; } 
				 if (GUILayout.Button("Options",EditorStyles.toolbarButton, GUILayout.Width(100))) { current_tab = "Options"; } 
				 if (GUILayout.Button("About",EditorStyles.toolbarButton, GUILayout.Width(100))) 
				 { 
					 EditorUtility.DisplayDialog( 
						"(MDF) Modular Database Framework Version: " + current_version, 
						"The Modular Database Framework was developed by Dragon Lens Studios INC. " + 
						"We hope you enjoy!", 
						"Continue"); 
				 } 
				 EditorGUILayout.EndHorizontal(); 
				 if (current_tab != "Options" && current_tab != "Manage Databases" ) 
				 { 
					 EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(false)); 
					 EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(false)); 
					 if (GUILayout.Button("Prefabs")) { current_tab = "Prefabs"; } 
					 EditorGUILayout.EndHorizontal(); 
					 EditorGUILayout.EndVertical(); 
				 } 
				 EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)); 
 
				 switch(current_tab) // This is a Switch case statement that will check the current tab string and switch tabs 
				 { 
					 case "Manage Databases": database_manager_gui.OnGUI(); break; 
					 case "Options": options_gui.OnGUI(); break; 
					 case "Prefabs": _DB_Prefabs_GUI.Repaint(); _DB_Prefabs_GUI.OnGUI(); break; 
				 } 
 
				 EditorGUILayout.EndHorizontal(); 
 
			} 
		 } 
	 } 
} 
