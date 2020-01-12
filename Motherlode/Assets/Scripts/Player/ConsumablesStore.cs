using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
public class ConsumablesStore : MonoBehaviour {
	public CameraFollowTarget FollowScript;
	public Text MoneyText;
	public Text Fuel;
	float fuelPrice = 500f;
	public Text Health;
	float healthPrice = 3000f;
	public Text Dynamite;
	float dynamitePrice = 2000f;
	public Text Teleport;
	float teleportPrice = 3500f;
	public Text Beacon;
	float beaconPrice = 8500f;
	public Text Store;
	float storePrice = 50000f;
	public Text Dump;
	float dumpPrice = 35000f;

	public string BuySound = "ChaChing";
	public string ErrorSound = "ErrorSound";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MoneyText.text = "$" + Mathf.RoundToInt(PlayerBank.instance.TeamMoney);
		Fuel.text = "$" + fuelPrice;
		Health.text = "$" + healthPrice;
		Dynamite.text = "$" + dynamitePrice;
		Teleport.text = "$" + teleportPrice;
		Beacon.text = "$" + beaconPrice;
		Store.text = "$" + storePrice;
		Dump.text = "$" + dumpPrice;
		//inputs
		
		if (XCI.GetButtonDown(XboxButton.A, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire1")) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostFuel(fuelPrice);
		}
		if (XCI.GetButtonDown(XboxButton.B, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire3")) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostHealth(healthPrice);
		}
		if (XCI.GetButtonDown(XboxButton.X, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire2")) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostDynamite(dynamitePrice);
		}
		if ((XCI.GetButtonDown(XboxButton.Y, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire4")) && TacitusMission.instance.State == TacitusMission.QuestState.Completed) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostTeleports(teleportPrice);
		}
		if ((XCI.GetButtonDown(XboxButton.RightBumper, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire5")) && TacitusMission.instance.State == TacitusMission.QuestState.Completed) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostBeacons(beaconPrice);
		}
		if ((XCI.GetButtonDown(XboxButton.DPadLeft, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire6")) && TiberiumMission.instance.State == TiberiumMission.QuestState.Completed) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostStores(storePrice);
		}/*
		if (XCI.GetButtonDown(XboxButton.DPadRight, FollowScript.playersId.controller)) {
			BoostDumps(dumpPrice);
		}
		*/
	}
	void BoostFuel(float f) {
		if (PlayerBank.instance.TeamMoney  < f) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= f;
		FollowScript.playersInventory.FuelCanisters++;
	}
	void BoostHealth(float f) {
		if (PlayerBank.instance.TeamMoney < f) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= f;
		FollowScript.playersInventory.RepairKits++;
	}
	void BoostDynamite(float f) {
		if (PlayerBank.instance.TeamMoney < f) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= f;
		FollowScript.playersInventory.Dynamites++;
	}
	void BoostTeleports(float f) {
		if (PlayerBank.instance.TeamMoney < f) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= f;
		FollowScript.playersInventory.TeleportUses++;
	}
	void BoostBeacons(float f) {
		if (PlayerBank.instance.TeamMoney < f) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= f;
		FollowScript.playersInventory.TeleportBeacons++;
	}
	void BoostStores(float f) {
		if (PlayerBank.instance.TeamMoney < f) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= f;
		FollowScript.playersInventory.PocketStations++;
	}
	void BoostDumps(float f) {
		if (PlayerBank.instance.TeamMoney < f) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= f;
		FollowScript.playersInventory.PocketDumps++;
	}
}
