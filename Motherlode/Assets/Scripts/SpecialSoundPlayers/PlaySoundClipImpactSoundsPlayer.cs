using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundClipImpactSoundsPlayer : MonoBehaviour {

	public string ClipName1;
	public string ClipName2;
	public string ClipName3;

	public float volume = 0.4f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySound1() {
		if (ClipName1 != "")
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.ImpactSounds, ClipName1, volume, 0.03f, transform.position, null);
	}
	public void PlaySound2() {
		if (ClipName2 != "")
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.ImpactSounds, ClipName2, volume, 0.03f, transform.position, null);
	}
	public void PlaySound3() {
		if (ClipName3 != "")
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.ImpactSounds, ClipName3, volume, 0.03f, transform.position, null);
	}
}
