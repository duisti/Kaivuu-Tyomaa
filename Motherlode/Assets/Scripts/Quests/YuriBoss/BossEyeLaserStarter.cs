using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeLaserStarter : MonoBehaviour {

	public string WarningSound = "EyeLaserWarning";
	public List<string> Messages;

	[SerializeField]
	float shootCountDown = 2.4f;
	[SerializeField]
	public float timer = 9f;
	public float BaseTimer = 9f;
	float timerThrow = 1.5f;
	[SerializeField]
	float lasering = 3.5f;

	public GameObject Laser;
	public List<ConstantRotator> SpinningLasers;

	TrackTarget tracker;
	// Use this for initialization
	void Awake() {
		SetTimer();
	}

	void Start () {
		tracker = GetComponent<TrackTarget>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Laser.activeSelf) {
			lasering -= Time.deltaTime;
			if (lasering <= 0f) {
				lasering = 3.5f;
				Laser.SetActive(false);
				SetTimer();
				SwitchTarget();
			}
			return;
		}
		timer -= Time.deltaTime;
		if (timer <= 0f) {
			if (shootCountDown == 2.4f) {
				MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.AnnouncerSounds, Messages[Random.Range(0, Messages.Count)], 1f, 0f, transform.position, transform.parent);
				MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.WeaponSounds, WarningSound, 1f, 0.1f, transform.position, transform.parent);
			}
			shootCountDown -= Time.deltaTime;
			if (shootCountDown <= Time.deltaTime) {
				shootCountDown = 2.4f;
				Laser.SetActive(true);
			}
		}
	}
	void SwitchTarget() {
		List<GameObject> possibilities = new List<GameObject>();
		Vector3 vect = transform.position;
		vect.z = 0f;
		for (int i = 0; i < GameMaster.instance.PlayerObjects.Count; i++) {
			if (GameMaster.instance.PlayerObjects[i] != null && Vector3.Distance(vect, GameMaster.instance.PlayerObjects[i].transform.position) < 100f) {
				possibilities.Add(GameMaster.instance.PlayerObjects[i]);
			}
		}
		Transform target = null;
		if (possibilities.Count != 0) {
			target = possibilities[Random.Range(0, possibilities.Count)].transform;
		}
		if (target != null) {
			tracker.TrackedTarget = target;
		}
		if (SpinningLasers.Count != 0) {
			if (Random.Range(0, 100) < 50) {
				for (int i = 0; i < SpinningLasers.Count; i++) {
					SpinningLasers[i].Speed *= -1f;
				}
			}
		}
	}

	void SetTimer() {
		timer = Random.Range(-timerThrow, timerThrow) + BaseTimer;
	}
}

