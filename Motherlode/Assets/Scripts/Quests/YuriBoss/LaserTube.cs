using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTube : MonoBehaviour {

	LineRenderer LineRend = null;
	float Damage = 10f;
	public GameObject ExplosionEffect;

	bool shouldBeActive = false;

	public LayerMask layer;

	// Use this for initialization
	void Awake() {
		if (LineRend == null) {
			LineRend = GetComponent<LineRenderer>();
		}
		gameObject.SetActive(false);

	}

	void Start () {
		
	}

	void OnEnable() {
		shouldBeActive = true;
	}

	void OnDisable() {
		shouldBeActive = false;
		LineRend.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!shouldBeActive) {
			LineRend.enabled = shouldBeActive;
			return;
		}
		//raycast
		Vector2 transPos = new Vector2(transform.position.x, transform.position.y);
		Vector2 vect = transform.forward * 300f;
		RaycastHit2D hit = Physics2D.Raycast(transPos, transform.forward, Mathf.Infinity, layer);
		if (hit != false) {
			vect = hit.point;
			var script = hit.collider.gameObject.GetComponent<NPCHealth>();
			if (script != null) {
				shouldBeActive = false;
				Instantiate(ExplosionEffect, hit.point, Quaternion.identity);
				script.Damage(Damage);
			}
		}
		LineRend.SetPosition(0, transform.position);
		LineRend.SetPosition(1, new Vector3(vect.x, vect.y, 0f));
		LineRend.enabled = shouldBeActive;
	}
}
