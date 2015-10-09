using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ammoHUDScript : MonoBehaviour {

	private Text ammoText;
	private Text reloadText;

    private GameObject ammoTextObj;
    private GameObject reloadTextObj;

    void Awake()
	{
        ammoTextObj = GameObject.FindGameObjectWithTag(Tags.AMMOTEXT);
        reloadTextObj = GameObject.FindGameObjectWithTag(Tags.RELOADTEXT);
		//TODO uncommented
	}


    void Update() {
        
        if (ammoTextObj == null) {
            ammoTextObj = GameObject.FindGameObjectWithTag(Tags.AMMOTEXT);
            if (ammoTextObj != null) {
                Debug.LogWarning("ammotext assigned");
                ammoText = ammoTextObj.GetComponent<Text>();
            }
        }

        if (reloadTextObj == null) {
            reloadTextObj = GameObject.FindGameObjectWithTag(Tags.RELOADTEXT);
            if (reloadTextObj != null) {
                Debug.LogWarning("reload assigned");
                reloadText = reloadTextObj.GetComponent<Text>();
            }
        }
    }


	//Update the text on the HUD which shows total ammo and ammo in clip
	public void updateAmmoText(int totAmmo, int clipAmmo)
	{
        if (ammoText == null) {
            //Debug.LogError("Ammotext is null");
            //return;
            print ("HERE!!!");
			ammoTextObj = GameObject.FindGameObjectWithTag(Tags.AMMOTEXT);
			ammoText = ammoTextObj.GetComponent<Text>();
        }
		ammoText.text = totAmmo + " / " + clipAmmo;		
		//TODO uncommented
		
	}

	//Check whether the clip is less than a third full
	//if less than a third full, so reload warning
	public void checkReloadWarning(int clipAmmo, int clipSize, int totAmmo)
	{

        if (reloadText == null) {
            //Debug.LogError("reloadText is null");
            return;
        }
        if (ammoText == null ) {
            //Debug.LogError("Ammotext is null");
            return;
        }

        if (totAmmo != 0 && clipAmmo != 0)
		{
			reloadText.text = "Reload";

			if (clipAmmo/(clipSize * 1.0f) <= 0.33)
			{
				reloadText.enabled = true;
			}
			else
			{
				reloadText.enabled = false;
			}
		}
		else if (totAmmo == 0 && clipAmmo == 0)
		{
			reloadText.text = "Out of Ammo";
			reloadText.enabled = true;
		}



	}
}
