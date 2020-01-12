using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToTacitusMission : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TacitusMission.instance.Tacitus.Add(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
