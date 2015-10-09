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

    public static int movingParam;


    private bool moving;
    private bool reset;

    // Use this for initialization
    void Start() {
        movingParam = Animator.StringToHash("Moving");
        main = gameObject.GetComponent<Animator>();
        frontWheelL = gameObject.transform.GetChild(0).GetChild(2).GetComponentInChildren<Animator>();
        frontWheelR = gameObject.transform.GetChild(0).GetChild(3).GetComponentInChildren<Animator>();
        backWheelL = gameObject.transform.GetChild(0).GetChild(4).GetComponentInChildren<Animator>();
        backWheelR = gameObject.transform.GetChild(0).GetChild(5).GetComponentInChildren<Animator>();
        moving = false;
        reset = true;
    }




    void enableAnimation() {
        main.SetBool(movingParam, true);
        frontWheelL.SetBool(movingParam, true);
        frontWheelR.SetBool(movingParam, true);
        backWheelL.SetBool(movingParam, true);
        backWheelR.SetBool(movingParam, true);
    }

    void stopAnimation() {
        main.SetBool(movingParam, false);
        frontWheelL.SetBool(movingParam, false);
        frontWheelR.SetBool(movingParam, false);
        backWheelL.SetBool(movingParam, false);
        backWheelR.SetBool(movingParam, false);
    }


    // Update is called once per frame
    void Update() {
        if (!reset && moving) {
            enableAnimation();
            reset = true;
        }
        else if (!reset && !moving) {
            reset = true;
            stopAnimation();
        }
    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {




        if (stream.isWriting) {
            stream.SendNext(moving);
        }
        //receiving other players things
        else {
            bool temp = (bool)stream.ReceiveNext();
            if (temp != moving) {
                reset = false;
                
            }
            moving = temp;
        }

    }

}
