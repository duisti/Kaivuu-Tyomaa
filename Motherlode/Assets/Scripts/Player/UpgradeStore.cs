using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class UpgradeStore : MonoBehaviour {
	public CameraFollowTarget FollowScript;
	public Text MoneyText;
	public Text Fuel;
	public Text Health;
	public Text Cargo;
	public Text Drill;
	public Text Jets;

	public string BuySound = "ChaChing";
	public string ErrorSound = "ErrorSound";
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update() {
		MoneyText.text = "$" + Mathf.RoundToInt(PlayerBank.instance.TeamMoney);
		//deal with prices
		bool fuelUpsAvailable = true;
		if (FollowScript.playersUpgrades.CurrentFuel == UpgradesData.instance.FuelUpgrades.Count - 1) {
			Fuel.text = "No more upgrades";
			fuelUpsAvailable = false;
		}
		else Fuel.text = "$" + UpgradesData.instance.PriceTable[FollowScript.playersUpgrades.CurrentFuel + 1];

		bool healthUpsAvailable = true;
		if (FollowScript.playersUpgrades.CurrentHealth == UpgradesData.instance.HealthUpgrades.Count - 1) {
			Health.text = "No more upgrades";
			healthUpsAvailable = false;
		}
		else Health.text = "$" + UpgradesData.instance.PriceTable[FollowScript.playersUpgrades.CurrentHealth + 1];

		bool cargoUpsAvailable = true;
		if (FollowScript.playersUpgrades.CurrentCargo == UpgradesData.instance.CargoUpgrades.Count - 1) {
			Cargo.text = "No more upgrades";
			cargoUpsAvailable = false;
		}
		else Cargo.text = "$" + UpgradesData.instance.PriceTable[FollowScript.playersUpgrades.CurrentCargo + 1];

		bool drillUpsAvailable = true;
		if (FollowScript.playersUpgrades.CurrentDrill == UpgradesData.instance.DrillUpgrades.Count - 1) {
			Drill.text = "No more upgrades";
			drillUpsAvailable = false;
		}
		else Drill.text = "$" + UpgradesData.instance.PriceTable[FollowScript.playersUpgrades.CurrentDrill + 1];

		bool jetUpsAvailable = true;
		if (FollowScript.playersUpgrades.CurrentJumpJet == UpgradesData.instance.JumpjetUpgrades.Count - 1) {
			Jets.text = "No more upgrades";
			jetUpsAvailable = false;
		}
		else Jets.text = "$" + UpgradesData.instance.PriceTable[FollowScript.playersUpgrades.CurrentJumpJet + 1];

		//handle inputs
		
		if ((XCI.GetButtonDown(XboxButton.A, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire1")) && fuelUpsAvailable) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostFuel(bank);
		}
		if ((XCI.GetButtonDown(XboxButton.B, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire3")) && healthUpsAvailable) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostHealth(bank);
		}
		if ((XCI.GetButtonDown(XboxButton.X, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire2")) && cargoUpsAvailable) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostCargo(bank);
		}
		if ((XCI.GetButtonDown(XboxButton.Y, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire4")) && drillUpsAvailable) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostDrill(bank);
		}
		if ((XCI.GetButtonDown(XboxButton.RightBumper, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire5")) && jetUpsAvailable) {
			float bank = PlayerBank.instance.TeamMoney;
			BoostJet(bank);
		}
	}

	void BoostFuel(float b) {
		int i = FollowScript.playersUpgrades.CurrentFuel + 1;
		if (b < UpgradesData.instance.PriceTable[i]) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= UpgradesData.instance.PriceTable[i];
		FollowScript.playersUpgrades.CurrentFuel = i;
	}
	void BoostHealth(float b) {
		int i = FollowScript.playersUpgrades.CurrentHealth + 1;
		if (b < UpgradesData.instance.PriceTable[i]) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= UpgradesData.instance.PriceTable[i];
		FollowScript.playersUpgrades.CurrentHealth = i;
	}
	void BoostCargo(float b) {
		int i = FollowScript.playersUpgrades.CurrentCargo + 1;
		if (b < UpgradesData.instance.PriceTable[i]) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= UpgradesData.instance.PriceTable[i];
		FollowScript.playersUpgrades.CurrentCargo = i;
	}
	void BoostDrill(float b) {
		int i = FollowScript.playersUpgrades.CurrentDrill + 1;
		if (b < UpgradesData.instance.PriceTable[i]) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= UpgradesData.instance.PriceTable[i];
		FollowScript.playersUpgrades.CurrentDrill = i;
	}
	void BoostJet(float b) {
		int i = FollowScript.playersUpgrades.CurrentJumpJet + 1;
		if (b < UpgradesData.instance.PriceTable[i]) {
			print("not enough moneys!");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		PlayerBank.instance.TeamMoney -= UpgradesData.instance.PriceTable[i];
		FollowScript.playersUpgrades.CurrentJumpJet = i;
	}

}
