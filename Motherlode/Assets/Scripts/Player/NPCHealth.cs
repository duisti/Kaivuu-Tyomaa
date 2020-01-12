using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour {
	public float MaxHealth = 10f;
	public float health = 10f;
	Transform possibleParent;
	//can be null
	public GameObject DeathGO;

	public enum CurrentStatus {
		Normal,
		Stunned,
		Teleporting
	}
	public CurrentStatus State = CurrentStatus.Normal;
	public bool UsesFuel = false;
	public float MaxFuel = 25f;
	public float currentFuel = 25f;

	Upgrades upgrades;
	// Use this for initialization
	void Start () {
		possibleParent = transform.parent;
		if (UsesFuel) {
			upgrades = GetComponent<Upgrades>();
			UpdateStats();
			currentFuel = MaxFuel;
			health = MaxHealth;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (UsesFuel) {
			UpdateStats();
			if (currentFuel <= 0) {
				Damage(health);
			}
		}
	}

	public void Refill() {
		currentFuel = MaxFuel;
	}
	public void Repair() {
		health = MaxHealth;
	}

	void UpdateStats() {
		MaxFuel = UpgradesData.instance.FuelUpgrades[upgrades.CurrentFuel];
		MaxHealth = UpgradesData.instance.HealthUpgrades[upgrades.CurrentHealth];
	}

	public void Damage(float f) {
		health -= f;
		if (health <= 0f) {
			Kill();
		}
	}

	void Kill() {
		var go = this.gameObject;
		if (possibleParent != null) {
			go = possibleParent.gameObject;
		}
		if (DeathGO != null) {
			Instantiate(DeathGO, transform.position, Quaternion.identity);
		}
		Destroy(go);
	}
}
