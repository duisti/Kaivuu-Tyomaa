using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerStatuses : MonoBehaviour {
	public Text HealthText;
	public Text FuelText;
	public Text CargoText;
	public Text DepthText;
	public Text MoneyText;
	public Text StatsText;
	public Text ConsumablesText;
	public CameraFollowTarget FollowScript;
	public Image Flasher;
	float oldHealth;
	float flashTime = 0.5f;

	float MaxDepth = 0f;
	int depthInt = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (FollowScript == null) {
			FollowScript = transform.parent.GetComponent<CameraFollowTarget>();
			return;
		}
		if (FollowScript.TrackedTarget == null) return;
		HealthText.text = "Hull: " + Mathf.RoundToInt(FollowScript.playersHealth.health) + "/" + Mathf.RoundToInt(FollowScript.playersHealth.MaxHealth);
		FuelText.text = "Fuel (Litres): " + Mathf.RoundToInt(FollowScript.playersHealth.currentFuel) + "/" + Mathf.RoundToInt(FollowScript.playersHealth.MaxFuel);
		CargoText.text = "Cargo (Kg): " + Mathf.RoundToInt(FollowScript.playersInventory.currentInventoryWeight) + "/" + Mathf.RoundToInt(FollowScript.playersInventory.MaxInventoryWeight) + 
			"\nCargo ($$$): " + Mathf.RoundToInt(FollowScript.playersInventory.InventoryValue);
		MoneyText.text = "$" + Mathf.RoundToInt(PlayerBank.instance.TeamMoney);
		//stats text
		int a = UpgradesData.instance.DrillUpgrades.Count - 1;
		int b = UpgradesData.instance.JumpjetUpgrades.Count - 1;
		int c = UpgradesData.instance.CargoUpgrades.Count - 1;
		int d = UpgradesData.instance.HealthUpgrades.Count - 1;
		int e = UpgradesData.instance.FuelUpgrades.Count - 1;
		StatsText.text = "Upgrades" +
			"\nDrill " + FollowScript.playersUpgrades.CurrentDrill + "/" + a +
			"\nBoosters " + FollowScript.playersUpgrades.CurrentJumpJet + "/" + b +
			"\nCapacity " + FollowScript.playersUpgrades.CurrentCargo + "/" + c + 
			"\nHull " + FollowScript.playersUpgrades.CurrentHealth + "/" + d +
			"\nFuel " + FollowScript.playersUpgrades.CurrentFuel + "/" + e;
		//stats text end
		//consumables
		ConsumablesText.text = FollowScript.playersInventory.FuelCanisters +
			"\n" + FollowScript.playersInventory.RepairKits +
			"\n" + FollowScript.playersInventory.Dynamites +
			"\n" + FollowScript.playersInventory.TeleportUses +
			"\n" + FollowScript.playersInventory.TeleportBeacons +
			"\n" + FollowScript.playersInventory.PocketStations +
			"\n" + FollowScript.playersInventory.PocketDumps;
			
		//consumables end
		//color
		Color co = Flasher.color;
		co.a = Mathf.MoveTowards(co.a, 0f, flashTime * Time.unscaledDeltaTime);
		Flasher.color = co;
		AttemptFlash();
		oldHealth = FollowScript.playersHealth.health;
		//colorend
		//depth
		CalcDepth();
	}

	void AttemptFlash() {
		if (oldHealth > FollowScript.playersHealth.health) {
			Color c = Flasher.color;
			c.a = 0.5f;
			Flasher.color = c;
		}
	}

	void CalcDepth() {
		float f = FollowScript.TrackedTarget.transform.position.y * GameMaster.instance.DepthMultiplier;
		if (-MaxDepth > f && depthInt < GameMaster.instance.Bonus.Steps.Count) {
			MaxDepth = f;
			if (MaxDepth < -GameMaster.instance.Bonus.Steps[depthInt]) {
				
				GameMaster.instance.Bonus.ActivateBonus(depthInt);
				depthInt++;
			}
		}
			if (Mathf.Abs(f) > GameMaster.instance.DepthCounterBuzzOut * GameMaster.instance.DepthMultiplier) {
			f = Random.Range(-10000, -99999);
		}
		f = Mathf.RoundToInt(f);
		DepthText.text = "Depth: " + f + "ft";
	}

}
