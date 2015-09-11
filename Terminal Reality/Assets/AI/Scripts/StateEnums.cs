using UnityEngine;
using System.Collections;

/// <summary>
/// this class holds all the enums for the AI
/// </summary>
public class StateEnums : MonoBehaviour {


	public enum ZombieStates{
		Idle,
		Wandering,
		Chasing,
		Searching,
		Puking,
		Attacking,
		Alerted,
		Dying,
		Dead,
		Shot
	};

	public enum HumanStates{
		Running, 
		Screaming, 
		Dying, 
		Dead, 
		Walking, 
		Idle
	};


	

}
