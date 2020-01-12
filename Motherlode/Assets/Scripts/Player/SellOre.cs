using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class SellOre : MonoBehaviour {
	public CameraFollowTarget FollowScript;
	public Text MoneyText;
	public Text Sell;
	public string BuySound = "ChaChing";
	public string ErrorSound = "ErrorSound";

	public float SellMultiplier = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MoneyText.text = "$" + Mathf.RoundToInt(PlayerBank.instance.TeamMoney);
		//float rfFullAmount = Mathf.RoundToInt(FollowScript.playersHealth.MaxFuel - FollowScript.playersHealth.currentFuel) * RefillCostPerLitre + BaseRepairExtraCost;
		string sell = FollowScript.playersInventory.currentInventoryWeight + "kg for $" + FollowScript.playersInventory.InventoryValue;
		Sell.text = "Trade Goods; " + sell;
		
		if (XCI.GetButtonDown(XboxButton.A, FollowScript.playersId.controller)
			|| (FollowScript.playersId.GetControllerInt() == 4) && Input.GetButtonDown("Fire1")) {
			SellOres();
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
}
