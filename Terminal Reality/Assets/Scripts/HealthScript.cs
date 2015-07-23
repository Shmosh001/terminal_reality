using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {


	public int health;

	public bool isEnemy;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0){
			Destroy(gameObject,1);
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
	}
}
