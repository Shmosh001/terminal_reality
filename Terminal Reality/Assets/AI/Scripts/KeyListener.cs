using UnityEngine;
using System.Collections;

public class KeyListener : MonoBehaviour {

	public GameObject player;
	public GameObject enemy;
	public bool debug;
	public int damage;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown(KeyCode.Space)){
			if (debug){
				Debug.Log("We shot");
			}
			EnemyHealthScript script = enemy.GetComponent<EnemyHealthScript>();
			script.takeDamage(damage, player.gameObject);
		}



	}
}
