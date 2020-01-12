using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBank : MonoBehaviour {
	public static PlayerBank instance;
	public float TeamMoney = 0f;
	// Use this for initialization
	void Awake() {
		instance = this;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
