using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTransform : MonoBehaviour {
	public Vector3 StartingRotation;
	public Vector3 EndRotation;
	Vector3 TargetRotation;
	public float RotationSpeed = 1f;

	void Awake() {
		TargetRotation = EndRotation;
		//transform.localEulerAngles = StartingRotation;
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.Abs(StartingRotation.x - EndRotation.x) * RotationSpeed * Time.deltaTime;
		float y = Mathf.Abs(StartingRotation.y - EndRotation.y) * RotationSpeed * Time.deltaTime;
		float z = Mathf.Abs(StartingRotation.z - EndRotation.z) * RotationSpeed * Time.deltaTime;
		transform.localEulerAngles = new Vector3(Mathf.MoveTowardsAngle(transform.localEulerAngles.x, TargetRotation.x, x),
			Mathf.MoveTowardsAngle(transform.localEulerAngles.y, TargetRotation.y, y),
			Mathf.MoveTowardsAngle(transform.localEulerAngles.z, TargetRotation.z, z));
		if (Mathf.Approximately(transform.localEulerAngles.x, TargetRotation.x) && 
			Mathf.Approximately(transform.localEulerAngles.y, TargetRotation.y) && 
			Mathf.Approximately(transform.localEulerAngles.z, TargetRotation.z)) {
			if (TargetRotation == EndRotation) {
				TargetRotation = StartingRotation;
			}
			else TargetRotation = EndRotation;
		}
		
	}
}
