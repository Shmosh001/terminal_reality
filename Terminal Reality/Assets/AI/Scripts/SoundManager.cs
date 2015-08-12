using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip maleScream;
	public AudioClip femaleScream;


	public void playMaleScream(Vector3 position){
		playAudioClip(maleScream,position);
	}

	public void playFemaleScream(Vector3 position){
		playAudioClip(femaleScream,position);
	}





	void playAudioClip(AudioClip clip, Vector3 position){
		AudioSource.PlayClipAtPoint(clip, position);
	}

}
