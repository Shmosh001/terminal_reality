using UnityEngine;
using System.Collections;

/// <summary>
/// this class is responsible for golding all variables to do with an AI entities health and has methods for changing this
/// </summary>
public class EnemyHealthScript : MonoBehaviour {

    //PUBLIC VARS

    //the entities health
	public int health;
    //enemy identifier
	public bool isEnemy;

    //PRIVATE VARS

    //boolean which determines if the entity has died or not
    private bool dead;
    //the finite state machine
	private ZombieFSM fsm;



    /// <summary>
    /// initialization
    /// </summary>
    void Start () {
		fsm = gameObject.GetComponent<ZombieFSM>();
	}

    /// <summary>
    /// Update is called once per frame and checks if the entity is dead
    /// </summary>
    void Update () {
        //we check to see if health is smaller than 0 and that the entity has not registered being dead yet
		if (health <= 0 && !dead){
            //we alert the fsm which will handle animations etc
            fsm.alertDead();
			dead = true;
		}
	}

    /// <summary>
    /// increases the entities health by <value> amount
    /// </summary>
    /// <param name="value">
    /// amount of health
    /// </param>
    [PunRPC]
    public void gainHealth(int value){
		health += value;
		if (health > 100){
			health = 100;
		}
	}

    /// <summary>
    /// decreases the entities <entity> health by a certain amount <value>
    /// </summary>
    /// <param name="value">
    /// decrease amount
    /// </param>
    /// <param name="entity">
    /// entity
    /// </param>
    [PunRPC]
	public void takeDamage(int value, GameObject entity){
        //we check if the entity is alive and subtract the amount if it is and alert the fsm that the unit has been shot
        if (health > 0){
			health -= value;
			fsm.alertShot(entity);
		}
        //if the entity is dead and has not registered being dead, we alert the fsm
		if (health <= 0 && !dead){
			fsm.alertDead();
			dead = true;
		}

	}
}
