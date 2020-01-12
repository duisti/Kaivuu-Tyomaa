using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {
	public int CurrentDrill = 0;
	public int CurrentJumpJet = 0;
	public int CurrentHealth = 0;
	public int CurrentCargo = 0;
	public int CurrentFuel = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentDrill >= UpgradesData.instance.DrillUpgrades.Count) {
			CurrentDrill = UpgradesData.instance.DrillUpgrades.Count - 1;
		}
		if (CurrentJumpJet >= UpgradesData.instance.JumpjetUpgrades.Count) {
			CurrentJumpJet = UpgradesData.instance.JumpjetUpgrades.Count - 1;
		}
		if (CurrentHealth >= UpgradesData.instance.HealthUpgrades.Count) {
			CurrentHealth = UpgradesData.instance.HealthUpgrades.Count - 1;
		}
		if (CurrentCargo >= UpgradesData.instance.CargoUpgrades.Count) {
			CurrentCargo = UpgradesData.instance.CargoUpgrades.Count - 1;
		}
		if (CurrentFuel >= UpgradesData.instance.FuelUpgrades.Count) {
			CurrentFuel = UpgradesData.instance.FuelUpgrades.Count - 1;
		}
	}
}
