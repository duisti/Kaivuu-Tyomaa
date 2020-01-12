using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c) {
		var script = c.gameObject.GetComponent<Dynamite>();
		if (script != null) {
			var dyn = c.gameObject.GetComponent<DestroyAfterTime>();
			dyn.timer = 0f;
		}
	}
}
