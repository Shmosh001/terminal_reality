using UnityEngine;
using System.Collections;

public class WheelchairAnimationController : MonoBehaviour {


    //main animator
    private Animator main;
    //wheel animators
    private Animator backWheelL;
    private Animator backWheelR;
    private Animator frontWheelL;
    private Animator frontWheelR;

    


    private bool moving;
    private bool reset;

    // Use this for initialization
    void Start() {
        
        main = gameObject.GetComponent<Animator>();
        frontWheelL = gameObject.transform.GetChild(0).GetChild(2).GetComponentInChildren<Animator>();
        frontWheelR = gameObject.transform.GetChild(0).GetChild(3).GetComponentInChildren<Animator>();
        backWheelL = gameObject.transform.GetChild(0).GetChild(4).GetComponentInChildren<Animator>();
        backWheelR = gameObject.transform.GetChild(0).GetChild(5).GetComponentInChildren<Animator>();
        moving = false;
        reset = true;
    }


    public void enableMotion() {
        frontWheelL.SetBool(WheelchairHash.movingBool, true);
        frontWheelR.SetBool(WheelchairHash.movingBool, true);
        backWheelL.SetBool(WheelchairHash.movingBool, true);
        backWheelR.SetBool(WheelchairHash.movingBool, true);
    }

    public void disableMotion() {
        frontWheelL.SetBool(WheelchairHash.movingBool, false);
        frontWheelR.SetBool(WheelchairHash.movingBool, false);
        backWheelL.SetBool(WheelchairHash.movingBool, false);
        backWheelR.SetBool(WheelchairHash.movingBool, false);
    }


    /// <summary>
    /// sets a trigger 
    /// </summary>
    /// <param name="name">
    /// hash of the trigger
    /// </param>

    public void setTriggerWC(int name) {

        
       
            main.SetTrigger(name);

            if (name == WheelchairHash.patrolingTrigger || name == WheelchairHash.chasingTrigger) {
                enableMotion();
            }
            else {
                disableMotion();
            }
        



    }





    /// <summary>
    /// sets a boolean to true/false
    /// </summary>
    /// <param name="name">
    /// hash of boolean
    /// </param>
    /// <param name="value">
    /// value
    /// </param>

    public void setBooleanWC(int name, bool value) {

        
        if (main.isActiveAndEnabled) {
            main.SetBool(name, value);
        }
    }


}
