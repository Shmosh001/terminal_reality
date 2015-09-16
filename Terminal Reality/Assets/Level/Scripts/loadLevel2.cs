using UnityEngine;
using System.Collections;

public class loadLevel2 : MonoBehaviour {

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			Application.LoadLevel ("LevelTwo");
		}

	}
}
