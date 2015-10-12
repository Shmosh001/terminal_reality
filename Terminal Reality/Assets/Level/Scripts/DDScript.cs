﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DDScript : MonoBehaviour {

    private AudioSource audioSource;
    private Animator anim;
	public bool open;
    //private Text pushE;

    public GameObject needKey1;
    public GameObject needKey2;
    public GameObject doorCounterpart;



	// Use this for initialization
	void Start () 
	{
        audioSource = GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
		open = false;
		
		
	}


    //WHEN THE PLAYER INTERACTS WITH THE DOOR//
    public void interaction()
	{
		//IF THE DOOR IS CLOSED//
		if(!open)
		{
            //play sound of this component

            audioSource.Play();
            open = true;

            anim.SetTrigger("OpenFWD");
            doorCounterpart.GetComponent<DDScript>().interaction();
		}
	}


	//WHEN SOMETHING ENTERS THE DOORS TRIGGER//
	void OnTriggerEnter (Collider other)
	{

        Debug.Log(other.name);
		//IF A PLAYER ENTERS THE DOOR'S TRIGGER//
		if (other.tag == Tags.PLAYER1)
		{
			if (!open) //Only show hint if the door is closed
			{
				//if the player has a key - show "Push E"
				if (other.GetComponentInParent<playerDataScript>().hasKey)
				{
					//pushE.enabled = true;
				}
				//if the player does not have a key - show "Need Key"
				else{
                    Debug.Log("enabled");
                    //needKey1.SetActive(true);
				}
			}
		}


        if (other.tag == Tags.PLAYER2) {
            if (!open) //Only show hint if the door is closed
            {

				//if the player has a key - show "Push E"
				if (other.GetComponentInParent<playerDataScript>().hasKey)
				{
					//pushE.enabled = true;
				}
				//if the player does not have a key - show "Need Key"
				else{
                    //needKey2.SetActive(true);
                }

            }
        }
        if (!open && other.tag == Tags.ENEMY ) {
            other.GetComponent<ZombieFSM>().stopWandering();

        }

    }
	
	//WHEN SOMETHING LEAVES THE DOOR'S TRIGGER//
	void OnTriggerExit (Collider other)
	{
		//IF A PLAYER LEAVES THE DOOR'S TRIGGER//
		if (other.tag == Tags.PLAYER1)
		{
            //pushE.enabled = false;
            //needKey1.SetActive(false);
        }

        else if(other.tag == Tags.PLAYER2)
        {
            //needKey2.SetActive(false);
        }

	}





}
