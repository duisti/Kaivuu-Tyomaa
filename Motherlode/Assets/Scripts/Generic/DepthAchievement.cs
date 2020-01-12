using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthAchievement : MonoBehaviour {

	public float MoneyReward = 3000f;
	public GameObject PressToCloseObject;
	public float timerForStartButton = 3f;

	public string PopSound = "button1";
	public string MusicClip = "e1m2";
	string emptyMusic = "empty";

	// Use this for initialization
	void Start () {
		PlayerBank.instance.TeamMoney += MoneyReward;
		Time.timeScale = 0f;
		MasterAudioSummoner.instance.PlayAudio(MasterAudioSummoner.instance.MenuSounds, PopSound, 1f, 0f, transform.position, null);
		if (MusicClip != "") {
			MusicManager.instance.PlayFile(MusicClip);
		}
		var s = PressToCloseObject.GetComponent<PressToClose>();
		s.Target = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerForStartButton <= 0f) {
			PressToCloseObject.SetActive(true);
		}
		else timerForStartButton -= Time.unscaledDeltaTime;
	}
}
