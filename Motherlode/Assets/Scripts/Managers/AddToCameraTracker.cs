using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToCameraTracker : MonoBehaviour {

	// Use this for initialization

	void Start () {
		CamerasTracker.instance.AddCamera(transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
