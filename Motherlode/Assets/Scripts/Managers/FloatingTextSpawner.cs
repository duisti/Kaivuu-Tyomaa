using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour {

	public static FloatingTextSpawner instance;
	public GameObject Prefab;

	// Use this for initialization
	void Awake() {
		instance = this;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnFloatingText(string s, Color c, Vector3 pos) {
		FloatingText script = Instantiate(Prefab, pos, Quaternion.identity).GetComponent<FloatingText>();
		script.InvokeText(s, c);
	}
}
