using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.IO; 
#if UNITY_EDITOR 
using UnityEditor; 
#endif 
 
namespace MDF_EDITOR 
{ 
namespace DATABASE 
{ 
	 public static partial class DATABASE{ 
 
		 private static OPTIONS_DB_HANDLER options; 
		 public static OPTIONS_DB_HANDLER Options 
		 { 
			 get
			 { 
 #if UNITY_EDITOR 
				 if(Directory.Exists(OPTIONS_DB_HANDLER.ASSET_PATH) == false){Directory.CreateDirectory(OPTIONS_DB_HANDLER.ASSET_PATH);} 
				 if (File.Exists(OPTIONS_DB_HANDLER.DATABASE_PATH) == false){AssetDatabase.CreateAsset((OPTIONS_DB_HANDLER)ScriptableObject.CreateInstance("OPTIONS_DB_HANDLER"),OPTIONS_DB_HANDLER.DATABASE_PATH);AssetDatabase.SaveAssets();} 
				 options = (OPTIONS_DB_HANDLER)AssetDatabase.LoadAssetAtPath(OPTIONS_DB_HANDLER.DATABASE_PATH, typeof(OPTIONS_DB_HANDLER)); 
				 EditorUtility.SetDirty(options); 
 #endif 
				 return options; 
			 }
		 } 
		 private static DB_INFO_HANDLER db_info; 
		 public static DB_INFO_HANDLER DB_Info 
		 { 
			 get
			 { 
 #if UNITY_EDITOR 
				 if(Directory.Exists(DB_INFO_HANDLER.ASSET_PATH) == false){Directory.CreateDirectory(DB_INFO_HANDLER.ASSET_PATH);} 
				 if (File.Exists(DB_INFO_HANDLER.DATABASE_PATH) == false){AssetDatabase.CreateAsset((DB_INFO_HANDLER)ScriptableObject.CreateInstance("DB_INFO_HANDLER"),DB_INFO_HANDLER.DATABASE_PATH);AssetDatabase.SaveAssets();} 
				 db_info = (DB_INFO_HANDLER)AssetDatabase.LoadAssetAtPath(DB_INFO_HANDLER.DATABASE_PATH, typeof(DB_INFO_HANDLER)); 
				 EditorUtility.SetDirty(db_info); 
 #endif 
				 return db_info; 
			 } 
		 } 
		 private static Prefabs_DB_Handler _Prefabs; 
		 public static Prefabs_DB_Handler Prefabs 
		 { 
			 get
			 { 
 #if UNITY_EDITOR 
				 if(Directory.Exists(Prefabs_DB_Handler.ASSET_PATH) == false){Directory.CreateDirectory(Prefabs_DB_Handler.ASSET_PATH);} 
				 if (File.Exists(Prefabs_DB_Handler.DATABASE_PATH) == false){AssetDatabase.CreateAsset((Prefabs_DB_Handler)ScriptableObject.CreateInstance("Prefabs_DB_Handler"),Prefabs_DB_Handler.DATABASE_PATH);AssetDatabase.SaveAssets();} 
				 _Prefabs = (Prefabs_DB_Handler)AssetDatabase.LoadAssetAtPath(Prefabs_DB_Handler.DATABASE_PATH, typeof(Prefabs_DB_Handler)); 
				 EditorUtility.SetDirty(_Prefabs); 
  #endif 
				 if(_Prefabs == null)
					 _Prefabs = (Prefabs_DB_Handler)Resources.Load(Prefabs_DB_Handler.ASSET_PATH +"/"+ Prefabs_DB_Handler.ASSET_NAME);
				 return _Prefabs; 
			 } 
		 } 

 }
 }
 }