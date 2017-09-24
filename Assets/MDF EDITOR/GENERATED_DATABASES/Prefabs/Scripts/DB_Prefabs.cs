using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
 

 [System.Serializable] 
 
	 public partial class DB_Prefabs
 {
 
	 public int ID; 
	 public string NAME; 
	 public GameObject[] Skeleton; 

	 public DB_Prefabs() 
	 { 

	 } 
	 public DB_Prefabs(DB_Prefabs _object) 
	 { 

	 NAME = _object.NAME; 
	 Skeleton = _object.Skeleton; 
	} 

 	 public DB_Prefabs( 
	 string _NAME = default(string),
	 GameObject[] _Skeleton = default(GameObject[])) 
	 { 
 
	 Skeleton = _Skeleton; 
	} 

 	 public DB_Prefabs GetCopy() { return new DB_Prefabs(this); }
}