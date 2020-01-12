using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class GameMaster : MonoBehaviour {
	public static GameMaster instance;
	public GameObject PlayerVehicle;
	public GameObject PlayerCamera;


	public float DepthMultiplier = 5f;
	public float DepthCounterBuzzOut = 1000f;

	List<float> respawnTimers = new List<float>();
	List<float> oldTimers = new List<float>();

	public TeleportBeacon CurrentBeacon;

	public enum PlayerStatus {
		NotInGame,
		Alive,
		Respawning
	}

	public List<PlayerStatus> PlayerState;
	public List<XboxController> Controllers;
	public List<GameObject> PlayerObjects = new List<GameObject>();
	public List<CameraFollowTarget> PlayerCameras = new List<CameraFollowTarget>();

	public GameObject SpawnEffect;
	public string SpawnSound = "WarpIn";
	[HideInInspector]
	public BonusActivator Bonus;
	// Use this for initialization
	void Awake() {
		instance = this;
		PlayerObjects.Clear();
		for (int i = 0; i < 4; i++) {
			respawnTimers.Add(5f);
			oldTimers.Add(respawnTimers[i]);
			PlayerObjects.Add(null);
			PlayerCameras.Add(null);
		}
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//read Inputs
		HandleJoins();
		//handle respawn timer
		HandleRespawnTimers(Time.deltaTime);
		//cheats
		if (Input.GetKeyDown(KeyCode.Insert)) {
			for (int i = 0; i < PlayerObjects.Count; i++) {
				if (PlayerObjects[i] != null) {
					PlayerObjects[i].transform.position = new Vector3(0, -1515f, 0f);
				}
			}
		}
		if (Input.GetKey(KeyCode.RightAlt) && Input.GetKeyDown(KeyCode.Alpha4)) {
			PlayerBank.instance.TeamMoney = 999999999f;
			for (int i = 0; i < PlayerObjects.Count; i++) {
				if (PlayerObjects[i] != null) {
					var s = PlayerObjects[i].GetComponent<PlayerInventory>();
					s.Dynamites += 250;
					s.RepairKits += 200;
					s.FuelCanisters += 100;
				}
			}
		}
		//cheats end
	}
	void Spawn(int playerInt) {
		Vector2 RandomSpawn = new Vector2(Random.Range(-3f, 3f), 4f);
		if (SpawnEffect != null) Instantiate(SpawnEffect, RandomSpawn, Quaternion.identity);
		PlayerObjects[playerInt] = Instantiate(PlayerVehicle, RandomSpawn, Quaternion.identity) as GameObject;
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, SpawnSound, 1f, 0f, PlayerObjects[playerInt].transform.position, null);
		var script = PlayerObjects[playerInt].GetComponent<PlayerId>();
		script.controller = Controllers[playerInt];
		var cam = PlayerCameras[playerInt];
		if (cam == null) {
			cam = Instantiate(PlayerCamera, RandomSpawn, Quaternion.identity).GetComponent<CameraFollowTarget>();
		}
		cam.TrackedTarget = PlayerObjects[playerInt].transform;
		cam.Init();
		PlayerCameras[playerInt] = cam;
		RecalcCamBounds();
	}

	void RecalcCamBounds() {
		List<Camera> cameras = new List<Camera>();
		for (int i = 0; i < PlayerCameras.Count; i++) {
			if (PlayerCameras[i] != null) {
				cameras.Add(PlayerCameras[i].GetComponent<Camera>());
			}
		}
		switch (cameras.Count - 1) {
			case 3:
				cameras[0].rect = new Rect(-0.5f, 0.5f, 1, 1);
				cameras[1].rect = new Rect(0.5f, 0.5f, 1, 1);
				cameras[2].rect = new Rect(-0.5f, -0.5f, 1, 1);
				cameras[3].rect = new Rect(0.5f, -0.5f, 1, 1);
				break;
			case 2:
				cameras[0].rect = new Rect(-0.5f, 0.5f, 1, 1);
				cameras[1].rect = new Rect(0.5f, 0.5f, 1, 1);
				cameras[2].rect = new Rect(0f, -0.5f, 1, 1);
				break;
			case 1:
				cameras[0].rect = new Rect(-0.5f, 0, 1, 1);
				cameras[1].rect = new Rect(0.5f, 0, 1, 1);
				break;
			case 0:
				cameras[0].rect = new Rect(0, 0, 1, 1);
				break;
			default:
				cameras[0].rect = new Rect(0, 0, 1, 1);
				break;
		}
	}

	void HandleJoins() {
		//XCI.GetAxis(XboxAxis.RightTrigger, controller);
		for (int i = 0; i < PlayerState.Count; i++) {
			if (PlayerState[i] == PlayerStatus.Alive && PlayerObjects[i] == null) {
				PlayerState[i] = PlayerStatus.Respawning;
			}
			if (PlayerState[i] == PlayerStatus.NotInGame && XCI.GetButton(XboxButton.A, Controllers[i])) {
				PlayerState[i] = PlayerStatus.Respawning;
				MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, "ChaChing", 1f, 0f, Vector3.zero, null);
			}
			else if (PlayerState[3] == PlayerStatus.NotInGame && Input.GetButtonDown("Jump")) {
				PlayerState[3] = PlayerStatus.Respawning;
				MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, "ChaChing", 1f, 0f, Vector3.zero, null);
			}
		}
	}
	void HandleRespawnTimers(float deltaTime) {
		for (int i = 0; i < PlayerState.Count; i++) {
			if (PlayerState[i] == PlayerStatus.Respawning) {
				respawnTimers[i] -= deltaTime;
				if (respawnTimers[i] <= 0f) {
					PlayerState[i] = PlayerStatus.Alive;
					Spawn(i);
					respawnTimers[i] = oldTimers[i];
				}
			}
		}
	}

	public void AttemptTeleport(GameObject g) {
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, "teslacharge", 0.7f, 0f, transform.position, null);
		if (CurrentBeacon == null) {
			return;
		}
		CurrentBeacon.Teleport(g);
	}

	public void NewBeacon(TeleportBeacon script) {
		if (CurrentBeacon != null) Destroy(CurrentBeacon.gameObject);
		CurrentBeacon = script;
	}
}
