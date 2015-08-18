using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {


	public int health;

	public bool isEnemy;
	private bool alerted;

	private ZombieFSM fsm;



	// Use this for initialization
	void Start () {
		fsm = gameObject.GetComponent<ZombieFSM>();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0 && !alerted){
			fsm.alertDead();
			alerted = true;
			//Destroy(gameObject,1);
		}
	}


	public void gainHealth(int value){
		health += value;
		if (health > 100){
			health = 100;
		}
	}

	public void takeDamage(int value, GameObject entity){
		if (health > 0){
			health -= value;
			fsm.alertShot(entity);
		}
		if (health <= 0 && !alerted){
			fsm.alertDead();
			alerted = true;
		}

	}
}
