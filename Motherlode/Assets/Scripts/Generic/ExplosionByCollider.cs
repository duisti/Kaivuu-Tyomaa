using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D;

public class ExplosionByCollider : MonoBehaviour {

	public Texture2D Stamp;
	public float TotalHpPercentageDamage = 0.1f;
	public float DamagePerExplosion = 10f;

	public List<string> SoundNames;

	public bool OnlySound = false;
	// Use this for initialization
	void Start() {
		if (!OnlySound) { 
		var circle = GetComponent<CircleCollider2D>();
		//stamp
		float radius = circle.radius * ((transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3f);
		D2dDestructible.StampAll(transform.position, new Vector2(radius, radius) * 2f, transform.localEulerAngles.z, Stamp, 1f);
		//end stamp
		//circlecast
		RaycastHit2D[] results = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, 0f);
		DamageTargets(results);
		}
		if (SoundNames.Count != 0) {
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.ExplosionSounds, SoundNames[Random.Range(0, SoundNames.Count)], 1f, 0.1f, transform.position, null);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void DamageTargets(RaycastHit2D[] hits) {
		for (int i = 0; i < hits.Length; i++) {
			Rigidbody2D rg = hits[i].transform.GetComponent<Rigidbody2D>();
			if (rg != null) {
				var heading = hits[i].transform.transform.position - transform.position;
				var distance = heading.magnitude;
				var direction = heading / distance; // This is now the normalized direction.
				float amount = (DamagePerExplosion * Vector3.Distance(hits[i].transform.transform.position, transform.position)) / 2f;
				direction *= amount;
				rg.AddForce(direction, ForceMode2D.Impulse);
			}
			var s = hits[i].transform.GetComponent<NPCHealth>();
			if (s != null) {
				s.Damage(DamagePerExplosion + s.MaxHealth * TotalHpPercentageDamage);
			}
		}
	}
}
