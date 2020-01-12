using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {
	TextMesh text;
	Vector3 velocity = new Vector3(0f, 2f, 0f);
	float fadeTimer = 2.5f;
	float originalFade;
	int SortingOrder = 150;
	// Use this for initialization
	void Awake() {
		text = GetComponent<TextMesh>();
		originalFade = fadeTimer;
		gameObject.GetComponent<MeshRenderer>().sortingOrder = SortingOrder;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Color c = text.color;
		c.a = Mathf.InverseLerp(0f, originalFade, fadeTimer);
		text.color = c;
		fadeTimer -= Time.unscaledDeltaTime;
		Vector3 vect = transform.position + (velocity * Time.unscaledDeltaTime);
		vect.z = -1f;
		transform.position = vect;
		if (fadeTimer <= 0f) {
			Destroy(gameObject);
		}
	}

	public void InvokeText(string s, Color c) {
		if (text == null) {
			text = GetComponent<TextMesh>();
		}
		text.text = s;
		text.color = c;
	}
}
