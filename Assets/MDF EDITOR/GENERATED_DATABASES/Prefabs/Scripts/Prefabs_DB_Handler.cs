using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Linq; 

	 public partial class Prefabs_DB_Handler : ScriptableObject, IEnumerable<DB_Prefabs> 
{ 
 
	 public const string ASSET_NAME = "Prefabs"; // Physical File name of the Database 
	 public const string ASSET_TYPE = ".asset"; // Type of Database 
	 public const string ASSET_PATH = "Database"; // Location of the Database 
	 public const string DATABASE_PATH = "Assets/Resources/" + ASSET_PATH + "/" + ASSET_NAME + ASSET_TYPE; 
 
	 [SerializeField] 
	 private List<DB_Prefabs> prefabs_database; 
 
	 void OnEnable() 
	 { 
		 if (prefabs_database == null) 
		 { 
			prefabs_database = new List<DB_Prefabs> (); 
	 	 } 
	 } 
 
	 public DB_Prefabs this[int _ID] 
	 { 
		 get 
		 { 
			 return Find(_ID);
		 } 
	 } 
 
	 public DB_Prefabs this[string _NAME] 
	 { 
		 get 
		 { 
			 return Find(_NAME);
		 } 
	 } 
 
	 public DB_Prefabs this[DB_Prefabs _DB] 
	 { 
		 get 
		 { 
			 return Find(_DB.ID);
		 } 
	 } 
 
	 public void ADD(DB_Prefabs _database) 
	 { 
		 _database.ID = COUNT; 
 		 prefabs_database.Add(_database); 
	 } 
 
	 public void ADD_AT( int _index, DB_Prefabs _database) 
	 { 
		_database.ID = COUNT; 
 		prefabs_database.Insert(_index, _database); 
	 } 
 
	 public void REMOVE(DB_Prefabs _database) 
	 { 
		 prefabs_database.Remove(_database); 
	 } 
 
	 public void REMOVE(string _name) 
	 { 
		for (int i = 0; i < prefabs_database.Count; i++)
			if (prefabs_database[i].NAME == _name)
			{
				prefabs_database.RemoveAt(i);
				break;
			}
	 } 
 
	 public void REMOVE(int _id) 
	 { 
		for (int i = 0; i < prefabs_database.Count; i++)
			if (prefabs_database[i].ID == _id)
			{
				prefabs_database.RemoveAt(i);
				break;
			}
	 } 
 
	 public void REMOVE_AT( int _index) 
	 { 
		 prefabs_database.RemoveAt(_index); 
	 } 
 
	 public bool MoveUp(int _index) 
	 { 
		if (_index <= 0 || _index >= COUNT)
			return false;

		DB_Prefabs temp = prefabs_database[_index - 1];
		prefabs_database[_index - 1] = prefabs_database[_index];
		prefabs_database[_index] = temp;
		return true;
	 } 
 
	 public bool MoveDown(int _index) 
	 { 
		if (_index == COUNT - 1 || _index < 0 || _index >= COUNT)
			return false;

		DB_Prefabs temp = prefabs_database[_index + 1];
		prefabs_database[_index + 1] = prefabs_database[_index];
		prefabs_database[_index] = temp;
		return true;
	 } 
 
	 public int COUNT 
	 { 
		 get { return prefabs_database.Count;} 
	 } 
 
	 public DB_Prefabs Find (string _name) 
	 { 
		for (int i = 0; i < prefabs_database.Count; i++)
			if (prefabs_database[i].NAME == _name)
				 return prefabs_database[i];

		Debug.LogError("The name: " + _name + " was not found in the " + ASSET_NAME + " database.");
		return null;
	 } 
 
	 public DB_Prefabs Find (int _id) 
	 { 
		if (_id >= 0 && _id < COUNT)
			return prefabs_database[_id];

		Debug.LogError("The ID: " + _id + " was not found in the " + ASSET_NAME + " database.");
		return null;
	 } 
 
	 public void SortAlphabetically() 
	 { 
		 prefabs_database.Sort((x,y) => string.Compare(x.NAME, y.NAME)); 
	 } 
 
	 public void SortAlphabeticallyReverse() 
	 { 
	 	 prefabs_database.Sort((x,y) => string.Compare(y.NAME, x.NAME)); 
	 } 
 
	 public IEnumerator<DB_Prefabs>GetEnumerator() 
	 { 
		 return prefabs_database.GetEnumerator(); 
	 } 
 
	 IEnumerator IEnumerable.GetEnumerator() 
	 { 
		 return prefabs_database.GetEnumerator(); 
	 } 
 

 }