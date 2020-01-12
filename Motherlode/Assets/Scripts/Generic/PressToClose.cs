using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PressToClose : MonoBehaviour {
	public GameObject Target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Target == null) return;
		if (XCI.GetButtonDown(XboxButton.Start, XboxController.Any) || Input.GetKeyDown(KeyCode.Space)) {
			Time.timeScale = 1f;
			Target.SetActive(false);
		}
	}
}
