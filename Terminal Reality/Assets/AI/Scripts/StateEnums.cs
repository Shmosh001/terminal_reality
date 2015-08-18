using UnityEngine;
using System.Collections;

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

	//list all states and have a switch statement with behaviour


	public enum HumanStates{
		Running, 
		Screaming, 
		Dying, 
		Dead, 
		Walking, 
		Idle
	};


	

}
