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



    //PRIVATE VARS

    //boolean which determines if the entity has died or not
    private bool dead;
    //the finite state machine
	private ZombieFSM fsm;
    private BossZombieFSM bfsm;



    /// <summary>
    /// initialization
    /// </summary>
    void Start () {
        if (isBoss) {
            bfsm = gameObject.GetComponent<BossZombieFSM>();
        }
        else {
            fsm = gameObject.GetComponent<ZombieFSM>();
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
            else {
                fsm.alertDead();
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
	public void takeDamageN(int value, string tag, int id){
        //we check if the entity is alive and subtract the amount if it is and alert the fsm that the unit has been shot

        if (this.gameObject.GetComponent<PhotonView>().viewID != id) {
            return;
        }

        GameObject entity = GameObject.FindGameObjectWithTag(tag);

        if (entity == null) {
            Debug.LogError("tagged object not found");
            return;
        }

        if (health > 0){
			health -= value;
            //Debug.LogWarning("h:" + health);
            if (isBoss) {
               // Debug.LogWarning("Boss shot");
                bfsm.alertShot(entity);
            }
            else {
                fsm.alertShot(entity);
            }
        }
        //if the entity is dead and has not registered being dead, we alert the fsm
		if (health <= 0 && !dead){
            if (isBoss) {
                bfsm.alertDead();
            }
            else {
                fsm.alertDead();
            }
			dead = true;
		}

	}



   /* public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(health);
        }
        else {
            health = (int)stream.ReceiveNext();
        }
    }*/




    public void takeDamage(int value, string tag) {
        //we check if the entity is alive and subtract the amount if it is and alert the fsm that the unit has been shot

        GameObject entity = GameObject.FindGameObjectWithTag(tag);

        if (entity == null) {
            Debug.LogError("tagged object not found");
            return;
        }

        if (health > 0) {
            health -= value;
            //Debug.LogWarning("h:" + health);
            if (isBoss) {
               // Debug.LogWarning("Boss shot");
                bfsm.alertShot(entity);
            }
            else {
                fsm.alertShot(entity);
            }
        }
        //if the entity is dead and has not registered being dead, we alert the fsm
        if (health <= 0 && !dead) {
            if (isBoss) {
                bfsm.alertDead();
            }
            else {
                fsm.alertDead();
            }
            dead = true;
        }

    }
}
