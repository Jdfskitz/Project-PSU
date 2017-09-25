using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour{

//PlayerInstantiator pInstantiator;

public int strength;
public int intellect;
public int agility;
public int spirit;
public int armor;
public int defense;
public int healthPoints;

//Private List<int> StatPoints = {strength, intellect, agility, spirit, armor, defense, healthpoints};

//Whatever stats are decided to be implemented

public void Start(){
//pInstantiator = new FindObjectOfType<pInstantiator>();
}

public void instantiatedPlayer(GameObject go){
//string connection= “database,host,user,pass,port”;
//MySql.Data.MysqlConnection cnn = new MySql.Data.MysqlConnection cnn(connection);
//establish mysqlcommand
//establish selection from query
//establish reader
//establish that instantiated prefab Player stats match database
}


public void characterLevelUp()
{
//Check database for stat changes
}

public void GearEquipped()
{

}

public void GearRemoved()
{

}

public void DebuffApplied()
{

//remove on DebuffTime.deltaTime;
}

//* items and debuts will affect player stats in a different manner this is only BASE stats without any gear, gear modifications and debuff modifications will be handled separately*/

}



