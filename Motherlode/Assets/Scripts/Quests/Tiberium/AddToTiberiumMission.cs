using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToTiberiumMission : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TiberiumMission.instance.TiberiumObjects.Add(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
