using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ammoHUDScript : MonoBehaviour {

	public Text ammoText;
	public Text reloadText;

    


	//Update the text on the HUD which shows total ammo and ammo in clip
	public void updateAmmoText(int totAmmo, int clipAmmo)
	{
        
		ammoText.text = totAmmo + " / " + clipAmmo;		
		
		
	}

	//Check whether the clip is less than a third full
	//if less than a third full, so reload warning
	public void checkReloadWarning(int clipAmmo, int clipSize, int totAmmo)
	{
		if (gameObject.tag == Tags.PLAYER1)
		{
			reloadText.transform.position = new Vector2( (Screen.width/4)-100,Screen.height/2);
		}
		if (gameObject.tag == Tags.PLAYER2)
		{
			reloadText.transform.position = new Vector2( 3*(Screen.width/4)-100,Screen.height/2);
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
		else if (clipAmmo/(clipSize * 1.0f) >= 0.3)
		{
			reloadText.enabled = false;
		}



	}
}
