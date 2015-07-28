using UnityEngine;
using System.Collections;

public class StateEnums : MonoBehaviour {





	public enum ZombieStates{
		Idle,
		Wandering,
		Running,
		Searching,
		Puking,
		Attacking,
		Alerted,
		Dying,
		Dead
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
