using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomMusicClip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M)) {
			string randomClip = MusicManager.instance.MusicTracks[Random.Range(0, MusicManager.instance.MusicTracks.Count)].name;
			MusicManager.instance.PlayFile(randomClip);
		}
	}
}
