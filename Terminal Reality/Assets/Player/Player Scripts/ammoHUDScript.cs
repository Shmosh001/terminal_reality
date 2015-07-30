using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ammoHUDScript : MonoBehaviour {

	public Text ammoText;


	protected void updateAmmoText(int totAmmo, int clipAmmo)
	{
		ammoText.text = totAmmo + " / " + clipAmmo;
	}
}
