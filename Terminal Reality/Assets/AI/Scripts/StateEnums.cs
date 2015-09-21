using UnityEngine;
using System.Collections;

/// <summary>
/// this class holds all the enums for the AI
/// </summary>
public class StateEnums : MonoBehaviour {


	public enum ZombieStates: byte{
		Idle = 0,
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

	public enum HumanStates : byte{
		Running = 0, 
		Screaming, 
		Dying, 
		Dead, 
		Walking, 
		Idle
	};


	

}
