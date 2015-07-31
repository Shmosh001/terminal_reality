using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHealthScript : MonoBehaviour {

	playerDataScript playerData;

	public Text healthText;

	// Use this for initialization
	void Start () {
	
		playerData = this.GetComponent<playerDataScript>();
		updateHealthHUD();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//REDUCE PLAYER'S HEALTH BY DAMAGE//
	public void reducePlayerHealth(int damage)
	{
	}

	//INCREASE PLAYER'S HEALTH//
	public void increasePlayerHealth(int healthPoints)
	{
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
		healthText.text = playerData.health + "";
	}
}
