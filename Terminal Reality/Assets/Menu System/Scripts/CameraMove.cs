﻿using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {


    public Transform lead;
    public Transform lead2;
    public int offsetDistance;


	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(lead.position.x, transform.position.y, lead2.position.z + offsetDistance);
	}
}
