using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHealthScript : MonoBehaviour {

	playerDataScript playerData;
	public UIBarScript UIBarScript;

	// Use this for initialization
	void Start () {
	
		playerData = this.GetComponent<playerDataScript>();
		updateHealthHUD();

	}
	
	// Update is called once per frame
	void Update () {

		//////////////
		//TEMP CODE//
		/////////////
		if (Input.GetKeyDown(KeyCode.KeypadMinus)) // decrease health //
		{
			if (playerData.health >= 5)
			{
				playerData.health -= 5;			
				updateHealthHUD();
			}
		}
		if (Input.GetKeyDown(KeyCode.KeypadPlus)) // increase health //
		{
			if (playerData.health <= 95)
			{
				playerData.health += 5;
				updateHealthHUD();
			}
		}
	
	}

	//REDUCE PLAYER'S HEALTH BY DAMAGE//
	public void reducePlayerHealth(int damage)
	{
		//If the damage received does NOT kill the player
		//i.e. damage does not make player health <= 0
		if ((playerData.health - damage) > 0)
		{
			playerData.health -= damage;
			updateHealthHUD();
		}
		//if the damage kills the player//
		else if ((playerData.health - damage) <= 0)
		{
			playerData.health = 0;
			updateHealthHUD();
			print ("PLAYER IS DEAD!!!"); //temp print out
		}

	}

	//INCREASE PLAYER'S HEALTH//
	public void increasePlayerHealth(int healthPoints)
	{

		//TODO: IF WE DECIDE ON DOING INCREMENTAL HEALING AND NOT ONLY FULL HEALS//

	}

	//FULL UP (MAX) PLAYER'S HEALTH//
	public void fullPlayerHealth()
	{
		playerData.health = 100;
		updateHealthHUD();
	}

	//UPDATE THE HEALTH DISPLAYED ON THE HUD//
	private void updateHealthHUD()
	{
		UIBarScript.UpdateValue(playerData.health, 100);
	}
}
