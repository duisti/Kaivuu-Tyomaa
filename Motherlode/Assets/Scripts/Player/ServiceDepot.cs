using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class ServiceDepot : MonoBehaviour {
	public CameraFollowTarget FollowScript;
	public Text MoneyText;
	public Text Sell;
	public Text RepairText;
	public string BuySound = "ChaChing";
	public string ErrorSound = "ErrorSound";

	float RepairCostPerHP = 50f;
	float BaseRepairExtraCost = 200f;
	float SellMultiplier = 0.75f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MoneyText.text = "$" + Mathf.RoundToInt(PlayerBank.instance.TeamMoney);
		//float rfFullAmount = Mathf.RoundToInt(FollowScript.playersHealth.MaxFuel - FollowScript.playersHealth.currentFuel) * RefillCostPerLitre + BaseRepairExtraCost;
		string sell = FollowScript.playersInventory.currentInventoryWeight + "kg for $" + FollowScript.playersInventory.InventoryValue * SellMultiplier;
		Sell.text = "Trade Goods; " + sell;

		string repString = Mathf.RoundToInt(FollowScript.playersHealth.MaxHealth - FollowScript.playersHealth.health) + "/" + Mathf.RoundToInt(FollowScript.playersHealth.MaxHealth);
		RepairText.text = "Repair, " + repString + " ($" + RepairCostPerHP + " per HP + $200 tax)";
		
		if (XCI.GetButtonDown(XboxButton.A, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire1")) {
			SellOres();
		}
		if (XCI.GetButtonDown(XboxButton.B, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire3")) {
			float bank = PlayerBank.instance.TeamMoney;
			Repair(bank);
		}
	}

	void SellOres() {
		if (FollowScript.playersInventory.InventoryValue <= 0) {
			print("ei tarvi myyä");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		FollowScript.playersInventory.ClearInventory(SellMultiplier);
		
	}

	void Repair(float money) {
		float amountNeedToRepair = Mathf.Abs(FollowScript.playersHealth.health - FollowScript.playersHealth.MaxHealth);
		if (amountNeedToRepair < 0.01f) {
			print("too early");
			return;
		}
		float cost = amountNeedToRepair * RepairCostPerHP;
		float takeOffBase = money - BaseRepairExtraCost;
		if (takeOffBase < 0) {
			print("ei oo rahea");
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, ErrorSound, 1f, 0f, transform.position, null);
			return;
		}
		float spare = takeOffBase - cost;
		if (spare < 0) {
			float f = amountNeedToRepair - (Mathf.Abs(spare) / RepairCostPerHP);
			amountNeedToRepair = f;
			cost = amountNeedToRepair * RepairCostPerHP;
		}
		cost += BaseRepairExtraCost;
		//make them pay
		PlayerBank.instance.TeamMoney -= cost;
		//give repair
		FollowScript.playersHealth.health += amountNeedToRepair;
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, BuySound, 1f, 0f, transform.position, null);
		//otetaan pois base hinta
		//-10
		//2 - ((abs-10)/20)
		//1.5 pinnaa
		//kerrotaan 20 = 30 maksua

		//tilanne #2
		//tarvitaan 3
		//maksaa 60
		//rahaa 60
		//base hinta pois, rahaa 50 (-10)
		// 50 - 60 = -10
		//3 - ((abs-10/20)
		//2.5 pinnaa
		//kerrotaan 20 = 50 maksua

		//tilanne #3
		//tarvitaan 3
		//maksaa 60
		//rahaa 5
		//base hinta pois, rahaa -5
		//tässä kohtaa heitetään error, ei riitä rahet

	}
}
