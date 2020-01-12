using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundClipEffectSound : MonoBehaviour {

	public List<string> possibleSounds;
	public bool InvokeOnStart = false;
	public float volume = 0.4f;
	// Use this for initialization
	void Start () {
		if (InvokeOnStart) PlaySound();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySound() {
		if (possibleSounds.Count == 0) return;
		string random = possibleSounds[Random.Range(0, possibleSounds.Count)];
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, random, volume, 0.03f, transform.position, null);
	}
}
