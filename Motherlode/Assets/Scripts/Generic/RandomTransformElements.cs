using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTransformElements : MonoBehaviour {

	public bool RandomRotation = true;
	public float RandomScaleThrow = 0.07f;
	public bool EvenScaleDistribution = false;

	// Use this for initialization
	void Awake () {
		if (RandomRotation) {
			transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, Random.Range(0, 359.9f));
		}
		if (EvenScaleDistribution) {
			float f = Random.Range(-RandomScaleThrow, RandomScaleThrow);
			transform.localScale += new Vector3(f, f, f);
		} else transform.localScale += new Vector3(Random.Range(-RandomScaleThrow, RandomScaleThrow), Random.Range(-RandomScaleThrow, RandomScaleThrow), Random.Range(-RandomScaleThrow, RandomScaleThrow));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
