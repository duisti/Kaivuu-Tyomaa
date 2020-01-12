using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeLaser : MonoBehaviour {

	bool init;

	public Transform AimerHierarchy;
	public Transform StartOffset;
	public Transform LaserEnd;

	public LineRenderer LineRend;

	public LayerMask mask;
	public GameObject EndPrefab;
	//Transform eyeLaserStart;

	void Init() {
		//eyeLaserStart = new GameObject("EyeLaserStart").GetComponent<Transform>();
		//eyeLaserStart.transform.parent = AimerHierarchy;
		LaserEnd = Instantiate(EndPrefab, transform.position, Quaternion.identity, AimerHierarchy).GetComponent<Transform>();
		init = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	void OnDisable() {
		if (LaserEnd != null) {
			LaserEnd.gameObject.SetActive(false);
		}
		LineRend.enabled = false;
	}
	// Update is called once per frame
	void Update () {
		if (!init) {
			Init();
			return;
		}
		//RePositionEyeLaserStart();
		LaserEnd.gameObject.SetActive(true);
		var vect = StartOffset.position;
		var heading = StartOffset.forward - vect;
		var distance = heading.magnitude;
		var direction = heading / distance;
		vect += direction * 15f;
		RaycastHit hit;
		if (Physics.Raycast(StartOffset.position, StartOffset.forward, out hit, 50f, mask, QueryTriggerInteraction.Collide)) {
			vect = hit.point;
		}
		LaserEnd.transform.position = vect;
		LineRend.SetPosition(0, StartOffset.position);
		LineRend.SetPosition(1, vect);
		LineRend.enabled = true;
	}
	/*
	void RePositionEyeLaserStart() {
		eyeLaserStart.transform.position = new Vector3(StartOffset.transform.position.x, StartOffset.transform.position.y, 0f);
	}
	*/
}
