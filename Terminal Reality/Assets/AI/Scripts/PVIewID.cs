using UnityEngine;
using System.Collections;

public class PVIewID : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




    void OnGUI() {

        Vector3 characterPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));

        characterPos = new Vector3(Mathf.Clamp(characterPos.x, 0 + (60 / 2), Screen.width - (60 / 2)),
                                        Mathf.Clamp(characterPos.y, 50, Screen.height),
                                        characterPos.z);

        GUILayout.BeginArea(new Rect((characterPos.x + 20) - (60 / 2), (Screen.height - characterPos.y) + 20, 60, 40));

        GUILayout.Label(gameObject.GetComponent<PhotonView>().viewID.ToString());

        GUILayout.EndArea();
    }
    
}
