using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiberiumMission : MonoBehaviour {
	public static TiberiumMission instance;

	public enum QuestState {
		NotStarted,
		Started,
		Completed
	}
	
	bool done = false;
	public QuestState State = QuestState.NotStarted;

	public List<GameObject> TiberiumObjects = new List<GameObject>();

	int neededTiberium = 35;
	public int currentTiberium = 0;
	// Use this for initialization
	void Awake() {
		instance = this;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (State == QuestState.Completed) {
			if (!done) {
				done = true;
				GameMaster.instance.Bonus.TiberiumQuest[1].SetActive(true);
				//quest stuff?
			}
			return;
		}
		if (TiberiumObjects.Count != 0 && State == QuestState.NotStarted) {
			for (int i = 0; i < TiberiumObjects.Count; i++) {
					if (TiberiumObjects[i] == null) {
						State = QuestState.Started;
						GameMaster.instance.Bonus.TiberiumQuest[0].SetActive(true);
					}
				
			}
			return;
		} 
		if (State == QuestState.Started) {
			if (currentTiberium >= neededTiberium) {
				State = QuestState.Completed;
			}
			for (int i = 0; i < TiberiumObjects.Count; i++) {
				if (TiberiumObjects[i] == null) {
					currentTiberium++;
					TiberiumObjects.RemoveAt(i);
					return;
				}
			}
		}
	}
}
