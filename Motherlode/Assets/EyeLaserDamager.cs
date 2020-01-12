using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLaserDamager : MonoBehaviour {
	public float Dps = 100f;
	float radius;

	public LayerMask layer;
	void Awake() {
		var coll = GetComponent<CircleCollider2D>();
		radius = coll.radius;
		coll.enabled = false;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, layer);
		foreach (Collider2D c in hits) {
			var script = c.GetComponent<NPCHealth>();
			if (script != null) {
				script.Damage(Dps * Time.deltaTime);
			}
		}
	}
}
