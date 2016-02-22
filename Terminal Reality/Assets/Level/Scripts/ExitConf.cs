using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitConf : MonoBehaviour {


    public GameObject go;

    public Button yes;
    public Button no;
    
    
    // Use this for initialization
	void Start () {
        yes.onClick.AddListener(() => { onYes(); });
        no.onClick.AddListener(() => { onNo(); });
    }
	
    public void onNo() {
        go.SetActive(false);
    }

    public void onYes() {
        Application.LoadLevel("MainMenu");
    }





}
