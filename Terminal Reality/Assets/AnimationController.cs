using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	private string[] animations;
	private string currentAnim;
	private Animator anim;






	// Use this for initialization
	void Start () {
		animations = new string[]	{
			"Run1",
			"Run2",
			"Run3",
			"Walk1",
			"Walk2",
			"Wander"};
		currentAnim = "Wander";
		anim = this.gameObject.GetComponent<Animator>();
		count = 0;
		size = 5;

	}


	private int count,size;
	// Update is called once per frame
	void Update () {

		//anim = new Animation();
		if(Input.GetKeyDown(KeyCode.Space)){
			//currentAnim = "Run1";
			//anim.Play(animations[3]);
			count++;
			if (count > 5){count = 0;}
			currentAnim = animations[count];


		
		}



	/*	if (Input.GetKeyDown(KeyCode.H)){
			currentAnim = "Run2";

		}*/
		anim.speed = 1;
		anim.Play(currentAnim);
	}


	void enterRunningAnimation(){}
	void enterDyingAnimation(){}
	void enterWanderingAnimation(){

	}
}
