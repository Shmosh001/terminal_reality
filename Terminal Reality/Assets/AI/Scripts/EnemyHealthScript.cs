using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {


	public int health;

	public bool isEnemy;


	private ZombieFSM fsm;



	// Use this for initialization
	void Start () {
		fsm = gameObject.GetComponent<ZombieFSM>();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0){
			fsm.alertDead();
			//Destroy(gameObject,1);
		}
	}


	public void gainHealth(int value){
		health += value;
		if (health > 100){
			health = 100;
		}
	}

	public void takeDamage(int value){
		health -= value;
		fsm.alertShot();
	}
}
