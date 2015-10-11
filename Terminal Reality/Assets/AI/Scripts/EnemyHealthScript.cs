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
    public bool isBoss;
    public bool isGeneral;
    public bool isWheelChair;



    //PRIVATE VARS

    //boolean which determines if the entity has died or not
    private bool dead;
    //the finite state machine
	private ZombieFSM fsm;
    private BossZombieFSM bfsm;
    private WheelChairFSM wcfsm;



    /// <summary>
    /// initialization
    /// </summary>
    void Start () {
        if (isBoss) {
            bfsm = gameObject.GetComponent<BossZombieFSM>();
        }
        else if (isGeneral) {
            fsm = gameObject.GetComponent<ZombieFSM>();
        }
        else if (isWheelChair) {
            wcfsm = gameObject.GetComponent<WheelChairFSM>();
        }
	}

    /// <summary>
    /// Update is called once per frame and checks if the entity is dead
    /// </summary>
    void Update () {
        //we check to see if health is smaller than 0 and that the entity has not registered being dead yet
		if (health <= 0 && !dead){
            //we alert the fsm which will handle animations etc
            if (isBoss) {
                bfsm.alertDead();
            }
            else if (isGeneral) {
                fsm.alertDead(transform.forward);
            }
            else if (isWheelChair) {
                wcfsm.alertDead(transform.forward);
            }
            dead = true;
		}
	}

    /// <summary>
    /// increases the entities health by <value> amount
    /// </summary>
    /// <param name="value">
    /// amount of health
    /// </param>

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

	public void takeDamage(int value, GameObject target, Vector3 hitPoint){
        //we check if the entity is alive and subtract the amount if it is and alert the fsm that the unit has been shot


        
        GameObject entity = target;




        if (health > 0){
			health -= value;
            //Debug.LogWarning("h:" + health);
            if (isBoss) {
                bfsm.alertShot(entity);
            }
            else if (isGeneral) {
                fsm.alertShot(entity);
            }
            else if (isWheelChair) {
                wcfsm.alertShot(entity);
            }
        }
        //if the entity is dead and has not registered being dead, we alert the fsm
		if (health <= 0 && !dead){
            if (isBoss) {
                bfsm.alertDead();
            }
            else if (isGeneral) {
                fsm.alertDead(transform.forward);
            }
            else if (isWheelChair) {
                wcfsm.alertDead(transform.forward);
            }
            dead = true;
		}

	}





}
