using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {

		public float minIntensity = 0.0f; 
		public float maxIntensity = 1.5f;		
		float random;
		
		void Start()
		{
			random = Random.Range(0.0f, 65535.0f);
		}
		
		void Update()
		{
			float noise = Mathf.PerlinNoise(random, Time.time);
			light.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);

		}
	
}
