using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MDF_EDITOR.DATABASE;
using UnityEngine;
// THIS IS THE NAMESPACE REQUIRED TO ACCESS THE MDF DATABASE FRAMEWORK

namespace MDF_EDITOR
{
    public class DB_INFO_HANDLER : ScriptableObject, IEnumerable<DB_Database_INFO>
    {
        public const string ASSET_NAME = "_Database_Information"; // Name of the physical database file
        public const string ASSET_TYPE = ".asset"; // extension of the database file
        public const string ASSET_PATH = "Assets/MDF EDITOR/FRAMEWORK"; //location of the database file

        public const string DATABASE_PATH = ASSET_PATH + "/" + ASSET_NAME + ASSET_TYPE;
            // full directory of the database file

        [SerializeField] // SerializeField allows the list to be viewable in the Inspector in Unity
        private List<DB_Database_INFO> db_info; // this is the list that contains a list for all the values above

        public int COUNT // this will count all the objects inside the database
        {
            get { return db_info.Count; }
        }

        private void OnEnable()
        {
            // this is enabled ONCE per load

            if (db_info == null)
            {
                db_info = new List<DB_Database_INFO>(); // Creates the list if it's NULL (does not exist)
            }
        }

        // This is the function you call to access the database from scripts and other places. also note:  ElementAt() requires System.Linq 
        public DB_Database_INFO DB_Database_INFO(int _index)
        {
            return db_info.ElementAt(_index);
        }

        // BELOW are BASE OPERATIONS for lists

        public void Add(DB_Database_INFO _database)
            // This will add to the Database, and it will take any/all parameters the database contains
        {
            _database.ID = COUNT;
            db_info.Add(_database);
        }

        public void AddAt(int _index, DB_Database_INFO _database)
            // This will add at a specifc index, same as add except with an index needed aswell
        {
            _database.ID = COUNT;
            db_info.Insert(_index, _database);
        }

        public void Remove(DB_Database_INFO _database)
            // This will remove the current database object selected in the parameters for this
        {
            db_info.Remove(_database);
        }

        public void RemoveAt(int _index) // This will remove the database object at the index provided
        {
            db_info.RemoveAt(_index);
        }

        public DB_Database_INFO Find(string _name) // use this to FIND something in the databse (by name)
        {
            return db_info.Find(x => x.NAME == (_name));
        }

        public DB_Database_INFO Find(int _id) // use this to FIND something in the databse (by id)
        {
            return db_info.Find(x => x.ID == (_id));
        }

        public int Get_ID(int _id) // This will use find and locate the databse by id and then return back that ID
        {
            return Find(_id).ID;
        }

        public string Get_Name(string name) // this will return the the Name database on a search name
        {
            return Find(name).NAME;
        }

        public void SortAlphabeticallyAtoZ() // This will sort the database A - Z (This will change ID's for objects!)
        {
            db_info.Sort((x, y) => string.Compare(x.NAME, y.NAME));
        }

        public void SortAlphabeticallyZtoA() // This will sort the database Z - A (This will change ID's for objects!)
        {
            db_info.Sort((x, y) => string.Compare(y.NAME, x.NAME));
        }

        public IEnumerator<DB_Database_INFO> GetEnumerator()
        {
            return db_info.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    } // END DB_INFO_HANDLER
} // END MDF_EDITOR NAMESPACE