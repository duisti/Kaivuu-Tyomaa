using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesData : MonoBehaviour {
	public static UpgradesData instance;
	public List<float> DrillUpgrades;
	public List<float> JumpjetUpgrades;
	public List<float> HealthUpgrades;
	public List<float> CargoUpgrades;
	public List<float> FuelUpgrades;

	public List<float> PriceTable;

	// Use this for initialization
	void Awake() {
		instance = this;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
