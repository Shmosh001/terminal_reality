using UnityEngine;
using System.Collections;

public class LevelCreator : MonoBehaviour {

    //public GameObject parent;


    public GameObject newPrefab;
    public GameObject parent;



	// Use this for initialization
	void Start () 
	{

        for (int i = 0; i < 8; i++) {
            Instantiate(newPrefab, parent.transform.GetChild(i).transform.position, parent.transform.GetChild(i).transform.rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
