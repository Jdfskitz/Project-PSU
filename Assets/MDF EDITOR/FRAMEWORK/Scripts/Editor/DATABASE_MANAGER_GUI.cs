using MDF_EDITOR; // THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

//TODO: Add handling for variables being transfered between tabs. Make sure that the variables are added correctly and setup with what was provided in the tabs.

namespace MDF_EDITOR
{
    namespace DATABASE
    {
        public class DATABASE_MANAGER_GUI : EditorWindow
        {
            public static DB_Database_INFO current_database;
            public static int current_db_index;
            public static int current_tab_index; // this represents the current tab index number
            public static int current_var_index; // this represents the current variable index number
            public static DATABASE_MANAGER_GUI database_manager_gui;
            private DATABASE_GUI db_gui;
            private const bool show_menu = false; // when true this will show in the Unity Toolbar dropdown menu
            private const string menu_name = "MDF EDITOR/Database Manager %#m"; // this is the menu name that will appear as a dropdown from the unity toolbar
            private static GUISkin custom_skin; // This is the GUISKIN variable that contains the skin selected
            private static string custom_skin_path = "Assets/MDF EDITOR/FRAMEWORK/Skin/DatabaseGUISkin.guiskin"; // Location of GUISKIN
            private string current_tab; // This contains a string that stores the name of the tab, this is used to navigate the GUI
            private Vector2 list_scroll_position; //tracks gui scroll position
            private Vector2 tab_scroll_position; //tracks gui scroll position
            private Vector2 var_scroll_position; //tracks gui scroll position
            private int selected_id;
            private string hotkey_key;

            private readonly HashSet<string> csharpReservedNames = new HashSet<string>(new string[] {"if", "else", "private", "public", "static", "class", "struct", "new", "enum", "void",
                "namespace", "using","switch","case","break", "default", "for", "while", "do", "return", "byte", "int", "float", "double", "string","String",
                "char", "bool", "virtual", "override", "abstract", "as", "catch", "checked", "const",  "continue",  "decimal",  "delegate",  "do",  "event",
                "extern", "true", "false", "foreach", "goto", "fixed", "explicit", "finally", "implicit", "in", "interface", "internal", "is", "lock", "long",
            "null", "object", "operator", "out", "params", "protected", "readonly", "ref", "sbyte", "sealed", "short", "sizeof", "stackalloc", "this", "throw", "try",
            "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "volatile"});
            private readonly HashSet<string> MDFReservedNames = new HashSet<string>(new string[] {"ID", "NAME" }); 
            private readonly HashSet<string> unityReservedNames = new HashSet<string>(new string[] {}); //TODO: Add unity specific and other common types to be checked.

            private State state; // this handles the GUI states, Blank is empty and Edit means a item is selected.
            private enum State { BLANK, EDIT }

            //DYNAMIC GUI Variables
            private string generate_button_name;

            [MenuItem(menu_name, !show_menu, 2)] // enables the Menu if hide_menu is false;
            public static void Init()
            {
                database_manager_gui = EditorWindow.GetWindow<DATABASE_MANAGER_GUI>();
                database_manager_gui.minSize = new Vector2(1100, 400);
                database_manager_gui.Show();

            }

            /// <summary>
            /// This is like Update, and runs constantly just at different intervals 
            /// </summary>
            public void OnGUI()
            {
                if (custom_skin == null) { custom_skin = (GUISkin)(AssetDatabase.LoadAssetAtPath(custom_skin_path, typeof(GUISkin))); }
                GUI.skin = custom_skin;
                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                DisplayListArea();
                DisplayMainArea();
                EditorGUILayout.EndHorizontal();
            }
            /// <summary>
            /// Displays the a list of databases. renders a Delete button, an ID/Name field where you can select the field to edit that database and a button to add new database on the bottom.
            /// </summary>
            private void DisplayListArea()
            {
                EditorGUILayout.BeginVertical(GUILayout.Width(400));
                EditorGUILayout.Space();
                list_scroll_position = EditorGUILayout.BeginScrollView(list_scroll_position, "box", GUILayout.ExpandHeight(true));
                GUILayout.BeginHorizontal(GUILayout.MaxWidth(250));
                EditorStyles.toolbar.alignment = TextAnchor.MiddleLeft;
                GUILayout.Label("Delete", EditorStyles.toolbar, GUILayout.Width(45));
                EditorStyles.toolbar.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("ID", EditorStyles.toolbar, GUILayout.Width(25));
                GUILayout.Label("Database Name", EditorStyles.toolbar, GUILayout.Width(322));
                GUILayout.EndHorizontal();

                Populate_List_Area(); // Populates the view of the list

                EditorGUILayout.EndScrollView();
                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                EditorGUILayout.LabelField("Databases: " + DATABASE.DB_Info.COUNT, GUILayout.Width(100));

                if (GUILayout.Button("New " + " Database"))
                {
                    GUI.FocusControl("Clear");
                    DATABASE.DB_Info.Add(new DB_Database_INFO());
                    current_db_index = DATABASE.DB_Info.COUNT - 1;
                    selected_id = current_db_index;
                    current_database = DATABASE.DB_Info.DB_Database_INFO(current_db_index);
                    state = State.EDIT;
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
            }

            /// <summary>
            /// This is actual list for the databases that is rendered in <see cref="DisplayListArea"/>
            /// </summary>
            private void Populate_List_Area()
            {
                if (DATABASE.DB_Info.COUNT > 0)
                {
                    for (int current_db_index = 0; current_db_index < DATABASE.DB_Info.COUNT; current_db_index++)
                    {
                        if (DATABASE.DB_Info.DB_Database_INFO(current_db_index) == null)
                            continue;

                        DATABASE.DB_Info.DB_Database_INFO(current_db_index).ID = current_db_index;

                        EditorGUILayout.BeginHorizontal();
                        if (GUILayout.Button("DEL", GUILayout.Width(35)))
                        {
                            if (EditorUtility.DisplayDialog("ARE YOU SURE YOU WANT TO REMOVE " + DATABASE.DB_Info.DB_Database_INFO(current_db_index).NAME + "?", "THIS WILL DELETE THE DATABASE PERMANENTLY! Note: please remove ANY reference to this DATABASE before you delete otherwise you will get compiler errors.", "Delete", "Keep"))
                            {
                                GUI.FocusControl("Clear");
                                current_database = DATABASE.DB_Info.DB_Database_INFO(current_db_index);
                                if (current_database.SAVED == true && current_database.NAME != "")
                                {
                                    MANAGE_DATABASE.DeleteDatabase(current_database);
                                    DATABASE.DB_Info.RemoveAt(current_db_index);
                                    MANAGE_DATABASE.Create_Script_Database_Class();
                                    MANAGE_DATABASE.Create_MAIN_Database_GUI();
                                    AssetDatabase.Refresh();
                                }

                                if (current_database.SAVED == false || current_database.NAME == string.Empty)
                                {
                                    DATABASE.DB_Info.RemoveAt(current_db_index);
                                }

                                state = State.BLANK;
                                return;
                            }
                        }

                        GUILayout.Label(DATABASE.DB_Info.DB_Database_INFO(current_db_index).ID.ToString(), "box", GUILayout.Width(37));
                        if (GUILayout.Toggle(selected_id == current_db_index, DATABASE.DB_Info.DB_Database_INFO(current_db_index).NAME, "box", GUILayout.ExpandWidth(true)))
                        {
                            if (selected_id != current_db_index)
                            {
                                GUI.FocusControl("Clear");
                                current_tab = "";
                            }
                            selected_id = current_db_index;

                            current_database = DATABASE.DB_Info.DB_Database_INFO(current_db_index);
                            state = State.EDIT;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }

            /// <summary>
            /// This is the top menu bar styled menu with a toggle button to trigger these tabs.
            /// </summary>
            private void DisplayDBTabArea()
            {
                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));

                if (GUILayout.Toggle(current_tab == "Information", "Information", "button")) { current_tab = "Information"; }
                if (GUILayout.Toggle(current_tab == "Setup", "Setup", "button")) { current_tab = "Setup"; }
                if (GUILayout.Toggle(current_tab == "Options", "Options", "button")) { current_tab = "Options"; }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            /// <summary>
            /// This is the main area that has handling for switching between each of the tabs. 
            /// </summary>
            private void DisplayMainArea()
            {
                EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                EditorGUILayout.Space();
                switch (state)
                {
                    case State.EDIT:
                        DisplayDBTabArea();

                        switch (current_tab)
                        {
                            case "Information": MainTab(); break;
                            case "Setup": GUIConfigTab(); break;
                            case "Options": OptionsTab(); break;

                            default: current_tab = "Information"; MainTab(); break;
                        }
                        break;
                        //			default: MainTab(); break;	
                }
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
            } //END DisplayMainArea METHOD

            /// <summary>
            /// The main tab controls the main viewpoint for databases the first page when a database is selected etc.
            /// </summary>
            private void MainTab()
            {
                EditorGUIUtility.labelWidth = 105;
                current_database.NAME = EditorGUILayout.TextField("DATABASE NAME: ", current_database.NAME.MaxSize(40).NoSpecialCharacters());
                current_database.show_in_menu = EditorGUILayout.Toggle("SHOW IN MENU: ", current_database.show_in_menu);

                if (current_database.show_in_menu)
                {
                    current_database.show_in_menu_name = EditorGUILayout.TextField("MENU NAME: ", current_database.show_in_menu_name.MaxSize(25));
                    current_database.enable_shortcut = EditorGUILayout.Toggle("Enable Shortcut:", current_database.enable_shortcut);
                    if (current_database.enable_shortcut == true)
                    {
                        GUILayout.BeginHorizontal();
                        current_database.hotkey1 = (HOTKEY)EditorGUILayout.EnumPopup("Hotkey 1: ", current_database.hotkey1);
                        current_database.hotkey2 = (HOTKEY)EditorGUILayout.EnumPopup("Hotkey 2: ", current_database.hotkey2);
                        current_database.shortcut_letter = EditorGUILayout.TextField("Shortcut Key: ", current_database.shortcut_letter.MaxSize(1).TextOnly().ToLower());
                        GUILayout.EndHorizontal();
                        ENUM_VALIDATION.Validate_Menu_Shortcut_Key(current_database);
                    }
                }

                GUI_Generate_Button();

            } //END MainTab Method

            /// <summary>
            /// This is the config tab where you select and setup variables for a database.
            /// </summary>
            private void GUIConfigTab()
            {
                //TODO: Find a better way to display the variables list in the editor so it won't take so much screen space up.
                GUILayout.BeginHorizontal();
                //GUILayout.FlexibleSpace();
                //EditorStyles.largeLabel.fontSize = 14;
                //GUILayout.Label("The Variables that NEED TO ALWAYS be included are: " +
                //                "ID, " +
                //                "NAME\n" +
                //                "Just type them in the Name Field in CAPS where you want them!\n", EditorStyles.largeLabel);
                //GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Enable Tabs:", EditorStyles.label, GUILayout.Width(80));
                current_database.enable_tabs = EditorGUILayout.Toggle(current_database.enable_tabs);
                GUILayout.EndHorizontal();
                EditorStyles.largeLabel.fontSize = 14;


                EditorGUILayout.Space();

                if (current_database.enable_tabs == true)
                {
                    //					if(current_database.tabs_per_row < 1){current_database.tabs_per_row = 1;}
                    //					if(current_database.tabs_per_row > 10){current_database.tabs_per_row = 10;}
                    //					current_database.tabs_per_row = EditorGUILayout.IntField("Tabs Per Row (MAX: 10): ", current_database.tabs_per_row);

                    if (GUILayout.Button("Add Tab", GUILayout.Width(120)))
                    {
                        GUI.FocusControl("Clear");
                        current_database.tabs.Add(new DB_Tabs());
                    }

                    tab_scroll_position = EditorGUILayout.BeginScrollView(tab_scroll_position, "box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false));

                    //Iterate through tabs to display on screen
                    for (current_tab_index = 0; current_tab_index < current_database.tabs.Count; current_tab_index++)
                    {

                        GUILayout.Label("", "box", GUILayout.Height(10));
                        GUILayout.BeginHorizontal();

                        if (GUILayout.Button("DELETE TAB", GUILayout.Width(90)))
                        {
                            GUI.FocusControl("Clear");
                            current_database.tabs.Remove(current_database.tabs[current_tab_index]);
                            return;
                        }

                        GUILayout.Label("Tab Name:", GUILayout.Width(65));
                        current_database.tabs[current_tab_index].tab_name = EditorGUILayout.TextField(current_database.tabs[current_tab_index].tab_name.MaxSize(20));

                        if (GUILayout.Button("ADD VARIABLE", GUILayout.Width(120)))
                        {
                            GUI.FocusControl("Clear");

                            var tab_var = new DB_Variables();
                            current_database.tabs[current_tab_index].tab_variables.Add(tab_var);
                            current_database.variables.Add(tab_var);
                        }

                        GUILayout.EndHorizontal();
                        GUILayout.Space(10);

                        //Iterate through tab variables to display on screen
                        for (current_var_index = 0; current_var_index < current_database.tabs[current_tab_index].tab_variables.Count; current_var_index++)
                        {
                            GUILayout.BeginHorizontal();

                            if (GUILayout.Button("DEL", GUILayout.Width(30)))
                            {
                                GUI.FocusControl("Clear");
                                current_database.tabs[current_tab_index].tab_variables.Remove(current_database.tabs[current_tab_index].tab_variables[current_var_index]);
                                return;
                            }

                            GUILayout.Label("List Type:", GUILayout.Width(55));
                            current_database.tabs[current_tab_index].tab_variables[current_var_index].var_list_type = (LIST_TYPE)EditorGUILayout.EnumPopup(current_database.tabs[current_tab_index].tab_variables[current_var_index].var_list_type);

                            //							if (current_database.tabs[current_tab_index].tab_variables[current_var_index].var_list_type == LIST_TYPE.DICTIONARY
                            //							    || current_database.tabs[current_tab_index].tab_variables[current_var_index].var_list_type == LIST_TYPE.SORTED_DICTIONARY 
                            //							    || current_database.tabs[current_tab_index].tab_variables[current_var_index].var_list_type == LIST_TYPE.SORTED_LIST
                            //							    )
                            //								
                            //							{
                            //								
                            //								if (current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type == SELECTED_TYPE.NONE)
                            //								{
                            //									GUILayout.Label("CHOOSE TYPE:",GUILayout.Width(120));
                            //									current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type = (SELECTED_TYPE)EditorGUILayout.EnumPopup(current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type);
                            //								}
                            //								else
                            //								{
                            //									GUILayout.Label("Type:",GUILayout.Width(60));
                            //									if (current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type == SELECTED_TYPE.VALUE)
                            //									{
                            //										current_database.tabs[current_tab_index].tab_variables[current_var_index].variable_value_type = (VALUE_DATATYPE)EditorGUILayout.EnumPopup(current_database.tabs[current_tab_index].tab_variables[current_var_index].variable_value_type);
                            //									}
                            //									if (current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type == SELECTED_TYPE.REFERENCE)
                            //									{
                            //										current_database.tabs[current_tab_index].tab_variables[current_var_index].variable_reference_type = (REFERENCE_DATATYPE)EditorGUILayout.EnumPopup(current_database.tabs[current_tab_index].tab_variables[current_var_index].variable_reference_type);
                            //									}
                            //								}
                            //								
                            //								if (current_database.tabs[current_tab_index].tab_variables[current_var_index].second_selected_variable_type == SELECTED_TYPE.NONE)
                            //								{
                            //									GUILayout.Label("CHOOSE TYPE:",GUILayout.Width(120));
                            //									current_database.tabs[current_tab_index].tab_variables[current_var_index].second_selected_variable_type = (SELECTED_TYPE)EditorGUILayout.EnumPopup(current_database.tabs[current_tab_index].tab_variables[current_var_index].second_selected_variable_type);
                            //								}
                            //								else
                            //								{	
                            //									GUILayout.Label("Type:",GUILayout.Width(60));
                            //									if (current_database.tabs[current_tab_index].tab_variables[current_var_index].second_selected_variable_type == SELECTED_TYPE.VALUE)
                            //									{
                            //										current_database.tabs[current_tab_index].tab_variables[current_var_index].second_value_variable_type = (VALUE_DATATYPE)EditorGUILayout.EnumPopup(current_database.tabs[current_tab_index].tab_variables[current_var_index].second_value_variable_type);
                            //									}
                            //									if (current_database.tabs[current_tab_index].tab_variables[current_var_index].second_selected_variable_type == SELECTED_TYPE.REFERENCE)
                            //									{
                            //										current_database.tabs[current_tab_index].tab_variables[current_var_index].second_ref_variable_type = (REFERENCE_DATATYPE)EditorGUILayout.EnumPopup(current_database.tabs[current_tab_index].tab_variables[current_var_index].second_ref_variable_type);
                            //									}
                            //								}
                            //							}
                            if (current_database.tabs[current_tab_index].tab_variables[current_var_index].var_list_type == LIST_TYPE.ARRAY
                               || current_database.tabs[current_tab_index].tab_variables[current_var_index].var_list_type == LIST_TYPE.LIST
                               || current_database.tabs[current_tab_index].tab_variables[current_var_index].var_list_type == LIST_TYPE.NONE)
                            {

                                if (current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type == SELECTED_TYPE.NONE)
                                {
                                    GUILayout.Label("CHOOSE TYPE:", GUILayout.Width(120));

                                    current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type = (SELECTED_TYPE)EditorGUILayout.EnumPopup(current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type);
                                }
                                else
                                {
                                    GUILayout.Label("Type:", GUILayout.Width(40));

                                    if (current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type == SELECTED_TYPE.VALUE)
                                    {
                                        current_database.tabs[current_tab_index].tab_variables[current_var_index].variable_value_type = (VALUE_DATATYPE)EditorGUILayout.EnumPopup(current_database.tabs[current_tab_index].tab_variables[current_var_index].variable_value_type);
                                    }
                                    if (current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type == SELECTED_TYPE.REFERENCE)
                                    {
                                        current_database.tabs[current_tab_index].tab_variables[current_var_index].variable_reference_type = (REFERENCE_DATATYPE)EditorGUILayout.EnumPopup(current_database.tabs[current_tab_index].tab_variables[current_var_index].variable_reference_type);
                                    }
                                }
                            }

                            GUILayout.Label("Name:", GUILayout.Width(40));
                            current_database.tabs[current_tab_index].tab_variables[current_var_index].var_name = EditorGUILayout.TextField(current_database.tabs[current_tab_index].tab_variables[current_var_index].var_name.MaxSize(40).NoSpecialCharacters().ValidVarName());

                            //if (current_database.tabs[current_tab_index].tab_variables[current_var_index].var_name == "ID")
                            //{
                            //    current_database.tabs[current_tab_index].tab_variables[current_var_index].var_name = "ID";
                            //    current_database.tabs[current_tab_index].tab_variables[current_var_index].variable_value_type = VALUE_DATATYPE.INT;
                            //    current_database.tabs[current_tab_index].tab_variables[current_var_index].var_list_type = LIST_TYPE.NONE;
                            //    current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type = SELECTED_TYPE.VALUE;
                            //    current_database.tabs[current_tab_index].tab_variables[current_var_index].var_type = "int";
                            //}

                            //if (current_database.tabs[current_tab_index].tab_variables[current_var_index].var_name == "NAME")
                            //{
                            //    current_database.tabs[current_tab_index].tab_variables[current_var_index].var_name = "NAME";
                            //    current_database.tabs[current_tab_index].tab_variables[current_var_index].variable_value_type = VALUE_DATATYPE.STRING;
                            //    current_database.tabs[current_tab_index].tab_variables[current_var_index].var_list_type = LIST_TYPE.NONE;
                            //    current_database.tabs[current_tab_index].tab_variables[current_var_index].selected_variable_type = SELECTED_TYPE.VALUE;
                            //    current_database.tabs[current_tab_index].tab_variables[current_var_index].var_type = "string";
                            //}
                            ENUM_VALIDATION.Validate_Datatype(current_database,current_tab_index,current_var_index); // This checks the ENUM for the data types and assigns the proper value to be used for generation

                            GUILayout.EndHorizontal();
                            GUILayout.Space(5);
                        }
                    }

                    EditorGUILayout.EndScrollView();
                }

                if (current_database.enable_tabs == false)
                {


                    if (GUILayout.Button("Add Database Variable"))
                    {
                        GUI.FocusControl("Clear");
                        current_database.variables.Add(new DB_Variables());
                    }
                    var_scroll_position = EditorGUILayout.BeginScrollView(var_scroll_position, "box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false));


                    for (current_var_index = 0; current_var_index < current_database.variables.Count; current_var_index++)
                    {
                        GUILayout.BeginHorizontal();

                        if (GUILayout.Button("DEL", GUILayout.Width(30)))
                        {
                            GUI.FocusControl("Clear");
                            current_database.variables.Remove(current_database.variables[current_var_index]);
                            return;
                        }

                        GUILayout.Label("List Type:", GUILayout.Width(55));
                        current_database.variables[current_var_index].var_list_type = (LIST_TYPE)EditorGUILayout.EnumPopup(current_database.variables[current_var_index].var_list_type);

                        //						if (current_database.variables[current_var_index].var_list_type == LIST_TYPE.DICTIONARY
                        //						    || current_database.variables[current_var_index].var_list_type == LIST_TYPE.SORTED_DICTIONARY 
                        //						    || current_database.variables[current_var_index].var_list_type == LIST_TYPE.SORTED_LIST
                        //						    )
                        //						{
                        //							if (current_database.variables[current_var_index].selected_variable_type == SELECTED_TYPE.NONE)
                        //							{
                        //								GUILayout.Label("CHOOSE TYPE:",GUILayout.Width(120));
                        //								current_database.variables[current_var_index].selected_variable_type = (SELECTED_TYPE)EditorGUILayout.EnumPopup(current_database.variables[current_var_index].selected_variable_type);
                        //							}
                        //							else
                        //							{
                        //								GUILayout.Label("Type:",GUILayout.Width(60));
                        //								if (current_database.variables[current_var_index].selected_variable_type == SELECTED_TYPE.VALUE)
                        //								{
                        //									current_database.variables[current_var_index].variable_value_type = (VALUE_DATATYPE)EditorGUILayout.EnumPopup(current_database.variables[current_var_index].variable_value_type);
                        //								}
                        //								if (current_database.variables[current_var_index].selected_variable_type == SELECTED_TYPE.REFERENCE)
                        //								{
                        //									current_database.variables[current_var_index].variable_reference_type = (REFERENCE_DATATYPE)EditorGUILayout.EnumPopup(current_database.variables[current_var_index].variable_reference_type);
                        //								}
                        //							}
                        //							
                        //							if (current_database.variables[current_var_index].second_selected_variable_type == SELECTED_TYPE.NONE)
                        //							{
                        //								GUILayout.Label("CHOOSE TYPE:",GUILayout.Width(120));
                        //								current_database.variables[current_var_index].second_selected_variable_type = (SELECTED_TYPE)EditorGUILayout.EnumPopup(current_database.variables[current_var_index].second_selected_variable_type);
                        //							}
                        //							else
                        //							{	
                        //								GUILayout.Label("Type:",GUILayout.Width(60));
                        //								if (current_database.variables[current_var_index].second_selected_variable_type == SELECTED_TYPE.VALUE)
                        //								{
                        //									current_database.variables[current_var_index].second_value_variable_type = (VALUE_DATATYPE)EditorGUILayout.EnumPopup(current_database.variables[current_var_index].second_value_variable_type);
                        //								}
                        //								if (current_database.variables[current_var_index].second_selected_variable_type == SELECTED_TYPE.REFERENCE)
                        //								{
                        //									current_database.variables[current_var_index].second_ref_variable_type = (REFERENCE_DATATYPE)EditorGUILayout.EnumPopup(current_database.variables[current_var_index].second_ref_variable_type);
                        //								}
                        //							}
                        //						}
                        if (current_database.variables[current_var_index].var_list_type == LIST_TYPE.ARRAY
                           || current_database.variables[current_var_index].var_list_type == LIST_TYPE.LIST
                           || current_database.variables[current_var_index].var_list_type == LIST_TYPE.NONE)
                        {
                            if (current_database.variables[current_var_index].selected_variable_type == SELECTED_TYPE.NONE)
                            {
                                GUILayout.Label("CHOOSE TYPE:", GUILayout.Width(120));

                                current_database.variables[current_var_index].selected_variable_type = (SELECTED_TYPE)EditorGUILayout.EnumPopup(current_database.variables[current_var_index].selected_variable_type);
                            }
                            else
                            {
                                GUILayout.Label("Type:", GUILayout.Width(40));

                                if (current_database.variables[current_var_index].selected_variable_type == SELECTED_TYPE.VALUE)
                                {
                                    current_database.variables[current_var_index].variable_value_type = (VALUE_DATATYPE)EditorGUILayout.EnumPopup(current_database.variables[current_var_index].variable_value_type);
                                }
                                if (current_database.variables[current_var_index].selected_variable_type == SELECTED_TYPE.REFERENCE)
                                {
                                    current_database.variables[current_var_index].variable_reference_type = (REFERENCE_DATATYPE)EditorGUILayout.EnumPopup(current_database.variables[current_var_index].variable_reference_type);
                                }
                            }
                        }

                        GUILayout.Label("Name:", GUILayout.Width(40));
                        current_database.variables[current_var_index].var_name = EditorGUILayout.TextField(current_database.variables[current_var_index].var_name.MaxSize(40).NoSpecialCharacters().ValidVarName());

                        //if (current_database.variables[current_var_index].var_name == "ID")
                        //{
                        //    current_database.variables[current_var_index].var_name = "ID";
                        //    current_database.variables[current_var_index].variable_value_type = VALUE_DATATYPE.INT;
                        //    current_database.variables[current_var_index].var_list_type = LIST_TYPE.NONE;
                        //    current_database.variables[current_var_index].selected_variable_type = SELECTED_TYPE.VALUE;
                        //}

                        //if (current_database.variables[current_var_index].var_name == "NAME")
                        //{
                        //    current_database.variables[current_var_index].var_name = "NAME";
                        //    current_database.variables[current_var_index].variable_value_type = VALUE_DATATYPE.STRING;
                        //    current_database.variables[current_var_index].var_list_type = LIST_TYPE.NONE;
                        //    current_database.variables[current_var_index].selected_variable_type = SELECTED_TYPE.VALUE;

                        //}

                        ENUM_VALIDATION.Validate_Datatype(current_database,current_tab_index,current_var_index); // This checks the ENUM for the data types and assigns the proper value to be used for generation

                        GUILayout.EndHorizontal();
                        GUILayout.Space(5);
                    }

                    EditorGUILayout.EndScrollView();

                }
                GUI_Generate_Button();

            }

            /// <summary>
            /// This is the options tab that handles the options for each individual database.
            /// </summary>
            private void OptionsTab()
            {
                current_database.enable_custom_location = EditorGUILayout.Toggle("CUSTOM LOCATION:", current_database.enable_custom_location);
                current_database.enable_database_prefix = EditorGUILayout.Toggle("ENABLE PREFIX:", current_database.enable_database_prefix);
                current_database.enable_database_suffix = EditorGUILayout.Toggle("ENABLE SUFFIX:", current_database.enable_database_suffix);
                //				current_database.enable_custom_asset_type = EditorGUILayout.Toggle("CUSTOM TYPE:",current_database.enable_custom_asset_type);
                //				current_database.enable_custom_database_class = EditorGUILayout.Toggle("CUSTOM CLASS:",current_database.enable_custom_database_class);
                current_database.standalone = EditorGUILayout.Toggle("STANDALONE:", current_database.standalone);

                if (current_database.enable_custom_location)
                {
                    GUILayout.BeginHorizontal();
                    //if (GUILayout.Button("Database Location",EditorStyles.miniButton,GUILayout.Width(110)))
                    //{
                    //	GUI.FocusControl("Clear");
                    //	//current_database.database_location = EditorUtility.OpenFolderPanel("",DATABASE.Options.DB_Options().DEFAULT_DATABASE_DIRECTORY,"");
                    //	current_database.database_location = EditorUtility.SaveFolderPanel("",DATABASE.Options.DB_Options().DEFAULT_DATABASE_DIRECTORY, DATABASE.Options.DB_Options().DEFAULT_DATABASE_DIRECTORY);
                    //}

                    current_database.database_location = EditorGUILayout.TextField("Database Location:", current_database.database_location);
                    //Debug.Log("PERSISTANT:" + Application.persistentDataPath + "/Resources/" + current_database.database_location); 
                    //Debug.Log("StreamingAssets:" + Application.streamingAssetsPath + "/Resources/" + current_database.database_location); 
                    if (GUILayout.Button("Create Folder"))
                    {
                        //if (AssetDatabase.IsValidFolder("Resources/" + current_database.database_location) == false)
                        //{
                        //    //AssetDatabase.CreateFolder(Application.dataPath + "/Resources/", current_database.database_location);
                        //    AssetDatabase.CreateFolder("Resources/", current_database.database_location);
                        //    Debug.Log("FOLDER CREATED!");
                        //}
                        if (Directory.Exists(Application.dataPath + "/Resources/" + current_database.database_location) == false)
                        {
                            Debug.Log("Location Does not exist creating the director: " + Application.dataPath + "/Resources/" + current_database.database_location);
                            Directory.CreateDirectory(Application.dataPath + "/Resources/" + current_database.database_location);
                            Debug.Log("Location Created at: " + Application.dataPath + "/Resources/" + current_database.database_location);
                            AssetDatabase.Refresh();
                        }

                    }
                    GUILayout.EndHorizontal();

                }

                if (current_database.enable_database_prefix)
                {
                    current_database.database_prefix = EditorGUILayout.TextField("DATABASE PREFIX: ", current_database.database_prefix.NoSpecialCharacters());
                }

                if (current_database.enable_database_suffix)
                {
                    current_database.database_suffix = EditorGUILayout.TextField("DATABASE SUFFIX: ", current_database.database_suffix.NoSpecialCharacters());
                }

                if (current_database.enable_custom_asset_type)
                {
                    current_database.asset_type = EditorGUILayout.TextField("ASSET TYPE: ", current_database.asset_type.MaxSize(40));
                }

                if (current_database.enable_custom_database_class)
                {
                    current_database.database_class = EditorGUILayout.TextField("DATABASE HANDLER: ", current_database.database_class.NoSpecialCharacters().MaxSize(60));
                }

                GUI_Generate_Button();

            }

            /// <summary>
            /// The Generate Database Button placement and action.
            /// </summary>
            private void GUI_Generate_Button()
            {
                GUILayout.Space(20);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (current_database.SAVED == true)
                {
                    generate_button_name = "Modify Database";

                    if (GUILayout.Button(generate_button_name, GUILayout.Width(130)))
                    {
                        Generate_Database();
                    }
                }
                if (current_database.SAVED == false)
                {
                    generate_button_name = "Generate Database";

                    if (GUILayout.Button(generate_button_name, GUILayout.Width(130)))
                    {

                        Generate_Database();
                    }
                }
                GUILayout.FlexibleSpace();

                GUILayout.EndHorizontal();
            }

            /// <summary>
            /// This method is called when the Generate/Modify database button is pressed on the database. Handles error validation here first and then calls <see cref="MANAGE_DATABASE.Generate_All_Databases"/>
            /// </summary>
            private void Generate_Database()
            {
                current_database.dropdown_bool_list.Clear();

                if (current_database.enable_custom_asset_type == false)
                    current_database.asset_type = DATABASE.Options.DB_Options().DEFAULT_ASSET_TYPE;

                if (current_database.enable_custom_location == false)
                    current_database.database_location = DATABASE.Options.DB_Options().DEFAULT_DATABASE_DIRECTORY;

                if (Directory.Exists(Application.dataPath + "/Resources/" + current_database.database_location) == false)
                {
                    Debug.Log("Location Does not exist creating the director: " + Application.dataPath + "/Resources/" + current_database.database_location);
                    Directory.CreateDirectory(Application.dataPath + "/Resources/" + current_database.database_location);
                    Debug.Log("Location Created at: " + Application.dataPath + "/Resources/" + current_database.database_location);
                }

                //Prefix, suffix and custom class handling
                if (current_database.enable_database_prefix == false)
                    current_database.database_prefix = DATABASE.Options.DB_Options().DEFAULT_DATABASE_PREFIX;

                if (current_database.enable_database_suffix == false)
                    current_database.database_suffix = DATABASE.Options.DB_Options().DEFAULT_DATABASE_SUFFIX;

                if (current_database.enable_custom_database_class == false)
                    current_database.database_class = DATABASE.Options.DB_Options().DEFAULT_DATABASE_CLASS;

                //Make sure everything is good to go
                if (!ValidateDatabase() || !ValidateVars())
                    return;

                if (EditorUtility.DisplayDialog("Database Created!!", "Database Created at: " + current_database.database_location, "Okay"))
                {
                    GUI.SetNextControlName("Clear");
                    state = State.BLANK;
                    DATABASE_GUI.CLOSE();
                    MANAGE_DATABASE.Generate_All_Databases(current_database);
                }

            } //END Generate_Database METHOD

            /// <summary>
            /// Handles error checking for the DB itself making sure no duplicates/errors occur from user input.
            /// </summary>
            /// <returns></returns>
            private bool ValidateDatabase()
            {
                if (current_database.NAME == "")
                    if (EditorUtility.DisplayDialog("Name the Database!", "Unable to create the Database without a name.", "Okay"))
                        return false;

                //				if (current_database.variables.GroupBy(x => x.var_name).Any(grp => grp.Count() > 1))
                //				list_of_names.Clear();

                //Check for DB name duplicates
                for (int i = 0; i < DATABASE.DB_Info.COUNT; i++)
                    for (int j = i + 1; j < DATABASE.DB_Info.COUNT; j++)
                        if (DATABASE.DB_Info.DB_Database_INFO(i).NAME.ToUpper() == DATABASE.DB_Info.DB_Database_INFO(j).NAME.ToUpper() &&
                            EditorUtility.DisplayDialog("DUPLICATE DATABASE NAME DETECTED!!", "Please rename your DATABASE to ensure that it is named UNIQUE, you can only have 1 per EXACT name. You can use combinations of Lowercase and Uppercase letters if you want but no databases can be the exact same name", "Okay"))
                            return false;

                if (current_database.enable_custom_asset_type == true && current_database.asset_type == "")
                    if (EditorUtility.DisplayDialog("Database MUST have a file type!!", "Unable to create the Database without a type.", "Okay"))
                        return false;

                if (current_database.enable_custom_database_class == true && current_database.database_class == "")
                    if (EditorUtility.DisplayDialog("Database MUST have a DATABASE CLASS!!", "Unable to create the Database without a DATABASE CLASS.", "Okay"))
                        return false;

                if (current_database.database_location == "")
                    if (EditorUtility.DisplayDialog("Database MUST have a Location!", "Unable to create the Database without a Location!.", "Okay"))
                        return false;

                if (current_database.show_in_menu == true && current_database.show_in_menu_name == "")
                    if (EditorUtility.DisplayDialog("Database MUST have a Menu name when Menu is Enabled!", "Unable to create the Database without a Menu Name!.", "Okay"))
                        return false;

                if (current_database.show_in_menu == true && current_database.enable_shortcut == true && current_database.hotkey1 == current_database.hotkey2)
                    if (EditorUtility.DisplayDialog("Menu shortcut Cannot have the same 2 hot-keys!!", "Unable to create the Database while the Menu hot-keys are the same please change them and ensure they are different.!.", "Okay"))
                        return false;

                if (current_database.show_in_menu == true && current_database.enable_shortcut == true && current_database.shortcut_key == "")
                    if (EditorUtility.DisplayDialog("Menu shortcut cannot be BLANK!", "Please enter a Letter for your Shortcut Key!", "Okay"))
                        return false;

                return true;
            }

            /// <summary>
            /// Does error checking for variables in the DB before generation.
            /// </summary>
            /// <returns></returns>
            private bool ValidateVars()
            {
                //Add vars to vars list for tabs
                if (current_database.enable_tabs)
                {
                    foreach (var item in current_database.tabs)
                    {
                        for (int i = 0; i < item.tab_variables.Count; i++)
                        {
                            if (item.tab_variables[i].var_list_type == LIST_TYPE.ARRAY || item.tab_variables[i].var_list_type == LIST_TYPE.LIST)
                                current_database.dropdown_bool_list.Add(false);

                            current_database.variables.Add(item.tab_variables[i]);
                        }
                    }
                }

                //No tabs
                else
                {
                    for (int i = 0; i < current_database.variables.Count; i++)
                        if (current_database.variables[i].var_list_type == LIST_TYPE.ARRAY || current_database.variables[i].var_list_type == LIST_TYPE.LIST)
                            current_database.dropdown_bool_list.Add(false);
                }

                ////Error checking code
                //if (!current_database.variables.Any(v => v.var_name == "ID"))
                //    if (EditorUtility.DisplayDialog("Database MUST have ID Variable!!", "The Database MUST have ID Variable, with the type of INT", "Okay"))
                //        return false;

                //if (!current_database.variables.Any(v => v.var_name == "NAME"))
                //    if (EditorUtility.DisplayDialog("Database MUST have NAME Variable!!", "The Database MUST have NAME Variable, with the type of STRING", "Okay"))
                //        return false;

                //If the variable name is a C# keyword
                for (int i = 0; i < current_database.variables.Count; i++)
                {
                    if (csharpReservedNames.Contains(current_database.variables[i].var_name)
                        && EditorUtility.DisplayDialog("Invalid Variable Name", "The " + current_database.variables[i].var_type.ToUpper() + " variable called '" +
                        current_database.variables[i].var_name + "' is invalid because this name is a reserved keyword by C#", "OK"))
                        return false;

                    else if (MDFReservedNames.Contains(current_database.variables[i].var_name)
                        && EditorUtility.DisplayDialog("Invalid Variable Name", "The " + current_database.variables[i].var_type.ToUpper() + " variable called '" +
                        current_database.variables[i].var_name + "' is invalid because this name is reserved by the MDF asset", "OK"))
                        return false;

                    else if (unityReservedNames.Contains(current_database.variables[i].var_name)
                        && EditorUtility.DisplayDialog("Invalid Variable Name", "The " + current_database.variables[i].var_type.ToUpper() + " variable called '" +
                        current_database.variables[i].var_name + "' is invalid because this name is reserved by Unity", "OK"))
                        return false;
                }

                if (current_database.variables.Any(v => v.var_name == ""))
                    if (EditorUtility.DisplayDialog("BLANK VARIABLE NAME DETECTED!!", "You must have a names for all your database variables!", "Okay"))
                        return false;

                if (current_database.variables.GroupBy(x => x.var_name).Any(grp => grp.Count() > 1))
                    if (EditorUtility.DisplayDialog("DUPLICATE VARIABLE DETECTED!!", "Please rename your variables to ensure that they are all named UNIQUE, you can only have 1 per EXACT name. You can use combinations of Lowercase and Uppercase letters if you want but no variable can be the exact same PER database", "Okay"))
                        return false;

                if (current_database.variables.Any(x => x.var_type == ""))
                    if (EditorUtility.DisplayDialog("NO VARIABLE TYPE SELECTED", "Please make sure all variables have a TYPE selected.", "Okay"))
                        return false;

                //TODO: Support for more list types
                //					if (current_database.tabs.Any(x => x.tab_variables.Any(y => y.var_second_type == "" && y.var_list_type == LIST_TYPE.DICTIONARY || y.var_list_type == LIST_TYPE.SORTED_DICTIONARY || y.var_list_type == LIST_TYPE.SORTED_LIST)))
                //					{
                //						if (EditorUtility.DisplayDialog("NO VARIABLE TYPE SELECTED", "Please select a variable type for your variables!", "Okay"))
                //						{
                //							return;
                //						}
                //					}

                return true;
            }
        }
    }
}