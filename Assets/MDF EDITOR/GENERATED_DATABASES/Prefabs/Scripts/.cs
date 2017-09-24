using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
 

 [System.Serializable] 
 
	 public partial class DB_Prefabs
 {
 
	 public int ID; 
	 public string NAME; 
	 public DB_Prefabs() 
	 { 

	 } 
	 public DB_Prefabs(DB_Prefabs _object) 
	 { 

	 NAME = _object.NAME; 
	} 

 	 public DB_Prefabs( 
	 string _NAME = default(string))
	 { 
 
	} 

 	 public DB_Prefabs GetCopy() { return new DB_Prefabs(this); }
}