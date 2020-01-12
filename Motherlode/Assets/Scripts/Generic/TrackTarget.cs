using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTarget : MonoBehaviour {

	public float TrackingSpeed = 30f;
	public Transform TrackedTarget;
	public Transform NullTransform;

	Vector3 firstPos;

	// Use this for initialization
	void Start () {
		firstPos = transform.position;
		firstPos.z = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (TrackedTarget == null) {
			if (NullTransform == null) {
				Track(firstPos, Time.deltaTime);
			} else Track(NullTransform.position, Time.deltaTime);
		}
		else Track(TrackedTarget.position, Time.deltaTime);
		//trackedPos.position
	}

	void Track(Vector3 pos, float delta) {
		Vector3 targetDir = pos - transform.position;
		float step = TrackingSpeed * delta;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		//Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
	}
}
