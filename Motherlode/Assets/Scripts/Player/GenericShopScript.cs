using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericShopScript : MonoBehaviour {

	public enum ShopType {
		Huoltoasema,
		OreDump,
		UpgradeStation,
		Accessories,
		MiniHuoltsikka,
		//MiniDump
	}

	public ShopType Shop;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c) {
		var script = c.GetComponent<PlayerId>();
		if (script == null) return;
		if (script.CameraScript == null) return;
		script.CameraScript.CurrentOpenedShop = (int)Shop;
	}

	void OnTriggerExit2D(Collider2D c) {
		var script = c.GetComponent<PlayerId>();
		if (script == null) return;
		if (script.CameraScript == null) return;
		script.CameraScript.CurrentOpenedShop = 100;
	}


}
