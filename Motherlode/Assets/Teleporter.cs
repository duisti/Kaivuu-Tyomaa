using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	public Transform Destination;
	Vector3 dest;
	public GameObject Effect;
	public string SoundEffect = "";
	// Use this for initialization
	void Awake() {
		dest = Destination.position;
		dest.z = 0f;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.gameObject.GetComponent<PlayerId>()) {
			c.transform.position = dest;
			Instantiate(Effect, dest, Quaternion.identity);
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, SoundEffect, 1f, 0f, dest, null);
		}
	}
}
