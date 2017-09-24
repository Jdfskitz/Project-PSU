using MDF_EDITOR.DATABASE;
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace MDF_EDITOR
{
    /// <summary>
    /// This class handles validation for setting the values for <see cref="DB_Database_INFO"/> fields.
    /// </summary>
	public static class ENUM_VALIDATION
	{
        /// <summary>
        /// This sets the shortcut key accordingly based on unity bound symbols.
        /// </summary>
		public static void Validate_Menu_Shortcut_Key(DB_Database_INFO _database_info)
		{
			string key1 = "_";
			string key2 = "_";
            

			switch(_database_info.hotkey1)
			{
			case HOTKEY.NONE: key1 = "_"; break;
			case HOTKEY.CTRL_or_CMD: key1 = "%"; break;
			case HOTKEY.SHIFT: key1 = "#"; break;
			case HOTKEY.ALT: key1 = "&"; break;
//			case HOTKEY.HOME: key1 = "HOME"; break;
//			case HOTKEY.END: key1 = "END"; break;
//			case HOTKEY.PAGE_UP: key1 = "PGUP"; break;
//			case HOTKEY.PAGE_DOWN: key1 = "PGDN"; break;
//			case HOTKEY.UP: key1 = "UP"; break;
//			case HOTKEY.DOWN: key1 = "DOWN"; break;
//			case HOTKEY.LEFT: key1 = "LEFT"; break;
//			case HOTKEY.RIGHT: key1 = "RIGHT"; break;
//			case HOTKEY.F1: key1 = "F1"; break;
//			case HOTKEY.F2: key1 = "F2"; break;
//			case HOTKEY.F3: key1 = "F3"; break;
//			case HOTKEY.F4: key1 = "F4"; break;
//			case HOTKEY.F5: key1 = "F5"; break;
//			case HOTKEY.F6: key1 = "F6"; break;
//			case HOTKEY.F7: key1 = "F7"; break;
//			case HOTKEY.F8: key1 = "F8"; break;
//			case HOTKEY.F9: key1 = "F9"; break;
//			case HOTKEY.F10: key1 = "F10"; break;
//			case HOTKEY.F11: key1 = "F11"; break;
//			case HOTKEY.F12: key1 = "F12"; break;
			}

			switch(_database_info.hotkey2)
			{
			case HOTKEY.NONE: key2 = "_"; break;
			case HOTKEY.CTRL_or_CMD: key2 = "%"; break;
			case HOTKEY.SHIFT: key2 = "#"; break;
			case HOTKEY.ALT: key2 = "&"; break;
//			case HOTKEY.HOME: key2 = "HOME"; break;
//			case HOTKEY.END: key2 = "END"; break;
//			case HOTKEY.PAGE_UP: key2 = "PGUP"; break;
//			case HOTKEY.PAGE_DOWN: key2 = "PGDN"; break;
//			case HOTKEY.UP: key2 = "UP"; break;
//			case HOTKEY.DOWN: key2 = "DOWN"; break;
//			case HOTKEY.LEFT: key2 = "LEFT"; break;
//			case HOTKEY.RIGHT: key2 = "RIGHT"; break;
//			case HOTKEY.F1: key2 = "F1"; break;
//			case HOTKEY.F2: key2 = "F2"; break;
//			case HOTKEY.F3: key2 = "F3"; break;
//			case HOTKEY.F4: key2 = "F4"; break;
//			case HOTKEY.F5: key2 = "F5"; break;
//			case HOTKEY.F6: key2 = "F6"; break;
//			case HOTKEY.F7: key2 = "F7"; break;
//			case HOTKEY.F8: key2 = "F8"; break;
//			case HOTKEY.F9: key2 = "F9"; break;
//			case HOTKEY.F10: key2 = "F10"; break;
//			case HOTKEY.F11: key2 = "F11"; break;
//			case HOTKEY.F12: key2 = "F12"; break;
			}

			_database_info.shortcut_key = key1 + key2 + _database_info.shortcut_letter;
		}

        /// <summary>
        /// This validates the data types info the string form for each of the variables.
        /// </summary>
		public static void Validate_Datatype(DB_Database_INFO _database_info, int _tab_index, int _var_index)
		{

			if (_database_info.enable_tabs == true)
			{
				
				if(_database_info.tabs[_tab_index].tab_variables[_var_index].var_list_type == LIST_TYPE.NONE 
				   ||_database_info.tabs[_tab_index].tab_variables[_var_index].var_list_type == LIST_TYPE.ARRAY
				   ||_database_info.tabs[_tab_index].tab_variables[_var_index].var_list_type == LIST_TYPE.LIST)
					
				{
//					_database_info.tabs[_tab_index].tab_variables[_var_index].second_value_variable_type = VALUE_DATATYPE.NONE;
//					_database_info.tabs[_tab_index].tab_variables[_var_index].second_ref_variable_type = REFERENCE_DATATYPE.NONE;
				}
				
				if (_database_info.tabs[_tab_index].tab_variables[_var_index].selected_variable_type == SELECTED_TYPE.VALUE)
				{
					switch (_database_info.tabs[_tab_index].tab_variables[_var_index].variable_value_type)
					{
					    case VALUE_DATATYPE.NONE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = ""; break;
					    case VALUE_DATATYPE.STRING: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "string"; break;
    //					case VALUE_DATATYPE.CHAR: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "char"; break;
				        case VALUE_DATATYPE.INT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "int"; break;
                        case VALUE_DATATYPE.LONG: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "long"; break;
                        case VALUE_DATATYPE.DOUBLE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "double"; break;
					    case VALUE_DATATYPE.FLOAT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "float"; break;
    //					case VALUE_DATATYPE.SHORT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "short"; break;
    //					case VALUE_DATATYPE.BYTE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "byte"; break;
					    case VALUE_DATATYPE.BOOL: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "bool"; break;
					    case VALUE_DATATYPE.VECTOR2: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Vector2"; break;
					    case VALUE_DATATYPE.VECTOR3: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Vector3"; break;
					    case VALUE_DATATYPE.VECTOR4: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Vector4"; break;
    //					case VALUE_DATATYPE.QUATERNION: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Quaternion"; break;
    //					case VALUE_DATATYPE.RAY: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Ray"; break;
    //					case VALUE_DATATYPE.RAY2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Ray2D"; break;
					    case VALUE_DATATYPE.RECT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Rect"; break;
					    case VALUE_DATATYPE.COLOR: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Color"; break;
					    case VALUE_DATATYPE.COLOR32: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Color32"; break;
					}
				}
				
//				if (_database_info.tabs[_tab_index].tab_variables[_var_index].second_selected_variable_type == SELECTED_TYPE.VALUE)
//				{
//					switch (_database_info.tabs[_tab_index].tab_variables[_var_index].second_value_variable_type)
//					{
//					case VALUE_DATATYPE.NONE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = ""; break;
//					case VALUE_DATATYPE.STRING: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "string"; break;
////					case VALUE_DATATYPE.CHAR: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "char"; break;
//					case VALUE_DATATYPE.INT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "int"; break;
////					case VALUE_DATATYPE.LONG: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "long"; break;
//					case VALUE_DATATYPE.DOUBLE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "double"; break;
//					case VALUE_DATATYPE.FLOAT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "float"; break;
////					case VALUE_DATATYPE.SHORT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "short"; break;
////					case VALUE_DATATYPE.BYTE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "byte"; break;
//					case VALUE_DATATYPE.BOOL: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "bool"; break;
//					case VALUE_DATATYPE.VECTOR2: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Vector2"; break;
//					case VALUE_DATATYPE.VECTOR3: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Vector3"; break;
//					case VALUE_DATATYPE.VECTOR4: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Vector4"; break;
////					case VALUE_DATATYPE.QUATERNION: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Quaternion"; break;
////					case VALUE_DATATYPE.RAY: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Ray"; break;
////					case VALUE_DATATYPE.RAY2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Ray2D"; break;
//					case VALUE_DATATYPE.RECT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Rect"; break;
//					case VALUE_DATATYPE.COLOR: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Color"; break;
//					case VALUE_DATATYPE.COLOR32: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Color32"; break;
//					}
//				}
				
				if (_database_info.tabs[_tab_index].tab_variables[_var_index].selected_variable_type == SELECTED_TYPE.REFERENCE)
				{
					switch (_database_info.tabs[_tab_index].tab_variables[_var_index].variable_reference_type)
					{
					    case REFERENCE_DATATYPE.NONE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = ""; break;
					    case REFERENCE_DATATYPE.GAMEOBJECT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "GameObject"; break;
					    case REFERENCE_DATATYPE.TRANSFORM: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Transform"; break;
					    case REFERENCE_DATATYPE.OBJECT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Object"; break;
					    case REFERENCE_DATATYPE.CAMERA: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Camera"; break;
					    case REFERENCE_DATATYPE.LIGHT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Light"; break;
					    case REFERENCE_DATATYPE.COMPONENT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Component"; break;
					    case REFERENCE_DATATYPE.SCRIPTABLE_OBJECT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "ScriptableObject"; break;
					    case REFERENCE_DATATYPE.AVATAR: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Avatar"; break;
					    case REFERENCE_DATATYPE.CHARACTER_CONTROLLER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "CharacterController"; break;
					    case REFERENCE_DATATYPE.ANIMATION: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Animation"; break;
					    case REFERENCE_DATATYPE.ANIMATION_CLIP: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "AnimationClip"; break;
					    case REFERENCE_DATATYPE.ANIMATIOR: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Animator"; break;
					    case REFERENCE_DATATYPE.AUDIO_CLIP: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "AudioClip"; break;
					    case REFERENCE_DATATYPE.AUDIO_SOURCE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "AudioSource"; break;
					    case REFERENCE_DATATYPE.AUDIO_LISTENER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "AudioListener"; break;
					    case REFERENCE_DATATYPE.COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Collider"; break;
					    case REFERENCE_DATATYPE.COLLIDER_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Collider2D"; break;
					    case REFERENCE_DATATYPE.BOX_COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "BoxCollider"; break;
					    case REFERENCE_DATATYPE.BOX_COLLIDER_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "BoxCollider2D"; break;
					    case REFERENCE_DATATYPE.CAPSULE_COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "CapsuleCollider"; break;
					    case REFERENCE_DATATYPE.EDGE_COLLIDER_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "EdgeCollider2D"; break;
					    case REFERENCE_DATATYPE.POLYGON_COLLIDER2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "PolygonCollider2D"; break;
					    case REFERENCE_DATATYPE.MESH_COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "MeshCollider"; break;
					    case REFERENCE_DATATYPE.TERRIAN_COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "TerrainCollider"; break;
					    case REFERENCE_DATATYPE.WHEEL_COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "WheelCollider"; break;
    //					case REFERENCE_DATATYPE.COLLISION: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Collision"; break;
    //					case REFERENCE_DATATYPE.COLLISION_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Collision2D"; break;
					    case REFERENCE_DATATYPE.PHYSIC_MATERIAL: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "PhysicMaterial"; break;
					    case REFERENCE_DATATYPE.PHYSICS_MATERIAL_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "PhysicsMaterial2D"; break;
    //					case REFERENCE_DATATYPE.PHYSICS: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Physics"; break;
    //					case REFERENCE_DATATYPE.PHYSICS_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Physics2D"; break;
					    case REFERENCE_DATATYPE.RIGIDBODY: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Rigidbody"; break;
					    case REFERENCE_DATATYPE.RIGIDBODY_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Rigidbody2D"; break;
					    case REFERENCE_DATATYPE.EFFECTOR_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Effector2D"; break;
					    case REFERENCE_DATATYPE.PLATFORM_EFFECTOR_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "PlatformEffector2D"; break;
					    case REFERENCE_DATATYPE.SURFACE_EFFECTOR_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "SurfaceEffector2D"; break;
					    case REFERENCE_DATATYPE.AREA_EFFECTOR_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "AreaEffector2D"; break;
					    case REFERENCE_DATATYPE.POINT_EFFECTOR_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "PointEffector2D"; break;
					    case REFERENCE_DATATYPE.FONT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Font"; break;
					    case REFERENCE_DATATYPE.SPRITE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Sprite"; break;
					    case REFERENCE_DATATYPE.SPRITE_RENDERER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "SpriteRenderer"; break;
					    case REFERENCE_DATATYPE.MESH: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Mesh"; break;
					    case REFERENCE_DATATYPE.CUBEMAP: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Cubemap"; break;
					    case REFERENCE_DATATYPE.MATERIAL: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Material"; break;
					    case REFERENCE_DATATYPE.TEXTURE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Texture"; break;
					    case REFERENCE_DATATYPE.TEXTURE_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Texture2D"; break;
					    case REFERENCE_DATATYPE.TEXTURE_3D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Texture3D"; break;
					    case REFERENCE_DATATYPE.SPARSE_TEXTURE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "SparseTexture"; break;
					    case REFERENCE_DATATYPE.SHADER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Shader"; break;
					    case REFERENCE_DATATYPE.CLOTH: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Cloth"; break;
					    case REFERENCE_DATATYPE.SKYBOX: _database_info.tabs[_tab_index].tab_variables[_var_index].var_type = "Skybox"; break;
					}
				}
//				
//				if (_database_info.tabs[_tab_index].tab_variables[_var_index].second_selected_variable_type == SELECTED_TYPE.REFERENCE)
//				{
//					switch (_database_info.tabs[_tab_index].tab_variables[_var_index].second_ref_variable_type)
//					{
//					case REFERENCE_DATATYPE.NONE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = ""; break;
//					case REFERENCE_DATATYPE.GAMEOBJECT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "GameObject"; break;
//					case REFERENCE_DATATYPE.TRANSFORM: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Transform"; break;
//					case REFERENCE_DATATYPE.OBJECT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Object"; break;
//					case REFERENCE_DATATYPE.CAMERA: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Camera"; break;
//					case REFERENCE_DATATYPE.LIGHT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Light"; break;
//					case REFERENCE_DATATYPE.COMPONENT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Component"; break;
//					case REFERENCE_DATATYPE.SCRIPTABLE_OBJECT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "ScriptableObject"; break;
//					case REFERENCE_DATATYPE.AVATAR: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Avatar"; break;
//					case REFERENCE_DATATYPE.CHARACTER_CONTROLLER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "CharacterController"; break;
//					case REFERENCE_DATATYPE.ANIMATION: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Animation"; break;
//					case REFERENCE_DATATYPE.ANIMATION_CLIP: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "AnimationClip"; break;
//					case REFERENCE_DATATYPE.ANIMATIOR: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Animator"; break;
//					case REFERENCE_DATATYPE.AUDIO_CLIP: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "AudioClip"; break;
//					case REFERENCE_DATATYPE.AUDIO_SOURCE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "AudioSource"; break;
//					case REFERENCE_DATATYPE.AUDIO_LISTENER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "AudioListener"; break;
//					case REFERENCE_DATATYPE.COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Collider"; break;
//					case REFERENCE_DATATYPE.COLLIDER_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Collider2D"; break;
//					case REFERENCE_DATATYPE.BOX_COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "BoxCollider"; break;
//					case REFERENCE_DATATYPE.BOX_COLLIDER_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "BoxCollider2D"; break;
//					case REFERENCE_DATATYPE.CAPSULE_COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "CapsuleCollider"; break;
//					case REFERENCE_DATATYPE.EDGE_COLLIDER_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "EdgeCollider2D"; break;
//					case REFERENCE_DATATYPE.POLYGON_COLLIDER2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "PolygonCollider2D"; break;
//					case REFERENCE_DATATYPE.MESH_COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "MeshCollider"; break;
//					case REFERENCE_DATATYPE.TERRIAN_COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "TerrainCollider"; break;
//					case REFERENCE_DATATYPE.WHEEL_COLLIDER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "WheelCollider"; break;
////					case REFERENCE_DATATYPE.COLLISION: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Collision"; break;
////					case REFERENCE_DATATYPE.COLLISION_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Collision2D"; break;
//					case REFERENCE_DATATYPE.PHYSIC_MATERIAL: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "PhysicMaterial"; break;
//					case REFERENCE_DATATYPE.PHYSICS_MATERIAL_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "PhysicsMaterial2D"; break;
////					case REFERENCE_DATATYPE.PHYSICS: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Physics"; break;
////					case REFERENCE_DATATYPE.PHYSICS_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Physics2D"; break;
//					case REFERENCE_DATATYPE.RIGIDBODY: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Rigidbody"; break;
//					case REFERENCE_DATATYPE.RIGIDBODY_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Rigidbody2D"; break;
//					case REFERENCE_DATATYPE.EFFECTOR_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Effector2D"; break;
//					case REFERENCE_DATATYPE.PLATFORM_EFFECTOR_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "PlatformEffector2D"; break;
//					case REFERENCE_DATATYPE.SURFACE_EFFECTOR_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "SurfaceEffector2D"; break;
//					case REFERENCE_DATATYPE.AREA_EFFECTOR_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "AreaEffector2D"; break;
//					case REFERENCE_DATATYPE.POINT_EFFECTOR_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "PointEffector2D"; break;
//					case REFERENCE_DATATYPE.FONT: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Font"; break;
//					case REFERENCE_DATATYPE.SPRITE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Sprite"; break;
//					case REFERENCE_DATATYPE.SPRITE_RENDERER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "SpriteRenderer"; break;
//					case REFERENCE_DATATYPE.MESH: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Mesh"; break;
//					case REFERENCE_DATATYPE.CUBEMAP: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Cubemap"; break;
//					case REFERENCE_DATATYPE.MATERIAL: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Material"; break;
//					case REFERENCE_DATATYPE.TEXTURE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Texture"; break;
//					case REFERENCE_DATATYPE.TEXTURE_2D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Texture2D"; break;
//					case REFERENCE_DATATYPE.TEXTURE_3D: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Texture3D"; break;
//					case REFERENCE_DATATYPE.SPARSE_TEXTURE: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "SparseTexture"; break;
//					case REFERENCE_DATATYPE.SHADER: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Shader"; break;
//					case REFERENCE_DATATYPE.CLOTH: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Cloth"; break;
//					case REFERENCE_DATATYPE.SKYBOX: _database_info.tabs[_tab_index].tab_variables[_var_index].var_second_type = "Skybox"; break;
//					}
//				}
			}

			if (_database_info.enable_tabs == false)
			{
				if(_database_info.variables[_var_index].var_list_type == LIST_TYPE.NONE 
				   ||_database_info.variables[_var_index].var_list_type == LIST_TYPE.ARRAY
				   ||_database_info.variables[_var_index].var_list_type == LIST_TYPE.LIST)
				{
//					_database_info.variables[_var_index].second_value_variable_type = VALUE_DATATYPE.NONE;
//					_database_info.variables[_var_index].second_ref_variable_type = REFERENCE_DATATYPE.NONE;
				}
				
				if (_database_info.variables[_var_index].selected_variable_type == SELECTED_TYPE.VALUE)
				{
					switch (_database_info.variables[_var_index].variable_value_type)
					{
		    			case VALUE_DATATYPE.NONE: _database_info.variables[_var_index].var_type = ""; break;
					    case VALUE_DATATYPE.STRING: _database_info.variables[_var_index].var_type = "string"; break;
    //					case VALUE_DATATYPE.CHAR: _database_info.variables[_var_index].var_type = "char"; break;
				        case VALUE_DATATYPE.INT: _database_info.variables[_var_index].var_type = "int"; break;
                        case VALUE_DATATYPE.LONG: _database_info.variables[_var_index].var_type = "long"; break;
                        case VALUE_DATATYPE.DOUBLE: _database_info.variables[_var_index].var_type = "double"; break;
					    case VALUE_DATATYPE.FLOAT: _database_info.variables[_var_index].var_type = "float"; break;
    //					case VALUE_DATATYPE.SHORT: _database_info.variables[_var_index].var_type = "short"; break;
    //					case VALUE_DATATYPE.BYTE: _database_info.variables[_var_index].var_type = "byte"; break;
					    case VALUE_DATATYPE.BOOL: _database_info.variables[_var_index].var_type = "bool"; break;
					    case VALUE_DATATYPE.VECTOR2: _database_info.variables[_var_index].var_type = "Vector2"; break;
					    case VALUE_DATATYPE.VECTOR3: _database_info.variables[_var_index].var_type = "Vector3"; break;
					    case VALUE_DATATYPE.VECTOR4: _database_info.variables[_var_index].var_type = "Vector4"; break;
    //					case VALUE_DATATYPE.QUATERNION: _database_info.variables[_var_index].var_type = "Quaternion"; break;
    //					case VALUE_DATATYPE.RAY: _database_info.variables[_var_index].var_type = "Ray"; break;
    //					case VALUE_DATATYPE.RAY2D: _database_info.variables[_var_index].var_type = "Ray2D"; break;
					    case VALUE_DATATYPE.RECT: _database_info.variables[_var_index].var_type = "Rect"; break;
					    case VALUE_DATATYPE.COLOR: _database_info.variables[_var_index].var_type = "Color"; break;
					    case VALUE_DATATYPE.COLOR32: _database_info.variables[_var_index].var_type = "Color32"; break;
					}
				}
//				if (_database_info.variables[_var_index].second_selected_variable_type == SELECTED_TYPE.VALUE)
//				{
//					switch (_database_info.variables[_var_index].second_value_variable_type)
//					{
//					case VALUE_DATATYPE.NONE: _database_info.variables[_var_index].var_second_type = ""; break;
//					case VALUE_DATATYPE.STRING: _database_info.variables[_var_index].var_second_type = "string"; break;
////					case VALUE_DATATYPE.CHAR: _database_info.variables[_var_index].var_second_type = "char"; break;
//					case VALUE_DATATYPE.INT: _database_info.variables[_var_index].var_second_type = "int"; break;
////					case VALUE_DATATYPE.LONG: _database_info.variables[_var_index].var_second_type = "long"; break;
//					case VALUE_DATATYPE.DOUBLE: _database_info.variables[_var_index].var_second_type = "double"; break;
//					case VALUE_DATATYPE.FLOAT: _database_info.variables[_var_index].var_second_type = "float"; break;
////					case VALUE_DATATYPE.SHORT: _database_info.variables[_var_index].var_second_type = "short"; break;
////					case VALUE_DATATYPE.BYTE: _database_info.variables[_var_index].var_second_type = "byte"; break;
//					case VALUE_DATATYPE.BOOL: _database_info.variables[_var_index].var_second_type = "bool"; break;
//					case VALUE_DATATYPE.VECTOR2: _database_info.variables[_var_index].var_second_type = "Vector2"; break;
//					case VALUE_DATATYPE.VECTOR3: _database_info.variables[_var_index].var_second_type = "Vector3"; break;
//					case VALUE_DATATYPE.VECTOR4: _database_info.variables[_var_index].var_second_type = "Vector4"; break;
////					case VALUE_DATATYPE.QUATERNION: _database_info.variables[_var_index].var_second_type = "Quaternion"; break;
////					case VALUE_DATATYPE.RAY: _database_info.variables[_var_index].var_second_type = "Ray"; break;
////					case VALUE_DATATYPE.RAY2D: _database_info.variables[_var_index].var_second_type = "Ray2D"; break;
//					case VALUE_DATATYPE.RECT: _database_info.variables[_var_index].var_second_type = "Rect"; break;
//					case VALUE_DATATYPE.COLOR: _database_info.variables[_var_index].var_second_type = "Color"; break;
//					case VALUE_DATATYPE.COLOR32: _database_info.variables[_var_index].var_second_type = "Color32"; break;
//					}
//				}
				if (_database_info.variables[_var_index].selected_variable_type == SELECTED_TYPE.REFERENCE)
				{
					switch (_database_info.variables[_var_index].variable_reference_type)
					{
					case REFERENCE_DATATYPE.NONE: _database_info.variables[_var_index].var_type = ""; break;
					case REFERENCE_DATATYPE.GAMEOBJECT: _database_info.variables[_var_index].var_type = "GameObject"; break;
					case REFERENCE_DATATYPE.TRANSFORM: _database_info.variables[_var_index].var_type = "Transform"; break;
					case REFERENCE_DATATYPE.OBJECT: _database_info.variables[_var_index].var_type = "Object"; break;
					case REFERENCE_DATATYPE.CAMERA: _database_info.variables[_var_index].var_type = "Camera"; break;
					case REFERENCE_DATATYPE.LIGHT: _database_info.variables[_var_index].var_type = "Light"; break;
					case REFERENCE_DATATYPE.COMPONENT: _database_info.variables[_var_index].var_type = "Component"; break;
					case REFERENCE_DATATYPE.SCRIPTABLE_OBJECT: _database_info.variables[_var_index].var_type = "ScriptableObject"; break;
					case REFERENCE_DATATYPE.AVATAR: _database_info.variables[_var_index].var_type = "Avatar"; break;
					case REFERENCE_DATATYPE.CHARACTER_CONTROLLER: _database_info.variables[_var_index].var_type = "CharacterController"; break;
					case REFERENCE_DATATYPE.ANIMATION: _database_info.variables[_var_index].var_type = "Animation"; break;
					case REFERENCE_DATATYPE.ANIMATION_CLIP: _database_info.variables[_var_index].var_type = "AnimationClip"; break;
					case REFERENCE_DATATYPE.ANIMATIOR: _database_info.variables[_var_index].var_type = "Animator"; break;
					case REFERENCE_DATATYPE.AUDIO_CLIP: _database_info.variables[_var_index].var_type = "AudioClip"; break;
					case REFERENCE_DATATYPE.AUDIO_SOURCE: _database_info.variables[_var_index].var_type = "AudioSource"; break;
					case REFERENCE_DATATYPE.AUDIO_LISTENER: _database_info.variables[_var_index].var_type = "AudioListener"; break;
					case REFERENCE_DATATYPE.COLLIDER: _database_info.variables[_var_index].var_type = "Collider"; break;
					case REFERENCE_DATATYPE.COLLIDER_2D: _database_info.variables[_var_index].var_type = "Collider2D"; break;
					case REFERENCE_DATATYPE.BOX_COLLIDER: _database_info.variables[_var_index].var_type = "BoxCollider"; break;
					case REFERENCE_DATATYPE.BOX_COLLIDER_2D: _database_info.variables[_var_index].var_type = "BoxCollider2D"; break;
					case REFERENCE_DATATYPE.CAPSULE_COLLIDER: _database_info.variables[_var_index].var_type = "CapsuleCollider"; break;
					case REFERENCE_DATATYPE.EDGE_COLLIDER_2D: _database_info.variables[_var_index].var_type = "EdgeCollider2D"; break;
					case REFERENCE_DATATYPE.POLYGON_COLLIDER2D: _database_info.variables[_var_index].var_type = "PolygonCollider2D"; break;
					case REFERENCE_DATATYPE.MESH_COLLIDER: _database_info.variables[_var_index].var_type = "MeshCollider"; break;
					case REFERENCE_DATATYPE.TERRIAN_COLLIDER: _database_info.variables[_var_index].var_type = "TerrainCollider"; break;
					case REFERENCE_DATATYPE.WHEEL_COLLIDER: _database_info.variables[_var_index].var_type = "WheelCollider"; break;
//					case REFERENCE_DATATYPE.COLLISION: _database_info.variables[_var_index].var_type = "Collision"; break;
//					case REFERENCE_DATATYPE.COLLISION_2D: _database_info.variables[_var_index].var_type = "Collision2D"; break;
					case REFERENCE_DATATYPE.PHYSIC_MATERIAL: _database_info.variables[_var_index].var_type = "PhysicMaterial"; break;
					case REFERENCE_DATATYPE.PHYSICS_MATERIAL_2D: _database_info.variables[_var_index].var_type = "PhysicsMaterial2D"; break;
//					case REFERENCE_DATATYPE.PHYSICS: _database_info.variables[_var_index].var_type = "Physics"; break;
//					case REFERENCE_DATATYPE.PHYSICS_2D: _database_info.variables[_var_index].var_type = "Physics2D"; break;
					case REFERENCE_DATATYPE.RIGIDBODY: _database_info.variables[_var_index].var_type = "Rigidbody"; break;
					case REFERENCE_DATATYPE.RIGIDBODY_2D: _database_info.variables[_var_index].var_type = "Rigidbody2D"; break;
					case REFERENCE_DATATYPE.EFFECTOR_2D: _database_info.variables[_var_index].var_type = "Effector2D"; break;
					case REFERENCE_DATATYPE.PLATFORM_EFFECTOR_2D: _database_info.variables[_var_index].var_type = "PlatformEffector2D"; break;
					case REFERENCE_DATATYPE.SURFACE_EFFECTOR_2D: _database_info.variables[_var_index].var_type = "SurfaceEffector2D"; break;
					case REFERENCE_DATATYPE.AREA_EFFECTOR_2D: _database_info.variables[_var_index].var_type = "AreaEffector2D"; break;
					case REFERENCE_DATATYPE.POINT_EFFECTOR_2D: _database_info.variables[_var_index].var_type = "PointEffector2D"; break;
					case REFERENCE_DATATYPE.FONT: _database_info.variables[_var_index].var_type = "Font"; break;
					case REFERENCE_DATATYPE.SPRITE: _database_info.variables[_var_index].var_type = "Sprite"; break;
					case REFERENCE_DATATYPE.SPRITE_RENDERER: _database_info.variables[_var_index].var_type = "SpriteRenderer"; break;
					case REFERENCE_DATATYPE.MESH: _database_info.variables[_var_index].var_type = "Mesh"; break;
					case REFERENCE_DATATYPE.CUBEMAP: _database_info.variables[_var_index].var_type = "Cubemap"; break;
					case REFERENCE_DATATYPE.MATERIAL: _database_info.variables[_var_index].var_type = "Material"; break;
					case REFERENCE_DATATYPE.TEXTURE: _database_info.variables[_var_index].var_type = "Texture"; break;
					case REFERENCE_DATATYPE.TEXTURE_2D: _database_info.variables[_var_index].var_type = "Texture2D"; break;
					case REFERENCE_DATATYPE.TEXTURE_3D: _database_info.variables[_var_index].var_type = "Texture3D"; break;
					case REFERENCE_DATATYPE.SPARSE_TEXTURE: _database_info.variables[_var_index].var_type = "SparseTexture"; break;
					case REFERENCE_DATATYPE.SHADER: _database_info.variables[_var_index].var_type = "Shader"; break;
					case REFERENCE_DATATYPE.CLOTH: _database_info.variables[_var_index].var_type = "Cloth"; break;
					case REFERENCE_DATATYPE.SKYBOX: _database_info.variables[_var_index].var_type = "Skybox"; break;
					}
				}
//				if (_database_info.variables[_var_index].second_selected_variable_type == SELECTED_TYPE.REFERENCE)
//				{
//					switch (_database_info.variables[_var_index].second_ref_variable_type)
//					{
//					case REFERENCE_DATATYPE.NONE: _database_info.variables[_var_index].var_second_type = ""; break;
//					case REFERENCE_DATATYPE.GAMEOBJECT: _database_info.variables[_var_index].var_second_type = "GameObject"; break;
//					case REFERENCE_DATATYPE.TRANSFORM: _database_info.variables[_var_index].var_second_type = "Transform"; break;
//					case REFERENCE_DATATYPE.OBJECT: _database_info.variables[_var_index].var_second_type = "Object"; break;
//					case REFERENCE_DATATYPE.CAMERA: _database_info.variables[_var_index].var_second_type = "Camera"; break;
//					case REFERENCE_DATATYPE.LIGHT: _database_info.variables[_var_index].var_second_type = "Light"; break;
//					case REFERENCE_DATATYPE.COMPONENT: _database_info.variables[_var_index].var_second_type = "Component"; break;
//					case REFERENCE_DATATYPE.SCRIPTABLE_OBJECT: _database_info.variables[_var_index].var_second_type = "ScriptableObject"; break;
//					case REFERENCE_DATATYPE.AVATAR: _database_info.variables[_var_index].var_second_type = "Avatar"; break;
//					case REFERENCE_DATATYPE.CHARACTER_CONTROLLER: _database_info.variables[_var_index].var_second_type = "CharacterController"; break;
//					case REFERENCE_DATATYPE.ANIMATION: _database_info.variables[_var_index].var_second_type = "Animation"; break;
//					case REFERENCE_DATATYPE.ANIMATION_CLIP: _database_info.variables[_var_index].var_second_type = "AnimationClip"; break;
//					case REFERENCE_DATATYPE.ANIMATIOR: _database_info.variables[_var_index].var_second_type = "Animator"; break;
//					case REFERENCE_DATATYPE.AUDIO_CLIP: _database_info.variables[_var_index].var_second_type = "AudioClip"; break;
//					case REFERENCE_DATATYPE.AUDIO_SOURCE: _database_info.variables[_var_index].var_second_type = "AudioSource"; break;
//					case REFERENCE_DATATYPE.AUDIO_LISTENER: _database_info.variables[_var_index].var_second_type = "AudioListener"; break;
//					case REFERENCE_DATATYPE.COLLIDER: _database_info.variables[_var_index].var_second_type = "Collider"; break;
//					case REFERENCE_DATATYPE.COLLIDER_2D: _database_info.variables[_var_index].var_second_type = "Collider2D"; break;
//					case REFERENCE_DATATYPE.BOX_COLLIDER: _database_info.variables[_var_index].var_second_type = "BoxCollider"; break;
//					case REFERENCE_DATATYPE.BOX_COLLIDER_2D: _database_info.variables[_var_index].var_second_type = "BoxCollider2D"; break;
//					case REFERENCE_DATATYPE.CAPSULE_COLLIDER: _database_info.variables[_var_index].var_second_type = "CapsuleCollider"; break;
//					case REFERENCE_DATATYPE.EDGE_COLLIDER_2D: _database_info.variables[_var_index].var_second_type = "EdgeCollider2D"; break;
//					case REFERENCE_DATATYPE.POLYGON_COLLIDER2D: _database_info.variables[_var_index].var_second_type = "PolygonCollider2D"; break;
//					case REFERENCE_DATATYPE.MESH_COLLIDER: _database_info.variables[_var_index].var_second_type = "MeshCollider"; break;
//					case REFERENCE_DATATYPE.TERRIAN_COLLIDER: _database_info.variables[_var_index].var_second_type = "TerrainCollider"; break;
//					case REFERENCE_DATATYPE.WHEEL_COLLIDER: _database_info.variables[_var_index].var_second_type = "WheelCollider"; break;
////					case REFERENCE_DATATYPE.COLLISION: _database_info.variables[_var_index].var_second_type = "Collision"; break;
////					case REFERENCE_DATATYPE.COLLISION_2D: _database_info.variables[_var_index].var_second_type = "Collision2D"; break;
//					case REFERENCE_DATATYPE.PHYSIC_MATERIAL: _database_info.variables[_var_index].var_second_type = "PhysicMaterial"; break;
//					case REFERENCE_DATATYPE.PHYSICS_MATERIAL_2D: _database_info.variables[_var_index].var_second_type = "PhysicsMaterial2D"; break;
////					case REFERENCE_DATATYPE.PHYSICS: _database_info.variables[_var_index].var_second_type = "Physics"; break;
////					case REFERENCE_DATATYPE.PHYSICS_2D: _database_info.variables[_var_index].var_second_type = "Physics2D"; break;
//					case REFERENCE_DATATYPE.RIGIDBODY: _database_info.variables[_var_index].var_second_type = "Rigidbody"; break;
//					case REFERENCE_DATATYPE.RIGIDBODY_2D: _database_info.variables[_var_index].var_second_type = "Rigidbody2D"; break;
//					case REFERENCE_DATATYPE.EFFECTOR_2D: _database_info.variables[_var_index].var_second_type = "Effector2D"; break;
//					case REFERENCE_DATATYPE.PLATFORM_EFFECTOR_2D: _database_info.variables[_var_index].var_second_type = "PlatformEffector2D"; break;
//					case REFERENCE_DATATYPE.SURFACE_EFFECTOR_2D: _database_info.variables[_var_index].var_second_type = "SurfaceEffector2D"; break;
//					case REFERENCE_DATATYPE.AREA_EFFECTOR_2D: _database_info.variables[_var_index].var_second_type = "AreaEffector2D"; break;
//					case REFERENCE_DATATYPE.POINT_EFFECTOR_2D: _database_info.variables[_var_index].var_second_type = "PointEffector2D"; break;
//					case REFERENCE_DATATYPE.FONT: _database_info.variables[_var_index].var_second_type = "Font"; break;
//					case REFERENCE_DATATYPE.SPRITE: _database_info.variables[_var_index].var_second_type = "Sprite"; break;
//					case REFERENCE_DATATYPE.SPRITE_RENDERER: _database_info.variables[_var_index].var_second_type = "SpriteRenderer"; break;
//					case REFERENCE_DATATYPE.MESH: _database_info.variables[_var_index].var_second_type = "Mesh"; break;
//					case REFERENCE_DATATYPE.CUBEMAP: _database_info.variables[_var_index].var_second_type = "Cubemap"; break;
//					case REFERENCE_DATATYPE.MATERIAL: _database_info.variables[_var_index].var_second_type = "Material"; break;
//					case REFERENCE_DATATYPE.TEXTURE: _database_info.variables[_var_index].var_second_type = "Texture"; break;
//					case REFERENCE_DATATYPE.TEXTURE_2D: _database_info.variables[_var_index].var_second_type = "Texture2D"; break;
//					case REFERENCE_DATATYPE.TEXTURE_3D: _database_info.variables[_var_index].var_second_type = "Texture3D"; break;
//					case REFERENCE_DATATYPE.SPARSE_TEXTURE: _database_info.variables[_var_index].var_second_type = "SparseTexture"; break;
//					case REFERENCE_DATATYPE.SHADER: _database_info.variables[_var_index].var_second_type = "Shader"; break;
//					case REFERENCE_DATATYPE.CLOTH: _database_info.variables[_var_index].var_second_type = "Cloth"; break;
//					case REFERENCE_DATATYPE.SKYBOX: _database_info.variables[_var_index].var_second_type = "Skybox"; break;
//					}
//				}
			}

		} 

	} 

}
