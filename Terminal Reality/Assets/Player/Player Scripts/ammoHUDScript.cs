using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ammoHUDScript : MonoBehaviour {

	public Text ammoText;
	public Text reloadText;

	//Update the text on the HUD which shows total ammo and ammo in clip
	protected void updateAmmoText(int totAmmo, int clipAmmo)
	{
		ammoText.text = totAmmo + " / " + clipAmmo;
	}

	//Check whether the clip is less than a third full
	//if less than a third full, so reload warning
	protected void checkReloadWarning(int clipAmmo, int clipSize)
	{
		if (clipAmmo/(clipSize * 1.0f) <= 0.33)
		{
			reloadText.enabled = true;
		}
		else
		{
			reloadText.enabled = false;
		}
	}
}
