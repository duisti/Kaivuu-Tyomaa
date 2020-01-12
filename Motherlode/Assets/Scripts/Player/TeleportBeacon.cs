using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBeacon : MonoBehaviour {
	public GameObject TeleportEffect;
	public string Sound = "teslashot";
	int minUses = 4;
	int addedRandom = 4;
	public int Uses = 10;
	// Use this for initialization
	void Start () {
		Uses = minUses + Random.Range(0, addedRandom);
		GameMaster.instance.NewBeacon(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Teleport(GameObject g) {
		if (TeleportEffect != null) {
			Instantiate(TeleportEffect, g.transform.position, Quaternion.identity);
			Instantiate(TeleportEffect, transform.position, Quaternion.identity);
		}
		Vector3 vect = new Vector3(transform.position.x, transform.position.y, g.transform.position.z);
		g.transform.position = vect;
		if (Sound != "") {
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, Sound, 1f, 0f, transform.position, null);
		}
		Uses--;
		if (Uses <= 0) {
			Destroy(gameObject);
		}
	}
}
