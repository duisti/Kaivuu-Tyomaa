using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffWhenTooFar : MonoBehaviour {

	[Tooltip("Can be null, if null, first child is selected. If still null, will return on every update frame.")]
	public GameObject ChildObject;
	public float ClosingDistance = 60f;

	// Use this for initialization
	void Start () {
		if (ChildObject == null) {
			ChildObject = transform.GetChild(0).gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (ChildObject == null) return;
		if (CamerasTracker.instance == null) return;
		int count = CamerasTracker.instance.Cameras.Count;
		if (count > 0) {
			for (int i = 0; i < count; i++) {
				if (Vector3.Distance(transform.position, CamerasTracker.instance.Cameras[i].transform.position) < ClosingDistance) {
					ChildObject.SetActive(true);
					return;
				}
			}
		}
		ChildObject.SetActive(false);
	}
}
