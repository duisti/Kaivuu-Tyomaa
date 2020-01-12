using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacitusMission : MonoBehaviour {
	public static TacitusMission instance;
	
	public enum QuestState {
		NotStarted,
		Started,
		Completed
	}

	bool done = false;

	public QuestState State = QuestState.NotStarted;

	public List<GameObject> Tacitus = new List<GameObject>();

	float timer = 2f;

	// Use this for initialization
	void Awake() {
		instance = this;
	}
	
	void Start() {

	}

	// Update is called once per frame
	void Update () {
		if (State == QuestState.Completed) {

			if (!done) {
				timer -= Time.deltaTime;
				if (timer <= 0f) {
					done = true;
					GameMaster.instance.Bonus.TacitusQuest[1].SetActive(true);
					//quest stuff?
				}
			}
			return;
		}
		if (Tacitus.Count != 0 && State == QuestState.NotStarted && GameMaster.instance.Bonus.TacitusQuest[0].activeSelf == true) {
			State = QuestState.Started;
			return;
		}
		if (State == QuestState.Started) {
			
			for (int i = 0; i < Tacitus.Count; i++) {
				if (Tacitus[i] == null) {
					State = QuestState.Completed;
					Tacitus.RemoveAt(i);
					return;
				}
			}
		}
	}
}
