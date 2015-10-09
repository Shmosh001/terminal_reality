using UnityEngine;
using System.Collections;

public class WheelchairHash : MonoBehaviour {



    public static int idleTrigger;
    public static int attackingTrigger;
    public static int patrolingTrigger;
    public static int chasingTrigger;
    public static int movingBool;
    



    // Use this for initialization
    void Start () {
        idleTrigger = Animator.StringToHash("Idle");
        attackingTrigger = Animator.StringToHash("Attack");
        patrolingTrigger = Animator.StringToHash("Patrol");
        chasingTrigger = Animator.StringToHash("Chasing");
        movingBool = Animator.StringToHash("Moving");
    }
	

}
