using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHealthScript : MonoBehaviour {

	playerDataScript playerData;
	public UIBarScript UIBarScript;

	// Use this for initialization
	void Start () {
	
		playerData = this.GetComponent<playerDataScript>();
		//updateHealthHUD();
		//TODO uncommented

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//REDUCE PLAYER'S HEALTH BY DAMAGE//
	public void reducePlayerHealth(int damage)
	{
		//If the damage received does NOT kill the player
		//i.e. damage does not make player health <= 0
		if ((playerData.health - damage) > 0)
		{
			print ("Health: " + playerData.health);
			playerData.health -= damage;
			//updateHealthHUD();
			//TODO uncommented
		}
		//if the damage kills the player//
		else if ((playerData.health - damage) <= 0)
		{
			playerData.health = 0;
			//updateHealthHUD();
			//TODO uncommented
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
		//updateHealthHUD();
		//TODO uncommented
	}

	//UPDATE THE HEALTH DISPLAYED ON THE HUD//
	private void updateHealthHUD()
	{
		UIBarScript.UpdateValue(playerData.health, 100);
	}
}
