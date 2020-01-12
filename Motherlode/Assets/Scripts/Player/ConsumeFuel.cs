using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeFuel : MonoBehaviour {

	public NPCHealth FuelScript;
	[Tooltip("Per minute.")]
	public float ConsumeRate = 2f;
	public bool RateByScale = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (FuelScript == null) return;
		float f = (ConsumeRate / 60f) * Time.deltaTime;
		if (RateByScale) {
			f *= (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
		}
		FuelScript.currentFuel -= f;
	}
}
