
//using MDF_EDITOR.DATABASE;
//using MDF_EDITOR;
//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Collections.Generic;

//using System;

//namespace MDF_EDITOR {

//public class Database_Dropdown_Handler {
////public struct Database_Dropdown_Handler {

//	//Access Variables to other databases attached to datbase the database object.
//	private Vector2 _scrollSelectPos;

//	private bool dropdown_select;
//	private bool class_dropdown_db_select;
//	private bool quest_dropdown_db_select;
//	private string button_name = "None";


////		public void ShowListDropdown<SUPER_T>(SUPER_T _LIST_OF_STUFF){
////
////			if (_LIST_OF_STUFF.
////			{
////				Debug.Log("Taco Bell was called!");
////			}

////			SortedList<int,string> tacos;
////		
////			tacos = (SortedList<int,string>)Convert.ChangeType(_LIST_OF_STUFF,typeof(SortedList<int,string>));
////			Debug.Log(tacos.Count);
//			//				ACTOR_DB_HANDLER = (Actor_DB_Handler)Convert.ChangeType(DB_HANDLER,typeof(Actor_DB_Handler));


////			Debug.Log(_LIST_OF_STUFF.Count);
////		}

////		public void ShowListDropdown<List>(List _LIST_OF_STUFF) where List : List{
////			Debug.Log(_LIST_OF_STUFF.
////		}

//		public void ShowDatabaseDropdown(DB_Database_INFO DATABASE_OBJECT){
//			Debug.Log(DATABASE_OBJECT.NAME);

////			if (DATABASE_OBJECT.NAME == "Weapon")
////			{
////				float min_width = 200.0f;
//				EditorGUILayout.BeginVertical();

//				if(GUILayout.Button("Choose: " + button_name))
//				{
//					dropdown_select = !dropdown_select;
//				}

//				if (dropdown_select == true)
//				{
//					EditorGUILayout.BeginVertical();
//					_scrollSelectPos = EditorGUILayout.BeginScrollView(_scrollSelectPos, "box",GUILayout.ExpandWidth(true),GUILayout.ExpandHeight(false));
										
//					if(GUILayout.Button("None","box"))
//					{
//						//							DATABASE_HANDLER.QUEST_DB_HANDLER.DB_Quest(MAIN_DB_GUI_HANDLER.CURRENT_INDEX).attached_actor_database = DATABASE_HANDLER.DEFAULT_ACTOR;
//						//							DATABASE_HANDLER.QUEST_DB_HANDLER.DB_Quest(MAIN_DB_GUI_HANDLER.CURRENT_INDEX).attached_actor_database.NAME = "None";
//						button_name = "None";
//						dropdown_select = false;
//					}
					
////					for(int i = 0; i < DATABASE.DATABASE.Weapon.COUNT; i++)
////					{
////						
////						if(GUILayout.Button(DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME,"box",GUILayout.MinWidth(min_width)))
////						{
////							//								DATABASE_HANDLER.QUEST_DB_HANDLER.DB_Quest(MAIN_DB_GUI_HANDLER.CURRENT_INDEX).attached_actor_database = DATABASE_HANDLER.ACTOR_DB_HANDLER.DB_Actor(i);
////							//								DATABASE_HANDLER.QUEST_DB_HANDLER.DB_Quest(MAIN_DB_GUI_HANDLER.CURRENT_INDEX).attached_actor_database.NAME = DATABASE_HANDLER.ACTOR_DB_HANDLER.DB_Actor(i).NAME;
////						    button_name = DATABASE.DATABASE.DB_Info.DB_Database_INFO(i).NAME;
////							dropdown_select = false;
////						}
////					}
					
//					EditorGUILayout.EndScrollView();
//					EditorGUILayout.EndVertical();
//				}

//				EditorGUILayout.EndVertical();
////			}

//		} // END ShowDatabaseDropdown Method


//		public void List_Field()
//		{

//		}

//	} // END Database_Dropdown_Handler CLASS
//} // END MDF_DATABASE NAMESPACE


