using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSoundByDistance : MonoBehaviour {

	float originalVolume;
	AudioSource source;
	public float maxDist = 25f;
	float maxVolDist = 5f;
	// Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource>();
		if (source == null) return;
		originalVolume = source.volume;
		AdjustVolume();
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		AdjustVolume();
	}

	void AdjustVolume() {
		if (CamerasTracker.instance == null) return;
		int count = CamerasTracker.instance.Cameras.Count;
		/*
		if (count == 0) {
			source.volume = 0f;
			return;
		}
		*/
		float dist = maxDist + 1f;
		if (count != 0) {
			for (int i = 0; i < count; i++) {
				Vector3 vect = CamerasTracker.instance.Cameras[i].position;
				vect.z = transform.position.z;
				float f = Vector3.Distance(vect, transform.position);
				if (f < dist) {
					dist = f;
				}
			}
		} else {
			Vector3 vect = new Vector3(0, 0, 0);
			vect.z = transform.position.z;
			float f = Vector3.Distance(vect, transform.position);
			if (f < dist) {
				dist = f;
			}
		}

		if (dist < maxDist) {
			source.volume = Mathf.InverseLerp(maxDist, maxVolDist, dist) * originalVolume;
		}
		else source.volume = 0f;
	}
}
