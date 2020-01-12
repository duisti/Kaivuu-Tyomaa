using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissiles : MonoBehaviour {

	public int BurstAmount = 10;
	float timeBetweenBursts = 0.2f;

	float timerThrow = 3f;
	[SerializeField]
	float timer = 25f;

	public float BaseTimer = 25f;

	public GameObject MissilePrefab;

	List<GameObject> PossibleTargets = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0f) {
			StartCoroutine(MissileCoroutine());
			timer = 999999f;
		}
	}

	IEnumerator MissileCoroutine() {
		yield return null;
		int burst = BurstAmount;
		PossibleTargets.Clear();
		Vector3 vect = transform.position;
		vect.z = 0f;
		for (int i = 0; i < GameMaster.instance.PlayerObjects.Count; i++) {
			if (GameMaster.instance.PlayerObjects[i] != null && Vector3.Distance(vect, GameMaster.instance.PlayerObjects[i].transform.position) < 100f) {
				PossibleTargets.Add(GameMaster.instance.PlayerObjects[i]);
			}
		}
		yield return null;
		while (burst > 0) {
			if (PossibleTargets.Count != 0) {
				for (int i = 0; i < PossibleTargets.Count; i++) {
					var script = Instantiate(MissilePrefab, transform.position, Quaternion.identity).GetComponent<TrackTarget>();
					script.TrackedTarget = PossibleTargets[i].transform;
				}
			} else {
				var script = Instantiate(MissilePrefab, transform.position, Quaternion.identity).GetComponent<TrackTarget>();
				script.TrackedTarget = null;
			}
			yield return new WaitForSeconds(timeBetweenBursts);
			burst--;
		}
		timer = Random.Range(-timerThrow, timerThrow) + BaseTimer;
	}
}
