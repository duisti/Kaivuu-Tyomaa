using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCHealth))]
public class SoundWhenDamaged : MonoBehaviour {
	float cooldown = 3f;
	float originalcooldown;

	bool played;

	float oldHealth;
	PlaySoundClipImpactSound script;
	NPCHealth health;
	// Use this for initialization
	void Awake() {
		script = GetComponent<PlaySoundClipImpactSound>();
		health = GetComponent<NPCHealth>();
		originalcooldown = cooldown;
	}
	void Start () {
		oldHealth = health.health;
	}
	
	// Update is called once per frame
	void Update () {
		if (played) {
			cooldown -= Time.deltaTime;
			if (cooldown <= 0f) {
				played = false;
			}
			return;
		}
		if (oldHealth > health.health) {
			played = true;
			cooldown = originalcooldown;
			script.PlaySound();
		}
		oldHealth = health.health;
	}
}
