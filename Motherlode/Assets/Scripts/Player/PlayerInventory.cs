using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
public class PlayerInventory : MonoBehaviour {

	public float MaxInventoryWeight = 100f;
	public float currentInventoryWeight = 0f;
	[SerializeField]
	public float InventoryValue = 0f;

	NPCHealth healthScript;
	PlayerId idScript;

	Upgrades upgrades;

	public int Dynamites = 0;
	public GameObject DynamitePrefab;
	public string DynamiteDropSound = "Throw_Object";
	public int RepairKits = 0;
	float repairAmount = 10f;
	public string RepairSound = "Hammer_Repair";
	public int FuelCanisters = 0;
	float refillAmount = 5f;
	public string RefillSound = "BlobSound";
	public int TeleportUses = 0;
	public string TeleportSound = "teslacharge";
	public int TeleportBeacons = 0;
	public GameObject BeaconPrefab;
	public string BeaconSound = "LimpPlant";
	public int PocketStations = 0;
	public string PocketStationSound = "DrillDeploying";
	public GameObject PocketStationPrefab;
	public int PocketDumps = 0;
	public string PocketDumpSound = "DrillDeploying";
	//the object which you place
	//the object which you use to teleport

	
	// Use this for initialization
	void Awake() {
		upgrades = GetComponent<Upgrades>();
		healthScript = GetComponent<NPCHealth>();
		idScript = GetComponent<PlayerId>();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MaxInventoryWeight = UpgradesData.instance.CargoUpgrades[upgrades.CurrentCargo];
		if (idScript.Shopping) return;
		if (XCI.GetButtonDown(XboxButton.X, idScript.controller)
			|| (idScript.GetControllerInt() == 4) && Input.GetButtonDown("Fire2")) {
			DropDynamite();
		}
		if (XCI.GetButtonDown(XboxButton.A, idScript.controller)
			|| (idScript.GetControllerInt() == 4) && Input.GetButtonDown("Fire1")) {
			Refill();
		}
		if (XCI.GetButtonDown(XboxButton.B, idScript.controller)
			|| (idScript.GetControllerInt() == 4) && Input.GetButtonDown("Fire3")) {
			Repair();
		}
		if (XCI.GetButtonDown(XboxButton.Y, idScript.controller)
			|| (idScript.GetControllerInt() == 4) && Input.GetButtonDown("Fire4")) {

			if (TacitusMission.instance.State == TacitusMission.QuestState.Completed) {
				Teleport();
			} else MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, "ErrorSound", 1f, 0f, transform.position, null);
		}
		if (XCI.GetButtonDown(XboxButton.RightBumper, idScript.controller)
			|| (idScript.GetControllerInt() == 4) && Input.GetButtonDown("Fire5")) {
			if (TacitusMission.instance.State == TacitusMission.QuestState.Completed && healthScript.transform.position.y > -1490f) {
				Beacon();
			} else MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, "ErrorSound", 1f, 0f, transform.position, null);

		}
		if (XCI.GetButtonDown(XboxButton.DPadLeft, idScript.controller)
			|| (idScript.GetControllerInt() == 4) && Input.GetButtonDown("Fire6")) {
			if (TiberiumMission.instance.State == TiberiumMission.QuestState.Completed && healthScript.transform.position.y > -1490f) {
				PocketStation();
			} else MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, "ErrorSound", 1f, 0f, transform.position, null);
		}
	}

	void DropDynamite() {
		if (Dynamites <= 0) {
			return;
		}
		Vector3 randomRot = new Vector3(0, 0, Random.Range(0, 359.9f));
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.WeaponSounds, DynamiteDropSound, 1f, 0f, transform.position, null);
		Rigidbody2D rg = Instantiate(DynamitePrefab, transform.position + Vector3.up * 0.5f, Quaternion.Euler(randomRot)).GetComponent<Rigidbody2D>();
		rg.velocity = transform.GetComponent<Rigidbody2D>().velocity;
		Dynamites--;

	}

	void Refill() {
		if (FuelCanisters <= 0) {
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, RefillSound, 1f, 0f, transform.position, null);
		healthScript.currentFuel = Mathf.MoveTowards(healthScript.currentFuel, healthScript.MaxFuel, refillAmount);
		FuelCanisters--;
	}
	void Repair() {
		if (RepairKits <= 0) {
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, RepairSound, 1f, 0f, transform.position, null);
		healthScript.health = Mathf.MoveTowards(healthScript.health, healthScript.MaxHealth, repairAmount);
		RepairKits--;
	}

	void Teleport() {
		if (TeleportUses <= 0 || GameMaster.instance.CurrentBeacon == null) {
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, TeleportSound, 1f, 0f, transform.position, null);
		GameMaster.instance.AttemptTeleport(this.gameObject);
		TeleportUses--;
	}

	void Beacon() {
		if (TeleportBeacons <= 0) {
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BeaconSound, 1f, 0f, transform.position, null);
		Rigidbody2D rg = Instantiate(BeaconPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity).GetComponent<Rigidbody2D>();
		rg.velocity = transform.GetComponent<Rigidbody2D>().velocity;
		TeleportBeacons--;
	}

	void PocketStation() {
		if (PocketStations <= 0) {
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, PocketStationSound, 1f, 0f, transform.position, null);
		Instantiate(PocketStationPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
		//rg.velocity = transform.GetComponent<Rigidbody2D>().velocity;
		PocketStations--;
	}

	public void ClearInventory(float multiplier) {
		//add money to players
		PlayerBank.instance.TeamMoney += InventoryValue * multiplier;
		//play some money sound? :D
		InventoryValue = 0f;
		currentInventoryWeight = 0f;
	}

	public void AddToInventory(float value, float weight, string oreName) {
		float f = currentInventoryWeight + weight;
		if (f > MaxInventoryWeight) {
			print("inventory is full");
			//invoke floating text with inventory full message
			return;
		}
		InventoryValue += value;
		currentInventoryWeight = f;
		//invoke floating text with ore gathered
		//play bing bing wahoo sound
	}
}
