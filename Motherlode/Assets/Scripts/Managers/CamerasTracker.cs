using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasTracker : MonoBehaviour {

	public static CamerasTracker instance;
	public List<Transform> Cameras = new List<Transform>();

	void Awake() {
		instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Cameras.Count == 0) return;
		for (int i = 0; i < Cameras.Count; i++) {
			if (Cameras[i] == null) {
				Cameras.RemoveAt(i);
				return;
			}
		}
	}

	public void ClearCameras() {
		Cameras.Clear();
	}
	public void AddCamera(Transform c) {
		Cameras.Add(c);
	}
}
