using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ammoHUDScript : MonoBehaviour {

	private Text ammoText;
	private Text reloadText;
	
	void Awake()
	{
		ammoText = GameObject.FindGameObjectWithTag("ammoText").GetComponent<Text>() as Text;
		reloadText = (Text)GameObject.FindGameObjectWithTag("reloadText").GetComponent<Text>() as Text;
	}

	//Update the text on the HUD which shows total ammo and ammo in clip
	public void updateAmmoText(int totAmmo, int clipAmmo)
	{
		ammoText.text = totAmmo + " / " + clipAmmo;		
		
	}

	//Check whether the clip is less than a third full
	//if less than a third full, so reload warning
	protected void checkReloadWarning(int clipAmmo, int clipSize, int totAmmo)
	{
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
