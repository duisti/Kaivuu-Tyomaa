using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D;

public class LerpStampDelay : MonoBehaviour {

	float maxEfficiencyDepth = 100f;
	float minEfficiencyDepth = 800f;
	D2dRepeatStamp script;
	float baseDelay = 0.06f;
	// Use this for initialization
	void Start () {
		script = GetComponent<D2dRepeatStamp>();
		baseDelay = script.Delay;
	}
	
	// Update is called once per frame
	void Update () {
		float d = Mathf.InverseLerp(maxEfficiencyDepth, minEfficiencyDepth, Mathf.Abs(transform.position.y)) * baseDelay;
		d = Mathf.Clamp(d, 0.01f, baseDelay);
		script.Delay = d;
	}
}
