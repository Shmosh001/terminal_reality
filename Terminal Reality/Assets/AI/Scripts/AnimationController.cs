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
		/*animations = new string[]	{
			"Basic Run 01",
			"Basic Run 02",
			"Basic Run 03",
			"Basic Walk 01",
			"Basic Walk 02",
			"Etc Walk Zombi 01"};*/
		currentAnim = "Wander";
		anim = this.gameObject.GetComponent<Animator>();
		print (anim);
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
			if (count > size){
				count = 0;

			}
			currentAnim = animations[count];
			//print("space pressed");
		}
		anim.speed = 1;
		print (currentAnim);
		anim.Play(currentAnim);
		//print (anim.animation);

	}


	void enterRunningAnimation(){}
	void enterDyingAnimation(){}
	void enterWanderingAnimation(){}
}
