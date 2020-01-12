using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerId : MonoBehaviour {
	public XboxController controller;
	public CameraFollowTarget CameraScript;
	public SpriteRenderer GlowSprite;
	public Color p1c;
	public Color p2c;
	public Color p3c;
	public Color p4c;

	public bool Shopping = false;
	// Use this for initialization
	void Start () {
		if (GlowSprite == null) {
			GlowSprite = transform.Find("Glow").GetComponent<SpriteRenderer>();
		}
		
		switch (controller) {
			case XboxController.First: GlowSprite.color = p1c; break;
			case XboxController.Second: GlowSprite.color = p2c; break;
			case XboxController.Third: GlowSprite.color = p3c; break;
			case XboxController.Fourth: GlowSprite.color = p4c; break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetControllerInt() {
		int i = (int)controller;
		print(i);
		return i;
	}
}
