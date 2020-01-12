using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using Destructible2D;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerId))]
public class PlayerController : MonoBehaviour {


	public bool initialized = false;

	Upgrades upgrades;

	public GameObject facingObject;
	public GameObject wheelsObject;
	public GameObject drillObject;
	[HideInInspector]
	public bool facingRight = true;         // For determining which way the player is currently facing.
	float jumpTreshold = 0.05f;
	bool jumping;

	public float speed = 15f;
	float maxForce = 4.8f;

	public float jumpForce = 18f;         // Amount of force added when the player jumps.
	float maxUpwardsForce = 8f;
	public Animator WheelAnim;                  // Reference to the player's animator component.

	AudioSource engineSound;
	float maxPitchAdjust = 1f;
	
	Rigidbody2D rg;
	public GameObject jetpackSound;
	AudioSource jetSource;
	float soundBase;

	[SerializeField]
	float vertical;
	[SerializeField]
	float horizontal;
	[SerializeField]
	float jumpVertical;

	PlayerId playerID;
	XboxController controller;

	//aim indic
	float aimIndicatorDist = 1f;
	public Transform Aimer;
	// Use this for initialization
	//drill raycast stuff
	public Animator DrillAnim;
	public LayerMask drillMask;
	float rcDist = 0.8f;
	float unDeployTimer = 0.6f;
	float oldTimer;
	bool drilling = false;

	public D2dRepeatStamp DrillStamper;
	NPCHealth healthScript;
	void Awake() {
		playerID = GetComponent<PlayerId>();
		upgrades = GetComponent<Upgrades>();
		healthScript = GetComponent<NPCHealth>();
		rg = GetComponent<Rigidbody2D>();
		if (jetpackSound != null) {
			jetSource = jetpackSound.GetComponent<AudioSource>();
			soundBase = jetSource.volume;
		}
		//drillMask = LayerMask.NameToLayer("Diggable");
		oldTimer = unDeployTimer;
		engineSound = GetComponent<AudioSource>();
	}

	void Start() {
		controller = playerID.controller;
	}

	void DrillRayCast() {

		var heading = Aimer.position - drillObject.transform.position;
		var distance = heading.magnitude;
		var direction = heading / distance;
		RaycastHit2D[] hit = Physics2D.RaycastAll(drillObject.transform.position, direction, rcDist, drillMask, -5f, 5f);
		if (hit.Length != 0) {
			print("hit");
			if (!drilling) {
				DrillAnim.Play("Drill_Deploy");
			}
			unDeployTimer = oldTimer;
			drilling = true;
		}
	}
	void LateUpdate() {
		//RaycastHit2D[] hit = Physics2D.RaycastAll(drillObject.transform.position, Vector2.down, rcDist * 3f, drillMask, -5f, 5f);
		if (jumping ||
			//hit.Length == 0 || 
			Mathf.Abs(horizontal) == 0f && Mathf.Abs(vertical) == 0f) {
			if (drilling) {
				drilling = false;
				DrillAnim.Play("Drill_Undeploy");
				
				//print("stop drill, hitcount is " + hit.Length);
			}
			return;
		}
		DrillRayCast();
		if (drilling && unDeployTimer > 0f) {
			unDeployTimer -= Time.deltaTime;
			if (unDeployTimer <= 0f) {
				drilling = false;
				DrillAnim.Play("Drill_Undeploy");
			}
		}
	}

	void Update() {
		if (healthScript.State == NPCHealth.CurrentStatus.Stunned) {
			jumpVertical = 0f;
			vertical = 0f;
			horizontal = 0f;
			rg.isKinematic = false;
			return;
		}
		if (healthScript.State == NPCHealth.CurrentStatus.Teleporting) {
			jumpVertical = 0f;
			vertical = 0f;
			horizontal = 0f;
			rg.isKinematic = true;
			rg.velocity = Vector2.zero;
			return;
		}
		rg.isKinematic = false;
		jumpVertical = XCI.GetAxis(XboxAxis.RightTrigger, controller);
		vertical = XCI.GetAxis(XboxAxis.LeftStickY, controller);
		horizontal = XCI.GetAxis(XboxAxis.LeftStickX, controller);
		if (playerID.GetControllerInt() == 4) {
			jumpVertical = Input.GetAxis("Jump");
			vertical = Input.GetAxis("Vertical");
			horizontal = Input.GetAxis("Horizontal");
		}
		if (jumpVertical > jumpTreshold) {
			jumping = true;
		}
		else jumping = false;
		if (jetpackSound != null) {
			jetpackSound.gameObject.SetActive(jumping);
		}
		SetFlip(horizontal);
		ApplyAim();
		//aim
		Vector3 v3Dir = drillObject.transform.position - Aimer.position;
		float angle = Mathf.Atan2(-v3Dir.y, -v3Dir.x) * Mathf.Rad2Deg;
		//handle exeptions
		if (angle > 0f) {
			if (angle > 90f) {
				angle = Mathf.Clamp(angle, 180f, 180f);
			}
			else angle = 0f;
		} else 
		if (angle < 0f) {
			if (angle < -45f && angle > -90f) {
				horizontal = 0f;
				angle = -90f;
			} else if (angle > -45f) {
				angle = 0f;
			}
			if (angle > -135f && angle < -90f) {
				horizontal = 0f;
				angle = -90f;
			}
			else if (angle < -135f) {
				angle = 180f;
			}

		}
		drillObject.transform.eulerAngles = new Vector3(0, 0, angle);
		if (WheelAnim != null) {
			WheelAnim.speed = Mathf.Abs(horizontal) * 2f;
		}
		float f = Mathf.MoveTowards(engineSound.pitch, Mathf.Abs(horizontal) * 2f, maxPitchAdjust * Time.deltaTime);
		engineSound.pitch = Mathf.Clamp(f, 0.25f, 1f);
		//drillObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		//update stats
		UpdateStats();
	}

	void UpdateStats() {
		DrillStamper.Hardness = UpgradesData.instance.DrillUpgrades[upgrades.CurrentDrill];
		jumpForce = UpgradesData.instance.JumpjetUpgrades[upgrades.CurrentJumpJet];
		maxUpwardsForce = jumpForce / 2f;
	}

	void FixedUpdate() {
		if ((rg.velocity.x < maxForce && horizontal > 0) || (rg.velocity.x > -maxForce && horizontal < 0)) {
			rg.AddForce(Vector2.right * horizontal * speed);
		}
		if ((rg.velocity.y < maxUpwardsForce && jumpVertical > jumpTreshold)) {
			rg.AddForce(Vector2.up * jumpVertical * jumpForce);
			//activate Sound
			jetpackSound.transform.localScale = new Vector3(jumpVertical, jumpVertical, jumpVertical);
			jetSource.volume = jumpVertical * soundBase;
		}
	}

	void SetFlip(float input) {
		if (input == 0) {
			return;
		}
		float f = input;
		if (f < 0) {
			f = -1;
		}
		else if (f > 0) {
			f = 1;
		}
		var scale = new Vector3(Mathf.Round(f), facingObject.transform.localScale.y, facingObject.transform.localScale.z);
		facingObject.transform.localScale = scale;
		scale = new Vector3(Mathf.Round(f), wheelsObject.transform.localScale.y, wheelsObject.transform.localScale.z);
		wheelsObject.transform.localScale = scale;
	}

	public void ApplyAim() {
		Vector2 newPos = new Vector2(horizontal, vertical);
		newPos.Normalize();

		if (newPos == Vector2.zero) {
			newPos += new Vector2(newPos.x + facingObject.transform.localScale.x, newPos.y);
		}

		newPos += Vector2.up * 0.5f;
		newPos *= aimIndicatorDist;

		Aimer.localPosition = newPos;
	}
}