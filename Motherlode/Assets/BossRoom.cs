using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour {

	public GameObject BossObject;
	public BossHealth Boss;

	public Transform BossSpawnPos;

	bool anyoneInRange;
	float rangeCheckAmount = 100f;

	bool bossActuallyDied;

	float DespawnTimer = 5f;

	public List<GameObject> GameObjectsToToggle;

	public string BossFailMusic = "empty";
	public string BossWinMusic = "victory";

	public GameObject TeleObject;

	// Use this for initialization
	void Start () {
		ToggleObjects(false);
		
	}
	void ToggleObjects(bool b) {
		for (int i = 0; i < GameObjectsToToggle.Count; i++) {
			GameObjectsToToggle[i].SetActive(b);
		}
	}

	IEnumerator EveryoneSurface() {
		yield return null;
		yield return new WaitForSeconds(15f);
		for (int i = 0; i < GameMaster.instance.PlayerObjects.Count; i++) {
			if (GameMaster.instance.PlayerObjects[i] != null) {
				Vector2 RandomSpawn = new Vector2(Random.Range(-3f, 3f), 4f);
				GameMaster.instance.PlayerObjects[i].transform.position = RandomSpawn;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (bossActuallyDied) return;
		if (BossObject == null) return;
		if (Boss == null) {
			anyoneInRange = false;
			RangeCheck();
			if (anyoneInRange) {
				Boss = Instantiate(BossObject, BossSpawnPos.position, Quaternion.identity, transform).GetComponent<BossHealth>();
				ToggleObjects(true);
			}
			return;
		}
		if (Boss.BossStatus == BossHealth.State.Dead) {
			bossActuallyDied = true;
			MusicManager.instance.PlayFile(BossWinMusic);
			StartCoroutine(EveryoneSurface());

		}
		anyoneInRange = false;
		RangeCheck();
		if (!anyoneInRange && Boss.BossStatus != BossHealth.State.Dead) {
			Boss.BossStatus = BossHealth.State.NoTargets;
		}
		if (anyoneInRange && Boss.BossStatus != BossHealth.State.Dead) {
			Boss.BossStatus = BossHealth.State.Alive;
		}
		if (Boss.BossStatus == BossHealth.State.NoTargets) {
			DespawnTimer -= Time.deltaTime;
			if (DespawnTimer <= 0f) {
				Destroy(Boss.gameObject);
				Boss = null;
				MusicManager.instance.PlayFile(BossFailMusic);
				ToggleObjects(false);
			}
		}
		else DespawnTimer = 5f;
	}

	void RangeCheck() {
		var vect = transform.position;
		for (int i = 0; i < GameMaster.instance.PlayerObjects.Count; i++) {
			if (GameMaster.instance.PlayerObjects[i] != null && Vector3.Distance(vect, GameMaster.instance.PlayerObjects[i].transform.position) < rangeCheckAmount) {
				anyoneInRange = true;
			}
		}
	}
}
