using UnityEngine;
using System.Collections;

public class AIEntity <T>: MonoBehaviour{

	//could have this in an AIEntity class ad base method
	public void activateEntity(){
		this.gameObject.SetActive(true);
	}

	//could also be in base class
	//choses between 2 states randomly
	public T chooseAction(T state1, T state2){
		int choice = Random.Range(0,1);
		if (choice == 1){
			return state1;
		}
		else{
			return state2;
		}
	}
	//general class
	public float checkDistance(Transform entity1, Transform entity2){
		return Vector3.Distance(entity1,entity2);
	}
}
