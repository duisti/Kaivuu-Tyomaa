using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour {

	public enum PickableType {
		Inventory,
		Instant
	}
	public PickableType Type = PickableType.Inventory;

	public float Damage = 0f;

	public float Value = 10f;
	//weight only applied if pickabletype = inventory
	public float Weight = 5f;
	//can be null
	public GameObject SpawnedObject;
	Transform parent;

	NPCHealth healthScript;
	[Tooltip("Leave as '' if you don't want a floating text.")]
	
	public string PickUpText = "";
	public string PickUpSound = "";
	public Color TextColor = Color.green;
	// Use this for initialization
	void Start () {
		parent = transform.parent;
		healthScript = GetComponent<NPCHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c) {
		var script = c.gameObject.GetComponent<PlayerInventory>();
		if (script == null) return;
		float oldWeight = script.currentInventoryWeight;
		if (Type == PickableType.Inventory) {
			script.AddToInventory(Value, Weight, parent.name);
		} else if (Type == PickableType.Instant) {
			PlayerBank.instance.TeamMoney += Value;
		}
		if (SpawnedObject != null)
			Instantiate(SpawnedObject, transform.position, Quaternion.identity);
		if (Damage != 0f) {
			var s = c.gameObject.GetComponent<NPCHealth>();
			if (s != null) {
				s.Damage(Damage);
			}
		}
		//play a sound!
		if (oldWeight + Weight <= script.MaxInventoryWeight) {
			if (PickUpSound != "")
				MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.EffectSounds, PickUpSound, 0.7f, 0.02f, transform.position, null);
			if (PickUpText != "") {
				string te = "";
				if (Value != 0f) {
					te = "+" + PickUpText + " ($" + Value + ")";
				}
				else te = PickUpText;
				FloatingTextSpawner.instance.SpawnFloatingText(te, TextColor, transform.position);
			}
		} else {
			FloatingTextSpawner.instance.SpawnFloatingText("Cargo full!", Color.red, transform.position);
		}
		//kill it with fire
		healthScript.Damage(healthScript.health);
	}
}
