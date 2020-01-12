using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackAmountOfPlayers : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MusicManager.instance.PlayFile("e2m1");
	}
	
	// Update is called once per frame
	void Update () {
		int o = 0;
		for (int i = 0; i < GameMaster.instance.PlayerCameras.Count; i++) {
			if (GameMaster.instance.PlayerCameras[i] != null) {
				o++;
			}
		}
		if (o != 0) {
			Destroy(gameObject);
		}
	}
}
