using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicLaser : MonoBehaviour {

	public List<GameObject> Lasers;
	public float PeriodicTimer = 15f;
	float random = 5f;
	float baseTimer;
	public float BeamTime = 3f;
	float oldBeamTime = 3f;

	public string ActivationSound = "SmallLasers_Shoot";

	bool active;
	// Use this for initialization
	void Awake() {
		baseTimer = PeriodicTimer;
		oldBeamTime = BeamTime;
		PeriodicTimer = Random.Range(3f, 12f);
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Lasers.Count == 0) return;
		if (active) {
			BeamTime -= Time.deltaTime;
			if (BeamTime <= 0f) {
				BeamTime += oldBeamTime;
				active = false;
				Toggle();
			}
			return;
		}
		PeriodicTimer -= Time.deltaTime;
		if (PeriodicTimer <= 0f) {
			active = true;
			PeriodicTimer += baseTimer + Random.Range(-random, random);
			Toggle();
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.WeaponSounds, ActivationSound, 1f, 0.1f, transform.position, null);
		}
		
	}

	void Toggle() {
		for (int i = 0; i < Lasers.Count; i++) {
			Lasers[i].SetActive(active);
		}
	}
}
