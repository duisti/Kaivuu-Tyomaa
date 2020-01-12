using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpawner : MonoBehaviour {

	public float Radius = 2f;
	public GameObject Prefab;
	public int AmountOfExplosions = 25;

	bool spawnedLastFrame = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (AmountOfExplosions <= 0) {
			Destroy(gameObject);
			return;
		}
		//if (spawnedLastFrame) {
		//	spawnedLastFrame = false;
		//	return;
		//}
		Vector2 vect = new Vector2(Random.Range(-Radius, Radius) + transform.position.x, Random.Range(-Radius, Radius) + transform.position.y);
		Instantiate(Prefab, vect, Quaternion.identity);
		AmountOfExplosions--;
		spawnedLastFrame = true;
	}
}
