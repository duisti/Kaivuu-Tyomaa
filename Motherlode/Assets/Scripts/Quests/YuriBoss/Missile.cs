using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	public float Speed = 1f;
	public float SpeedPerSecond = 0.5f;
	public float MaxSpeed = 10f;
	float random = 0.02f;

	public string SoundClip = "V3";
	public GameObject Explosion;
	public float Damage = 15f;

	public LayerMask mask;

	void Awake() {
		//always set Z to 0
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		//rotate 90deg so its sideways
		transform.eulerAngles = new Vector3(0, 90f, 0f);
		MaxSpeed -= Random.Range(-random, random);

	}
	// Use this for initialization
	void Start () {
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.WeaponSounds, SoundClip, 0.6f, 0.1f, transform.position, null);
	}
	
	// Update is called once per frame
	void Update () {
		var newVector = transform.position;
		newVector += transform.forward * Speed;
		newVector.z = 0f;
		//get direction
		var heading = newVector - transform.position;
		var distance = heading.magnitude;
		var direction = heading / distance;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Vector3.Distance(transform.position, newVector), mask);
		if (hit != false) {
			//vect = hit.point;
			Instantiate(Explosion, hit.point, Quaternion.identity);
			var script = hit.collider.gameObject.GetComponent<NPCHealth>();
			if (script != null) {
				
				script.Damage(Damage);
				
			}
			Destroy(gameObject);
			return;
		}
		transform.position =newVector;
		if (Speed < MaxSpeed) {
			Speed += SpeedPerSecond * Time.deltaTime;
		}
		else Speed = MaxSpeed;
	}
}
