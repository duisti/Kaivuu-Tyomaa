using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportCollisions : MonoBehaviour {

	Rigidbody2D rg;
	public Vector2 Velocities;
	float FallDamageMinVel = -15f;
	float FallDamageMultiplier = 2f;
	float checkPosHeight = 0.4f;
	NPCHealth healthScript;
	public PlaySoundClipImpactSoundsPlayer ImpactSoundScript;
	public List<int> ClipsToPlay;
	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody2D>();
		healthScript = GetComponent<NPCHealth>();
		if (ImpactSoundScript == null) {
			ImpactSoundScript = GetComponent<PlaySoundClipImpactSoundsPlayer>();
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Velocities = rg.velocity;
	}

	void OnCollisionEnter2D(Collision2D c) {
			Vector2 checkPos = new Vector2(transform.position.x, transform.position.y + checkPosHeight);
			if (c.contacts.Length == 0) return;
			var contact = c.contacts[0];
			Vector2 collPos = contact.point;
			if (checkPos.y > collPos.y) {
			if (Velocities.y < FallDamageMinVel) {
				float f = Mathf.Abs((Velocities.y - FallDamageMinVel) * FallDamageMultiplier);
				healthScript.Damage(f);
				if (healthScript.health > 0f)
					PlaySounds(2);
			}
			else if (Velocities.y < FallDamageMinVel / 2f) {
				PlaySounds(1);
			}
			}
		
	}

	void PlaySounds(int count) {
		if (ClipsToPlay.Count == 0 || ImpactSoundScript == null) return;
		int c = Mathf.Clamp(count, 0, ClipsToPlay.Count);
		for (int i = 0; i < c; i++) {
			if (ClipsToPlay[i] == 1) {
				ImpactSoundScript.PlaySound1();
			}
			if (ClipsToPlay[i] == 2) {
				ImpactSoundScript.PlaySound2();
			}
			if (ClipsToPlay[i] == 3) {
				ImpactSoundScript.PlaySound3();
			}
		}
	}
}
