using MDF_EDITOR.DATABASE; // THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK 
using UnityEngine; 
using UnityEditor; 
using System.Collections; 
using System.Collections.Generic; 
 
namespace MDF_EDITOR 
{ 
	 namespace DATABASE 
	 { 
		 public class DB_Prefabs_GUI: EditorWindow 
		 { 
			 private Vector2 Skeleton_List_Index; 
			 private List<bool> foldout_expanded = new List<bool>{false}; 
			 private int current_index; 
			 private static DB_Prefabs current_Prefabs; 
			 private static DB_Prefabs_GUI _DB_Prefabs_GUI; 
			 private const string menu_shortcut = ""; 
			 private const string menu_name = "MDF EDITOR/" + "" + " " + menu_shortcut; 
			 private const bool show_menu  = false; 
			 private static string custom_skin_path = "Assets/MDF EDITOR/FRAMEWORK/Skin/DatabaseGUISkin.guiskin"; 
			 private static GUISkin custom_skin; 
			 private Vector2 database_scroll_position; 
			 private int selected_id; 
			 private State state; 
			 private enum State {BLANK, EDIT} 
 
			 [MenuItem(menu_name, !show_menu)] 
			 public static void Init() 
			 { 
				  _DB_Prefabs_GUI = GetWindow<DB_Prefabs_GUI>(); 
				  _DB_Prefabs_GUI.minSize = new Vector2(1100, 400); 
				  _DB_Prefabs_GUI.Show(); 
			 } 
 
			 public void OnGUI() 
			 { 
				 if( custom_skin == null ){ custom_skin = (GUISkin)(AssetDatabase.LoadAssetAtPath(custom_skin_path, typeof(GUISkin))); } GUI.skin = custom_skin; 
				 EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)); 
				 DisplayListArea(); 
				 DisplayMainArea(); 
				 EditorGUILayout.EndHorizontal(); 
			 } 
 
			 private void DisplayListArea() 
			 { 
				 EditorGUILayout.BeginVertical(GUILayout.Width(400)); 
				 EditorGUILayout.Space(); 
				 database_scroll_position = EditorGUILayout.BeginScrollView(database_scroll_position,"box",GUILayout.ExpandHeight(true)); 
				 GUILayout.BeginHorizontal(GUILayout.MaxWidth(250)); 
				 EditorStyles.toolbar.alignment = TextAnchor.MiddleLeft; 
				 GUILayout.Label("Delete",EditorStyles.toolbar,GUILayout.Width(45)); 
				 EditorStyles.toolbar.alignment = TextAnchor.MiddleCenter; 
				 GUILayout.Label("ID",EditorStyles.toolbar,GUILayout.Width(25)); 
				 GUILayout.Label("Prefabs" + " Name",EditorStyles.toolbar,GUILayout.Width(322)); 
				 GUILayout.EndHorizontal(); 
 
				 Populate_List_Area(); // This populates the database fields to appear in the list 
 
				 EditorGUILayout.EndScrollView(); 
				 EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)); 
				 EditorGUILayout.LabelField("Total: " + ": " + DATABASE.Prefabs.COUNT, GUILayout.Width(100)); 
 
				 if (GUILayout.Button("New " + "Prefabs")) 
				 { 
					 GUI.FocusControl("Clear"); 
					 DATABASE.Prefabs.ADD(new DB_Prefabs("",new GameObject[0])); 
				 current_index = DATABASE.Prefabs.COUNT - 1; 
				 selected_id = current_index;
				 current_Prefabs = DATABASE.Prefabs[current_index]; 
				 state = State.EDIT; 
			 } 
 
			 EditorGUILayout.EndHorizontal(); 
			 EditorGUILayout.Space(); 
			 EditorGUILayout.EndVertical(); 
			 } ////END DisplayListArea METHOD 
 
			 private void Populate_List_Area() 
			 { 
				 if (DATABASE.Prefabs.COUNT > 0) 
				 { 
					 for (current_index = 0; current_index < DATABASE.Prefabs.COUNT; current_index++) 
					 { 
						EditorGUILayout.BeginHorizontal(); 
						if (GUILayout.Button(new GUIContent("▲", "Moves this entry up"), EditorStyles.miniButtonLeft, GUILayout.Width(35)))
							if (DATABASE.Prefabs.MoveUp(current_index))
							{
								selected_id = selected_id == current_index ? selected_id - 1 : selected_id;
								current_index--;
							}

						if (GUILayout.Button(new GUIContent("▼", "Moves this entry down"), EditorStyles.miniButtonRight, GUILayout.Width(35)))
							if (DATABASE.Prefabs.MoveDown(current_index))
							{
								selected_id = selected_id == current_index ? selected_id + 1 : selected_id;
								current_index++;
							}

						 if(DATABASE.Prefabs[current_index] == null )
 							 continue;

						 DATABASE.Prefabs[current_index].ID = current_index; 
						 if (GUILayout.Button("DEL", GUILayout.Width (35))) 
						 { 
							 if (DATABASE.Options.DB_Options().SHOW_DELETE_POPUP == true) 
							 { 
								 if (EditorUtility.DisplayDialog("Remove " + DATABASE.Prefabs[current_index].NAME + "?","This will remove " + DATABASE.Prefabs[current_index].NAME + " from the DATABASE","Delete","Keep")) 
								 { 
									 GUI.FocusControl("Clear"); 
									 DATABASE.Prefabs.REMOVE_AT(current_index); 
									 state = State.BLANK; 
									 return; 
								 } 
							 } 
 
							 if (DATABASE.Options.DB_Options().SHOW_DELETE_POPUP == false) 
							 { 
								 GUI.FocusControl("Clear"); 
								 DATABASE.Prefabs.REMOVE_AT(current_index); 
								 state = State.BLANK; 
								 return; 
							 } 
						 } 
 
						 GUILayout.Label(DATABASE.Prefabs[current_index].ID.ToString(),"box",GUILayout.Width(37)); 
						 if (GUILayout.Toggle(selected_id == current_index,DATABASE.Prefabs[current_index].NAME,"box",GUILayout.ExpandWidth(true))) 
						 { 
							 if (selected_id != current_index) 
							 { 
								 GUI.FocusControl("Clear"); 
							 } 
							 selected_id = current_index; 
							 current_Prefabs = DATABASE.Prefabs[current_index]; 
							 state = State.EDIT; 
						 } 
						 EditorGUILayout.EndHorizontal(); 
					 } 
 
				 } 
 
			 } 
 
			 private void DisplayMainArea() 
			 { 
				 EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true)); 
				 EditorGUILayout.Space(); 
				 switch(state) 
				 { 
					 case State.EDIT: 
						 MainArea(); 
							 break; 
				 } 
				 EditorGUILayout.Space(); 
				 EditorGUILayout.EndVertical(); 
			 } 
 
			 private void MainArea() 
			 { 
				 GUILayout.Label(" ID:" + current_Prefabs.ID ,GUILayout.Width(50)); 
				current_Prefabs.NAME = EditorGUILayout.TextField("NAME: ", current_Prefabs.NAME); 
				 foldout_expanded[0] = EditorGUILayout.Foldout(foldout_expanded[0],"Skeleton"); 
				 if (foldout_expanded[0] == true) 
				 { 
					 EditorGUILayout.BeginVertical(); 
					 GUILayout.BeginHorizontal(); 
					 EditorStyles.toolbar.alignment = TextAnchor.MiddleLeft; 
					 GUILayout.Label("Delete",EditorStyles.toolbar,GUILayout.Width(45)); 
					 EditorStyles.toolbar.alignment = TextAnchor.MiddleCenter; 
					 GUILayout.Label("Index",EditorStyles.toolbar,GUILayout.Width(45)); 
					 GUILayout.Label("GameObject",EditorStyles.toolbar); 
					 GUILayout.Label("",EditorStyles.toolbar); 
					 GUILayout.EndHorizontal(); 
					 Skeleton_List_Index = EditorGUILayout.BeginScrollView(Skeleton_List_Index,"box",GUILayout.MaxHeight(200),GUILayout.Width(350)); 
					 for(int i = 0; i < current_Prefabs.Skeleton.Length; i++) 
					 { 
						 GUILayout.Space(2.5f); 
						 EditorGUILayout.BeginHorizontal(); 

						 if(GUILayout.Button("DEL",EditorStyles.toolbarButton,GUILayout.Width(45))) 
						 { 
						 GUI.FocusControl("Clear"); 
						 ArrayUtility.RemoveAt(ref current_Prefabs.Skeleton, i); 
						 return; 
					 } 
					 GUILayout.Space(5.0f); 
					 GUILayout.Label(i.ToString(),EditorStyles.miniButtonMid, GUILayout.Width(35)); 
					 current_Prefabs.Skeleton[i] = (GameObject)EditorGUILayout.ObjectField(new GUIContent(""), current_Prefabs.Skeleton[i], typeof(GameObject) , true, GUILayout.Width(238));					 EditorGUILayout.EndHorizontal(); 
					 } 
					 EditorGUILayout.EndScrollView(); 
					 if(GUILayout.Button("New","box", GUILayout.Width(350))) 
					 { 
						 ArrayUtility.Add(ref current_Prefabs.Skeleton,null); 
					 } 
					 EditorGUILayout.EndVertical(); 
					 EditorGUILayout.Separator(); 
				 } 
 
			 } 
		 } 
	 } 
} 
