using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

	public enum State {
		Alive,
		Dead,
		NoTargets
	}
	public State BossStatus = State.Alive;

	//boss death
	float timerBetweenExplos = 0.15f;
	float exploRadius = 3f;
	public GameObject DeathExplosionPrefab;
	bool spawnExplos = false;
	public string BossDeathSound = "BossDeath";
	Vector3 fallSpeed = new Vector3(0, -2f, 0);
	Vector3 origPos;
	public Transform ExplosionsOffset;
	//boss death end

	public string MusicTrack = "BossMusicLoop";
	public string SecondMusicTrack = "Drok";
	public List<NPCHealth> BossHands;
	List<float> TrackedHealths = new List<float>();
	float dmgPerDynamite = 35f;
	public int LifePerHand = 10;
	public int LifeBoost = 10;

	public List<string> HitSounds;
	public string YurisPissed = "FoolYouCantControlMe";
	float timerToMessage = 3f;
	

	bool activatedBoost;

	public BossEyeLaserStarter Laser;
	public SpawnMissiles Missiles;
	public TrackTarget LaserTracker;
	public List<GameObject> SphereLasers;
	// Use this for initialization
	void Awake() {
		for (int i = 0; i < BossHands.Count; i++) {
			BossHands[i].MaxHealth = dmgPerDynamite * LifePerHand;
			BossHands[i].health = BossHands[i].MaxHealth;
			TrackedHealths.Add(BossHands[i].MaxHealth);
		}
		origPos = transform.position;
	}

	void Start () {
		MusicManager.instance.PlayFile(MusicTrack);
		
	}

	void Deactivate() {
		Laser.Laser.SetActive(false);
		Laser.enabled = false;
		Missiles.enabled = false;
		LaserTracker.enabled = false;
		for (int i = 0; i < SphereLasers.Count; i++) {
			SphereLasers[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (BossStatus == State.Dead) {
			if (Vector3.Distance(transform.position, origPos) > 22f) {
				spawnExplos = false;
				Destroy(gameObject);
				return;
			}
			timerBetweenExplos -= Time.deltaTime;
			if (timerBetweenExplos <= 0f) {
				timerBetweenExplos += 0.15f;
				Vector3 vect = ExplosionsOffset.position;
				vect.z = 0f;
				var rand = Random.insideUnitCircle * exploRadius;
				vect = vect + new Vector3(rand.x, rand.y, 0f);
				Instantiate(DeathExplosionPrefab, vect, Quaternion.identity);
			}
			transform.position += fallSpeed * Time.deltaTime;
			return;
		}
		int nulls = 0;
		for (int i = 0; i < BossHands.Count; i++) {
			if (BossHands[i] != null) {
				if (TrackedHealths[i] > BossHands[i].health) {
					MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.AnnouncerSounds, HitSounds[Random.Range(0, HitSounds.Count)], 1f, 0.03f, transform.position, null);
				}
				TrackedHealths[i] = BossHands[i].health;
			} else nulls++;
		}
		if (nulls > 0 && timerToMessage > 0f) {
			if (!activatedBoost) {
				//boost health
				for (int i = 0; i < BossHands.Count; i++) {
					if (BossHands[i] != null) {
						BossHands[i].MaxHealth += dmgPerDynamite * LifeBoost;
						BossHands[i].health += dmgPerDynamite * LifeBoost;
					}
				}
				activatedBoost = true;
				if (SecondMusicTrack != "") {
					MusicManager.instance.PlayFile(SecondMusicTrack);
				}
				Buffs();
			}
			
			timerToMessage -= Time.deltaTime;
			if (timerToMessage <= 0f) {
				MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.AnnouncerSounds, YurisPissed, 1f, 0f, transform.position, null);
				
			}
		}
		if (nulls == BossHands.Count) {
			BossStatus = State.Dead;
			spawnExplos = true;
			MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.AnnouncerSounds, BossDeathSound, 1f, 0f, transform.position, null);
			Deactivate();
		}
		
	}

	void Buffs() {
		Laser.BaseTimer *= 0.33f;
		Missiles.BaseTimer *= 0.7f;
		LaserTracker.TrackingSpeed *= 1.1f;
		Laser.timer += 6f;
	}
	
}
